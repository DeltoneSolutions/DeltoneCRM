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
    public partial class FetchLinkCompanyAdminLoad : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnLinkComanies());
        }

        protected string ReturnLinkComanies()
        {
            String strOutput = "{\"aaData\":[";

            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = @"SELECT distinct CP.CompanyID , CP.CompanyName, CP.Active
                                FROM dbo.Companies CP
                                INNER JOIN CompanyLinked CPL ON CPL.CompanyIdLinked =CP.CompanyID ";
                    cmd.Connection = con;
                   
                    cmd.Connection.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                var comId = sdr["CompanyID"].ToString();
                                var CoName = sdr["CompanyName"].ToString();
                                var active = sdr["Active"].ToString();
                                var linked = "Y";

                                


                                var Link = "<input name='selectchk'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                var LinkLocked = "<input id='" + sdr["CompanyID"].ToString() + "' name='selectlock'  value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                if (linked == "Y")
                                    LinkLocked = "<input name='selectlock' checked='true' value='" + sdr["CompanyID"].ToString() + "' type='checkbox' />";

                                strOutput = strOutput + "[\"" + Link + "\"," + "\"" + comId + "\","
                                               + "\"" + CoName + "\","
                                            + "\"" + active + "\","
                                             + "\"" + linked + "\","
                                        + "\"" + LinkLocked + "\"],";
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