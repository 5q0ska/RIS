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

        public TJedlo Food(string id)
        {
            BJedlo.BJedloCol bjedla = new BJedlo.BJedloCol(_ctx);
            bjedla.GetAll();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return (TJedlo) bjedla.FirstOrDefault(x => x.Key == Int32.Parse(id)).Value.toTransferObject("sk");
        }

        public ICollection<TJedlo> Menu()
        {
            BJedlo.BJedloCol bjedla = new BJedlo.BJedloCol(_ctx);
            bjedla.GetAll();
            
            IList<TJedlo> listJedal = bjedla.toTransferList("sk").Cast<TJedlo>().ToList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return listJedal;
        }

        public ICollection<TJedlo> DenneMenu()
        {
            throw new System.NotImplementedException();
        }

        public TObjednavka Objednavka(string id)
        {
            BObjednavka.BObjednavkaCol objednavka = new BObjednavka.BObjednavkaCol(_ctx);
            objednavka.GetAll();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavka.FirstOrDefault(x => x.Key == Int32.Parse(id)).Value.ToTransferObject();
        }

        public ICollection<TObjednavka> VsetkyObjednavky()
        {
            BObjednavka.BObjednavkaCol objednavka = new BObjednavka.BObjednavkaCol(_ctx);
            objednavka.GetAll();

            IList<TObjednavka> objednavky = objednavka.ToTransferList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavky;
        }

        public ICollection<TObjednavkaMenu> PolozkyObjednavky(string id)
        {
            BObjednavka.BObjednavkaCol objednavka = new BObjednavka.BObjednavkaCol(_ctx);
            var obj = objednavka.GetById(Convert.ToInt32(id));

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return obj.Items;
        }

        public TObjednavka VytvorObjednavku(int stol, int ucet, double suma)
        {
            var objednavka = new BObjednavka(stol, ucet, suma, _ctx);
            objednavka = new BObjednavka(objednavka.entityObjednavka);

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavka.ToTransferObject();
        }

        public TObjednavka PridajPolozku(int objednavka, int podnik, int menu, int jedlo)
        {
            // Vytvorenie novej polozky v objednavke
            new BObjednavka_menu(objednavka,podnik,menu,jedlo, _ctx);

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");

            // nacitanie objednavky
            BObjednavka.BObjednavkaCol objCol = new BObjednavka.BObjednavkaCol(_ctx);
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

        public ICollection<TStol> Stoly()
        {
            BStol.BStolCol stol = new BStol.BStolCol();
            stol.GetAll(_ctx);

            IList<TStol> stoly = stol.ToTransferList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return stoly;
        }

        public ICollection<TObjednavka> NeuvareneJedla()
        {
            BObjednavka.BObjednavkaCol objednavka = new BObjednavka.BObjednavkaCol(_ctx);
            objednavka.GetAllNotAccepted();

            IList<TObjednavka> objednavky = objednavka.ToTransferList();

            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Origin", "*");
            return objednavky;
        }
    }
}
