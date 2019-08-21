using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_AddItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                String strSupplierID = Request.Form["DropDownList1"].ToString();
                String strItemCode = Request.Form["NewItemCode"].Trim().ToString();
                String strDescription = Request.Form["NewDescription"].Trim().ToString();
                float cog = float.Parse(Request.Form["NewCOG"].ToString());
                float resellPrice = float.Parse(Request.Form["NewResellPrice"].ToString());
                String bestprice = Request.Form["Bestprice"].ToString();
                String faulty = Request.Form["Faulty"].ToString();
                int quantity = 0;
                string qty = Request.Form["Quantity"].ToString();
                if (!string.IsNullOrEmpty(qty))
                    quantity = Convert.ToInt32(qty);
            double? dsb=null;
            var dsbVal = Request.Form["DSB"].ToString();
            if (!string.IsNullOrEmpty(dsbVal))
                dsb = Convert.ToDouble(dsbVal);
                String SendBP = String.Empty;
                String SendFaulty = String.Empty;

            if (bestprice == "true")
            {
                SendBP = "Y";
            }
            else
            {
                SendBP = "N";
            }

            if (faulty == "true")
            {
                SendFaulty = "Y";
            }
            else
            {
                SendFaulty = "N";
            }

                //Response.Write(strDescription);

                DeltoneCRMDAL dal = new DeltoneCRMDAL();
                Response.Write(dal.AddNewItem(strSupplierID, strItemCode, strDescription, cog, resellPrice,
                    SendBP, SendFaulty, quantity, dsb));
        }
    }
}