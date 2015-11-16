using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    class BZmena_otvaracich_hodin
    {
        public DateTime zaciatok_platnosti { get; set; }
        public int id_podniku { get; set; }
        public int cislo_dna { get; set; }
        public DateTime koniec_platnosti { get; set; }
        public DateTime cas_otvorenia { get; set; }
        public int doba_otvorenia { get; set; }

        public BDen_v_tyzdni den_v_tyzdni { get; set; }
        public BPodnik podnik { get; set; }

        private zmena_otvaracich_hodin entityZmenaOtvaracichHodin;

        public BZmena_otvaracich_hodin(zmena_otvaracich_hodin zoh)
        {
            zaciatok_platnosti = zoh.zaciatok_platnosti;
            id_podniku = zoh.id_podniku;
            cislo_dna = zoh.cislo_dna;
            koniec_platnosti = zoh.koniec_platnosti;
            cas_otvorenia = zoh.cas_otvorenia;
            doba_otvorenia = zoh.doba_otvorenia;
            den_v_tyzdni = new BDen_v_tyzdni(zoh.den_v_tyzdni);
            podnik = new BPodnik(zoh.podnik);
            entityZmenaOtvaracichHodin = zoh;
        }
    }
}
