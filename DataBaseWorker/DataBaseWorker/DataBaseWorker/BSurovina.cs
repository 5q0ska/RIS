using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BSurovina
    {
        public int id_surovina { get; set; }
        public int nazov { get; set; }
        public int alergen { get; set; }
        public string jednotka { get; set; }

        public ICollection<BJedlo_surovina> jedlo_surovina
        {
            get
            {
                ICollection<BJedlo_surovina> jedlo_surovina_temp = new List<BJedlo_surovina>();
                foreach (var jedloSurovina in entitySurovina.jedlo_surovina)
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

        public ICollection<BNapoj_surovina> napoj_surovina
        {
            get
            {
                List<BNapoj_surovina> napoj_surovina_temp = new List<BNapoj_surovina>();
                foreach (var napojSurovina in entitySurovina.napoj_surovina)
                {
                    BNapoj_surovina pom = new BNapoj_surovina(napojSurovina);
                    napoj_surovina_temp.Add(pom);
                }
                return napoj_surovina_temp;
            }
            set
            {
                this.napoj_surovina = value;
                
            }
        }

        public BText text { get; set; }

        public surovina entitySurovina;

        public BSurovina()
        {
            this.Reset();
        }

        public BSurovina(surovina s)
        {
            id_surovina = s.id_surovina;
            nazov = s.nazov;
            alergen = s.alergen;
            jednotka = s.jednotka;
            text = new BText(s.text);

        
            

            entitySurovina = s;
        }

        private void Reset()
        {
            id_surovina = -1;
            nazov = 0;
            alergen = 0;
            jednotka = null;
            text = new BText();

            jedlo_surovina = new List<BJedlo_surovina>();
            
            napoj_surovina = new List<BNapoj_surovina>();
            

        }

        private void FillBObject()
        {
            id_surovina = entitySurovina.id_surovina;
            nazov = entitySurovina.nazov;
            alergen = entitySurovina.alergen;
            jednotka = entitySurovina.jednotka;
            text = new BText(entitySurovina.text);

            jedlo_surovina = new List<BJedlo_surovina>();
       /*     foreach (var jedloSurovina in entitySurovina.jedlo_surovina)
            {
                BJedlo_surovina pom = new BJedlo_surovina(jedloSurovina);
                jedlo_surovina.Add(pom);
            }
            napoj_surovina = new List<BNapoj_surovina>();
            foreach (var napojSurovina in entitySurovina.napoj_surovina)
            {
                BNapoj_surovina pom = new BNapoj_surovina(napojSurovina);
                napoj_surovina.Add(pom);
            }*/

        }

        private void FillEntity()
        {
            entitySurovina.id_surovina = id_surovina;
            entitySurovina.alergen = alergen;
            entitySurovina.jednotka = jednotka;
            entitySurovina.nazov = nazov;
            entitySurovina.text = text.entityText;
        /*    foreach (var jedloSurovina in jedlo_surovina)
            {
                entitySurovina.jedlo_surovina.Add(jedloSurovina.entityJedloSurovina);
            }
            foreach (var napojSurovina in napoj_surovina)
            {
                entitySurovina.napoj_surovina.Add(napojSurovina.entityNapojSurovina);
            }*/


        }


        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_surovina == -1) // INSERT
                {
                    this.FillEntity();
                    risContext.surovina.Add(entitySurovina);
                    risContext.SaveChanges();
                    this.FillBObject();
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.surovina where a.id_surovina == id_surovina select a;
                    entitySurovina = temp.Single();
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
                var temp = risContext.surovina.First(i => i.id_surovina == id_surovina);
                risContext.surovina.Remove(temp);
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
                var temp = from a in risContext.surovina where a.id_surovina == id select a;
                entitySurovina = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BSurovinaCol : Dictionary<int, BSurovina>
        {

            public BSurovinaCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.surovina select a;
                    List<surovina> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_surovina, new BSurovina(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }

    }
}
