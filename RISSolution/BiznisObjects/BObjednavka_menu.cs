using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseEntities;
using TransferObjects;

namespace BiznisObjects
{
    public class BObjednavka_menu
    {
        public int id_polozky { get; set; }
        public int id_menu { get; set; }
        public int id_objednavky { get; set; }
        public int id_podniku { get; set; }
        public int id_jedla { get; set; }
        public int mnozstvo { get; set; }

        public BMenu_jedlo menu_jedlo { get; set; }
        public BObjednavka objednavka { get; set; }

        public objednavka_menu entityObjednavkaMenu { get; set; }

        public BObjednavka_menu()
        {
            this.Reset();
        }

        public BObjednavka_menu(objednavka_menu om)
        {
            entityObjednavkaMenu = om;
            FillBObject();
        }

        public BObjednavka_menu(int objednavka, int podnik, int menu, int jedlo, risTabulky risContext)
        {
            entityObjednavkaMenu = new objednavka_menu
            {
                id_objednavky = objednavka,
                id_podniku = podnik,
                id_menu = menu,
                id_jedla = jedlo,
                mnozstvo = 1
            };
            if (risContext != null)
            {
                risContext.objednavka_menu.Add(entityObjednavkaMenu);
                risContext.SaveChanges();
            }
            FillBObject();
        }

        private void Reset()
        {
            id_polozky = 0;
            id_menu = 0;
            id_objednavky = 0;
            id_podniku = 0;
            id_jedla = 0;
            mnozstvo = 0;

            //menu_jedlo = new BMenu_jedlo();
            //objednavka = new BObjednavka();

            entityObjednavkaMenu = new objednavka_menu();
        }

        private void FillBObject()
        {
            id_polozky = entityObjednavkaMenu.id_polozky;
            id_menu = entityObjednavkaMenu.id_menu;
            id_objednavky = entityObjednavkaMenu.id_objednavky;
            id_podniku = entityObjednavkaMenu.id_podniku;
            id_jedla = entityObjednavkaMenu.id_jedla;
            mnozstvo = entityObjednavkaMenu.mnozstvo;

            //menu_jedlo = new BMenu_jedlo(entityObjednavkaMenu.menu_jedlo);
            //objednavka = new BObjednavka(entityObjednavkaMenu.objednavka);
        }

        private void FillEntity()
        {
            entityObjednavkaMenu.id_polozky = id_polozky;
            entityObjednavkaMenu.id_menu = id_menu;
            entityObjednavkaMenu.id_objednavky = id_objednavky;
            entityObjednavkaMenu.id_podniku = id_podniku;
            entityObjednavkaMenu.id_jedla = id_jedla;
            entityObjednavkaMenu.mnozstvo = mnozstvo;

            //entityObjednavkaMenu.menu_jedlo = menu_jedlo.entityMenuJedlo;
            //entityObjednavkaMenu.objednavka = objednavka.entityObjednavka;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = risContext.objednavka_menu.First(i => i.id_polozky == id_polozky);

                if (temp == null) // INSERT
                {
                    this.FillEntity();
                    risContext.objednavka_menu.Add(entityObjednavkaMenu);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    entityObjednavkaMenu = temp;
                    this.FillEntity();
                    risContext.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Save()"), ex);
            }

            return success;
        }

        public bool Del(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = risContext.objednavka_menu.First(i => i.id_polozky == id_polozky);
                risContext.objednavka_menu.Remove(temp);
                risContext.SaveChanges();
                Reset();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Del()"), ex);
            }

            return success;
        }

        public bool Get(risTabulky risContext, int id_polozky)
        {
            bool success = false;
            try
            {
                var temp = risContext.objednavka_menu.First(i => i.id_polozky == id_polozky);
                if (temp == null)
                {
                    return false;
                }
                entityObjednavkaMenu = temp;
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public TObjednavkaMenu ToTransferObject()
        {
            return new TObjednavkaMenu(id_polozky, id_objednavky, id_podniku, id_menu, id_jedla, mnozstvo);
        }

        public class BObjednavka_menuCol : Dictionary<string, BObjednavka_menu>
        {

            public BObjednavka_menuCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.objednavka_menu select a;
                    List<objednavka_menu> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_menu + "," + a.id_objednavky, new BObjednavka_menu(a));
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }

        }
    }
}
