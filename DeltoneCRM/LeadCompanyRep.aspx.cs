using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class LeadCompanyRep : System.Web.UI.Page
    {
        public string userlevel = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"].ToString() == "ADMIN")
            {
                userlevel = "1";
            }
            else
                userlevel = "0";



        }
    }
}