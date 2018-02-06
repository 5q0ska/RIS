using System.Collections.Generic;
using System.ServiceModel;
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
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "stoly")]
        ICollection<TStol> Stoly();

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
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/{id}/polozky")]
        ICollection<TObjednavkaMenu> PolozkyObjednavky(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavky/neuvarene")]
        ICollection<TObjednavka> NeuvareneJedla();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/nova")]
        TObjednavka VytvorObjednavku(int stol, int ucet, double suma);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/polozka/nova")]
        TObjednavkaMenu PridajPolozku(int objednavka, int podnik, int menu, int jedlo);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/polozka")]
        TObjednavkaMenu ZmenMnoztvo(int id, int mnozstvo);
    }
}
