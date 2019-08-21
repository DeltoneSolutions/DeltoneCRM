using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class ProcessAddCommission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var orderid = Request["orderId"].ToString();
            var userid = Request["userid"].ToString();
            var amount = Request["amount"].ToString();
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var targetHndler = new OrderDAL(conn);


            targetHndler.UpdateOrderCommission(userid, orderid, amount);


            Response.Write("Ok");

        }

    }
}