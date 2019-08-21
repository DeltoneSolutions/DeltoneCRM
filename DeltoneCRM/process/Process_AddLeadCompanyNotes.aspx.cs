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
    public partial class Process_AddLeadCompanyNotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String CompVal = Request.Form["CompanyID"].ToString();
            performCompanyNotesUpdates(CompVal);
        }

        protected int performCompanyNotesUpdates(String CompanyID)
        {

            int RowEffected = -1;

           // UpdateCompanyNotesAudit(Convert.ToInt32(CompanyID), Request.Form["CompNotes"].ToString());

            var previousNotes = getCompanyNotes(CompanyID);

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateCompNotes = "UPDATE dbo.LeadCompany SET Notes = @CompNotes, NotesCreatedDate=CURRENT_TIMESTAMP WHERE CompanyID = " + CompanyID;
            SqlCommand cmd = new SqlCommand(strUpdateCompNotes, conn);

            var notes = Session["LoggedUser"] + "--<b><font  color=\"red\">" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</font></b>--"  + Request.Form["CompNotes"].ToString();
            //var pos = notes.IndexOf('-');

           var  comnotes = notes + "<br/><br/>" + previousNotes;
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
            String strUpdateCompNotes = "UPDATE dbo.Companies SET RepNotes = @CompNotes WHERE CompanyID = " + comId;
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

        private void UpdateCompanyNotesAudit(int companyID, string newdata)
        {
            var columnName = "Notes";
            var talbeName = "LeadCompany";
            var ActionType = "Updated";
            int primaryKey = companyID;
            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var companyNote = new DeltoneCRM_DAL.CompanyDAL(connectionstring).getCompanyNotes(companyID.ToString());


            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionstring;

            new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(companyNote, newdata, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, companyID);
        }

        public String getCompanyNotes(String CID)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Notes FROM dbo.LeadCompany WHERE CompanyID = " + CID;
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
    }
}