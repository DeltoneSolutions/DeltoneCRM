using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchSampleAllocateNotExistCompanyByRepTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int displayLength = int.Parse(Request["iDisplayLength"]);
            int displayStart = int.Parse(Request["iDisplayStart"]);
            int sortCol = int.Parse(Request["iSortCol_0"]);
            string sortDir = Request["sSortDir_0"];
            var search = Request["sSearch"];
            var rep = Request["rep"];
            var startDate = Request["startdate"];
            var endDate = Request["enddate"];
            var sortdir = Request["sortdir"];
            var sortColumn = Request["sortcol"];
            Response.Write(ReturnNoExistsCompanies(rep, startDate, endDate, displayLength,
                displayStart, search, sortdir, sortColumn));
        }

        public string ReturnNoExistsCompanies(string rep, string startDate, string endDate, int length, int startLen, string searche, string sortdir, string sortColun)
        {
            List<DataItem> sampleComList = new List<DataItem>();
            var count = 0;


            var orderByString = "Order By OrderedDateTime desc";
            orderByString = "Order By " + sortColun + " " + sortdir;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                using (SqlCommand cmd = new SqlCommand())
                {
                    if (!string.IsNullOrEmpty(searche))
                    {
                        cmd.CommandText = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,CP.Hold,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].CompaniesNotExists CP
                             inner  join [dbo].ContactsNotExists CT on CP.CompanyID = CT.CompanyID  
							   inner join [dbo].Logins lc on lc.LoginID=CP.OwnershipAdminID 
                              OUTER APPLY 
    (SELECT Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].OrdersNotExists ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
    ) AS MBG
                            WHERE CP.OwnershipAdminID = " + rep + "and CP.CompanyName like " + "'" + searche + "%'" + "  " +
                    "    " + orderByString;

                    }

                    else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                    {
                        cmd.CommandText = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,CP.Hold,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].CompaniesNotExists CP
                             inner  join [dbo].ContactsNotExists CT on CP.CompanyID = CT.CompanyID  
							   inner join [dbo].Logins lc on lc.LoginID=CP.OwnershipAdminID 
                              OUTER APPLY 
    (SELECT Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].OrdersNotExists ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
    ) AS MBG
                            WHERE CP.OwnershipAdminID = " + rep + " and MBG.OrderedDateTime between @startDate and @endDate  " +
                          "   " + orderByString; ;

                        var starDateTimeContver = Convert.ToDateTime(startDate).ToString("yyyy-MM-dd");
                        var endDateTimeContver = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");
                        cmd.Parameters.AddWithValue("@startDate", starDateTimeContver);
                        cmd.Parameters.AddWithValue("@endDate", endDateTimeContver);
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,CP.Hold,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].CompaniesNotExists CP
                             inner  join [dbo].ContactsNotExists CT on CP.CompanyID = CT.CompanyID  
							   inner join [dbo].Logins lc on lc.LoginID=CP.OwnershipAdminID 
                              OUTER APPLY 
    (SELECT Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].OrdersNotExists ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
    ) AS MBG
                            WHERE CP.OwnershipAdminID = " + rep + " " +
                           "    " + orderByString; 
                    }


                    var canAdd = true;
                    cmd.Connection = conn;
                    conn.Open();
                    DataTable comTabldata = new DataTable();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        comTabldata.Load(sdr);
                        var orderString = "";
                        var userAllocated = "";
                        var linked = "N";

                        var leadNotes = "";
                        var query = comTabldata.AsEnumerable().ToList();
                        count = query.Count();
                        var ress = query.Take(startLen + length).Skip(startLen);
                        foreach (var row in ress)
                        {
                            var sold = "N";
                            DateTime? renewalDate = row.Field<DateTime?>("OrderedDateTime");
                             var soldss = row.Field<string>("Hold");
                             if (!string.IsNullOrEmpty(soldss))
                                 sold = soldss;
                            var LinkLocked = "<input id='" + row.Field<int>("CompanyID").ToString() + "' name='selectlock'  value='" + row.Field<int>("CompanyID").ToString() + "' type='checkbox' class='select_com_aa'/>";
                            var Link = "<input name='selectchk'  value='" + row.Field<int>("CompanyID").ToString() + "' type='checkbox'  class='select_comchk'/>";
                            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                            {

                                var obj = new DataItem()
                                {
                                    Active = row.Field<string>("Active"),

                                    CompayId = row.Field<int>("CompanyID").ToString(),
                                    CompayName = row.Field<string>("CompanyName"),
                                    Contact = row.Field<string>("FullName"),
                                    Notes = row.Field<string>("Notes"),
                                    Owner = row.Field<string>("createdUser")

                                };

                                String strCompanyView = "<img src='Images/Edit.png'  onclick='ViewCompany(" + obj.CompayId + ")'/>";
                                obj.View = strCompanyView;

                                userAllocated = GetAssignedUserId(obj.CompayId, out leadNotes);
                                obj.AllocatedRep = userAllocated;
                                if (!string.IsNullOrEmpty(userAllocated))
                                {

                                    obj.Allocated = "Y";
                                    LinkLocked = "<input name='selectlock' checked='true' value='" + obj.CompayId + "' type='checkbox' />";
                                }
                                var dexc = obj.Notes;

                                dexc = dexc + leadNotes;
                                dexc = Regex.Replace(dexc, @"\t|\n|\r", "");
                                dexc = dexc.Replace(@"\", "-");
                                dexc = dexc.Replace("\"", "");
                               
                                var fullNotes = dexc;
                                if (dexc.Length > 50)
                                    dexc = dexc.Substring(0, 50);
                                fullNotes = Regex.Replace(fullNotes, "<.*?>", String.Empty);
                                obj.Notes = dexc;
                                obj.FullNotes = fullNotes;
                                obj.Link = Link;
                                orderString = LoadAllOrderForCompany(obj.CompayId, startDate, endDate);
                                obj.orderItems = orderString;
                                obj.AllocatedRep = userAllocated;
                                obj.Locked = sold;
                                obj.CompanyAllocationHistory = GetCopanyAllocatedHistory(obj.CompayId);
                                if (renewalDate.HasValue)
                                    obj.LastOrderDate = renewalDate.GetValueOrDefault().ToString("dd-MMM-yyyy");
                                sampleComList.Add(obj);

                            }
                            else
                            {
                                var obj = new DataItem()
                                {
                                    Active = row.Field<string>("Active"),
                                    Allocated = linked,

                                    CompayId = row.Field<int>("CompanyID").ToString(),
                                    CompayName = row.Field<string>("CompanyName"),
                                    Contact = row.Field<string>("FullName"),
                                    Notes = row.Field<string>("Notes"),
                                    Owner = row.Field<string>("createdUser")

                                };

                                String strCompanyView = "<img src='Images/Edit.png'  onclick='ViewCompany(" + obj.CompayId + ")'/>";
                                obj.View = strCompanyView;

                                userAllocated = GetAssignedUserId(obj.CompayId, out leadNotes);
                                obj.AllocatedRep = userAllocated;
                                if (!string.IsNullOrEmpty(userAllocated))
                                {

                                    obj.Allocated = "Y";
                                    LinkLocked = "<input name='selectlock' checked='true' value='" + obj.CompayId + "' type='checkbox' />";
                                }
                                var dexc = obj.Notes;

                                dexc = dexc + leadNotes;
                                dexc = Regex.Replace(dexc, @"\t|\n|\r", "");
                                dexc = dexc.Replace(@"\", "-");
                                dexc = dexc.Replace("\"", "");
                               
                                var fullNotes = dexc;
                                if (dexc.Length > 50)
                                    dexc = dexc.Substring(0, 50);
                                fullNotes = Regex.Replace(fullNotes, "<.*?>", String.Empty);
                                obj.Notes = dexc;
                                obj.FullNotes = fullNotes;
                                obj.Link = Link;
                                orderString = LoadAllOrderForCompany(obj.CompayId, startDate, endDate);
                                obj.orderItems = orderString;
                                obj.AllocatedRep = userAllocated;
                                obj.Locked = sold;
                                obj.CompanyAllocationHistory = GetCopanyAllocatedHistory(obj.CompayId);
                                if (renewalDate.HasValue)
                                    obj.LastOrderDate = renewalDate.GetValueOrDefault().ToString("dd-MMM-yyyy");
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

        private string LoadAllOrderForCompany(string comId, string starDate, string endDate)
        {
            string resultString = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var query = @"Select OrderedDateTime,Total,OrderID from  [dbo].OrdersNotExists where companyId={0}  order by OrderedDateTime desc";

                    //                    if (!string.IsNullOrEmpty(starDate) && !string.IsNullOrEmpty(endDate))
                    //                    {
                    //                        query = @"Select OrderedDateTime,Total,XeroInvoiceNumber from  [dbo].orders where companyId={0} and 
                    //                             Status<>@status and (OrderedDateTime >=@startDa and OrderedDateTime <=@endtDa)  order by OrderedDateTime desc";
                    //                        var st = Convert.ToDateTime(starDate).ToString("yyyy-MM-dd");
                    //                        var end = Convert.ToDateTime(endDate).ToString("yyyy-MM-dd");

                    //                        cmd.Parameters.AddWithValue("@startDa", st);
                    //                        cmd.Parameters.AddWithValue("@endtDa", end);
                    //                    }

                    query = string.Format(query, comId);
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {


                                if (resultString == "")
                                    resultString = Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy") + "," + sdr["Total"] + "," + sdr["OrderID"];
                                else
                                {
                                    resultString = resultString + ";";
                                    resultString = resultString + Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy") + "," + sdr["Total"] + "," + sdr["OrderID"];
                                }
                            }

                        }

                    }

                    conn.Close();
                }

            }

            return resultString;
        }

        private string GetCopanyAllocatedHistory(string comId)
        {
            var returnResult = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @" SELECT LPC.UserId ,LPC.Notes,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName ,LPC.createddate,LPC.ExpiryDate,LPC.Notes
From [dbo].LeadCompanyNotExists LPC 
inner JOIN [dbo].Logins lcR ON lcR.LoginID=LPC.UserId
where LPC.CompanyId=@comId 
order by LPC.createddate desc";
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
                                if (returnResult == "")
                                    returnResult = sdr["AlloUserName"].ToString() + "," + Convert.ToDateTime(sdr["createddate"]).ToString("dd-MMM-yyyy")
                                        + "," + Convert.ToDateTime(sdr["ExpiryDate"]).ToString("dd-MMM-yyyy");
                                else
                                {
                                    returnResult = returnResult + ";";
                                    returnResult = returnResult + sdr["AlloUserName"].ToString() + "," + Convert.ToDateTime(sdr["createddate"]).ToString("dd-MMM-yyyy")
                                        + "," + Convert.ToDateTime(sdr["ExpiryDate"]).ToString("dd-MMM-yyyy");
                                }
                            }

                        }

                    }

                    conn.Close();

                }

            }
            return returnResult;

        }

        public string GetAssignedUserId(string comId, out string notes)
        {
            var userid = "";
            notes = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @" WITH CTE_LeadComNoexist (comId,maxrecId) as (SELECT  LPC.CompanyId, MAX(LPC.Id)  
FROM  LeadCompanyNotExists LPC 
where LPC.CompanyId=@comId And convert(varchar(10),LPC.ExpiryDate, 120) >= CAST(getdate() as date)
GROUP By LPC.CompanyID )

SELECT LPC.UserId ,LPC.Notes,lpcv.comId as companyID,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName 
From LeadCompanyNotExists LPC

inner join CTE_LeadComNoexist lpcv  on LPC.Id=lpcv.maxrecId
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
                            }

                        }

                    }

                    conn.Close();

                }

            }

            return userid;
        }
    }
}