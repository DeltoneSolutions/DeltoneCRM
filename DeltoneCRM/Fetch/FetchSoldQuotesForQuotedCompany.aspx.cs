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
    public partial class FetchSoldQuotesForQuotedCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strCompanyID = Request.QueryString["cid"].ToString();
            Response.Write(ReturnQuotes(strCompanyID));
        }

        protected string ReturnQuotes(String strCompanyID)
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var sold = 3;
                    cmd.CommandText = @"SELECT QT.QuoteID, QT.CompanyID, QT.ContactID,QT.QuoteBy, QT.QuoteDateTime, CT.FirstName + ' ' + CT.LastName AS FullName, 
                            QT.Total, QT.Status, QT.Flag FROM dbo.Quote QT INNER JOIN dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID WHERE QT.CompanyID = " + strCompanyID + " and QuoteCategory= " + sold;

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {


                                var falgChec = 2;

                                falgChec = 1; // sold quotes

                                String strViewQuote = "<img src='Images/Edit.png' onclick='ViewQuote(" + sdr["QuoteID"] + ", "
                                   + sdr["ContactID"] + ", " + sdr["CompanyID"] + ","
                                   + falgChec + ")'/>";


                                //Modification Done here
                                strOutput = strOutput + "[\"" + sdr["QuoteID"] + "\"," + "\"" + sdr["QuoteDateTime"].ToString()
                                    + "\"," + "\"" + sdr["FullName"].ToString() + "\"," + "\"" + sdr["Total"].ToString() + "\","
                                    + "\"" + sdr["Status"].ToString() + "\","
                                     + "\"" + sdr["QuoteBy"].ToString() + "\","
                                    + "\"" + strViewQuote + "\"],";





                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }
                        strOutput = strOutput + "]}";
                        conn.Close();

                    }

                }

            }
            return strOutput;
        }
    }
}