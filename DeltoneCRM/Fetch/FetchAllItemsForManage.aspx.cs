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
    public partial class FetchAllItemsForManage : System.Web.UI.Page
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
                    cmd.CommandText = "SELECT SU.SupplierName, IT.ItemID, IT.Description, IT.OEMCode, IT.COG, IT.ManagerUnitPrice, IT.SupplierItemCode, IT.Active FROM dbo.Items IT, dbo.Suppliers SU WHERE IT.SupplierID = SU.SupplierID";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //EditOrder(OrderID, CompanyID, ContactID)
                             Decimal strCOG;
                             if (!String.IsNullOrEmpty(sdr["cog"].ToString()) && sdr["cog"] != System.DBNull.Value)
                             {
                                 strCOG = (Decimal)(sdr["cog"]);
                             }
                             else
                             {
                                 strCOG = 0;
                             }
                          
                            Decimal strResellPrice; 
                                
                              if(!String.IsNullOrEmpty(sdr["ManagerUnitPrice"].ToString()) && sdr["ManagerUnitPrice"] != System.DBNull.Value)
                              {
                                  strResellPrice = (Decimal)(sdr["ManagerUnitPrice"]);
                              }
                              else
                              {
                                  strResellPrice = 0;
                              }

                                
                            String strEdit = "<img src='../Images/Edit.png'  onclick='Edit(" + sdr["ItemID"] + ")'/>";
                            String strView = "<img src='../Images/View.png' onclick='View(" + sdr["ItemID"] + ")'/>";
                            strOutput = strOutput + "[\"" + sdr["ItemID"] + "\"," + "\"" + sdr["SupplierName"] + "\"," + "\"" + sdr["SupplierItemCode"] + "\"," + "\"" + sdr["Description"].ToString() + "\"," + "\"" + string.Format("{0:C}", strCOG) + "\"," + "\"" + string.Format("{0:C}", strResellPrice) + "\"," + "\"" + sdr["Active"] + "\"," + "\"" + strEdit + "\"],";

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