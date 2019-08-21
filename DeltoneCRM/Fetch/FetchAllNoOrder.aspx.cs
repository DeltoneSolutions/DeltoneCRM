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
    public partial class FetchAllNoOrder : System.Web.UI.Page
    {
        string months = "1"; string rep = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["mon"] != null)
                months = Request["mon"];
            rep = Request["rep"];
            if (rep == "self")
                rep = Session["LoggedUserID"].ToString();
            var startDate = "";
            var endDate = "";
            var canApplyDateFilter = false;
            if (Request["starDate"] != null)
            {
                startDate = Request["starDate"];

                canApplyDateFilter = true;
            }
            if (Request["endDate"] != null)
            {
                endDate = Request["endDate"];
                canApplyDateFilter = true;
            }

            Response.Write(ReturnCompanies(rep, months, startDate, endDate, canApplyDateFilter));


        }




        public string ReturnCompanies(string rep, string selectedMonth, string stDate, string enDate, bool canApplyFilter)
        {
            var month = Convert.ToInt32(selectedMonth);

            var startDate = DateTime.Now.AddMonths(month);
            // var startDate = Convert.ToDateTime("01/" + startDateDateCal.Month + "/" + startDateDateCal.Year);

            // var daysOfMonth = DateTime.DaysInMonth(startDateDateCal.Year, startDateDateCal.Month);


            var endDate = DateTime.Now;
            var bothValue = false;
            if (!string.IsNullOrEmpty(stDate) && !string.IsNullOrEmpty(enDate))
            {
                bothValue = true;
            }

            if (!string.IsNullOrEmpty(stDate))
            {
                startDate = Convert.ToDateTime(stDate);

            }
            if (!string.IsNullOrEmpty(enDate))
            {
                endDate = Convert.ToDateTime(enDate);
            }

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
                                  
                              
                            WHERE CP.OwnershipAdminID = " + rep + " and  CP.Active='Y'  ORDER BY CP.CompanyName";


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
                                var isSupperAccount = "N";

                                var orderString = LoadAllOrderForCompany(comId);



                                if (sdr["IsSupperAcount"] != DBNull.Value)
                                {
                                    if (sdr["IsSupperAcount"].ToString() == "True")
                                        isSupperAccount = "Y";
                                }


                                String strCompanyView = "<img src='Images/Edit.png'  onclick='ViewCompany(" + sdr["CompanyID"] + ")'/>";

                                var Link = "<input name='selectchk'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var LinkLocked = "<input id='" + sdr["CompanyID"].ToString() + "' name='selectlock'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var leadNotes = "";

                                var userAllocated = GetAssignedUserId(comId, out leadNotes);
                                leadNotes = Regex.Replace(leadNotes, @"\t|\n|\r", "");
                                leadNotes = leadNotes.Replace(@"\", "-");
                                leadNotes = leadNotes.Replace("\"", "");
                                leadNotes = Regex.Replace(leadNotes, "<.*?>", String.Empty);

                                var dexcMain = Regex.Replace(sdr["Notes"].ToString(), @"\t|\n|\r", "");
                                dexcMain = Regex.Replace(dexcMain, @"\t|\n|\r", "");
                                dexcMain = dexcMain.Replace(@"\", "-");
                                dexcMain = dexcMain.Replace("\"", "");
                               
                                var dexc = dexcMain;
                              
                                //if (string.IsNullOrEmpty(dexc))
                                //    dexc = leadNotes;
                                if (dexc.Length > 50)
                                    dexc = dexc.Substring(0, 50);
                                

                                //dexcMain = dexcMain + "  Lead Notes: " + leadNotes;
                                if (dexcMain.Contains("<br/>"))
                                {
                                    var indexofFirstBr = dexcMain.IndexOf("<br/>");

                                    dexcMain = dexcMain.Substring(0, indexofFirstBr);
                                }
                                dexcMain = Regex.Replace(dexcMain, "<.*?>", String.Empty);
                                //else
                                //    if (dexcMain.Contains("."))
                                //    {
                                //        var indexofFirstBr = dexcMain.IndexOf(".");

                                //        dexcMain = dexcMain.Substring(0, indexofFirstBr);
                                //    }

                                var lastOrderDate = LoadLastOrderDateForCompany(comId);

                                if (canApplyFilter)
                                {
                                    if (bothValue == true)
                                    {
                                        canAdd = false;
                                        if (!string.IsNullOrEmpty(lastOrderDate))
                                        {
                                            var lastOrderDateDateType = Convert.ToDateTime(lastOrderDate);
                                            var startDateDateType = (startDate);
                                            var endDateDateType = (endDate);
                                            if (lastOrderDateDateType.Date >= startDateDateType.Date
                                                && lastOrderDateDateType.Date <= endDateDateType.Date)
                                                canAdd = true;
                                            //else
                                            //    if (bothValue == false)
                                            //    {
                                            //        if (lastOrderDateDateType >= endDateDateType)
                                            //            canAdd = false;
                                            //        else
                                            //            canAdd = true;
                                            //    }
                                        }
                                    }
                                    else
                                        if (!string.IsNullOrEmpty(lastOrderDate))
                                        {
                                            var lastOrderDateDateType = Convert.ToDateTime(lastOrderDate);
                                            var startDateDateType = (startDate);
                                            var endDateDateType = (endDate);
                                            if (lastOrderDateDateType.Date >= startDateDateType.Date
                                                && lastOrderDateDateType.Date <= endDateDateType.Date)
                                                canAdd = false;
                                            else
                                                if (bothValue == false)
                                                {
                                                    if (lastOrderDateDateType >= endDateDateType)
                                                        canAdd = false;
                                                    else
                                                        canAdd = true;
                                                }
                                        }
                                }
                                else
                                    if (selectedMonth != "0")
                                    {
                                        if (!string.IsNullOrEmpty(lastOrderDate))
                                        {
                                            var lastOrderDateDateType = Convert.ToDateTime(lastOrderDate);
                                            var startDateDateType = (startDate);
                                            var endDateDateType = (endDate);
                                            if (lastOrderDateDateType.Date >= startDateDateType.Date
                                                && lastOrderDateDateType.Date <= endDateDateType.Date)
                                                canAdd = false;
                                            else
                                                if (lastOrderDateDateType >= endDateDateType)
                                                    canAdd = false;
                                                else
                                                    canAdd = true;
                                        }
                                    }



                                var areaCode = sdr["DEFAULT_AreaCode"].ToString();

                                var defauNumber = sdr["DEFAULT_Number"].ToString();
                                var constructArea = areaCode;
                                // constructArea = sdr["DEFAULT_AreaCode"].ToString().Replace("(", string.Empty).Trim();
                                // constructArea = sdr["DEFAULT_AreaCode"].ToString().Replace(")", string.Empty);

                                if (constructArea == "02" || constructArea == "03" || constructArea == "07" || constructArea == "08")
                                {
                                    constructArea = constructArea + " " + defauNumber;
                                }

                                else
                                {
                                    constructArea = constructArea + defauNumber;
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
                                                 + "\"" + dexcMain + "\","
                                                + "\"" + constructArea + "\","
                                                   + "\"" + isSupperAccount + "\","
                                                    + "\"" + userAllocated + "\","
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

        private string LoadAllOrderForCompany(string comId)
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


        private string ReturnCompanyData()
        {
            var strOutput = "{\"aaData\":[";
            var month = DateTime.Now.Month;
            var startdate = DateTime.Now;
            if (months == "1")
                month = -1;
            else if (months == "2")
                month = -2;
            else if (months == "3")
                month = -3;
            else if (months == "6")
                month = -6;
            else if (months == "12")
                month = -12;
            var rowvail = false;
            //First table view for order and second for reminder then join both view to get order deatails

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    //                    cmd.CommandText = @";WITH CTE_LastOrder (CompanyID, CreatedDateTime,OrderId) As
                    //(
                    //SELECT  orc.CompanyID, MAX(CreatedDateTime) LastOrderDate,OrderID
                    //FROM  [dbo].Orders orc 
                    //GROUP By CompanyID,OrderID
                    //) ,
                    //CTEReminder  (CompanyID, MessageCreatedDate,EventId,Description) As
                    //(
                    //SELECT  erv.CompanyID, MAX(EventStart) MessageCreatedDate,EventId,erv.Description
                    //FROM  [dbo].[CalendarEvent] erv 
                    //GROUP By CompanyID,EventId,erv.Description
                    //) 
                    //
                    //SELECT CP.CompanyID,CP.CompanyName,LO.CreatedDateTime as CreatedDateTime,OrderId,ctr.Description,ctr.MessageCreatedDate,
                    // CT.FirstName + ' ' + CT.LastName AS FullName ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                    //                  ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email from [dbo].Companies CP
                    //JOIN CTE_LastOrder LO ON CP.CompanyID = LO.CompanyID
                    // join [dbo].Contacts CT on CP.CompanyID = CT.CompanyID  
                    // join CTEReminder ctr ON CP.CompanyID = ctr.CompanyID
                    //                    
                    //WHERE LO.CreatedDateTime  < Cast(Floor(Cast(dateAdd(Month,@monthPeriod, GetDate()) as Float))as DateTime)
                    //and CP.OwnershipAdminID=@ownerId
                    //
                    //Union SELECT  CP.CompanyID,CP.CompanyName,CP.CreatedDateTime as CreatedDateTime,0 as OrderId,CE.Description,CE.EventStart as MessageCreatedDate,
                    // CT.FirstName + ' ' + CT.LastName AS FullName ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                    //                  ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email 
                    //				  from [dbo].Companies CP
                    //				    left join  [dbo].CalendarEvent CE on CP.CompanyID=CE.CompanyID 
                    //				join  [dbo].Contacts CT on CP.CompanyID = CT.CompanyID  
                    //				 where CP.CompanyID 
                    //not in (select CompanyID from [dbo].Orders)
                    //and  OwnershipAdminID=@ownerId
                    //
                    //;";

                    cmd.CommandText = @"Select CP.CompanyID,CP.CompanyName,LO.CreatedDateTime as CreatedDateTime,LO.OrderID ,
CT.FirstName + ' ' + CT.LastName AS FullName ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                  ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email ,CAL.EventStart as MessageCreatedDate,CAL.Description
 from [dbo].Companies CP
 join [dbo].Contacts CT 
 ON CP.CompanyId=CT.CompanyID
 Cross Apply (
                Select Top 1 ORC.CreatedDateTime,ORC.OrderID 
                 From  [dbo].Orders ORC
                 Where ORC.CreatedDateTime< Cast(Floor(Cast(dateAdd(Month,@monthPeriod, GetDate()) as Float))as DateTime) 
				 AND ORC.CompanyID=CP.CompanyID
				  Order By OrderID Desc
             ) LO

			 LEFT JOIN 
			  [dbo].CalendarEvent CAL
				  ON CAL.CompanyId=CP.CompanyID

			 
			 where CP.OwnershipAdminID=@ownerId";

                    cmd.Parameters.AddWithValue("@monthPeriod", month);
                    cmd.Parameters.AddWithValue("@ownerId", rep);

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            rowvail = true;
                            while (sdr.Read())
                            {

                                var dexc = "";
                                if (sdr["Description"] != DBNull.Value)
                                    dexc = Regex.Replace(sdr["Description"].ToString(), @"\t|\n|\r", "");
                                dexc = dexc.Replace(@"\", "-");
                                if (dexc.Length > 20)
                                    dexc = dexc.Substring(0, 20);
                                var eventDate = "";
                                if (sdr["MessageCreatedDate"] != DBNull.Value)
                                    eventDate = Convert.ToDateTime(sdr["MessageCreatedDate"].ToString()).ToShortDateString();
                                var createdDateTime = Convert.ToDateTime(sdr["CreatedDateTime"].ToString()).ToString("dd/MM/yyyy");
                                var output = " { " + eventDate + " } ---> " + dexc;
                                var TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                                var MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                                String strEditOrder = "<img src='../Images/View.png'  onclick='OpenCompany(" + sdr["CompanyID"].ToString() + ");'>";
                                strOutput = strOutput + "[\"" + sdr["CompanyID"].ToString() + "\","
                                        + "\"" + sdr["OrderId"].ToString() + "\","
                                   + "\"" + sdr["CompanyName"].ToString() + "\","
                                   + "\"" + sdr["FullName"].ToString() + "\","
                                   + "\"" + TeleBuilder + "\","
                                    + "\"" + MobileBuilder + "\","
                                   + "\"" + createdDateTime + "\","
                                   + "\"" + output + "\","
                                   + "\"" + strEditOrder + "\"],";



                            }

                        }

                    }

                    conn.Close();


                    if (rowvail)
                    {
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));
                    }


                }

            }
            strOutput = strOutput + "]}";
            return strOutput;

        }




        private string FillCompaniesNOOrderSinceCreated(string strBuilder)
        {
            var query = @"SELECT  CP.CompanyID,CP.CompanyName,
 CT.FirstName + ' ' + CT.LastName AS FullName ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                  ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email 
				  from [dbo].Companies CP
				    left join  [dbo].CalendarEvent CE on CP.CompanyID=CE.CompanyID 
				join  [dbo].Contacts CT on CP.CompanyID = CT.CompanyID  
				 where CP.CompanyID 
not in (select CompanyID from [dbo].Orders)
and  OwnershipAdminID=@repId";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@repId", rep);

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var dexc = Regex.Replace(sdr["Description"].ToString(), @"\t|\n|\r", "");
                                dexc = dexc.Replace(@"\", "-");
                                if (dexc.Length > 20)
                                    dexc = dexc.Substring(0, 20);
                                var eventDate = Convert.ToDateTime(sdr["MessageCreatedDate"].ToString()).ToShortDateString();

                                var output = " { " + eventDate + " } ---> " + dexc;
                                var TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                                var MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                                String strEditOrder = "<img src='../Images/View.png'  onclick='OpenCompany(" + sdr["CompanyID"].ToString() + ");'>";
                                strBuilder = strBuilder + "[\"" + sdr["CompanyID"].ToString() + "\","
                                        + "\"" + "" + "\","
                                   + "\"" + sdr["CompanyName"].ToString() + "\","
                                   + "\"" + sdr["FullName"].ToString() + "\","
                                   + "\"" + TeleBuilder + "\","
                                    + "\"" + MobileBuilder + "\","
                                   + "\"" + "" + "\","
                                   + "\"" + output + "\","
                                   + "\"" + strEditOrder + "\"],";
                            }
                        }
                    }
                    conn.Close();


                }
            }


            return strBuilder;

        }



        public string ReturnCompanies()
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var month = DateTime.Now.Month;
                    var yearset = "YEAR(getDate())";
                    var enddate = DateTime.Now;
                    var startdate = DateTime.Now;
                    if (months == "1")
                    {
                        month = -1;

                    }

                    else if (months == "2")
                        month = -2;
                    else if (months == "3")
                        month = -3;
                    else if (months == "6")
                        month = -6;
                    else if (months == "12")
                    {
                        month = -12;
                        yearset = "DATEADD(YEAR,-1,GETDATE())";
                    }

                    startdate = startdate.AddMonths(month);

                    var listCom = new List<NoOrderCompany>();

                    //  var startdate = "(DATEADD(month," + month + ",GETDATE()))";

                    //                    cmd.CommandText = @"SELECT CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                    //                                  CP.CompanyID ,lo.FirstName + ' ' + lo.LastName AS createdUser ,DEFAULT_Number , Email FROM dbo.Companies CP
                    //                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                    //                              inner join Logins lo on lo.LoginID=CP.OwnershipAdminID 
                    //                               Where CP.OwnershipAdminID =" + rep + 
                    //                              " And CP.CompanyID not in " +
                    //                             " (select CompanyID from Orders where CreatedDateTime between " + startdate.ToShortDateString() 
                    //                             + "   AND " + enddate.ToShortDateString() + ")";

                    cmd.CommandText = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                                  CP.CompanyID ,lo.FirstName + ' ' + lo.LastName AS createdUser ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join Logins lo on lo.LoginID=CP.OwnershipAdminID And CP.OwnershipAdminID =" + rep;
                    //         " left join Orders o on o.CompanyId=CP.CompanyID where o.CreatedDateTime >=@start"
                    //     + "   AND o.CreatedDateTime <=@end ";
                    //cmd.Parameters.AddWithValue("@start", SqlDbType.DateTime).Value = startdate;
                    //cmd.Parameters.AddWithValue("@end", SqlDbType.DateTime).Value = enddate;
                    var rowvail = false;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            rowvail = true;
                            while (sdr.Read())
                            {
                                var obj = new NoOrderCompany()
                                {
                                    comId = sdr["CompanyID"].ToString(),
                                    CoName = sdr["CompanyName"].ToString(),
                                    contactName = sdr["FullName"].ToString(),
                                    TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"],
                                    MobileBuilder = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"],
                                    Email = sdr["Email"].ToString()
                                };

                                listCom.Add(obj);
                                // var lasterOrderDate = getLastOrderDateBasedOnCompanyID(sdr["CompanyID"].ToString(), month);
                                //strOutput = strOutput + "[\"" + sdr["CompanyID"] + "\","
                                //       + "\"" + sdr["CompanyName"].ToString() + "\","
                                //       + "\"" + sdr["FullName"].ToString() + "\","
                                //       + "\"" + sdr["DEFAULT_Number"].ToString() + "\","

                                //       + "\"" + sdr["Email"].ToString() + "\"],";
                            }

                        }

                    }

                    conn.Close();
                    var noORders = CheckOrder(listCom, startdate, enddate);

                    strOutput = FillStr(noORders, strOutput);
                    if (noORders.Count() > 0)
                    {
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));
                    }

                }

            }
            strOutput = strOutput + "]}";
            return strOutput;
        }

        private string FillStr(List<NoOrderCompany> obj, string str)
        {
            obj = obj.OrderByDescending(x => x.LastORderDate).ToList();
            foreach (var item in obj)
            {
                if (!string.IsNullOrEmpty(getLastOrderDateBasedOnCompanyID(item.comId)))
                {
                    item.LastORderDate = getLastOrderDateBasedOnCompanyID(item.comId).Split(',')[0];
                    item.OrderId = getLastOrderDateBasedOnCompanyID(item.comId).Split(',')[1];
                }
                item.lastDateCalledThem = getLastCalendarDate(item.comId);
                String strEditOrder = "<img src='../Images/View.png'  onclick='OpenCompany(" + item.comId.ToString() + ");'>";
                str = str + "[\"" + item.comId + "\","
                     + "\"" + item.OrderId + "\","
                + "\"" + item.CoName + "\","
                + "\"" + item.contactName + "\","
                + "\"" + item.TeleBuilder + "\","
                 + "\"" + item.MobileBuilder + "\","
                + "\"" + item.LastORderDate + "\","
                + "\"" + item.lastDateCalledThem + "\","
                + "\"" + strEditOrder + "\"],";
            }

            return str;
        }

        private List<NoOrderCompany> CheckOrder(List<NoOrderCompany> obj, DateTime startdate, DateTime enddate)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();

            var listCom = new List<NoOrderCompany>();

            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            foreach (var item in obj)
            {
                String strQuery = "SELECT o.OrderedDateTime FROM dbo.Orders o WHERE CompanyID = " + item.comId +
                         "   AND o.OrderedDateTime>=@start  AND o.OrderedDateTime<=@end ORDER BY o.OrderID DESC";

                var star = startdate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                var en = enddate.ToString("yyyy-MM-dd HH:mm:ss.fff");
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strQuery;
                    cmd.Parameters.AddWithValue("@start", SqlDbType.DateTime).Value = star;
                    cmd.Parameters.AddWithValue("@end", SqlDbType.DateTime).Value = en;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                        }
                        else
                            listCom.Add(item);
                    }
                }
                conn.Close();
            }

            return listCom;
        }

        public string getLastCalendarDate(string companyId)
        {
            string output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strQuery = "SELECT TOP 1 EventStart,Description,ModifiedDate FROM dbo.CalendarEvent WHERE CompanyId = " + companyId + " ORDER BY EventStart DESC";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {

                            var dexc = Regex.Replace(sdr["Description"].ToString(), @"\t|\n|\r", "");
                            dexc = dexc.Replace(@"\", "-");
                            if (dexc.Length > 20)
                                dexc = dexc.Substring(0, 20);
                            var eventDate = Convert.ToDateTime(sdr["EventStart"].ToString()).ToShortDateString();
                            // if (sdr["ModifiedDate"] != DBNull.Value)
                            // createdDate = Convert.ToDateTime(sdr["ModifiedDate"].ToString()).ToShortDateString();

                            output = " { " + eventDate + " } ---> " + dexc;
                        }
                    }
                }
            }
            conn.Close();
            return output;
        }

        public string getLastOrderDateBasedOnCompanyID(string CompanyID)
        {
            string output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strQuery = "SELECT TOP 1 OrderedDateTime,OrderID FROM dbo.Orders WHERE CompanyID = " + CompanyID + " ORDER BY OrderedDateTime DESC";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            output = Convert.ToDateTime(sdr["OrderedDateTime"].ToString()).ToShortDateString() + "," + sdr["OrderID"].ToString();
                        }
                    }
                }
            }
            conn.Close();
            return output;
        }

        private IList<NoOrderCo> GetCompaniesNoORderFirst()
        {

            var listCom = new List<NoOrderCo>();


            return listCom;
        }

        public class NoOrderCompany
        {
            public string comId { get; set; }
            public string CoName { get; set; }
            public string contactName { get; set; }
            public string ContactNumber { get; set; }
            public string Email { get; set; }
            public string LastORderDate { get; set; }
            public string lastDateCalledThem { get; set; }
            public string TeleBuilder { get; set; }
            public string MobileBuilder { get; set; }
            public string OrderId { get; set; }
        }

        public class NoOrderCo
        {
            public string comId { get; set; }
            public string CoName { get; set; }
            public string LastORderDate { get; set; }
            public string OrderId { get; set; }
        }
    }
}