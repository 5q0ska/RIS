using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BObrazok
    {
        public int id_obrazka { get; set; }
        public string metadata { get; set; }

        public ICollection<BAkcia> akcia { get; set; }
        public ICollection<BDenne_menu> denne_menu { get; set; }
        public ICollection<BMenu> menu { get; set; }

        public obrazok entityObrazok { get; set; }

        public BObrazok()
        {

        }

        public BObrazok(obrazok o)
        {
            id_obrazka = o.id_obrazka;
            metadata = o.metadata;

            akcia = new List<BAkcia>();
            foreach (var akcia1 in o.akcia)
            {
                BAkcia pom = new BAkcia(akcia1);
                akcia.Add(pom);
            }
            denne_menu = new List<BDenne_menu>();
            foreach (var denneMenu in o.denne_menu)
            {
                BDenne_menu pom = new BDenne_menu(denneMenu);
                denne_menu.Add(pom);
            }
            menu = new List<BMenu>();
            foreach (var menu1 in o.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }
            entityObrazok = o;
        }
    }
}
