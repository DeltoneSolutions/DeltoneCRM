using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM.Fetch
{
    public partial class FetchAllContacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String searchTerm = Request.QueryString["term"].ToString();
            //String searchTerm = "In";
            Response.Write(ReturnAllCompanies(searchTerm));
        }

        protected string ReturnAllCompanies(String strSearchTerm)
        {
            String strOutput = "[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        cmd.CommandText = "SELECT CT.*, CP.CompanyName FROM dbo.Contacts CT LEFT JOIN dbo.Companies CP ON CT.CompanyID = CP.CompanyID WHERE CT.FirstName + ' ' + CT.LastName LIKE '%" + strSearchTerm + "%' ORDER BY CT.ContactID";
                    }
                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                    {
                        cmd.CommandText = "SELECT CT.*, CP.CompanyName FROM dbo.Contacts CT LEFT JOIN dbo.Companies CP ON CT.CompanyID = CP.CompanyID WHERE CT.FirstName + ' ' + CT.LastName LIKE '%" + strSearchTerm + "%'  AND CP.OwnershipAdminID = " + Session["LoggedUserID"] + " ORDER BY CT.ContactID";
                    }

                    cmd.Connection = conn;
                    conn.Open();


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            String CombinedName = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString() + " - " + sdr["CompanyName"].ToString().ToUpper();
                            strOutput = strOutput + "{\"id\": \"" + sdr["CompanyID"] + "\", \"value\": \"" + CombinedName + "\"},";
                        }
                    }

                    int Length = strOutput.Length;
                    strOutput = strOutput.Substring(0, (Length - 1));
                }
                strOutput = strOutput + "]";
                conn.Close();
            }

            return strOutput;
        }
    }
}