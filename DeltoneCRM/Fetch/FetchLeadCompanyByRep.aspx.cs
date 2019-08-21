using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchLeadCompanyByRep : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var rep =  Request["rep"];
            Response.Write(ReturnCompanies(rep));
        }


        public string ReturnCompanies(string rep)
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {



                    if (rep == "0")
                        cmd.CommandText = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,lo.CreatedDate,lo.ExpiryDate,
                                  CP.CompanyID ,lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId   inner join Logins lc on lo.UserId=lc.LoginID AND CP.OwnershipAdminID  <> lo.UserId  AND convert(varchar(10),lo.ExpiryDate, 120) >= CAST(getdate() as date) ";
                    else
                        cmd.CommandText = @"SELECT Distinct CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,lo.CreatedDate,lo.ExpiryDate,
                                  CP.CompanyID ,lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  
                              inner join LeadCompany lo on CP.CompanyID=lo.CompanyId   inner join Logins lc on lo.UserId=lc.LoginID And CP.OwnershipAdminID <> " + rep + " AND lo.UserId = " + rep + " AND convert(varchar(10),lo.ExpiryDate, 120) >= CAST(getdate() as date) ";
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
                                var contactName = sdr["FullName"].ToString();
                                var expiryDate = sdr["ExpiryDate"].ToString();
                                var repName = sdr["createdUser"].ToString();

                                strOutput = strOutput + "[\"" + comId + "\"," + "\"" + CoName + "\","
                                        + "\"" + contactName + "\","
                                        + "\"" + repName + "\","
                                        + "\"" + expiryDate + "\"],";
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