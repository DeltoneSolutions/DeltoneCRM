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
    public partial class VerifyIfCommishIsSplit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String responseString = "0";
            String Owner1 = String.Empty;
            String Owner2 = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SplitWithID, OrderEnteredBy, AccountOwner FROM dbo.Orders WHERE OrderID = " + Request.QueryString["OID"].ToString();
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["SplitWithID"] != DBNull.Value)
                                responseString = sdr["SplitWithID"].ToString();
                            Owner1 = sdr["AccountOwner"].ToString();
                            Owner2 = sdr["OrderEnteredBy"].ToString();

                        }
                    }
                }
                conn.Close();
            }

            Response.Write(responseString + "|" + Owner1 + "|" + Owner2);
        }
    }
}