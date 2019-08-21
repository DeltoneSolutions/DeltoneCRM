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
    public partial class VerifyIfCompanyExists : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            String responseString = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CompanyID FROM dbo.Companies WHERE CompanyName = '" + Request.QueryString["CompanyName"].ToString() + "'";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            responseString = responseString + "RED|";
                            while (sdr.Read())
                            {
                                responseString = responseString + sdr["CompanyID"].ToString() + "|";
                            }
                        }
                        else
                        {
                            responseString = responseString + "GREEN|0|";
                        }
                    }
                }
                conn.Close();

                using (SqlCommand cmd2 = new SqlCommand())
                {
                    cmd2.CommandText = "SELECT CompanyID FROM dbo.Quote_Companies WHERE CompanyName = '" + Request.QueryString["CompanyName"].ToString() + "'";
                    cmd2.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd2.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            responseString = responseString + "RED|";
                            while (sdr.Read())
                            {
                                responseString = responseString + sdr["CompanyID"].ToString();
                            }
                        }
                        else
                        {
                            responseString = responseString + "GREEN|0";
                        }
                    }
                }
                conn.Close();

                Response.Write(responseString);
            }
        }
    }
}