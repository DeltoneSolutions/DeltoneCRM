using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchCalendarReminder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUserID"] == null)
                return;
            Response.Write(GetEventReminderByRep());

        }

        protected string GetEventReminderByRep()
        {
            var message = "";
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var eventsList = new List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent>();
            var eventsList12Am = new List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent>();
            var rep = Convert.ToInt32(Session["LoggedUserID"].ToString());
            eventsList = new CalendarEventDAL(cs).getReminderEvents(rep);
            var currentDateTime = DateTime.Now;
            var datetimeStart = DateTime.Now.AddMinutes(-2);
            var datetimeEnd = DateTime.Now.AddMinutes(3);
            var currentDate = datetimeStart.Date;
            var filteredList = (from res in eventsList where res.start.Date == currentDate select res).ToList();
            if (currentDateTime.Hour == 10 || currentDateTime.Hour == 11)
            {
                if (currentDateTime.Minute < 2)
                    eventsList12Am = (from res in filteredList where res.start.Hour == 0 select res).ToList();
            }
            filteredList = (from re in filteredList where re.start >= datetimeStart && re.start <= datetimeEnd select re).ToList();
            filteredList.AddRange(eventsList12Am);
            List<int> ids = new List<int>();
            //if (Session["idsEvents"] != null)
             //   ids = Session["idsEvents"] as List<int>;


            foreach (var item in filteredList)
            {
               // if (!ids.Contains(item.id))
               // {
                    if (message == "")
                        message = item.title + "--" + item.description;
                    else
                        message = message + "," + item.title + "--" + item.description;

                   // ids.Add(item.id);
              //  }
            }
          //  Session["idsEvents"] = ids;
            return message;

        }


    }
}