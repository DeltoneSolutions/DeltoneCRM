using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DeltoneCRM
{
    public partial class CompanyLinkList : System.Web.UI.Page
    {
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
        public static void UpdateCompany(List<LinkComSelect> coms)
        {
            if (coms != null)
            {
                UpdateCompanyList(coms);
            }
        }

        private static void UpdateCompanyList(List<LinkComSelect> coms)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var sqlStr = @"INSERT INTO CompanyLinked(CompanyId,CompanyIdLinked,CreatedUserId,CreatedDate)
                          Values(@CompanyMainId,@companyIdLinked,@createdUserId,CURRENT_TIMESTAMP)  ";
            var comLocked = "";
            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            foreach (var item in coms)
            {
                var statu = item.selected;

                DeleteLinkedCompanies(item.companyId);
                var listLinkedCompanies = (from lico in coms where lico.companyId != item.companyId select lico).ToList();

                foreach (var liEle in listLinkedCompanies)
                {
                    if (liEle.selected)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = sqlStr;
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.Parameters.AddWithValue("@CompanyMainId", item.companyId);
                        cmd.Parameters.AddWithValue("@companyIdLinked", liEle.companyId);
                        cmd.Parameters.AddWithValue("@createdUserId", loggedInUserId);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        comLocked = " Linked company Id : " + liEle.companyId;
                        var comstatusMess = comLocked;
                        CreateAuditRecord(item.companyId
                            , "", comstatusMess);
                    }
                }
            }
        }

        private static void DeleteLinkedCompanies(int comId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            var strSqlContactStmt = @"DELETE FROM CompanyLinked WHERE CompanyId = @companyId  ";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@companyId", SqlDbType.Int).Value = comId;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        private static void CreateAuditRecord(int primaryKey, string coluhmnpreVious, string columcurrnet)
        {
            var columnName = "CompanyIdLinked";
            var talbeName = "CompanyLinked";
            var ActionType = "Created";
            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var newvalues = " Company  Id " + primaryKey + " : " + columcurrnet;

            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(coluhmnpreVious, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, primaryKey);
        }

        public class LinkComSelect
        {
            public int companyId { get; set; }
            public bool selected { get; set; }
        }
    }
}