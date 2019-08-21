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
    public partial class FetchSupperAccountForAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var rep = Request["rep"];
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
                        cmd.CommandText = @"SELECT Distinct  CP.CompanyID, CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                                  lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                              inner join dbo.Contacts CT on CP.CompanyID = CT.CompanyID   
                         inner join Logins lc on lc.LoginID=CP.OwnershipAdminID and CP.IsSupperAcount = 1 
                         
                              ";
                    else
                        cmd.CommandText = @"SELECT Distinct CP.CompanyID , CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,
                                  lc.FirstName + ' ' + lc.LastName AS createdUser,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode 
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                              inner join dbo.Contacts CT on CP.CompanyID = CT.CompanyID  inner join Logins lc on lc.LoginID=CP.OwnershipAdminID and CP.IsSupperAcount = 1   
                            WHERE lc.LoginID = " + rep + "  ";
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
                                var ViewEdit = "<img src='../Images/Edit.png'  onclick='OpenCompany(" + comId + ");'>";
                                var TeleBuilder = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"];
                                strOutput = strOutput + "[\"" + comId + "\"," + "\"" + CoName + "\","
                                        + "\"" + contactName + "\","
                                        + "\"" + TeleBuilder + "\","
                                        + "\"" + sdr["createdUser"].ToString() + "\","
                                        + "\"" + ViewEdit + "\"],";
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