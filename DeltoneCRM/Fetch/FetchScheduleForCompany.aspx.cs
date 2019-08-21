using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchScheduleForCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strCompanyID = Request.QueryString["companyid"].ToString();
            Response.Write(ReturnEvents(strCompanyID));
        }

        public string ReturnEvents(string strCompanyID)
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    var strSqlContactStmt = @"SELECT EventId, Description, Title,CreatedDate, EventStart, CompanyId,CreatedUserId,
                      EventEnd, AllDay , lo.FirstName + ' ' + lo.LastName AS createdUser FROM dbo.CalendarEvent 
                 ce inner join Logins lo on ce.CreatedUserId=lo.LoginID where  ce.CompanyId=@comId Order by EventStart desc";

                    cmd.Parameters.AddWithValue("@comId", SqlDbType.Int).Value = strCompanyID;
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                string strViewQuote = "<img src='../Images/Edit.png' onclick='ViewCal(" + sdr["EventId"] + ")'/>";
                                var desc = sdr["Description"].ToString();
                                if (desc.Length > 15)
                                    desc = desc.Substring(0, 14).TrimEnd();
                              //  if (desc.Contains('/'))
                                //    desc = desc.Replace('/', '-');

                                desc = desc.Replace("\"", "");

                                if (desc.Contains('\n'))
                                    desc = desc.Replace('\n', ' ');


                                strOutput = strOutput + "[\""
                                  + sdr["EventId"] + "\","
                                   + "\"" + Convert.ToDateTime(sdr["CreatedDate"].ToString()) + "\","
                                    + "\"" + Convert.ToDateTime(sdr["EventStart"].ToString()) + "\","
                                    + "\"" + desc + "\","
                                            + "\"" + strViewQuote + "\"],";


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