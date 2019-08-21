using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class updatestatusTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var configId = Request.QueryString["configId"].ToString();
            if (!string.IsNullOrEmpty(configId))
                Response.Write(UpdateTargetConfig(Convert.ToInt32(configId)));
        }

        public string UpdateTargetConfig(int configId)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var retur = "OK";
            var targetConfig = new RepDayOffDAL(connectionString);
            var isreached = true;
            targetConfig.UpdateTargetConfig(configId, isreached);

            return retur;

        }
    }
}