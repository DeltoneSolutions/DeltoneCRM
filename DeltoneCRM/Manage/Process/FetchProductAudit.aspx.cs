using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class FetchProductAudit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Return  all the Contact according to the Company ID

            String itemId = Request.QueryString["itemId"].ToString();
            Response.Write(ReturnContacts(itemId));


        }

        protected string ReturnContacts(String itemId)
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT  au.AuditId,au.Action_Type , au.CreatedDateTime, " +
                    " lo.FirstName  AS createdUser , au.Column_Name,au.Pre_Value,au.New_Value " +
                    " FROM dbo.AuditLog au inner join Logins lo on au.CreatedUserId=lo.LoginID WHERE au.CompanyId=-1 and au.RecordId=" + itemId;

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

                                var createdDate = Convert.ToDateTime(sdr["CreatedDateTime"]).ToString("dd/MM/yyyy");

                                  //  dataChanges = sdr["Column_Name"] + " value changed from " + prValue + " to " + newVal;


                                    strOutput = strOutput + "[\"" + sdr["AuditId"] + "\"," + "\"" + sdr["Action_Type"] + "\"," + "\"" + createdDate + "\","
                                    + "\"" + sdr["createdUser"] + "\","
                                     + "\"" + prValue + "\","
                                    + "\"" + newVal + "\"],";
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