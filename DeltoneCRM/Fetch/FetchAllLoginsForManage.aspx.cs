using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM.Fetch
{
    public partial class FetchAllLoginsForManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnAllItems());
        }

        protected string ReturnAllItems()
        {
            String strOutput = "{\"aaData\":[";
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Logins WHERE LoginID NOT IN (33,26,25,24,22,21,9,8,4)";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            String fullName = sdr["FirstName"] + " " + sdr["LastName"];
                            String AccessName = "";
                            String AccessLevel = sdr["AccessLevel"].ToString();

                            if (AccessLevel == "1")
                            {
                                AccessName = "Admin";
                            }
                            else{
                                AccessName = "Operator";
                            }


                            //EditOrder(OrderID, CompanyID, ContactID)
                            String strEdit = "<img src='../Images/Edit.png'  onclick='Edit(" + sdr["LoginID"] + ")'/>";
                            String strView = "<img src='../Images/View.png' onclick='View(" + sdr["LoginID"] + ")'/>";
                            strOutput = strOutput + "[\"" + sdr["LoginID"] + "\"," + "\"" + fullName + "\"," + "\"" + sdr["Username"] + "\"," + "\"" + sdr["EmailAddress"] + "\"," + "\"" + AccessName + "\"," + "\"" + sdr["Active"] + "\"," + "\"" + strEdit + "\"],";

                        }
                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));

                    }
                    strOutput = strOutput + "]}";
                    conn.Close();
                }

            }
            return strOutput;
        }
    }
}