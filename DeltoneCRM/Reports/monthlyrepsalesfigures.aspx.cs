using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Reports
{
    public partial class monthlyrepsalesfigures : System.Web.UI.Page
    {
       
             public static string RepName;
        public static int dateRange;

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("..\\dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            {
                Response.Redirect("Reports.aspx");
            }
            else
            Response.Redirect("Reports.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillRepsList();
                enddate.Value = DateTime.Now.Year.ToString();
                var dateNow=DateTime.Now;
                StartDateTxt.Value = new DateTime(dateNow.Year, dateNow.Month, 1).ToString("dd-MM-yyyy");
                var days = DateTime.DaysInMonth(dateNow.Year,dateNow.Month);
                var endate = new DateTime(dateNow.Year, dateNow.Month, days);
                EndDateTxt.Value = endate.ToString("dd-MM-yyyy");
            }

            if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            {
                repdropTr.Visible = false;
                Button2.Visible = false;
            }
            
        }

        public void fillRepsList()
        {
            if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            {
            }
            else
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    String strLoggedUsers = String.Empty;
                    DataTable subjects = new DataTable();

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    SqlDataAdapter adapter = new SqlDataAdapter("select FirstName + ' ' + LastName AS FullName, LoginID from dbo.Logins WHERE LoginID NOT IN (4, 9, 8, 19,20,21,22) AND Active = 'Y' ", conn);
                    adapter.Fill(subjects);
                    ddlRepList.DataSource = subjects;
                    ddlRepList.DataTextField = "FullName";
                    ddlRepList.DataValueField = "LoginID";
                    ddlRepList.DataBind();
                    ddlRepList.Items.Insert(0, new ListItem("ALL", "9999"));
                }
            }
        }

        protected void GR_Click(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            {
                RepName = Session["LoggedUserID"].ToString();
            }
            else
            RepName = ddlRepList.SelectedValue.ToString();
            String Month = DropDownList1.SelectedValue.ToString();
            String Year = enddate.Value.ToString();
           // Response.Redirect("MonthlySalesFigures.aspx?LoginID=" + RepName + "&Month=" + Month + "&Year=" + Year);
            var startDate = StartDateTxt.Value;
            var endDate = EndDateTxt.Value;

            Response.Redirect("MonthlySalesFigures.aspx?LoginID=" + RepName + "&start=" + startDate + "&end=" + endDate);
        }
        
    }
}