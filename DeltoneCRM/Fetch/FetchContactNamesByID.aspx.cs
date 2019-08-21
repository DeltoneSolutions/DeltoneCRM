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
    public partial class FetchContactNamesByID : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String strOutput = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT FirstName, LastName, Email, DEFAULT_Number ,STREET_AddressLine1 , STREET_City,STREET_PostalCode,POSTAL_Region FROM dbo.Contacts WHERE ContactID = " + Request.QueryString["CID"];
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strOutput = strOutput + sdr["FirstName"].ToString() + "|";
                                strOutput = strOutput + sdr["LastName"].ToString() + "|";
                                strOutput = strOutput + sdr["Email"].ToString() + "|";
                                strOutput = strOutput + sdr["DEFAULT_Number"].ToString() + "|"; 
                                var STREET_AddressLine1 = "";
                                var STREET_City = "";
                                var STREET_PostalCode = "";
                                var POSTAL_Region = "";

                                if (sdr["STREET_AddressLine1"] != DBNull.Value)
                                    STREET_AddressLine1 = sdr["STREET_AddressLine1"].ToString();
                                if (sdr["STREET_City"] != DBNull.Value)
                                    STREET_City = sdr["STREET_City"].ToString();
                                if (sdr["STREET_PostalCode"] != DBNull.Value)
                                    STREET_PostalCode = sdr["STREET_PostalCode"].ToString();
                                if (sdr["POSTAL_Region"] != DBNull.Value)
                                    POSTAL_Region = sdr["POSTAL_Region"].ToString();

                                strOutput = strOutput + STREET_AddressLine1 + "|";
                                strOutput = strOutput + STREET_City + "|";
                                strOutput = strOutput + STREET_PostalCode + "|";
                                strOutput = strOutput + POSTAL_Region;
                            }
                        }

                    }
                    conn.Close();

                }

            }
            Response.Write(strOutput);
        }
    }
}