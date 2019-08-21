using System;
using System.Configuration;
using System.Data.SqlClient;

namespace DeltoneCRM.Fetch
{
    public partial class FetchLinkCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var strCompanyID = Request.QueryString["companyid"].ToString();
            Response.Write(ReturnLinkComanies(strCompanyID));
        }

        protected string ReturnLinkComanies(String strCompanyID)
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT DISTINCT (CP.CompanyID ), CT.*,   CP.CompanyName, CT.FirstName + ' ' + CT.LastName AS FullName,CP.Active,CP.CreatedDateTime as coCreatedDate
                                ,DEFAULT_Number ,DEFAULT_AreaCode, DEFAULT_CountryCode
                               ,MOBILE_AreaCode, MOBILE_CountryCode ,MOBILE_Number ,Email FROM dbo.Companies CP
                               join dbo.Contacts CT on CP.CompanyID = CT.CompanyID INNER JOIN CompanyLinked CPL ON CPL.CompanyIdLinked =CP.CompanyID WHERE CPL.CompanyId =@coID";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@coID", strCompanyID);
                    cmd.Connection.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var ViewEdit = "<img src='../Images/Edit.png'  onclick='OpenCompanyLink(" + sdr["CompanyID"].ToString() + ");'>";
                                var active = sdr["Active"].ToString();
                                var comId = sdr["CompanyID"].ToString();
                                var coName = sdr["CompanyName"].ToString();
                                var contactName = sdr["FullName"].ToString();
                                var telePhone = sdr["DEFAULT_AreaCode"].ToString() + sdr["DEFAULT_CountryCode"] + sdr["DEFAULT_Number"]; ;
                                var email = sdr["Email"].ToString();
                                var mobile = sdr["MOBILE_AreaCode"].ToString() + sdr["MOBILE_CountryCode"] + sdr["MOBILE_Number"];
                                var address = sdr["STREET_AddressLine1"].ToString();
                               // var createdDate = Convert.ToDateTime(sdr["CreatedDateTime"]).ToString("dd-MMM-yyyy");
                                var createdDate = "";
                                if (sdr["coCreatedDate"] != DBNull.Value)
                                    createdDate = Convert.ToDateTime(sdr["coCreatedDate"]).ToString("dd-MMM-yyyy");
                                strOutput = strOutput + "[\"" + comId + "\"," + "\"" + coName + "\","
                                       + "\"" + contactName + "\","
                                       + "\"" + createdDate + "\","
                                         + "\"" + telePhone + "\","
                                          + "\"" + mobile + "\","
                                          
                                           + "\"" + email + "\","
                                           + "\"" + address + "\","
                                            + "\"" + active + "\","
                                       + "\"" + ViewEdit + "\"],";
                            }
                            int lenght = strOutput.Length;
                            strOutput = strOutput.Substring(0, (lenght - 1));
                        }
                       
                    }
                }

                con.Close();
            }
            strOutput = strOutput + "]}";
            return strOutput;
        }
    }
}