using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BStol
    {
        public int id_stola { get; set; }
        public int pocet_miest { get; set; }

        public ICollection<BObjednavka> objednavka { get; set; }

        private stol entityStol;

        public BStol(stol s)
        {
            id_stola = s.id_stola;
            pocet_miest = s.pocet_miest;
            objednavka = new List<BObjednavka>();
            foreach (var objednavka1 in s.objednavka)
            {
                BObjednavka pom = new BObjednavka(objednavka1);
                objednavka.Add(pom);
            }
            entityStol = s;
        }
    }
}
