using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    class BOtvaracie_hodiny
    {
        public int id_podniku { get; set; }
        public int cislo_dna { get; set; }
        public DateTime cas_otvorenia { get; set; }
        public int doba_otvorenia { get; set; }

        public BDen_v_tyzdni den_v_tyzdni { get; set; }
        public BPodnik podnik { get; set; }

        private otvaracie_hodiny entityOtvaracieHodiny;

        public BOtvaracie_hodiny(otvaracie_hodiny oh)
        {
            id_podniku = oh.id_podniku;
            cislo_dna = oh.cislo_dna;
            cas_otvorenia = oh.cas_otvorenia;
            doba_otvorenia = oh.doba_otvorenia;
            den_v_tyzdni = new BDen_v_tyzdni(oh.den_v_tyzdni);
            this.podnik = new BPodnik(oh.podnik);
            entityOtvaracieHodiny = oh;
        }

    }
}
