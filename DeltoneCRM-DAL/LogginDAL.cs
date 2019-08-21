using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DeltoneCRM_DAL
{
   public class LogginDAL
   {

       public String CONNSTRING;

       public LogginDAL(String connString)
       {
           CONNSTRING = connString;
       }


       public String getOrderGuid(int OrderID)
       {
           String output = String.Empty;
           SqlConnection conn = new SqlConnection();
           conn.ConnectionString = CONNSTRING;
           String strSqlOrderStmt = "select XeroGuid from Orders where OrderID=" + OrderID;

           using (SqlCommand cmd = new SqlCommand())
           {

               cmd.CommandText = strSqlOrderStmt;
               cmd.Connection = conn;
               conn.Open();
               using (SqlDataReader sdr = cmd.ExecuteReader())
               {
                   while (sdr.Read())
                   {

                       output = sdr["XeroGuid"].ToString();

                   }
               }
           }
           conn.Close();

           return output;
       }





       //This Message Fetch from the Log
       public String FetchFromLog(int OrderID)
       {
           String output = String.Empty;
           String get_strOutPut = String.Empty;
           SqlConnection conn = new SqlConnection();
           conn.ConnectionString = CONNSTRING;

           try
           {

               String strSqlContactStmt = "Select * from DELTONECRM_LOG where OrderID=" + OrderID;
               using (SqlCommand cmd = new SqlCommand())
               {
                   cmd.CommandText = strSqlContactStmt;
                   cmd.Connection = conn;
                   conn.Open();

                   using (SqlDataReader sdr = cmd.ExecuteReader())
                   {
                       while (sdr.Read())
                       {
                           if (sdr.HasRows)
                           {
                               get_strOutPut = sdr["Msg"].ToString() + ":" + sdr["Soucefile"].ToString() + ":" + sdr["Method"].ToString() + ":" + sdr["Result"].ToString();
                           }

                       }
                   }
               }
               conn.Close();
           }
           catch (Exception ex)
           {
              String Error= ex.StackTrace.ToString() + ":" + ex.Message.ToString();
           }


          return output;
            
       }


       /*This Method Returns rows in the Log given by OrderID*/
       public bool Rows_Exsists(int OrderID)
       {
           bool output=false;
           int count = -1;
           SqlConnection conn = new SqlConnection();
           conn.ConnectionString = CONNSTRING;
           String strSqlContactStmt = "SELECT COUNT(*) as Row_Count  FROM DELTONECRM_LOG WHERE OrderID=" + OrderID;
           using (SqlCommand cmd = new SqlCommand())
           {
               cmd.CommandText = strSqlContactStmt;
               cmd.Connection = conn;
               conn.Open();
               using (SqlDataReader sdr = cmd.ExecuteReader())
               {
                   while (sdr.Read())
                   {
                       count =   Int32.Parse(sdr["Row_Count"].ToString());
                   }
               }
           }
           conn.Close();

           output = (count > 0) ? true : false;


           return output;
       }



       /*This Method Updates the Log given by OrderID */
       public String UpdateLog(int OrderID, String strMessage, String filename, String method_name,String result)
       {
           String output = String.Empty;
           SqlConnection conn = new SqlConnection();
           conn.ConnectionString = CONNSTRING;
           String strSQLInsertStmt = "Update DELTONECRM_LOG set Msg=@msg,CreatedDateTime=CURRENT_TIMESTAMP,CreatedBy='System Generated',Soucefile=@filename,Method=@method,Result=@result where OrderID=@orderid";
        
           SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
           cmd.Parameters.AddWithValue("@orderid", OrderID);
           cmd.Parameters.AddWithValue("@msg", strMessage);
           cmd.Parameters.AddWithValue("@filename", filename);
           cmd.Parameters.AddWithValue("@method", method_name);
           cmd.Parameters.AddWithValue("@result", result);

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





       //This Method Write to the Log with
       public String  WriteTOLog(int OrderID, String strMessage,String filename,String method_name,String result)
       {
           String   insert_strOutput = String.Empty;
           SqlConnection conn = new SqlConnection();
           conn.ConnectionString = CONNSTRING;
           String strSQLInsertStmt = "insert into DELTONECRM_LOG(Msg,ORDERID,Method,CreatedDateTime,CreatedBy,Result) values(@msg,@orderid,@method,CURRENT_TIMESTAMP,'System Generated',@result);";
           SqlCommand cmd = new SqlCommand(strSQLInsertStmt, conn);
           cmd.Parameters.AddWithValue("@orderid", OrderID);
           cmd.Parameters.AddWithValue("@msg", strMessage);
           cmd.Parameters.AddWithValue("@filename", filename);
           cmd.Parameters.AddWithValue("@method", method_name);

           cmd.Parameters.AddWithValue("@result", result);
           
           try
           {
               conn.Open();
               insert_strOutput = cmd.ExecuteNonQuery().ToString();
               conn.Close();
           }
           catch (Exception ex)
           {
               if (conn != null) { conn.Close(); }
               insert_strOutput = ex.Message.ToString();
           }
           return insert_strOutput;
       }


       //This Method List Errors Retunred for the Orders
       public String fetchErrorLog(int OrderID)
       {
           String strOutPut = String.Empty;

           return strOutPut;
       }


   }
}
