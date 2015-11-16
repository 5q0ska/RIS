using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BTyp_jedla
    {
        public int id_typu { get; set; }
        public int typ { get; set; }

        public ICollection<BJedlo> jedlo { get; set; }
        public BText text { get; set; }

        private typ_jedla entityTypJedla;

        public BTyp_jedla(typ_jedla tj)
        {
            id_typu = tj.id_typu;
            typ = tj.typ;
            text = new BText(tj.text);

            jedlo = new List<BJedlo>();
            foreach (var jedlo1 in tj.jedlo)
            {
                BJedlo pom = new BJedlo(jedlo1);
                jedlo.Add(pom);
            }
            entityTypJedla = tj;
        }
    }
}
