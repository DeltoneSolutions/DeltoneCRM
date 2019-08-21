using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM.Manage
{
    public partial class addcustomer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            Populate();

            /*
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String AddStr = "SELECT FirstName, LastName, LoginID FROM dbo.Logins";
            SqlCommand ADDcmd = new SqlCommand(AddStr, conn);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(ADDcmd);
            table.Columns.Add("FullName", typeof(string), "FirstName + ' ' + LastName");

            adapter.Fill(table);


            DropDownList1.DataSource = table;
            DropDownList1.DataValueField = "LoginID";
            DropDownList1.DataTextField = "FullName";
            DropDownList1.DataBind();
            */

            if (Session["USERPROFILE"].ToString() == "ADMIN")
            {
                canshowadmin1.Style.Add("display","block");
                canshowadmin2.Style.Add("display", "block");
            }
        }


        protected void Populate()
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT FirstName + ' ' +  LastName as FullName,LoginID FROM dbo.Logins WHERE LoginID NOT IN (4, 9, 8, 19,21,22) AND Active = 'Y'", conn);
                adapter.Fill(subjects);
                DropDownList1.DataSource = subjects;
                DropDownList1.DataValueField = "LoginID";
                DropDownList1.DataTextField = "FullName";
                DropDownList1.DataBind();
            }

        }


        /*
        protected void PopulateDropDownList(int CompanyID)
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName,LoginID from dbo.Logins", conn);
                adapter.Fill(subjects);
                ddlUsers.DataSource = subjects;
                ddlUsers.DataTextField = "FirstName";
                ddlUsers.DataValueField = "LoginID";
                ddlUsers.DataBind();
            }

            //Get the Current Account OwnerShip
            String strAccountOwnerShip = CompanyOwnerShip(CompanyID);
            String[] arr = strAccountOwnerShip.Split(':');
            String firstname = arr[0];
            String ownershipid = arr[1];

            //Select the value in the dropdown list
            ddlUsers.Items.FindByValue(ownershipid).Selected = true;
            ddlUsers.Items.FindByText(firstname).Selected = true;

        }
        */


    }
}