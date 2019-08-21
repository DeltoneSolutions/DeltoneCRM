using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.process
{
    public partial class ProcessDeleteLinkedCompanies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(DeleteLink(Request.QueryString["contactid"]));
        }

        private  void DeleteLinkedCompanies(int comId)
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

        private void DeleteLinkedCompaniesBy(int comId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            var strSqlContactStmt = @"DELETE FROM CompanyLinked WHERE CompanyIdLinked = @companyId  ";
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

        protected int DeleteLink(String AID)
        {
            int RowEffected = -1;

            DeleteLinkedCompanies(Convert.ToInt32(AID));
            DeleteLinkedCompaniesBy(Convert.ToInt32(AID));

            return RowEffected;
        }
    }
}