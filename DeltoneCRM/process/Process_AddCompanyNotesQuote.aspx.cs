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
    public partial class Process_AddCompanyNotesQuote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String CompVal = Request.Form["CompanyID"].ToString();
            performCompanyNotesUpdates(CompVal);
        }

        protected int performCompanyNotesUpdates(String CompanyID)
        {

            int RowEffected = -1;

            var prevoiusNote = getCompanyNotes((CompanyID));

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateCompNotes = "UPDATE dbo.Quote_Companies SET Notes = @CompNotes WHERE CompanyID = " + CompanyID;
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
          
            return RowEffected;

        }

        public String getCompanyNotes(String CID)
        {
            SqlConnection conn = new SqlConnection();
            String companyNote = String.Empty;

            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = "SELECT Notes FROM dbo.Quote_Companies WHERE CompanyID = " + CID;
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