using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BDen_v_tyzdni
    {
        public int cislo_dna { get; set; }
        public int text_id { get; set; }

        public ICollection<BOtvaracie_hodiny> otvaracie_hodiny { get; set; }
        public ICollection<BZmena_otvaracich_hodin> zmena_otvaracich_hodin { get; set; }

        public BText text { get; set; }

        public den_v_tyzdni entityDenVTyzdni { get; set; }

        public BDen_v_tyzdni()
        {
            this.Reset();
        }

        public BDen_v_tyzdni(den_v_tyzdni denVTyzdni)
        {
            cislo_dna = denVTyzdni.cislo_dna;
            text_id = denVTyzdni.text_id;

            otvaracie_hodiny = new List<BOtvaracie_hodiny>();
            foreach (var otvaracieHodiny in denVTyzdni.otvaracie_hodiny)
            {
                BOtvaracie_hodiny pom = new BOtvaracie_hodiny(otvaracieHodiny);
                otvaracie_hodiny.Add(pom);
            }
            
            zmena_otvaracich_hodin = new List<BZmena_otvaracich_hodin>();
            foreach (var zmenaOtvaracichHodin in denVTyzdni.zmena_otvaracich_hodin)
            {
                BZmena_otvaracich_hodin pom = new BZmena_otvaracich_hodin(zmenaOtvaracichHodin);
                zmena_otvaracich_hodin.Add(pom);
            }

            text = new BText(denVTyzdni.text);

            entityDenVTyzdni = denVTyzdni;
        }

        
        private void Reset()
        {
            cislo_dna = 0;
            text_id = 0;
            otvaracie_hodiny = new List<BOtvaracie_hodiny>();
            zmena_otvaracich_hodin = new List<BZmena_otvaracich_hodin>();
            text = new BText();

            entityDenVTyzdni = new den_v_tyzdni();
        }

        private void FillBObject()
        {
            cislo_dna = entityDenVTyzdni.cislo_dna;
            text_id = entityDenVTyzdni.text_id;

            otvaracie_hodiny = new List<BOtvaracie_hodiny>();
            foreach (var otvaracieHodiny in entityDenVTyzdni.otvaracie_hodiny)
            {
                BOtvaracie_hodiny pom = new BOtvaracie_hodiny(otvaracieHodiny);
                otvaracie_hodiny.Add(pom);
            }

            zmena_otvaracich_hodin = new List<BZmena_otvaracich_hodin>();
            foreach (var zmenaOtvaracichHodin in entityDenVTyzdni.zmena_otvaracich_hodin)
            {
                BZmena_otvaracich_hodin pom = new BZmena_otvaracich_hodin(zmenaOtvaracichHodin);
                zmena_otvaracich_hodin.Add(pom);
            }

            text = new BText(entityDenVTyzdni.text);
        }

        private void FillEntity()
        {
            entityDenVTyzdni.cislo_dna = cislo_dna;
            entityDenVTyzdni.text_id = text_id;

            foreach (var otvaracieHodiny in otvaracie_hodiny)
            {
                entityDenVTyzdni.otvaracie_hodiny.Add(otvaracieHodiny.entityOtvaracieHodiny);
            }

            foreach (var zmenaOtvaracichHodin in zmena_otvaracich_hodin)
            {
                entityDenVTyzdni.zmena_otvaracich_hodin.Add(zmenaOtvaracichHodin.entityZmenaOtvaracichHodin);
            }

            entityDenVTyzdni.text = text.entityText;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                if (cislo_dna == 0) // INSERT
                {
                    this.FillEntity();
                    risContext.den_v_tyzdni.Add(entityDenVTyzdni);
                    risContext.SaveChanges();
                    cislo_dna = entityDenVTyzdni.cislo_dna; //treba ostestovat automaticke vygenerovanie id po ulozeni
                    success = true;
                }
                else // UPDATE
                {
                    var temp = from a in risContext.den_v_tyzdni where a.cislo_dna == cislo_dna select a;
                    entityDenVTyzdni = temp.Single();
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
                var temp = risContext.den_v_tyzdni.First(i => i.cislo_dna == cislo_dna);
                risContext.den_v_tyzdni.Remove(temp);
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

        public bool Get(risTabulky risContext, int cDna)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.den_v_tyzdni where a.cislo_dna == cDna select a;
                entityDenVTyzdni = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BDen_v_tyzdniCol : Dictionary<int, BDen_v_tyzdni>
        {

            public BDen_v_tyzdniCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.den_v_tyzdni select a;
                    List<den_v_tyzdni> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.cislo_dna, new BDen_v_tyzdni(a));
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
