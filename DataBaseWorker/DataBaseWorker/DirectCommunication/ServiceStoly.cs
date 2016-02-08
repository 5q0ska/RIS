using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DatabaseParser;
using DataBaseWorker;
using DataHolder;
using DBExecutor;
using IDataExecutor;
using IDirectCommunication;

namespace DirectCommunication
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceStoly" in both code and config file together.
    public class ServiceStoly : IServiceStoly
    {
        private IDBDataExecutor aDBExecutor = new DBDataExecutor();

        public risTabulky risContext
        {
            get { return aDBExecutor.risContext; }
        }

        public IList<Jedlo> jedlaVponuke(int id_typu, String id_jazyka)
        {
            risTabulky risContext = aDBExecutor.risContext;
            BTyp_jedla typ = new BTyp_jedla();
            typ.Get(risContext, id_typu);
            return typ.toListJedlo(id_jazyka);
        }

        public IList<Surovina> surovinyJedla(int id_jedla, String id_jazyka)
        {
            risTabulky risContext = aDBExecutor.risContext;
            BJedlo jedlo = new BJedlo();
            jedlo.Get(risContext, id_jedla);
            return jedlo.listSurovinyJedla(id_jazyka);
        }

        public IList<TypJedla> typyJedal(String id_jazyka)
        {
            risTabulky risContext = aDBExecutor.risContext;
            BTyp_jedla.BTypJedlaCol kolBTypJedlaCol = new BTyp_jedla.BTypJedlaCol(risContext);
            kolBTypJedlaCol.GetAll();
            return kolBTypJedlaCol.toList(id_jazyka);

        }

        public IList<Surovina> vsetkySuroviny(String id_jazyka,String searchString)
        {
            risTabulky risContext = aDBExecutor.risContext;
            BSurovina.BSurovinaCollection surovinaCol=new BSurovina.BSurovinaCollection();
            surovinaCol.GetNameStartingWith(searchString,risContext);
            return surovinaCol.ToList();


        }


    }
}
