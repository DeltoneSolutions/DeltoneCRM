using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;
using System.Web.Configuration;
using System.Net;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net.Mime;
using DeltoneCRM_DAL;
using DeltoneCRM.Classes;
using System.IO;

namespace DeltoneCRM.Classes
{
    public class DeltoneQuoteEmailSender
    {
        QuoteDAL quoteDAL = new QuoteDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

        public DeltoneQuoteEmailSender()
        {
        }


        public void SendEmail(string bcc, string to, string body, string subject)
        {

        }


        public void SendMail(String strBody, String QuoteID, String strClientEmail)
        {
            String ContactName = String.Empty;
            String ContactEmail = String.Empty;

            //ContactName = orderDAL.getContactPersonForOrder(Int32.Parse(strOrderID));
            ContactEmail = quoteDAL.getContactPersonEmail(Int32.Parse(QuoteID));
           var  EmailFooter = quoteDAL.getEmailFooterText(Int32.Parse(QuoteID));
            

            var fromAddress = new MailAddress("info@deltonesolutions.com.au", "DELTONE SOLUTIONS PTY LTD");
            var toAddress = new MailAddress("sentemails@deltonesolutions.com.au", "CustomerName");
            var BccAddress = new MailAddress("sentemails@deltonesolutions.com.au", "CUSTOMER MAIL COPY");
            const String fromPassword = "deltonerep1#";
            String XeroOrderID = String.Empty;
            XeroOrderID = QuoteID;
            String subject = "Thanks for your order. REF# " + XeroOrderID;


            //Adding Image as a Embeded Image 

            String Imgview = "<img src=\"cid:companylogo\" height='80' width='780'>";
            String bottonBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";
            string body = HTMLBody(Int32.Parse(XeroOrderID), Imgview, bottonBanner);


            AlternateView avHtml = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            LinkedResource logo = new LinkedResource("C:\\temp\\DeltoneCRM\\DeltoneCRM\\Images\\top-banner-email-780.png");
            LinkedResource btmbanner = new LinkedResource("C:\\temp\\DeltoneCRM\\DeltoneCRM\\Images\\bottom-banner-email.jpg");
            logo.ContentId = "companylogo";
            btmbanner.ContentId = "bottombanner";
            avHtml.LinkedResources.Add(logo);
            avHtml.LinkedResources.Add(btmbanner);
            //string body = HTMLBODY(Int32.Parse(strOrderID), DTSNumber);

            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };


                MailMessage deltonemail = new MailMessage(fromAddress, toAddress);
                deltonemail.Subject = subject;
                deltonemail.IsBodyHtml = true;
                deltonemail.Body = body;

                deltonemail.Bcc.Add(BccAddress);

                deltonemail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess |
                    DeliveryNotificationOptions.OnFailure |
                    DeliveryNotificationOptions.Delay;

                //deltonemail.Headers.Add("Disposition-Notification-To", "sajith.jayaratne@improvata.com.au");

                deltonemail.AlternateViews.Add(avHtml);



                //deltonemail.Bcc.Add(new MailAddress("info@deltonesolutions@gmail.com");


                //using (MailMessage message = new MailMessage(fromAddress, toAddress)
                //{   

                //Subject = subject,
                //Body = body
                //})
                ///{
                smtp.Send(deltonemail);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + ":" + ex.StackTrace.ToString());
            }
        }

        public void SendMail_Old(String strBody, String QuoteID, String strClientEmail)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtpout.asia.secureserver.net");
                mail.From = new MailAddress("info@deltonesolutions.com.au");
                mail.To.Add(strClientEmail);
                mail.CC.Add("info@deltonesolutions.com.au");
                mail.Subject = "QUOTATION #: " + QuoteID;
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = strBody;
                mail.Body = htmlBody;
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public String HTMLBody(int QuoteID, String imgFile, String bottomBanner)
        {
            String output = String.Empty;
            String WhichDB = String.Empty;
            String EmailHeader = String.Empty;
            String EmailFooter = String.Empty;

            EmailHeader = quoteDAL.getEmailHeaderText(QuoteID);
            EmailFooter = quoteDAL.getEmailFooterText(QuoteID);

            List<QuoteItems> quoteList = new List<QuoteItems>();
            quoteList = getOrderedList(QuoteID);
            WhichDB = quoteDAL.getWhichDBToQuery(QuoteID);

            var quoteUser = quoteDAL.getOrderOwner(QuoteID);

            output = "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 3.2//EN'><html>";
            output = output + "<body style='font-family:Calibri;'><table align='center' cellpadding='0' cellspacing='0' width='100%'><tr><td><table align='center' cellpadding='0' cellspacing='0' width='780px' height='85px'>";
            output = output + "<tr><td>" + imgFile + "</td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>";
            output = output + EmailHeader.Replace(Environment.NewLine, "<br/>") + "</td></tr><tr><td>&nbsp;</td></tr><tr><td><table style='width:720px;'><tr>";
            output = output + "<td style='width:320px;text-align:left;font-family:Calibri;'><span style:'color:blue;font-family:Calibri;'>";
            output = output + "Model - Deltone Brand</span></td><td style='width:200px;text-align:center;font-family:Calibri;'>";
            output = output + "Quantity</td><td style='width:100px;text-align:center;font-family:Calibri;'>";
            //output = output + "Price ex. GST</td><td style='width:100px;text-align:center;font-family:Calibri;'>";
            //output = output + "Price inc. GST</td></tr><tr><td><hr></td><td><hr></td><td><hr></td><td><hr></td></tr>";

            output = output + "Unit Price Inc.GST</td><td style='width:100px;text-align:center;font-family:Calibri;'>";
            output = output + "Total Price inc. GST</td></tr><tr><td><hr></td><td><hr></td><td><hr></td><td><hr></td></tr>";

            foreach (QuoteItems item in quoteList)
            {
                output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;'>" + item.ItemDescription + "</td>";
                output = output + "<td style='width:200px;text-align:center;font-family:Calibri;'>" + item.Quantity + "</td>";

                float theUnitPrice = item.UnitPrice;
                float totalPrice = theUnitPrice * item.Quantity;

                //float theUnitPrice = item.UnitPrice;
                //float exGSTUnitPrice = theUnitPrice - ((theUnitPrice * 10) / 100);

                output = output + "<td style='width:100px;text-align:center;font-family:Calibri;'>" + String.Format("{0:C2}", item.UnitPrice) + "</td>";
                output = output + "<td style='width:100px;text-align:center;font-family:Calibri;'>" + String.Format("{0:C2}", totalPrice) + "</td></tr>";
            }
            output = output + "<tr><td>&nbsp;</td><tr>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            String ShippingCost = String.Empty;
            ShippingCost = quoteDAL.getShippingCost(QuoteID);

            String Quotetotal = String.Empty;
            Quotetotal = quoteDAL.getTotal(QuoteID);

            var quoteMember = "SALES TEAM";
            if (!string.IsNullOrEmpty(quoteUser))
                quoteMember = quoteUser;

            Char spacer = (char)13;
            output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;'>DELIVERY & HANDLING</td><td style='width:200px;text-align:center;font-family:Calibri;'>&nbsp;</td><td style='width:200px;text-align:center;font-family:Calibri;'>&nbsp;</td><td style='width:200px;text-align:center;font-family:Calibri;'>" + String.Format("{0:C2}", float.Parse(ShippingCost)) + "</td></tr>";
            output = output + "<tr><td><hr></td><td><hr></td><td><hr></td><td><hr></td></tr>";
            output = output + "<tr><td>&nbsp;</td><td>&nbsp;</td><td style='width:200px;text-align:center;font-family:Calibri;'>TOTAL INC.GST</td><td style='width:200px;text-align:center;font-family:Calibri;'>" + String.Format("{0:C2}", float.Parse(Quotetotal)) + "</td></tr>";
            output = output + "</table></td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>" + EmailFooter.Replace(spacer.ToString(), "<br/>") + "</td></tr>";

            output = output + "<tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>";
            output = output + "<tr><td style='font-family:Calibri;'>Kind Regards</td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>" + quoteMember + "</td></tr><tr><td style='font-family:Calibri;'><strong>1300 787 783</strong></td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>" + bottomBanner + "</td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";

            return output;
        }

        public List<QuoteItems> getOrderedList(int QuoteID)
        {
            List<QuoteItems> Quote_Items = new List<QuoteItems>();
            QuoteItems OrderedItem;

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "SELECT * FROM dbo.Quote_Item WHERE QuoteID = " + QuoteID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OrderedItem = new QuoteItems();
                        OrderedItem.ItemDescription = sdr["Description"].ToString();
                        OrderedItem.Quantity = float.Parse(sdr["Quantity"].ToString());
                        OrderedItem.UnitPrice = float.Parse(sdr["UnitAmount"].ToString());

                        Quote_Items.Add(OrderedItem);
                    }
                }
            }
            conn.Close();

            return Quote_Items;
        }

        public void SendMail(String strBody, int strQuoteID, String strClientEmail, string fromEmailRep = "")
        {
            String ContactEmail = String.Empty;
            ContactEmail = quoteDAL.getContactPersonEmail(strQuoteID);
            var comId = quoteDAL.getCompayID(strQuoteID);

            String theName = String.Empty;
            String theEmail = String.Empty;

            List<String> SplitString = ContactEmail.Split('|').ToList();
            theName = SplitString[1];
            theEmail = SplitString[0];
            var fromInfo = "info@deltonesolutions.com.au";
            if (!string.IsNullOrEmpty(fromEmailRep))
                fromInfo = fromEmailRep;

            var fromAddress = new MailAddress("info@deltonesolutions.com.au", "DELTONE SOLUTIONS PTY LTD");
            var toAddress = new MailAddress(theEmail, theName);
            var BccAddress = new MailAddress("sentemails@deltonesolutions.com.au", "EMAIL SENT TO CUSTOMER");



            const String fromPassword = "deltonerep1#";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);


            String Subject = "DELTONE QUOTATION #: " + strQuoteID;

            var EmailFooter = quoteDAL.getEmailFooterText(strQuoteID);
            if(EmailFooter.Contains("To confirm the order"))
                Subject = "CONFIRMATION OF ORDER #: " + strQuoteID;

            String imgView = "<img src=\"cid:companylogo\" height='80' width='780'>";
            String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";
            String body = HTMLBody(strQuoteID, imgView, bottomBanner);

            AlternateView avHTML = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            LinkedResource logo = new LinkedResource("C:\\temp\\DeltoneCRM\\DeltoneCRM\\Images\\top-banner-email-780.png");
            LinkedResource btmbanner = new LinkedResource("C:\\temp\\DeltoneCRM\\DeltoneCRM\\Images\\bottom-banner-email.jpg");
            logo.ContentId = "companylogo";
            btmbanner.ContentId = "bottombanner";
            avHTML.LinkedResources.Add(logo);
            avHTML.LinkedResources.Add(btmbanner);

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
              //  var attachements = new List<Attachment>();
                MailMessage deltonemail = new MailMessage(fromAddress, toAddress);
                deltonemail.Subject = Subject;
                deltonemail.IsBodyHtml = true;
                deltonemail.Body = body;
                deltonemail.Bcc.Add(BccAddress);

                 string fileInvoicePath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\CompanyUploads\\Quotes\\" + comId + "\\" + strQuoteID.ToString() + "\\";
               // string fileInvoicePath = "C:\\WebProjects\\DeltoneCRM-GIT\\DeltoneCRM\\CompanyUploads\\Quotes\\" + comId + "\\" + strQuoteID.ToString() + "\\";

                //if (Directory.Exists(fileInvoicePath))
                //{
                //    DirectoryInfo diSource = new DirectoryInfo(fileInvoicePath);
                //    foreach (FileInfo fi in diSource.GetFiles())
                //    {
                //        var comFilePath = fileInvoicePath + fi.Name;
                //        if (File.Exists(comFilePath))
                //        {
                //            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(comFilePath);
                //            attachment.Name = fi.Name;
                //            attachements.Add(attachment);
                //        }
                //    }
                //}

                //if (attachements != null)
                //    if (attachements.Count() > 0)
                //        foreach (var item in attachements)
                //            deltonemail.Attachments.Add(item);

                deltonemail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;

                deltonemail.AlternateViews.Add(avHTML);

                smtp.Send(deltonemail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + " : " + ex.StackTrace.ToString());
            }
        }
    }
}