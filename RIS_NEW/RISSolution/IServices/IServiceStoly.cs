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
        TFood Food(string id);

        [OperationContract]
        [WebInvoke(Method = "GET", 
            ResponseFormat = WebMessageFormat.Json, 
            BodyStyle = WebMessageBodyStyle.Wrapped, 
            UriTemplate = "menu")]
        ICollection<TFood> Menu();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "stoly")]
        ICollection<TTable> Stoly();

        [OperationContract]
        ICollection<TFood> DenneMenu();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/{id}")]
        TFoodOrder Objednavka(string id);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavky")]
        ICollection<TFoodOrder> VsetkyObjednavky();

        /* ercisk
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/{id}/polozky")]
        ICollection<TObjednavkaMenu> PolozkyObjednavky(string id);
        */ // ercisk

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavky/neuvarene")]
        ICollection<TFoodOrder> NeuvareneJedla();

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/nova")]
        TFoodOrder VytvorObjednavku(int stol, int ucet, double suma);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/polozka/nova")]
        TFoodOrder PridajPolozku(int objednavka, int podnik, int menu, int jedlo);

        /* ercisk
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/polozka")]
        TObjednavkaMenu ZmenMnoztvo(int id, int mnozstvo);
        */ // ercisk
    }
}
