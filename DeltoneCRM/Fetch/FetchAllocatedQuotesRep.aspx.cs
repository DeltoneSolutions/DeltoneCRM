using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchAllocatedQuotesRep : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            var listQuotes = GetQuoteRep();
            var str = ReturnRepAllocatedQuotes(listQuotes);
            Response.Write(str);
        }

        private List<QuoteAssign> GetQuoteRep()
        {
            var listObj = new List<QuoteAssign>();
            var query = @"SELECT QT.QuoteID,QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,CT.ContactID,'Existing Customer' as Type, 'LiveDB' as DbType ," +
                       " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy, LOS.FirstName + ' ' + LOS.LastName as quoteAssigned,QTA.CreatedDate as quoteAllocatedDate,QTA.ExpiryDate ,DATEDIFF(DAY,CAST(getdate() as date), convert(varchar(10),QTA.ExpiryDate, 120)) as daysRemain,QTA.Id as QuoteAlloID FROM dbo.Quote QT INNER JOIN " +
                       "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID " +
                         " INNER JOIN dbo.QuoteAllocate QTA on  QT.QuoteID=QTA.QuoteId INNER JOIN LOGINS LOS ON QTA.UserId =LOS.LoginID  " +
                   " where QT.Flag='LiveDB' and QTA.UserId=@userID and convert(varchar(10),QTA.ExpiryDate, 120) >= CAST(getdate() as date) " +
                        " Union SELECT QT.QuoteID, QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID , CT.ContactID,'New Customer' as Type, 'QuoteDB' as DbType ," +
                       " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy, LOS.FirstName + ' ' + LOS.LastName as quoteAssigned,QTA.CreatedDate as quoteAllocatedDate,QTA.ExpiryDate,DATEDIFF(DAY,CAST(getdate() as date), convert(varchar(10),QTA.ExpiryDate, 120)) as daysRemain,QTA.Id as QuoteAlloID FROM dbo.Quote QT INNER JOIN " +
                       "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID " +
                       " INNER JOIN dbo.QuoteAllocate QTA on  QT.QuoteID=QTA.QuoteId INNER JOIN LOGINS LOS ON QTA.UserId =LOS.LoginID " +
                      " where QT.Flag='QuoteDB' and QTA.UserId=@userID and  convert(varchar(10),QTA.ExpiryDate, 120) >= CAST(getdate() as date) ";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var conRep = Convert.ToInt32(Session["LoggedUserID"].ToString());

                    cmd.Parameters.AddWithValue("@userID", conRep);


                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    ReadExecuteReader(listObj, cmd);
                    conn.Close();

                }

            }



            return listObj;
        }

        private static void ReadExecuteReader(List<QuoteAssign> comList, SqlCommand cmd)
        {
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        var sbj = new QuoteAssign();
                        sbj.QuoteId = Convert.ToInt32(sdr["QuoteID"].ToString());
                      //  sbj.QuotedById = Convert.ToInt32(sdr["QuoteByID"].ToString());
                        sbj.QuotedDate = Convert.ToDateTime(sdr["QuoteDate"].ToString()).ToShortDateString();
                        sbj.QuotedByName = sdr["QuoteBy"].ToString();
                        sbj.CompanyID = sdr["CompanyID"].ToString();
                        sbj.QuoteAllocateId = sdr["QuoteAlloID"].ToString();
                        sbj.ContactID = sdr["ContactID"].ToString();
                        sbj.QuoteTotal = sdr["Total"].ToString();
                        sbj.CompanyName = sdr["CompanyName"].ToString();
                        sbj.QuotedAssignedName = sdr["quoteAssigned"].ToString();
                        sbj.ContactName = sdr["ContactName"].ToString();
                        sbj.DbType = sdr["DbType"].ToString();
                        sbj.Status = sdr["Status"].ToString();
                        sbj.CustomerType = sdr["Type"].ToString();
                        sbj.QuoteAllocatedDate = Convert.ToDateTime(sdr["quoteAllocatedDate"].ToString()).ToShortDateString();
                        sbj.QuoteExpiryDate = sdr["ExpiryDate"].ToString();
                        sbj.RemainingDays = sdr["daysRemain"].ToString();
                        comList.Add(sbj);

                    }
                }
            }

        }


        private string ReturnRepAllocatedQuotes(List<QuoteAssign> listObj)
        {



            string strOutput = "{\"aaData\":[";
            foreach (var item in listObj)
            {
                item.DbType = string.Format("\\\"{0}\\\"", item.DbType); 
                var strViewQuote = "<img src='../Images/Edit.png' onclick='Edit(" + item.QuoteId + ", " +
                    item.CompanyID + ", " + item.ContactID + "," + item.DbType+ ")'/>";


                strOutput = strOutput + "[\"" + item.QuoteAllocateId + "\"," 
                                          + "\"" + item.QuotedDate + "\","
                                           + "\"" + item.QuoteAllocatedDate + "\","
                                            + "\"" + item.RemainingDays + "\","
                                        + "\"" + item.CompanyName + "\","
                                        + "\"" + item.ContactName + "\","
                                          + "\"" + item.QuoteTotal + "\","
                                            + "\"" + item.QuotedByName + "\","
                                              + "\"" + item.CustomerType + "\","
                                              + "\"" + item.Status + "\","
                                        + "\"" + strViewQuote + "\"],";
            }

            if (listObj.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }

            strOutput = strOutput + "]}";

            return strOutput;

        }

        protected class QuoteAssign
        {
            public int QuoteId { get; set; }
            public int QuotedById { get; set; }
            public string QuotedDate { get; set; }
            public string CompanyID { get; set; }
            public string QuoteAllocateId { get; set; }
            public string ContactID { get; set; }
            public string CompanyName { get; set; }
            public string ContactName { get; set; }
            public string QuoteTotal { get; set; }
            public string QuotedByName { get; set; }
            public string QuotedAssignedName { get; set; }
            public string QuotedAssignedId { get; set; }
            public string CustomerType { get; set; }
            public string DbType { get; set; }
            public string Status { get; set; }
            public string QuoteAllocatedDate { get; set; }
            public string QuoteExpiryDate { get; set; }
            public string RemainingDays { get; set; }
        }
    }
}