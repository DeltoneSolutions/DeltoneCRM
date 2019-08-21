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
    public partial class AccountOwner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hidden_repid.Value = Request.QueryString["LoginID"].ToString();
            datelbl.Text = DateTime.Now.ToString();

            LoginDAL logindal = new LoginDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            Replbl.Text = logindal.getLoginNameFromID(Request.QueryString["LoginID"].ToString());

        }
    }
}