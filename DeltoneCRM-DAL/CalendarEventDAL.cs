using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
    public class CalendarEventDAL
    {
        private String CONNSTRING;

        public CalendarEventDAL(String connString)
        {
            CONNSTRING = connString;
        }


        public List<CalendarEvent> getEvents(int eveId)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT EventId, Description, Title, EventStart, CompanyId,CreatedDate,ModifiedDate,CreatedUserId,BgColor,Url,
                      EventEnd, AllDay , lo.FirstName + ' ' + lo.LastName AS createdUser FROM dbo.CalendarEvent 
                 ce inner join Logins lo on ce.CreatedUserId=lo.LoginID where ce.EventId=@eveId";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@eveId", SqlDbType.Int).Value = eveId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventCal = new CalendarEvent()
                    {
                        id = Convert.ToInt32(reader["EventId"]),
                        title = Convert.ToString(reader["Title"]),

                        start = Convert.ToDateTime(reader["EventStart"]),
                        end = Convert.ToDateTime(reader["EventEnd"]),
                        allDay = Convert.ToBoolean(reader["AllDay"]),
                        createdDate = reader["CreatedDate"].ToString(),
                        companyId = reader["CompanyId"].ToString(),
                    };
                    eventCal.modifiedDate = "";
                    if (reader["ModifiedDate"] != DBNull.Value)
                        eventCal.modifiedDate = reader["ModifiedDate"].ToString();

                    eventCal.description = Convert.ToString(reader["Description"]);
                    if (reader["BgColor"] != DBNull.Value)
                        eventCal.color = Convert.ToString(reader["BgColor"]);
                    if (reader["Url"] != DBNull.Value)
                        eventCal.url = Convert.ToString(reader["Url"]);
                    else
                        eventCal.url = "";
                    events.Add(eventCal);
                }
            }
            conn.Close();




            return events;

        }

        public List<CalendarEvent> getEventsByComID(int comId)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT EventId, Description, Title, EventStart, CompanyId,CreatedDate,ModifiedDate,CreatedUserId,BgColor,Url,
                      EventEnd, AllDay , lo.FirstName + ' ' + lo.LastName AS createdUser FROM dbo.CalendarEvent 
                 ce inner join Logins lo on ce.CreatedUserId=lo.LoginID where ce.CompanyId=@eveId";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@eveId", SqlDbType.Int).Value = comId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventCal = new CalendarEvent()
                    {
                        id = Convert.ToInt32(reader["EventId"]),
                        title = Convert.ToString(reader["Title"]),

                        start = Convert.ToDateTime(reader["EventStart"]),
                        end = Convert.ToDateTime(reader["EventEnd"]),
                        allDay = Convert.ToBoolean(reader["AllDay"]),
                        createdDate = reader["CreatedDate"].ToString(),
                        companyId = reader["CompanyId"].ToString(),
                    };
                    eventCal.modifiedDate = "";
                    if (reader["ModifiedDate"] != DBNull.Value)
                        eventCal.modifiedDate = reader["ModifiedDate"].ToString();

                    eventCal.description = Convert.ToString(reader["Description"]);
                    if (reader["BgColor"] != DBNull.Value)
                        eventCal.color = Convert.ToString(reader["BgColor"]);
                    if (reader["Url"] != DBNull.Value)
                        eventCal.url = Convert.ToString(reader["Url"]);
                    else
                        eventCal.url = "";
                    events.Add(eventCal);
                }
            }
            conn.Close();




            return events;

        }

        public List<CalendarEvent> GetAllCAllBackEvents()
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            var userId = 1;
           // var user2Id = 2;
            //var user3Id = 2;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT EventId, Description, Title, EventStart, CompanyId,CreatedDate,ModifiedDate,CreatedUserId,BgColor,Url,
                      EventEnd, AllDay , lo.FirstName + ' ' + lo.LastName AS createdUser FROM dbo.CalendarEvent 
                 ce inner join Logins lo on ce.CreatedUserId=lo.LoginID where ce.CreatedUserId=@userId and ce.IsLeadEvent=@leadeve ";
            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.AddWithValue("@leadeve", SqlDbType.Bit).Value = true;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventCal = new CalendarEvent()
                    {
                        id = Convert.ToInt32(reader["EventId"]),
                        title = Convert.ToString(reader["Title"]),

                        start = Convert.ToDateTime(reader["EventStart"]),
                        end = Convert.ToDateTime(reader["EventEnd"]),
                        allDay = Convert.ToBoolean(reader["AllDay"]),
                        createdDate = reader["CreatedDate"].ToString(),
                        companyId = reader["CompanyId"].ToString(),
                    };

                    eventCal.modifiedDate = "";
                    if (reader["ModifiedDate"] != DBNull.Value)
                        eventCal.modifiedDate = reader["ModifiedDate"].ToString();

                    eventCal.description = Convert.ToString(reader["Description"]);
                    if (reader["BgColor"] != DBNull.Value)
                        eventCal.color = Convert.ToString(reader["BgColor"]);
                    if (reader["Url"] != DBNull.Value)
                        eventCal.url = Convert.ToString(reader["Url"]);
                    else
                        eventCal.url = "";
                    events.Add(eventCal);
                }
            }
            conn.Close();




            return events;

        }

        public List<CalendarEvent> getReminderEvents(int userId)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT EventId, Description, Title, EventStart, CompanyId,CreatedDate,ModifiedDate,CreatedUserId,BgColor,Url,
                      EventEnd, AllDay , lo.FirstName + ' ' + lo.LastName AS createdUser FROM dbo.CalendarEvent 
                 ce inner join Logins lo on ce.CreatedUserId=lo.LoginID where ce.CreatedUserId=@userId and Reminder=@reminder";
            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.AddWithValue("@reminder", SqlDbType.Bit).Value = true;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventCal = new CalendarEvent()
                    {
                        id = Convert.ToInt32(reader["EventId"]),
                        title = Convert.ToString(reader["Title"]),

                        start = Convert.ToDateTime(reader["EventStart"]),
                        end = Convert.ToDateTime(reader["EventEnd"]),
                        allDay = Convert.ToBoolean(reader["AllDay"]),
                        createdDate = reader["CreatedDate"].ToString(),
                        companyId = reader["CompanyId"].ToString(),
                    };

                    eventCal.modifiedDate = "";
                    if (reader["ModifiedDate"] != DBNull.Value)
                        eventCal.modifiedDate = reader["ModifiedDate"].ToString();

                    eventCal.description = Convert.ToString(reader["Description"]);
                    if (reader["BgColor"] != DBNull.Value)
                        eventCal.color = Convert.ToString(reader["BgColor"]);
                    if (reader["Url"] != DBNull.Value)
                        eventCal.url = Convert.ToString(reader["Url"]);
                    else
                        eventCal.url = "";
                    events.Add(eventCal);
                }
            }
            conn.Close();




            return events;

        }

        public List<CalendarEvent> getSearchEvents(string searchString, int userId)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT EventId, Description, Title, EventStart, CompanyId,CreatedDate,ModifiedDate,CreatedUserId,BgColor,Url,Reminder,
                      EventEnd, AllDay , lo.FirstName + ' ' + lo.LastName AS createdUser FROM dbo.CalendarEvent 
                 ce inner join Logins lo on ce.CreatedUserId=lo.LoginID where ( Title LIKE '%" + searchString + "%' or Description LIKE '%" + searchString + "%' ) and ce.CreatedUserId=@userId";
            using (SqlCommand cmd = new SqlCommand())
            {


                cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = userId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventCal = new CalendarEvent()
                    {
                        id = Convert.ToInt32(reader["EventId"]),
                        title = Convert.ToString(reader["Title"]),

                        start = Convert.ToDateTime(reader["EventStart"]),
                        end = Convert.ToDateTime(reader["EventEnd"]),
                        allDay = Convert.ToBoolean(reader["AllDay"]),
                        createdDate = reader["CreatedDate"].ToString(),
                        companyId = reader["CompanyId"].ToString(),
                    };

                    var isnotify = false;
                    if (reader["Reminder"] != DBNull.Value)
                    {
                        isnotify = Convert.ToBoolean(reader["Reminder"]);

                    }

                    eventCal.isreminder = isnotify;

                    eventCal.modifiedDate = "";
                    if (reader["ModifiedDate"] != DBNull.Value)
                        eventCal.modifiedDate = reader["ModifiedDate"].ToString();

                    eventCal.description = Convert.ToString(reader["Description"]);
                    if (reader["BgColor"] != DBNull.Value)
                        eventCal.color = Convert.ToString(reader["BgColor"]);
                    if (reader["Url"] != DBNull.Value)
                        eventCal.url = Convert.ToString(reader["Url"]);
                    else
                        eventCal.url = "";
                    events.Add(eventCal);
                }
            }
            conn.Close();




            return events;

        }

        public List<CalendarEvent> getEvents(DateTime start, DateTime end, int userId = 0)
        {
            List<CalendarEvent> events = new List<CalendarEvent>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT EventId, Description, Title, EventStart,QuoteEvent ,CompanyId,CreatedDate,ModifiedDate,CreatedUserId,BgColor,Url,Reminder,
                      EventEnd, AllDay , lo.FirstName + ' ' + lo.LastName AS createdUser FROM dbo.CalendarEvent 
                 ce inner join Logins lo on ce.CreatedUserId=lo.LoginID where EventStart>=@start 
                                   AND EventEnd<=@end AND ce.CreatedUserId=@userId";
            using (SqlCommand cmd = new SqlCommand())
            {
                var now = DateTime.Now;
                if (start == DateTime.MinValue)
                    start = new DateTime(now.Year, now.Month, 1);
                if (end == DateTime.MinValue)
                    end = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month));

                cmd.Parameters.AddWithValue("@start", SqlDbType.DateTime).Value = start;
                cmd.Parameters.AddWithValue("@end", SqlDbType.DateTime).Value = end;
                cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = userId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var isnotify = false;
                    var isquoteEvent = false;
                    if (reader["Reminder"] != DBNull.Value)
                    {
                        isnotify = Convert.ToBoolean(reader["Reminder"]);

                    }
                    if (reader["QuoteEvent"] != DBNull.Value)
                    {
                        isquoteEvent = Convert.ToBoolean(reader["QuoteEvent"]);

                    }
                    var eventCal = new CalendarEvent()
                      {
                          id = Convert.ToInt32(reader["EventId"]),
                          title = Convert.ToString(reader["Title"]),

                          start = Convert.ToDateTime(reader["EventStart"]),
                          end = Convert.ToDateTime(reader["EventEnd"]),
                          allDay = Convert.ToBoolean(reader["AllDay"]),
                          createdDate = reader["CreatedDate"].ToString(),
                          companyId = reader["CompanyId"].ToString(),
                          isreminder = isnotify,
                          isquoteevent = isquoteEvent
                      };

                    eventCal.modifiedDate = "";
                    if (reader["ModifiedDate"] != DBNull.Value)
                        eventCal.modifiedDate = reader["ModifiedDate"].ToString();

                    eventCal.description = Convert.ToString(reader["Description"]);
                    if (reader["BgColor"] != DBNull.Value)
                        eventCal.color = Convert.ToString(reader["BgColor"]);
                    if (reader["Url"] != DBNull.Value)
                        eventCal.url = Convert.ToString(reader["Url"]);
                    else
                        eventCal.url = "";
                    events.Add(eventCal);
                }
            }
            conn.Close();
            //using (SqlCommand cmd = new SqlCommand())
            //{

            //    var querystring = "SELECT  QT.QuoteID , CO.CompanyName, QT.QuoteBy , QT.CallBackDate " +
            //                 "  FROM dbo.Quote QT INNER JOIN " +
            //                 "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB' and QT.QuoteCategory=1 " +
            //                  " Union SELECT QT.QuoteID,  CO.CompanyName, QT.QuoteBy , QT.CallBackDate " +
            //                  " FROM dbo.Quote QT  " +
            //                 " INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and QT.QuoteCategory=1 ";
            //    cmd.CommandText = querystring;

            //    cmd.Connection = conn;
            //    conn.Open();
            //    using (SqlDataReader sdr = cmd.ExecuteReader())
            //    {
            //        if (sdr.HasRows)
            //        {
            //            while (sdr.Read())
            //            {
            //                var eventCal = new CalendarEvent();
            //                if (sdr["CallBackDate"] != DBNull.Value)
            //                    eventCal.start = Convert.ToDateTime(sdr["CallBackDate"].ToString());
            //                eventCal.id = Convert.ToInt32(sdr["QuoteID"].ToString());
            //                eventCal.title = sdr["CompanyName"].ToString();
            //                eventCal.allDay = true;
            //                events.Add(eventCal);
            //            }
            //        }
            //    }
            //    conn.Close();
            //}



            return events;
            //side note: if you want to show events only related to particular users,
            //if user id of that user is stored in session as Session["userid"]
            //the event table also contains an extra field named 'user_id' to mark the event for that particular user
            //then you can modify the SQL as:
            //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
            //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
        }


        //this method updates the event title and description
        public void updateEvent(int id, String title, String description, string color, bool isreminderset)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE CalendarEvent SET Title=@title, Description=@description , 
                    BgColor=@BgColor, Reminder=@Reminder,ModifiedDate=CURRENT_TIMESTAMP WHERE EventId=@event_id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = description;
                cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@BgColor", SqlDbType.NVarChar).Value = color;
                cmd.Parameters.Add("@Reminder", SqlDbType.Bit).Value = isreminderset;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        //this method updates the event start and end time ... allDay parameter added for FullCalendar 2.x
        public void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE CalendarEvent SET EventStart=@event_start, EventEnd=@event_end, AllDay=@all_day, ModifiedDate=CURRENT_TIMESTAMP WHERE EventId=@event_id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = end;
                cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = allDay;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        //this mehtod deletes event with the id passed in.
        public void deleteEvent(int id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM CalendarEvent WHERE (EventId = @event_id)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void deleteEventQuote(int companyId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM CalendarEvent WHERE (CompanyId = @comId and QuoteEvent=@quoteyes)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@comId", SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("@quoteyes", SqlDbType.Bit).Value = true;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        //this method adds events to the database
        public int addEvent(CalendarEvent cevent, int companyId, int userId)
        {
            //add event to the database and return the primary key of the added event row

            //insert
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO CalendarEvent(Title, Description, EventStart, EventEnd, AllDay,CreatedUserId,CompanyId,BgColor,Url,CreatedDate,Reminder) 
                                     VALUES(@title, @description, @event_start, @event_end, @all_day,@userId,@CompanyId,@BgColor,@Url,CURRENT_TIMESTAMP,@Reminder)";


            int key = 0;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = cevent.title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = cevent.description;
                cmd.Parameters.Add("@BgColor", SqlDbType.NVarChar).Value = cevent.color;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;
                cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = cevent.url;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@Reminder", SqlDbType.Bit).Value = cevent.isreminder;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();



            }
            conn.Close();
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                //get primary key of inserted row
                var strcmd = @"SELECT max(EventId) FROM CalendarEvent where Title=@title AND Description=@description 
                AND EventStart=@event_start AND EventEnd=@event_end AND AllDay=@all_day AND CreatedUserId=@userId";
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.CommandText = strcmd;
                key = (int)cmd.ExecuteScalar();
            }
            conn.Close();
            return key;
        }



        public int addLeadEvent(CalendarEvent cevent, int companyId, int userId)
        {
            //add event to the database and return the primary key of the added event row

            //insert
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO CalendarEvent(Title, Description, EventStart, EventEnd, AllDay,CreatedUserId,CompanyId,BgColor,Url,CreatedDate,Reminder,IsLeadEvent) 
                                     VALUES(@title, @description, @event_start, @event_end, @all_day,@userId,@CompanyId,@BgColor,@Url,CURRENT_TIMESTAMP,@Reminder,@IsLeadEvent)";


            int key = 0;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = cevent.title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = cevent.description;
                cmd.Parameters.Add("@BgColor", SqlDbType.NVarChar).Value = cevent.color;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;
                cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = cevent.url;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@Reminder", SqlDbType.Bit).Value = cevent.isreminder;
                cmd.Parameters.Add("@IsLeadEvent", SqlDbType.Bit).Value = true;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();



            }
            conn.Close();
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                //get primary key of inserted row
                var strcmd = @"SELECT max(EventId) FROM CalendarEvent where Title=@title AND Description=@description 
                AND EventStart=@event_start AND EventEnd=@event_end AND AllDay=@all_day AND CreatedUserId=@userId";
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.CommandText = strcmd;
                key = (int)cmd.ExecuteScalar();
            }
            conn.Close();
            return key;
        }

        public int addQuoteEvent(CalendarEvent cevent, int companyId, int userId)
        {
            //add event to the database and return the primary key of the added event row

            //insert
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO CalendarEvent(Title, Description, EventStart, EventEnd, AllDay,CreatedUserId,CompanyId,
                                  BgColor,Url,CreatedDate,Reminder,IsLeadEvent,QuoteEvent) 
                                     VALUES(@title, @description, @event_start, @event_end, @all_day,@userId,@CompanyId,@BgColor
                                     ,@Url,CURRENT_TIMESTAMP,@Reminder,@IsLeadEvent,@quoteEvent)";


            int key = 0;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = cevent.title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = cevent.description;
                cmd.Parameters.Add("@BgColor", SqlDbType.NVarChar).Value = cevent.color;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;
                cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = cevent.url;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@Reminder", SqlDbType.Bit).Value = cevent.isreminder;
                cmd.Parameters.Add("@IsLeadEvent", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@quoteEvent", SqlDbType.Bit).Value = true;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();



            }
            conn.Close();
            using (SqlCommand cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                //get primary key of inserted row
                var strcmd = @"SELECT max(EventId) FROM CalendarEvent where Title=@title AND Description=@description 
                AND EventStart=@event_start AND EventEnd=@event_end AND AllDay=@all_day AND CreatedUserId=@userId";
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = cevent.title;
                cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = cevent.description;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;
                cmd.Parameters.Add("@userId", SqlDbType.Int).Value = userId;
                cmd.CommandText = strcmd;
                key = (int)cmd.ExecuteScalar();
            }
            conn.Close();
            return key;
        }

        public class CalendarEvent
        {
            public int id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public bool isreminder { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public bool allDay { get; set; }
            public string color { get; set; }
            public string url { get; set; }
            public string createdDate { get; set; }
            public string modifiedDate { get; set; }
            public string companyId { get; set; }
            public bool isquoteevent { get; set; }
        }

    }
}
