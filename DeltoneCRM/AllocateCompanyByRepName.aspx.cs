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
    public partial class AllocateCompanyByRepName : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                NumberAccountDropDownList.DataSource = subjects;
                NumberAccountDropDownList.DataTextField = "FullName";
                NumberAccountDropDownList.DataValueField = "LoginID";
                NumberAccountDropDownList.DataBind();
            }

            //Get the Current Account OwnerShip


        }

        protected void btn_allocate_Click(object sender, EventArgs e)
        {
        }
        protected void btn_undoClickEvent(object sender, EventArgs e)
        {
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            Response.Redirect("Manage\\ManageCentral.aspx");
        }

        [System.Web.Services.WebMethod]
        public static void UpdateAllocateCompany(List<CompanyAlloSelect> coms, string mode, string companyOwnerId,
            string allocaRepId, string alloPeriod, string numberOfQuotes)
        {
            var listQuotes = DoCompanyAllocation(coms, mode, companyOwnerId, allocaRepId, alloPeriod, numberOfQuotes);



            if (mode == "2" )
            {
                if (coms != null)
                {
                    numberOfQuotes = listQuotes.Count().ToString();
                    var alloRepId = Convert.ToInt32(allocaRepId);
                    AddCompanies(listQuotes, numberOfQuotes, alloPeriod, alloRepId);
                }
            }
            else
                if (mode == "3")
                {
                    if (coms != null)
                    {
                        
                        var alloRepId = Convert.ToInt32(allocaRepId);
                        AddCompanies(listQuotes, numberOfQuotes, alloPeriod, alloRepId);
                    }

                }

                else
                {
                    var alloRepId = Convert.ToInt32(allocaRepId);
                    AddCompanies(listQuotes, numberOfQuotes, alloPeriod, alloRepId);
                }

        }

        private static DateTime GetAllocationTime(string dropVal)
        {
            var allocatePeriod = dropVal;
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


        private static void AddCompanies(List<DeltoneCRM_DAL.CompanyDAL.CompanyView> objLst,
            string numberOfAccount, string allocationPeriod, int RepId)
        {
            var repId = RepId;
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var listRecentAddedQuotes = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            if (repId > 0)
            {
                var numberAccount = Convert.ToInt32(numberOfAccount);
                var allocTime = GetAllocationTime(allocationPeriod);
                var currentDate = DateTime.Now;
                var comDal = new CompanyDAL(CONNSTRING);
                Random rnd = new Random();
                for (var i = 0; i < numberAccount; i++)
                {
                    var newAccountQuote = GetNewCompaySel(objLst, repId);
                    if (newAccountQuote != null)
                    {
                        listRecentAddedQuotes.Add(newAccountQuote);
                        comDal.InsertLead(newAccountQuote.CompanyID, repId, currentDate, allocTime);
                    }

                }

            }


        }


        private static List<DeltoneCRM_DAL.CompanyDAL.CompanyView> DoCompanyAllocation(List<CompanyAlloSelect> coms, string mode,
            string repOwnerId, string allocaRepId,
          string alloPeriod, string numberOfQuotes)
        {
            var listrepCompanies = GetCompaniesBYRep(repOwnerId);
            var listFilterByMode = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            if (mode == "3")
            {
                var numerOfComAllocations = Convert.ToInt32(numberOfQuotes);
                if (numerOfComAllocations > coms.Count())
                    numerOfComAllocations = coms.Count();

                //coms = ListOfRandomCompanies(coms, numerOfComAllocations);
                foreach (var item in coms)
                {
                    var selecItem = (from seQu in listrepCompanies where seQu.CompanyID == item.CompanyId select seQu).SingleOrDefault();
                    if (selecItem != null)
                        listFilterByMode.Add(selecItem);

                }
            }
            else
                if (mode == "2")
                {
                    foreach (var item in coms)
                    {
                        var selecItem = (from seQu in listrepCompanies where seQu.CompanyID == item.CompanyId select seQu).SingleOrDefault();
                        if (selecItem != null)
                            listFilterByMode.Add(selecItem);

                    }
                }
                else
                {
                    listFilterByMode = listrepCompanies;
                }


            return listFilterByMode;
        }

        private static List<CompanyAlloSelect> ListOfRandomCompanies(List<CompanyAlloSelect> coms, int numberOfAccounts)
        {
            var result = new List<CompanyAlloSelect>();
            Random rnd = new Random();
            for (var count = 0; count < numberOfAccounts; count++)
            {
                var pickedCom = coms.OrderBy(x => rnd.Next()).Take(1).ToList();
                if (pickedCom.Count() > 0)
                {
                    result.Add(pickedCom[0]);
                }
            }

            return result;
        }

        private static DeltoneCRM_DAL.CompanyDAL.CompanyView GetNewCompaySel(List<DeltoneCRM_DAL.CompanyDAL.CompanyView> coViewAll, int repId)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var comDal = new CompanyDAL(CONNSTRING);
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
                    return GetNewCompaySel(coViewAll, repId);
                }
                else
                    if (listCompanylead.Any(x => x.CompanyId == pickedObj.CompanyID
                   && x.UserId == repId))
                    {
                        return GetNewCompaySel(coViewAll, repId);
                    }

                    else
                    {
                        return pickedObj;
                    }
            }
            return null;

        }

        private static List<DeltoneCRM_DAL.CompanyDAL.CompanyView> GetCompaniesBYRep(string repId)
        {
            var comList = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            //            var query = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
            //                                  CP.CompanyID ,lo.FirstName + ' ' + lo.LastName AS createdUser ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
            //                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
            //                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID where CP.OwnershipAdminID=7";
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var query = @"SELECT  CP.CompanyName, 
                                CP.CompanyID ,Hold FROM dbo.Companies CP
                               where CP.OwnershipAdminID=@ownerID and CP.Active='Y' and (Hold IS NULL OR Hold<>'Y') and (IsSupperAcount IS NULL OR IsSupperAcount<>'1')";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@ownerID", repId);
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


        public class CompanyAlloSelect
        {
            public int CompanyId { get; set; }
            public bool selected { get; set; }
        }

    }
}