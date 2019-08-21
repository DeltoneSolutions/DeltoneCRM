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
    public partial class FetchAllCompaniesForManage : System.Web.UI.Page
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
                    if (Session["USERPROFILE"] == "ADMIN")
                    {
                        cmd.CommandText = "SELECT CP.CompanyID, CP.CompanyName, LG.FirstName, LG.LastName, CP.CompanyWebsite, CP.Active FROM dbo.Companies CP, dbo.Logins LG WHERE CP.OwnershipAdminID  = LG.LoginID";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT CP.CompanyID, CP.CompanyName, LG.FirstName, LG.LastName, CP.CompanyWebsite, CP.Active FROM dbo.Companies CP, dbo.Logins LG WHERE CP.OwnershipAdminID  = LG.LoginID AND CP.OwnershipAdminID = " + Session["LoggedUserID"].ToString();
                    }
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            String strEdit = "<img src='../Images/Edit.png'  onclick='Edit(" + sdr["CompanyID"] + ")'/>";
                            String strView = "<img src='../Images/View.png' onclick='View(" + sdr["CompanyID"] + ")'/>";
                            strOutput = strOutput + "[\"" + sdr["CompanyID"] + "\"," + "\"" + sdr["CompanyName"] + "\"," + "\"" + sdr["FirstName"] + " " + sdr["LastName"] + "\"," + "\"" + sdr["CompanyWebsite"] + "\"," + "\"" + sdr["Active"] + "\"," + "\"" + strEdit + "\"],";

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