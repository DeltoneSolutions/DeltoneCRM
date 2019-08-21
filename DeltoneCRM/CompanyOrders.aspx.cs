using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class CompanyOrders : System.Web.UI.Page
    {
        int OrderId;
        String strConact=String.Empty;
        String strCompany=String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            divDisplay.Style.Value = "";

            if (!String.IsNullOrEmpty(Request.QueryString["Order"]))
            {
                OrderId = Int32.Parse(Request.QueryString["Order"].ToString());
            }
            if (!String.IsNullOrEmpty(Request.QueryString["ContactID"]))
            {
                strConact = Request.QueryString["ContactID"].ToString();
                //set Hidden Fild value
                hdnContactID.Value = strConact;

            }
            if (!String.IsNullOrEmpty(Request.QueryString["CompanyID"]))
            {
                strCompany = Request.QueryString["CompanyID"].ToString();
                //Set Hidden Field value
                hdnCompanyID.Value = strCompany;
            }

            //Display the Result for Update or Creation of new Order
            if (!String.IsNullOrEmpty(Request.QueryString["Update"]))
            {
                lblMessage.Text = "Order: " + OrderId + " Saved successfully";
                
            }
            if (!String.IsNullOrEmpty(Request.QueryString["Result"]))
            {
                lblMessage.Text = "Order:" + OrderId + " Created successfully";
                
            }




        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Order.aspx?Oderid=81&cid=4&Compid=1");
        }

        protected void btnCrateOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("Order.aspx?cid=" + strConact + "&Compid=" + strCompany);
        }


    }
}