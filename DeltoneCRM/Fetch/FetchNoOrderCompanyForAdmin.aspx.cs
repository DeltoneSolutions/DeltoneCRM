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
    public partial class FetchNoOrderCompanyForAdmin : System.Web.UI.Page
    {
        string months = "1"; string rep = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            months = Request["mon"];
            rep = Request["rep"];
            if (rep == "self")
                rep = Session["LoggedUserID"].ToString();
            Response.Write(ReturnCompanyData());
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
 from Companies CP
 join Contacts CT 
 ON CP.CompanyId=CT.CompanyID
 Cross Apply (
                Select Top 1 ORC.CreatedDateTime,ORC.OrderID 
                 From  Orders ORC
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