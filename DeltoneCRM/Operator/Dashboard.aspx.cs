using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Operator
{
    public partial class Dashboard : System.Web.UI.Page
    {
        String strUserProfile = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!(String.IsNullOrEmpty(Session["USERPROFILE"] as String)))
            {
                strUserProfile = Session["USERPROFILE"].ToString();
                if (strUserProfile.Equals("STANDARD"))
                {
                    //Admin User make Pending Approval Panel Visible
                    pnlPendingApproval.Visible = true;
                }
            }
        }
    }
}