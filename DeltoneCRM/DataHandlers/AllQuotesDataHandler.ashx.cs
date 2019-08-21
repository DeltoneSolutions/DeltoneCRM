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
    /// Summary description for AllQuotesDataHandler
    /// </summary>
    public class AllQuotesDataHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState 
    {

        String theStatus = String.Empty;

        public void ProcessRequest(HttpContext context)
        {
            int displayLength = int.Parse(context.Request["iDisplayLength"]);
            int displayStart = int.Parse(context.Request["iDisplayStart"]);
            int sortCol = int.Parse(context.Request["iSortCol_0"]);
            string sortDir = context.Request["sSortDir_0"];
            string search = context.Request["sSearch"];
            string status = context.Request["sStatus"];
            theStatus = context.Request["sStatus"];

            String cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var usertype = string.Empty;
            if ( context.Session["USERPROFILE"] != null)
            {
                usertype = context.Session["USERPROFILE"].ToString();
            }

            List<LiveDBQuotes> liveDBQuotesItems = new List<LiveDBQuotes>();
            int filteredCount = 0;

            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("getAllQuotes", conn);
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

                SqlParameter paramStatusString = new SqlParameter()
                {
                    ParameterName = "@Status",
                    Value = status
                };
                cmd.Parameters.Add(paramStatusString);

                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                //DataTable dt = new DataTable();
                //dt.Load(sdr);

                while (sdr.Read())
                {
                    LiveDBQuotes item = new LiveDBQuotes();
                    item.QuoteID = Convert.ToInt32(sdr["QuoteID"]);
                    filteredCount = Convert.ToInt32(sdr["TotalCount"]);
                    item.QuoteDate = sdr["QuoteDateTime"].ToString();
                    item.CompanyName = sdr["CompanyName"].ToString();
                    item.CustomerType = sdr["CustomerType"].ToString();
                    item.ContactName = sdr["ContactName"].ToString();
                    item.QuoteTotal = String.Format("{0:C2}", float.Parse(sdr["Total"].ToString()));
                    item.QuotedBy = sdr["QuoteBy"].ToString();
                    item.QuoteStatus = sdr["Status"].ToString();
                    item.View = "<img src='../Images/Edit.png' onclick='Edit(" + sdr["QuoteID"] + ", " + sdr["CompanyID"] + ", " + sdr["ContactID"] + ")'/>";
                    liveDBQuotesItems.Add(item);
                }
                conn.Close();
            }

        //    liveDBQuotesItems = liveDBQuotesItems.OrderByDescending(x => x.QuoteDate).ToList();

            var result = new
            {
                iTotalRecords = getItemsTotalCount(),
                iTotalDisplayRecords = filteredCount,
                aaData = liveDBQuotesItems
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
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.Quote WHERE Status = '" + theStatus + "'", conn);
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