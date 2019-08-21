using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_AddTarget : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strStaffID = Request.Form["NewStaffMember"].ToString();
            float strCommission =  float.Parse(Request.Form["NewCommission"].ToString());
            int strWorkingDays = Int32.Parse(Request.Form["NewWorkingDays"].ToString());
            String strMonth = Request.Form["NewMonth"].ToString();
            String strYear = Request.Form["NewYear"].ToString();

            //Response.Write(strDescription);

            DeltoneCRMDAL dal = new DeltoneCRMDAL();
            Response.Write(dal.AddNewTarget(strStaffID, strCommission, strWorkingDays, strMonth, strYear));
        }
    }
}