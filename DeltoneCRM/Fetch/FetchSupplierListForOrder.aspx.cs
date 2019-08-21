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
    public partial class FetchSupplierListForOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(ReturnAllSuppliers());
        }

        protected string ReturnAllSuppliers()
        {
            String strOutput = "";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT * FROM dbo.Suppliers";

                    cmd.Connection = conn;
                    conn.Open();


                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                strOutput = strOutput + sdr["SupplierName"] + "|";
                            }

                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }
                    }
                    conn.Close();
            
                }
            }

            return strOutput;
        }
    }
}