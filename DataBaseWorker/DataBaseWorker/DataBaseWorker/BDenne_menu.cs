using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BDenne_menu
    {
        public DateTime datum_platnosti { get; set; }
        public int id_menu { get; set; }
        public int id_podniku { get; set; }
        public int id_obrazka { get; set; }
        public int cena { get; set; }

        public BMenu menu { get; set; }
        public BObrazok obrazok { get; set; }

        private denne_menu entityDenneMenu;

        public BDenne_menu(denne_menu dm)
        {
            datum_platnosti = dm.datum_platnosti;
            id_menu = dm.id_menu;
            id_podniku = dm.id_podniku;
            if (dm.id_obrazka != null) id_obrazka = (int) dm.id_obrazka;
            cena = dm.cena;
            menu = new BMenu(dm.menu);
            obrazok = new BObrazok(dm.obrazok);
            entityDenneMenu = dm;
        }
    }
}
