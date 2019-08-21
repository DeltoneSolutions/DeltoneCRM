using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchSampleAllocateQuoteByRepTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int displayLength = int.Parse(Request["iDisplayLength"]);
            int displayStart = int.Parse(Request["iDisplayStart"]);
            int sortCol = int.Parse(Request["iSortCol_0"]);
            string sortDir = Request["sSortDir_0"];
            string search = Request["sSearch"];
            var rep = Request["rep"];
            var startdate = Request["startdate"];
            var enddate = Request["enddate"];
            var sortdir = Request["sortdir"];
            var sortColumn = Request["sortcol"];
            Response.Write(ReturnQuotes(rep, startdate, enddate, displayLength,
                displayStart, search, sortdir, sortColumn));
        }

        protected string ReturnQuotes(string rep, string startDate,
            string endDate, int length, int startLen, string searche, string sortdir, string sortColun)
        {

            var listCom = new List<string>();
            String strOutput = "{\"aaData\":[";
            var count = 0;
            var orderByString = "Order By QuoteDateTime desc";
            orderByString = "Order By " + sortColun + " " + sortdir;
            List<DataItem> sampleComList = new List<DataItem>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                           "  CT.FirstName + ' ' + CT.LastName AS ContactName,CT.Email, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                           " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID " +

                      "   LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) > CAST(getdate() as date) "

                          + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB' and QT.QuoteCategory<>3 AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email ) "

                            + "    " + orderByString;
                    if (!string.IsNullOrEmpty(rep))
                    {
                        var createdBy = rep;
                        if (Convert.ToInt32(rep) > 0)
                        {
                            querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                          "  CT.FirstName + ' ' + CT.LastName AS ContactName, CT.Email,QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                          " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) > CAST(getdate() as date)"

                         + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB' and  QT.QuoteByID=@createdBy and QT.QuoteCategory<>3 AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email ) "

                          + "    " + orderByString;
                            cmd.Parameters.AddWithValue("@createdBy", createdBy);

                            if (!string.IsNullOrEmpty(searche))
                            {
                                querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                        "  CT.FirstName + ' ' + CT.LastName AS ContactName, CT.Email,QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                        " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) > CAST(getdate() as date)"

                       + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB' and CO.CompanyName like " + "'" + searche + "%'" + " and  QT.QuoteByID=@createdBy and QT.QuoteCategory<>3 AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email )"

                        + "    " + orderByString;
                            }

                            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                            {
                                querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                         "  CT.FirstName + ' ' + CT.LastName AS ContactName, CT.Email,QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                         " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) > CAST(getdate() as date)"

                        + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB' and  QT.QuoteCategory<>3 and  QT.QuoteByID=@createdBy and QT.QuoteDateTime between @startDate and @endDate AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email ) "

                         + "    " + orderByString;

                                var starDateTimeContver = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
                                var endDateTimeContver = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                                cmd.Parameters.AddWithValue("@startDate", starDateTimeContver);
                                cmd.Parameters.AddWithValue("@endDate", endDateTimeContver);

                            }

                        }
                        else
                        {
                            querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                               "  CT.FirstName + ' ' + CT.LastName AS ContactName,CT.Email, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                               " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) > CAST(getdate() as date) "

                              + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB'  and QT.QuoteCategory<>3 AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email ) "

                               + "    " + orderByString;

                            if (!string.IsNullOrEmpty(searche))
                            {
                                querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                                  "  CT.FirstName + ' ' + CT.LastName AS ContactName,CT.Email, QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                                  " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) > CAST(getdate() as date) "

                                 + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB'  and QT.QuoteCategory<>3 and CO.CompanyName like " + "'" + searche + "%'"
                                  + " AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email )   " + orderByString;
                            }



                            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                            {
                                querystring = @"SELECT QT.CompanyID, QT.QuoteID, alQt.QuoteID as linkedtableComId,ls.FirstName + ' ' + ls.LastName as repAssignedName,  QT.QuoteDateTime as  QuoteDate, CO.CompanyName,CO.CompanyID ,QT.CallBackDate, CT.ContactID,'New Customer' as Type," +
                                  "  CT.FirstName + ' ' + CT.LastName AS ContactName, CT.Email,QT.Total, QT.Status, QT.QuoteBy FROM dbo.Quote QT INNER JOIN " +
                                  " dbo.Quote_Contacts CT ON QT.ContactID = CT.ContactID INNER JOIN dbo.Quote_Companies CO ON CO.CompanyID=QT.CompanyID LEFT JOIN QuoteAllocate alQt on alQt.QuoteId=QT.QuoteID AND convert(varchar(10),alQt.ExpiryDate, 120) > CAST(getdate() as date) "

                                 + " LEFT JOIN Logins ls ON ls.LoginID=alQt.UserId   where  QT.Flag='QuoteDB' and  QT.QuoteCategory<>3 and QT.QuoteDateTime between @startDate and @endDate  AND NOT EXISTS (SELECT * FROM   [Contacts] od WHERE  od.Email = CT.Email ) "

                                   + "    " + orderByString;

                                var starDateTimeContver = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
                                var endDateTimeContver = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                                cmd.Parameters.AddWithValue("@startDate", starDateTimeContver);
                                cmd.Parameters.AddWithValue("@endDate", endDateTimeContver);
                            }
                        }

                    }
                  


                    cmd.CommandText = querystring;
                    cmd.Connection = conn;
                    conn.Open();

                    DataTable comTabldata = new DataTable();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            comTabldata.Load(sdr);
                            var query = comTabldata.AsEnumerable().ToList();
                            count = query.Count();
                            var ress = query.Take(startLen + length).Skip(startLen);

                            foreach (var row in ress)
                            {
                                var obj = new DataItem()
                                {

                                    QUOTEID = row.Field<int>("QuoteID").ToString(),
                                    COMPANYNAME = row.Field<string>("CompanyName"),
                                    CONTACTNAME = row.Field<string>("ContactName"),
                                    QUOTEDBY = row.Field<string>("QuoteBy"),
                                    CUSTOMERTYPE = row.Field<string>("Type"),
                                    STATUS = row.Field<string>("Status"),
                                    CREATEDDATE = row.Field<DateTime>("QuoteDate").ToString("dd-MMM-yyyy"),

                                };

                                var renewalDate = row.Field<decimal?>("Total").ToString();

                                var total = String.Format("{0:C2}", float.Parse(renewalDate));
                                obj.QUOTETOTAL = total;


                                var Link = "<input name='selectchk'  value='" + obj.QUOTEID + "' type='checkbox' />";

                                var LinkLocked = "<input id='" + obj.QUOTEID + "' name='selectlock'  value='" + obj.QUOTEID + "' type='checkbox' />";

                                var linked = "N";
                                var rem = "";
                                rem = row.Field<string>("repAssignedName");
                                var LinID = row.Field<int?>("linkedtableComId");
                                if (LinID.GetValueOrDefault() > 0)
                                {
                                    linked = "Y";
                                }

                                var comID = "0";
                                var contactId = "0";
                                comID = row.Field<int>("CompanyID").ToString();
                                contactId = row.Field<int>("ContactID").ToString();

                                if (linked == "Y")
                                    LinkLocked = "<input name='selectlock' checked='true' value='" + obj.QUOTEID + "' type='checkbox' />";
                                String strCompanyView = "<img src='Images/Edit.png'  onclick='ViewQuote(" + obj.QUOTEID + ',' + comID + ',' + contactId + ")'/>";
                                obj.View = strCompanyView;
                                obj.ALLOCATEDREP = rem;
                                obj.Link = Link;
                                obj.ALLOCATED = LinkLocked;

                                sampleComList.Add(obj);

                            }
                        }

                    }

                    conn.Close();
                }

            }
            var totalRec = count;
            var result = new
            {
                iTotalRecords = totalRec,
                iTotalDisplayRecords = totalRec,
                aaData = sampleComList
            };

            JavaScriptSerializer js = new JavaScriptSerializer();
            var res = js.Serialize(result);
            return res;
        }

        public bool CanAdddCompany(string email)
        {
            var flag = true;


            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

            String strSqlContactStmt = "SELECT Email FROM dbo.Contacts WHERE Email = " + email;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        flag = false;
                        while (sdr.Read())
                        {
                            strContact = sdr["Email"].ToString();
                        }
                        return flag;
                    }
                }

                conn.Close();

            }

            return flag;
        }


        public class DataItem
        {
            public string View { get; set; }
            public string QUOTEID { get; set; }
            public string CREATEDDATE { get; set; }
            public string COMPANYNAME { get; set; }
            public string CONTACTNAME { get; set; }
            public string QUOTETOTAL { get; set; }
            public string QUOTEDBY { get; set; }
            public string CUSTOMERTYPE { get; set; }
            public string STATUS { get; set; }
            public string Locked { get; set; }
            public string ALLOCATED { get; set; }
            public string ALLOCATEDREP { get; set; }
            public string Select { get; set; }
            public string orderItems { get; set; }
            public string Link { get; set; }
            public string CompanyAllocationHistory { get; set; }
        }
    }
}