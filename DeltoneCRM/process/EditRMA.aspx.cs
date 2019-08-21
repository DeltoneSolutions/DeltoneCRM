using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using DeltoneCRM_DAL;

namespace DeltoneCRM.process
{
    public partial class EditRMA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            var connStr = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            conn.ConnectionString = connStr;
            String SqlStmt = @"UPDATE dbo.RMATracking SET SentToSupplier=@SentToSupplier, 
                      SentToSupplierDateTime=@SentToSupplierDateTime, ApprovedRMA=@ApprovedRMA, ApprovedRMADateTime=@ApprovedRMADateTime,
                      CreditInXero=@CreditInXero, CreditInXeroDateTime=@CreditInXeroDateTime, RMAToCustomer=@RMAToCustomer, 
                      RMAToCustomerDateTime=@RMAToCustomerDateTime, AdjustedNoteFromSupplier=@AdjustedNoteFromSupplier, 
                      AdjustedNoteFromSupplierDateTime=@AdjustedNoteFromSupplierDateTime, Status=@Status, SupplierRMANumber=@SupplierRMANumber, 
                TrackingNumber=@TrackingNumber , InHouse=@inHouse,Notes=@notes WHERE RMAID = " + Request.QueryString["QID"].ToString();
            SqlCommand cmd = new SqlCommand(SqlStmt, conn);

            String RMAID = Request.QueryString["QID"].ToString();

            String STS = Request.QueryString["STS"].ToString();
            String STSWrite = String.Empty;
            //DateTime STSDateTime = new DateTime();
            if (STS == "true")
            {
                STSWrite = "1";
            }
            else
            {
                STSWrite = "0";
            }

            String inHouse = Request.QueryString["INHouse"].ToString();
            String inHouseWrite = String.Empty;
            String completed = Request.QueryString["chk_Completed"].ToString();
            String notes = Request.QueryString["Notes"].ToString();
            //DateTime STSDateTime = new DateTime();
            if (inHouse == "true")
            {
                inHouseWrite = "1";
            }
            else
            {
                inHouseWrite = "0";
            }

            cmd.Parameters.AddWithValue("@inHouse", inHouseWrite);

            String ARMA = Request.QueryString["ARMA"].ToString();
            String ARMAWrite = String.Empty;

            if (ARMA == "true")
            {
                ARMAWrite = "1";
            }
            else
            {
                ARMAWrite = "0";
            }

            String CIX = Request.QueryString["CIX"].ToString();
            String CIXWrite = String.Empty;

            if (CIX == "true")
            {
                CIXWrite = "1";
            }
            else
            {
                CIXWrite = "0";
            }

            String RTC = Request.QueryString["RTC"].ToString();
            String RTCWrite = String.Empty;

            if (RTC == "true")
            {
                RTCWrite = "1";
            }
            else
            {
                RTCWrite = "0";
            }

            String ANFS = Request.QueryString["ANFS"].ToString();
            String ANFSWrite = String.Empty;

            if (ANFS == "true")
            {
                ANFSWrite = "1";
            }
            else
            {
                ANFSWrite = "0";
            }

            String NewStatus = String.Empty;
            NewStatus = "IN PROGRESS";
            NewStatus = new CreditNotesDAL(connStr).RMAStatusByRMAID(RMAID);



            //if (inHouseWrite == "1")
            //{
            //    NewStatus = "COMPLETED";
            //}
            //else

            //    if (ANFSWrite == "1")
            //    {
            //        NewStatus = "COMPLETED";
            //    }
            //    else
            if (completed == "true")
                NewStatus = "COMPLETED";
            else
                NewStatus = "IN PROGRESS";

            //if (STS == "true" && ARMA == "true" && CIX == "true" && RTC == "true" && ANFS == "true")
            //{
            //    NewStatus = "COMPLETED";
            //}
            //else
            //{
            //    NewStatus = "IN PROGRESS";
            //}
            var notesValues = notes;

            var notesHistory = SetCreditNotesHistory(Convert.ToInt32(RMAID), notesValues);
            cmd.Parameters.AddWithValue("@notes", notesHistory);

            cmd.Parameters.AddWithValue("@SentToSupplier", STSWrite);
            if (STS == "true")
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

            if (ARMA == "true")
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

            if (CIX == "true")
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

            if (RTC == "true")
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

            if (ANFS == "true")
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
            cmd.Parameters.AddWithValue("@SupplierRMANumber", Request.QueryString["SRMAN"].ToString());
            cmd.Parameters.AddWithValue("@TrackingNumber", Request.QueryString["TN"].ToString());
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private  string SetCreditNotesHistory(int rmaId, string recentNotes)
        {
            var comNotes = HttpContext.Current.Session["LoggedUser"] + "--<b><font  color=\"red\">" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</font></b>--"
                + recentNotes;
            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var prevoiusNote = new CreditNoteRMAHandler(CONNSTRING).GetRMANotesById(rmaId);
            var combinecomNotes = comNotes + "<br/><br/>" + prevoiusNote;

          

            return combinecomNotes;
        }

        private void sentRMATOCustomer(string RmaId)
        {
            var ramIdC = Convert.ToInt32(RmaId);
            var rmaTrack = GetCreditNoteIdFromRAMTracking(ramIdC);


            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);
            var orderDal = new OrderDAL(strConnectionString);
            var creditNoteDal = new CreditNotesDAL(strConnectionString);
            var ordersendEmail = new OrderSendEmailDAL(strConnectionString);
            var creditNoteObj = creditNoteDal.getCreditNoteObj(rmaTrack.CreditNoteID);
            var creditEmailHandler = new CreditNoteRMAHandler(strConnectionString);

            if (creditNoteObj.IsAvail)
            {
                if (creditNoteObj.Reason == "CHANGED PRINTER")
                {
                    var contactId = Convert.ToInt32(creditNoteObj.ContactId);
                    var contact = cdal.GetContactByContactId(contactId);
                    if (!string.IsNullOrEmpty(creditNoteObj.Reason))
                    {

                        var orderID = Convert.ToInt32(creditNoteObj.OrderId);
                        var xeroOrderDTSnumber = orderDal.getXeroDTSID(orderID);
                        var subject = "RMA Request for " + creditNoteObj.Reason;

                        var body = "Hi , <br/>  RMA Request  for this order number :"
                            + xeroOrderDTSnumber;
                        if (!string.IsNullOrEmpty(rmaTrack.SupplierRMANumber))
                        {
                            body = body + "<br/> Supplier RMA number " + rmaTrack.SupplierRMANumber;
                        }
                        if (!string.IsNullOrEmpty(rmaTrack.TrackingNumber))
                        {
                            body = body + " <br/> TrackingNumber  " + rmaTrack.TrackingNumber;
                        }

                        var toEmail = contact.Email;

                        var from = "info@deltonesolutions.com.au";
                        var fromName = "Deltonesolutions";

                        var res = creditEmailHandler.SendCreditNoteEmail(rmaTrack.CreditNoteID, "", "", from, fromName, toEmail,
                                  "", "", subject, body, true, null);

                        if (res)
                        {
                            creditEmailHandler.UpdateRMISentToCustomer(rmaTrack.CreditNoteID);
                        }
                    }

                }


            }
        }

        protected String checkIfSTSHasValue(String RID)
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

        protected String checkIfARMAHasValue(String RID)
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

        private RMATrackingClient GetCreditNoteIdFromRAMTracking(int RmaId)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var rMATrackingClient = new RMATrackingClient();

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

        protected String checkIfCIXHasValue(String RID)
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

        protected String checkIfRTCHasValue(String RID)
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

        protected String checkIfANFSHasValue(String RID)
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

        public class RMATrackingClient
        {
            public int CreditNoteID { get; set; }
            public string SupplierRMANumber { get; set; }
            public string TrackingNumber { get; set; }
        }
    }
}