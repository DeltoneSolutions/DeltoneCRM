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
    public partial class FetchPromoItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(LoadPromoItems(Request.QueryString["term"].ToString()));
            //Response.Write(LoadPromoItems("i"));
        }

        //This Method Load the Promo Fees given by StrQuery
        protected String LoadPromoItems(String strQuery)
        {
            String strOutput = "[";


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT * FROM dbo.PromoDetails WHERE promoitem LIKE '%" + strQuery + "%'";

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strOutput = strOutput + "{\"ID\":\"" + sdr["PromoID"] + "\",\"promodesc\":\"" + sdr["PromoItem"] + "\",\"promocost\":\"" + sdr["PromoCost"] + "\",\"qty\":\"" + sdr["promoqty"] + "\"},";
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