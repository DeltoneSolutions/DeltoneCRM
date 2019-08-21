using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;





namespace DeltoneCRM_DAL
{
    public class CusDeliveryDAL
    {

        String CONNSTRING;

        public CusDeliveryDAL(String strConnString)
        {
            CONNSTRING = strConnString;
        }

        public String UpateDeliveryItems(int delItemID, String delName, float DelCost, String ActInact)
        {

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "Update dbo.DeliveryDetails set deliverydetails=@deliverycostname,deliverycost=@deliverycost,Active=@ActInact,AlterationDateTime=CURRENT_TIMESTAMP where deliveryid=@deliveryid";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@deliverycostname", delName);
            cmd.Parameters.AddWithValue("@deliverycost", DelCost);
            cmd.Parameters.AddWithValue("@deliveryid", delItemID);
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

    }
}
