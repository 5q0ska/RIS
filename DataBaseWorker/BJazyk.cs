using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    class BJazyk
    {
        public string kod_jazyka { get; set; }
        public string nazov { get; set; }

        public ICollection<BPreklad> preklad { get; set; }

        private jazyk entityJazyk;

        public BJazyk(jazyk j)
        {
            kod_jazyka = j.kod_jazyka;
            nazov = j.nazov;

            preklad = new List<BPreklad>();
            foreach (var p in j.preklad)
            {
                BPreklad pom = new BPreklad(p);
                preklad.Add(pom);
            }

            entityJazyk = j;
        }
    }
}
