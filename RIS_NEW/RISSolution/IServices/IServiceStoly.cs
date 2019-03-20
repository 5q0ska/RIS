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
            UriTemplate = "objednavky/nova/{jedlo}/{y}_{m}_{d}_{h}_{mi}_{s}_{mils}")]
        TFoodOrder VytvorObjednavku(string jedlo, string y, string m, string d, string h, string mi, string s, string mils);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavky/{objednavka}/pridat/{jedlo}")]
        TFoodOrder PridajPolozku(string objednavka, string jedlo);

        /* ercisk
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "objednavka/polozka")]
        TObjednavkaMenu ZmenMnoztvo(string id, string mnozstvo);
        */ // ercisk

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "zaplat/{objednavka}")]
        bool zaplat(string objednavka);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "odosli/{objednavka}")]
        bool odosli(string objednavka);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "vymaz_jedlo/{id_objednavky}/jedlo/{id_jedla}")]
        bool vymazJedlo(string id_objednavky, string id_jedla);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "test/{text}")]
        bool test(string text);

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "testtest")]
        bool test2();

        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "testtesttest")]
        ICollection<TAlergen> test3();

        [OperationContract]
        [WebInvoke(Method = "GET",
           ResponseFormat = WebMessageFormat.Json,
           BodyStyle = WebMessageBodyStyle.Wrapped,
           UriTemplate = "testtesttesttest")]
        ICollection<TAlergen> test4();
    }
}
