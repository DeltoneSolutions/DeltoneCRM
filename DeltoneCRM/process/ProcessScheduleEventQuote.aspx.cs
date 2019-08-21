using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.process
{
    public partial class ProcessScheduleEventQuote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var obj =new DeltoneCRM.DataHandlers.DisplayEventHandler.ImproperCalendarEvent();
            obj.title = Request.Form["title"].ToString();
            obj.description = Request.Form["description"].ToString();
            obj.start = Request.Form["start"].ToString();
            obj.color = Request.Form["color"].ToString();
            obj.QuoteId = Request.Form["QuoteId"].ToString();
            obj.companyId = Convert.ToInt32( Request.Form["companyId"].ToString());
            HttpContext.Current.Session["calendarObj"] = obj;
        }
    }
}