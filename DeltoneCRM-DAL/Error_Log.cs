using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;



namespace DeltoneCRM_DAL
{   
    //Contains method definition relate to the system level Error handling 
   public  class Error_Log
    {
        public Error_Log()
        {
        }

        /// <summary>
        /// Log Application Errors in the DataBase 
        /// </summary>
        public void Log_Error(String TypeName,String Error,String stackStrace,DateTime DateCreated,String connString )
        {
          ///sql connection and write to the DataStore 
          String output = String.Empty;
          SqlConnection conn = new SqlConnection();
          conn.ConnectionString =connString;

          String strSQLUpdateStmt = "insert into DELTONE_ERROR(Error_Type,Message,Stack_Trace,CreatedDate,CreatedBy) values (@Err_Type,@Msg,@StackT,@date_Created,'SYS_GENERATED');";
            
          SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
          cmd.Parameters.AddWithValue("@Err_Type", TypeName);
          cmd.Parameters.AddWithValue("@Msg", Error);
          cmd.Parameters.AddWithValue("@StackT", stackStrace);
          cmd.Parameters.AddWithValue("@date_Created", DateCreated);
           
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

            





        }

    }
}
