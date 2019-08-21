using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class AllocateCompanyRepTestTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                PopulateDropDownList();
        }

        //Company OwnerShip details dropdown List Population
        protected void PopulateDropDownList()
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins WHERE Active = 'Y' and LoginID NOT IN ( 25, 19,18,23,26,8,21,22)", conn);
                adapter.Fill(subjects);
                RepNameDropDownList.DataSource = subjects;
                RepNameDropDownList.DataTextField = "FullName";
                RepNameDropDownList.DataValueField = "LoginID";
                RepNameDropDownList.DataBind();
                NumberAccountDropDownList.DataSource = subjects;
                NumberAccountDropDownList.DataTextField = "FullName";
                NumberAccountDropDownList.DataValueField = "LoginID";
                NumberAccountDropDownList.DataBind();
            }

            //Get the Current Account OwnerShip


        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            Response.Redirect("Manage\\ManageCentral.aspx");
        }
    }
}