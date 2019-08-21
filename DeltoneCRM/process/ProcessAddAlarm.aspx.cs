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
    public partial class ProcessAddAlarm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "INSERT INTO dbo.Alarms (CompanyID, Description, StartDate, CreatedDateTime, CreatedBy, Active, AlarmTriggered, UserID) VALUES (@CompanyID, @Description, @StartDate, CURRENT_TIMESTAMP, @CreatedBy, 'Y', 'N', @UserID)";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@CompanyID", Request.QueryString["CID"].ToString());
            cmd.Parameters.AddWithValue("@Description", Request.QueryString["Notes"].ToString());
            cmd.Parameters.AddWithValue("@StartDate", Request.QueryString["StartDate"].ToString());
            cmd.Parameters.AddWithValue("@CreatedBy", Session["LoggedUser"].ToString());
            cmd.Parameters.AddWithValue("@UserID", Session["LoggedUserID"].ToString());

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            Response.Write("OK");

        }

        
    }
}