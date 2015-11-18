using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;

namespace DataBaseWorker
{
    public class BMenu_jedlo
    {
        public int id_jedla { get; set; }
        public int id_menu { get; set; }
        public double cena { get; set; }
        public int id_typu { get; set; }
        public int id_podniku { get; set; }

        public BJedlo jedlo { get; set; }
        public BMenu menu { get; set; }

        public menu_jedlo entityMenuJedlo { get; set; }

        public BMenu_jedlo()
        {
            this.Reset();
        }

        public BMenu_jedlo(menu_jedlo mj)
        {
            id_jedla = mj.id_jedla;
            id_menu = mj.id_menu;
            cena = mj.cena;
            id_typu = mj.id_typu;
            id_podniku = mj.id_podniku;

            jedlo = new BJedlo(mj.jedlo);
            menu = new BMenu(mj.menu);

            entityMenuJedlo = mj;
        }

        private void Reset()
        {
            id_jedla = 0;
            id_menu = 0;
            cena = 0;
            id_typu = 0;
            id_podniku = 0;

            jedlo = new BJedlo();
            menu = new BMenu();

            entityMenuJedlo = new menu_jedlo();
        }

        private void FillBObject()
        {
            id_jedla = entityMenuJedlo.id_jedla;
            id_menu = entityMenuJedlo.id_menu;
            cena = entityMenuJedlo.cena;
            id_typu = entityMenuJedlo.id_typu;
            id_podniku = entityMenuJedlo.id_podniku;

            jedlo = new BJedlo(entityMenuJedlo.jedlo);
            menu = new BMenu(entityMenuJedlo.menu);
        }

        private void FillEntity()
        {
            entityMenuJedlo.id_jedla = id_jedla;
            entityMenuJedlo.id_menu = id_menu;
            entityMenuJedlo.cena = cena;
            entityMenuJedlo.id_typu = id_typu;
            entityMenuJedlo.id_podniku = id_podniku;

            entityMenuJedlo.jedlo = jedlo.entityJedlo;
            entityMenuJedlo.menu = menu.entityMenu;
        }

        public bool Save(risTabulky risContext)
        {
            bool success = false;

            try
            {
                var temp = from a in risContext.menu_jedlo where a.id_menu == id_menu &&
                               a.id_jedla == id_jedla select a;

                if (!temp.Any()) // INSERT
                {
                    this.FillEntity();
                    risContext.menu_jedlo.Add(entityMenuJedlo);
                    risContext.SaveChanges();
                    success = true;
                }
                else // UPDATE
                {
                    entityMenuJedlo = temp.Single();
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
                var temp = risContext.menu_jedlo.First(i => i.id_menu == id_menu &&
                               i.id_jedla == id_jedla);
                risContext.menu_jedlo.Remove(temp);
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

        public bool Get(risTabulky risContext, int idMenu, int idJedla)
        {
            bool success = false;
            try
            {
                var temp = from a in risContext.menu_jedlo where a.id_menu == idMenu && 
                               a.id_jedla == idJedla select a;
                entityMenuJedlo = temp.Single();
                this.FillBObject();
                success = true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("{0}.{1}", this.GetType(), "Get()"), ex);
            }

            return success;
        }

        public class BMenu_jedloCol : Dictionary<string, BMenu_jedlo>
        {

            public BMenu_jedloCol()
            {
            }

            public bool GetAll(risTabulky risContext)
            {
                try
                {
                    var temp = from a in risContext.menu_jedlo select a;
                    List<menu_jedlo> tempList = temp.ToList();
                    foreach (var a in tempList)
                    {
                        this.Add(a.id_menu + "," + a.id_jedla, new BMenu_jedlo(a));
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
