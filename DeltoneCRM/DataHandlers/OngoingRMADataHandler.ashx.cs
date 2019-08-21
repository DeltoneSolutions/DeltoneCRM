using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using DeltoneCRM.Classes;

namespace DeltoneCRM.DataHandlers
{
    /// <summary>
    /// Summary description for OngoingRMADataHandler
    /// </summary>
    public class OngoingRMADataHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int displayLength = int.Parse(context.Request["iDisplayLength"]);
            int displayStart = int.Parse(context.Request["iDisplayStart"]);
            int sortCol = int.Parse(context.Request["iSortCol_0"]);
            string sortDir = context.Request["sSortDir_0"];
            string search = context.Request["sSearch"];

            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            List<OngoingRMA> ongoingRMAItems = new List<OngoingRMA>();
            int filteredCount = 0;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("getAllActiveRMAs", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramDisplayLength = new SqlParameter()
                {
                    ParameterName = "@DisplayLength",
                    Value = displayLength
                };
                cmd.Parameters.Add(paramDisplayLength);

                SqlParameter paramDisplayStart = new SqlParameter()
                {
                    ParameterName = "@DisplayStart",
                    Value = displayStart
                };
                cmd.Parameters.Add(paramDisplayStart);

                SqlParameter paramSortCol = new SqlParameter()
                {
                    ParameterName = "@SortCol",
                    Value = sortCol
                };
                cmd.Parameters.Add(paramSortCol);

                SqlParameter paramSortDir = new SqlParameter()
                {
                    ParameterName = "@SortDir",
                    Value = sortDir
                };
                cmd.Parameters.Add(paramSortDir);

                SqlParameter paramSearchString = new SqlParameter()
                {
                    ParameterName = "@Search",
                    Value = search
                };
                cmd.Parameters.Add(paramSearchString);

                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    OngoingRMA item = new OngoingRMA();
                    item.CreditNoteID = Convert.ToInt32(sdr["CreditNoteID"]);
                    item.XeroCreditNote = sdr["XeroCreditNoteID"].ToString();
                    filteredCount = Convert.ToInt32(sdr["TotalCount"]);
                    item.SupplierName = sdr["SupplierName"].ToString();
                    String hasItBeenRaised = sdr["Raised"].ToString();
                    if (hasItBeenRaised == "1")
                    {
                        item.Raised = "<img src='../Images/success.png' style='width:15px;'/>";
                    }
                    else
                    {
                        item.Raised = "";
                    }
                    
                    item.RaisedDateTime = sdr["RaisedDateTime"].ToString().Substring(0,10);

                    String hasItBeenSentToSupplier = sdr["SentToSupplier"].ToString();
                    if (hasItBeenSentToSupplier == "1")
                    {
                        item.SentToSupplier = "<img src='../Images/success.png' style='width:15px;'/>";
                    }
                    else
                    {
                        item.SentToSupplier = "";
                    }

                    if (sdr["SentToSupplierDateTime"].ToString() != "")
                    {
                        item.SentToSupplierDateTime = sdr["SentToSupplierDateTime"].ToString().Substring(0, 10);
                    }
                    

                    String hasItBeenApproved = sdr["ApprovedRMA"].ToString();
                    if (hasItBeenApproved == "1")
                    {
                        item.ApprovedRMA = "<img src='../Images/success.png' style='width:15px;'/>";
                    }
                    else
                    {
                        item.ApprovedRMA = "";
                    }

                    if (sdr["ApprovedRMADateTime"].ToString() != "")
                    {
                        item.ApprovedRMADateTime = sdr["ApprovedRMADateTime"].ToString().Substring(0, 10);
                    }
                    

                    String hasItBeenCreditedInXero = sdr["CreditInXero"].ToString();
                    if (hasItBeenCreditedInXero == "1")
                    {
                        item.CreditInXero = "<img src='../Images/success.png' style='width:15px;'/>";
                    }
                    else
                    {
                        item.CreditInXero = "";
                    }

                    String hasItBeenInHouse = sdr["InHouse"].ToString();
                    if (hasItBeenInHouse == "1")
                    {
                        item.InHouse = "<img src='../Images/success.png' style='width:15px;'/>";
                    }
                    else
                    {
                        item.InHouse = "";
                    }

                    if (sdr["CreditInXeroDateTime"].ToString() != "")
                    {
                        item.CreditInXeroDateTime = sdr["CreditInXeroDateTime"].ToString().Substring(0, 10);
                    }
                    

                    String hasItBeenSentToCustomer = sdr["RMAToCustomer"].ToString();
                    if (hasItBeenSentToCustomer == "1")
                    {
                        item.RMAToCustomer = "<img src='../Images/success.png' style='width:15px;'/>";
                    }
                    else
                    {
                        item.RMAToCustomer = "";
                    }

                    if (sdr["RMAToCustomerDateTime"].ToString() != "")
                    {
                        item.RMAToCustomerDateTime = sdr["RMAToCustomerDateTime"].ToString().Substring(0, 10);
                    }
                    

                    String hasItBeenAdjusted = sdr["AdjustedNoteFromSupplier"].ToString();
                    if (hasItBeenAdjusted == "1")
                    {
                        item.AdjustedNoteFromSupplier = "<img src='../Images/success.png' style='width:15px;'/>";
                    }
                    else
                    {
                        item.AdjustedNoteFromSupplier = "";
                    }

                    if (sdr["AdjustedNoteFromSupplierDateTime"].ToString() != "")
                    {
                        item.AdjustedNoteFromSupplierDateTime = sdr["AdjustedNoteFromSupplierDateTime"].ToString().Substring(0, 10);
                    }
                    
                    item.SupplierRMANumber = sdr["SupplierRMANumber"].ToString();
                    item.TrackingNumber = sdr["TrackingNumber"].ToString();
                    item.Status = sdr["Status"].ToString();
                    item.ViewEdit = "<img src='../Images/Edit.png' onClick='Edit(" + sdr["RMAID"] + ")'/>";
                    ongoingRMAItems.Add(item);
                }
                conn.Close();
            }

            var result = new
            {
                iTotalRecords = getItemsTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = ongoingRMAItems
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(result));
            
        }

        private int getItemsTotalCount()
        {
            int TotalitemCount = 0;
            String cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.RMATracking WHERE Status NOT IN ('COMPLETED')", conn);
                conn.Open();

                TotalitemCount = (int)cmd.ExecuteScalar();
                conn.Close();
            }

            return TotalitemCount;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}