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
    public partial class Process_attuser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strFirstName = Request.Form["NewFirstName"].ToString();
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL dal = new RepDayOffDAL(connString);
            dal.InsertRepAttendance(strFirstName);
            Response.Write("OK");
        }
    }
}