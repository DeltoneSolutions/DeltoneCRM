using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using DeltoneCRM_DAL;


namespace DeltoneCRM.Manage.Process
{
    public partial class Process_UpdatePromoItem : System.Web.UI.Page
    {
        String connString;

        protected void Page_Load(object sender, EventArgs e)
        {
            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ProItemDAL promoDAL = new ProItemDAL(connString);
            int itemID =Int32.Parse(Request.Form["ItemID"].ToString());
            String itemname = Request.Form["NewItem"].ToString();
            int promoqty = Int32.Parse(Request.Form["NewDefaultQty"].ToString());
            //Modififed  28/05/2015 Sumudu Kodikara 
            float PromoPrice = float.Parse(Request.Form["NewCost"].ToString());
            float ShippingCost = float.Parse(Request.Form["NewShippingCost"].ToString());
            string ActInact = Request.Form["ActInact"].ToString();

            string finalActInact = "";

            if (ActInact == "false")
            {
                finalActInact = "N";
            }
            else
            {
                finalActInact = "Y";
            }

            Response.Write(promoDAL.udpatePromotionalitem(itemID, itemname, promoqty, PromoPrice, ShippingCost, finalActInact));
        }
    }
}