using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using DeltoneCRM_DAL;
using System.Configuration;

namespace DeltoneCRM.Reports
{
    public partial class MonthlySalesFigures : System.Web.UI.Page
    {
        LoginDAL login = new LoginDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);


        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("..\\dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            Response.Redirect("sel_MonthlySalesFigures.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            hidden_repid.Value = Request.QueryString["LoginID"].ToString();
           // hidden_month.Value = Request.QueryString["Month"].ToString();
           // hidden_year.Value = Request.QueryString["Year"].ToString();
            var startDate = Request.QueryString["start"].ToString();
             var endDate = Request.QueryString["end"].ToString();

            hidden_start.Value = startDate;
            hidden_end.Value = endDate;
            //int monthNumber = int.Parse(Request.QueryString["Month"]);

            Monthlbl.Text = SetReportTitle(startDate,endDate);
         //   Monthlbl.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNumber).ToUpper();

            Replbl.Text = login.getLoginNameFromID(Request.QueryString["LoginID"].ToString()).ToUpper();
            datelbl.Text = DateTime.Now.ToString();

        }

        private string SetReportTitle(string start, string end)
        {

            var coString = " From : " + start;

            if (!string.IsNullOrEmpty(end))
                coString = coString + " To : " + end;

            return coString;
        }
    }
}