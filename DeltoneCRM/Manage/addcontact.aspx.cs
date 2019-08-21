using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage
{
    public partial class addcontact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CompID.Value = Request.QueryString["cid"].ToString();
            ReLocation.Value = Request.QueryString["loc"].ToString();
            if (Request.QueryString["fNa"] != null)
                NewFirstName.Value = Request.QueryString["fNa"].ToString();
            if (Request.QueryString["lNa"] != null)
                NewLastName.Value = Request.QueryString["lNa"].ToString();
            if (Request.QueryString["cEm"] != null)
                NewEmailAddy.Value = Request.QueryString["cEm"].ToString();
                

        }
    }
}