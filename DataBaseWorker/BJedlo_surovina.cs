using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BJedlo_surovina
    {
        public int id_surovina { get; set; }
        public int id_jedla { get; set; }
        public int id_typu { get; set; }
        public double mnozstvo { get; set; }

        public BJedlo jedlo { get; set; }
        public BSurovina surovina { get; set; }

        private jedlo_surovina entityJedloSurovina;

        public BJedlo_surovina(jedlo_surovina js)
        {
            id_surovina = js.id_surovina;
            id_jedla = js.id_jedla;
            id_typu = js.id_typu;
            mnozstvo = js.mnozstvo;
            
            jedlo = new BJedlo(js.jedlo);
            surovina = new BSurovina(js.surovina);

            entityJedloSurovina = js;
        }
    }
}
