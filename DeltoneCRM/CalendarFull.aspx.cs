using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class CalendarFull : System.Web.UI.Page
    {
        public string clickeddateEvent { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedUserID"] == null)
                return;

            var qstrgin = Request.QueryString["evId"];
            if (qstrgin != null)
            {
                clickeddateEvent = EventSelectedByCompanyEventId(Convert.ToInt32(qstrgin));
            }
        }


        private string EventSelectedByCompanyEventId(int eveID)
        {
            var dateEventstarted = "";
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var selectedEve = new CalendarEventDAL(cs).getEvents(eveID);
            if (selectedEve.Count() > 0)
            {
                var contactStr = "";
                var comId = Convert.ToInt32(selectedEve[0].companyId.ToString());
                if (comId > 0)
                {
                    var conId = GetContacts(comId.ToString());
                    contactStr = PopulateContact(conId);
                }
                var desc = Regex.Replace(selectedEve[0].description.Trim(), @"\t|\n|\r", "");

                dateEventstarted = selectedEve[0].id.ToString() + "#rr#"
                    + selectedEve[0].title.Trim() + "#rr#"
                     + desc + "#rr#"
                      + selectedEve[0].start + "#rr#"
                       + selectedEve[0].end + "#rr#"
                        + selectedEve[0].allDay + "#rr#"
                         + selectedEve[0].color + "#rr#"
                          + selectedEve[0].url + "#rr#"
                           + selectedEve[0].createdDate + "#rr#"
                            + selectedEve[0].modifiedDate + "#rr#"
                    + selectedEve[0].start.ToString("yyyy-MM-dd") + "#rr#"
                     + comId + "," + contactStr;

            }

            return dateEventstarted;

        }

        private string GetContacts(string companyId)
        {
            var conId = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strCompanyID = Convert.ToInt32(companyId);
                    cmd.CommandText = "SELECT ContactID FROM dbo.Contacts WHERE CompanyID=" + strCompanyID;

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            conId = sdr["ContactID"].ToString();
                        }


                    }
                    conn.Close();
                }

            }

            return conId;

        }


        //This Method populate the Contact given by ContactID
        protected String PopulateContact(String strContactID)
        {

            String strContact = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " SELECT * FROM dbo.Contacts WHERE ContactID=" + strContactID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            String PostalAddressBuilder = sdr["STREET_AddressLine1"].ToString() +
                                " " +
                                sdr["STREET_City"]
                                + " "
                               + " " + sdr["STREET_Region"]
                               + " " + sdr["STREET_PostalCode"]
                               + " " + sdr["STREET_Country"];
                            // String PhysicalAddressBuilder = sdr["POSTAL_AddressLine1"].ToString() + sdr["POSTAL_City"] + sdr["POSTAL_Region"] + sdr["POSTAL_PostalCode"] + sdr["POSTAL_Country"];
                            string TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                            string MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                            string faxBuilder = sdr["FAX_AreaCode"].ToString() + sdr["FAX_CountryCode"] + sdr["FAX_Number"];
                            string contactBuilder = sdr["FirstName"].ToString() + " "
                                + sdr["LastName"];

                            strContact = contactBuilder;
                            strContact = strContact + "," + TeleBuilder + " " + MobileBuilder + " " + sdr["Email"].ToString();
                            strContact = strContact + "," + PostalAddressBuilder;


                        }
                    }

                    conn.Close();
                }
            }

            return strContact;

        }

        //this method only updates title and description
        //this is called when a event is clicked on the calendar
        [System.Web.Services.WebMethod(true)]
        public static string UpdateEvent(DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent cevent)
        {
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
          //  if (idList != null && idList.Contains(cevent.id))
            {
                //if (CheckAlphaNumeric(cevent.title) && CheckAlphaNumeric(cevent.description))
                //{
                string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                new CalendarEventDAL(cs).updateEvent(cevent.id, cevent.title, cevent.description,
                    cevent.color, cevent.isreminder);



                return "updated event with id:" + cevent.id + " update title to: " + cevent.title +
                " update description to: " + cevent.description + " notify " + cevent.isreminder;
                // }

            }

            return "unable to update event with id:" + cevent.id + " title : " + cevent.title +
                " description : " + cevent.description;
        }

        //this method only updates start and end time
        //this is called when a event is dragged or resized in the calendar
        [System.Web.Services.WebMethod(true)]
        public static string UpdateEventTime(DeltoneCRM.DataHandlers.DisplayEventHandler.ImproperCalendarEvent
            improperEvent)
        {
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
           // if (idList != null && idList.Contains(improperEvent.id))
            {
                string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                new CalendarEventDAL(cs).updateEventTime(improperEvent.id,
                                         Convert.ToDateTime(improperEvent.start),
                                         Convert.ToDateTime(improperEvent.end),
                                         improperEvent.allDay);  //allDay parameter added for FullCalendar 2.x

                return "updated event with id:" + improperEvent.id + " update start to: " + improperEvent.start +
                    " update end to: " + improperEvent.end;
            }

            return "unable to update event with id: " + improperEvent.id;
        }

        // this method used to add custom date selection 
        [System.Web.Services.WebMethod(true)]
        public static string UpdateEventTimeCustomDate(DeltoneCRM.DataHandlers.DisplayEventHandler.ImproperCalendarEvent
            improperEvent)
        {
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
          //  if (idList != null && idList.Contains(improperEvent.id))
            {
                string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                var startDate = Convert.ToDateTime(improperEvent.start);
                var endDate = Convert.ToDateTime(improperEvent.end);

               // if (endDate.Date < startDate.Date)
                    endDate = startDate.AddHours(1);

                improperEvent.allDay = false;

                new CalendarEventDAL(cs).updateEventTime(improperEvent.id,
                                        startDate,
                                        endDate,
                                         improperEvent.allDay);  //allDay parameter added for FullCalendar 2.x

                return "updated event with id:" + improperEvent.id + " update start to: " + improperEvent.start +
                    " update end to: " + improperEvent.end;
            }

            return "unable to update event with id: " + improperEvent.id;
        }

        //called when delete button is pressed
        [System.Web.Services.WebMethod(true)]
        public static String deleteEvent(int id)
        {
            //idList is stored in Session by JsonResponse.ashx for security reasons
            //whenever any event is update or deleted, the event id is checked
            //whether it is present in the idList, if it is not present in the idList
            //then it may be a malicious user trying to delete someone elses events
            //thus this checking prevents misuse
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];
            //if (idList != null && idList.Contains(id))
            {
                new CalendarEventDAL(cs).deleteEvent(id);
                return "deleted event with id:" + id;
            }

            return "unable to delete event with id: " + id;
        }

        //called when Add button is clicked
        //this is called when a mouse is clicked on open space of any day or dragged 
        //over mutliple days
        [System.Web.Services.WebMethod]
        public static int addEvent(DeltoneCRM.DataHandlers.DisplayEventHandler.ImproperCalendarEvent improperEvent)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent cevent = new DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent()
            {
                title = improperEvent.title,
                description = improperEvent.description,
                start = Convert.ToDateTime(improperEvent.start),
                end = Convert.ToDateTime(improperEvent.end),
                allDay = improperEvent.allDay,
                color = improperEvent.color,
                isreminder = improperEvent.isreminder

            };
            cevent.url = "";

            // if (CheckAlphaNumeric(cevent.title) && CheckAlphaNumeric(cevent.description))
            // {
            var userId = 0;
            if (HttpContext.Current.Session["LoggedUserID"] != null)
                userId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            int key = new CalendarEventDAL(cs).addEvent(cevent, 0, userId);

            List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];

            if (idList != null)
            {
                idList.Add(key);
            }

            return key; //return the primary key of the added cevent object
            // }

            return -1; //return a negative number just to signify nothing has been added
        }

        private static bool CheckAlphaNumeric(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z0-9 ]*$");
        }
    }
}