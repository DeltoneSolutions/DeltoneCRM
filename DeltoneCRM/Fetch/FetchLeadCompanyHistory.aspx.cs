using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchLeadCompanyHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnLeadCompaniesHistory("","",""));
        }


        public string ReturnLeadCompaniesHistory(string rep, string startDate, string endDate)
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
                                  JOIN LeadCompany LPC ON CP.CompanyID=LPC.CompanyId  JOIN Logins 
                                        lcR ON lcR.LoginID=LPC.UserId  ORDER BY CP.CompanyName
                              ";
                    else
                    {
                        cmd.CommandText = @"SELECT Id ,CP.CompanyName ,CT.FirstName + ' ' + CT.LastName AS FullName,UserId, lc.FirstName + ' ' + lc.LastName AS repname , lcuser.FirstName   + ' ' + lcuser.LastName AS allocateduser
                             ,LPC.CompanyId ,LPC.CreatedDate ,ExpiryDate ,LPC.Notes, CP.Notes FROM LeadCompany LPC
  join Companies CP on CP.CompanyID=LPC.CompanyId join Contacts CT on CP.CompanyID = CT.CompanyID  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID
  inner join  Logins lcuser on lcuser.LoginID=LPC.UserId";


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
                                var companyOwner = sdr["repname"].ToString();
                               // var active = sdr["Active"].ToString();
                                var allocatedDate = Convert.ToDateTime(sdr["CreatedDate"].ToString()).ToString("dd-MMM-yyyy");
                                var expiryDate = Convert.ToDateTime(sdr["ExpiryDate"].ToString()).ToString("dd-MMM-yyyy");
                                var locked = "N";

                               var orderString = LoadAllOrderForCompany(comId, startDate, endDate);

                                //if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
                                //{
                                //    if (string.IsNullOrEmpty(orderString))
                                //        //canAdd = false;
                                //}

                                //if (sdr["Hold"] != DBNull.Value)
                                //{
                                //    locked = sdr["Hold"].ToString();
                                //}

                                String strCompanyView = "<img src='Images/Edit.png'  onclick='ViewCompany(" + sdr["CompanyID"] + ")'/>";

                                var Link = "<input name='selectchk'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var LinkLocked = "<input id='" + sdr["CompanyID"].ToString() + "' name='selectlock'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var linked = "N";
                                var leadNotes = "";
                                var allocateduser = sdr["allocateduser"].ToString();

                               // var userAllocated = GetAssignedUserId(comId, out leadNotes);
                               // if (!string.IsNullOrEmpty(userAllocated))
                               // {
                                    linked = "Y";

                               // }

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

                             //   var TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                                if (canAdd)
                                {
                                    strOutput = strOutput + "[\"" + comId + "\"," 
                                        + "\"" + strCompanyView + "\","
                                         + "\"" + CoName + "\","
                                         + "\"" + contactName + "\","
                                            + "\"" + companyOwner + "\","
                                            + "\"" + lastOrderDate + "\","
                                            + "\"" + allocatedDate + "\","
                                             + "\"" + expiryDate + "\","
                                               + "\"" + dexc + "\","
                                                 + "\"" + fullNotes + "\","
                                               + "\"" + allocateduser + "\","
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
    }
}