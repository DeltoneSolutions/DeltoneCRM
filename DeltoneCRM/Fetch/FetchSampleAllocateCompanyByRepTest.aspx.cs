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
    public partial class FetchSampleAllocateCompanyByRepTest : System.Web.UI.Page
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
            Response.Write(ReturnCompanies(rep, startdate, enddate, displayLength,
                displayStart, search, sortdir, sortColumn));
        }

        public string ReturnCompanies(string rep, string startDate,
            string endDate, int length, int startLen, string searche,string sortdir,string sortColun)
        {
            List<DataItem> sampleComList = new List<DataItem>();
            var count = 0;
            //var searchQuer=searche+'%';
            var orderByString = "Order By OrderedDateTime desc";
            orderByString = "Order By " + sortColun + " " + sortdir;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (rep == "0")
                        cmd.CommandText = @"SELECT Distinct  CP.CompanyID , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,LPC.CompanyId as linkedtableComId ,LPC.UserId,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName,
                                 lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID   inner join Logins lc on lc.LoginID=CP.OwnershipAdminID   
                                 LEFT JOIN LeadCompany LPC ON CP.CompanyID=LPC.CompanyId LEFT JOIN Logins 
                                        lcR ON lcR.LoginID=LPC.UserId and convert(varchar(10),LPC.ExpiryDate, 120) < CAST(getdate() as date) ORDER BY CP.CompanyName
                              ";
                    else
                    {
                        if (!string.IsNullOrEmpty(searche))
                        {
                            cmd.CommandText = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].Companies CP
                             OUTER APPLY (SELECT TOP 1 *
                    FROM   Contacts C
                    WHERE CP.CompanyID = C.CompanyID
                    ORDER  BY C.CompanyID ASC) CT
							   inner join [dbo].Logins lc on lc.LoginID=CP.OwnershipAdminID 
                              OUTER APPLY 
    (SELECT Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
    ) AS MBG
                            WHERE CP.OwnershipAdminID = " + rep + "and CP.CompanyName like " + "'" + searche + "%'" + " and (CP.IsSupperAcount IS NULL OR CP.IsSupperAcount<>'1') " +
                     "  and (CP.Hold IS NULL OR CP.Hold<>'Y')  " + orderByString;
                        }
                        else if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                        {
                            cmd.CommandText = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].Companies CP
                              OUTER APPLY (SELECT TOP 1 *
                    FROM   Contacts C
                    WHERE CP.CompanyID = C.CompanyID
                    ORDER  BY C.CompanyID ASC) CT  
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
                        else


                            cmd.CommandText = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].Companies CP
                              OUTER APPLY (SELECT TOP 1 *
                    FROM   Contacts C
                    WHERE CP.CompanyID = C.CompanyID
                    ORDER  BY C.CompanyID ASC) CT 
							   inner join [dbo].Logins lc on lc.LoginID=CP.OwnershipAdminID 
                              OUTER APPLY 
    (SELECT Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
    ) AS MBG
                            WHERE CP.OwnershipAdminID = " + rep + "  and (CP.IsSupperAcount IS NULL OR CP.IsSupperAcount<>'1') " +
                            "  and (CP.Hold IS NULL OR CP.Hold<>'Y')  " + orderByString; 


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
                            DateTime? renewalDate = row.Field<DateTime?>("OrderedDateTime");
                            
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
                                    userAllocated = GetAssignedRealUserByCOmId(obj.CompayId);
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
                                    obj.Notes = dexc;
                                    fullNotes = Regex.Replace(fullNotes, "<.*?>", String.Empty);
                                    obj.FullNotes = fullNotes;
                                    obj.Link = Link;
                                    orderString = LoadAllOrderForCompany(obj.CompayId, startDate, endDate);
                                    obj.orderItems = orderString;
                                   
                                  
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
                                userAllocated = GetAssignedRealUserByCOmId(obj.CompayId);
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
                                obj.Notes = dexc;
                                fullNotes = Regex.Replace(fullNotes, "<.*?>", String.Empty);
                                obj.FullNotes = fullNotes;
                                obj.Link = Link;
                                orderString = LoadAllOrderForCompany(obj.CompayId, startDate, endDate);
                                obj.orderItems = orderString;
                                obj.AllocatedRep = userAllocated;
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

        private string GetCopanyAllocatedHistory(string comId)
        {
            var returnResult = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @" SELECT LPC.UserId ,LPC.Notes,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName ,LPC.createddate,LPC.ExpiryDate,LPC.Notes
From [dbo].LeadCompany LPC 
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

        public
            string GetAssignedRealUserByCOmId(string comId)
        {
            var userid = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @" SELECT LPC.UserId ,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName 
From LeadCompany LPC  inner JOIN Logins lcR ON lcR.LoginID=LPC.UserId Where LPC.CompanyId=@comId AND convert(varchar(10),LPC.ExpiryDate, 120) > CAST(getdate() as date)";

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

                                userid = sdr["AlloUserName"].ToString();
                            }

                        }

                    }

                    conn.Close();

                }

            }

            return userid;
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

                    cmd.CommandText = @" WITH CTE_LeadCom (comId,maxrecId) as (SELECT  LPC.CompanyId, MAX(LPC.Id)  
FROM  LeadCompany LPC 
where LPC.CompanyId=@comId 
GROUP By LPC.CompanyID )

SELECT LPC.UserId ,LPC.Notes,lpcv.comId as companyID,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName 
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
                            }

                        }

                    }

                    conn.Close();

                }

            }

            return userid;
        }

        private string LoadAllOrderForCompany(string comId, string starDate, string endDate)
        {
            string resultString = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var query = @"Select OrderedDateTime,Total,XeroInvoiceNumber from  [dbo].orders where companyId={0} and Status<>@status order by OrderedDateTime desc";

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
                    cmd.Parameters.AddWithValue("@status", "CANCELLED");
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {


                                if (resultString == "")
                                    resultString = Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy") + "," + sdr["Total"] + "," + sdr["XeroInvoiceNumber"];
                                else
                                {
                                    resultString = resultString + ";";
                                    resultString = resultString + Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy") + "," + sdr["Total"] + "," + sdr["XeroInvoiceNumber"];
                                }
                            }

                        }

                    }

                    conn.Close();
                }

            }

            return resultString;
        }

        private string LoadLastOrderDateForCompany(string comId)
        {
            string resultString = "";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var query = @"
                Select Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where  ORC.CompanyID=@comId
				  Order By OrderID Desc";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@comId", comId);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                resultString = Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("dd-MMM-yyyy");
                            }

                        }

                    }

                    conn.Close();
                }

            }

            return resultString;
        }

        private int getItemsTotalCount(string repuser)
        {
            int TotalitemCount = 0;
            String cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd;
                var query = @"SELECT Distinct  CP.CompanyID,ROW_NUMBER() OVER(ORDER BY OrderedDateTime ASC) AS Row# , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes ,MBG.OrderedDateTime ,
                                lc.FirstName + ' ' + lc.LastName 
								AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM 
							   [dbo].Companies CP
                             inner  join [dbo].Contacts CT on CP.CompanyID = CT.CompanyID  
							   inner join [dbo].Logins lc on lc.LoginID=CP.OwnershipAdminID  
                                  
                            
                              OUTER APPLY 
    (SELECT Top 1 ORC.OrderedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
    ) AS MBG
                            WHERE CP.OwnershipAdminID = " + repuser + " and (CP.IsSupperAcount IS NULL OR CP.IsSupperAcount<>'1') " +
                        "  and (CP.Hold IS NULL OR CP.Hold<>'Y')";
                cmd = new SqlCommand(query, conn);


                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    TotalitemCount = dt.Rows.Count;
                }

                conn.Close();
            }

            return TotalitemCount;
        }
    }

    public class DataItem
    {
        public string View { get; set; }
        public string CompayId { get; set; }
        public string CompayName { get; set; }
        public string Contact { get; set; }
        public string LastOrderDate { get; set; }
        public string FullNotes { get; set; }
        public string Notes { get; set; }
        public string Owner { get; set; }
        public string Active { get; set; }
        public string Locked { get; set; }
        public string Allocated { get; set; }
        public string AllocatedRep { get; set; }
        public string Select { get; set; }
        public string orderItems { get; set; }
        public string Link { get; set; }
        public string CompanyAllocationHistory { get; set; }
    }

    public class DataTableData
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DataItem> data { get; set; }
    }
}