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
    public partial class AssignRepQuotesAndReminders : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PopulateDropDownList();
        }

        protected void PopulateDropDownList()
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins WHERE Active = 'Y' and LoginID NOT IN ( 25, 19,18,23,26,8,21,22)", conn);
                adapter.Fill(subjects);
                RepNameDropDownList.AppendDataBoundItems = true;
                RepNameDropDownList.DataSource = subjects;
                RepNameDropDownList.DataTextField = "FullName";
                RepNameDropDownList.DataValueField = "LoginID";
                RepNameDropDownList.DataBind();
                NumberAccountDropDownList.DataSource = subjects;
                NumberAccountDropDownList.DataTextField = "FullName";
                NumberAccountDropDownList.DataValueField = "LoginID";
                NumberAccountDropDownList.DataBind();
            }
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

        [System.Web.Services.WebMethod]
        public static void UpdateQuote(List<QuoteSelect> coms, string mode, string quoteOwnerId,
            string allocaRepId, string alloPeriod,string numberOfQuotes)
        {
            var listQuotes=DoQuoteAllocation(coms,mode,quoteOwnerId,allocaRepId,alloPeriod,numberOfQuotes);

            if (mode == "2")
            {
                if (coms != null)
                {
                    numberOfQuotes = listQuotes.Count().ToString();
                    var alloRepId = Convert.ToInt32(allocaRepId);
                    AddQuotes(listQuotes, numberOfQuotes, alloPeriod, alloRepId);
                }
            }
            else
            {
                var alloRepId = Convert.ToInt32(allocaRepId);
                AddQuotes(listQuotes, numberOfQuotes, alloPeriod, alloRepId);
            }

        }

        private void InsertManualRecord(List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> objLst)
        {
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            foreach (var item in objLst)
            {

            }
        }

        private static void AddQuotes(List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> objLst,string numberOfAccount,string allocationPeriod,int RepId)
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
                    }

                }
                
            }
            

        }

        private static DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne GetNewRecentQuote(List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> coViewAll, int repId)
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
                else
                    if (listQuotesAllocate.Any(x => x.QuoteId == pickedObj.QuoteId
                   && x.UserId == repId))
                    {
                        return GetNewRecentQuote(coViewAll, repId);
                    }

                    else
                    {
                        return pickedObj;
                    }
            }
            return null;

        }

        private static List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> DoQuoteAllocation(List<QuoteSelect> coms, string mode, string quoteOwnerId, string allocaRepId, 
            string alloPeriod, string numberOfQuotes)
        {
            var listrepQuotes = GetRepQuote(quoteOwnerId);
            var listFilterByMode = new List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>();
            if (mode == "2")
            {
                foreach (var item in coms)
                {
                    var selecItem = (from seQu in listrepQuotes where seQu.QuoteId == item.QuoteId select seQu).SingleOrDefault();
                    listFilterByMode.Add(selecItem);

                }
            }
            else
            {
                listFilterByMode = listrepQuotes;
            }


            return listFilterByMode;
        }

        private static List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> GetRepQuote(string quoteOwnerId)
        {
            var comList = new List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>();
            string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;



            var query = @" SELECT QT.QuoteID, QT.QuoteDateTime as  QuoteDate,QT.QuoteByID, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                  "  CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                  " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and  QT.QuoteByID=@createdBy and QT.QuoteCategory=0 and QT.Status<>'CANCELLED' ";
           
            

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@createdBy", quoteOwnerId);
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

        public class QuoteSelect
        {
            public int QuoteId { get; set; }
            public bool selected { get; set; }
        }
    }
}