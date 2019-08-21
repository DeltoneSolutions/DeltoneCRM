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
    public partial class Process_AddCompanyNotes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String CompVal = Request.Form["CompanyID"].ToString();
            performCompanyNotesUpdates(CompVal);
        }

        protected int performCompanyNotesUpdates(String CompanyID)
        {

            int RowEffected = -1;

            var prevoiusNote = UpdateCompanyNotesAudit(Convert.ToInt32(CompanyID), Request.Form["CompNotes"].ToString());

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateCompNotes = "UPDATE dbo.Companies SET Notes = @CompNotes WHERE CompanyID = " + CompanyID;
            SqlCommand cmd = new SqlCommand(strUpdateCompNotes, conn);
            var comNotes = Session["LoggedUser"] + "--<b><font  color=\"red\">" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</font></b>--" + Request.Form["CompNotes"].ToString().Trim();
            var combinecomNotes = comNotes + "<br/><br/>" + prevoiusNote;
            cmd.Parameters.AddWithValue("@CompNotes", combinecomNotes);

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
            UpdateRepLatestNotes(comNotes, CompanyID);
            return RowEffected;

        }

        private void UpdateRepLatestNotes(string notes,string comId)
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

        private string UpdateCompanyNotesAudit(int companyID, string newdata)
        {
            var columnName = "Notes";
            var talbeName = "Companies";
            var ActionType = "Updated";
            int primaryKey = companyID;
            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var companyNote = new DeltoneCRM_DAL.CompanyDAL(connectionstring).getCompanyNotes(companyID.ToString());


            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionstring;

            new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(companyNote, newdata, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, companyID);

            return companyNote;
        }


    }
}