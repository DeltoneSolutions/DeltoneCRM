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
    public partial class getOrderState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnOrderState(Request.QueryString["ContactID"].ToString()));
        }

        protected String ReturnOrderState(String CID)
        {
            String strOutput = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT STREET_Region FROM dbo.Contacts WHERE ContactID = " + CID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strOutput = sdr["STREET_Region"].ToString();

                            }

                        }

                    }
                    conn.Close();
                }

            }
            return strOutput;
        }
    }
}