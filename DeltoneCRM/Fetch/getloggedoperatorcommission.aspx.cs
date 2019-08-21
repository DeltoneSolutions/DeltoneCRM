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
    public partial class getloggedoperatorcommission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(getOperatorCommish(Request.QueryString["repid"]));
        }

        public String getOperatorCommish(String repID)
        {
            String commission = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select commission from dbo.Logins where LoginID = " + Session["LoggedUserID"];
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            commission = sdr["commission"].ToString();
                        }
                    }
                    conn.Close();
                }
            }

            return commission;
        }
    }
}