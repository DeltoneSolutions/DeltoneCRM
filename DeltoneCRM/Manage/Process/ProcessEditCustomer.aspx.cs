using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using DeltoneCRM_DAL;


namespace DeltoneCRM.Manage.Process
{
    public partial class ProcessEditCustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            CompanyDAL deltone_Company = new CompanyDAL(strConnString);
            int CompanyID = Int32.Parse(Request.Form["CompanyID"].ToString());
            String strCompanyName = Request.Form["CompanyName"].ToString();
            String WebSite = Request.Form["WebSite"].ToString();
            String  CompnayOwner =Request.Form["AccountOwner"].ToString();
            String OwnershipPeriod = Request.Form["OwnershipPeriod"].ToString();
            string strScript = "<script language='javascript'>$(document).ready(function (){ alert('" + CompnayOwner + "' });</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false); 
            var loggedinUserId=Convert.ToInt32 (Session["LoggedUserID"].ToString());
            Response.Write(deltone_Company.EditCompnay(CompanyID, strCompanyName, WebSite, CompnayOwner, OwnershipPeriod, loggedinUserId));
        }

        private void SubmitChanges()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "UPDATE dbo.Companies SET CompanyName = @CompanyName, CompanyWebsite = @CompanyWebsite, OwnershipAdminID = @OwnershipAdminID, Active = @Active";
                }
            }
         }
    }
}