using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseParser;
using System.Runtime.Serialization;
using System.ServiceModel;
using DataBaseWorker;
using DataHolder;


namespace IDirectCommunication
{

    
    [ServiceContract]
    public interface IServiceSprava
    {
        risTabulky risContext { get; }


        [OperationContract]
        TJedlo jedlo(int id_jedla, String id_jazyka);

        [OperationContract]
        ICollection<TJedlo> vsetkyJedla(String startingWith, String id_jazyka);

        [OperationContract]
        IList<TTypJedla> typyJedal(String id_jazyka);

        [OperationContract]
        Boolean jedlo(String session,TJedlo jedlo);

    }
}
