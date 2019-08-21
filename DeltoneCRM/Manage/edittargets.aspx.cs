using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace DeltoneCRM.Manage
{
    public partial class edittargets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Today.Month);
            lblMonthName.Text = Month.ToUpper() ;
        }
    }
}