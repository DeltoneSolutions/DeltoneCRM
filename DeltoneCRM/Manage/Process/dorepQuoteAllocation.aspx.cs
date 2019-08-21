using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class dorepQuoteAllocation : System.Web.UI.Page
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

        private  void AddQuotes(List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> objLst, string numberOfAccount, string allocationPeriod, int RepId)
        {
            var repId = RepId;
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var listRecentAddedQuotes = new List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>();
            if (repId > 0)
            {
                var numberAccount = Convert.ToInt32(numberOfAccount);
                var allocTime = GetAllocationTime(allocationPeriod);
                var currentDate = DateTime.Now;
                var comDal = new QuoteDAL(CONNSTRING);
                Random rnd = new Random();
                for (var i = 0; i < numberAccount; i++)
                {
                    var newAccountQuote = GetNewRecentQuote(objLst, repId);
                    if (newAccountQuote != null)
                    {
                        listRecentAddedQuotes.Add(newAccountQuote);
                        comDal.InsertQuoteAllocate(newAccountQuote.QuoteId, repId, currentDate, allocTime);
                        GetQuoteCompaniesByQuoteId(objLst, newAccountQuote.CompanyId, repId, allocTime);
                    }
                    

                }

            }


        }

        private void GetQuoteCompaniesByQuoteId(List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> objLst,
            int companyId, int repId, DateTime allocTime)
        {
            var listQuotesByCOmpaies = new List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>();

            listQuotesByCOmpaies = (from lis in objLst where lis.CompanyId == companyId select lis).ToList();

            if (listQuotesByCOmpaies.Count() > 0)
            {
                string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                var currentDate = DateTime.Now;
                var comDal = new QuoteDAL(CONNSTRING);
                Random rnd = new Random();
                var listQuotesAllocate = comDal.GetAssignQuotes();
                for (var i = 0; i < listQuotesByCOmpaies.Count(); i++)
                {
                    if (listQuotesAllocate.Any(x => x.QuoteId == listQuotesByCOmpaies[i].QuoteId
                    && x.ExpiryDate.Date > currentDate.Date))
                    {
                        
                    }
                    else
                    {
                        comDal.InsertQuoteAllocate(listQuotesByCOmpaies[i].QuoteId, repId, currentDate, allocTime);
                    }
                }
            }

        
        }


        protected void DoAllocation(RepCompanyAllocationSec obj)
        {
            var listFilterByMode = new List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>();
            var numberAllocationAccounts = obj.NumberOfAllocationCom;
            if (obj.AllocationMode == 1)
            {
                obj.StartDate = "";
                obj.EndDate = "";
                var listComByOwner = GetRepQuote(obj.CompanyOwnerRepName,
                    obj.StartDate, obj.EndDate);
                listFilterByMode = listComByOwner;
            }
            else
                if (obj.AllocationMode == 2)
                {
                    obj.StartDate = "";
                    obj.EndDate = "";
                    var listComByOwner = GetRepQuote(obj.CompanyOwnerRepName,
                   obj.StartDate, obj.EndDate);
                    var listComIds = obj.SelectedCompanies;

                    foreach (var item in listComIds)
                    {
                        var selecItem = (from seQu in listComByOwner where seQu.QuoteId == item.CompanyId select seQu).ToList();
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
                            var listComByOwner = GetRepQuote(obj.CompanyOwnerRepName,
                  obj.StartDate, obj.EndDate);
                            foreach (var item in listComIds)
                            {
                                var selecItem = (from seQu in listComByOwner where seQu.QuoteId == item.CompanyId select seQu).ToList();
                                if (selecItem.Count() > 0)
                                    listFilterByMode.Add(selecItem[0]);
                            }

                            numberAllocationAccounts = listFilterByMode.Count().ToString();
                        }
                        else
                        {
                            var listComByOwner = GetRepQuote(obj.CompanyOwnerRepName,
                          obj.StartDate, obj.EndDate);
                            listFilterByMode = listComByOwner;
                            numberAllocationAccounts = listFilterByMode.Count().ToString();
                        }
                    }
                }
            AddQuotes(listFilterByMode, numberAllocationAccounts,
                obj.AllocationPeriod.ToString(), Convert.ToInt32(obj.AllocateRepName));
        }

        private DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne GetNewRecentQuote(List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> coViewAll,
            int repId)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var comDal = new QuoteDAL(CONNSTRING);
            var listQuotesAllocate = comDal.GetAssignQuotes();

            Random rnd = new Random();
            var currentDate = DateTime.Now;
            var pickedCom = coViewAll.OrderBy(x => rnd.Next()).Take(1).ToList();
            if (pickedCom.Count > 0)
            {
                var pickedObj = pickedCom[0];
                if (listQuotesAllocate.Any(x => x.QuoteId == pickedObj.QuoteId
                    && x.ExpiryDate.Date > currentDate.Date))
                {
                    return GetNewRecentQuote(coViewAll, repId);
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




        private static List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> GetRepQuote(string quoteOwnerId, string startDate,
           string endDate)
        {
            var comList = new List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>();
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    var query = @" SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate,QT.QuoteByID, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                          "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                          " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  and QT.QuoteCategory=0 and QT.Status<>'CANCELLED' AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email ) ";
                    if (Convert.ToInt32(quoteOwnerId) > 0)
                    {
                        query = @" SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate,QT.QuoteByID, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                              "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                              " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and  QT.QuoteByID=@createdBy and QT.QuoteCategory=0 and QT.Status<>'CANCELLED' AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email )";

                        if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                        {
                            query = @" SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate,QT.QuoteByID, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                             "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                             " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and  QT.QuoteByID=@createdBy and QT.QuoteDateTime between @startDate and @endDate QT.QuoteByID=@createdBy and QT.QuoteCategory=0 and QT.Status<>'CANCELLED' AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email )";


                            var starDateTimeContver = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
                            var endDateTimeContver = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                            cmd.Parameters.AddWithValue("@startDate", starDateTimeContver);
                            cmd.Parameters.AddWithValue("@endDate", endDateTimeContver);
                        }

                        cmd.Parameters.AddWithValue("@createdBy", quoteOwnerId);

                    }
                    else
                    {

                        query = @" SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate,QT.QuoteByID, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                              "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                              " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  and QT.QuoteCategory=0 and QT.Status<>'CANCELLED' AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email )";

                        if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                        {
                            query = @" SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate,QT.QuoteByID, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                             "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                             " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB'  and QT.QuoteDateTime between @startDate and @endDate and QT.QuoteCategory=0 and QT.Status<>'CANCELLED' AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email )";

                            var starDateTimeContver = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
                            var endDateTimeContver = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                            cmd.Parameters.AddWithValue("@startDate", starDateTimeContver);
                            cmd.Parameters.AddWithValue("@endDate", endDateTimeContver);
                        }
                    }

                   
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var sbj = new DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne();
                                sbj.QuoteId = Convert.ToInt32(sdr["QuoteID"].ToString());
                                sbj.QuotedById = Convert.ToInt32(sdr["QuoteByID"].ToString());
                                sbj.QuotedDate = sdr["QuoteDate"].ToString();
                                sbj.QuotedByName = sdr["QuoteBy"].ToString();
                                sbj.QuoteTotal = sdr["Total"].ToString();
                                sbj.CompanyName = sdr["CompanyName"].ToString();
                                sbj.CompanyId = Convert.ToInt32(sdr["CompanyID"].ToString());
                                sbj.ContactName = sdr["ContactName"].ToString();
                                sbj.Status = sdr["Status"].ToString();
                                sbj.CustomerType = sdr["Type"].ToString();
                                comList.Add(sbj);

                            }
                        }
                    }

                }

            }


            return comList;

        }
    }
}