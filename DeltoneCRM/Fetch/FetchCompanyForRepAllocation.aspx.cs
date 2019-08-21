using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchCompanyForRepAllocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var rep = Request["rep"];
            var startdate = Request["startdate"];
            var enddate = Request["enddate"];
            if (rep == "-1")
                Response.Write(ReturnCompaniesForLock(rep));
            else
                Response.Write(ReturnCompanies(rep, startdate, enddate));
        }

        private bool CheckDicCompanyID(string cmId, string comCreateddate, Dictionary<string, DateTime> comDic)
        {
            DateTime valueRep;
            if (comDic.TryGetValue(cmId, out valueRep))
            {
                if (valueRep < Convert.ToDateTime(comCreateddate))
                {

                }
            }


            return true;

        }


        public string ReturnCompanies(string rep, string startDate, string endDate)
        {
            var comList = new Dictionary<string, DateTime>();
            String strOutput = "{\"aaData\":[";
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
                        cmd.CommandText = @"SELECT Distinct  CP.CompanyID , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                 CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,CP.Notes,
                                lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID  
                                  
                              
                            WHERE CP.OwnershipAdminID = " + rep + " and (CP.IsSupperAcount IS NULL OR CP.IsSupperAcount<>'1') and (CP.Hold IS NULL OR CP.Hold<>'Y') ORDER BY CP.CompanyName";


                        var query = "";

                    }

                    var canAdd = true;
                    var isadded = false;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                canAdd = true;
                                var comId = sdr["CompanyID"].ToString();

                                var CoName = sdr["CompanyName"].ToString();
                                var contactName = sdr["FullName"].ToString();
                                var repName = sdr["createdUser"].ToString();
                                var active = sdr["Active"].ToString();
                                var locked = "N";

                                var orderString = LoadAllOrderForCompany(comId, startDate, endDate);

                                //if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                                //{
                                //    if (string.IsNullOrEmpty(orderString))
                                //        //canAdd = false;
                                //}

                                if (sdr["Hold"] != DBNull.Value)
                                {
                                    locked = sdr["Hold"].ToString();
                                }

                                String strCompanyView = "<img src='Images/Edit.png'  onclick='ViewCompany(" + sdr["CompanyID"] + ")'/>";

                                var Link = "<input name='selectchk'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var LinkLocked = "<input id='" + sdr["CompanyID"].ToString() + "' name='selectlock'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var linked = "N";
                                var leadNotes = "";

                                var userAllocated = GetAssignedUserId(comId ,out leadNotes);
                                if (!string.IsNullOrEmpty(userAllocated))
                                {
                                    linked = "Y";

                                }

                                leadNotes = Regex.Replace(leadNotes, @"\t|\n|\r", "");
                                leadNotes = leadNotes.Replace(@"\", "-");
                                leadNotes = leadNotes.Replace("\"", "");
                                

                                if (linked == "Y")
                                    LinkLocked = "<input name='selectlock' checked='true' value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var dexcMain = Regex.Replace(sdr["Notes"].ToString(), @"\t|\n|\r", "");
                                dexcMain = dexcMain.Replace(@"\", "-");
                                dexcMain = dexcMain.Replace("\"", "");
                                var fullNotes = dexcMain;
                                var dexc = dexcMain;

                                fullNotes = fullNotes + "  Lead Notes: " + leadNotes;
                              
                                if (dexc.Length > 50)
                                    dexc = dexc.Substring(0, 50);
                                dexc = Regex.Replace(dexc, "<.*?>", String.Empty);
                                fullNotes = Regex.Replace(fullNotes, "<.*?>", String.Empty);

                                var lastOrderDate = LoadLastOrderDateForCompany(comId);


                                if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                                {
                                    if (string.IsNullOrEmpty(lastOrderDate))
                                    {
                                        canAdd = false;
                                    }
                                    else
                                    {
                                        var lastOrderDateDateType = Convert.ToDateTime(lastOrderDate);
                                        var startDateDateType = Convert.ToDateTime(startDate);
                                        var endDateDateType = Convert.ToDateTime(endDate);
                                        if (lastOrderDateDateType.Date >= startDateDateType.Date
                                            && lastOrderDateDateType.Date <= endDateDateType.Date)
                                            canAdd = true;
                                        else
                                            canAdd = false;
                                    }

                                }

                                var TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                                if (canAdd)
                                {
                                    strOutput = strOutput + "[\"" + comId + "\","
                                        + "\"" + strCompanyView + "\","
                                        + "\"" + CoName + "\","
                                            + "\"" + contactName + "\","
                                            + "\"" + lastOrderDate + "\","
                                               + "\"" + dexc + "\","
                                                 + "\"" + fullNotes + "\","
                                               + "\"" + repName + "\","
                                                + "\"" + active + "\","
                                                 + "\"" + locked + "\","
                                                 + "\"" + userAllocated + "\","
                                                  + "\"" + LinkLocked + "\","
                                                   + "\"" + Link + "\","
                                                   + "\"" + orderString + "\","
                                            + "\"" + strCompanyView + "\"],";
                                    isadded = true;
                                }
                            }
                            if (isadded)
                            {
                                int Length = strOutput.Length;
                                strOutput = strOutput.Substring(0, (Length - 1));
                            }
                        }

                    }

                    conn.Close();

                }

            }
            strOutput = strOutput + "]}";
            return strOutput;
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
where LPC.CompanyId=@comId And convert(varchar(10),LPC.ExpiryDate, 120) >= CAST(getdate() as date)
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


        public string ReturnCompaniesForLock(string rep)
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT   CP.CompanyID , CP.CompanyName, CP.Active,CP.Hold FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID   inner join Logins lc on lc.LoginID=CP.OwnershipAdminID   
                              ";

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var comId = sdr["CompanyID"].ToString();
                                var CoName = sdr["CompanyName"].ToString();

                                var active = sdr["Active"].ToString();
                                var locked = "N";

                                if (sdr["Hold"] != DBNull.Value)
                                {
                                    locked = sdr["Hold"].ToString();
                                }

                                var Link = "<input name='selectchk'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var LinkLocked = "<input id='" + sdr["CompanyID"].ToString() + "' name='selectlock'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                if (locked == "Y")
                                    LinkLocked = "<input name='selectlock' checked='true' value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                strOutput = strOutput + "[\"" + Link + "\"," + "\"" + comId + "\","
                                               + "\"" + CoName + "\","
                                            + "\"" + active + "\","
                                             + "\"" + locked + "\","
                                        + "\"" + LinkLocked + "\"],";
                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }

                    }

                    conn.Close();

                }

            }
            strOutput = strOutput + "]}";
            return strOutput;
        }
    }
}