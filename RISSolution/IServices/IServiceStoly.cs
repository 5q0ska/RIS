using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using TransferObjects;

namespace IServices
{
    [ServiceContract]
    public interface IServiceStoly
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "food/{id}")]
        TJedlo Food(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "menu")]
        ICollection<TJedlo> Menu();

        [OperationContract] 
        ICollection<TJedlo> DenneMenu();

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "objednavka/{id}")]
        TObjednavka Objednavka(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "objednavky")]
        ICollection<TObjednavka> VsetkyObjednavky();

        [OperationContract]
        [WebInvoke(Method = "POST",
           RequestFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "objednavka/nova")]
        TObjednavka VytvorObjednavku(int stol, int ucet, double suma);
    }
}
