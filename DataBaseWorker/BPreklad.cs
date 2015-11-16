using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BPreklad
    {
        public string kod_jazyka { get; set; }
        public int text_id { get; set; }
        public string preklad1 { get; set; }

        public BJazyk jazyk { get; set; }
        public BText text { get; set; }

        private preklad entityPreklad;

        public BPreklad(preklad p)
        {
            kod_jazyka = p.kod_jazyka;
            text_id = p.text_id;
            preklad1 = p.preklad1;

            jazyk = new BJazyk(p.jazyk);
            text = new BText(p.text);

            entityPreklad = p;
        }
    }
}
