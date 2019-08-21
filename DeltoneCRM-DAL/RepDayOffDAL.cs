using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltoneCRM_DAL
{
    public class RepDayOffDAL
    {
        private String CONNSTRING;

        public RepDayOffDAL(String connString)
        {
            CONNSTRING = connString;
        }

        public void InsertRepDayOff(RepDayOff obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO RepDayOff(UserId, DayOff, CreatedUserId, CreatedDate, TargetId,Approved) 
                                     VALUES(@userId, @dayOff, @createduserId,CURRENT_TIMESTAMP ,@targetId,@approved)";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@targetId", SqlDbType.NVarChar).Value = obj.TargetId;
                cmd.Parameters.Add("@approved", SqlDbType.Bit).Value = obj.IsApproved;
                cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = obj.UserId;
                cmd.Parameters.Add("@dayOff", SqlDbType.DateTime).Value = obj.DayOff;
                cmd.Parameters.Add("@createduserId", SqlDbType.NVarChar).Value = obj.CreateduserId;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void InsertRepAttendance(string repName)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO AttendanceLog(Name, CreatedDate,ModifiedDate,Active ) 
                                     VALUES(@username, CURRENT_TIMESTAMP ,CURRENT_TIMESTAMP,@acti)";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@username", SqlDbType.NVarChar).Value = repName;
                cmd.Parameters.Add("@acti", SqlDbType.Bit).Value = true;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void DeleteAtteName(int Id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM AttendanceLog WHERE (Id = @Idrep)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@Idrep", SqlDbType.Int).Value = Id;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public List<AtteandanceLog> GetAttendanceReps()
        {

            var listReps = new List<AtteandanceLog>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT name,Id FROM dbo.AttendanceLog";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var name = reader["name"].ToString();
                    var obj = new AtteandanceLog()
                    {
                        Id=Convert.ToInt32(reader["Id"].ToString()),
                        Name=reader["name"].ToString()
                    };

                    listReps.Add(obj);
                }
            }
            conn.Close();

            return listReps;
        }

        public void UpdateStatsTargetConfig(int Id, bool reached)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"UPDATE StatsTargetConfig SET Reached=@reached WHERE Id=@Id";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                cmd.Connection = conn;
                cmd.Parameters.Add("@reached", SqlDbType.Bit).Value = reached;

                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public class AtteandanceLog
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public void UpdateTargetConfig(int configId, bool status)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            using (SqlCommand cmd = new SqlCommand("dbo.spupdatestatsconfig", conn))
            {
                cmd.Parameters.Add("@configtargetId", SqlDbType.Int).Value = configId;
                cmd.Parameters.Add("@isreached", SqlDbType.Bit).Value = status;
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }

        public void CreateAndUpdateInsertred()
        {
            SqlConnection con = new SqlConnection();
           // con.ConnectionString=ConnectionState.
        }

        public void InsertIntoTargetConfig(StatsTargetConfig obj, int userId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            using (SqlCommand cmd = new SqlCommand("dbo.spinsertstatsconfig", conn))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = obj.TargetTitle;
                cmd.Parameters.Add("@amount", SqlDbType.Money).Value = obj.TargetAmount;
                cmd.Parameters.Add("@targetday", SqlDbType.DateTime).Value = obj.TargetDay;
                cmd.Parameters.Add("@reached", SqlDbType.Bit).Value = obj.IsReached;
                cmd.Parameters.Add("@createduserid", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@createdate", SqlDbType.DateTime).Value = DateTime.Now;
                cmd.Parameters.Add("@createdId", SqlDbType.Int).Direction = ParameterDirection.Output;

                conn.Open();
                cmd.ExecuteNonQuery();
                int contractID = Convert.ToInt32(cmd.Parameters["@createdId"].Value);
                conn.Close();
            }

        }

        public void DeleteTargetConfig(int targetId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            using (SqlCommand cmd = new SqlCommand("dbo.spdeletestatsconfig", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@deleteid", SqlDbType.Int).Value = targetId;
               // cmd.Connection = conn;
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
        }

        public IList<StatsTargetConfig> GetAllTargetItems()
        {
            var result = new List<StatsTargetConfig>();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = CONNSTRING;
            using (SqlCommand cmd = new SqlCommand("dbo.spgetstatssonfigtargets", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var obj = new StatsTargetConfig()
                 {
                     Id = Convert.ToInt32(sdr["Id"]),
                     IsReached = Convert.ToBoolean(sdr["Reached"]),
                     TargetDay = Convert.ToDateTime(sdr["TargetDay"]),
                     TargetAmount = Convert.ToDecimal(sdr["Amount"]),
                     TargetTitle = sdr["Title"].ToString()

                 };

                    result.Add(obj);
                }
            }
            con.Close();

            return result;
        }

        public void CreateStatsConfig(StatsTargetConfig obj, int userId)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO StatsTargetConfig(Title, Amount, CreatedUserId, CreatedDate, TargetDay,Reached) 
                                     VALUES(@targettitle, @targetamount, @createduserId,CURRENT_TIMESTAMP ,@targetday,@reached)";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@targettitle", SqlDbType.NVarChar).Value = obj.TargetTitle;
                cmd.Parameters.Add("@reached", SqlDbType.Bit).Value = obj.IsReached;
                cmd.Parameters.Add("@targetamount", SqlDbType.Money).Value = obj.TargetAmount;
                cmd.Parameters.Add("@targetday", SqlDbType.DateTime).Value = obj.TargetDay;
                cmd.Parameters.Add("@createduserId", SqlDbType.NVarChar).Value = userId;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void DeleteStatsConfig(int Id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM StatsTargetConfig WHERE (Id = @Idrep)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@Idrep", SqlDbType.Int).Value = Id;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public IList<StatsTargetConfig> GetAlltargets()
        {
            var result = new List<StatsTargetConfig>();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, Title, Amount, CreatedUserId, CreatedDate,TargetDay,Reached FROM dbo.StatsTargetConfig";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var statsConfig = new StatsTargetConfig()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        TargetDay = Convert.ToDateTime(reader["TargetDay"]),
                        TargetAmount = Convert.ToDecimal(reader["Amount"]),
                        IsReached = Convert.ToBoolean(reader["Reached"]),
                        TargetTitle = reader["Title"].ToString(),

                    };

                    result.Add(statsConfig);
                }
            }
            conn.Close();

            return result;
        }

        public void InsertHolidayByMonth(AusHoliday obj)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"INSERT INTO AusHoilday(Year, Month, HolidayDate, CreatedDate) 
                                     VALUES(@year, @month, @holidayDate,CURRENT_TIMESTAMP)";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.Add("@year", SqlDbType.Int).Value = obj.Year;
                cmd.Parameters.Add("@month", SqlDbType.Int).Value = obj.Month;
                cmd.Parameters.Add("@holidayDate", SqlDbType.DateTime).Value = obj.HolidayDate;
                cmd.CommandText = strSqlContactStmt;
                cmd.ExecuteNonQuery();

            }
            conn.Close();
        }

        public void DeleteHolidayByMonth(int Id)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM AusHoilday WHERE (Id = @Idrep)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@Idrep", SqlDbType.Int).Value = Id;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void DeleteRepDayOff(int repDayoffId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"DELETE FROM RepDayOff WHERE (Id = @Idrep)";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = strSqlContactStmt;
                cmd.Parameters.Add("@Idrep", SqlDbType.Int).Value = repDayoffId;
                cmd.ExecuteNonQuery();
            }
            conn.Close();
        }

        public IList<RepDayOff> GetAllDayOffList()
        {
            var listRepDayOff = new List<RepDayOff>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, UserId, DayOff, CreatedUserId, CreatedDate,TargetId FROM dbo.RepDayOff";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var repDayfff = new RepDayOff()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        DayOff = Convert.ToDateTime(reader["DayOff"]),
                        TargetId = Convert.ToInt32(reader["TargetId"]),
                        UserId = Convert.ToInt32(reader["UserId"]),

                    };

                    listRepDayOff.Add(repDayfff);
                }
            }
            conn.Close();
            return listRepDayOff;
        }

        public IList<AusHoliday> GetAusHoliday()
        {
            var listRepDayOff = new List<AusHoliday>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, Year, Month, HolidayDate, CreatedDate FROM dbo.AusHoilday";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var repDayfff = new AusHoliday()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                        Year = Convert.ToInt32(reader["Year"]),
                        HolidayDate = Convert.ToDateTime(reader["HolidayDate"]),
                        Month = Convert.ToInt32(reader["Month"]),

                    };


                    listRepDayOff.Add(repDayfff);
                }
            }
            conn.Close();


            return listRepDayOff;
        }

        public IList<RepDayOff> GetRepDayOffByRepId(int repId)
        {
            var listRepDayOff = new List<RepDayOff>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, UserId, DayOff, CreatedUserId, CreatedDate,TargetId FROM dbo.RepDayOff WHERE  UserId=@userId";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = repId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var repDayfff = new RepDayOff()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),

                        DayOff = Convert.ToDateTime(reader["DayOff"]),
                        TargetId = Convert.ToInt32(reader["TargetId"]),
                        UserId = Convert.ToInt32(reader["UserId"]),

                    };


                    listRepDayOff.Add(repDayfff);
                }
            }
            conn.Close();


            return listRepDayOff;
        }

        public IList<RepDayOff> GetRepDayOffByRepIdANdTarId(int repId, int tarId)
        {
            var listRepDayOff = new List<RepDayOff>();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var strSqlContactStmt = @"SELECT Id, UserId, DayOff, CreatedUserId, CreatedDate,TargetId,Approved FROM dbo.RepDayOff WHERE  UserId=@userId and TargetId=@tarId";

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = repId;
                cmd.Parameters.AddWithValue("@tarId", SqlDbType.Int).Value = tarId;
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var repDayfff = new RepDayOff()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),

                        DayOff = Convert.ToDateTime(reader["DayOff"]),
                        TargetId = Convert.ToInt32(reader["TargetId"]),
                        UserId = Convert.ToInt32(reader["UserId"]),

                    };
                    if (reader["Approved"] != DBNull.Value)
                        repDayfff.IsApproved = Convert.ToBoolean(reader["Approved"].ToString());


                    listRepDayOff.Add(repDayfff);
                }
            }
            conn.Close();


            return listRepDayOff;
        }
    }

    public class StatsTargetConfig
    {
        public int Id { get; set; }
        public decimal TargetAmount { get; set; }
        public string TargetTitle { get; set; }
        public DateTime TargetDay { get; set; }
        public bool IsReached { get; set; }
    }

    public class RepDayOff
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TargetId { get; set; }
        public bool IsApproved { get; set; }
        public DateTime DayOff { get; set; }
        public int CreateduserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class AusHoliday
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime HolidayDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
