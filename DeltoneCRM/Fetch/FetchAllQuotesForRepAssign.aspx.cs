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
    public partial class FetchAllQuotesForRepAssign : System.Web.UI.Page
    {
        string reID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

            reID = Request["reId"];

            Response.Write(ReturnQuotes());
        }

        protected string ReturnQuotes()
        {

            var listCom = new List<string>();
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                           "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                           " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) < CAST(getdate() as date) "

                          + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB'  "

                            + "   {1} ";
                    if (!string.IsNullOrEmpty(reID))
                    {
                        var createdBy = reID;
                        if (Convert.ToInt32(reID) > 0)
                        {
                             querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                           "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                           " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) < CAST(getdate() as date)"

                          + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB' and  QT.QuoteByID=@createdBy "

                            + "   {1} ";
                            cmd.Parameters.AddWithValue("@createdBy", createdBy);
                        }
                    }
                    GetTabSelectionQuery(cmd, querystring);


                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var comQuoteId = sdr["CompanyID"].ToString();
                                if (!listCom.Contains(comQuoteId))
                                {
                                    listCom.Add(comQuoteId);
                                    // String strViewQuote = "<img src='../Images/Edit.png' onclick='Edit(" + sdr["QuoteID"] + ", " + sdr["CompanyID"] + ", " + sdr["ContactID"] + ")'/>";

                                    var total = String.Format("{0:C2}", float.Parse(sdr["Total"].ToString()));
                                    var Link = "<input name='selectchk'  value='" + sdr["QuoteID"].ToString() + "' type='checkbox' />";

                                    var LinkLocked = "<input id='" + sdr["QuoteID"].ToString() + "' name='selectlock'  value='" + sdr["QuoteID"].ToString() + "' type='checkbox' />";

                                    var linked = "N";

                                    if (sdr["linkedtableComId"] != DBNull.Value)
                                    {
                                        linked = "Y";
                                    }

                                    if (linked == "Y")
                                        LinkLocked = "<input name='selectlock' checked='true' value='" + sdr["QuoteID"].ToString() + "' type='checkbox' />";

                                    //Modification Done here
                                    strOutput = strOutput + "[\"" + sdr["QuoteID"] + "\"," + "\"" + sdr["QuoteDate"].ToString() + "\","
                                        + "\"" + sdr["CompanyName"].ToString() + "\","
                                        + "\"" + sdr["ContactName"].ToString() + "\","
                                          + "\"" + total + "\","

                                        + "\"" + sdr["QuoteBy"].ToString() + "\","
                                         + "\"" + sdr["Type"].ToString() + "\","


                                        + "\"" + sdr["Status"].ToString() + "\","
                                         + "\"" + LinkLocked + "\","
                                         + "\"" + sdr["repAssignedName"].ToString() + "\","
                                        + "\"" + Link + "\"],";
                                }


                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }


                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }

            }
            return strOutput;
        }

        private void GetTabSelectionQuery(SqlCommand cmd, string querystring)
        {
            var secondArg = "  order by QuoteDate desc";

            secondArg = "  and QT.QuoteCategory=0 order by QuoteDate desc";
            var firstarg = " and QT.QuoteCategory=0 ";
            cmd.CommandText = string.Format(querystring, firstarg, secondArg);

        }
    }
}