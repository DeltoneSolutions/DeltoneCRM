using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM.process
{
    public partial class processPrinterListChanges : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SavePrinterListChanges(Request.QueryString["CompID"].ToString(), Request.QueryString["PrinterValues"].ToString());
        }

        //This Method Approve the Order Status 
        protected int SavePrinterListChanges(String CompID, String PrinterList)
        {
            int RowEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "Update dbo.Companies set PrinterList = @PrinterList where CompanyID=@CompID";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@CompID", CompID);
            cmd.Parameters.AddWithValue("@PrinterList", PrinterList);

            try
            {
                conn.Open();
                RowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            return RowEffected;

        }
    }
}