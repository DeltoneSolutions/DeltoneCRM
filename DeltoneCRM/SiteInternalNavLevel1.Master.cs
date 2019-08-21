using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class SiteInternalNavLevel1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Welcome - You are logged in as Administrator

            if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
            {
                if (Session["USERPROFILE"].ToString().Equals("ADMIN"))
                {
                    lbWelComeMsg.Text = "Welcome " + Session["LoggedUser"] + ".";
                    searchtd.Visible = true;
                }
                if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
                {
                    lbWelComeMsg.Text = "Welcome " + Session["LoggedUser"] + ".";
                    ManageLink.Visible = false;
                    ReportsLink.Visible = false;
                    noorderForRep.Visible = true;
                    leadsrep.Visible = true;
                    salerepReports.Visible = true;
                    quoteallocateRep.Visible = true;
                }
            }
        }

        //Get and Set Property for Master Page WelCome Message
        public string WelComeMessage
        {
            get
            {
                return lbWelComeMsg.Text;
            }
            set
            {
                lbWelComeMsg.Text = value;
            }
        }
        //End Get and Set Property for Master Page WelCome Message


        protected void lnkLogout_Click(object sender, EventArgs e)
        {

        }

        protected void lbLogout_Click(object sender, EventArgs e)
        {
            Session["USERPROFILE"] = null;
            Session["LoggedUser"] = null;
            Response.Redirect("http://delcrm");
        }
    }
}