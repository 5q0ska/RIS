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

        public TFood Food(string id)
        {
            BFood.BFoodCol bjedla = new BFood.BFoodCol(_ctx);
            bjedla.GetAll();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return (TFood) bjedla.FirstOrDefault(x => x.Key == Int32.Parse(id)).Value.toTransferObject("sk");
        }

        public ICollection<TFood> Menu()
        {
            BFood.BFoodCol bjedla = new BFood.BFoodCol(_ctx);
            bjedla.GetAll();
            
            IList<TFood> listJedal = bjedla.toTransferList("sk").Cast<BFood>().ToList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return listJedal;
        }

        public ICollection<TFood> DenneMenu()
        {
            throw new System.NotImplementedException();
        }

        public BFoodOrder Objednavka(string id)
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

        public TFoodOrder VytvorObjednavku(int stol, int ucet, double suma)
        {
            var objednavka = new BFoodOrder(stol, ucet, suma, _ctx);
            objednavka = new BFoodOrder(objednavka.entityFoodOrder);

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavka.ToTransferObject();
        }

        /* ercisk
        public BFoodOrder PridajPolozku(int objednavka, int podnik, int menu, int jedlo)
        {
            // Vytvorenie novej polozky v objednavke
            new BObjednavka_menu(objednavka,podnik,menu,jedlo, _ctx);

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            // nacitanie objednavky
            BFoodOrder.BFoodOrderCol objCol = new BFoodOrder.BFoodOrderCol(_ctx);
            return objCol.GetById(objednavka);
        }

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
        */ // ercisk

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
    }
}
