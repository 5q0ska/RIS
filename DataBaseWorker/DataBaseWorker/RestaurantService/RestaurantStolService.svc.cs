using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RestaurantService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RestaurantStolService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RestaurantStolService.svc or RestaurantStolService.svc.cs at the Solution Explorer and start debugging.
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DatabaseParser;
    using DataBaseWorker;
    using DataHolder;
    using DBExecutor;
    using ICommunicationDMDB;
    using IDataExecutor;

   
        public class RestaurantStolService : ICommunicationDMDB.IRestaurantStolService
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


            public IList jedloList { get; private set; }
            //get { return risContext.jedlo; } ;
            //}
        }
    



}
