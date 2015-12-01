using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;
using DataHolder;

namespace DataBaseWorker
{
    public class BJedlo
    {
        public int id_jedla { get; set; }
        public int nazov { get; set; }
        public int id_typu { get; set; }
        public int mnozstvo_kalorii { get; set; }
        public int dlzka_pripravy { get; set; }

        public ICollection<BMenu_jedlo> menu_jedlo
        {
            get
            {
                List<BMenu_jedlo> menu_jedlo_temp = new List<BMenu_jedlo>();
                foreach (var menuJedlo in entityJedlo.menu_jedlo)
                {
                    BMenu_jedlo pom = new BMenu_jedlo(menuJedlo);
                    menu_jedlo_temp.Add(pom);
                }
                return menu_jedlo_temp;
            }
            set { this.menu_jedlo = value; }
        }

        public ICollection<BJedlo_surovina> jedlo_surovina
        {
            get
            {
                List<BJedlo_surovina>  jedlo_surovina_temp = new List<BJedlo_surovina>();
                foreach (var jedloSurovina in entityJedlo.jedlo_surovina)
                {
                    BJedlo_surovina pom = new BJedlo_surovina(jedloSurovina);
                    jedlo_surovina_temp.Add(pom);
                }
                return jedlo_surovina_temp;
            }
            set
            {
                this.jedlo_surovina = value;
                
            }
        }

        public IList<Surovina> listSurovinyJedla(String id_jazyk)
        {
            List<Surovina> suroviny=new List<Surovina>();
            foreach (var jedloSurovina in entityJedlo.jedlo_surovina)
            {
                bool alergen = false;
                if (jedloSurovina.surovina.alergen == 1) alergen = true;
                Surovina surovina=new Surovina(jedloSurovina.surovina.id_surovina,new BText(jedloSurovina.surovina.text).getPreklad(id_jazyk),alergen,jedloSurovina.surovina.jednotka);
                suroviny.Add(surovina);
            }
            return suroviny;
        }

        public BTyp_jedla typ_jedla { get; set; }
        public BText text { get; set; }

        public jedlo entityJedlo;

        public BJedlo()
        {
            this.Reset();
        }

        public BJedlo(jedlo j)
        {
            id_jedla = j.id_jedla;
            nazov = j.nazov;
            id_typu = j.id_typu;
            if (j.mnozstvo_kalorii != null) mnozstvo_kalorii = (int) j.mnozstvo_kalorii;
            if (j.dlzka_pripravy != null) dlzka_pripravy = (int) j.dlzka_pripravy;
            

           

            typ_jedla = new BTyp_jedla(j.typ_jedla);
            this.text = new BText(j.text);

            entityJedlo = j;
        }


        private void Reset()
        {
            id_jedla = -1;
            nazov = 0;
            id_typu = 0;
            mnozstvo_kalorii = 0;
            dlzka_pripravy = 0;
            menu_jedlo = new List<BMenu_jedlo>();
            
            jedlo_surovina = new List<BJedlo_surovina>();
           
            typ_jedla = new BTyp_jedla();
            this.text = new BText();

        }

        private void FillBObject()
        {
            id_jedla = entityJedlo.id_jedla;
            nazov = entityJedlo.nazov;
            id_typu = entityJedlo.id_typu;
            if (entityJedlo.mnozstvo_kalorii != null) mnozstvo_kalorii = (int)entityJedlo.mnozstvo_kalorii;
            if (entityJedlo.dlzka_pripravy != null) dlzka_pripravy = (int)entityJedlo.dlzka_pripravy;
            menu_jedlo = new List<BMenu_jedlo>();
       /*     foreach (var menuJedlo in entityJedlo.menu_jedlo)
            {
                BMenu_jedlo pom = new BMenu_jedlo(menuJedlo);
                menu_jedlo.Add(pom);
            }

            jedlo_surovina = new List<BJedlo_surovina>();
            foreach (var jedloSurovina in entityJedlo.jedlo_surovina)
            {
                BJedlo_surovina pom = new BJedlo_surovina(jedloSurovina);
                jedlo_surovina.Add(pom);
            }*/

            typ_jedla = new BTyp_jedla(entityJedlo.typ_jedla);
            this.text = new BText(entityJedlo.text);

         }

        private void FillEntity()
        {
            entityJedlo.id_jedla = id_jedla;
            entityJedlo.id_typu = id_typu;
            entityJedlo.dlzka_pripravy = dlzka_pripravy;
            entityJedlo.mnozstvo_kalorii = mnozstvo_kalorii;
            entityJedlo.nazov = nazov;
            entityJedlo.text = text.entityText;
      /*      foreach (var menuJedlo in menu_jedlo)
            {
                entityJedlo.menu_jedlo.Add(menuJedlo.entityMenuJedlo);
            }

        
            foreach (var jedloSurovina in jedlo_surovina)
            {
                entityJedlo.jedlo_surovina.Add(jedloSurovina.entityJedloSurovina);
            }*/
        }


        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_jedla == -1) // INSERT
                {
                    this.FillEntity();
                    risContext.jedlo.Add(entityJedlo);
                    risContext.SaveChanges();
                    this.FillBObject();
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.jedlo where a.id_jedla == id_jedla select a;
                    entityJedlo = temp.Single();
                    this.FillEntity();
                    risContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Save()"), ex);
            }

            return success;
        }

        public bool Del(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = risContext.jedlo.First(i => i.id_jedla == id_jedla);
                risContext.jedlo.Remove(temp);
                risContext.SaveChanges();
                success = true;
                this.Reset();
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Del()"), ex);
            }

            return success;
        }

        public bool Get(risTabulky risContext, int id)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.jedlo where a.id_jedla == id select a;
                entityJedlo = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BJedloCol : Dictionary<int, BJedlo>
        {
            private risTabulky risContext;

            public BJedloCol(risTabulky risContext)
            {
                this.risContext = risContext;
            }

            public bool GetAll()
            {
                try
                {
                    var temp = from a in risContext.jedlo select a;
                    List<jedlo> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_jedla, new BJedlo(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            public IList<Jedlo> toList(String id_jazyka)
            {
                IList<Jedlo> result = new List<Jedlo>();
                foreach (var jedlo in this)
                {
                    result.Add(new Jedlo(jedlo.Value.id_jedla,jedlo.Value.text.getPreklad(id_jazyka),jedlo.Value.id_typu,jedlo.Value.typ_jedla.text.getPreklad(id_jazyka)));
                }
                return result;
            }

        }


    }
}
