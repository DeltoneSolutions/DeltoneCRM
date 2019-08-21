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
    public partial class addtarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {  

            /*
            String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            //String AddStr = "SELECT FirstName, LastName, LoginID FROM dbo.Logins";
            String AddStr = "SELECT FirstName + ' ' +  LastName as fullname,LoginID FROM dbo.Logins";
            SqlCommand ADDcmd = new SqlCommand(AddStr, conn);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(ADDcmd);
            //table.Columns.Add("FullName", typeof(string), "FirstName + ' ' + LastName"); 
            
            adapter.Fill(table);
            

            DropDownList1.DataSource = table;
            DropDownList1.DataValueField = "LoginID";
            DropDownList1.DataTextField = "FullName";
            DropDownList1.DataBind();
            */

              String strLoggedUsers = String.Empty;
              DataTable subjects = new DataTable();
              String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
              using(SqlConnection conn=new SqlConnection())
              {
                 conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                 //SqlDataAdapter adapter = new SqlDataAdapter("SELECT FirstName + ' ' +  LastName as FullName,LoginID FROM dbo.Logins", conn);
                 SqlDataAdapter adapter = new SqlDataAdapter("select FirstName + ' ' + LastName AS FullName, LoginID from dbo.Logins WHERE LoginID NOT IN (4, 9, 8, 19,20,21,22) AND Active = 'Y' ", conn);
                 adapter.Fill(subjects);
                 NewStaffMember.DataSource = subjects;
                 NewStaffMember.DataTextField = "FullName";
                 NewStaffMember.DataValueField = "LoginID";
                 NewStaffMember.DataBind();
              }

        }

    }
}