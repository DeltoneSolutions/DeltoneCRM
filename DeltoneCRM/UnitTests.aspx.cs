/* Sample Usage of XeroIntergration Class*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Collections;
using XeroApi.Model;
using XeroApi.Model.Reporting;
using XeroApi;
using XeroApi.Model;
using XeroApi.OAuth;
using System.Security.Cryptography.X509Certificates;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth.Signing;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Core;
using Xero.Api.Example.Applications;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Serialization;
using DeltoneCRM_DAL;
using System.Web.Configuration;

namespace DeltoneCRM
{
    public partial class UnitTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Xero Model Create Contact Unit Test
            XeroIntergration xero = new XeroIntergration();

            //Create Contact
            //Repository repos = xero.CreateRepository();
           // XeroApi.Model.Contact DeltoneContact ;
           // XeroApi.Model.Item item;

            
            //DeltoneContact = xero.CreateContact(repos, "DeltoneTestCompan_5", "Shehan", "Peris", "4", "61", "2434684", String.Empty, String.Empty, String.Empty, "4", "61", "123456", "ShehanPeris@gmail.com", "12 Kauri Grove", "Spingwale", "Australia", "3150", "VIC", "26 Tamarisk Avenue", "Glen waverley", "Australia", "3150", "VIC");

            //Response.Write(DeltoneContact.ContactID);//Fetch the Contact ID Created in Xero System .store it in the Dletone Contact Table
          

             //Fetch the Contact by Guid 
             //DeltoneContact = xero.findContact(repos, "7B08CAB7-7157-4EFC-954C-59B3FC43F7C5");
             //Response.Write(DeltoneContact.FirstName);

         
            //Deltone Test Create Item
            //xero.CreateItem(repos, "DeltoneItem", "DeltoneTestDescription", (Decimal)salesUnitPrice, (Decimal)purchaseunitPrice);

           //Deltone Update Item

            //Update an Invoice 
            String strGuid="adsfhajhsdfkjhajdshf";
            //xero.UpdtateInvoice(repos, strGuid);


            //XeroIntergration xero = new XeroIntergration();
            //Repository repos = xero.CreateRepository();

            String Guid = "4dc4e979-11e5-4994-a6a9-912c102d3a13";// Expected value
            //String companyname = "REGIONAL SERVICES AUSTRALIA PTY LTD";
            String companyname = "AUSJET";
           // string ActualValue = xero.findContactByName(repos, companyname); //Actual Value

            QuoteDAL quote = new QuoteDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
           // var sss = quote.GetAllQuotesByCompany(176); //360

           // var testName = "Janine O'Connell";
          // var ttt= testName.Insert(8, "\\");

            var tttt = new CallNDataHandler();
          //  tttt.TestFileDownload();

            var mmm = new RepDayOffDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            var obj = new StatsTargetConfig()
            {
                IsReached=false,
                TargetAmount=7000,
                TargetDay=DateTime.Now.AddDays(2),
                TargetTitle="Lunch Target"
               
            };

          //  mmm.InsertIntoTargetConfig(obj, 1);

           // var result = mmm.GetAllTargetItems();

            //mmm.DeleteTargetConfig(2);

          //  mmm.UpdateTargetConfig(5, true);

            var ddd = new CreditNoteRMAHandler(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
          // var nnn= ddd.GetRMANotesById(100);

          //  SendMail();

            SendCreditCardNotificationEmail(0);
          
 
        }

        private void SendCreditCardNotificationEmail(int orderId)
        {


            ILog _logger = LogManager.GetLogger(typeof(WebForm1));

            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            OrderDAL ordal = new OrderDAL(strConnectionString);

            var strCompanyID = 1;    //ordal.getCompanyIDFromOrder(orderId);
            var comID = Convert.ToInt32(strCompanyID);
            var companyName = "Test"; //ordal.getCompanyName(comID);

            var fromInfo = "info@deltonesolutions.com.au";
            var fromAddress = new MailAddress(fromInfo, "DELTONE SOLUTIONS PTY LTD");

            var BccAddress = new MailAddress("sentemails@deltonesolutions.com.au", "EMAIL SENT TO CUSTOMER");

            var orderDts = "test111Da";

            var contactName = "Dimitri";

            //var toemail = "accounts@deltonesolutions.com.au";

            // var toemail = "paruthi001@gmail.com";
            var toemail = "dimitri@deltonesolutions.com.au";

            // var  toemail = "paruthi001@gmail.com";
            var toAddress = new MailAddress(toemail, contactName);

            const String fromPassword = "deltonerep1#";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);

            if (string.IsNullOrEmpty(orderDts))
                orderDts = orderId.ToString();

            var Subject = "Processing Credit Card Order " + orderDts;

            //  String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";
            String body = "Hi Dimitri, Credit Card Order Processing : " + orderDts;

            AlternateView avHTML = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);

            var invoiceFilePath = DelToneCommonSettings.fileInvoicePath;
            //invoiceFilePath = @"C:\inetpub\wwwroot\DeltoneCRM\Invoices\Supplier";

            //var attachements = new List<System.Net.Mail.Attachment>();

            //var invoiceFileName = "INHOUSE" + "-" + orderDts + "-" + companyName + ".pdf";
            //var invoiceFile = invoiceFilePath + invoiceFileName;
            //if (System.IO.File.Exists(invoiceFile))
            //{
            //    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(invoiceFile);
            //    attachment.Name = invoiceFileName;
            //    attachements.Add(attachment);
            //}


            try
            {
                var smtp = new SmtpClient
                {
                   // Host = "smtpout.asia.secureserver.net",
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = netWorkCrdential
                };

                MailMessage deltonemail = new MailMessage(fromAddress, toAddress);
                deltonemail.Subject = Subject;
                deltonemail.IsBodyHtml = true;
                deltonemail.Body = body;
                //  deltonemail.Bcc.Add(BccAddress);
                //  deltonemail.CC.Add("krit@deltonesolutions.com.au");
                // deltonemail.CC.Add("taras@deltonesolutions.com.au");
                //foreach (var item in attachements)
                //{
                //    deltonemail.Attachments.Add(item);
                //}
                deltonemail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;

                deltonemail.AlternateViews.Add(avHTML);

                smtp.Send(deltonemail);
                _logger.Info("Inhouse Email Notification  :" + orderDts);
            }
            catch (Exception ex)
            {
                _logger.Error(" Inhouse Error Occurred  Notification method  : " + orderId + " DTS number: " + orderDts + ex.Message);
                //Console.WriteLine(ex.Message.ToString() + " : " + ex.StackTrace.ToString());


            }


        }

       
           
            public bool SendMail()
            {
                string MailUser = "info@deltonesolutions.com.au";    //WebConfigurationManager.AppSettings["MailUser"].ToString();
                string MailPass = "deltonerep1";                            // WebConfigurationManager.AppSettings["mailPass"].ToString();
                string MailTo = "paruthi001@gmail.com";  // WebConfigurationManager.AppSettings["MailTo"].ToString();
                try
                {
                  //  ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
                  //  service.Credentials = new NetworkCredential(MailUser, MailPass);
                  ////  service.AutodiscoverUrl(MailUser);
                  //  EmailMessage emailMessage = new EmailMessage(service);
                  //  emailMessage.Subject = "Test";
                  //  emailMessage.Body = new MessageBody("Test Me");
                  //  emailMessage.ToRecipients.Add(MailTo);
                  //  emailMessage.SendAndSaveCopy();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
        

        public int GetRMAByCreditNoteIdAndSuppName()
        {
            var rmaId = 0;
             //  string UrlQuote = "http://delcrm/RepAllocateQuoteDisplay.aspx?OderID={0}&CompID={1}&cid={2}&DB={3}&Flag=Y";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var strSqlContactStmt = @"select [DELTONELIVE].[dbo].[CalendarEvent].EventId,url,[DELTONELIVE].[dbo].[CalendarEvent].CompanyId as rrr,[DELTONELIVE].[dbo].[QUOTE].QuoteID as ttt,[DELTONELIVE].[dbo].[QUOTE].ContactID 
  from [DELTONELIVE].[dbo].[CalendarEvent]   
  join [DELTONELIVE].[dbo].[QUOTE] on [DELTONELIVE].[dbo].[CalendarEvent].CompanyId=[DELTONELIVE].[dbo].[QUOTE].CompanyID
    
  where QuoteEvent=1 and convert(varchar(10),[DELTONELIVE].[dbo].[CalendarEvent].CreatedDate, 120)='2019-02-12'";
            var listEven = new List<QuoteSetVal>();
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var eventID = (reader["EventId"].ToString());
                        var companyID = (reader["rrr"].ToString());
                        var quoteID = (reader["ttt"].ToString());
                        var contactID = (reader["ContactID"].ToString());
                        string UrlQuote = "http://delcrm/RepAllocateQuoteDisplay.aspx?OderID=" + quoteID + "&CompID=" + companyID + "&cid=" + contactID + "&DB=QuoteDB&Flag=Y";
                      //  UrlQuote = string.Format(UrlQuote, quoteID, companyID, contactID, "QuoteDB");
                        var obj = new QuoteSetVal()
                        {
                            EventID = eventID,
                            Url = UrlQuote
                        };
                        listEven.Add(obj);
                    }
                }
            }
            conn.Close();

           // saveCompanyPaymentTerms(listEven);
            return rmaId;
        }

        public String saveCompanyPaymentTerms(List<QuoteSetVal> listele)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
           
            try
            {
                foreach (var itt in listele)
                {
                    String SQLStmt = "UPDATE [DELTONELIVE].[dbo].[CalendarEvent] SET url = '" + itt.Url + "' WHERE EventID = " + itt.EventID;
                    SqlCommand cmd = new SqlCommand(SQLStmt, conn);
                    conn.Open();
                    //cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return "ok";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }
    }

    public class QuoteSetVal
    {
        public string Url { get; set; }
        public string EventID { get; set; }
    }
}