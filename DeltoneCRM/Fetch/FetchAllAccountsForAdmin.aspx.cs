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
    public partial class FetchAllAccountsForAdmin : System.Web.UI.Page
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


        public string ReturnCompanies(string rep, string startDate, string endDate)
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {



                    if (rep == "0")
                        cmd.CommandText = @"SELECT   CP.CompanyID , CP.CompanyName,CP.Notes, CT.FirstName + ' ' + CT.LastName AS FullName,CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,
                                 lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode ,LO.CreatedDateTime as orderCreateDate,LO.OrderID 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID   inner join Logins lc on lc.LoginID=CP.OwnershipAdminID   
                                    OUTER APPLY (
                Select Top 1 ORC.CreatedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
             ) LO
                              ";
                    else
                        cmd.CommandText = @"SELECT   CP.CompanyID , CP.CompanyName,CP.Notes, CT.FirstName + ' ' + CT.LastName AS FullName,CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,
                                lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode ,LO.CreatedDateTime as orderCreateDate,LO.OrderID 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID  

                               OUTER APPLY (
                Select Top 1 ORC.CreatedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where  ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
             ) LO
                            
                            WHERE CP.OwnershipAdminID = " + rep + " ";
                    cmd.Connection = conn;
                    conn.Open();
                    var canAdd = true;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                canAdd = true;
                                var comId = sdr["CompanyID"].ToString();
                                var orderId = sdr["OrderID"].ToString();
                                var CoName = sdr["CompanyName"].ToString();
                                var contactName = sdr["FullName"].ToString();
                                var repName = sdr["createdUser"].ToString();
                                var active = sdr["Active"].ToString();
                                var locked = "N";
                                var IsSupperAcco = "N";
                                var lastOrderDate = "";
                                if (sdr["orderCreateDate"] != DBNull.Value)
                                    lastOrderDate =Convert.ToDateTime( sdr["orderCreateDate"]).ToString("dd-MMM-yyyy");


                                var orderString = LoadAllOrderForCompany(comId, startDate, endDate);
                                var leadNotes = "";

                                var userAllocated = GetAssignedUserId(comId, out leadNotes);


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


                                if (sdr["Hold"] != DBNull.Value)
                                {
                                    locked = sdr["Hold"].ToString();
                                }
                                String strCompanyView = "<img src='Images/Edit.png'  onclick='ViewCompany(" + sdr["CompanyID"] + ")'/>";

                                var dexc = Regex.Replace(sdr["Notes"].ToString(), @"\t|\n|\r", "");
                                dexc = dexc.Replace(@"\", "-");
                                dexc = dexc.Replace("\"", "");
                                var fullNotes = dexc;

                                leadNotes = Regex.Replace(leadNotes, @"\t|\n|\r", "");
                                leadNotes = leadNotes.Replace(@"\", "-");
                                leadNotes = leadNotes.Replace("\"", "");
                                fullNotes = fullNotes + "  Lead Notes: "+ leadNotes;

                                fullNotes = Regex.Replace(fullNotes, "<.*?>", String.Empty);
                                dexc = fullNotes;
                                if (dexc.Length > 50)
                                    dexc = dexc.Substring(0, 50);
                                if (dexc.Trim() == "Lead Notes:")
                                    dexc = "";

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
                                              + "\"" + TeleBuilder + "\","
                                               + "\"" + repName + "\","
                                                + "\"" + active + "\","
                                                 + "\"" + locked + "\","
                                                  + "\"" + userAllocated + "\","
                                                 + "\"" + orderString + "\","

                                            + "\"" + strCompanyView + "\"],";
                                }
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

SELECT LPC.UserId ,LPC.Notes ,lpcv.comId as companyID,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName 
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