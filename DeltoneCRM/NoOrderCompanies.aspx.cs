using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class NoOrderCompanies : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("dashboard1.aspx");



        }
    }
}