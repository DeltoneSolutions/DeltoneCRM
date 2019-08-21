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
    public partial class getstatsforallreps : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public List<String> getOrderListStartAndEnd(String RepName, String startDate, String endDate)
        {

            List<String> OrderList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT OrderID FROM dbo.Orders WHERE CreatedDateTime between  "
                            + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 0";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT OrderID FROM dbo.Orders 
                                   WHERE AccountOwner = '" + RepName + "' AND CreatedDateTime between  "
                                                           + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" +
                                                           " AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderList.Add(sdr["orderID"].ToString());
                        }
                    }
                }
            }

            return OrderList;
        }
    }
}