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
    public partial class CallBackLeadCompanyList : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;
            if (!IsPostBack)
                PopulateDropDownList();

        }


        //Company OwnerShip details dropdown List Population
        protected void PopulateDropDownList()
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins WHERE Active = 'Y' and LoginID NOT IN ( 25, 19,18,23,26,8,21,22)", conn);
                adapter.Fill(subjects);
                RepNameDropDownList.DataSource = subjects;
                RepNameDropDownList.DataTextField = "FullName";
                RepNameDropDownList.DataValueField = "LoginID";
                RepNameDropDownList.DataBind();

            }

            //Get the Current Account OwnerShip


        }

        protected void btn_allocate_Click(object sender, EventArgs e)
        {
            var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
            if (repId > 0)
            {
              //  var listCompanyReminders = GetEventListCompany();
               // AddAccount(listCompanyReminders);
                messagelable.Text = "Successfully ReAllocated.";
                undoButton.Visible = true;
                // SetGridViewData();
            }
            else
                messagelable.Text = "Please Select Rep";

        }



        private void AddAccount(List<DeltoneCRM_DAL.CompanyDAL.CompanyView> coViewAll)
        {
            //var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
            //var listRecentAddedMoney = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            //if (repId > 0)
            //{
            //    var numberAccount = Convert.ToInt32(NumberAccountDropDownList.Text);
            //    if (coViewAll.Count() < numberAccount)
            //        numberAccount = coViewAll.Count();
            //    var allocTime = GetAllocationTime();
            //    var currentDate = DateTime.Now;
            //    var comDal = new CompanyDAL(CONNSTRING);
            //    Random rnd = new Random();
            //    for (var i = 0; i < numberAccount; i++)
            //    {
            //        var newHouseAccount = GetNewHouseCompany(coViewAll, repId);
            //        if (newHouseAccount != null)
            //        {
            //            listRecentAddedMoney.Add(newHouseAccount);
            //            comDal.InsertLead(newHouseAccount.CompanyID, repId, currentDate, allocTime);
            //        }

            //    }
            //    Session["recentAddedCompany"] = listRecentAddedMoney;
            //}
            //else
            //    messagelable.Text = "Please Select Rep";

        }

        protected void btn_undoClickEvent(object sender, EventArgs e)
        {
            if (Session["recentAddedCompany"] != null)
            {
                var recentCompanies = Session["recentAddedCompany"] as List<DeltoneCRM_DAL.CompanyDAL.CompanyView>;
                var comDal = new CompanyDAL(CONNSTRING);
                var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
                comDal.DeleteLeadRecentCompanies(recentCompanies, repId);
                Session["recentAddedCompany"] = null;
                undoButton.Visible = false;
                messagelable.Text = "Successfully Deleted.";
            }
        }

        private static DeltoneCRM_DAL.CompanyDAL.CompanyView GetNewHouseCompany(List<DeltoneCRM_DAL.CompanyDAL.CompanyView> coViewAll, int repId)
        {
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var comDal = new CompanyDAL(conn);
            var listCompanylead = comDal.GetLeadCompanies();
            Random rnd = new Random();
            var currentDate = DateTime.Now;
            var pickedCom = coViewAll.OrderBy(x => rnd.Next()).Take(1).ToList();
            if (pickedCom.Count > 0)
            {
                var pickedObj = pickedCom[0];
                if (listCompanylead.Any(x => x.CompanyId == pickedObj.CompanyID
                    && x.ExpiryDate.Date > currentDate.Date))
                {
                    return GetNewHouseCompany(coViewAll, repId);
                }
                else
                    if (listCompanylead.Any(x => x.CompanyId == pickedObj.CompanyID
                   && x.UserId == repId))
                    {
                        return GetNewHouseCompany(coViewAll, repId);
                    }

                    else
                    {
                        return pickedObj;
                    }
            }
            return null;

        }


        private static DateTime GetAllocationTime(string allocato)
        {
            var allocatePeriod = allocato;
            var dateTimeNow = DateTime.Now;
            if (allocatePeriod == "1")
                dateTimeNow = dateTimeNow.AddDays(7);
            else
                if (allocatePeriod == "2")
                    dateTimeNow = dateTimeNow.AddDays(14);
                else if (allocatePeriod == "3")
                    dateTimeNow = dateTimeNow.AddDays(21);
                else
                    dateTimeNow = dateTimeNow.AddDays(28);

            return dateTimeNow;
        }

        private static List<DeltoneCRM_DAL.CompanyDAL.CompanyView> GetEventListCompany(string month,string yearV)
        {
            var listCompany = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var reminderList = new CalendarEventDAL(conn).GetAllCAllBackEvents();
            var monthtype = month;
            var year = yearV;
            reminderList = GetAllCurrentMonthValid(reminderList, monthtype, year);
            if (reminderList.Count() > 0)
            {
                foreach (var item in reminderList)
                {
                    if (item.companyId != "0")
                    {
                        var comView = new DeltoneCRM_DAL.CompanyDAL.CompanyView();
                        comView.CompanyID = Convert.ToInt32(item.companyId);

                        listCompany.Add(comView);
                    }
                }
            }

            return listCompany;
        }

        private static List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent> GetAllCurrentMonthValid(List<DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent>
          listEvents, string monthType, string year)
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


        private List<DeltoneCRM_DAL.CompanyDAL.CompanyView> GetReALlocationCompany()
        {
            var comList = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            //            var query = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
            //                                  CP.CompanyID ,lo.FirstName + ' ' + lo.LastName AS createdUser ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
            //                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
            //                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID where CP.OwnershipAdminID=7";

            var query = @"SELECT Distinct CP.CompanyName, 
                                  CP.CompanyID  FROM dbo.Companies CP
                             
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId  inner join
                            CalendarEvent ce on lo.CompanyId=ce.CompanyId and ce.IsLeadEvent=1 and 
                           convert(varchar(10),ce.EventStart, 120) >= CAST(getdate() as date)  and 
                          convert(varchar(10),lo.ExpiryDate, 120) < CAST(getdate() as date) ";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var sbj = new DeltoneCRM_DAL.CompanyDAL.CompanyView();
                                sbj.CompanyID = Convert.ToInt32(sdr["CompanyID"].ToString());
                                sbj.CompanyName = sdr["CompanyName"].ToString();
                                comList.Add(sbj);

                            }
                        }
                    }

                }

            }


            return comList;
        }


        private List<DeltoneCRM_DAL.CompanyDAL.CompanyView> GetCompanyBYHouseAccount()
        {
            var comList = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            //            var query = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
            //                                  CP.CompanyID ,lo.FirstName + ' ' + lo.LastName AS createdUser ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
            //                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
            //                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID where CP.OwnershipAdminID=7";

            var query = @"SELECT  CP.CompanyName, 
                                CP.CompanyID ,Hold FROM dbo.Companies CP
                               where CP.OwnershipAdminID=7 and CP.Active='Y' and (Hold IS NULL OR Hold<>'Y') and (LeadLocked IS NULL OR LeadLocked<>'Y')";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var sbj = new DeltoneCRM_DAL.CompanyDAL.CompanyView();
                                sbj.CompanyID = Convert.ToInt32(sdr["CompanyID"].ToString());
                                sbj.CompanyName = sdr["CompanyName"].ToString();
                                comList.Add(sbj);

                            }
                        }
                    }

                }

            }


            return comList;
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            Response.Redirect("Manage\\ManageCentral.aspx");
        }

        public class ComGrid
        {
            public string CompanyName { get; set; }
            public int CompanyID { get; set; }
            public string ContactName { get; set; }
            public string RepName { get; set; }
            public DateTime CreateDate { get; set; }
        }

        private static void AllocateCompanyToRep(List<ComReAssign> coms, string mode, string numberofaccoutns,
            string allocationPeriod, string allocrepId,string month,string year)
        {
            var repId = Convert.ToInt32(allocrepId);
            var listRecentAddedMoney = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            if (repId > 0)
            {
                var alloceCompanies = DoFilterForCompany(coms, mode,month,year);
                var numberAccount = Convert.ToInt32(numberofaccoutns);
                if (alloceCompanies.Count() < numberAccount)
                    numberAccount = alloceCompanies.Count();
                var allocTime = GetAllocationTime(allocationPeriod);
                var currentDate = DateTime.Now;
                var connn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                var comDal = new CompanyDAL(connn);
             
                for (var i = 0; i < numberAccount; i++)
                {
                    var newHouseAccount = GetNewHouseCompany(alloceCompanies, repId);
                    if (newHouseAccount != null)
                    {
                        listRecentAddedMoney.Add(newHouseAccount);
                        comDal.InsertLead(newHouseAccount.CompanyID, repId, currentDate, allocTime);
                    }

                }
               // Session["recentAddedCompany"] = listRecentAddedMoney;
            }
        }

        private static List<DeltoneCRM_DAL.CompanyDAL.CompanyView> DoFilterForCompany(List<ComReAssign> coms, string mode,string month,string year)
        {
            var listCompanyReminders = GetEventListCompany(month, year);
            var listObj = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            if (mode == "2")
            {
                foreach (var item in coms)
                {
                    var obj = new DeltoneCRM_DAL.CompanyDAL.CompanyView();
                    obj.CompanyID = Convert.ToInt32(item.ComId);
                    listObj.Add(obj);
                }
            }
            else
                return listCompanyReminders;

            return listObj;

        }

        [System.Web.Services.WebMethod]
        public static void AllocateAccounts(List<ComReAssign> coms, string mode, string numberofaccoutns,
            string allocationPeriod, string repId, string month, string year)
        {
            if (mode == "1")
            {
                AllocateCompanyToRep(coms, mode, numberofaccoutns, allocationPeriod, repId, month, year);
            }
            else
            {
                if (mode == "2")
                {
                    AllocateCompanyToRep(coms, mode, numberofaccoutns, allocationPeriod, repId, month, year);
                }
            }
        }

        public class ComReAssign
        {
            public int ComId { get; set; }
            public bool selected { get; set; }
        }
    }
}