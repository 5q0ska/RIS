using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BTyp_napoja
    {
        public int id_typu { get; set; }
        public int text_id { get; set; }

        public BText text { get; set; }
        public ICollection<BNapoj> napoj { get; set; }

        private typ_napoja entityTypNapoja;

        public BTyp_napoja(typ_napoja tn)
        {
            id_typu = tn.id_typu;
            text_id = tn.text_id;
            text = new BText(tn.text);

            napoj = new List<BNapoj>();
            foreach (var napoj1 in tn.napoj)
            {
                BNapoj pom = new BNapoj(napoj1);
                napoj.Add(pom);
            }

            entityTypNapoja = tn;
        }
    }
}
