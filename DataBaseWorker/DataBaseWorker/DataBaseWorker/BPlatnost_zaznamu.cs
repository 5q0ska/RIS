using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    class BPlatnost_zaznamu
    {
        public int typ_platnosti { get; set; }

        public ICollection<BMenu> menu { get; set; }

        private platnost_zaznamu entityPlatnostZaznamu;

        public BPlatnost_zaznamu(platnost_zaznamu pz)
        {
            typ_platnosti = pz.typ_platnosti;
            menu = new List<BMenu>();
            foreach (var menu1 in pz.menu)
            {
                BMenu pom = new BMenu(menu1);
                menu.Add(pom);
            }

            entityPlatnostZaznamu = pz;
        }
    }
}
