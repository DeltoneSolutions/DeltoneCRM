using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class CompanyLockList : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            Response.Redirect("Manage\\ManageCentral.aspx");
        }


        [System.Web.Services.WebMethod]
        public static void UpdateCompany(List<ComSelect> coms)
        {
            if (coms != null)
            {
               UpdateCompanyList(coms);
            }
        }

        private static void UpdateCompanyList(List<ComSelect> coms)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var sqlStr = @"UPDATE dbo.Companies SET Hold =@hold  WHERE CompanyID =@comId ";
            var comLocked = "";
            foreach (var item in coms)
            {
                var comId = Convert.ToInt32(item.companyId);
                var statu = item.selected;
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sqlStr;
                cmd.Connection = conn;
                conn.Open();
                comLocked = " Not Locked";
                var holdStatus = "N";
                if (statu)
                {
                    holdStatus = "Y";
                    comLocked = " Locked";
                }
                cmd.Parameters.AddWithValue("@hold", holdStatus);
                cmd.Parameters.AddWithValue("@comId", comId);

                cmd.ExecuteNonQuery();
                conn.Close();

                var comstatusMess =  comLocked;
                CreateAuditRecord(comId
                    , "", comstatusMess);
                    

            }
        }

        private static void CreateAuditRecord(int primaryKey, string coluhmnpreVious, string columcurrnet)
        {
            var columnName = "Status";
            var talbeName = "Companies";
            var ActionType = "Changed";
            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var newvalues = "Company  Id " + primaryKey + " : " + columcurrnet;

            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(coluhmnpreVious, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, primaryKey);
        }


        public class ComSelect
        {
            public string companyId { get; set; }
            public bool selected { get; set; }
        }
    }
}