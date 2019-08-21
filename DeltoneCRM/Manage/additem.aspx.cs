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
    public partial class additem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            SqlConnection conn = new SqlConnection(strConn);
            conn.Open();
            String AddStr = "SELECT SupplierName, SupplierID FROM dbo.Suppliers";
            SqlCommand ADDcmd = new SqlCommand(AddStr, conn);
            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(ADDcmd);
            conn.Close();
            adapter.Fill(table);
            DropDownList1.DataSource = table;
            DropDownList1.DataValueField = "SupplierID";
            DropDownList1.DataTextField = "SupplierName";
            DropDownList1.DataBind();
        }
    }
}