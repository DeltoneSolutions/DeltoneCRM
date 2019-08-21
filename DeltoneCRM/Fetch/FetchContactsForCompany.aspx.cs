using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Data;

namespace DeltoneCRM.Fetch
{
    public partial class FetchContactsForCompany : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strOutput = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT ContactID, FirstName, LastName FROM dbo.Contacts WHERE CompanyID = " + Request.QueryString["CID"];
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {

                                strOutput = strOutput + sdr["ContactID"].ToString() + "|";
                                strOutput = strOutput + sdr["FirstName"].ToString() + "|";
                                strOutput = strOutput + sdr["LastName"].ToString() + "~";

                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }

                    }
                    conn.Close();

                }

            }
            Response.Write(strOutput);
        }

    }
}