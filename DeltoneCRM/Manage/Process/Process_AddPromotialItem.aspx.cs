using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_AddPromotialItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Request.Form["NewItem"]) && !String.IsNullOrEmpty(Request.Form["NewCost"]) && !String.IsNullOrEmpty(Request.Form["NewDefaultQty"]) && !String.IsNullOrEmpty(Request.Form["NewShippingCost"]))
            {
                String strPromoItemName = Request.Form["NewItem"].ToString();
                float ItemCost = float.Parse(Request.Form["NewCost"].ToString());
                int ItemQty = int.Parse(Request.Form["NewDefaultQty"].ToString());
                float ItemShipCost = float.Parse(Request.Form["NewShippingCost"].ToString());

                DeltoneCRMDAL dal = new DeltoneCRMDAL();
                Response.Write(dal.AddNewPromoItem(strPromoItemName, ItemCost, ItemQty, ItemShipCost));
            }
        }
    }
}