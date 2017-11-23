using DatabaseEntities;

namespace BiznisObjects
{
    public class BMenu_jedlo
    {
        public int id_jedla { get; set; }
        public int id_menu { get; set; }
        public double cena { get; set; }
        public int id_podniku { get; set; }

        public BJedlo jedlo { get; set; }
        public BMenu menu { get; set; }

        public menu_jedlo entityMenuJedlo;

        public BMenu_jedlo()
        {
            this.Reset();
        }

        public BMenu_jedlo(menu_jedlo mj)
        {
            id_jedla = mj.id_jedla;
            id_menu = mj.id_menu;
            cena = mj.cena;
            id_podniku = mj.id_podniku;

            jedlo = new BJedlo(mj.jedlo);
            menu = new BMenu(mj.menu);

            entityMenuJedlo = mj;
        }

        private void Reset()
        {
            id_jedla = 0;
            id_menu = 0;
            cena = 0.0;
            id_podniku = 0;

            jedlo = new BJedlo();
            menu = new BMenu();
        }
    }
}
