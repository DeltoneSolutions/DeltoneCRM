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
    public partial class FetchUserCalendarEvents : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            String searchTerm = Request.QueryString["term"].ToString();
            //String searchTerm = "In";
            Response.Write(ReturnAllCompanies(searchTerm));

        }

        protected string ReturnAllCompanies(String strSearchTerm)
        {
            String strOutput = "[";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT EventId,Title FROM dbo.CalendarEvent WHERE ( Title LIKE '%" + strSearchTerm + "%' or Description LIKE '%" + strSearchTerm + "%')  AND CreatedUserId IN (" + Session["LoggedUserID"] + ")";
                   

                    cmd.Connection = conn;
                    conn.Open();


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            var combine = sdr["title"].ToString();
                            strOutput = strOutput + "{\"id\": \"" + sdr["EventId"] + "\", \"value\": \"" + combine + "\"},";
                        }
                    }

                    int Length = strOutput.Length;
                    strOutput = strOutput.Substring(0, (Length - 1));
                    conn.Close();
                }
                strOutput = strOutput + "]";
            }

            return strOutput;
        }
    }
}