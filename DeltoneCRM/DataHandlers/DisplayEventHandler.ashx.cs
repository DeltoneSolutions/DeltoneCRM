using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DeltoneCRM.DataHandlers
{
    /// <summary>
    /// Summary description for DisplayEventHandler
    /// </summary>
    public class DisplayEventHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            DateTime start = Convert.ToDateTime(context.Request.QueryString["start"]);
            DateTime end = Convert.ToDateTime(context.Request.QueryString["end"]);
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            List<int> idList = new List<int>();
            List<ImproperCalendarEvent> tasksList = new List<ImproperCalendarEvent>();
            var userId = 0;
            var searchString = "";
            if (context.Request.QueryString["qqq"] != null)
            {
                searchString = context.Request.QueryString["qqq"].ToString();
            }
            if (context.Session["LoggedUserID"] != null)
            {
                userId = Convert.ToInt32(context.Session["LoggedUserID"].ToString());
            }
            var eventsList = new List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent>();

            if (!string.IsNullOrEmpty(searchString))
            {
                eventsList = new CalendarEventDAL(cs).getSearchEvents(searchString, userId);
            }
            else
                eventsList = new CalendarEventDAL(cs).getEvents(start, end, userId);

            var callList = GetLeadEventList(userId, searchString);

            eventsList.AddRange(callList);
            eventsList = eventsList.OrderBy(x => x.start).ToList();


            foreach (DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent cevent in eventsList)
            {
                var comId = Convert.ToInt32(cevent.companyId);

                var imObj = new ImproperCalendarEvent
                  {
                      id = cevent.id,
                      title = cevent.title,
                      start = String.Format("{0:s}", cevent.start),
                      end = String.Format("{0:s}", cevent.end),
                      color = cevent.color,
                      description = cevent.description + " : " + cevent.createdDate,
                      allDay = cevent.allDay,
                      url = cevent.url,
                      createdDate = cevent.createdDate,
                      modifiedDate = cevent.modifiedDate,
                      companyId = comId,
                      isreminder = cevent.isreminder,
                      IsQuoteEvent = cevent.isquoteevent
                  };
                if (!string.IsNullOrEmpty(imObj.url))
                {

                    var QuoteIdFromUrl = GetQuoteIdFromUrl(imObj.url);
                    if (!string.IsNullOrEmpty(QuoteIdFromUrl))
                    {
                       
                        var notesQuote=getQuoteNotes(Convert.ToInt32(QuoteIdFromUrl));
                       // imObj.description = imObj.description + notesQuote;
                    }
                       
                }
                if (comId > 0)
                    imObj = SetContactDetails(comId, imObj);

                tasksList.Add(imObj);

                idList.Add(cevent.id);
            }


            //Generate JSON serializable events
            context.Session["idList"] = idList;

            //Serialize events to string
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(tasksList);

            //Write JSON to response object
            context.Response.Write(sJSON);
        }


        private List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent> GetLeadEventList(int userId, string sear)
        {
            var eventsList = new List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent>();
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var leadComList = new CompanyDAL(cs).GetLeadCompanyNOExpires(userId);

            var callBackEvents = new CalendarEventDAL(cs).GetAllCAllBackEvents();
            callBackEvents = callBackEvents.OrderByDescending(x => x.start).ToList();
            foreach (var com in leadComList)
            {
                var eve = (from cl in callBackEvents where cl.companyId == com.CompanyId select cl).ToList();
                if (eve.Count > 0)
                {
                    eventsList.Add(eve[0]);
                }

            }

            if (!string.IsNullOrEmpty(sear))
            {
                eventsList = (from ec in eventsList
                              where ec.description.Contains(sear) ||
                                  ec.title.Contains(sear)
                              select ec).ToList();
            }

            return eventsList;
        }

        private string GetQuoteIdFromUrl(string url)
        {
            var quoteId = "";

            Uri myUri = new Uri(url);
             quoteId = HttpUtility.ParseQueryString(myUri.Query).Get("OderID");

            return quoteId;
        }

        private ImproperCalendarEvent SetContactDetails(int comId, ImproperCalendarEvent eventC)
        {

            var contactId = GetContacts(comId);
            if (!string.IsNullOrEmpty(contactId))
                eventC = PopulateContact(contactId, eventC);
            return eventC;

        }

        private string getQuoteNotes(int quoteId)
        {

            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var companyNote = new DeltoneCRM_DAL.CompanyDAL(connectionstring).getQuoteCompanyNotes(quoteId.ToString());


            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionstring;


            return companyNote;
        }



        private string GetContacts(int companyId)
        {
            var conId = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT ContactID FROM dbo.Contacts WHERE CompanyID=" + companyId;

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
        protected ImproperCalendarEvent PopulateContact(string strContactID,
            ImproperCalendarEvent eventC)
        {

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

                            eventC.contactbuilder = contactBuilder;
                            eventC.telephonebuilder = TeleBuilder + " " + MobileBuilder + " " + sdr["Email"].ToString();
                            eventC.addressbuilder = PostalAddressBuilder;



                        }
                    }

                    conn.Close();
                }
            }
           

            return eventC;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        //Do not use this object, it is used just as a go between between javascript and asp.net
        public class ImproperCalendarEvent
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public bool isreminder { get; set; }
            public string start { get; set; }
            public string end { get; set; }
            public bool allDay { get; set; }
            public string color { get; set; }
            public string url { get; set; }
            public string createdDate { get; set; }
            public string modifiedDate { get; set; }
            public string contactbuilder { get; set; }
            public string telephonebuilder { get; set; }
            public string addressbuilder { get; set; }
            public int companyId { get; set; }
            public bool IsQuoteEvent { get; set; }
            public string QuoteId { get; set; }
        }


        string[] colorArrays = { "#0066ff", "#339966", "#669900", "#ff33cc", "#ff9900", "#33cc33", "#e600e6", "#0073e6" };

    }
}