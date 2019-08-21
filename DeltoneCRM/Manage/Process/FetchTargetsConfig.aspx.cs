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
    public partial class FetchTargetsConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnAllItems());
        }

        protected string ReturnAllItems()
        {
            String strOutput = "{\"aaData\":[";

            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
           
            var targetConfigs = repDayOffDal.GetAllTargetItems();

            foreach (var item in targetConfigs)
            {
                String strEdit = "<img src='../../Images/Edit.png'  onclick='Delete(" + item.Id + ")'/>";
                strOutput = strOutput + "[\"" + item.Id + "\","
                      + "\"" + item.TargetTitle + "\","
                        + "\"" + Math.Truncate( item.TargetAmount) + "\","
                     + "\"" + item.TargetDay.ToString("dd-MM-yyyy") + "\","
                     + "\"" + strEdit + "\"],";
            }

            if (targetConfigs.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }
            strOutput = strOutput + "]}";



            return strOutput;
        }
    }
}