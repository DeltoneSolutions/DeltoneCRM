using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchRMABy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String RID = Request.QueryString["RID"].ToString();
            string suppName = Request.QueryString["sName"].ToString();
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            String SqlStmt = "SELECT * FROm dbo.RMATracking WHERE CreditNoteID =@crId AND SupplierName=@suppName";
           
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Parameters.Add("@suppName", SqlDbType.NVarChar).Value = suppName;
                cmd.Parameters.Add("@crId", SqlDbType.Int).Value = RID;
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
                             + sdr["InHouse"].ToString()+ "|"
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