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
    public partial class process_Deletetstatsconfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var targetTitle = Request.QueryString["Id"].ToString();
            Response.Write(DeleteTargetConfig(targetTitle));
        }

        public string DeleteTargetConfig(string targetConfigId)
        {

            var retur = "OK";
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
            repDayOffDal.DeleteTargetConfig(Convert.ToInt32(targetConfigId));
            return retur;

        }
    }
}