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
    public partial class DeleteRepDayOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String dayoffId = Request.QueryString["dayoffId"].ToString();
            Response.Write(DeleteRepDayOffItem(dayoffId));
        }

        public string DeleteRepDayOffItem(string dayOffId)
        {

            var retur = "OK";
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
            repDayOffDal.DeleteRepDayOff(Convert.ToInt32(dayOffId));
            return retur;

        }
    }
}