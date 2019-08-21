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
    public class CreditNoteRMAHandler
    {
        private String CONNSTRING;

        public CreditNoteRMAHandler(String connString)
        {
            CONNSTRING = connString;
        }

        public bool SendCreditNoteEmail(int creditNoteId, string comId,string suppName,string from, string from_name, string to,
             string cc, string bcc, string subject, string body, bool isHtml,
             System.Collections.Generic.List<Attachment> attachmentsList)
        {
            ILog _logger = LogManager.GetLogger(typeof(CreditNoteRMAHandler));

            var sendEmail = new EmailSender();
            try
            {
                attachmentsList = GetFilesForRMARequest(creditNoteId, comId, suppName);
                sendEmail.SendEmail(from, from_name, to, cc, bcc, subject, body, true, attachmentsList);
                _logger.Info(" Email sent Credit note Id: " + creditNoteId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(" Error Occurred" + ex.Message);
                return false;
            }

            return true;
        }

        public bool SendCreditNoteWithFileForSupplier( string from, string from_name, string to,
           string cc, string bcc, string subject, string body, bool isHtml,
           System.Collections.Generic.List<Attachment> attachmentsList)
        {
            ILog _logger = LogManager.GetLogger(typeof(CreditNoteRMAHandler));

            var sendEmail = new EmailSender();
            try
            {
                sendEmail.SendEmail(from, from_name, to, cc, bcc, subject, body, true, attachmentsList);
                _logger.Info(" export clicked: " );
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(" Error Occurred" + ex.Message);
                return false;
            }

            return true;
        }


        public bool SendCreditNoteEmailAlternative(int creditNoteId, string comId, string suppName, string from, string from_name, string to,
            string cc, string bcc, string subject, string body, bool isHtml,
            System.Collections.Generic.List<Attachment> attachmentsList)
        {
            ILog _logger = LogManager.GetLogger(typeof(CreditNoteRMAHandler));

            var sendEmail = new EmailSender();
            try
            {
                attachmentsList = GetFilesForRMARequest(creditNoteId, comId, suppName);
                sendEmail.SendEmailAlternativeView(from, from_name, to, cc, bcc, subject, body, true, attachmentsList);
                _logger.Info(" Email SendCreditNoteEmailAlternative: " + body);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(" Error Occurred SendCreditNoteEmailAlternative :" + ex.Message);
                return false;
            }

            return true;
        }



        private System.Collections.Generic.List<Attachment> GetFilesForRMARequest(int creditId, string comID, string supName)
        {
            var attachements = new System.Collections.Generic.List<Attachment>();
            var filePaths = DelToneCommonSettingsPath.fileattachement;
            filePaths = string.Format(filePaths, comID, creditId, supName);
            if (Directory.Exists(filePaths))
            {
                var allSuppNeedFiles = Directory.GetFiles(filePaths);
                foreach (string filePath in allSuppNeedFiles)
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(filePath);
                    attachment.Name = Path.GetFileName(filePath);
                    attachements.Add(attachment);

                }
            }

            return attachements;

        }

        public string GetRMANotesById(int rmaid)
        {
            var result = "";

            SqlConnection sqlcon = new SqlConnection();
            sqlcon.ConnectionString = CONNSTRING;
            using (SqlCommand cmd = new SqlCommand("dbo.spgetrmantoes", sqlcon))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rmaid", SqlDbType.Int).Value = rmaid;
               // cmd.Parameters.AddWithValue("@notes", SqlDbType.VarChar).Direction = ParameterDirection.Output;
                sqlcon.Open();
               
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = (reader["notes"].ToString());
                    }
                }
                sqlcon.Close();

            }

            return result;
        }

        public int GetRMAByCreditNoteIdAndSuppName(int creditNoteId, string suppName)
        {
            var rmaId = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT RMAID from RMATracking rt where rt.CreditNoteID=@creditNoteId AND rt.SupplierName=@suppName";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@creditNoteId", SqlDbType.Int).Value = creditNoteId;
                cmd.Parameters.AddWithValue("@suppName", SqlDbType.NVarChar).Value = suppName;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rmaId = Convert.ToInt32(reader["RMAID"]);
                    }
                }
            }
            conn.Close();
            return rmaId;
        }
        public string GetSupplierRMANumberByCreditNoteIdAndSuppName(int creditNoteId, string suppName)
        {
            var rmaId ="";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT SupplierRMANumber from RMATracking rt where rt.CreditNoteID=@creditNoteId AND rt.SupplierName=@suppName";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@creditNoteId", SqlDbType.Int).Value = creditNoteId;
                cmd.Parameters.AddWithValue("@suppName", SqlDbType.NVarChar).Value = suppName;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rmaId = reader["SupplierRMANumber"].ToString();
                    }
                }
            }
            conn.Close();
            return rmaId;
        }
        public string GetRAMFAULTYGoodsByCreditId(int creditId)
        {
            var conCatString = "";
            var crId = Convert.ToInt32(creditId);
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT BatchNumber,ModelNumber,ErrorMessage,SupplierName ,FaultyNotes from RMATracking 
                                      where CreditNoteID=@creditNoteId ";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@creditNoteId", SqlDbType.Int).Value = crId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        conCatString = conCatString + reader["SupplierName"].ToString();
                        if (reader["BatchNumber"] != DBNull.Value)
                            conCatString = conCatString + " Batch Number: " + reader["BatchNumber"].ToString() + " ";
                        if (reader["ModelNumber"] != DBNull.Value)
                            conCatString = conCatString + " Model Number: " + reader["ModelNumber"].ToString() + " ";
                        if (reader["ErrorMessage"] != DBNull.Value)
                            conCatString = conCatString + " Error : " + reader["ErrorMessage"].ToString() + " ";
                        if (reader["FaultyNotes"] != DBNull.Value)
                            conCatString = conCatString + " FaultyNotes : " + reader["FaultyNotes"].ToString() + " ";
                    }
                }
            }
            conn.Close();
            return conCatString;
        }

        public CreditNoteRMAFaultyGoods GetRMAFaultyGoods(CreditNoteRMAFaultyGoods obj)
        {
            var creditId = Convert.ToInt32(obj.CreditNoteId);
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT RMAID,BatchNumber,ModelNumber,ErrorMessage,FaultyNotes from RMATracking 
                                      rt where rt.CreditNoteID=@creditNoteId AND rt.SupplierName=@suppName";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@creditNoteId", SqlDbType.Int).Value = creditId;
                cmd.Parameters.AddWithValue("@suppName", SqlDbType.NVarChar).Value = obj.SupplierName;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        obj.RMAId = Convert.ToInt32(reader["RMAID"].ToString());
                        if (reader["BatchNumber"] != DBNull.Value)
                            obj.BatchNumber = reader["BatchNumber"].ToString();
                        if (reader["ModelNumber"] != DBNull.Value)
                            obj.ModelNumber = reader["ModelNumber"].ToString();
                        if (reader["ErrorMessage"] != DBNull.Value)
                            obj.ErrorMessage = reader["ErrorMessage"].ToString();
                        if (reader["FaultyNotes"] != DBNull.Value)
                            obj.FaultyNotes = reader["FaultyNotes"].ToString();
                        
                    }
                }
            }
            conn.Close();
            return obj;

        }

        public void UpdateRMAFORFaultyGoods(CreditNoteRMAFaultyGoods obj)
        {
            var creditId = Convert.ToInt32(obj.CreditNoteId);

            if (GetRMAByCreditNoteIdAndSuppName(creditId, obj.SupplierName) == 0)
                new CreditNotesDAL(CONNSTRING).WriteSupplierIntoRMA(obj.CreditNoteId, obj.SupplierName);
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            string sqlStmt = @"UPDATE dbo.RMATracking SET BatchNumber=@batchNumber,
                            ModelNumber=@modelNumber,ErrorMessage=@errorMessage,FaultyNotes=@faultyNotes WHERE CreditNoteID =@crId AND SupplierName=@suppName";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = sqlStmt;

                cmd.Parameters.Add("@batchNumber", SqlDbType.NVarChar).Value = obj.BatchNumber;
                cmd.Parameters.Add("@modelNumber", SqlDbType.NVarChar).Value = obj.ModelNumber;
                cmd.Parameters.Add("@errorMessage", SqlDbType.NVarChar).Value = obj.ErrorMessage;
                cmd.Parameters.Add("@suppName", SqlDbType.NVarChar).Value = obj.SupplierName;
                cmd.Parameters.Add("@faultyNotes", SqlDbType.NVarChar).Value = obj.FaultyNotes;
                cmd.Parameters.Add("@crId", SqlDbType.Int).Value = creditId;

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }




        public void UpdateRMISentToSupplier(int creditNoteId)
        {
            //  String SqlStmt = "UPDATE dbo.RMATracking SET SentToSupplier=@SentToSupplier, SentToSupplierDateTime=@SentToSupplierDateTime, 
            //ApprovedRMA=@ApprovedRMA, ApprovedRMADateTime=@ApprovedRMADateTime, CreditInXero=@CreditInXero, 
            //CreditInXeroDateTime=@CreditInXeroDateTime, RMAToCustomer=@RMAToCustomer, RMAToCustomerDateTime=@RMAToCustomerDateTime, 
            //AdjustedNoteFromSupplier=@AdjustedNoteFromSupplier, AdjustedNoteFromSupplierDateTime=@AdjustedNoteFromSupplierDateTime,
            //Status=@Status, SupplierRMANumber=@SupplierRMANumber, TrackingNumber=@TrackingNumber WHERE RMAID = " + Request.QueryString["QID"].ToString();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            string sqlStmt = @"UPDATE dbo.RMATracking SET SentToSupplier=@SentToSupplier, SentToSupplierDateTime=CURRENT_TIMESTAMP WHERE CreditNoteID =@crId";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = sqlStmt;
                cmd.Parameters.Add("@SentToSupplier", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@crId", SqlDbType.Int).Value = creditNoteId;

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void UpdateRMISentToSupplierByRAMID(int rmaID)
        {
            //  String SqlStmt = "UPDATE dbo.RMATracking SET SentToSupplier=@SentToSupplier, SentToSupplierDateTime=@SentToSupplierDateTime, 
            //ApprovedRMA=@ApprovedRMA, ApprovedRMADateTime=@ApprovedRMADateTime, CreditInXero=@CreditInXero, 
            //CreditInXeroDateTime=@CreditInXeroDateTime, RMAToCustomer=@RMAToCustomer, RMAToCustomerDateTime=@RMAToCustomerDateTime, 
            //AdjustedNoteFromSupplier=@AdjustedNoteFromSupplier, AdjustedNoteFromSupplierDateTime=@AdjustedNoteFromSupplierDateTime,
            //Status=@Status, SupplierRMANumber=@SupplierRMANumber, TrackingNumber=@TrackingNumber WHERE RMAID = " + Request.QueryString["QID"].ToString();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            string sqlStmt = @"UPDATE dbo.RMATracking SET SentToSupplier=@SentToSupplier, SentToSupplierDateTime=CURRENT_TIMESTAMP WHERE RMAID =@crId";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = sqlStmt;
                cmd.Parameters.Add("@SentToSupplier", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@crId", SqlDbType.Int).Value = rmaID;

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void UpdateRMISentToCustomer(int creditNoteId)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            string sqlStmt = @"UPDATE dbo.RMATracking SET RMAToCustomer=@RMAToCustomer, RMAToCustomerDateTime=CURRENT_TIMESTAMP WHERE CreditNoteID =@crId";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = sqlStmt;
                cmd.Parameters.Add("@RMAToCustomer", SqlDbType.Int).Value = 1;
                cmd.Parameters.Add("@crId", SqlDbType.Int).Value = creditNoteId;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public static class DelToneCommonSettingsPath
        {
             public const string fileattachement = "C:\\inetpub\\wwwroot\\DeltoneCRM\\CompanyUploads\\CreditsNotes\\{0}\\{1}\\{2}";    //this one for a server
            // var domainNameWithFile = "http://localhost:65085//CompanyUploads/CreditsNotes/";
            // var domainNameWithFileServer = "http://delcrm//CompanyUploads/CreditsNotes/";

            //  public const string fileattachement = "C:\\WebProjects\\DeltoneCRM-GIT\\DeltoneCRM\\CompanyUploads\\CreditsNotes\\{0}\\{1}\\{2}";   //this one for a local
            // var filePathWithDomain = domainNameWithFile + str_AccountID + "/" + str_CreditId + "/" + str_SuppName + "/" + filePathOri;
        }

        public class RMAUpdate
        {
            public string SuppName { get; set; }
            public string CreId { get; set; }
            public string STS { get; set; }
            public string ARMA { get; set; }
            public string CIX { get; set; }
            public string RTC { get; set; }
            public string ANFS { get; set; }
            public string INHouse { get; set; }
            public string SRMAN { get; set; }
            public string TN { get; set; }
            public string Notes { get; set; }
            public string chk_Completed { get; set; }

        }
    }
}
