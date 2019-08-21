using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class donotexistcompanyallocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var jsonString = String.Empty;

            Request.InputStream.Position = 0;
            using (var inputStream = new StreamReader(Request.InputStream))
            {
                jsonString = inputStream.ReadToEnd();
            }

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var serJsonDetails = (RepCompanyAllocationSec)javaScriptSerializer.Deserialize(jsonString, typeof(RepCompanyAllocationSec));
            DoAllocation(serJsonDetails);
            Response.Headers.Add("Content-type", "application/json");
            Response.Write("1");
        }

        private void AddCompanies(List<DeltoneCRM_DAL.CompanyDAL.CompanyView> objLst,
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
                        comDal.InsertLeadNotExist(newAccountQuote.CompanyID, repId, currentDate, allocTime);
                    }

                }

            }


        }

        protected void DoAllocation(RepCompanyAllocationSec obj)
        {
            var listFilterByMode = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            var numberAllocationAccounts = obj.NumberOfAllocationCom;
            if (obj.AllocationMode == 1)
            {
                if (obj.IsFilterApplied)
                {
                    var listComByOwner = LoadCompanyForAllocation(obj.CompanyOwnerRepName,
                         obj.StartDate, obj.EndDate);
                    listFilterByMode = listComByOwner;
                    //numberAllocationAccounts = numberAllocationAccounts;
                }
                else
                {
                    obj.StartDate = "";
                    obj.EndDate = "";
                    var listComByOwner = LoadCompanyForAllocation(obj.CompanyOwnerRepName,
                        obj.StartDate, obj.EndDate);
                    listFilterByMode = listComByOwner;
                }
            }
            else
                if (obj.AllocationMode == 2)
                {
                    obj.StartDate = "";
                    obj.EndDate = "";
                    var listComByOwner = LoadCompanyForAllocation(obj.CompanyOwnerRepName,
                   obj.StartDate, obj.EndDate);
                    var listComIds = obj.SelectedCompanies;

                    foreach (var item in listComIds)
                    {
                        var selecItem = (from seQu in listComByOwner where seQu.CompanyID == item.CompanyId select seQu).ToList();
                        if (selecItem.Count() > 0)
                            listFilterByMode.Add(selecItem[0]);
                    }
                    numberAllocationAccounts = listFilterByMode.Count().ToString();
                }
                else
                {
                    if (obj.IsFilterApplied)
                    {
                        var listComIds = obj.SelectedCompanies;
                        if (listComIds.Count() > 0)
                        {
                            var listComByOwner = LoadCompanyForAllocation(obj.CompanyOwnerRepName,
                  obj.StartDate, obj.EndDate);
                            foreach (var item in listComIds)
                            {
                                var selecItem = (from seQu in listComByOwner where seQu.CompanyID == item.CompanyId select seQu).ToList();
                                if (selecItem.Count() > 0)
                                    listFilterByMode.Add(selecItem[0]);
                            }

                            numberAllocationAccounts = listFilterByMode.Count().ToString();
                        }
                        else
                        {
                            var listComByOwner = LoadCompanyForAllocation(obj.CompanyOwnerRepName,
                          obj.StartDate, obj.EndDate);
                            listFilterByMode = listComByOwner;
                            numberAllocationAccounts = numberAllocationAccounts;
                        }
                    }
                }
            AddCompanies(listFilterByMode, numberAllocationAccounts,
                obj.AllocationPeriod.ToString(), Convert.ToInt32(obj.AllocateRepName));
        }

        private DeltoneCRM_DAL.CompanyDAL.CompanyView GetNewCompaySel(List<DeltoneCRM_DAL.CompanyDAL.CompanyView> coViewAll,
            int repId)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var comDal = new CompanyDAL(CONNSTRING);
            var listCompanylead = comDal.GetLeadCompaniesNotExist();

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
                //else
                //    if (listCompanylead.Any(x => x.CompanyId == pickedObj.CompanyID
                //   && x.UserId == repId))
                //    {
                //        return GetNewCompaySel(coViewAll, repId);
                //    }

                else
                {
                    return pickedObj;
                }
            }
            return null;

        }


        protected List<DeltoneCRM_DAL.CompanyDAL.CompanyView> LoadCompanyForAllocation(string rep, string startDate,
           string endDate)
        {
            List<DeltoneCRM_DAL.CompanyDAL.CompanyView> sampleComList = new List<DeltoneCRM_DAL.CompanyDAL.CompanyView>();
            var count = 0;
            //var searchQuer=searche+'%';
            var orderByString = "Order By OrderedDateTime desc";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].CompaniesNotExists CP
                             inner  join [dbo].ContactsNotExists CT on CP.CompanyID = CT.CompanyID  
							   inner join [dbo].Logins lc on lc.LoginID=CP.OwnershipAdminID 
                              OUTER APPLY 
    (SELECT Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
    ) AS MBG
                            WHERE CP.OwnershipAdminID = " + rep + "  and (CP.IsSupperAcount IS NULL OR CP.IsSupperAcount<>'1') " +
                           "  and (CP.Hold IS NULL OR CP.Hold<>'Y')  " + orderByString;

                    if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                    {
                        cmd.CommandText = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].CompaniesNotExists CP
                             inner  join [dbo].ContactsNotExists CT on CP.CompanyID = CT.CompanyID  
							   inner join [dbo].Logins lc on lc.LoginID=CP.OwnershipAdminID 
                              OUTER APPLY 
    (SELECT Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
    ) AS MBG
                            WHERE CP.OwnershipAdminID = " + rep + " and MBG.OrderedDateTime between @startDate and @endDate  and (CP.IsSupperAcount IS NULL OR CP.IsSupperAcount<>'1') " +
                       "  and (CP.Hold IS NULL OR CP.Hold<>'Y') " + orderByString; ;

                        var starDateTimeContver = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
                        var endDateTimeContver = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                        cmd.Parameters.AddWithValue("@startDate", starDateTimeContver);
                        cmd.Parameters.AddWithValue("@endDate", endDateTimeContver);
                    }

                    cmd.Connection = conn;
                    conn.Open();
                    DataTable comTabldata = new DataTable();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        comTabldata.Load(sdr);

                        var query = comTabldata.AsEnumerable().ToList();
                        count = query.Count();
                        foreach (var row in query)
                        {
                            DateTime? renewalDate = row.Field<DateTime?>("OrderedDateTime");
                            var obj = new DeltoneCRM_DAL.CompanyDAL.CompanyView()
                            {
                                CompanyID = row.Field<int>("CompanyID"),
                                CompanyName = row.Field<string>("CompanyName"),
                            };

                            sampleComList.Add(obj);
                        }

                    }

                    conn.Close();

                }

            }


            return sampleComList;
        }

        private DateTime GetAllocationTime(string dropVal)
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
    }
}