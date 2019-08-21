using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using DeltoneCRM_DAL;

namespace DeltoneCRM.Manage.ViewEditScreens
{
    public partial class ViewEditPromotionalItem : System.Web.UI.Page
    {
        String strConnString;

        protected void Page_Load(object sender, EventArgs e)
        {
            strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ProItemDAL prodal = new ProItemDAL(strConnString);

            if(!String.IsNullOrEmpty(Request.QueryString["promoid"]))
            {
                int promoid= Int32.Parse( Request.QueryString["promoid"].ToString());
                String strPromoItem = prodal.getPromoItem(promoid);
                String[] arrPromoItem = strPromoItem.Split(':');
                //Populate Items 

                hdnEditPromoItemID.Value = arrPromoItem[0].ToString();
                NewItem.Value = arrPromoItem[1].ToString();
                NewDefaultQty.Value = arrPromoItem[2].ToString();

                double cost = Convert.ToDouble(arrPromoItem[3].ToString());
                NewCost.Value = cost.ToString("0.00");
                double shippingcost = Convert.ToDouble(arrPromoItem[4].ToString());
                NewShippingCost.Value = shippingcost.ToString("0.00");

                if (arrPromoItem[5].ToString().Trim().Equals("N"))
                {
                    activechkbox.Checked = true;
                }
                else
                {
                    activechkbox.Checked = false;
                    string strScript = "<script language='javascript'>$(document).ready(function (){$('#activechkbox').toggleCheckedState(true);});</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
                }


            }

        }

    }
}