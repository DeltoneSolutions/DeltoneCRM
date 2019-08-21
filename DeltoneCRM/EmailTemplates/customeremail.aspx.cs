using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.EmailTemplates
{
    public partial class customeremail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String OrderID = "DTS-1234";
            lbl_OrderNumber.Text = OrderID;
        }
    }
}