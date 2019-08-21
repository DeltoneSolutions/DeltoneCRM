using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class Calendar : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        [WebMethod(enableSession: true)]  
        public static string GetCalendarEvents()
        {
            var listEvents = new List<DiaryEvent>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    var querystring = "";

                    if (System.Web.HttpContext.Current.Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        querystring = "SELECT distinct( QT.QuoteID) , CO.CompanyName, QT.QuoteBy , QT.CallBackDate " +
                        "  FROM dbo.Quote QT INNER JOIN " +
                        "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB' and QT.QuoteCategory=1 " +
                         " Union SELECT distinct(QT.QuoteID),  CO.CompanyName, QT.QuoteBy , QT.CallBackDate " +
                         " FROM dbo.Quote QT  " +
                        " INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and QT.QuoteCategory=1 ";

                    }
                    else
                    {
                        var createdBy = System.Web.HttpContext.Current.Session["LoggedUser"].ToString();
                        querystring = "SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName FROM dbo.Quote QT INNER JOIN " +
                    "  QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                    "  dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.QuoteBy=@createdBy And QT.QuoteCategory=" + 1;
                        cmd.Parameters.AddWithValue("@createdBy", createdBy);

                    }

                    cmd.CommandText = querystring;

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var events = new DiaryEvent();
                                events.start = sdr["CallBackDate"].ToString();
                                events.id = Convert.ToInt32(sdr["QuoteID"].ToString());
                                events.title = sdr["CompanyName"].ToString();
                                listEvents.Add(events);
                            }
                        }
                    }
                    conn.Close();
                }

            }
            return JsonConvert.SerializeObject(listEvents); ;
        }




        public class DiaryEvent
        {
            public int id;
            public string title;
            public string start;
            public string end;
            public string color;
            public string allDay;
        }
    }
}