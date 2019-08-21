using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using DeltoneCRM_DAL;

namespace DeltoneCRM.EmailTemplates.query
{
    public partial class getOrderDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String OrderID = "2824";
            Response.Write(getOrderDetailsList(OrderID));
        }

        public String getOrderDetailsList(String OID)
        {
            OrderDAL order = new OrderDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

            String returnString = order.getOrderItemList(OID);

            return returnString;
        }
    }
}