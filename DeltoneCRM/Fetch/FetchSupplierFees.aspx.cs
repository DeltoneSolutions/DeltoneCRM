using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

namespace DeltoneCRM.Fetch
{
    public partial class FetchSupplierFees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(LoadSuppliers(Request.QueryString["term"].ToString()));
            //Response.Write(LoadSuppliers("a"));
        }

        //This Method Load the Supplier Frees given by StrQuery
        protected String LoadSuppliers(String strQuery)
        {
            String strOutput = "[";


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT * FROM dbo.Suppliers WHERE SupplierName LIKE '%" + strQuery + "%'";

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strOutput = strOutput + "{\"ID\":\"" + sdr["SupplierID"] + "\",\"value\":\"" + sdr["SupplierName"] + "\",\"StandardDeliveryCost\":\"" + sdr["StandardDeliveryCost"] + "\"},";
                        }

                        int Length = strOutput.Length;
                        strOutput = strOutput.Substring(0, (Length - 1));

                    }
                    conn.Close();

                }
            }

            strOutput = strOutput + "]";
            return strOutput;

        }
        //End Load Items given by StrQuery
    }
}