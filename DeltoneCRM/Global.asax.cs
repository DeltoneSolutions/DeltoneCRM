using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using DeltoneCRM;
using DeltoneCRM_DAL;
using System.Web.Configuration;
using System.Configuration;
using System.Net;

namespace DeltoneCRM
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
    delegate(object sender1, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                            System.Security.Cryptography.X509Certificates.X509Chain chain,
                            System.Net.Security.SslPolicyErrors sslPolicyErrors)
    {
        return true; // **** Always accept
    };
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        //Handle the Internal Exceptions, Log them and Redirect the user to Difertent URL
        void Application_Error(object sender, EventArgs e)
        {
            Exception excep = Server.GetLastError();
            HttpException lastErrorWrapper = Server.GetLastError() as HttpException;

            Exception lastError = lastErrorWrapper;
            if (lastErrorWrapper.InnerException != null)
                lastError = lastErrorWrapper.InnerException;

            string lastErrorTypeName = lastError.GetType().ToString();
            string lastErrorMessage = lastError.Message;
            string lastErrorStackTrace = lastError.StackTrace;
            
            //Log  the Error in Data Store 
            Error_Log log = new Error_Log();
            String strConnString =ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            log.Log_Error(lastErrorTypeName, lastErrorMessage, lastErrorStackTrace, DateTime.Now, strConnString);
            var _logger1 = LogManager.GetLogger(typeof(Global));
            _logger1.Error("Application error :" + excep);
            
            //Server.Transfer("~/Error/ErrorPage.aspx");
            //Response.Redirect("~/Error/ErrorPage.aspx"); //Redirect the page the Custom Error Page

        }


    }
}
