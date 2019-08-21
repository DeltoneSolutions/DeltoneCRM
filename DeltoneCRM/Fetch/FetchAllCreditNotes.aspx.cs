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

namespace DeltoneCRM.Fetch
{
    public partial class FetchAllCreditNotes : System.Web.UI.Page
    {

        public enum RMASTATUS
        {

            CREDITPENDING = 1,
            AWAITINGRMA = 2,
            AWAITINGTRACKING = 3,
            CREDITINXERO = 4,
            COMPLETED = 6,
            All = 5,
            RMARECEIVED = 7,
            TRACKINGRECEIVED = 8,
            NOTCOMPLETED = 9
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var status = (int)RMASTATUS.All;

            if (!string.IsNullOrEmpty(Request["s"]))
                status = Convert.ToInt32(Request["s"]);
            Response.Write(ReturnCreditNotes(status));
        }

        //This  method  Return all the CreditNotes 
        protected String ReturnCreditNotes(int status)
        {
            String strOutput = "{\"data\":[";

            using (SqlConnection conn = new SqlConnection())
            {
                var statusRepl = "";
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        if (status == (int)RMASTATUS.NOTCOMPLETED)
                        {

                            cmd.CommandText = @"SELECT CN.CreditNote_ID, CN.XeroCreditNoteID, CP.CompanyName,ar.RMAToCustomer,ar.InHouse,ar.SentToSupplier, ar.AdjustedNoteFromSupplier,CP.CompanyID, CT.ContactID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,ar.Status as AStatus,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
                     join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.CreditNoteReason<>'PRICE REDUCTION' AND ar.Status<>'COMPLETED'
                          ORDER BY CN.CreditNote_ID DESC";
                        }
                        else
                            if (status == (int)RMASTATUS.CREDITINXERO)
                            {

                                cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName,ar.AdjustedNoteFromSupplier, ar.InHouse,CP.CompanyID,ar.Status as AStatus, CT.ContactID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
                     join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID 
                         AND ar.CreditInXero=1 ORDER BY CN.CreditNote_ID DESC";
                            }
                            else
                                if (status == (int)RMASTATUS.AWAITINGTRACKING)
                                {
                                    cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName,ar.Status as AStatus, CP.CompanyID, CT.ContactID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.InHouse, ar.AdjustedNoteFromSupplier,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.CreditNoteReason<>'PRICE REDUCTION'
                     AND  ar.RMAToCustomer=1   AND (ar.TrackingNumber is null or  ar.TrackingNumber='') ORDER BY CN.CreditNote_ID DESC";
                                }
                                else
                                    if (status == (int)RMASTATUS.TRACKINGRECEIVED)
                                    {
                                        cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName,ar.Status as AStatus, CP.CompanyID, CT.ContactID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.InHouse, ar.AdjustedNoteFromSupplier,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.CreditNoteReason<>'PRICE REDUCTION'
                         AND  ar.RMAToCustomer=1   AND ( ar.TrackingNumber<>'') ORDER BY CN.CreditNote_ID DESC";
                                    }

                                    else
                                        if (status == (int)RMASTATUS.COMPLETED)
                                        {
                                            statusRepl = "COMPLETED";
                                            cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName, CP.CompanyID, CT.ContactID,ar.Status as AStatus, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.CreditNoteReason<>'PRICE REDUCTION'
                         AND ( ar.Status='COMPLETED')  ORDER BY CN.CreditNote_ID DESC";
                                        }

                                        else
                                            if (status == (int)RMASTATUS.AWAITINGRMA)
                                            {
                                                cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID,ar.Status as AStatus, CP.CompanyName, CP.CompanyID, CT.ContactID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.InHouse, ar.AdjustedNoteFromSupplier,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.CreditNoteReason<>'PRICE REDUCTION'
                    AND  ar.SentToSupplier=1   AND (ar.SupplierRMANumber is null or  ar.SupplierRMANumber='')   AND  ar.RMAToCustomer=0  ORDER BY CN.CreditNote_ID DESC";
                                            }
                                            else
                                                if (status == (int)RMASTATUS.RMARECEIVED)
                                                {
                                                    cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID,ar.Status as AStatus, CP.CompanyName, CP.CompanyID, CT.ContactID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.InHouse, ar.AdjustedNoteFromSupplier,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.CreditNoteReason<>'PRICE REDUCTION'
                         AND  ar.SentToSupplier=1  AND ( ar.SupplierRMANumber <>'') AND  ar.RMAToCustomer=0  ORDER BY CN.CreditNote_ID DESC";
                                                }
                                                else
                                                    if (status == (int)RMASTATUS.CREDITPENDING)
                                                    {
                                                        cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID,ar.Status as AStatus,ar.AdjustedNoteFromSupplier,ar.InHouse, CP.CompanyName, CP.CompanyID, CT.ContactID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID 
                          AND ar.CreditInXero=0 ORDER BY CN.CreditNote_ID DESC";
                                                    }
                                                    else
                                                    {
                                                        cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID,ar.AdjustedNoteFromSupplier, CP.CompanyName, CP.CompanyID, CT.ContactID, ar.RMAID , ar.CreditInXero, ar.InHouse,ar.SupplierName,orc.XeroInvoiceNumber,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,ar.Status as AStatus,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID 
                         ORDER BY CN.CreditNote_ID DESC";
                                                    }

                    }
                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                    {
                        if (status == (int)RMASTATUS.NOTCOMPLETED)
                        {

                            cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName, CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.RMAToCustomer,ar.InHouse,ar.SentToSupplier, ar.AdjustedNoteFromSupplier,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,ar.Status as AStatus,CN.Total, 
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.CreditNoteReason<>'PRICE REDUCTION'
                                 AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + "ORDER BY CN.CreditNote_ID DESC";
                        }
                        else

                            if (status == (int)RMASTATUS.CREDITINXERO)
                            {

                                cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID,ar.Status as AStatus, CP.CompanyName,ar.InHouse,ar.AdjustedNoteFromSupplier, CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, 
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND ar.CreditInXero=1  
                                 AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY CN.CreditNote_ID DESC";
                            }
                            else
                                if (status == (int)RMASTATUS.AWAITINGTRACKING)
                                {
                                    cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID,ar.Status as AStatus, CP.CompanyName, CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.InHouse, ar.AdjustedNoteFromSupplier,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, 
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND (ar.TrackingNumber is null or  ar.TrackingNumber='')  AND CN.CreditNoteReason<>'PRICE REDUCTION' 
                                 AND  ar.RMAToCustomer=1  AND ar.Status<>'COMPLETED' AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY CN.CreditNote_ID DESC";
                                }
                                else
                                    if (status == (int)RMASTATUS.TRACKINGRECEIVED)
                                    {
                                        cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID,ar.Status as AStatus, CP.CompanyName, CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.InHouse, ar.AdjustedNoteFromSupplier,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, 
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND ( ar.TrackingNumber<>'')  AND CN.CreditNoteReason<>'PRICE REDUCTION'
                                AND  ar.RMAToCustomer=1   AND ar.Status<>'COMPLETED' AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY CN.CreditNote_ID DESC";
                                    }
                                    else
                                        if (status == (int)RMASTATUS.COMPLETED)
                                        {
                                            statusRepl = "COMPLETED";
                                            cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName,ar.Status as AStatus, CP.CompanyID, CT.ContactID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                    CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, 
                         dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND CN.CreditNoteReason<>'PRICE REDUCTION'
                       AND ( ar.Status='COMPLETED') AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY CN.CreditNote_ID DESC";
                                        }

                                        else
                                            if (status == (int)RMASTATUS.AWAITINGRMA)
                                            {
                                                cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName, ar.Status as AStatus,CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.InHouse, ar.AdjustedNoteFromSupplier,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, 
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND (ar.SupplierRMANumber is null or  ar.SupplierRMANumber='')  AND CN.CreditNoteReason<>'PRICE REDUCTION'
                               AND  ar.SentToSupplier=1   AND  ar.RMAToCustomer=0    AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + "ORDER BY CN.CreditNote_ID DESC";
                                            }
                                            else
                                                if (status == (int)RMASTATUS.RMARECEIVED)
                                                {
                                                    cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName,ar.Status as AStatus, CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,ar.InHouse, ar.AdjustedNoteFromSupplier,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, 
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND ( ar.SupplierRMANumber<>'')  AND CN.CreditNoteReason<>'PRICE REDUCTION'
                                  AND  ar.SentToSupplier=1  AND  ar.RMAToCustomer=0    AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + "ORDER BY CN.CreditNote_ID DESC";
                                                }
                                                else
                                                    if (status == (int)RMASTATUS.CREDITPENDING)
                                                    {
                                                        cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName,ar.InHouse,ar.AdjustedNoteFromSupplier,ar.Status as AStatus, CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, 
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
  join dbo.Orders orc  on CN.OrderID=orc.OrderID 
                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID AND ar.CreditInXero=0  
                                   AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + "ORDER BY CN.CreditNote_ID DESC";
                                                    }
                                                    else
                                                    {
                                                        cmd.CommandText = @"SELECT CN.CreditNote_ID,CN.XeroCreditNoteID,ar.AdjustedNoteFromSupplier, CP.CompanyName, CP.CompanyID, ar.RMAID,ar.SupplierName,orc.XeroInvoiceNumber,
                                       CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,ar.Status as AStatus,CN.Total, ar.CreditInXero, ar.InHouse,
                                        CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN 
join dbo.Orders orc  on CN.OrderID=orc.OrderID  

                    join   dbo.RMATracking ar  on CN.CreditNote_ID=ar.CreditNoteID, dbo.Contacts CT, dbo.Companies CP 
                                     WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID  
                                  AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY CN.CreditNote_ID DESC";
                                                    }


                    }
                    cmd.Connection = conn;
                    conn.Open();
                    var eleAdde = false;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                //EditOrder(OrderID, CompanyID, ContactID)
                                String InvoiceNum = String.Empty;
                                statusRepl = sdr["Status"].ToString();

                                //Modified Here 
                                var canAdd = true;
                                if (status == (int)RMASTATUS.NOTCOMPLETED)
                                {
                                    var statusRMA = sdr["AStatus"].ToString();
                                    if (sdr["AdjustedNoteFromSupplier"].ToString() == "1")
                                        canAdd = false;
                                    else
                                        if (sdr["RMAToCustomer"].ToString() == "1")
                                            canAdd = false;
                                        else if (sdr["SentToSupplier"].ToString() == "1")
                                            canAdd = false;
                                        else
                                            if (sdr["InHouse"] != DBNull.Value)
                                            {
                                                if (sdr["InHouse"].ToString() == "1")
                                                    canAdd = false;
                                            }
                                            else
                                                if (statusRMA == "COMPLETED")
                                                    canAdd = false;

                                }


                                else
                                {
                                    if (status == (int)RMASTATUS.AWAITINGRMA || status == (int)RMASTATUS.RMARECEIVED ||
                                        status == (int)RMASTATUS.AWAITINGTRACKING || status == (int)RMASTATUS.TRACKINGRECEIVED)
                                    {
                                        var statusRMA = sdr["AStatus"].ToString();

                                        if (statusRMA == "COMPLETED")
                                            canAdd = false;
                                        else if (sdr["AdjustedNoteFromSupplier"].ToString() == "1")
                                            canAdd = false;
                                        else
                                            if (sdr["InHouse"].ToString() == "1")
                                                canAdd = false;

                                        if (status == (int)RMASTATUS.AWAITINGRMA)
                                        {
                                            var reaon = sdr["CreditNoteReason"].ToString();
                                            if (reaon == "CHANGED PRINTER")
                                                canAdd = false;
                                        }
                                    }
                                    else
                                    {
                                        if (status == (int)RMASTATUS.CREDITINXERO || status == (int)RMASTATUS.CREDITPENDING)
                                        {
                                            var statusRMA = sdr["AStatus"].ToString();

                                            if (statusRMA == "COMPLETED")
                                                canAdd = false;
                                            else if (sdr["AdjustedNoteFromSupplier"].ToString() == "1")
                                                canAdd = false;
                                            else
                                                if (sdr["InHouse"].ToString() == "1")
                                                    canAdd = false;
                                        }

                                        else
                                        {
                                             statusRepl = sdr["AStatus"].ToString();
                                            if (status == (int)RMASTATUS.COMPLETED)
                                                statusRepl = "COMPLETED";
                                            ////if (status == (int)RMASTATUS.All)
                                            ////{
                                            //    if (sdr["AdjustedNoteFromSupplier"].ToString() == "1")
                                            //        statusRepl = statusRMA;
                                            //    else
                                            //        if (sdr["InHouse"].ToString() == "1")
                                            //            statusRepl = statusRMA;
                                            //        else
                                            //            if (sdr["AStatus"].ToString() == "COMPLETED")
                                            //                statusRepl = statusRMA;
                                            //}
                                        }
                                    }
                                }

                                if (canAdd)
                                {
                                    InvoiceNum = (sdr["XeroCreditNoteID"] == DBNull.Value) ? "DTS-" + sdr["XeroCreditNoteID"] : sdr["XeroCreditNoteID"].ToString();

                                    String strEditOrder = "<img src='../Images/Edit.png'  onclick='openWin(" + sdr["CreditNote_ID"].ToString() + "," + sdr["ContactID"] + "," + sdr["CompanyID"] + ");'>";
                                    String Total = sdr["Total"].ToString();
                                    Decimal ConvertedTotal = Math.Round(Convert.ToDecimal(Total), 2);
                                    String OrderDate = Convert.ToDateTime(sdr["DateCreated"]).ToString("dd-MMM-yyyy");
                                    String StrCreditReson = sdr["CreditNoteReason"].ToString();

                                    //String DueDate = (!DBNull.Value.Equals(sdr["DueDate"])) ? Convert.ToDateTime(sdr["DueDate"]).ToString("dd-MM-yyyy") : String.Empty;
                                    String CreatedBy = sdr["CreatedBy"].ToString();
                                    //  var allSuppName = GetAllSuppNames(sdr["CreditNote_ID"].ToString());
                                    String Link = "<input name='selectchk' value='" + sdr["RMAID"].ToString() + "' type='checkbox' />";
                                    String strRMAView = "<img src='../Images/Edit.png'  onclick='showRMAWindow(" + sdr["RMAID"].ToString() + ");'>";


                                    eleAdde = true;
                                    strOutput = strOutput + "[\"" + Link + "\"," + "\"" + sdr["CreditNote_ID"]
                                        + "\"," + "\"" + sdr["CompanyName"] + "\"," + "\"" + sdr["FirstName"]
                                        + " " + sdr["LastName"] + "\","
                                         + "\"" + sdr["XeroInvoiceNumber"] + "\","
                                        + "\"" + InvoiceNum + "\","
                                        + "\"" + "$" + ConvertedTotal + "\"," + "\"" + OrderDate + "\"," + "\""
                                        + StrCreditReson + "\"," + "\"" + CreatedBy + "\","
                                          + "\"" + sdr["SupplierName"].ToString() + "\","
                                        + "\"" + statusRepl + "\","
                                         + "\""
                                        + strEditOrder + "\","
                                         + "\"" + strRMAView + "\"],";
                                }

                            }
                            int Length = strOutput.Length;
                            if (eleAdde)
                                strOutput = strOutput.Substring(0, (Length - 1));
                        }

                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }


            }

            // SELECT CN.CreditNote_ID,CN.XeroCreditNoteID, CP.CompanyName, CP.CompanyID, CT.ContactID, CT.FirstName, CT.LastName, CN.CreatedBy,CN.Status,CN.Total, CN.DateCreated,CN.CreditNoteReason FROM dbo.CreditNotes CN, dbo.Contacts CT, dbo.Companies CP WHERE CN.ContactID = CT.ContactID AND CN.CompID = CT.CompanyID AND CN.CompID= CP.CompanyID ORDER BY CN.CreditNote_ID DESC

            return strOutput;
        }


        private string GetAllSuppNames(string str_ORDERID)
        {
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var creditnotedal = new CreditNotesDAL(connString);

            var allSupp = creditnotedal.GetSuppliersForCreditNoteById(str_ORDERID);

            string joined = string.Join(",", allSupp);

            return joined;
        }


    }
}