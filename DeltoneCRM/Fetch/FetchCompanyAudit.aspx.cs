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
    public partial class FetchCompanyAudit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Return  all the Contact according to the Company ID

            String strCompanyID = Request.QueryString["companyid"].ToString();
            Response.Write(ReturnContacts(strCompanyID));


        }

        protected string ReturnContacts(String strCompanyID)
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT  au.AuditId,au.Action_Type , au.CreatedDateTime, " +
                    " lo.FirstName + ' ' + lo.LastName AS createdUser , au.Column_Name,au.Pre_Value,au.New_Value " +
                    " FROM dbo.AuditLog au inner join Logins lo on au.CreatedUserId=lo.LoginID WHERE au.CompanyId=" + strCompanyID;

                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                string dataChanges = "";
                                var prValue = sdr["Pre_Value"].ToString();

                                prValue = prValue.Replace(",", "");
                                prValue = prValue.Replace("\"", "");

                                var newVal = sdr["New_Value"].ToString();
                                newVal = newVal.Replace(",", "");
                                newVal = newVal.Replace("\"", "");

                                if (sdr["Action_Type"].ToString() == "Updated")

                                    dataChanges = sdr["Column_Name"] + " value changed from " + prValue + " to " + newVal;
                                else
                                    dataChanges = newVal;

                                strOutput = strOutput + "[\"" + sdr["AuditId"] + "\"," + "\"" + sdr["Action_Type"] + "\"," + "\"" + sdr["CreatedDateTime"] + "\","
                                    + "\"" + sdr["createdUser"] + "\","
                                    + "\"" + dataChanges + "\"],";
                                strOutput = Regex.Replace(strOutput, @"\t|\n|\r", "");

                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));

                        }
                        strOutput = strOutput + "]}";
                        conn.Close();
                    }
                }

            }
            return strOutput;
        }
    }
}