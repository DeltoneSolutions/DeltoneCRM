using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class FetchRepDayOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String repId = Request.QueryString["repId"].ToString();
            string tarId = Request.QueryString["tarId"].ToString();
            Response.Write(ReturnAllItems(repId, tarId));
        }

        protected string ReturnAllItems(string repId,string tarId)
        {
            String strOutput = "{\"aaData\":[";

            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);

            var listDayOffs = repDayOffDal.GetRepDayOffByRepIdANdTarId(Convert.ToInt32(repId), Convert.ToInt32(tarId));
            foreach (var item in listDayOffs)
            {
                var apprAtat = "N";
                if (item.IsApproved)
                    apprAtat = "Y";
                String strEdit = "<img src='../../Images/Edit.png'  onclick='Delete(" + item.Id + ")'/>";
                strOutput = strOutput + "[\"" + item.Id + "\","
                     + "\"" + item.DayOff.ToString("dd-MM-yyyy") + "\","
                      + "\"" + apprAtat + "\","
                     + "\"" + strEdit + "\"],";
            }

            if (listDayOffs.Count() > 0)
            {
                int Length = strOutput.Length;
                strOutput = strOutput.Substring(0, (Length - 1));
            }
            strOutput = strOutput + "]}";
          


            return strOutput;
        }
    }
}