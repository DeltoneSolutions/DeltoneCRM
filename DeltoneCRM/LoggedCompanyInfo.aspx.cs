using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class LoggedCompanyInfo : System.Web.UI.Page
    {
        
            CompanyDAL company = new CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
            public string SampleVariable = "";
            public static string UrlCompany = "http://delcrm/ConpanyInfo.aspx?companyid={0}";
            protected void Page_Load(object sender, EventArgs e)
            {
                if (Session["USERPROFILE"] == null)
                    return;

                Session["setinfo"] = "true";

                hdnCompany.Value = Request.QueryString["CompanyID"].ToString();
                LoadCompanyNotes(Request.QueryString["CompanyID"].ToString());
                theCompID.Value = Request.QueryString["CompanyID"].ToString();

                lbl_CompName.Text = company.getCompanyNameByID(Request.QueryString["CompanyID"].ToString());
                Session["companyId"] = theCompID.Value;
                LoadCompanyPaymentTerms(hdnCompany.Value);
                if (Session["USERPROFILE"].ToString() == "ADMIN")
                {
                    SampleVariable = "true";
                }

                var contactId = GetContacts(theCompID.Value);

                PopulateContact(contactId);

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

                                contactName.Text = contactBuilder;
                                ContactTelephone.Text = TeleBuilder;
                                contactPhone.Text = MobileBuilder;
                                address.Text = PostalAddressBuilder;
                                email.Text = sdr["Email"].ToString();


                            }
                        }

                        conn.Close();
                    }
                }

                return strContact;

            }

            protected void LoadCompanyPaymentTerms(String CID)
            {
                if (!IsPostBack)
                {
                    ddlPaymentTerms.ClearSelection();
                    ddlPaymentTerms.Items.FindByValue(company.getCompanyPaymentTerms(CID)).Selected = true;
                }


            }

            protected void LoadCompanyNotes(String CompanyID)
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                String strCompNotes = "SELECT Notes FROM dbo.Companies WHERE CompanyID = " + CompanyID;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strCompNotes;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                CompNotes.Text = sdr["Notes"].ToString();
                            }
                        }
                    }
                }
                conn.Close();
            }

            protected void btn_SaveSettings_Click(object sender, EventArgs e)
            {
                String DPT = ddlPaymentTerms.SelectedValue;
                var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                company.saveCompanyPaymentTerms(hdnCompany.Value, DPT, loggedInUserId);
            }

            //over mutliple days
            [System.Web.Services.WebMethod]
            public static int addEvent(DeltoneCRM.DataHandlers.DisplayEventHandler.ImproperCalendarEvent improperEvent)
            {
                string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                var startDate = Convert.ToDateTime(improperEvent.start);
                //   var endateObj = Convert.ToDateTime(improperEvent.end);
                var endDate = startDate.AddHours(1);
                //if (startDate != endateObj)
                // endDate = endateObj;


                var isAllDay = false;
                DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent cevent = new DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent()
                {
                    title = improperEvent.title,
                    description = improperEvent.description,
                    start = startDate,
                    end = endDate,
                    allDay = isAllDay,
                    color = improperEvent.color

                };

                var comId = 0;

                if (HttpContext.Current.Session["companyId"] != null)
                    comId = Convert.ToInt32(HttpContext.Current.Session["companyId"].ToString());

                cevent.url = string.Format(UrlCompany, comId);

                var userId = 0;
                if (HttpContext.Current.Session["LoggedUserID"] != null)
                    userId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
                int key = new CalendarEventDAL(cs).addEvent(cevent, comId, userId);

                //List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];

                //if (idList != null)
                //{
                //    idList.Add(key);
                //}


                var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = connectionstring;

                var columnName = "CalendarEvent all columns Order";
                var talbeName = "CalendarEvent";
                var ActionType = "Event Created";
                int primaryKey = key;
                var companyName = new DeltoneCRM_DAL.CompanyDAL(connectionstring).getCompanyNameByID(comId.ToString());
                var lastString = "Event Scheduled for company " + companyName + ": " + cevent.title;

                new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", lastString, userId, conn, 0,
       columnName, talbeName, ActionType, primaryKey, comId);


                return key; //return the primary key of the added cevent object




                return -1; //return a negative number just to signify nothing has been added
            }
        
    }
}