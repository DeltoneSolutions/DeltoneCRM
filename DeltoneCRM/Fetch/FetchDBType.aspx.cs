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
    public partial class FetchDBType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(getDBName(Request.QueryString["Flag"]));
        }

        private String getDBName(String QuoteID)
        {
            String FlagType = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select Flag from dbo.Quote where QuoteID = " + QuoteID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                            FlagType = sdr["Flag"].ToString();
                        }
                    }
                    conn.Close();

                }
            }

            return FlagType;
        }
    }
}