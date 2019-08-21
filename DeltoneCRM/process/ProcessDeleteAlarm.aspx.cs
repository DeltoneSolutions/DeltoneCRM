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
    public partial class ProcessDeleteAlarm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(DeleteAlarm(Request.QueryString["AID"]));
        }

        protected int DeleteAlarm(String AID)
        {
            int RowEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "DELETE FROM dbo.Alarms WHERE AlarmID = @AlarmID";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@AlarmID", AID);

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