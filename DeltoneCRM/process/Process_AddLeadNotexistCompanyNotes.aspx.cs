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
    public partial class Process_AddLeadNotexistCompanyNotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String CompVal = Request.Form["CompanyID"].ToString();
            performCompanyNotesUpdates(CompVal);
        }

        public String getCompanyNotes(String CID)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Notes FROM dbo.LeadCompanyNotExists WHERE CompanyID = " + CID;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        companyNote = sdr["Notes"].ToString();
                    }
                }

                conn.Close();
            }

            return companyNote;
        }

        protected int performCompanyNotesUpdates(String CompanyID)
        {

            int RowEffected = -1;

            // UpdateCompanyNotesAudit(Convert.ToInt32(CompanyID), Request.Form["CompNotes"].ToString());

            var previousNotes = getCompanyNotes(CompanyID);

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateCompNotes = "UPDATE dbo.LeadCompanyNotExists SET Notes = @CompNotes, NotesCreatedDate=CURRENT_TIMESTAMP WHERE CompanyID = " + CompanyID;
            SqlCommand cmd = new SqlCommand(strUpdateCompNotes, conn);

            var notes = Session["LoggedUser"] + "--<b><font  color=\"red\">" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</font></b>--" + Request.Form["CompNotes"].ToString();
            //var pos = notes.IndexOf('-');

            var comnotes = notes + "<br/><br/>" + previousNotes;
            cmd.Parameters.AddWithValue("@CompNotes", comnotes);

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
            UpdateRepLatestNotes(notes, CompanyID);
            return RowEffected;

        }

        private void UpdateRepLatestNotes(string notes, string comId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateCompNotes = "UPDATE dbo.CompaniesNotExists SET RepNotes = @CompNotes WHERE CompanyID = " + comId;
            SqlCommand cmd = new SqlCommand(strUpdateCompNotes, conn);
            var comNotes = notes;

            cmd.Parameters.AddWithValue("@CompNotes", comNotes);

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
        }
    }
}