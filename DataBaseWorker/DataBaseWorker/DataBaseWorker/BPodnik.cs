using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BPodnik
    {
        public int id_podniku { get; set; }
        public string nazov { get; set; }
        public int telefon_cislo { get; set; }
        public int ico { get; set; }
        public string adresa { get; set; }

        public virtual List<BMenu> menu { get; set; }
        public virtual List<BOtvaracie_hodiny> otvaracie_hodiny { get; set; }
        public virtual List<BZmena_otvaracich_hodin> zmena_otvaracich_hodin { get; set; }

        public podnik entityPodnik { get; set; }

        public BPodnik()
        {
            this.Reset();
        }

        public BPodnik(podnik p)
        {
            id_podniku = p.id_podniku;
            nazov = p.nazov;
            telefon_cislo = p.telefon_cislo;
            ico = p.ico;
            adresa = p.adresa;
            
            menu = new List<BMenu>();
            foreach (var menu1 in p.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }
            otvaracie_hodiny = new List<BOtvaracie_hodiny>();
            foreach (var otvaracieHodiny in p.otvaracie_hodiny)
            {
                BOtvaracie_hodiny pom = new BOtvaracie_hodiny(otvaracieHodiny);
                otvaracie_hodiny.Add(pom);
            }
            zmena_otvaracich_hodin = new List<BZmena_otvaracich_hodin>();
            foreach (var zmenaOtvaracichHodin in p.zmena_otvaracich_hodin)
            {
                BZmena_otvaracich_hodin pom = new BZmena_otvaracich_hodin(zmenaOtvaracichHodin);
                zmena_otvaracich_hodin.Add(pom);
            }
            entityPodnik = p;
        }

        private void Reset()
        {
            id_podniku = 0;
            nazov = "";
            telefon_cislo = 0;
            ico = 0;
            adresa = "";

            menu = new List<BMenu>();
            otvaracie_hodiny = new List<BOtvaracie_hodiny>();
            zmena_otvaracich_hodin = new List<BZmena_otvaracich_hodin>();
            entityPodnik = new podnik();
        }

        private void FillBObject()
        {
            id_podniku = entityPodnik.id_podniku;
            nazov = entityPodnik.nazov;
            telefon_cislo = entityPodnik.telefon_cislo;
            ico = entityPodnik.ico;
            adresa = entityPodnik.adresa;

            menu = new List<BMenu>();
            foreach (var menu1 in entityPodnik.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }
            otvaracie_hodiny = new List<BOtvaracie_hodiny>();
            foreach (var otvaracieHodiny in entityPodnik.otvaracie_hodiny)
            {
                BOtvaracie_hodiny pom = new BOtvaracie_hodiny(otvaracieHodiny);
                otvaracie_hodiny.Add(pom);
            }
            zmena_otvaracich_hodin = new List<BZmena_otvaracich_hodin>();
            foreach (var zmenaOtvaracichHodin in entityPodnik.zmena_otvaracich_hodin)
            {
                BZmena_otvaracich_hodin pom = new BZmena_otvaracich_hodin(zmenaOtvaracichHodin);
                zmena_otvaracich_hodin.Add(pom);
            }
        }

        private void FillEntity()
        {
            entityPodnik.id_podniku = id_podniku;
            entityPodnik.nazov = nazov;
            entityPodnik.telefon_cislo = telefon_cislo;
            entityPodnik.ico = ico;
            entityPodnik.adresa = adresa;

            foreach (var menu1 in menu)
            {
                entityPodnik.menu.Add(menu1.entityMenu);
            }
            foreach (var otvaracieHodiny in otvaracie_hodiny)
            {
                entityPodnik.otvaracie_hodiny.Add(otvaracieHodiny.entityOtvaracieHodiny);
            }
            foreach (var zmenaOtvaracichHodin in zmena_otvaracich_hodin)
            {
                entityPodnik.zmena_otvaracich_hodin.Add(zmenaOtvaracichHodin.entityZmenaOtvaracichHodin);
            }
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (id_podniku == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.podnik.Add(entityPodnik);
                    risContext.SaveChanges();
                    id_podniku = entityPodnik.id_podniku;
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.podnik where a.id_podniku == id_podniku select a;
                    entityPodnik = temp.Single();
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
                var temp = risContext.podnik.First(i => i.id_podniku == id_podniku);
                risContext.podnik.Remove(temp);
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
                var temp = from a in risContext.podnik where a.id_podniku == id select a;
                entityPodnik = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BPodnikCol : Dictionary<int, BPodnik>
        {

            public BPodnikCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.podnik select a;
                    List<podnik> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_podniku, new BPodnik(a));
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
