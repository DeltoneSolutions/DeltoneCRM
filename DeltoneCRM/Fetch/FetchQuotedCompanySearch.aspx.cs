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
    public partial class FetchQuotedCompanySearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

            String searchTerm = Request.QueryString["term"].ToString();

            Response.Write(ReturnQuotes(searchTerm));
        }

        protected string ReturnQuotes(string searhString)
        {
            String strOutput = "[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        var querystring = @"SELECT DISTINCT CO.CompanyID ,QT.QuoteID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                        " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                        "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  and  ( co.CompanyName LIKE '%" + searhString + "%' OR  QT.QuoteID LIKE '%" + searhString + "%') ";

                        cmd.CommandText = querystring;




                    }
                    else
                    {
                        var createdBy = Session["LoggedUser"].ToString();
                        var querystring = @"SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                       "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                       " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and  QT.QuoteBy=@createdBy and ( co.CompanyName LIKE '%" + searhString + "%' OR  QT.QuoteID LIKE '%" + searhString + "%')  ";
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);
                        cmd.CommandText = querystring;

                    }
                    
                    cmd.Connection = conn;
                    conn.Open();
                    var listId=new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (!listId.Contains(sdr["CompanyID"].ToString()))
                                {
                                    listId.Add(sdr["CompanyID"].ToString());

                                    strOutput = strOutput + "{\"id\": \"" + sdr["CompanyID"] + "\", \"value\": \"" + sdr["CompanyName"] + "\"},";
                                }

                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }


                    }
                    strOutput = strOutput + "]";
                    conn.Close();
                }

            }
            return strOutput;
        }

      
    }
}