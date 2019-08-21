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
    public partial class FetchAllCompanies : System.Web.UI.Page
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
                        cmd.CommandText = @"SELECT * FROM dbo.Companies co  WHERE 
                                          co.CompanyName LIKE '%" + strSearchTerm + "%' "
                                          
                                           + " AND co.Active ='Y' ";
                    }
                    else if (Session["USERPROFILE"].ToString() == "STANDARD")
                    {
                        cmd.CommandText = @"SELECT * FROM dbo.Companies co   WHERE 
                                           co.CompanyName LIKE '%" + strSearchTerm + "%' "
                                           
                                         + " AND co.Active ='Y' AND co.OwnershipAdminID IN (" + Session["LoggedUserID"] + ")";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strOutput = strOutput + "{\"id\": \"" + sdr["CompanyID"] + "\", \"value\": \"" + sdr["CompanyName"] + "\"},";
                        }
                    }

                    int Length = strOutput.Length;
                    strOutput = strOutput.Substring(0, (Length-1));
                    conn.Close();
                }
                strOutput = strOutput + "]";
            }

            return strOutput;
        }

       
    }
}