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

        /* ercisk
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "stoly")]
        ICollection<TTable> Stoly();
        */ // ercisk
           
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


        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavky/neuvarene")]
        ICollection<TFoodOrder> NeuvareneJedla();
        */ // ercisk

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavky/nova/{jedlo}/{y}_{m}_{d}_{h}_{mi}_{s}_{mil}")]
        TFoodOrder VytvorObjednavku(int jedlo, int y, int m, int d, int h, int mi, int s, int mils);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavky/{objednavka}/pridat/{jedlo}")]
        TFoodOrder PridajPolozku(int objednavka, int jedlo);

        /* ercisk
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/polozka")]
        TObjednavkaMenu ZmenMnoztvo(int id, int mnozstvo);
        */ // ercisk

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "zaplat/{objednavka}")]
        bool zaplat(int objednavka);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "odosli/{objednavka}")]
        bool odosli(int objednavka);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "vymaz_jedlo/{id_objednavky}/jedlo/{id_jedla}")]
        bool vymazJedlo(int id_objednavky, int id_jedla);
    }
}
