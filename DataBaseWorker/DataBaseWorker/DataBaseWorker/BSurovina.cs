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

        public ICollection<BJedlo_surovina> jedlo_surovina { get; set; }
        public ICollection<BNapoj_surovina> napoj_surovina { get; set; }
        public BText text { get; set; }

        private surovina entitySurovina;

        public BSurovina(surovina s)
        {
            id_surovina = s.id_surovina;
            nazov = s.nazov;
            alergen = s.alergen;
            jednotka = s.jednotka;
            text = new BText(s.text);

            jedlo_surovina=new List<BJedlo_surovina>();
            foreach (var jedloSurovina in s.jedlo_surovina)
            {
                BJedlo_surovina pom = new BJedlo_surovina(jedloSurovina);
                jedlo_surovina.Add(pom);
            }
            napoj_surovina = new List<BNapoj_surovina>();
            foreach (var napojSurovina in s.napoj_surovina)
            {
                BNapoj_surovina pom = new BNapoj_surovina(napojSurovina);
                napoj_surovina.Add(pom);
            }

            entitySurovina = s;
        }
    }
}
