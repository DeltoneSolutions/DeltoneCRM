using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class CompanyOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Response.Redirect("Order.aspx?Oderid=51&cid=4&Compid=1");
        }
        protected void btnTest_Click(object sender, EventArgs e)
        {  
            
           
        }

        protected void btn_Click(object sender, EventArgs e)
        {
        }
       


    }
}