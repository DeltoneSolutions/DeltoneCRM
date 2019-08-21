using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace DeltoneCRM_DAL
{
    public class CommissionDAL
    {
        private String CONNSTRING;

        public CommissionDAL(String connString)
        {
            CONNSTRING = connString;
        }


        /// <summary>
        /// Remove Commission Entry given by type='ORDER'/'CREDITNOTE'
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public String RemoveCommissionEntry(int ID,String strType)
        {
           
            String strRowsEffected = String.Empty;
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSQLInsertStmt = "";

            if (strType.Equals("ORDER"))
            {
                strSQLInsertStmt = "DELETE FROM dbo.Commision WHERE OrderID=@OrderID AND Type='ORDER'";
            }
            if (strType.Equals("CREDITNOTE"))
            {
                strSQLInsertStmt = "DELETE FROM dbo.Commision WHERE OrderID=@OrderID AND Type='CREDITNOTE'";
            }
            
            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", ID);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }

            return strRowsEffected;

        }

        public String RemoveDummyCommissionEntry(int ID, String strType)
        {

            String strRowsEffected = String.Empty;
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;

            String strSQLInsertStmt = "";

            if (strType.Equals("ORDER"))
            {
                strSQLInsertStmt = "DELETE FROM dbo.DummyCommision WHERE OrderID=@OrderID AND Type='ORDER'";
            }
            if (strType.Equals("CREDITNOTE"))
            {
                strSQLInsertStmt = "DELETE FROM dbo.DummyCommision WHERE OrderID=@OrderID AND Type='CREDITNOTE'";
            }

            SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", ID);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }

            return strRowsEffected;

        }
    }
}
