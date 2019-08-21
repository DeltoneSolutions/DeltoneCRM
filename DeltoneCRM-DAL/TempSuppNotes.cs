using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;


namespace DeltoneCRM_DAL
{  

   //Class Supplier Notes
    public class SupplierNote
    {

        public String ID { get; set; }  
        public String SuppName { get; set; }
        public String Note { get; set; }
    }

    //End Supplier Notes

   public  class TempSuppNotes
    {
        private String CONNSTRING;
        //Constructor
        public TempSuppNotes(String strConnString)
        {
            CONNSTRING = strConnString;
        }

        //This Method Fetch the Supplier Given by id,sname
        public String getSupplierNoteRow(String id, String sname)
        {

            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT * FROM dbo.Temp_SuppNote where ID=@id AND SuppName=@sname";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@sname", sname);
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            output = sdr["Note"].ToString();
                        }
                    }
                    else
                    {
                        output = String.Empty;
                    }
                }
            }
            conn.Close();

            return output;
        }


        //This Method Inser the Temp Note given by Details
        public String insertNote(String id, String suppname, String Note)
        {
            String OutPut = String.Empty;
            int RowsEffected =-1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = " insert into dbo.Temp_SuppNote values(@Id,@suppname,@Note)";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@suppname", suppname);
            cmd.Parameters.AddWithValue("@Note", Note);
           
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            OutPut = RowsEffected.ToString();
            return OutPut;
        }


        //This Mehtod Remove Supplier given by Name
        public String RemoveSupplier(String suppname)
        {
            String strOutOut = String.Empty;
            int RowsEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "delete from dbo.Temp_SuppNote where SuppName=@suppname";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@suppname", suppname);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            strOutOut = RowsEffected.ToString();
            return strOutOut;
        }


        //Check wether IF Supplier Exsists or not 
        public String  IsSuppExsists(String suppname,String ID)
        {
            String strOutOut=String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT * FROM dbo.Temp_SuppNote where ID=@id AND SuppName=@sname";
            using (SqlCommand cmd = new SqlCommand())
            {    
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@sname", suppname);
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            strOutOut = strOutOut + sdr["ID"].ToString() + ":" + sdr["SuppName"].ToString() + ":" + sdr["Note"].ToString();
                        }
                        strOutOut = "YES";
                    }
                    else
                    {
                        strOutOut = "NO";
                    }
                }
            }
            conn.Close();
            return strOutOut;

        }

        //This Method fetch all the Temp Supp Notes 
        public String getTempSuppNotes(String id)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT * FROM dbo.Temp_SuppNote where ID='" + id +"'";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = output + sdr["ID"].ToString() + ":" + sdr["SuppName"].ToString() + ":" + sdr["Note"].ToString() ;
                        output = output + "|";
                    }
                }
            }
            conn.Close();

            return output;
        }


       //This Method Update Supplier Note given by Details
        public String UpdateSuppNotes(String id, String suppname, String Note)
        {

            String strOutOut = String.Empty;
            int RowsEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "UPDATE dbo.Temp_SuppNote SET Note=@Note WHERE ID=@id AND SuppName=@suppname";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@Note", Note);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@suppname", suppname);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Console.Write(ex.Message.ToString());
            }

            strOutOut = RowsEffected.ToString();
            return strOutOut;

        }



       //this Method Fetch the Supplier Notes as a Dictionary<String,String>
        public Dictionary<String, String> getNotesObject(int OrderID)
        {
            Dictionary<String, String> di_notes = new Dictionary<string, string>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "SELECT  * FROM dbo.SupplierNotes WHERE OrderID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        di_notes.Add(sdr["Suppliername"].ToString(), sdr["Notes"].ToString());
                    }
                }
            }
            conn.Close();

            return di_notes;
        }

    }
}
