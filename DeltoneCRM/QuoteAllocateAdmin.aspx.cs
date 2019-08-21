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
    public partial class QuoteAllocateAdmin : System.Web.UI.Page
    {
        string CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //var ress=  GetLast6MonthQuote();

        }

        protected void btn_undoClickEvent(object sender, EventArgs e)
        {
            if (Session["recentAddedQuotes"] != null)
            {
                var listQuotes = Session["recentAddedQuotes"] as List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>;
                var qtDal = new QuoteDAL(CONNSTRING);
                var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
                if (repId > 0)
                {
                    qtDal.DeleteQuoteRecentAllocate(listQuotes, repId);
                    Session["recentAddedQuotes"] = null;
                    undoButton.Visible = false;
                    messagelable.Text = "Successfully Deleted.";
                }


            }
        }

        protected void btn_allocate_Click(object sender, EventArgs e)
        {
            var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
            if (repId > 0)
            {
                var listlast6Month = GetLast6MonthQuote();

                var filterNotByCreatedUserId = (from lis in listlast6Month where lis.QuotedById != repId select lis).ToList();
                AddQuotes(filterNotByCreatedUserId);
                messagelable.Text = "Successfully Allocated.";
                undoButton.Visible = true;

            }
            else
                messagelable.Text = "Please Select Rep";
        }

        private DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne GetNewRecentQuote(List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> coViewAll, int repId)
        {
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

        private void AddQuotes(List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> objLst)
        {
            var repId = Convert.ToInt32(RepNameDropDownList.SelectedValue);
            var listRecentAddedQuotes = new List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>();
            if (repId > 0)
            {
                var numberAccount = Convert.ToInt32(NumberAccountDropDownList.SelectedValue);
                var allocTime = GetAllocationTime();
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
                Session["recentAddedQuotes"] = listRecentAddedQuotes;
            }
            else
                messagelable.Text = "Please Select Rep";

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



        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            Response.Redirect("Manage\\ManageCentral.aspx");
        }


        private List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne> GetLast6MonthQuote()
        {
            var comList = new List<DeltoneCRM_DAL.QuoteDAL.QuoteAssignOne>();

            
            var dateLastMonth = DateTime.Now.AddMonths(-6).ToString("yyyy-MM-dd");
            var qtCat = 0;//status new
            //            var query = @"SELECT QuoteID,QuoteByID,QuoteDateTime,QuoteBy,Total,Status,Flag FROM QUOTE 
            //                              where convert(varchar(10),QuoteDateTime, 120) >@last6month and  QuoteCategory=@qtCat ";

            var query = @"SELECT QT.QuoteID,QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,CT.ContactID,'Existing Customer' as Type," +
                    " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                    "dbo.Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='LiveDB' and QT.Status<>'CANCELLED'  and  convert(varchar(10),QuoteDateTime, 120) >@last6month and  QuoteCategory=@qtCat " +
                     " Union SELECT QT.QuoteID, QT.QuoteByID, QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID , CT.ContactID,'New Customer' as Type," +
                    " CT.FirstName + ' ' + CT.LastName AS ContactName, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                    "dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID where QT.Flag='QuoteDB' and QT.Status<>'CANCELLED' and convert(varchar(10),QuoteDateTime, 120) >@last6month and  QuoteCategory=@qtCat ";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Parameters.AddWithValue("@last6month", dateLastMonth);
                    cmd.Parameters.AddWithValue("@qtCat", qtCat);
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




    }
}