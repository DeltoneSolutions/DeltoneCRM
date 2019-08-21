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
    public partial class sel_LeadReport : System.Web.UI.Page
    {

        public static string RepName;
        public static int startdate;
        public static int enddate;
        public static string customer;

        protected void Page_Load(object sender, EventArgs e)
        {
            fillRepsList();
            fillCustomerList();
        }

        public void fillRepsList()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                String strLoggedUsers = String.Empty;
                DataTable subjects = new DataTable();

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName,LoginID from dbo.Logins WHERE LoginID NOT IN (4, 9, 8, 19,20,21,22) AND Active = 'Y'", conn);
                adapter.Fill(subjects);
                ddlRepList.DataSource = subjects;
                ddlRepList.DataTextField = "FirstName";
                ddlRepList.DataValueField = "LoginID";
                ddlRepList.DataBind();
                ddlRepList.Items.Insert(0, new ListItem("ALL", "ALL"));
            }
        }

        public void fillCustomerList()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                String strLoggedUsers = String.Empty;
                DataTable customers = new DataTable();

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select CompanyName,CompanyID from dbo.Companies ORDER BY CompanyName ASC", conn);
                adapter.Fill(customers);
                ddlCustomerList.DataSource = customers;
                ddlCustomerList.DataTextField = "CompanyName";
                ddlCustomerList.DataValueField = "CompanyID";
                ddlCustomerList.DataBind();
                ddlCustomerList.Items.Insert(0, new ListItem("ALL", "ALL"));
            }
        }

        protected void GR_Click(object sender, EventArgs e)
        {


            RepName = ddlRepList.SelectedValue.ToString();
            String dateRange = ddlRepList.SelectedValue.ToString();
            Response.Redirect("NoSalesAccounts.aspx?Repname=" + RepName + "&DateRange=" + dateRange);


        }
    }
}