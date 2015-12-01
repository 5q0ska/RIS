using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BObjednavka_menu
    {
        public int id_polozky { get; set; }
        public int id_menu { get; set; }
        public int id_objednavky { get; set; }
        public int id_podniku { get; set; }

        public BMenu menu { get; set; }
        public BObjednavka objednavka { get; set; }

        private objednavka_menu entityObjednavkaMenu;

        public BObjednavka_menu(objednavka_menu om)
        {
            id_polozky = om.id_polozky;
            id_menu = om.id_menu;
            id_objednavky = om.id_objednavky;
            id_podniku = om.id_podniku;

            menu = new BMenu(om.menu);
            objednavka = new BObjednavka(om.objednavka);

            entityObjednavkaMenu = om;
        }
    }
}
