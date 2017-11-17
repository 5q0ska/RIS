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

            return (TJedlo) bjedla.FirstOrDefault(x => x.Key == Int32.Parse(id)).Value.toTransferObject("sk");
        }

        public ICollection<TJedlo> Menu()
        {
            BJedlo.BJedloCol bjedla = new BJedlo.BJedloCol(_ctx);
            bjedla.GetAll();
            
            IList<TJedlo> listJedal = bjedla.toTransferList("sk").Cast<TJedlo>().ToList();
            
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

            return objednavka.FirstOrDefault(x => x.Key == Int32.Parse(id)).Value.ToTransferObject();
        }

        public ICollection<TObjednavka> VsetkyObjednavky()
        {
            BObjednavka.BObjednavkaCol objednavka = new BObjednavka.BObjednavkaCol(_ctx);
            objednavka.GetAll();

            IList<TObjednavka> objednavky = objednavka.ToTransferList();

            return objednavky;
        }

        public TObjednavka VytvorObjednavku(int stol, int ucet, double suma)
        {
            var objednavka = new BObjednavka(stol, ucet, suma, _ctx);
            objednavka = new BObjednavka(objednavka.entityObjednavka);
            return objednavka.ToTransferObject();
        }
    }
}
