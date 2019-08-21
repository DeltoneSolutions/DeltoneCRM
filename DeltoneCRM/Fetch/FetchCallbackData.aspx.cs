using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchCallbackData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var mon = Request.QueryString["rep"].ToString();
            var yer = Request.QueryString["ye"].ToString();
            Response.Write(GetCAllBackEvents(mon, yer));
        }

        protected string GetCAllBackEvents(string monthtype,string year)
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {

                var connStr = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                var reminderList = new CalendarEventDAL(connStr).GetAllCAllBackEvents();
                reminderList = GetAllCurrentMonthValid(reminderList, monthtype,year);
                if (reminderList.Count() > 0)
                {
                    foreach (var item in reminderList)
                    {
                        if (item.companyId != "0")
                        {
                            var viewCom = "<img src='../Images/Edit.png' onclick='ViewCom(" + item.companyId + ")'/>";
                            var comId = item.companyId;
                            var fillCom = FillCompanyContactObj(comId);
                            var CoName = fillCom.CoName;
                            var contactName = fillCom.contactName;
                            var telephone = fillCom.telephone;
                            var mobile = fillCom.mobile;
                            var view = item.url;
                            var dexc = Regex.Replace(item.description.ToString(), @"\t|\n|\r", "");
                            dexc = dexc.Replace(@"\", "-");
                            dexc=dexc.Replace("\"", "");
                            var message = item.start.Date.ToShortDateString() + " --> " + dexc;
                            strOutput = strOutput + "[\"" + comId + "\"," + "\"" + CoName + "\","
                                   + "\"" + contactName + "\","
                                   + "\"" + telephone + "\","
                                    + "\"" + mobile + "\","
                                     + "\"" + message + "\","
                                   + "\"" + viewCom + "\"],";
                        }

                    }
                    int Length = strOutput.Length;
                    strOutput = strOutput.Substring(0, (Length - 1));

                }
                strOutput = strOutput + "]}";


            }
            return strOutput;
        }

        private List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent> GetAllCurrentMonthValid(List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent>
            listEvents, string monthType,string year)
        {
            var currentDate = DateTime.Now;
            if (monthType == "0")
            {
                return listEvents;
            }
            else
            {
                var mon = Convert.ToInt32(monthType);
                var yec = Convert.ToInt32(year);
                listEvents = (from eveli in listEvents
                              where eveli.start.Month == mon
                                  && eveli.start.Year == yec
                              select eveli).ToList();

            }

            return listEvents;
        }

        protected FillCompanyContact FillCompanyContactObj(string comId)
        {

            var obj = new FillCompanyContact();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " SELECT Contacts.*,Companies.CompanyName FROM Contacts join Companies on Contacts.CompanyID=Companies.CompanyID WHERE Companies.CompanyID=" + comId;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //String PostalAddressBuilder = sdr["STREET_AddressLine1"].ToString() +
                            //    " " +
                            //    sdr["STREET_City"]
                            //    + " "
                            //   + " " + sdr["STREET_Region"]
                            //   + " " + sdr["STREET_PostalCode"]
                            //   + " " + sdr["STREET_Country"];
                            // String PhysicalAddressBuilder = sdr["POSTAL_AddressLine1"].ToString() + sdr["POSTAL_City"] + sdr["POSTAL_Region"] + sdr["POSTAL_PostalCode"] + sdr["POSTAL_Country"];
                            string TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                            string MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                            //   string faxBuilder = sdr["FAX_AreaCode"].ToString() + sdr["FAX_CountryCode"] + sdr["FAX_Number"];
                            string contactBuilder = sdr["FirstName"].ToString() + " "
                                + sdr["LastName"];

                            obj.contactName = contactBuilder;
                            obj.telephone = TeleBuilder;
                            obj.mobile = MobileBuilder;
                            obj.CoName = sdr["CompanyName"].ToString();
                        }
                    }

                    conn.Close();
                }
            }

            return obj;
        }

        protected class FillCompanyContact
        {
            public string CoName { get; set; }
            public string contactName { get; set; }
            public string telephone { get; set; }
            public string mobile { get; set; }
        }
    }
}