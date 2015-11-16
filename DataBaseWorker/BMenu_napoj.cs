using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    class BMenu_napoj
    {
        public int id_napoja { get; set; }
        public int id_menu { get; set; }
        public double cena { get; set; }
        public double mnozstvo { get; set; }
        public int id_podniku { get; set; }

        public BMenu menu { get; set; }
        public BNapoj napoj { get; set; }

        private menu_napoj entityMenuNapoj;

        public BMenu_napoj(menu_napoj mn)
        {
            id_napoja = mn.id_napoja;
            id_menu = mn.id_menu;
            cena = mn.cena;
            mnozstvo = mn.mnozstvo;
            id_podniku = mn.id_podniku;

            napoj = new BNapoj(mn.napoj);
            menu = new BMenu(mn.menu);

            entityMenuNapoj = mn;
        }
    }
}
