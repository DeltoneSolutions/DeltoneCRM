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
    public partial class FetchHoliday : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String year = Request.QueryString["year"].ToString();
            String month = Request.QueryString["month"].ToString();
            Response.Write(ReturnAllItems(year, month));
        }

        protected string ReturnAllItems(string year,string month)
        {
            String strOutput = "{\"aaData\":[";

            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
            var monCon=Convert.ToInt32(month);
            var yerCon = Convert.ToInt32(year);
            var listDayOffs = repDayOffDal.GetAusHoliday();
            listDayOffs = (from li in listDayOffs where li.Month == monCon && li.Year == yerCon select li).ToList();

            foreach (var item in listDayOffs)
            {
                String strEdit = "<img src='../../Images/Edit.png'  onclick='Delete(" + item.Id + ")'/>";
                strOutput = strOutput + "[\"" + item.Id + "\","
                     + "\"" + item.HolidayDate.ToString("dd-MM-yyyy") + "\","
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