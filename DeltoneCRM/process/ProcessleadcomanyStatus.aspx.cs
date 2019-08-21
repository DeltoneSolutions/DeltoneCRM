using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.process
{
    public partial class ProcessleadcomanyStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var status = Request.QueryString["stat"].ToString();
            var comId = Request.QueryString["CompID"].ToString();

            SaveCompanyStatus(comId,status);
        }

        private void SaveCompanyStatus(string comID,string stat)
        {
            int RowEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateCompNotes = @"UPDATE dbo.LeadCompany SET CalledStatus = @calledstatus
                                             WHERE CompanyID = " + comID;
            SqlCommand cmd = new SqlCommand(strUpdateCompNotes, conn);


            cmd.Parameters.AddWithValue("@calledstatus", stat);

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

        }
    }
}