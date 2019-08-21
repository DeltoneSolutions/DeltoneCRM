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
    public partial class getItemQty : System.Web.UI.Page
    {
        String stroutput = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            var strOutput = 0;
            var rep = Request["q"];
            if (rep == null)
                Response.Write(strOutput);
            else
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //Modification done here Add Supplier Name  //s.SupplierName //Write a Stored Proc for this
                        cmd.CommandText = "SELECT I.Quantity from dbo.Items I  WHERE I.SupplierItemCode=@supp" ;
                        cmd.Parameters.AddWithValue("@supp", rep);
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                if (sdr["Quantity"] != DBNull.Value)
                                    strOutput = Convert.ToInt32(sdr["Quantity"].ToString());
                            }


                        }
                        conn.Close();
                    }
                }

                Response.Write(strOutput);
            }


        }



        //This Method Load the Items given by StrQuery

    }
}