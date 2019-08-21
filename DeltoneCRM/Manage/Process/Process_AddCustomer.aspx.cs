using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_AddCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strCompany = Request.Form["NewCompany"].ToString();
            int AccountOwner = Int32.Parse(Request.Form["NewAccountOwner"].ToString());
            String strWebSite = String.Empty;
            var firstName = "";
            var lastName = "";
            var emailAddress = "";
            var isSkipChecked = "";
            if (!String.IsNullOrEmpty(Request.Form["FirstName"]))
            {
                firstName = Request.Form["FirstName"].ToString();
            }
            if (!String.IsNullOrEmpty(Request.Form["LastName"]))
            {
                lastName = Request.Form["LastName"].ToString();
            }
            if (!String.IsNullOrEmpty(Request.Form["EmailAddress"]))
            {
                emailAddress = Request.Form["EmailAddress"].ToString();
            }

            if (!String.IsNullOrEmpty(Request.Form["ISSkipClicked"]))
            {
                isSkipChecked = Request.Form["ISSkipClicked"].ToString();
            }




            if (!String.IsNullOrEmpty(Request.Form["NewWebsite"]))
            {
                strWebSite = Request.Form["NewWebsite"].ToString();
            }


            DeltoneCRMDAL dal = new DeltoneCRMDAL();
            String Comp_Count = dal.CompanyNameCount(strCompany);

            var userName = Session["LoggedUser"].ToString();

            if (Int32.Parse(Comp_Count) > 0)
            {
                SendNotificationEmail(strCompany, userName);
                Response.Write("ERROR");
            }
            else
            {

                if (!string.IsNullOrEmpty(isSkipChecked))
                {
                    var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                    Response.Write(dal.AddNewCompany(strCompany, strWebSite, AccountOwner, loggedInUserId));

                }
                else
                {
                    var connectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    var contactDal = new ContactDAL(connectionString);
                    var contact = contactDal.GetContactByContactNameandEmail(firstName, lastName, emailAddress);
                    if (contact.ContactId > 0)
                    {
                        SendNotificationEmail(strCompany, userName);
                        Response.Write("ERROR");
                    }
                    else
                    {
                        var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                        Response.Write(dal.AddNewCompany(strCompany, strWebSite, AccountOwner, loggedInUserId));
                    }
                }


            }

        }


        private void SendNotificationEmail(string comName, string loggUserName)
        {

            ILog _logger = LogManager.GetLogger(typeof(WebForm1));

            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);

            var fromInfo = "info@deltonesolutions.com.au";
            var fromAddress = new MailAddress(fromInfo, "DELTONE SOLUTIONS PTY LTD");

            var BccAddress = new MailAddress("sentemails@deltonesolutions.com.au", "EMAIL SENT TO CUSTOMER");




            var contactName = "Dimitri";

            var toemail = "dimitri@deltonesolutions.com.au";

         // var  toemail = "paruthi001@gmail.com";
            var toAddress = new MailAddress(toemail, contactName);

            const String fromPassword = "deltonerep1#";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);

            var Subject = "Deltone Creating A Deal";

            //  String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";
            String body = "Hi , Already Exist Company Name : " + comName + " . Action By " + loggUserName;

            AlternateView avHTML = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);


            try
            {
                var smtp = new SmtpClient
                {
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
                deltonemail.CC.Add("krit@deltonesolutions.com.au");
                deltonemail.CC.Add("taras@deltonesolutions.com.au");

                deltonemail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;

                deltonemail.AlternateViews.Add(avHTML);

                smtp.Send(deltonemail);
                _logger.Info(" Company Duplicates :" + comName + " user name:" + loggUserName);
            }
            catch (Exception ex)
            {
                _logger.Error(" Error Occurred Company Creation Notification method  :" + ex.Message);
                //Console.WriteLine(ex.Message.ToString() + " : " + ex.StackTrace.ToString());


            }

        }
    }
}