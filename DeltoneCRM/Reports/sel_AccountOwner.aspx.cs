using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Data;

namespace DeltoneCRM.Reports
{
    public partial class sel_AccountOwner : System.Web.UI.Page
    {

        public static string RepName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillRepsList();
            }
        }

        public void fillRepsList()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                String strLoggedUsers = String.Empty;
                DataTable subjects = new DataTable();

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName + ' ' + LastName AS FullName, LoginID from dbo.Logins WHERE LoginID NOT IN (4, 9, 8, 19,20,21,22) AND Active = 'Y' ORDER BY FullName ASC", conn);
                adapter.Fill(subjects);
                ddlRepList.DataSource = subjects;
                ddlRepList.DataTextField = "FullName";
                ddlRepList.DataValueField = "LoginID";
                ddlRepList.DataBind();
            }
        }

        protected void GR_Click(object sender, EventArgs e)
        {
            RepName = ddlRepList.SelectedValue.ToString();
            Response.Redirect("AccountOwner.aspx?LoginID=" + RepName);
        }
    }
}