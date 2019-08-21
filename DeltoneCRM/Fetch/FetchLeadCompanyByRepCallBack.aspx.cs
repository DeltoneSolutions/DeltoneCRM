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
    public partial class FetchLeadCompanyByRepCallBack : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var rep = Request["rep"];
            Response.Write(ReturnCompanies(rep));
        }

        public string ReturnCompanies(string rep)
        {
            var canremove = false;
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {



                    if (rep == "0")
                        cmd.CommandText = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,lo.CreatedDate,lo.ExpiryDate,ce.Description ,ce.EventStart,
                                  CP.CompanyID ,lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId   inner join Logins lc on lo.UserId=lc.LoginID inner join CalendarEvent ce on lo.CompanyId=ce.CompanyId and ce.IsLeadEvent=1  and convert(varchar(10),lo.ExpiryDate, 120) < CAST(getdate() as date) ";
                    else
                        cmd.CommandText = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,lo.CreatedDate,lo.ExpiryDate,ce.Description ,ce.EventStart,
                                  CP.CompanyID ,lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId   inner join Logins lc on lo.UserId=lc.LoginID AND lo.UserId = " + rep + " inner join CalendarEvent ce on lo.CompanyId=ce.CompanyId and ce.IsLeadEvent=1 and convert(varchar(10),lo.ExpiryDate, 120) < CAST(getdate() as date) ";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var comId = sdr["CompanyID"].ToString();
                                if (CanAllocateLeadCompany(comId) == false)
                                {
                                    var LinkLead = "<input id='" + sdr["CompanyID"].ToString() + "' name='selectlock'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";
                                    var CoName = sdr["CompanyName"].ToString();
                                    var contactName = sdr["FullName"].ToString();
                                    var expiryDate = sdr["ExpiryDate"].ToString();
                                    var repName = sdr["createdUser"].ToString();
                                    var dexc = Regex.Replace(sdr["Description"].ToString(), @"\t|\n|\r", "");
                                    dexc = dexc.Replace(@"\", "-");
                                    dexc = dexc.Replace("\"", "");
                                    var message = dexc + "-- " + sdr["EventStart"].ToString();

                                    strOutput = strOutput + "[\"" + comId + "\"," + "\"" + CoName + "\","
                                            + "\"" + contactName + "\","
                                            + "\"" + repName + "\","
                                            + "\"" + expiryDate + "\","
                                             + "\"" + message + "\","
                                            + "\"" + LinkLead + "\"],";
                                    canremove = true;
                                }
                            }

                        }
                        if (canremove)
                        {
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


        private bool CanAllocateLeadCompany(string comId)
        {
            var cannadd = false;
            var query = @"SELECT Distinct CP.CompanyName, 
                                  CP.CompanyID  FROM dbo.Companies CP
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId  and 
                          convert(varchar(10),lo.ExpiryDate, 120) >= CAST(getdate() as date) and lo.CompanyId=@coId";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@coId", SqlDbType.Int).Value = comId;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                cannadd = true;
                                return cannadd;

                            }
                        }
                    }
                    conn.Close();

                }

            }

            return false;
        }
    }
}