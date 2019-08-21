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
    public partial class FetchRMADetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String RID = Request.QueryString["RID"].ToString();
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            String SqlStmt = "SELECT * FROm dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var notes = "";
                        if (sdr["Notes"] != DBNull.Value)
                            notes = sdr["Notes"].ToString();
                        output = sdr["SentToSupplier"].ToString() + "|" + sdr["ApprovedRMA"].ToString() + "|" 
                            + sdr["CreditInXero"].ToString() + "|" + sdr["RMAToCustomer"].ToString() + "|" 
                            + sdr["AdjustedNoteFromSupplier"].ToString() 
                            + "|" + sdr["SupplierRMANumber"].ToString() + "|"
                            + sdr["TrackingNumber"].ToString() + "|"
                              + sdr["InHouse"].ToString() + "|"
                             + notes + "|"
                              + sdr["Status"].ToString();
                    }
                }
                conn.Close();
            }

            Response.Write(output);
        }
    }
}