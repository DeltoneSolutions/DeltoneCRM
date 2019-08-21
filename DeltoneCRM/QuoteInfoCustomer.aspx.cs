using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class QuoteInfoCustomer : System.Web.UI.Page
    {
        CompanyDAL company = new CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        public string SampleVariable = "";
        public static string UrlCompany = "http://delcrm/ConpanyInfo.aspx?companyid={0}";


        private void RedirectLoggedInfo(string comId)
        {
            

        }

      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

            RedirectLoggedInfo(Request.QueryString["CompanyID"].ToString());

            hdnCompany.Value = Request.QueryString["CompanyID"].ToString();
            LoadCompanyNotes(Request.QueryString["CompanyID"].ToString());
            theCompID.Value = Request.QueryString["CompanyID"].ToString();

            lbl_CompName.Text = company.getQuoteCompanyNameByID(Request.QueryString["CompanyID"].ToString());
            Session["companyId"] = theCompID.Value;
            LoadCompanyPaymentTerms(hdnCompany.Value);
           

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
                    cmd.CommandText = "SELECT ContactID FROM dbo.Quote_Contacts WHERE CompanyID=" + strCompanyID;

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
            if (string.IsNullOrEmpty(strContactID))
                return "";
            String strContact = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " SELECT * FROM dbo.Quote_Contacts WHERE ContactID=" + strContactID;
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

        private static void UpdateCom_Contacts(DeltoneCRM.ViewQuoteCom.ContactUpdate obj)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strSqlContactStmt = @"UPDATE Quote_Contacts SET FirstName=@FirstName, LastName=@LastName , AlteredBy=@AlteredBy ,
                                              STREET_AddressLine1=@STREET_AddressLine1, STREET_City=@STREET_City,DEFAULT_Number=@DEFAULT_Number,DEFAULT_AreaCode=@DEFAULT_AreaCode ,MOBILE_Number=@Mobile_Number , Email=@Email ,
                                             STREET_PostalCode=@STREET_PostalCode, STREET_Region=@STREET_Region,AlteredDateTime=CURRENT_TIMESTAMP 
                                                WHERE CompanyID=@ContactID";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = obj.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = obj.LastName;
                    cmd.Parameters.Add("@AlteredBy", SqlDbType.NVarChar).Value = HttpContext.Current.Session["LoggedUser"].ToString();
                    cmd.Parameters.Add("@STREET_AddressLine1", SqlDbType.NVarChar).Value = obj.Address1;
                    cmd.Parameters.Add("@STREET_City", SqlDbType.NVarChar).Value = obj.City;
                    cmd.Parameters.Add("@STREET_PostalCode", SqlDbType.NVarChar).Value = obj.PostCode;
                    cmd.Parameters.Add("@STREET_Region", SqlDbType.NVarChar).Value = obj.State;
                    cmd.Parameters.Add("@Mobile_Number", SqlDbType.NVarChar).Value = obj.Phone;
                    cmd.Parameters.Add("@DEFAULT_AreaCode", SqlDbType.NVarChar).Value = obj.AreaCode;
                    cmd.Parameters.Add("@DEFAULT_Number", SqlDbType.NVarChar).Value = obj.DefaultNumber;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = obj.Email;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = obj.ComId;

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        protected void LoadCompanyPaymentTerms(String CID)
        {
            if (!IsPostBack)
            {
                ddlPaymentTerms.ClearSelection();
                if (ddlPaymentTerms.Items.FindByValue(company.getCompanyPaymentTerms(CID)) != null)
                    ddlPaymentTerms.Items.FindByValue(company.getCompanyPaymentTerms(CID)).Selected = true;
            }


        }

        [System.Web.Services.WebMethod]
        public static void UpdateContact(DeltoneCRM.ViewQuoteCom.ContactUpdate obj)
        {
            UpdateCom_Contacts(obj);
        }

        protected static DeltoneCRM.ViewQuoteCom.ContactUpdate getExisitingCustomerContactDetails(String CustID)
        {
            var obj = new DeltoneCRM.ViewQuoteCom.ContactUpdate();

            String strCompanyName = "";
            String CustomerName = "";

            String CustomerShipAddressLine1 = "";
            String CustomerShipCity = "";
            String CustomerShipPostcode = "";
            String CustomerShipState = "";

            String CustomerContactNumber = "";
            String CustomerEmail = "";


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CT.*, CP.CompanyName FROM dbo.Quote_Contacts CT, dbo.Quote_Companies CP WHERE CT.CompanyID = CP.CompanyID AND CP.CompanyID = " + CustID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strCompanyName = sdr["CompanyName"].ToString();
                            CustomerName = sdr["FirstName"].ToString();
                            var lastName = sdr["LastName"].ToString();


                            CustomerShipAddressLine1 = sdr["STREET_AddressLine1"].ToString();
                            CustomerShipCity = sdr["STREET_City"].ToString();
                            CustomerShipPostcode = sdr["STREET_PostalCode"].ToString();
                            CustomerShipState = sdr["STREET_Region"].ToString();

                            CustomerContactNumber = sdr["DEFAULT_AreaCode"].ToString() + ' ' + sdr["DEFAULT_Number"].ToString();
                            CustomerEmail = sdr["Email"].ToString();
                            obj.CompanyName = strCompanyName;
                            obj.ComId = Convert.ToInt32(CustID);
                            obj.Address1 = CustomerShipAddressLine1;
                            obj.City = CustomerShipCity;
                            obj.Email = CustomerEmail;
                            obj.FirstName = CustomerName;
                            obj.LastName = lastName;
                            obj.AreaCode = sdr["DEFAULT_AreaCode"].ToString();
                            obj.DefaultNumber = sdr["DEFAULT_Number"].ToString();
                            obj.Phone = sdr["MOBILE_Number"].ToString();
                            obj.PostCode = CustomerShipPostcode;
                            obj.State = CustomerShipState;

                        }
                    }
                }
            }

            return obj;

        }

        [System.Web.Services.WebMethod]
        public static DeltoneCRM.ViewQuoteCom.ContactUpdate ReadContact(string comID)
        {
            return getExisitingCustomerContactDetails(comID);
        }

        protected void LoadCompanyNotes(String CompanyID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strCompNotes = "SELECT Notes FROM dbo.Quote_Companies WHERE CompanyID = " + CompanyID;
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
                           
                            displayDiv.InnerHtml = sdr["Notes"].ToString();
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