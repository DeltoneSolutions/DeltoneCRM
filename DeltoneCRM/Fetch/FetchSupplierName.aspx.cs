using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

namespace DeltoneCRM.Fetch
{
    public partial class FetchSupplierName : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["sid"]))
            {
                int sid = Int32.Parse(Request.QueryString["sid"].ToString().Trim());
                Response.Write(getSupplierName(sid));
            }

        }

        //This Method Fetch SupplierName given by SupplierID
        protected String getSupplierName(int SupplierID)
        {
            String sname = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                 conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                 using (SqlCommand cmd = new SqlCommand())
                 {

                     cmd.CommandText = "select SupplierName from Suppliers where SupplierID=" + SupplierID;
                     cmd.Connection = conn;
                     conn.Open();

                     using (SqlDataReader sdr = cmd.ExecuteReader())
                     {
                         while (sdr.Read())
                         {
                             
                             sname = sdr["SupplierName"].ToString();
                         }
                     }
                     conn.Close();
                 }
            }

            return sname;
        }
        //End Method Fetch SupplierName given by SupplierID

    }
}