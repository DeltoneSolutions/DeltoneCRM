using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DeltoneCRM.Reports
{
    public partial class NoSalesAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hidden_repid.Value = Request.QueryString["Repname"];
            hidden_dateid.Value = Request.QueryString["DateRange"];

            divdaterange.InnerText = Request.QueryString["DateRange"];
            thedate.InnerText = DateTime.Today.ToString() ;
        }
    }
}