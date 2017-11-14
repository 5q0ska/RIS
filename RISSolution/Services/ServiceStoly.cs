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
    }
}
