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

        public TFoodOrder VytvorObjednavku(string id_jedla, string y, string m, string d, string h, string mi, string s, string mils)
        {

            DateTimeOffset t = new DateTimeOffset(int.Parse(y), int.Parse(m), int.Parse(d), int.Parse(h), int.Parse(mi), int.Parse(s), int.Parse(mils), TimeSpan.Zero);
            var objednavka = new BFoodOrder(id_objednavka++, int.Parse(id_jedla), t, _ctx);
            objednavka = new BFoodOrder(objednavka.entityFoodOrder);

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavka.ToTransferObject();
        }

        public TFoodOrder PridajPolozku(string objednavka, string jedlo)
        {
            // Vytvorenie novej polozky v objednavke
            new BFoodOrder(int.Parse(objednavka), int.Parse(jedlo), DateTimeOffset.Now, _ctx);

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            // nacitanie objednavky
            BFoodOrder.BFoodOrderCol objCol = new BFoodOrder.BFoodOrderCol(_ctx);
            return objCol.GetById(int.Parse(objednavka));
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

        public bool zaplat(string id_objednavky)
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

        public bool odosli(string id_objednavky)
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

        public bool vymazJedlo(string id_objednavky, string id_jedla)
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

        public bool test(string text)
        {
            Console.WriteLine("prebehol test: " + text);
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "prebehol test: " + text);
            return true;
        }

        public bool test2()
        {
            Console.WriteLine("prebehol test bez parametru");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "prebehol test bez parametru");
            return true;
        }

        public ICollection<TAlergen> test3()
        {
            Console.WriteLine("prebehol test 3");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "prebehol test 3");
            ICollection<TAlergen> ret = new List<TAlergen>();
            ret.Add(new TAlergen(133, "test"));
            return ret;
        }

        public ICollection<TAlergen> test4()
        {
            Console.WriteLine("prebehol test 4");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            ICollection<TAlergen> ret = new List<TAlergen>();
            ret.Add(new TAlergen(133, "test"));
            return ret;
        }
    }
}
