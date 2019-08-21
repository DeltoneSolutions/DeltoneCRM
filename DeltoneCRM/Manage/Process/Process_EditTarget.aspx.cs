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




namespace DeltoneCRM.Manage.Process
{
    public partial class Process_EditTarget : System.Web.UI.Page
    {
        private String connString;
        protected void Page_Load(object sender, EventArgs e)
        {
             connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
             TargetDAL targetdal = new TargetDAL(connString);
             int TargetID = Int32.Parse(Request.Form["targetid"].ToString());
             int LoginID = Int32.Parse(Request.Form["user"].ToString());
             int TargetMonth = Int32.Parse(Request.Form["targetmonth"].ToString());
             int TargetYear = Int32.Parse(Request.Form["targetyears"].ToString());
             float TargetCommision = float.Parse(Request.Form["targetcommision"].ToString());
             int TargetWorkingDays = Int32.Parse(Request.Form["workingdays"].ToString());
             var dayOff = Request.Form["dayOff"];
            
             Response.Write(targetdal.updateTarget(TargetID,LoginID,TargetMonth,TargetYear,TargetCommision,TargetWorkingDays));

             var approved = Convert.ToBoolean(Request.Form["isapproeved"].ToString());

             //targetid,targetcommision,targetmonth,workingdays,targetyears,user
             if (!string.IsNullOrEmpty(dayOff))
                 InsertDayOff(dayOff, TargetID, LoginID, approved);

        }

        public void InsertDayOff(string dayOff,int targetId,int repId,bool approved)
        {
            var dayoffdate = Convert.ToDateTime(dayOff).ToString("yyyy-MM-dd");
           var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
           RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
           var userId = Convert.ToInt32( Session["LoggedUserID"].ToString());
           var dateTimeDayOff = Convert.ToDateTime (dayoffdate);
           var obj = new RepDayOff()
           {
               CreatedDate=DateTime.Now,
               CreateduserId = userId,
               UserId = repId,
               DayOff = dateTimeDayOff,
               TargetId = targetId,
               IsApproved = approved
           };
            repDayOffDal.InsertRepDayOff(obj);

        }
    }
}