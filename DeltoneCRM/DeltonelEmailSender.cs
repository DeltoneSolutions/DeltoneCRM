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



namespace DeltoneCRM
{
    public class DeltonelEmailSender
    {
        OrderDAL orderDAL = new OrderDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        ContactDAL contactDAL = new ContactDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        CompanyDAL companyDAL = new CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

        public DeltonelEmailSender()
        {
        }

        /*This method send Order conformation Email to Deltone Solutions */
        public void SendMail_2(String strBody, String strOrderID, String strClientEmail)
        {

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtpout.asia.secureserver.net");
                mail.From = new MailAddress("info@deltonesolutions.com.au");
                mail.To.Add(strClientEmail);
                mail.CC.Add("info@deltonesolutions.com.au"); //Another Copy to info@deltonesolutions.com.au
                mail.Subject = "NEW ORDER" + strOrderID;
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = strBody;
                mail.Body = htmlBody;
                //SmtpServer.Host = ConfigurationSettings.AppSettings["SMTP"].ToString();
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


        /*This method construct the HTML */
        public String HTMLBODY(int OrderID, String DTSNumber, String Imgfile, String bottomBanner)
        {

            String output = String.Empty;
            String OrderOwner = String.Empty;
            String PaymentTerms = String.Empty;
            String OrderAccountOwner = String.Empty;

            List<DeltoneItem> orderlist = new List<DeltoneItem>();
            orderlist = getOrderedList(OrderID);
            OrderOwner = orderDAL.getContactPersonForOrder(OrderID);
            PaymentTerms = orderDAL.getPaymentTerms(OrderID);
            OrderAccountOwner = orderDAL.getOrderCreatedBy(OrderID);
            var typeOfCall = orderDAL.getTypeOfCall(OrderID);
            output = "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 3.2//EN'><html >";
            output = output + "<body style='font-family:Calibri;'><table align='center' cellpadding='0' cellspacing='0' width='100%'><tr><td><table align='center' cellpadding='0' cellspacing='0' width='780px' height='85px'>";
            output = output + "<tr><td>" + Imgfile + "</td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>";
            output = output + "Dear  " + OrderOwner + "</td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>";

            if (typeOfCall == "Exchange")
            {
                output = output + "Thanks for contacting Deltone Solutions.  <br />";
                output = output + "<br /> Please find below the exchange order you placed with us today. A credit may be applicable to this order once the credit is approved. </td></tr>";
            }
            else
            {
                output = output + "Thanks for the order you placed with us today.  <br />";

            }
            if (PaymentTerms == "1")
                output = output + "<br /> We will dispatch the following goods and process your payment on the credit card provided.</td></tr>";
            else
                output = output + "<br /> We will dispatch the following goods on a " + PaymentTerms + " day account.</td></tr>";
               

            output = output + "<tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>";
            output = output + "<tr><td><table style='width:720px;'><tr>";
            output = output + "<td style='width:320px;text-align:left;font-family:Calibri;'><span style:'color:blue;font-family:Calibri;'>";
            output = output + "Model - Deltone Brand</span></td><td style='width:200px;text-align:center;font-family:Calibri;'>";
            output = output + "Quantity</td><td style='width:200px;text-align:center;font-family:Calibri;'>";

            //output = output + "Unit Price Inc.GST</td></tr><tr><td><hr></td><td><hr></td><td><hr></td></tr>";
            //output = output + "Total Price inc. GST</td></tr><tr><td><hr></td><td><hr></td><td><hr></td></tr>";
            //output= "<table><tr><td colspan='2'>" + Imgfile + "</td></tr><tr><td colspan='2'>THANK YOU FOR ORDERING WITH DELTONE SOLUTIONS.</td></tr><tr><td><b>ORDER NO:</b></td><td>"  + DTSNumber  + "</td></tr><tr><td  colspan='2'>ORDER SUMMERY</td></tr></table>";

            output = output + "Unit Price Inc.GST</td><td style='width:200px;text-align:center;font-family:Calibri;'>";
            output = output + "Total Price inc. GST</td></tr><tr><td style='width:200px;text-align:center;font-family:Calibri;'><hr></td><td><hr></td><td><hr></td><td><hr></td></tr>";

            //output = output + "<table><thead><tr><td>DESCRIPTION</td><td>QUANTITY</td><td>UNIT AMOUNT</td></tr></thead><tbody>";
            float UnitPriceFloat = 0;
            String ConvertedUnitPrice = String.Empty;

            // Create the Ordered Table 
            foreach (DeltoneItem item in orderlist)
            {

                output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;'>" + item.ItemDescription + "</td>";
                output = output + "<td style='width:200px;text-align:center;font-family:Calibri;'>" + item.Qty + "</td>";

                decimal theUnitPrice = item.UnitPrice;
                decimal totalPrice = theUnitPrice * item.Qty;

                var unitPriceFormat = String.Format("{0:C2}", item.UnitPrice);

                var totaPriceFormat = String.Format("{0:C2}", totalPrice);
                //float theUnitPrice = item.UnitPrice;
                //float exGSTUnitPrice = theUnitPrice - ((theUnitPrice * 10) / 100);

                output = output + "<td style='width:200px;text-align:center;font-family:Calibri;'>" + unitPriceFormat + "</td>";
                output = output + "<td style='width:200px;text-align:center;font-family:Calibri;'>" + totaPriceFormat + "</td></tr>";
            }
            output = output + "<tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>";
            String ShippingCost = String.Empty;
            ShippingCost = orderDAL.getCustomerShippingCost(OrderID);

            String OrderTotal = String.Empty;
            OrderTotal = orderDAL.getOrderTotal(OrderID);

            String Urgency = orderDAL.getOrderUrgency(OrderID);
            String UrgencyText = String.Empty;
            String FooterText = String.Empty;

            if (Urgency == "Standard")
            {
                UrgencyText = "in 2-4 business days";
                FooterText = "within the next 2-4 days";
            }
            else if (Urgency == "Urgent")
            {
                UrgencyText = "in 1-2 business days";
                FooterText = "within the next 2-4 days";
            }
            else if (Urgency == "End of Month")
            {
                UrgencyText = "at the end of the month";
                FooterText = "at the end of the month";
            }
            else
            {
                UrgencyText = "in 2-4 business days";
                FooterText = "within the next 2-4 days";
            }


            //output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;'>DELIVERY & HANDLING</td><td style='width:200px;text-align:center;font-family:Calibri;'>&nbsp;</td><td style='width:200px;text-align:center;font-family:Calibri;'>" + ShippingCost + "</td></tr>";

            //output = output + "<tr><td><hr></td><td><hr></td><td><hr></td></tr>";
            //output = output + "<tr><td>&nbsp;</td><td style='width:200px;text-align:center;font-family:Calibri;'>TOTAL</td><td style='width:200px;text-align:center;font-family:Calibri;'>" + OrderTotal + "</td></tr>";


            output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;'>DELIVERY & HANDLING</td><td style='width:200px;text-align:center;font-family:Calibri;'>&nbsp;</td><td style='width:200px;text-align:center;font-family:Calibri;'>&nbsp;</td><td style='width:200px;text-align:center;font-family:Calibri;'>" + ShippingCost + "</td></tr>";
            output = output + "<tr><td><hr></td><td><hr></td><td><hr></td><td><hr></td></tr>";
            output = output + "<tr><td>&nbsp;</td><td>&nbsp;</td><td style='width:200px;text-align:center;font-family:Calibri;'>TOTAL INC.GST</td><td style='width:200px;text-align:center;font-family:Calibri;'>" + OrderTotal + "</td></tr>";

            output = output + "</table></td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>Your order should arrive " + UrgencyText + " to the address below. </td></tr>";

            String CompanyID = String.Empty;
            CompanyID = orderDAL.getCompanyIDFromOrder(OrderID);

            String CompanyName = String.Empty;
            CompanyName = companyDAL.getCompanyNameByID(CompanyID);

            String ContactID = String.Empty;
            ContactID = orderDAL.getContactPersonID(OrderID);

            String ContactAddressLine1 = String.Empty;
            ContactAddressLine1 = contactDAL.getContactAddressLine1(ContactID);

            String ContactAddressLine2 = String.Empty;
            ContactAddressLine2 = contactDAL.getContactSubStaPost(ContactID);

            output = output + "<tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'><strong>" + CompanyName + "<br/>Attn: " + OrderOwner + "<br/>" + ContactAddressLine1 + "<br/>" + ContactAddressLine2;

            if (PaymentTerms == "1")
                output = output + "</strong></td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>An invoice and payment receipt will be emailed to you shortly. Please don’t hesitate to contact us if you have any questions or queries. </td></tr><tr><td>&nbsp;</td></tr>";
            else
                output = output + "</strong></td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>An invoice will be emailed and posted to you " + FooterText + ". Please don’t hesitate to contact us if you have any questions or queries. </td></tr><tr><td>&nbsp;</td></tr>";

            output = output + "<tr><td style='font-family:Calibri;'>Kind Regards</td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>" + OrderAccountOwner + "</td></tr><tr><td style='font-family:Calibri;'><strong>1300 787 783</strong></td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>" + bottomBanner + "</td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";
            //output = output + "</tbody></table>";
            //End Creating Order Table 
            return output;


        }

        /* Returns OrderdList given by orderid*/
        public List<DeltoneItem> getOrderedList(int OrderID)
        {
            List<DeltoneItem> Order_Items = new List<DeltoneItem>();
            DeltoneItem OrderedItem;

            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "select * from dbo.Ordered_Items where OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        OrderedItem = new DeltoneItem(); //Instantiate the Deltone Object and Assign values
                        OrderedItem.ItemDescription = sdr["Description"].ToString();
                        OrderedItem.Qty = Int32.Parse(sdr["Quantity"].ToString());
                        OrderedItem.UnitPrice = Convert.ToDecimal(sdr["UnitAmount"].ToString());

                        Order_Items.Add(OrderedItem); // Add it to  the Collection

                    }
                }
            }
            conn.Close();

            return Order_Items;
        }

        protected String FetchReferenceInfo(int OrderID)
        {
            String strNotes = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "SELECT Reference FROM dbo.Orders where OrderID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strNotes = sdr["Reference"].ToString();
                    }
                }
            }
            conn.Close();


            return strNotes;
        }


        /*This method send Order Confirmation using Gmail*/
        public void SendMail(String strBody, String strOrderID, String strClientEmail, String DTSNumber)
        {
            String ContactName = String.Empty;
            String ContactEmail = String.Empty;

            ContactName = orderDAL.getContactPersonForOrder(Int32.Parse(strOrderID));
            ContactEmail = orderDAL.getContactPersonEmail(Int32.Parse(strOrderID));


            var fromAddress = new MailAddress("info@deltonesolutions.com.au", "DELTONE SOLUTIONS PTY LTD");
            var toAddress = new MailAddress(ContactEmail, ContactName);
            var BccAddress = new MailAddress("sentemails@deltonesolutions.com.au", "CUSTOMER MAIL COPY");
            const String fromPassword = "deltonerep1#";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);
            String XeroOrderID = String.Empty;
            XeroOrderID = orderDAL.getXeroDTSID(Int32.Parse(strOrderID));
            var reference = FetchReferenceInfo(Int32.Parse(strOrderID));
            String subject = "Thanks for your order. REF# " + XeroOrderID;
            if (!string.IsNullOrEmpty(reference))
                subject = subject + " " + reference;

            //Adding Image as a Embeded Image 

            String Imgview = "<img src=\"cid:companylogo\" height='80' width='780'>";
            String bottonBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";
            string body = HTMLBODY(Int32.Parse(strOrderID), DTSNumber, Imgview, bottonBanner);


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
                    Credentials = netWorkCrdential
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


    }
}