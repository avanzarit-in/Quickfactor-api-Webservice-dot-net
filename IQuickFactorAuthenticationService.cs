using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace QuickFactorService
{
    [ServiceContract]
    public interface IQuickFactorAuthenticationService
    {

        [OperationContract]
        [WebInvoke
        (BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET",
        UriTemplate = "/IVR_ValidatePIN.asp?NUID={nuid}&PIN={pin}")]
        System.IO.Stream ValidatePIN(string nuid, string pin);

        [OperationContract]
        [WebInvoke
        (BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET",
        UriTemplate = "/IVR_SetSecretDate.asp?NUID={nuid}&SecretDate={secretDate}&SessionID={sessionid}")]
        System.IO.Stream SetSecretDate(string nuid, string secretDate,  string sessionid);

        [OperationContract]
        [WebInvoke
        (BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET",
        UriTemplate = "/IVR_AuthenticateUser.asp?NUID={nuid}&SecretDate={secretDate}")]
        System.IO.Stream AuthenticateUser(string nuid, string secretDate);

        [OperationContract]
        [WebInvoke
        (BodyStyle = WebMessageBodyStyle.Bare,
        Method = "GET",
        UriTemplate = "/IVR_CreateUser.asp?NUID={nuid}&pin={pin}")]
        System.IO.Stream CreateUser(string nuid,string pin);
    }
}
