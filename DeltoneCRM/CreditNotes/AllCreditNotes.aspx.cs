using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.CreditNotes
{
    public partial class AllCreditNotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static void RMAUpdateMe(DeltoneCRM_DAL.CreditNoteRMAHandler.RMAUpdate obj)
        {
            UpdateRMANUmber(obj);
        }

        private static void UpdateRMANUmber(DeltoneCRM_DAL.CreditNoteRMAHandler.RMAUpdate obj)
        {
            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;



            if (new CreditNoteRMAHandler(CONNSTRING).GetRMAByCreditNoteIdAndSuppName(Convert.ToInt32(obj.CreId), obj.SuppName) == 0)
                new CreditNotesDAL(CONNSTRING).WriteSupplierIntoRMA(obj.CreId, obj.SuppName);

            var rmaId = new CreditNoteRMAHandler(CONNSTRING).GetRMAByCreditNoteIdAndSuppName(Convert.ToInt32(obj.CreId), obj.SuppName);
            var RMAID = rmaId.ToString();
            String SqlStmt = @"UPDATE dbo.RMATracking SET SentToSupplier=@SentToSupplier, 
                      SentToSupplierDateTime=@SentToSupplierDateTime, ApprovedRMA=@ApprovedRMA, ApprovedRMADateTime=@ApprovedRMADateTime,
                      CreditInXero=@CreditInXero, CreditInXeroDateTime=@CreditInXeroDateTime, RMAToCustomer=@RMAToCustomer, 
                      RMAToCustomerDateTime=@RMAToCustomerDateTime, AdjustedNoteFromSupplier=@AdjustedNoteFromSupplier, 
                      AdjustedNoteFromSupplierDateTime=@AdjustedNoteFromSupplierDateTime, Status=@Status, SupplierRMANumber=@SupplierRMANumber, 
                TrackingNumber=@TrackingNumber , InHouse=@inHouse WHERE CreditNoteID =@crId AND SupplierName=@suppName";
            SqlCommand cmd = new SqlCommand(SqlStmt, conn);

            cmd.Parameters.Add("@suppName", SqlDbType.NVarChar).Value = obj.SuppName;
            cmd.Parameters.Add("@crId", SqlDbType.Int).Value = obj.CreId;
            String STS = obj.STS;
            String STSWrite = String.Empty;
            //DateTime STSDateTime = new DateTime();
            if (STS == "True")
            {
                STSWrite = "1";
            }
            else
            {
                STSWrite = "0";
            }

            String inHouse = obj.INHouse;
            String inHouseWrite = String.Empty;
            //DateTime STSDateTime = new DateTime();
            if (inHouse == "True")
            {
                inHouseWrite = "1";
            }
            else
            {
                inHouseWrite = "0";
            }

            cmd.Parameters.AddWithValue("@inHouse", inHouseWrite);

            String ARMA = obj.ARMA;
            String ARMAWrite = String.Empty;

            if (ARMA == "True")
            {
                ARMAWrite = "1";
            }
            else
            {
                ARMAWrite = "0";
            }

            String CIX = obj.CIX;
            String CIXWrite = String.Empty;

            if (CIX == "True")
            {
                CIXWrite = "1";
            }
            else
            {
                CIXWrite = "0";
            }

            String RTC = obj.RTC;
            String RTCWrite = String.Empty;

            if (RTC == "True")
            {
                RTCWrite = "1";
            }
            else
            {
                RTCWrite = "0";
            }

            String ANFS = obj.ANFS;
            String ANFSWrite = String.Empty;

            if (ANFS == "True")
            {
                ANFSWrite = "1";
            }
            else
            {
                ANFSWrite = "0";
            }

            String NewStatus = String.Empty;
            //if (STS == "True" && ARMA == "True" && CIX == "True" && RTC == "True" && ANFS == "True")
            //{
            //    NewStatus = "COMPLETED";
            //}
            //else
            //{

            //}
            NewStatus = new CreditNotesDAL(CONNSTRING).RMAStatusByCreditIdAndSuppllier(obj.CreId, obj.SuppName);

            if (inHouse == "True")
            {
                NewStatus = "COMPLETED";
            }
            else

                if (CIX == "True")
                {
                    NewStatus = "COMPLETED";
                }


            cmd.Parameters.AddWithValue("@SentToSupplier", STSWrite);
            if (STS == "True")
            {
                if (checkIfSTSHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@SentToSupplierDateTime", DateTime.Today);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SentToSupplierDateTime", DateTime.Parse(checkIfSTSHasValue(RMAID)));
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@SentToSupplierDateTime", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@ApprovedRMA", ARMAWrite);

            if (ARMA == "True")
            {
                if (checkIfARMAHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@ApprovedRMADateTime", DateTime.Today);
                    //  sentRMATOCustomer(RMAID);  // sent email to customer once RMA Approved
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ApprovedRMADateTime", DateTime.Parse(checkIfARMAHasValue(RMAID)));
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@ApprovedRMADateTime", DBNull.Value);
            }


            cmd.Parameters.AddWithValue("@CreditInXero", CIXWrite);

            if (CIX == "True")
            {
                if (checkIfCIXHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@CreditInXeroDateTime", DateTime.Today);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CreditInXeroDateTime", DateTime.Parse(checkIfCIXHasValue(RMAID)));
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@CreditInXeroDateTime", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@RMAToCustomer", RTCWrite);

            if (RTC == "True")
            {
                if (checkIfRTCHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@RMAToCustomerDateTime", DateTime.Today);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RMAToCustomerDateTime", DateTime.Parse(checkIfRTCHasValue(RMAID)));
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@RMAToCustomerDateTime", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplier", ANFSWrite);

            if (ANFS == "True")
            {
                if (checkIfANFSHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplierDateTime", DateTime.Today);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplierDateTime", DateTime.Parse(checkIfANFSHasValue(RMAID)));
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplierDateTime", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Status", NewStatus);

            cmd.Parameters.AddWithValue("@SupplierRMANumber", obj.SRMAN);

            cmd.Parameters.AddWithValue("@TrackingNumber", obj.TN);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private static String checkIfSTSHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT SentToSupplierDatetime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["SentToSupplierDatetime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        private static String checkIfARMAHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT ApprovedRMADateTime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["ApprovedRMADateTime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        private DeltoneCRM.process.EditRMA.RMATrackingClient GetCreditNoteIdFromRAMTracking(int RmaId)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var rMATrackingClient = new DeltoneCRM.process.EditRMA.RMATrackingClient();

            String SqlStmt = "SELECT CreditNoteID,SupplierRMANumber,TrackingNumber FROM dbo.RMATracking WHERE RMAID = " + RmaId;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        rMATrackingClient.CreditNoteID = Convert.ToInt32(sdr["CreditNoteID"].ToString());
                        if (sdr["SupplierRMANumber"] != DBNull.Value)
                            rMATrackingClient.SupplierRMANumber = sdr["SupplierRMANumber"].ToString();
                        if (sdr["TrackingNumber"] != DBNull.Value)
                            rMATrackingClient.TrackingNumber = sdr["TrackingNumber"].ToString();
                    }
                }
                conn.Close();
            }


            return rMATrackingClient;
        }

        private static String checkIfCIXHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT CreditInXeroDateTime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["CreditInXeroDateTime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        private static String checkIfRTCHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT RMAToCustomerDateTime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["RMAToCustomerDateTime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        private static String checkIfANFSHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT AdjustedNoteFromSupplierDateTime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["AdjustedNoteFromSupplierDateTime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        [System.Web.Services.WebMethod]
        public static void CreateCSVANDEMAILSupplier(string[] ids)
        {
            if (ids != null)
            {
                var listObj = GetRMAData(ids);
                var rmaFileName = CreateCSVAndEmail(listObj);
                if (rmaFileName != null)
                    SendEmailToSupplier(rmaFileName);

            }
        }

        private static void SendEmailToSupplier(RMAResult filesAndSupp)
        {

            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var subject = "RMA Request from Deltone Solutions ";
            String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";

            var bodytitle = "Hi  , <br/> Please fill RMA for all attached orders and send back to Deltone Solutions.";
            var creditEmailHandler = new CreditNoteRMAHandler(cs);
            var typeEmail = 2; //RMA Request Email
            //    var suppEmails = new OrderSendEmailDAL(cs).GetSupplierEmails(suppName, typeEmail);
            var body = CreateEmailTemplateForSupplier(bottomBanner, bodytitle);

            var filePath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\Uploads\\RMA\\";
            var from = "info@deltonesolutions.com.au";
            var fromName = "Deltonesolutions";
            var sendEmail = new EmailSender();
            if (filesAndSupp.FilesAndSuppNames != null)
            {
                foreach (var item in filesAndSupp.FilesAndSuppNames)
                {
                    var suppEmails = new OrderSendEmailDAL(cs).GetSupplierEmails(item.Key, typeEmail);
                    var fileName = item.Value + ".csv";
                    foreach (var sEmail in suppEmails)
                    {

                        var fileAtta = new System.Collections.Generic.List<Attachment>();
                        var fileFullPAth = filePath + fileName;
                        if (File.Exists(fileFullPAth))
                        {
                            System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(fileFullPAth);
                            attachment.Name = Path.GetFileName(fileFullPAth);
                            fileAtta.Add(attachment);
                        }
                        sendEmail.SendEmailAlternativeView(from, fromName, sEmail.SupplierEmailAddress,
                            "krit@deltonesolutions.com.au", "", subject, body, true, fileAtta);
                    }
                }
            }

            if (filesAndSupp.RMAIDS != null)
            {
                if (filesAndSupp.RMAIDS.Count() > 0)
                {
                    foreach (var id in filesAndSupp.RMAIDS)
                        UpdateRMACreditXeroInfo(id);
                }
            }

        }

        private static void UpdateRMACreditXeroInfo(int rmaId)
        {
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var creditEmailHandler = new CreditNoteRMAHandler(strConnectionString);
            creditEmailHandler.UpdateRMISentToSupplierByRAMID(rmaId);
        }

        public class RMAResult
        {
            public Dictionary<string, string> FilesAndSuppNames { get; set; }
            public List<int> RMAIDS { get; set; }
        }

        private static RMAResult CreateCSVAndEmail(List<SuppNameDetail> objlist)
        {
            String StringBuilder = String.Empty;

            var fileNames = new Dictionary<string, string>();
            var rmaObjResult = new RMAResult();
            rmaObjResult.FilesAndSuppNames = new Dictionary<string, string>();
            rmaObjResult.RMAIDS = new List<int>();
            var gropList = objlist.GroupBy(p => p.SuppName).ToList();
            string nameFile = "";

            //  var filDirctory = System.Web.HttpContext.Current.Server.MapPath("~/Uploads/RMA/");
            var filePath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\Uploads\\RMA\\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            foreach (var grItem in gropList)
            {
                var keyGroupSuppName = grItem.Key;
                nameFile = "RMARequest_Deltone_" + keyGroupSuppName + "_" + DateTime.Now.Ticks;
                rmaObjResult.FilesAndSuppNames.Add(keyGroupSuppName, nameFile);
                StreamWriter SW = File.CreateText(filePath + nameFile + ".csv");
                SW.Write("Company Name,Order No,Credit No,Reason\r\n");
                foreach (var item in grItem)
                {
                    StringBuilder = String.Empty;
                    StringBuilder = item.CompanyName + "," + item.XeroInvoiceNumber + "," + item.XeroCreditNoteID + "," + item.Reason;
                    rmaObjResult.RMAIDS.Add(item.RMAID);
                    // int Length = StringBuilder.Length;
                    //StringBuilder = StringBuilder.Substring(0, (Length - 1));
                    StringBuilder = StringBuilder + "\r\n";
                    SW.Write(StringBuilder);
                }
                SW.Close();
            }

            return rmaObjResult;

        }

        private static List<SuppNameDetail> GetRMAData(string[] ids)
        {

            var listObj = new List<SuppNameDetail>();

            var idsSelected = ids.ToList();
            foreach (var item in idsSelected)
            {
                var query = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName, CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, 
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND ar.RMAID=@rmaId";

                using (SqlConnection conn = new SqlConnection())
                {

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Parameters.AddWithValue("@rmaId", item);
                        cmd.Connection = conn;
                        conn.Open();
                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            if (sdr.HasRows)
                            {
                                while (sdr.Read())
                                {
                                    //var orderDate = Convert.ToDateTime(sdr["DateCreated"]).ToString("dd-MMM-yyyy");
                                    var obj = new SuppNameDetail();
                                    var suppName = sdr["SupplierName"].ToString();
                                    var strCreditReson = sdr["CreditNoteReason"].ToString();
                                    var companyName = sdr["CompanyName"].ToString();
                                    var orderNo = sdr["XeroInvoiceNumber"].ToString();
                                    var creditId = sdr["XeroCreditNoteID"].ToString();
                                    obj.SuppName = suppName;
                                    obj.CompanyName = companyName;
                                    obj.XeroInvoiceNumber = orderNo;
                                    obj.XeroCreditNoteID = creditId;
                                    obj.Reason = strCreditReson;
                                    obj.RMAID = Convert.ToInt32(item);
                                    listObj.Add(obj);

                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }

            return listObj;
        }


        private static string CreateEmailTemplateForSupplier(string bottomBanner, string title)
        {
            String output = String.Empty;
            var listItems = new List<string>();

            output = "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 3.2//EN'><html>";

            output = output + "<body style='font-family:Calibri;'><table align='center' cellpadding='0' cellspacing='0' width='100%'><tr><td><table  cellpadding='0' cellspacing='0' width='780px' height='85px'>";
            output = output + title + "</td></tr><tr><td>&nbsp;</td></tr><tr><td><table style='width:720px;'><tr>";


            Char spacer = (char)13;

            output = output + @"<tr><td style='font-family:Calibri;'>KIND REGARDS</td></tr><tr><td>&nbsp;
     </td></tr><tr><td style='font-family:Calibri;'></td></tr><tr><td style='font-family:Calibri;'>"
                                                      + bottomBanner + "</td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";

            return output;
        }


        protected class SuppNameDetail
        {
            public string SuppName { get; set; }
            public string CompanyName { get; set; }
            public string XeroInvoiceNumber { get; set; }
            public string XeroCreditNoteID { get; set; }
            public string Reason { get; set; }
            public int RMAID { get; set; }
        }
    }
}