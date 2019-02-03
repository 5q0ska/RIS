using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using BiznisObjects;
using DatabaseEntities;
using IServices;
using TransferObjects;

namespace Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ServiceStoly : IServiceStoly
    {
        private readonly risTabulky _ctx = new risTabulky();
        private int id_objednavka;

        public TFood Food(string id)
        {
            BFood.BFoodCol bjedla = new BFood.BFoodCol(_ctx);
            bjedla.GetAll();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return (TFood) bjedla.FirstOrDefault(x => x.Key == Int32.Parse(id)).Value.toTransferObject();
        }

        public ICollection<TFood> Menu()
        {
            BFood.BFoodCol bjedla = new BFood.BFoodCol(_ctx);
            bjedla.GetAll();
            
            IList<TFood> listJedal = bjedla.toTransferList().Cast<TFood>().ToList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return listJedal;
        }

        public ICollection<TFood> DenneMenu()
        {
            throw new System.NotImplementedException();
        }

        public TFoodOrder Objednavka(string id)
        {
            BFoodOrder.BFoodOrderCol objednavka = new BFoodOrder.BFoodOrderCol(_ctx);
            objednavka.GetAll();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavka.FirstOrDefault(x => x.Key == Int32.Parse(id)).Value.ToTransferObject();
        }

        public ICollection<TFoodOrder> VsetkyObjednavky()
        {
            BFoodOrder.BFoodOrderCol objednavka = new BFoodOrder.BFoodOrderCol(_ctx);
            objednavka.GetAll();

            IList<TFoodOrder> objednavky = objednavka.ToTransferList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavky;
        }

        /* ercisk
        public ICollection<TObjednavkaMenu> PolozkyObjednavky(string id)
        {
            BFoodOrder.BFoodOrderCol objednavka = new BFoodOrder.BFoodOrderCol(_ctx);
            var obj = objednavka.GetById(Convert.ToInt32(id));

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return obj.Items;
        }
        */

        public TFoodOrder VytvorObjednavku(int id_jedla, int y, int m, int d, int h, int mi, int s, int mils)
        {

            DateTimeOffset t = new DateTimeOffset(y, m, d, h, mi, s, mils, TimeSpan.Zero);
            var objednavka = new BFoodOrder(id_objednavka++, id_jedla, t, _ctx);
            objednavka = new BFoodOrder(objednavka.entityFoodOrder);

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavka.ToTransferObject();
        }

        public TFoodOrder PridajPolozku(int objednavka, int jedlo)
        {
            // Vytvorenie novej polozky v objednavke
            new BFoodOrder(objednavka,jedlo, DateTimeOffset.Now, _ctx);

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            // nacitanie objednavky
            BFoodOrder.BFoodOrderCol objCol = new BFoodOrder.BFoodOrderCol(_ctx);
            return objCol.GetById(objednavka);
        }
        /* ercisk
        public TObjednavkaMenu ZmenMnoztvo(int id, int mnozstvo)
        {
            if (mnozstvo > 0)
            {
                var bObjednavkaMenu = new BObjednavka_menu();
                bObjednavkaMenu.Get(_ctx, id);
                bObjednavkaMenu.mnozstvo = mnozstvo;
                bObjednavkaMenu.Save(_ctx);
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                return bObjednavkaMenu.ToTransferObject();
            }
            else if (mnozstvo == 0)
            {
                var bObjednavkaMenu = new BObjednavka_menu();
                bObjednavkaMenu.id_polozky = id;
                bObjednavkaMenu.Del(_ctx);
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                return bObjednavkaMenu.ToTransferObject();
            }
            return null;
        }

        public ICollection<TTable> Stoly()
        {
            BTable.BTableCol stol = new BTable.BTableCol();
            stol.GetAll(_ctx);

            IList<TTable> stoly = stol.ToTransferList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return stoly;
        }

        public ICollection<TFoodOrder> NeuvareneJedla()
        {
            BFoodOrder.BFoodOrderCol objednavka = new BFoodOrder.BFoodOrderCol(_ctx);
            objednavka.GetAllNotAccepted();

            IList<TFoodOrder> objednavky = objednavka.ToTransferList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavky;
        }
        */ // ercisk

        public bool zaplat(int id_objednavky)
        {
            try
            {
                TFoodOrder temp = Objednavka(id_objednavky + "");
                temp.IsPaid = 1;
                _ctx.SaveChanges();
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool odosli(int id_objednavky)
        {
            try
            {
                TFoodOrder temp = Objednavka(id_objednavky + "");
                temp.IsSended = 1;
                _ctx.SaveChanges();
                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool vymazJedlo(int id_objednavky, int id_jedla)
        {
            try
            {
                BOrderFoods.BOrderFoodsCol objednavka = new BOrderFoods.BOrderFoodsCol();
                objednavka.GetAll(_ctx);

                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                BOrderFoods t = objednavka.FirstOrDefault(x => x.Value.FoodId == Int32.Parse(id_jedla + "") && x.Value.OrderId == Int32.Parse(id_objednavky + "")).Value.ToTransferObject();
                _ctx.order_foods.Remove(t.entityOrderFoods);
                _ctx.SaveChanges();

                WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
