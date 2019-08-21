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
    public partial class FetchAllPromoItemsForManage : System.Web.UI.Page
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
                    cmd.CommandText = "SELECT * FROM dbo.PromoDetails";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //EditOrder(OrderID, CompanyID, ContactID)
                            Decimal strCost = (Decimal)(sdr["PromoCost"]);
                            String strEdit = "<img src='../Images/Edit.png'  onclick='Edit(" + sdr["PromoID"] + ")'/>";
                            String strView = "<img src='../Images/View.png' onclick='View(" + sdr["PromoID"] + ")'/>";
                            strOutput = strOutput + "[\"" + sdr["PromoID"] + "\"," + "\"" + sdr["PromoCode"] + "\"," +  "\"" + sdr["PromoItem"] + "\"," + "\"" + string.Format("{0:C}", strCost) + "\"," + "\"" + sdr["PromoQty"] + "\"," + "\"" + sdr["Active"] + "\"," +  "\"" + strEdit + "\"],";

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