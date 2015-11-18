using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BObjednavka
    {
        public int id_objednavky { get; set; }
        public int id_stola { get; set; }
        public int id_uctu { get; set; }
        public DateTime datum_objednania { get; set; }
        public int potvrdena { get; set; }
        public DateTime datum_zaplatenia { get; set; }
        public double suma { get; set; }

        public BStol stol { get; set; }
        public ICollection<BObjednavka_menu> objednavka_menu { get; set; }
        public BUcet ucet { get; set; }

        public objednavka entityObjednavka { get; set; }

        public BObjednavka()
        {
            this.Reset();
        }

        public BObjednavka(objednavka o)
        {
            id_objednavky = o.id_objednavky;
            id_stola = o.id_stola;
            id_uctu = o.id_uctu;
            datum_objednania = o.datum_objednania;
            datum_zaplatenia = (DateTime) o.datum_zaplatenia;
            potvrdena = (int) o.potvrdena;
            suma = o.suma;

            stol = new BStol(o.stol);
            ucet = new BUcet(o.ucet);

            objednavka_menu = new List<BObjednavka_menu>();
            foreach (var objednavkaMenu in o.objednavka_menu)
            {
                BObjednavka_menu pom = new BObjednavka_menu(objednavkaMenu);
                objednavka_menu.Add(pom);
            }

            entityObjednavka = o;
        }

        private void Reset()
        {
            id_objednavky = 0;
            id_stola = 0;
            id_uctu = 0;
            datum_objednania = DateTime.MinValue;
            datum_zaplatenia = DateTime.MinValue;
            potvrdena = 0;
            suma = 0;

            stol = new BStol(new stol());
            ucet = new BUcet(new ucet());

            objednavka_menu = new List<BObjednavka_menu>();

            entityObjednavka = new objednavka();
        }

        private void FillBObject()
        {
            id_objednavky = entityObjednavka.id_objednavky;
            id_stola = entityObjednavka.id_stola;
            id_uctu = entityObjednavka.id_uctu;
            datum_objednania = entityObjednavka.datum_objednania;
            datum_zaplatenia = (DateTime)entityObjednavka.datum_zaplatenia;
            potvrdena = (int)entityObjednavka.potvrdena;
            suma = entityObjednavka.suma;

            stol = new BStol(entityObjednavka.stol);
            ucet = new BUcet(entityObjednavka.ucet);

            objednavka_menu = new List<BObjednavka_menu>();
            foreach (var objednavkaMenu in entityObjednavka.objednavka_menu)
            {
                BObjednavka_menu pom = new BObjednavka_menu(objednavkaMenu);
                objednavka_menu.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityObjednavka.id_objednavky = id_objednavky;
            entityObjednavka.id_stola = id_stola;
            entityObjednavka.id_uctu = id_uctu;
            entityObjednavka.datum_objednania = datum_objednania;
            entityObjednavka.datum_zaplatenia = datum_zaplatenia;
            entityObjednavka.potvrdena = potvrdena;
            entityObjednavka.suma = suma;

            entityObjednavka.stol = stol.entityStol;
            entityObjednavka.ucet = ucet.entityUcet;

            foreach (var objednavkaMenu in objednavka_menu)
            {
                entityObjednavka.objednavka_menu.Add(objednavkaMenu.entityObjednavkaMenu);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_objednavky == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.objednavka.Add(entityObjednavka);
                    risContext.SaveChanges();
                    id_objednavky = entityObjednavka.id_objednavky;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.objednavka where a.id_objednavky == id_objednavky select a;
                    entityObjednavka = temp.Single();
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
                var temp = risContext.objednavka.First(i => i.id_objednavky == id_objednavky);
                risContext.objednavka.Remove(temp);
                risContext.SaveChanges();
                Reset();
                success = true;
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
                var temp = from a in risContext.objednavka where a.id_objednavky == id select a;
                entityObjednavka = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BObjednavkaCol : Dictionary<int, BObjednavka>
        {

            public BObjednavkaCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.objednavka select a;
                    List<objednavka> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_objednavky, new BObjednavka(a));
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
