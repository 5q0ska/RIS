using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BTyp_uctu
    {
        public int id { get; set; }
        public string nazov { get; set; }

        public ICollection<BUcet> ucet { get; set; }

        private typ_uctu entityUcet;

        public BTyp_uctu(typ_uctu tu)
        {
            id = tu.id;
            nazov = tu.nazov;

            ucet = new List<BUcet>();
            foreach (var ucet1 in tu.ucet)
            {
                BUcet pom = new BUcet(ucet1);
                ucet.Add(pom);
            }

            entityUcet = tu;
        }
    }
}
