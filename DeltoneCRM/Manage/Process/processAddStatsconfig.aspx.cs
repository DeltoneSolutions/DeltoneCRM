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
    public partial class processAddStatsconfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var targetTitle = Request.Form["targettitle"].ToString();
            var targetDay = Convert.ToDateTime(Request.Form["targetday"].ToString());
            var targetAmount = Convert.ToDecimal(Request.Form["targetamount"].ToString());
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var targetHndler = new RepDayOffDAL(conn);
            var targetObj = new StatsTargetConfig()
            {
                IsReached = false,
                TargetAmount = targetAmount,
                TargetDay = targetDay,
                TargetTitle = targetTitle
            };
          var userid=  Convert.ToInt32(Session["LoggedUserID"].ToString());
          targetHndler.InsertIntoTargetConfig(targetObj, userid);


            Response.Write("Ok");

        }
    }
}