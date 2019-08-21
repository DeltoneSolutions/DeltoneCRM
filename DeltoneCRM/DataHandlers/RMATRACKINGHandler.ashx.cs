using DeltoneCRM.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace DeltoneCRM.DataHandlers
{
    /// <summary>
    /// Summary description for RMATRACKINGHandler
    /// </summary>
    public class RMATRACKINGHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            var allStatus = (int)RMASTATUS.All;
            var status = allStatus;
            if (context.Request["s"] != null)
            {
                if (!string.IsNullOrEmpty(context.Request["s"]))
                {
                    status = Convert.ToInt32(context.Request["s"]);
                }
                else
                {
                    status = allStatus;
                }
                context.Response.Write(ReturnOrders(status, context));

            }
            else
                context.Response.Write(ReturnOrders(status, context));
        }

        protected String ReturnOrders(int param, HttpContext context)
        {
            List<RMATRACKINGUI> rmaList = new List<RMATRACKINGUI>();
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (param == (int)RMASTATUS.CREDITINXERO)
                    {
                        cmd.CommandText = @"SELECT ar.*,cn.XeroCreditNoteID  FROM dbo.RMATracking ar join dbo.CreditNotes cn
  on cn.CreditNote_ID=ar.CreditNoteID WHERE CreditInXero=1  ORDER BY RMAID DESC";
                    }
                    else
                    {
                        if (param == (int)RMASTATUS.CREDITPENDING)
                        {

                            cmd.CommandText = @"SELECT ar.*,cn.XeroCreditNoteID  FROM dbo.RMATracking ar join dbo.CreditNotes cn
  on cn.CreditNote_ID=ar.CreditNoteID WHERE CreditInXero=0 ORDER BY RMAID DESC";

                        }
                        else
                        {
                            if (param == (int)RMASTATUS.AWAITINGRMA)
                            {
                                cmd.CommandText = @"SELECT ar.*,cn.XeroCreditNoteID  FROM dbo.RMATracking ar join dbo.CreditNotes cn
  on cn.CreditNote_ID=ar.CreditNoteID WHERE  (SupplierRMANumber is null or  SupplierRMANumber='') ORDER BY RMAID DESC";


                            }
                            else
                            {
                                if (param == (int)RMASTATUS.AWAITINGTRACKING)
                                {
                                    cmd.CommandText = @"SELECT ar.*,cn.XeroCreditNoteID  FROM dbo.RMATracking ar join dbo.CreditNotes cn
  on cn.CreditNote_ID=ar.CreditNoteID  WHERE   (TrackingNumber is null or  TrackingNumber='')  ORDER BY RMAID DESC";


                                }
                                else

                                    cmd.CommandText = @"SELECT ar.*,cn.XeroCreditNoteID  FROM dbo.RMATracking ar join dbo.CreditNotes cn
  on cn.CreditNote_ID=ar.CreditNoteID  ORDER BY RMAID DESC";

                            }
                        }
                    }
                    cmd.Connection = conn;
                    conn.Open();


                  //  DataTable dt = new DataTable();
                   // var resultReader = cmd.ExecuteReader();
                   // dt.Load(resultReader);
                    //foreach (DataRow row in dt.Rows)
                    // {
                    //{
                    using (SqlDataReader row = cmd.ExecuteReader())
                    {
                        if (row.HasRows)
                        {

                            while (row.Read())
                            {
                                var rmaItem = new RMATRACKINGUI();
                                rmaItem.RMAID = row["RMAID"].ToString();
                                rmaItem.CreditNoteID = row["XeroCreditNoteID"].ToString();
                                if (row["AdjustedNoteFromSupplier"] != DBNull.Value)
                                    rmaItem.AdjustedNoteFromSupplier = row["AdjustedNoteFromSupplier"].ToString();
                                if (row["AdjustedNoteFromSupplierDateTime"] != DBNull.Value)
                                    rmaItem.AdjustedNoteFromSupplierDateTime = Convert.ToDateTime(row["AdjustedNoteFromSupplierDateTime"].ToString()).ToShortDateString();
                                if (row["ApprovedRMA"] != DBNull.Value)
                                    rmaItem.ApprovedRMA = row["ApprovedRMA"].ToString();
                                if (row["ApprovedRMADateTime"] != DBNull.Value)
                                    rmaItem.ApprovedRMADateTime = Convert.ToDateTime(row["ApprovedRMADateTime"].ToString()).ToShortDateString();
                                if (row["BatchNumber"] != DBNull.Value)
                                    rmaItem.BatchNumber = row["BatchNumber"].ToString();
                                if (row["CreditInXero"] != DBNull.Value)
                                    rmaItem.CreditInXero = row["CreditInXero"].ToString();
                                if (row["CreditInXeroDateTime"] != DBNull.Value)
                                    rmaItem.CreditInXeroDateTime = Convert.ToDateTime(row["CreditInXeroDateTime"].ToString()).ToShortDateString();
                                if (row["ErrorMessage"] != DBNull.Value)
                                    rmaItem.ErrorMessage = row["ErrorMessage"].ToString();
                                if (row["InHouse"] != DBNull.Value)
                                    rmaItem.InHouse = row["InHouse"].ToString();
                                if (row["ModelNumber"] != DBNull.Value)
                                    rmaItem.ModelNumber = row["ModelNumber"].ToString();
                                if (row["RaisedDateTime"] != DBNull.Value)
                                    rmaItem.RaisedDateTime = Convert.ToDateTime(row["RaisedDateTime"].ToString()).ToShortDateString();
                                if (row["RMAToCustomer"] != DBNull.Value)
                                    rmaItem.RMAToCustomer = row["RMAToCustomer"].ToString();
                                if (row["RMAToCustomerDateTime"] != DBNull.Value)
                                    rmaItem.RMAToCustomerDateTime = Convert.ToDateTime(row["RMAToCustomerDateTime"].ToString()).ToShortDateString();
                                if (row["SentToSupplier"] != DBNull.Value)
                                    rmaItem.SentToSupplier = row["SentToSupplier"].ToString();
                                if (row["SentToSupplierDatetime"] != DBNull.Value)
                                    rmaItem.SentToSupplierDatetime = Convert.ToDateTime(row["SentToSupplierDatetime"].ToString()).ToShortDateString();

                                rmaItem.Status = row["Status"].ToString();
                                rmaItem.SupplierName = row["SupplierName"].ToString();
                                if (row["SupplierRMANumber"] != DBNull.Value)
                                    rmaItem.SupplierRMANumber = row["SupplierRMANumber"].ToString();
                                if (row["TrackingNumber"] != DBNull.Value)
                                    rmaItem.TrackingNumber = row["TrackingNumber"].ToString();
                                rmaItem.ViewEdit = "<img src='../Images/Edit.png'  onclick='openWin(" + rmaItem.RMAID + ");'>";
                                //            // ApprovedOrdersList.Add(item);
                                rmaList.Add(rmaItem);
                            }
                        }
                    }
                    // }

                    //{
                    //    if (sdr.HasRows)
                    //    {

                    //        while (sdr.Read())
                    //        {




                    //            // item.ViewEdit = "<img src='../Images/Edit.png'  onclick='openWin(" + sdr["OrderID"].ToString() + "," + sdr["ContactID"] + "," + sdr["CompanyID"] + ");'>";
                    //            // ApprovedOrdersList.Add(item);

                    //        }

                    //    }

                    //}
                    // strOutput = strOutput + "]}";
                    conn.Close();
                }





            }
            var result = new
            {
                iTotalRecords = rmaList.Count(),
                iTotalDisplayRecords = 25,
                aaData = rmaList
            };
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = 5000000;
            return js.Serialize(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        public enum RMASTATUS
        {

            CREDITPENDING = 1,
            AWAITINGRMA = 2,
            AWAITINGTRACKING = 3,
            CREDITINXERO = 4,
            All = 5
        }
    }
}