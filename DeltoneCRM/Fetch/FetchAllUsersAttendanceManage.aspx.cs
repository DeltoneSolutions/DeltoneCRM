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
    public partial class FetchAllUsersAttendanceManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnAllItems());
        }

        protected string ReturnAllItems()
        {
            String strOutput = "{\"aaData\":[";

            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL dal = new RepDayOffDAL(connString);
            var getallNames = dal.GetAttendanceReps();
            var count = 0;
            foreach (var item in getallNames)
            {
                count = count + 1;
                string c = "<img src='../Images/error.png' onclick='DeleteCal(" + item.Id + ")'/>";
                strOutput = strOutput + "[\"" + item.Id + "\"," + "\"" + item.Name + "\"," + "\"" + c + "\"],";

            }

            if (getallNames.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }
            strOutput = strOutput + "]}";

            return strOutput;
        }
    }
}