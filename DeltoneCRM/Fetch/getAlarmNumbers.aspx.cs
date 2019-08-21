using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;


namespace DeltoneCRM.Fetch
{
    public partial class getAlarmNumbers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(CountAlarm());
        }

        protected String CountAlarm()
        {
            String TotalCount = String.Empty;
            if (Session["LoggedUserID"] == null)
                return TotalCount;
            using (SqlConnection conn = new SqlConnection())
            { 
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT COUNT(*) AS TOTAL FROM dbo.Alarms WHERE UserID = " + Session["LoggedUserID"].ToString() + "AND AlarmTriggered = 'N' AND StartDate <= getDate()";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            TotalCount = sdr["TOTAL"].ToString();
                        }

                    }

                }
                conn.Close();
            }
            return TotalCount;
        }
    }
}