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
    public partial class process_AddHoliday : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 yea = Int32.Parse(Request.Form["Year"].ToString());
            Int32 mon = Int32.Parse(Request.Form["Month"].ToString());
            var holDay = Convert.ToDateTime(Request.Form["holiday"].ToString());
            var holDayAus = Convert.ToDateTime(holDay).ToString("yyyy-MM-dd");
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var ausHol = Convert.ToDateTime(holDayAus);
            var ausHolidayObj = new AusHoliday()
            {
                CreatedDate=DateTime.Now,
                Month = mon,
                Year=yea,
                HolidayDate = ausHol
            
            };
            RepDayOffDAL dal = new RepDayOffDAL(conn);
            dal.InsertHolidayByMonth(ausHolidayObj);
            Response.Write("Ok");
        }
    }
}