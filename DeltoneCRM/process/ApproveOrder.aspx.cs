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
    public partial class ApproveOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(Request.QueryString["Order"]))
            {
                int OrderID = Int32.Parse(Request.QueryString["Order"].ToString());
                Response.Write(OrderApprove(OrderID));
            }

        }


        //This Method Approve the Order Status 
        protected int OrderApprove(int OrderID)
        {
            int RowEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "Update dbo.Orders set Status='APPROVED' where OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);

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