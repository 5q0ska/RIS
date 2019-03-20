using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using TransferObjects;
using BiznisObjects;

namespace IServices
{
    [ServiceContract]
    public interface IServiceSession
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "vytvor_profil/{meno}_{priezvisko}_{email}_{heslo}")]
        BRisUser registruj(string meno, string priezvisko, string email, string heslo);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Wrapped,
            UriTemplate = "odhlas/{email}")]
        bool logOut(string email);
    }
}
