using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Data;


namespace DeltoneCRM.Fetch
{
    public partial class getpersonalaccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnPersonalAccounts());
        }

        protected String ReturnPersonalAccounts()
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName, CP.CompanyID FROM dbo.Companies CP, dbo.Contacts CT WHERE CP.CompanyID = CT.CompanyID AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY CP.CompanyName ASC";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                //EditOrder(OrderID, CompanyID, ContactID)
                                //String strEditOrder = "<img src='Images/Edit.png'  onclick='EditOrder(" + sdr["OrderID"] + "," + CompanyID + "," + ContactID + ")'/>";
                                String strViewCompany = "<img src='Images/Edit.png'  onclick='OpenCompanyNewWindow(" + sdr["CompanyID"].ToString() + ")'/>";

                                strOutput = strOutput + "[\"" + sdr["CompanyName"] + "\"," + "\"" + sdr["FullName"] + "\"," + "\"" + strViewCompany + "\"],";

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
    }
}