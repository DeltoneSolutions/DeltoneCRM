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
    public partial class LeadCompanyAdmin : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"] == null)
                return;

            //if (!IsPostBack)
            //    SetGridViewData();
        }

        protected void LoadCompanyByRep(object sender, EventArgs e)
        {

            SetGridViewData();
        }

        private void SetGridViewData()
        {
            var dropUserId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
            var dataList = LoadGridView(dropUserId);
            Session["sorList"] = dataList;
            GridView1LeadAdmin.DataSource = dataList;
            GridView1LeadAdmin.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1LeadAdmin.PageIndex = e.NewPageIndex;

            if (Session["sorList"] != null)
            {
                var sessionLit = Session["sorList"] as List<ComGrid>;
                GridView1LeadAdmin.DataSource = sessionLit;
                GridView1LeadAdmin.DataBind();
            }
            else
            {

                SetGridViewData();
            }
        }

        private List<ComGrid> LoadGridView(int userId)
        {
            var listCom = new List<ComGrid>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {


                    if (userId == 0)
                        cmd.CommandText = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,lo.CreatedDate,
                                  CP.CompanyID ,lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId   inner join Logins lc on lo.UserId=lc.LoginID and convert(varchar(10),lo.ExpiryDate, 120) >= CAST(getdate() as date) ";
                    else
                        cmd.CommandText = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,lo.CreatedDate,
                                  CP.CompanyID ,lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId   inner join Logins lc on lo.UserId=lc.LoginID AND lo.UserId = " + userId + " AND convert(varchar(10),lo.ExpiryDate, 120) >= CAST(getdate() as date) ";

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var obj = new ComGrid()
                                {
                                    CompanyID = Convert.ToInt32(sdr["CompanyID"].ToString()),
                                    CompanyName = sdr["CompanyName"].ToString(),
                                    ContactName = sdr["FullName"].ToString(),
                                    RepName = sdr["createdUser"].ToString(),
                                    CreateDate = Convert.ToDateTime(sdr["CreatedDate"].ToString())
                                };

                                listCom.Add(obj);

                            }

                        }

                    }



                }
                conn.Close();
            }
            listCom = listCom.OrderByDescending(x => x.CreateDate).ToList();
            return listCom;
        }

        protected void btn_allocate_Click(object sender, EventArgs e)
        {
            var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
            if (repId > 0)
            {
                var listCompanyHouse = GetCompanyBYHouseAccount();
                AddAccount(listCompanyHouse);
                messagelable.Text = "Successfully Allocated.";
                undoButton.Visible = true;
                // SetGridViewData();
            }
            else
                messagelable.Text = "Please Select Rep";

        }

        private void AddAccount(List<DeltoneCRM_DAL.CompanyDAL.CompanyView> coViewAll)
        {
            var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
            var listRecentAddedMoney = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            if (repId > 0)
            {
                var numberAccount = Convert.ToInt32(NumberAccountDropDownList.SelectedValue);
                var allocTime = GetAllocationTime();
                var currentDate = DateTime.Now;
                var comDal = new CompanyDAL(CONNSTRING);
                Random rnd = new Random();
                for (var i = 0; i < numberAccount; i++)
                {
                    var newHouseAccount = GetNewHouseCompany(coViewAll, repId);
                    if (newHouseAccount != null)
                    {
                        listRecentAddedMoney.Add(newHouseAccount);
                        comDal.InsertLead(newHouseAccount.CompanyID, repId, currentDate, allocTime);
                    }

                }
                Session["recentAddedCompany"] = listRecentAddedMoney;
            }
            else
                messagelable.Text = "Please Select Rep";

        }

        protected void btn_undoClickEvent(object sender, EventArgs e)
        {
            if (Session["recentAddedCompany"] != null)
            {
                var recentCompanies = Session["recentAddedCompany"] as List<DeltoneCRM_DAL.CompanyDAL.CompanyView>;
                var comDal = new CompanyDAL(CONNSTRING);
                var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
                if (repId > 0)
                {
                    comDal.DeleteLeadRecentCompanies(recentCompanies, repId);
                    Session["recentAddedCompany"] = null;
                    undoButton.Visible = false;
                    messagelable.Text = "Successfully Deleted.";
                }
            }
        }

        private DeltoneCRM_DAL.CompanyDAL.CompanyView GetNewHouseCompany(List<DeltoneCRM_DAL.CompanyDAL.CompanyView> coViewAll,int repId)
        {
            var comDal = new CompanyDAL(CONNSTRING);
            var listCompanylead = comDal.GetLeadCompanies();
            Random rnd = new Random();
            var currentDate = DateTime.Now;
            var pickedCom = coViewAll.OrderBy(x => rnd.Next()).Take(1).ToList();
            if (pickedCom.Count > 0)
            {
                var pickedObj = pickedCom[0];
                if (listCompanylead.Any(x => x.CompanyId == pickedObj.CompanyID))
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


        private DateTime GetAllocationTime()
        {
            var allocatePeriod = AllocationDropDownList.SelectedValue;
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


    }
}