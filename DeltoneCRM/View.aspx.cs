using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class View : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Comment added here Sumudu Kodikara
            String strSavedContact = Request.QueryString["Contact"].ToString();
            lblResult.Text = strSavedContact + " has been saved";

        }

        



    }
}