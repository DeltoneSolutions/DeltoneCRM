using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;
using DeltoneCRM_DAL;
using System.Globalization;


namespace DeltoneCRM
{
    public partial class ConpanyInfo : System.Web.UI.Page
    {
        CompanyDAL company = new CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        public string SampleVariable = "";
        public static string UrlCompany = "http://delcrm/ConpanyInfo.aspx?companyid={0}";


        private void RedirectLoggedInfo(string comId)
        {
            var holdStats = GetStatusOfCompany(comId);
            if (!string.IsNullOrWhiteSpace(holdStats))
            {
                if (holdStats == "Y")
                {
                    Response.Redirect("LoggedCompanyInfo.aspx?CompanyID=" + comId);
                }
            }

        }

        private string GetStatusOfCompany(string comId)
        {
            var activeHold = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT Hold FROM dbo.Companies WHERE CompanyID =" + comId;


                    cmd.Connection = conn;
                    conn.Open();


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["Hold"] != DBNull.Value)
                                activeHold = sdr["Hold"].ToString();
                        }
                    }


                    conn.Close();
                }
            }

            return activeHold;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

            RedirectLoggedInfo(Request.QueryString["CompanyID"].ToString());

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

            if (Request.QueryString["lP"] != null)
            {
                backpageallocate.Visible = true;
            }

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
            if (string.IsNullOrEmpty(strContactID))
                return "";
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
                if (ddlPaymentTerms.Items.FindByValue(company.getCompanyPaymentTerms(CID)) != null)
                    ddlPaymentTerms.Items.FindByValue(company.getCompanyPaymentTerms(CID)).Selected = true;
            }


        }

        protected void LoadCompanyNotes(String CompanyID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strCompNotes = "SELECT CP.Notes ,lc.FirstName FROM dbo.Companies CP inner join Logins lc on lc.LoginID=CP.OwnershipAdminID  WHERE CP.CompanyID = " + CompanyID;
            var mainNotes = "";
            var firstName = "";
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
                            //  TextBoxDisplayNotes.Text
                            mainNotes = sdr["Notes"].ToString();
                            firstName = sdr["FirstName"].ToString();
                        }
                    }
                }
            }
            conn.Close();

            var leadnotes = "";
            var leadCreatedDate = "";
            leadnotes = GetLeadNotes(CompanyID);
            if (Session["USERPROFILE"].ToString() == "ADMIN")
            {
                var canAddName = false;
                if (!string.IsNullOrEmpty(mainNotes))
                    canAddName = true;
                else
                    if (!string.IsNullOrEmpty(leadnotes))
                        canAddName = true;
                mainNotes = mainNotes + leadnotes;


            }
            else
            {
                var pos = mainNotes.IndexOf("--");
                if (pos != -1)
                {
                    // mainNotes = mainNotes.Substring(pos +2);
                }

                mainNotes = mainNotes + leadnotes;
            }

            displayDiv.InnerHtml = mainNotes;
            TextBoxDisplayNotes.Text = mainNotes;
        }

        public string GetLeadNotes(string comId)
        {
            var notes = "";
            var userid = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @" SELECT LPC.UserId ,LPC.Notes,LPC.NotesCreatedDate,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName 
From LeadCompany LPC  inner JOIN Logins lcR ON lcR.LoginID=LPC.UserId Where LPC.CompanyId=@comId";

                    var comIdCov = Convert.ToInt32(comId);
                    cmd.Parameters.Add("@comId", SqlDbType.Int).Value = comIdCov;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var notesloc = sdr["Notes"].ToString();
                                if (notes == "")
                                {
                                    notes = notesloc;
                                    if (Session["USERPROFILE"].ToString() != "ADMIN")
                                    {
                                        userid = sdr["AlloUserName"].ToString();
                                        var pos = notesloc.IndexOf("--");
                                        if (pos != -1)
                                        {
                                            // notes = notesloc.Substring(pos +2);
                                        }


                                    }

                                }
                                else
                                {
                                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                                    {

                                        notes = notesloc;


                                    }
                                    else
                                    {
                                        var pos = notesloc.IndexOf("--");
                                        if (pos != -1)
                                        {
                                            // notesloc = notesloc.Substring(pos +2);
                                        }
                                        notes = notesloc;
                                    }

                                }

                            }

                        }

                    }

                    conn.Close();

                }

            }

            return notes;
        }

        public string GetAssignedUserId(string comId, out string notes, out string notecreated)
        {
            var userid = "";
            notes = "";
            notecreated = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @" WITH CTE_LeadCom (comId,maxrecId) as (SELECT  LPC.CompanyId, MAX(LPC.Id)  
FROM  LeadCompany LPC 
where LPC.CompanyId=@comId And convert(varchar(10),LPC.ExpiryDate, 120) >= CAST(getdate() as date)
GROUP By LPC.CompanyID )

SELECT LPC.UserId ,LPC.Notes,LPC.NotesCreatedDate,lpcv.comId as companyID,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName 
From LeadCompany LPC

inner join CTE_LeadCom lpcv  on LPC.Id=lpcv.maxrecId
inner JOIN Logins lcR ON lcR.LoginID=LPC.UserId";
                    var comIdCov = Convert.ToInt32(comId);
                    cmd.Parameters.Add("@comId", SqlDbType.Int).Value = comIdCov;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                //var comIdDD = sdr["userId"].ToString();
                                //  var CoName = sdr["companyID"].ToString();
                                if (sdr["AlloUserName"] != DBNull.Value)
                                    userid = sdr["AlloUserName"].ToString();
                                notes = sdr["Notes"].ToString();
                                notecreated = sdr["NotesCreatedDate"].ToString();
                            }

                        }

                    }

                    conn.Close();

                }

            }

            return userid;
        }

        protected void btn_SaveSettings_Click(object sender, EventArgs e)
        {
            String DPT = ddlPaymentTerms.SelectedValue;
            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            company.saveCompanyPaymentTerms(hdnCompany.Value, DPT, loggedInUserId);
        }

        protected void callAllocateClick(object sender, EventArgs e)
        {
            Response.Redirect("AllocateCompanyByRepName.aspx");
        }



        protected static ContactUpdate getExisitingCustomerContactDetails(String CustID)
        {
            var obj = new ContactUpdate();

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
                    cmd.CommandText = "SELECT CT.*, CP.CompanyName FROM dbo.Contacts CT, dbo.Companies CP WHERE CT.CompanyID = CP.CompanyID AND CP.CompanyID = " + CustID;
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
        public static void UpdateContactStats(string comID)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strSqlContactStmt = @"UPDATE Contacts SET Active=@acti, AlteredDateTime=CURRENT_TIMESTAMP 
                                                WHERE ContactID=@ContactID";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.Add("@acti", SqlDbType.VarChar).Value = 'N';
                    var cnId = Convert.ToInt32(comID);
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = cnId;

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static ContactUpdate ReadContact(string comID)
        {
            return getExisitingCustomerContactDetails(comID);
        }
        [System.Web.Services.WebMethod]
        public static void UpdateContact(ContactUpdate obj)
        {
            UpdateCom_Contacts(obj);
        }

        [System.Web.Services.WebMethod]
        public static void DeleteDummyOrder(int csId)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            //var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            // DeleteAuditRecord(csId);

            new OrderDAL(cs).DeleteFromDummyOrder(csId);


        }

        private static void UpdateCom_Contacts(ContactUpdate obj)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strSqlContactStmt = @"UPDATE Contacts SET FirstName=@FirstName, LastName=@LastName , AlteredBy=@AlteredBy ,
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

        [System.Web.Services.WebMethod]
        public static void UpdateCompany(List<LinkComSelect> coms, string comId)
        {
            if (coms != null)
            {
                UpdateCompanyList(coms, comId);
            }
        }

        private static void CreateObjectType(List<LinkComSelect> coms, string comId)
        {
            var listItme = new List<CreateSamObj>();
            foreach (var item in coms)
            {
                var obj = new CreateSamObj();
                obj.MainComId = Convert.ToInt32(comId);
                obj.IdsCom = new List<int>();
                obj.IdsCom.Add(item.companyId);
                listItme.Add(obj);
            }

            foreach (var itme in coms)
            {
                var obj = new CreateSamObj();
                obj.MainComId = Convert.ToInt32(comId);
            }

        }

        private static void UpdateCompanyList(List<LinkComSelect> coms, string comId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var sqlStr = @"INSERT INTO CompanyLinked(CompanyId,CompanyIdLinked,CreatedUserId,CreatedDate)
                          Values(@CompanyMainId,@companyIdLinked,@createdUserId,CURRENT_TIMESTAMP)  ";
            var comLocked = "";
            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            var listIds = new List<int>();
            foreach (var item in coms)
            {
                if (Convert.ToInt32(comId) != item.companyId)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sqlStr;
                    cmd.Connection = conn;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@CompanyMainId", comId);
                    cmd.Parameters.AddWithValue("@companyIdLinked", item.companyId);
                    cmd.Parameters.AddWithValue("@createdUserId", loggedInUserId);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    UpdateCompanyWithInCompany(item.companyId, Convert.ToInt32(comId));

                    var listids = GetCreatedCompanyLink(Convert.ToInt32(comId));
                    foreach (var ds in listids)
                    {
                        if (item.companyId != ds)
                        {
                            UpdateCompanyWithInCompany(item.companyId, ds);
                            UpdateCompanyWithInCompany(ds,item.companyId);
                        }

                    }
                }
            }


        }

        private static List<int> GetCreatedCompanyLink(int mainComId)
        {
            var ids = new List<int>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strSqlOrderStmt = "SELECT CompanyIdLinked FROM dbo.CompanyLinked WHERE CompanyId = " + mainComId;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["CompanyIdLinked"] != DBNull.Value)
                        {
                            var res = Convert.ToInt32(sdr["CompanyIdLinked"].ToString());
                            ids.Add(res);
                        }

                    }
                }
            }
            conn.Close();

            return ids;
        }

        private static void UpdateCompanyWithInCompany(int commainId, int linkedCompanyId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var sqlStr = @"INSERT INTO CompanyLinked(CompanyId,CompanyIdLinked,CreatedUserId,CreatedDate)
                          Values(@CompanyMainId,@companyIdLinked,@createdUserId,CURRENT_TIMESTAMP)  ";
            var comLocked = "";
            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sqlStr;
            cmd.Connection = conn;
            conn.Open();
            cmd.Parameters.AddWithValue("@CompanyMainId", commainId);
            cmd.Parameters.AddWithValue("@companyIdLinked", linkedCompanyId);
            cmd.Parameters.AddWithValue("@createdUserId", loggedInUserId);
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        private static void DeleteLinkedCompanies(int comId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            var strSqlContactStmt = @"DELETE FROM CompanyLinked WHERE CompanyId = @companyId  ";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@companyId", SqlDbType.Int).Value = comId;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public class ContactUpdate
        {
            public int ComId { get; set; }
            public string dbType { get; set; }
            public string CompanyName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string AreaCode { get; set; }
            public string DefaultNumber { get; set; }
            public string Address1 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostCode { get; set; }
        }

        public class LinkComSelect
        {
            public int companyId { get; set; }
            public bool selected { get; set; }
        }

        public class CreateSamObj
        {
            public int MainComId { get; set; }
            public IList<int> IdsCom { get; set; }
        }
    }
}