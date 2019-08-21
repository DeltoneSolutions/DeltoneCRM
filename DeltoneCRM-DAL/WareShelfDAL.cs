using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
    public class WareShelfDAL
    {
        private String CONNSTRING;

        public WareShelfDAL(String connString)
        {
            CONNSTRING = connString;
        }

        public void CreateShelf(WShelf obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO WShelf(ColumnName, RowNumber, CreatedDate) 
                                     VALUES(@columnName, @rowNumber, CURRENT_TIMESTAMP)";
            
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@columnName", SqlDbType.NVarChar).Value = obj.ColumnName;
                cmd.Parameters.Add("@rowNumber", SqlDbType.NVarChar).Value = obj.RowNumber;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public WShelf ISRowAndColumnExist(string columnName, string rowNumber)
        {

            var obj = new WShelf();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, ColumnName, RowNumber,CreatedDate FROM dbo.WShelf WHERE ColumnName=@columnName AND RowNumber=@rowNumber";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Add("@columnName", SqlDbType.NVarChar).Value = columnName;
                cmd.Parameters.Add("@rowNumber", SqlDbType.NVarChar).Value = rowNumber;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    obj.Id = Convert.ToInt32(reader["Id"]);
                    obj.ColumnName = Convert.ToString(reader["ColumnName"]);
                    obj.RowNumber = Convert.ToString(reader["RowNumber"]);
                    obj.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                }

               
            }
            conn.Close();

            return obj;

        }

        public void UpdateShelf(WShelf obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE WShelf SET ColumnName=@columnName, RowNumber=@rowNumber, ModifiedDate=CURRENT_TIMESTAMP WHERE Id=@Id";


            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = obj.Id;
                cmd.Connection = conn;
                cmd.Parameters.Add("@columnName", SqlDbType.NVarChar).Value = obj.ColumnName;
                cmd.Parameters.Add("@rowNumber", SqlDbType.NVarChar).Value = obj.RowNumber;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public IList<WShelf> GetAllShelfs()
        {
            var list = new List<WShelf>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, ColumnName, RowNumber, CreatedDate FROM dbo.WShelf ";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var eventCal = new WShelf()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ColumnName = Convert.ToString(reader["ColumnName"]),
                        RowNumber = Convert.ToString(reader["RowNumber"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString())
                    };

                    list.Add(eventCal);
                }
            }
            conn.Close();

            return list;
        }

        public WShelf GetShelfById(int shelfId)
        {
            var obj = new WShelf();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, ColumnName, RowNumber, CreatedDate FROM dbo.WShelf WHERE Id=@Id";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = shelfId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    obj.Id = Convert.ToInt32(reader["Id"]);
                    obj.ColumnName = Convert.ToString(reader["ColumnName"]);
                    obj.RowNumber = Convert.ToString(reader["RowNumber"]);
                    obj.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                }
            }
            conn.Close();

            return obj;
        }

    }


    public class WShelf
    {
        public int Id { get; set; }
        public string ColumnName { get; set; }
        public string RowNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
