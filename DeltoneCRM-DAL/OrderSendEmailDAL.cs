using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
    public class OrderSendEmailDAL
    {
        private String CONNSTRING;

        public OrderSendEmailDAL(String connString)
        {
            CONNSTRING = connString;
        }


        public List<SupplierEmail> GetSupplierEmails(string suppName, int typeEmail)
        {
            List<SupplierEmail> semails = new List<SupplierEmail>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var active = 1;
            var strSqlContactStmt = @"SELECT se.Id,se.SupplierId,se.SupplierName,se.SupplierEmail from SupplierEmail se , Suppliers sl  
                                          where se.SupplierID=sl.SupplierId and sl.SupplierName=@name and se.Type=@type and se.Active=@active";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@name", SqlDbType.NVarChar).Value = suppName;
                cmd.Parameters.AddWithValue("@type", SqlDbType.Int).Value = typeEmail;
                cmd.Parameters.AddWithValue("@active", SqlDbType.Int).Value = active;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var sObj = new SupplierEmail();
                    sObj.Id = Convert.ToInt32(reader["Id"].ToString());
                    sObj.SupplierId = Convert.ToInt32(reader["SupplierId"].ToString());
                    if (reader["SupplierName"] != DBNull.Value)
                        sObj.SupplierName = reader["SupplierName"].ToString();
                    sObj.SupplierEmailAddress = reader["SupplierEmail"].ToString();
                    semails.Add(sObj);
                }
            }
            conn.Close();
            return semails;

        }

        //this method adds Purchase Order email to the database
        public void AddPurchaseOrderEmail(OrderSendEmail purchaseOrderEmail)
        {

            //insert
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO OrderSendEmail(PurchaseOrderId, NoOfTry, Status, CreatedDate, SendDate,FileNames,SupplierName,EmailBody,OrderType,IsOrderNow) 
                                     VALUES(@purchaseOrderId, @noOfTry, @status,CURRENT_TIMESTAMP,@sendDate,@fileNames,@supplierName,@emailBody,@orderType,@IsOrderNow)";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@purchaseOrderId", SqlDbType.Int).Value = purchaseOrderEmail.PurchaseOrderId;
                cmd.Parameters.Add("@NoOfTry", SqlDbType.Int).Value = purchaseOrderEmail.NoOfTry;
                cmd.Parameters.Add("@status", SqlDbType.Int).Value = purchaseOrderEmail.Status;
                cmd.Parameters.Add("@sendDate", SqlDbType.DateTime).Value = purchaseOrderEmail.SendDate;
                cmd.Parameters.Add("@supplierName", SqlDbType.VarChar).Value = purchaseOrderEmail.SupplierName;
                cmd.Parameters.Add("@fileNames", SqlDbType.NVarChar).Value = purchaseOrderEmail.FileNames;
                cmd.Parameters.Add("@emailBody", SqlDbType.NVarChar).Value = purchaseOrderEmail.EmailBody;
                cmd.Parameters.Add("@orderType", SqlDbType.NVarChar).Value = purchaseOrderEmail.OrderType;
                cmd.Parameters.Add("@IsOrderNow", SqlDbType.Bit).Value = purchaseOrderEmail.IsOrderNow;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();



            }
            conn.Close();

        }

        public OrderSendEmail GetOrderSendEmailById(int orderSendEmailPurchaseId)
        {
            var obj = new OrderSendEmail();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id,NoOfTry,ResultMessage,SendDate ,SupplierName,FileNames,EmailBody ,OrderType  from OrderSendEmail where PurchaseOrderId=@pId";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@pId", SqlDbType.Int).Value = orderSendEmailPurchaseId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj.Id = Convert.ToInt32(reader["Id"]);
                        obj.PurchaseOrderId = orderSendEmailPurchaseId;
                        if (reader["ResultMessage"] != DBNull.Value)
                            obj.ResultMessage = reader["ResultMessage"].ToString();
                        obj.SupplierName = reader["SupplierName"].ToString();
                        obj.FileNames = reader["FileNames"].ToString();
                        if (reader["EmailBody"] != DBNull.Value)
                            obj.EmailBody = reader["EmailBody"].ToString();
                        if (reader["OrderType"] != DBNull.Value)
                            obj.OrderType = reader["OrderType"].ToString();

                        obj.NoOfTry = Convert.ToInt32(reader["NoOfTry"]);
                        obj.SendDate = Convert.ToDateTime(reader["SendDate"]);



                    }
                }
            }
            conn.Close();


            return obj;

        }

        public void UpdateNoTry(int notry, int Id, string message)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE OrderSendEmail SET ResultMessage=@resultMessage, NoOfTry=@noTry , ModifiedDate=CURRENT_TIMESTAMP WHERE Id=@id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@noTry", SqlDbType.Int).Value = notry;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@resultMessage", SqlDbType.VarChar).Value = message;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void UpdateStatus(int status, int Id, string resultMessage)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE OrderSendEmail SET Status=@stat , ResultMessage=@resultMessage, ModifiedDate=CURRENT_TIMESTAMP WHERE Id=@id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@stat", SqlDbType.Int).Value = status;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@resultMessage", SqlDbType.VarChar).Value = resultMessage;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public IList<OrderSendEmail> GetOrderSendEmail(int status)
        {
            var listObj = new List<OrderSendEmail>();


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id,PurchaseOrderId,NoOfTry,ResultMessage,SendDate ,SupplierName,FileNames,EmailBody ,OrderType from OrderSendEmail where Status=@status";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@status", SqlDbType.Int).Value = status;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var obj = new OrderSendEmail();
                        obj.Id = Convert.ToInt32(reader["Id"]);
                        obj.PurchaseOrderId = Convert.ToInt32(reader["PurchaseOrderId"]);
                        if (reader["ResultMessage"] != DBNull.Value)
                            obj.ResultMessage = reader["ResultMessage"].ToString();
                        obj.SupplierName = reader["SupplierName"].ToString();
                        obj.FileNames = reader["FileNames"].ToString();
                        if (reader["EmailBody"] != DBNull.Value)
                            obj.EmailBody = reader["EmailBody"].ToString();
                        if (reader["OrderType"] != DBNull.Value)
                            obj.OrderType = reader["OrderType"].ToString();

                        obj.NoOfTry = Convert.ToInt32(reader["NoOfTry"]);
                        obj.Status = status;
                        obj.SendDate = Convert.ToDateTime(reader["SendDate"]);

                        listObj.Add(obj);
                    }
                }
            }
            conn.Close();

            return listObj;

        }



        public void UpdatePurchaseOrderMessageAndTryEmail(int id, string resultMessage, int noTry)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE OrderSendEmail SET ResultMessage=@resultMessage, NoOfTry=@noTry , ModifiedDate=CURRENT_TIMESTAMP WHERE Id=@id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@resultMessage", SqlDbType.VarChar).Value = resultMessage;
                cmd.Parameters.Add("@noTry", SqlDbType.Int).Value = noTry;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void DeleteOrderSendEmail(int id, string supplierName)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM OrderSendEmail WHERE (PurchaseOrderId = @purchaseOrderId and SupplierName =@supplierName)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@purchaseOrderId", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@supplierName", SqlDbType.VarChar).Value = supplierName;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void RunPurchaseOrderEmail()
        {
            var statusEmail = 1;
            var list = GetOrderSendEmail(statusEmail);
            foreach (var item in list)
            {


                var datetimeNow = DateTime.Now;
                if (item.SendDate.Date == datetimeNow.Date)
                {
                    var hoursend = item.SendDate.Hour;
                    var hourCurrent = datetimeNow.Hour;
                    //   var diffTimeHours = item.SendDate.Subtract(datetimeNow).Hours;

                    if (hoursend == hourCurrent)
                    {
                        var minMin = -5;
                        var diffTime = item.SendDate.Subtract(datetimeNow).Minutes;
                        if (diffTime > minMin && diffTime < 30)
                        {
                            var result = SendEmailPurchaseOrder(item);
                            UpdateStatusSendEmail(result, item);
                        }

                    }
                }
                if (item.SendDate.Date < datetimeNow.Date)
                {
                    var resuMessage = "Email send permanently failed ";
                    var status = (int)StatusEmailSupplier.Failed;
                    UpdateStatus(status, item.Id, resuMessage);
                }
            }

        }

        public void RunPurchaseOrderEmailTTTT()
        {
            var statusEmail = 1;
            var list = GetOrderSendEmail(statusEmail);
            foreach (var item in list)
            {


                var datetimeNow = DateTime.Now;
                if (item.SendDate.Date == datetimeNow.Date)
                {
                    var hoursend = item.SendDate.Hour;
                    var hourCurrent = datetimeNow.Hour;
                    //   var diffTimeHours = item.SendDate.Subtract(datetimeNow).Hours;

                    if (hoursend == hourCurrent)
                    {
                        var minMin = -5;
                        var diffTime = item.SendDate.Subtract(datetimeNow).Minutes;
                        if (diffTime > minMin && diffTime < 30)
                        {
                            var result = SendEmailPurchaseOrder(item);
                            UpdateStatusSendEmail(result, item);
                        }

                    }
                }
                if (item.SendDate.Date < datetimeNow.Date)
                {
                    var resuMessage = "Email send permanently failed ";
                    var status = (int)StatusEmailSupplier.Failed;
                    UpdateStatus(status, item.Id, resuMessage);
                }
            }

        }

        protected void UpdateStatusSendEmail(bool result, OrderSendEmail obj)
        {

            if (result)
            {
                var resuMessage = "Email successfully sent";
                var status = (int)StatusEmailSupplier.SuccessfullySent;
                UpdateStatus(status, obj.Id, resuMessage);
            }
            else
            {
                var resuMessage = "Email send failed";
                var noTry = obj.NoOfTry + 1;
                if (noTry <= 3)
                    UpdatePurchaseOrderMessageAndTryEmail(obj.Id, resuMessage, noTry);
                else
                {
                    var resuMessageFailed = "Email send permanently failed ";
                    var status = (int)StatusEmailSupplier.Failed;
                    UpdateStatus(status, obj.Id, resuMessageFailed);
                }
            }
        }


        public string getXeroDTSID(int OrderID)
        {
            string output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            string strSqlOrderStmt = "SELECT XeroInvoiceNumber FROM dbo.Orders WHERE OrderID = " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["XeroInvoiceNumber"] != DBNull.Value)
                            output = sdr["XeroInvoiceNumber"].ToString();
                    }

                }
            }

            conn.Close();

            return output;
        }

        public string getContactID(int OrderID)
        {
            string output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            string strSqlOrderStmt = "SELECT ContactID FROM dbo.Orders WHERE OrderID = " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["ContactID"] != DBNull.Value)
                            output = sdr["ContactID"].ToString();
                    }

                }
            }

            conn.Close();

            return output;
        }

        public string GetCompanybyContactID(int contactId)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSqlContactStmt = "SELECT cm.CompanyName FROM dbo.Contacts cn,dbo.Companies cm  WHERE cn.CompanyID=cm.CompanyID AND cn.ContactID=" + contactId;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = sdr["CompanyName"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;

        }

        public String getOrderUrgency(int OrderID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT Urgency FROM dbo.Orders WHERE OrderID = " + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["Urgency"].ToString();
                    }

                }
            }

            conn.Close();

            return output;
        }

        public String getSupplierNotes(String XeroInvoiceNumber, string suppName)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT  Notes from dbo.SupplierNotes where XeroInvoiceNumber =@xeronumber AND  Suppliername=@suppname";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Parameters.AddWithValue("@xeronumber", XeroInvoiceNumber);
                cmd.Parameters.AddWithValue("@suppname", suppName);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["Notes"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }

        public bool RESendOrderSupplier(string orderId, string suppName, string comID, string contactId)
        {
            ILog _logger = LogManager.GetLogger(typeof(OrderSendEmailDAL));
            var orderTypeInt = Convert.ToInt32(orderId);
            var xeroInVoice = getXeroDTSID(orderTypeInt);
            var supplierDetails = GetSupplierEmails(suppName, 1);
            var orderUrgency = getOrderUrgency(orderTypeInt);
            var suppNotes = getSupplierNotes(xeroInVoice, suppName);
            var conContacid = Convert.ToInt32(contactId);
            var companyName = GetCompanybyContactID(conContacid);
            if (supplierDetails.Count() > 0)
            {
                var body = "Hi , <br/> Please process the attached order.";
                var subject = "Deltone Solutions Order " + xeroInVoice;
              var toAddress = supplierDetails[0].SupplierEmailAddress;
                var ccAddress = "";
                var bodyMessageCom = " <br/> Delivery Type : " + orderUrgency;

               // var toAddress = "paruthi001@gmail.com";
                foreach (var item in supplierDetails.Skip(1))
                {
                    if (ccAddress == "")
                        ccAddress = item.SupplierEmailAddress;
                    else
                        ccAddress = ccAddress + "," + item.SupplierEmailAddress;
                }

                body = body + bodyMessageCom;

                if (!string.IsNullOrEmpty(suppNotes))
                {
                    body = body + " <br/> Instructions : " + suppNotes;
                }

                var bccAddress = DelToneCommonSettings.bccInfoAddress;

                var fromName = DelToneCommonSettings.fromName;
                var fromAddress = DelToneCommonSettings.fromAddress;
                var invoiceFilePath = DelToneCommonSettings.fileInvoicePath;
                var invoiceAusJetPath = DelToneCommonSettings.fileCSVPath;
               
                var attachements = new List<Attachment>();
                var comFilePath = "";

                var invoiceFileName = suppName + "-" + xeroInVoice + "-" + companyName + ".pdf";
                var invoiceFile = invoiceFilePath + invoiceFileName;
                if (File.Exists(invoiceFile))
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(invoiceFile);
                    attachment.Name = invoiceFileName;
                    attachements.Add(attachment);
                }

                if (suppName == "Ausjet")
                {
                    var csvFile = "AUSJET - Order " + xeroInVoice + ".csv";
                    comFilePath = invoiceAusJetPath + csvFile;
                    if (File.Exists(comFilePath))
                    {
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(comFilePath);
                        attachment.Name = csvFile;
                        attachements.Add(attachment);
                    }
                }




                var sendEmail = new EmailSender();

                try
                {
                    var resultSend = sendEmail.SendEmail(fromAddress, fromName, toAddress, ccAddress, bccAddress, subject, body, true, attachements);
                    if (resultSend)
                        _logger.Info(" Resend Email  Sent Success :" + toAddress + " Order Id: " + orderId + " Xero Dts: " + xeroInVoice);
                    else
                        _logger.Info(" Resend Email Sent Error : " + toAddress + " Order Id: " + orderId + " Xero Dts: " + xeroInVoice);

                    return resultSend;
                }
                catch (Exception ex)
                {

                    _logger.Error("Resend order Error Occurred :" + ex + "  Email Sent  "
                        + toAddress + " Order Id: " + orderId + " Xero Dts: " + xeroInVoice);
                    return false;
                }
            }

            return false;

        }

        public bool SendEmailPurchaseOrder(OrderSendEmail obj)
        {
            var supplierDetails = GetSupplierEmails(obj.SupplierName, 1);
            ILog _logger = LogManager.GetLogger(typeof(OrderSendEmailDAL));
            var xeroInVoice = getXeroDTSID(obj.PurchaseOrderId);
            var body = "Hi , <br/> Please process the attached order.";
            var subject = "Deltone Solutions Order " + xeroInVoice;
            var contactID = Convert.ToInt32(getContactID(obj.PurchaseOrderId));
            var companyName = GetCompanybyContactID(contactID);
            if (supplierDetails.Count() > 0)
            {
                var toAddress = supplierDetails[0].SupplierEmailAddress;
                var ccAddress = "";
                // var toAddress = "paruthi001@gmail.com";
                foreach (var item in supplierDetails.Skip(1))
                {
                    if (ccAddress == "")
                        ccAddress = item.SupplierEmailAddress;
                    else
                        ccAddress = ccAddress + "," + item.SupplierEmailAddress;
                }

                var bodyMessageCom = " <br/> Delivery Type : " + obj.OrderType;

                body = body + bodyMessageCom;

                if (!string.IsNullOrEmpty(obj.EmailBody))
                {
                    body = body + " <br/> Instructions : " + obj.EmailBody;
                }

                var bccAddress = DelToneCommonSettings.bccInfoAddress;

                var fromName = DelToneCommonSettings.fromName;
                var fromAddress = DelToneCommonSettings.fromAddress;
                var invoiceFilePath = DelToneCommonSettings.fileInvoicePath;
                var invoiceAusJetPath = DelToneCommonSettings.fileCSVPath;
                var files = obj.FileNames.Split(',');
                var attachements = new List<Attachment>();
                var comFilePath = "";

                var invoiceFileName = obj.SupplierName + "-" + xeroInVoice + "-" + companyName + ".pdf";
                var invoiceFile = invoiceFilePath + invoiceFileName;
                if (File.Exists(invoiceFile))
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(invoiceFile);
                    attachment.Name = invoiceFileName;
                    attachements.Add(attachment);
                }

                if (obj.SupplierName == "Ausjet")
                {
                    var csvFile = "AUSJET - Order " + xeroInVoice + ".csv";
                    comFilePath = invoiceAusJetPath + csvFile;
                    if (File.Exists(comFilePath))
                    {
                        System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(comFilePath);
                        attachment.Name = csvFile;
                        attachements.Add(attachment);
                    }
                }


                //foreach (var file in files)
                //{
                //    var fIem = file;
                //    if (obj.SupplierName == "Ausjet")
                //    {
                //        comFilePath = invoiceFilePath + fIem;
                //        if (File.Exists(comFilePath))
                //        {
                //            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(comFilePath);
                //            attachment.Name = fIem;
                //            attachements.Add(attachment);
                //        }
                //        else
                //        {
                //            comFilePath = invoiceAusJetPath + fIem;
                //            if (File.Exists(comFilePath))
                //            {
                //                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(comFilePath);
                //                attachment.Name = fIem;
                //                attachements.Add(attachment);
                //            }
                //        }
                //    }
                //    else
                //    {
                //        comFilePath = invoiceFilePath + fIem;
                //        if (File.Exists(comFilePath))
                //        {
                //            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(comFilePath);
                //            attachment.Name = fIem;
                //            attachements.Add(attachment);
                //        }
                //    }
                //}

                var sendEmail = new EmailSender();

                try
                {
                    var resultSend = sendEmail.SendEmail(fromAddress, fromName, toAddress, ccAddress, bccAddress, subject, body, true, attachements);
                    if (resultSend)
                        _logger.Info(" Email Sent Success :" + toAddress + " Order Id: " + obj.PurchaseOrderId + " Xero Dts: " + xeroInVoice);
                    else
                        _logger.Info(" Email Sent Error : " + toAddress + " Order Id: " + obj.PurchaseOrderId + " Xero Dts: " + xeroInVoice);

                    return resultSend;
                }
                catch (Exception ex)
                {

                    _logger.Error(" Error Occurred :" + ex + "  Email Sent  "
                        + toAddress + " Order Id: " + obj.PurchaseOrderId + " Xero Dts: " + xeroInVoice);
                    return false;
                }
            }

            return false;
        }
    }


    public class OrderSendEmail
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int NoOfTry { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ResultMessage { get; set; }
        public DateTime SendDate { get; set; }
        public string FileNames { get; set; }
        public string SupplierName { get; set; }
        public string EmailBody { get; set; }
        public string OrderType { get; set; }
        public string XeroOrderId { get; set; }
        public bool IsOrderNow { get; set; }
    }

    public class SupplierEmail
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierEmailAddress { get; set; }
    }

    public enum StatusEmailSupplier
    {
        Created = 1,
        SuccessfullySent = 2,
        Failed = 3,
        NotSend = 4
    }



    public static class DelToneCommonSettings
    {
        public const string fileInvoicePath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\Invoices\\Supplier\\";
        public const string fileCSVPath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\Invoices\\Supplier\\SUPPLIERCSV\\";
        public const string bccInfoAddress = "sentemails@deltonesolutions.com.au";
        public const string fromAddress = "info@deltonesolutions.com.au";
        public const string fromName = "Deltonesolutions";
    }

}
