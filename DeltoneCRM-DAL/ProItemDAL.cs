using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;


namespace DeltoneCRM_DAL
{
    public class ProItemDAL
    {
        private String CONNSTRING;

        public ProItemDAL(String connString)
        {
            CONNSTRING = connString;
        }


        //This Method Fetch Promotional Item given by OrderID
        public String getPromotionlCost(int OrderID)
        {
            String output = String.Empty;
            Double ProCost = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT  * FROM dbo.OrderProItems WHERE OrderID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            int qty = Int32.Parse(sdr["PromoQty"].ToString());
                            double procost = float.Parse(sdr["ProCost"].ToString());
                            double shippingcost = float.Parse(sdr["ShippingCost"].ToString());
                            double lineCost = (qty * procost) + shippingcost;
                            ProCost = ProCost + lineCost;
                        }
                        ProCost = Math.Round(ProCost, 2);
                    }
                    else
                    {
                        ProCost = 0;
                    }
                }
            }
            conn.Close();
            return ProCost.ToString();
        }

        //This Method Update promotionalitem
        public String udpatePromotionalitem(int promoID,String promoname,int Qty,float promoCost,float ShippingCost, String ActInact)
        {
            

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "Update dbo.PromoDetails set promoitem=@itemname,promocost=@promocost,promoqty=@qty,Shippingcost=@shippingcost, Active=@ActInact where promoid=@PromoID" ;
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@itemname", promoname);
            cmd.Parameters.AddWithValue("@promocost", promoCost);
            cmd.Parameters.AddWithValue("@shippingcost", ShippingCost);
            cmd.Parameters.AddWithValue("@qty", Qty);
            cmd.Parameters.AddWithValue("@PromoID", promoID);
            cmd.Parameters.AddWithValue("@ActInact", ActInact);

            try
            {
                conn.Open();
                output = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                output = ex.Message.ToString();
            }
            return output;

        }

        public String getPromoItem(int PromoID)
        {
            String strPromoItem = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select * from dbo.PromoDetails where promoid=" + PromoID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strPromoItem = strPromoItem + sdr["promoid"].ToString() +":" + sdr["promoitem"].ToString() + ":" + sdr["promoqty"].ToString() + ":" + sdr["promocost"].ToString() + ":" + sdr["Shippingcost"].ToString() + ":" + sdr["Active"].ToString();

                    }
                }
            }
            conn.Close();
            return strPromoItem;
        }



    }
}
