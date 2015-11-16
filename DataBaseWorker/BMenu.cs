using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BMenu
    {
        public int text_id { get; set; }
        public int id_menu { get; set; }
        public int id_podniku { get; set; }
        public int id_obrazka { get; set; }
        public int nazov { get; set; }
        public int typ_platnosti { get; set; }

        public ICollection<BAkcia> akcia { get; set; }
        public ICollection<BDenne_menu> denne_menu { get; set; }
        public ICollection<BMenu_jedlo> menu_jedlo { get; set; }
        public ICollection<BMenu_napoj> menu_napoj { get; set; }
        public ICollection<BObjednavka_menu> objednavka_menu { get; set; }
        
        public BPodnik podnik { get; set; }
        public BObrazok obrazok { get; set; }
        public BText text { get; set; }
        public BText text1 { get; set; }
        public BPlatnost_zaznamu platnost_zaznamu { get; set; }

        private menu entityMenu; 

        public BMenu(menu m)
        {
            text_id = (int) m.text_id;
            id_menu = m.id_menu;
            id_podniku = m.id_podniku;
            id_obrazka = (int) m.id_obrazka;
            nazov = (int) m.nazov;
            typ_platnosti = (int) m.typ_platnosti;
            entityMenu = m;
            naplnListy();

            podnik = new BPodnik(m.podnik);
            obrazok = new BObrazok(m.obrazok);
            text = new BText(m.text);
            text1 = new BText(m.text1);
            platnost_zaznamu = new BPlatnost_zaznamu(m.platnost_zaznamu);
        }

        private void naplnListy()
        {
            akcia = new List<BAkcia>();
            foreach (var akcia1 in entityMenu.akcia)
            {
                BAkcia pom = new BAkcia(akcia1);
                akcia.Add(pom);
            }
            denne_menu = new List<BDenne_menu>();
            foreach (var denneMenu in entityMenu.denne_menu)
            {
                BDenne_menu pom = new BDenne_menu(denneMenu);
                denne_menu.Add(pom);
            }
            menu_jedlo = new List<BMenu_jedlo>();
            foreach (var menuJedlo in entityMenu.menu_jedlo)
            {
                BMenu_jedlo pom = new BMenu_jedlo(menuJedlo);
                menu_jedlo.Add(pom);
            }
            menu_napoj = new List<BMenu_napoj>();
            foreach (var menuNapoj in entityMenu.menu_napoj)
            {
                BMenu_napoj pom = new BMenu_napoj(menuNapoj);
                menu_napoj.Add(pom);
            }
            objednavka_menu = new List<BObjednavka_menu>();
            foreach (var objednavkaMenu in entityMenu.objednavka_menu)
            {
                BObjednavka_menu pom = new BObjednavka_menu(objednavkaMenu);
                objednavka_menu.Add(pom);
            }
        }
    }
}
