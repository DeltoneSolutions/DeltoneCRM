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
    public partial class FetchAllocatedCompanies : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnLeadCompaniesHistory("", "", ""));
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



                    //if (rep == "0")
                    cmd.CommandText = @"SELECT   CP.CompanyID , LPC.Id as leadID,CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,CP.Active,CP.Hold,CP.IsSupperAcount,CP.CreatedDateTime,LPC.CompanyId as linkedtableComId ,LPC.UserId,lcR.FirstName + ' ' + lcR.LastName AS AlloUserName,
                                 lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email ,LPC.CreatedDate as CrLp, LPC.ExpiryDate FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID   inner join Logins lc on lc.LoginID=CP.OwnershipAdminID   
                                  JOIN LeadCompany LPC ON CP.CompanyID=LPC.CompanyId  JOIN Logins 
                                        lcR ON lcR.LoginID=LPC.UserId  ORDER BY CrLp Desc";
//                    else
//                    {
//                        cmd.CommandText = @"SELECT Id ,CP.CompanyName ,CT.FirstName + ' ' + CT.LastName AS FullName,UserId, lc.FirstName + ' ' + lc.LastName AS repname , lcuser.FirstName   + ' ' + lcuser.LastName AS allocateduser
//                             ,LPC.CompanyId ,LPC.CreatedDate ,ExpiryDate ,LPC.Notes, CP.Notes FROM LeadCompany LPC
//  join Companies CP on CP.CompanyID=LPC.CompanyId join Contacts CT on CP.CompanyID = CT.CompanyID  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID
//  inner join  Logins lcuser on lcuser.LoginID=LPC.UserId";


//                        var query = "";

//                    }

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
                                var companyOwner = sdr["createdUser"].ToString();
                                // var active = sdr["Active"].ToString();
                                var allocatedDate = Convert.ToDateTime(sdr["CrLp"].ToString()).ToString("dd-MMM-yyyy");
                                var expiryDate = Convert.ToDateTime(sdr["ExpiryDate"].ToString()).ToString("dd-MMM-yyyy");
                                var locked = "N";
                                var allocatedRep = sdr["AlloUserName"].ToString();
                                var leadId = sdr["leadID"].ToString();
                              
                                    strOutput = strOutput + "[\"" + comId + "\","
                                        + "\"" + leadId + "\","
                                         + "\"" + CoName + "\","
                                         + "\"" + contactName + "\","
                                            + "\"" + companyOwner + "\","
                                            + "\"" + allocatedDate + "\","
                                               + "\"" + expiryDate + "\","
                                            + "\"" + allocatedRep + "\"],";
                                    isadded = true;
                                
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

    }
}