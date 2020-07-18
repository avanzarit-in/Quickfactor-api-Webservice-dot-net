using QuickFactorService.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel.Web;
using System.Text;
using System.Web.ClientServices;

namespace QuickFactorService
{
    public class QuickFactorAuthenticationService : IQuickFactorAuthenticationService
    {
        public Stream AuthenticateUser(string nuid, string secretDate)
        {
            string status = "0";
            string result = "0";
            string sessionId = Guid.NewGuid().ToString();

            try
            {
                IList<authentication_info> authInfo = GetAuthInfoByNuid(nuid);
                if (authInfo == null || authInfo.Count() == 0)
                {
                    status = "1";
                }
                else if (authInfo.Count() > 1)
                {
                    status = "2";
                }
                else
                {
                    if (authInfo.First().ANSWER == null)
                    {
                        status = "-1";
                        result = "Secret Date is missing";
                    }
                    else
                    {
                        string storedSecretDate = ClsTripleDES.Decrypt(authInfo.First().ANSWER);
                        if (secretDate.Equals(storedSecretDate))
                        {
                            status = "0";
                        }
                        else
                        {
                            status = "-1";
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                status = "-1";
                result = ex.Message;
            }


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<title>Password Express -  IVR User Authentication</title>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=ISO-8859-1\">");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<center>");
            sb.AppendLine("<h1>IVR Authentication Web Page</h1>");
            sb.AppendFormat("<h2>Status:  {0}</h2>",status);
            sb.AppendLine("<h3>Number of Groups:  </h3>");
            sb.AppendLine("<h4>Group Names:  </h4>");
            sb.AppendFormat("<status>{0}</status>",status);
            sb.AppendFormat("<result>{0}</result>",result);
            sb.AppendFormat("<sessionID>{0}</sessionID>",sessionId);
            sb.AppendLine("<NumberOfGroups></NumberOfGroups>");
            sb.AppendLine("<GroupName></GroupName>");
            sb.AppendLine("<TargetName></TargetName>");
            sb.AppendLine("</center>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            byte[] resultBytes = Encoding.UTF8.GetBytes(sb.ToString());
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return new MemoryStream(resultBytes);
        }

        public Stream CreateUser(string nuid,string pin)
        {
            int status=0;
            try
            {
                QuickFactorAuthenticationServiceModel tstDb = new QuickFactorAuthenticationServiceModel();
                authentication_info authInfo = new authentication_info();
                authInfo.NUID = nuid;
                authInfo.PIN = pin;
                tstDb.authentication_info.Add(authInfo);
                status = tstDb.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var sqlex = ex.InnerException.InnerException as SqlException;
                if (sqlex != null)
                {
                    switch (sqlex.Number)
                    {
                        case 2627: status = 2;
                            break;
                        default: status = -1;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                status = -1;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<title>Create User</title>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=ISO-8859-1\">");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<center>");
            sb.AppendFormat("<h1>User Created {0}</h1>",status);
            sb.AppendLine("</center>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            byte[] resultBytes = Encoding.UTF8.GetBytes(sb.ToString());
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return new MemoryStream(resultBytes);
        }

        public AuthInfoDTO GetSecretDateAndPin(string nuid)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            try
            {
                IList<authentication_info> authInfo = GetAuthInfoByNuid(nuid);
                if (authInfo == null || authInfo.Count() == 0)
                {
                    return null;
                }
                else
                {
                    AuthInfoDTO result = new AuthInfoDTO();
                    result.NUID = authInfo.First().NUID;
                    result.PIN = authInfo.First().PIN;
                    if (result.PIN.Equals("DISABLED"))
                    {
                        result.ANSWER = ClsTripleDES.Decrypt(authInfo.First().ANSWER);
                    }
                    else
                    {
                        result.ANSWER = authInfo.First().ANSWER;
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public System.IO.Stream SetSecretDate(string nuid, string secretDate, string sessionId)
        {
            string status = "0";
            string result = "0";

            try
            {
                IList<authentication_info> authInfo = GetAuthInfoByNuid(nuid);
                if (authInfo == null || authInfo.Count() == 0)
                {
                    status = "1";
                }
                else if (authInfo.Count() > 1)
                {
                    status = "2";
                }
                else
                {
                    if (authInfo.First().PIN.Equals("DISABLED"))
                    {
                        status = "-1";
                        result = "User Already Registered";
                    }
                }
                string encryptSecretDate = ClsTripleDES.Encrypt(secretDate);

                try
                {
                    QuickFactorAuthenticationServiceModel tstDb = new QuickFactorAuthenticationServiceModel();
                    authInfo.First().PIN = "DISABLED";
                    authInfo.First().ANSWER = encryptSecretDate;
                    tstDb.authentication_info.AddOrUpdate(authInfo.First());
                    int saveStatus = tstDb.SaveChanges();
                }
                catch (Exception ex)
                {
                    status = "-1";
                    result = ex.Message;
                }
            }
            catch (Exception ex)
            {
                status = "-1";
                result = ex.Message;
            }           

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns = \"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<title> Password Express - IVR SetSecretDate </title>");
            sb.AppendLine("<meta http - equiv = \"Content -Type\" content = \"text/html; charset=ISO-8859-1\">");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<center>");
            sb.AppendLine("<h1> IVR Secret Date Page </h1>");
            sb.AppendFormat("<h2> Status: {0}</h2>",status);
            sb.AppendFormat("<h3> Result: {0}</h3>",result);
            sb.AppendFormat("<status> {0} </status>",status);
            sb.AppendFormat("<result>{0}</result>",result);
            sb.AppendFormat("<sessionID> {0} </sessionID>",sessionId);
            sb.AppendLine("</center>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            byte[] resultBytes = Encoding.UTF8.GetBytes(sb.ToString());
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return new MemoryStream(resultBytes);
        }

        public System.IO.Stream ValidatePIN(string nuid, string pin)
        {
            string status = "0";
            string result = "0";
            string sessionId = Guid.NewGuid().ToString();

            try
            {
                IList<authentication_info> authInfo = GetAuthInfoByNuid(nuid);
                if (authInfo == null || authInfo.Count() == 0)
                {
                    status = "1";
                }
                else if (authInfo.Count() > 1)
                {
                    status = "2";
                }
                else
                {
                    if (authInfo.First().PIN.Equals("DISABLED"))
                    {
                        status = "-1";
                        result = "User Already Registered";
                    }else if(authInfo.First().PIN != pin)
                    {
                        status = "-1";
                        result = "Pin provided does not match";
                    }
                }
            }
            catch (Exception ex)
            {
                status = "-1";
                result = ex.Message;
            }
           

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<title>Password Express -  IVR ValidatePIN</title>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=ISO-8859-1\">");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<center>");
            sb.AppendLine("<h1>IVR Validate PIN Page</h1>");
            sb.AppendFormat("<h2>Returning status: {0}</h2>",status);
            sb.AppendFormat("<h3>Returning session ID:  {0}</h3>", sessionId);
            sb.AppendFormat("<status>{0}</status>",status);
            sb.AppendFormat("<result>{0}</result>",result);
            sb.AppendFormat("<sessionID>{0}</sessionID>",sessionId);
            sb.AppendLine("</center>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            byte[] resultBytes = Encoding.UTF8.GetBytes(sb.ToString());
            WebOperationContext.Current.OutgoingResponse.ContentType = "text/html";
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.OK;
            return new MemoryStream(resultBytes);

        }

        private IList<authentication_info> GetAuthInfoByNuid(string nuid)
        {
            using (var db = new QuickFactorAuthenticationServiceModel())
            {
                IList<AuthInfoDTO> authinfo = db.authentication_info.Where(c => c.NUID.Equals(nuid)).Select(c =>
           new AuthInfoDTO
           {
               NUID = c.NUID,
               PIN = c.PIN,
               ANSWER=c.ANSWER
           }).ToList() ;
                List<authentication_info> authInfoList = new List<authentication_info>();

                foreach (var item in authinfo)
                {
                    authInfoList.Add(new authentication_info { NUID = item.NUID, PIN=item.PIN, ANSWER=item.ANSWER });
                }

                return authInfoList;
            }            
        }
    }
}
