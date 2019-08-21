using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_AddDeliveryItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.Form["NewItem"]) && !String.IsNullOrEmpty(Request.Form["NewCost"]))
            {
                String strDeliveryItem = Request.Form["NewItem"].ToString();
                float deliveryCost = float.Parse(Request.Form["NewCost"].ToString());

                DeltoneCRMDAL dal = new DeltoneCRMDAL();
                Response.Write(dal.AddNewDeliveryItem(strDeliveryItem, deliveryCost));

            }
        }
    }
}