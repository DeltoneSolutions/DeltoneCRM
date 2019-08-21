using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace DeltoneCRM
{
    public partial class SubmitStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // check if the page was called properly
            if (Request.Params["ID"] != null)
            {
                Guid id = new Guid(Request.Params["ID"]);
                // check if there is a result in the controller
                if (SimpleProcessCollection.GetResult(id) == String.Empty)
                {
                    // no result - let's refresh again
                    Response.AddHeader("Refresh", "3");
                }
                else
                {
                    // everything's fine, we have the result
                    lblStatus.Text = "Job is done.";
                    Response.Redirect("Order.aspx");
                }
            }
            else
            {
                Response.Redirect("Order.aspx");
            }
        }
    }
}