using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
    public class EmailSender
    {
        string smtpHostName = "smtp.office365.com";
        string smtpUserName = "info@deltonesolutions.com.au";
         String smtpuserPassword = "deltonerep1#";


        public string SmtpHostName { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }

        public EmailSender()
        {
            SmtpHostName = smtpHostName;
            SmtpUserName = smtpUserName;
            SmtpPassword = smtpuserPassword;
        }


        //public void SendEmail(string bcc, string to, string body, string subject)
        //{

        //}

        public bool SendEmail(string from, string from_name, string to,
            string cc, string bcc, string subject, string body, bool isHtml,
            System.Collections.Generic.List<Attachment> attachmentsList)
        {
            ILog _logger = LogManager.GetLogger(typeof(EmailSender));
            SmtpClient smClient = new SmtpClient(SmtpHostName);
           
            smClient.Port = 587;
            smClient.UseDefaultCredentials = false;
           // smClient.Port = 3535;
            smClient.EnableSsl = true;
            smClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            var fromInfo = "info@deltonesolutions.com.au";
            var fromAddress = new MailAddress(fromInfo, "DELTONE SOLUTIONS PTY LTD");
            const String fromPassword = "deltonerep1#";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);
            var contactName = "au";
            var toAddress = new MailAddress(to, contactName);
            smClient.Credentials = netWorkCrdential;
            MailMessage message = new MailMessage(fromAddress,toAddress);
           // if (!string.IsNullOrEmpty(from_name))
              //  message.From = new MailAddress(from, from_name);
          //  else
             //   message.From = new MailAddress(from);

            message.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(cc))
            {
                var splitEmailByComma = cc.Split(',');
                foreach (var item in splitEmailByComma)
                {
                    if (!string.IsNullOrEmpty(item))
                        message.CC.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(bcc))
            {
                var splitEmailComma = bcc.Split(',');
                foreach (var item in splitEmailComma)
                {
                    if (!string.IsNullOrEmpty(item))
                        message.Bcc.Add(item);
                }
            }

            if (attachmentsList != null)
                if (attachmentsList.Count() > 0)
                    foreach (var item in attachmentsList)
                        message.Attachments.Add(item);

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            try
            {
                smClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(" Error Occurred Email Sender :" + ex);
                return false;
            }



        }


        public bool SendEmailAlternativeView(string from, string from_name, string to,
           string cc, string bcc, string subject, string body, bool isHtml,
           System.Collections.Generic.List<Attachment> attachmentsList)
        {
            ILog _logger = LogManager.GetLogger(typeof(EmailSender));
            SmtpClient smClient = new SmtpClient(SmtpHostName);
            smClient.Port = 587;
            smClient.UseDefaultCredentials = false;
            // smClient.Port = 3535;
            smClient.EnableSsl = true;
            smClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            var fromInfo = "info@deltonesolutions.com.au";
            var fromAddress = new MailAddress(fromInfo, "DELTONE SOLUTIONS PTY LTD");
            const String fromPassword = "deltonerep1#";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);
            var contactName = "au";
            var toAddress = new MailAddress(to, contactName);
            smClient.Credentials = netWorkCrdential;
            MailMessage message = new MailMessage(fromAddress, toAddress);
            if (!string.IsNullOrEmpty(cc))
            {
                var splitEmailByComma = cc.Split(',');
                foreach (var item in splitEmailByComma)
                {
                    if (!string.IsNullOrEmpty(item))
                        message.CC.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(bcc))
            {
                var splitEmailComma = bcc.Split(',');
                foreach (var item in splitEmailComma)
                {
                    if (!string.IsNullOrEmpty(item))
                        message.Bcc.Add(item);
                }
            }

            if (attachmentsList != null)
                if (attachmentsList.Count() > 0)
                    foreach (var item in attachmentsList)
                        message.Attachments.Add(item);
            String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";

            AlternateView avHTML = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            LinkedResource btmbanner = new LinkedResource("C:\\temp\\DeltoneCRM\\DeltoneCRM\\Images\\bottom-banner-email.jpg");

            btmbanner.ContentId = "bottombanner";

            avHTML.LinkedResources.Add(btmbanner);

            message.AlternateViews.Add(avHTML);

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            try
            {
                smClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(" Error Occurred Email Sender Quote  :" + ex);
                return false;
            }



        }
    }
}
