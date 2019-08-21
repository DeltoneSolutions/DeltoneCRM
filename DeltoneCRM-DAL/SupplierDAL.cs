using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace DeltoneCRM_DAL
{
    public class SupplierDAL
    {
        private String CONNSTRING;


        public SupplierDAL(String connString)
        {
            CONNSTRING = connString;
        }


        //Get Delivery Cost given by SuppilerName and OrderID
        public String getDelCostBySupplierName(String Suppname, int OrderID)
        {

            String strCost = String.Empty;
            String delString = FetchSupplierDeliveryString(OrderID);

            if (!String.IsNullOrEmpty(delString))
            {
                String[] arrItems  =delString.Split('|');
                for (int i = 0; i < arrItems.Length; i++)
                {
                    if (!String.IsNullOrEmpty(arrItems[i]))
                    { 
                      
                        //Check wether First Line Items Contains 
                        if (i == 0)
                        {
                            String[] item = arrItems[i].Split(',');
                            if (item[0].Equals(Suppname, StringComparison.InvariantCultureIgnoreCase))
                            {
                                strCost = item[1].ToString(); //Index(1)
                            }
                        }
                        if (i != 0)
                        {
                            //For Subsuquent Lint Items
                            String[] sitem = arrItems[i].Split(',');
                            if (sitem[1].Equals(Suppname, StringComparison.InvariantCultureIgnoreCase))
                            {
                                strCost = sitem[2].ToString(); //Index(2)
                            }

                        }

                    }

                }
            }

            if (strCost.Equals("0") || strCost.Equals("0.00") || String.IsNullOrEmpty(strCost))
            {
                strCost = "0";
            }

            return strCost;

        }






        //This Method Fetch the SupplierDelivery Charges given by OrderID
        public String FetchSupplierDeliveryString(int OrderID)
        {

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT SuppDeltems FROM dbo.Orders WHERE OrderID=" + OrderID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["SuppDeltems"].ToString();

                    }
                }
            }
            conn.Close();


            return output;
        }


        //This Method Return Current Suppliers
        public String getAllSuppliers()
        {
            String output = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT * FROM Suppliers";
         
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = output + sdr["SupplierName"].ToString() + ":";

                    }
                }
            }
            conn.Close();

            return output;

        }


        //This MEthod Uodate Supplier given by details
        public String UpdateSupplier(int SuppID, String SupplierName, float deliveryCost, String ActInact)
        {

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "Update Suppliers SET SupplierName=@SuppName,StandardDeliveryCost=@DeliveyCost,Active=@ActInact, AlterationDateTime=CURRENT_TIMESTAMP WHERE SupplierID=@SuppId";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@SuppName", SupplierName);
            cmd.Parameters.AddWithValue("@DeliveyCost", deliveryCost);
            cmd.Parameters.AddWithValue("@SuppId", SuppID);
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

       

        //This Method Fetch the Supplier given by SupplierID
        public String getSupplier(int SupplierID)
        {

            String strSupplier= String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select * from dbo.Suppliers where SupplierID=" + SupplierID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strSupplier = strSupplier + sdr["SupplierID"].ToString() + ":" + sdr["SupplierName"].ToString() + ":" + sdr["StandardDeliveryCost"].ToString() + ":" + sdr["Active"].ToString();

                    }
                }
            }
            conn.Close();
            return strSupplier;

        }




    }
}
