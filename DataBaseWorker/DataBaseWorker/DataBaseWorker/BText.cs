using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BText
    {
        public int text_id { get; set; }

        public ICollection<BAkcia> akcia { get; set; }
        public ICollection<BDen_v_tyzdni> den_v_tyzdni { get; set; }
        public ICollection<BJedlo> jedlo { get; set; }
        public ICollection<BMenu> menu { get; set; }
        public ICollection<BMenu> menu1 { get; set; }
        public ICollection<BNapoj> napoj { get; set; }
        public ICollection<BPreklad> preklad { get; set; }
        public ICollection<BSurovina> surovina { get; set; }
        public ICollection<BTyp_jedla> typ_jedla { get; set; }
        public ICollection<BTyp_napoja> typ_napoja { get; set; }

        public text entityText { get; set; }

        public String getPreklad(String kodJazyka)
        {
           /** var temp = from a in risContext.preklad where a.text_id == text_id && a.kod_jazyka==kodJazyka select a;
            preklad entityPreklad = temp.Single();
            return entityPreklad.preklad1;*/
            IEnumerable<string> preklad=from a in entityText.preklad.OfType<preklad>() where a.kod_jazyka == kodJazyka select a.preklad1;
            return preklad.FirstOrDefault();
        }

        public BText()
        {

        }

        public BText(text t)
        {
            text_id = t.text_id;
           
            entityText = t;
        }
    }
}
