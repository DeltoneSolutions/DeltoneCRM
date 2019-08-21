using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Reports.Queries
{
    public partial class getOrderedProducts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(GetALLOrderedOrders());
        }


        //Method returns all Approved Orders
        protected String GetALLOrderedOrders()
        {
            String strOutput = "{\"data\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"select co.CompanyName,cn.FirstName + ' ' + cn.LastName as contactName, cn.DEFAULT_AreaCode + '' + cn.DEFAULT_Number as tlenumber, 
                                         ord.OrderedDateTime,ord.OrderID,it.SupplierItemCode,it.Description, OT.Quantity
                                         as itemQty from Ordered_Items OT INNER JOIN
	
	                                  Items IT ON IT.ItemID = OT.ItemCode 
	                                   INNER JOIN  Orders ord ON ord.OrderID = OT.OrderID
	                                   INNER JOIN  Companies co ON co.CompanyID = ord.CompanyID 
	                                   INNER JOIN  Contacts cn ON cn.CompanyID = co.CompanyID


	
	";
                   
                    cmd.Connection = conn;
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow drow in dt.Rows)
                            {

                                 strOutput = strOutput + "[\""
                                    + drow["CompanyName"].ToString() + "\","
                                     + "\"" + (drow["contactName"].ToString()) + "\","
                                      + "\"" + (drow["tlenumber"].ToString()) + "\","
                                      + "\"" + (drow["OrderID"].ToString()) + "\","
                                       + "\"" + (drow["OrderedDateTime"].ToString()) + "\","
                                        + "\"" + (drow["SupplierItemCode"].ToString()) + "\","
                                          + "\"" + (drow["Description"].ToString()) + "\","
                                              + "\"" + drow["itemQty"].ToString() + "\"],";
                            }
                            int Length = strOutput.Length;
                            strOutput = strOutput.Substring(0, (Length - 1));
                        }
                        
                    }

                    //using (SqlDataReader sdr = cmd.ExecuteReader())
                    //{
                    //    if (sdr.HasRows)
                    //    {

                    //        while (sdr.Read())
                    //        {
                    //            strOutput = strOutput + "[\""
                    //                + sdr["CompanyName"].ToString() + "\","
                    //                 + "\"" + (sdr["contactName"].ToString()) + "\","
                    //                  + "\"" + (sdr["tlenumber"].ToString()) + "\","
                    //                  + "\"" + (sdr["OrderID"].ToString()) + "\","
                    //                   + "\"" + (sdr["OrderedDateTime"].ToString()) + "\","
                    //                    + "\"" + (sdr["SupplierItemCode"].ToString()) + "\","
                    //                      + "\"" + (sdr["Description"].ToString()) + "\","
                    //                          + "\"" + sdr["itemQty"].ToString() + "\"],";

                    //        }
                    //        int Length = strOutput.Length;
                    //        strOutput = strOutput.Substring(0, (Length - 1));
                    //    }

                    //}
                    strOutput = strOutput + "]}";
                    conn.Close();
                }

            }

            return strOutput;

        }
    }
}