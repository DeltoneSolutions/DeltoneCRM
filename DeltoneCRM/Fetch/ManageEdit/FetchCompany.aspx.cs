using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM.Fetch.ManageEdit
{
    public partial class FetchCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnAllItems());
        }

        protected string ReturnAllItems()
        {
            String strOutput = "";
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CP.CompanyID, CP.CompanyName, LG.FirstName, LG.LastName, CP.CompanyWebsite, CP.Active FROM dbo.Companies CP, dbo.Logins LG WHERE CP.OwnershipAdminID  = LG.LoginID AND CP.CompanyID = " + Request.QueryString["CompanyID"];
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strOutput = strOutput +  sdr["CompanyID"] + "|"+ sdr["CompanyName"] + "|" + sdr["FirstName"] + "|" + sdr["LastName"] + "|" + sdr["CompanyWebsite"] + "|" + sdr["Active"];

                        }

                    }

                }

            }
            return strOutput;
        }
    }
}