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
    public partial class FetchDeliveryFees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(LoadDeliveryFees(Request.QueryString["term"].ToString()));
        }

        //This Method Load the Promo Fees given by StrQuery
        protected String LoadDeliveryFees(String strQuery)
        {
            String strOutput = "[";


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT * FROM dbo.DeliveryDetails WHERE deliverydetails LIKE '%" + strQuery + "%'";

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strOutput = strOutput + "{\"ID\":\"" + sdr["deliveryid"] + "\",\"deliverydetails\":\"" + sdr["deliverydetails"] + "\",\"deliverycost\":\"" + sdr["deliverycost"] + "\"},";
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