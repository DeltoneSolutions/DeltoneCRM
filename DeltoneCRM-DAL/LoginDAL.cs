using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Sql;


namespace DeltoneCRM_DAL
{
    public class LoginDAL
    {
        private String CONNSTRING;

        public LoginDAL(String strconnString)
        {
            CONNSTRING = strconnString;
        }




        //This method Upodate the Login
        public String UpdateLogin(int LoginID, String firstname, String LastName, int AccessLevel, String ActInact,
            String EmailAddy, String Username, String Password, int? Department, bool? canshow)
        {
            string output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSQLUpdateStmt = "";
            if (!string.IsNullOrEmpty(Password))
            {
                strSQLUpdateStmt = @"Update dbo.Logins SET FirstName=@FirstName,LastName=@LastName,AccessLevel=@AccessLevel,
                         EmailAddress=@EmailAddy,Active=@ActInact,AlteredDateTime=CURRENT_TIMESTAMP,
                         Username=@Username,Password=@Password ,Department=@Department ,DonotShowOnStats=@showStats where LoginID=@LoginID";

            }
            else
            {
                strSQLUpdateStmt = @"Update dbo.Logins SET FirstName=@FirstName,LastName=@LastName,AccessLevel=@AccessLevel,
                         EmailAddress=@EmailAddy,Active=@ActInact,AlteredDateTime=CURRENT_TIMESTAMP,
                         Username=@Username,Department=@Department ,DonotShowOnStats=@showStats where LoginID=@LoginID";
            }
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@LoginID", LoginID);
            cmd.Parameters.AddWithValue("@FirstName", firstname);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@AccessLevel", AccessLevel);
            cmd.Parameters.AddWithValue("@ActInact", ActInact);
            cmd.Parameters.AddWithValue("@EmailAddy", EmailAddy);
            cmd.Parameters.AddWithValue("@Username", Username);
            if (!string.IsNullOrEmpty(Password))
            {
                cmd.Parameters.AddWithValue("@Password", Password);
            }


            if (Department != null)
                cmd.Parameters.AddWithValue("@Department", Department);
            else
                cmd.Parameters.AddWithValue("@Department", DBNull.Value);

            if (canshow != null)
                cmd.Parameters.AddWithValue("@showStats", canshow);
            else
                cmd.Parameters.AddWithValue("@showStats", DBNull.Value);
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


        public String getLoginDetails(int LoginID)
        {
            String strOutPut = String.Empty;


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select * from dbo.Logins where LoginID=" + LoginID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOutPut = strOutPut + sdr["LoginID"].ToString() + ":" + sdr["Username"].ToString() + ":" + sdr["Password"].ToString() + ":" + sdr["FirstName"].ToString() + ":" + sdr["LastName"].ToString() + ":" + sdr["AccessLevel"].ToString() + ":" + sdr["Active"].ToString() + ":" + sdr["EmailAddress"].ToString();

                        if (sdr["Department"] == DBNull.Value)
                        {
                            strOutPut = strOutPut + ":" + "0";
                        }
                        else
                        {
                            strOutPut = strOutPut + ":" + sdr["Department"].ToString();
                        }

                        if (sdr["DonotShowOnStats"] == DBNull.Value)
                        {
                            strOutPut = strOutPut + ":" + "0";
                        }
                        else
                        {

                            if (Convert.ToBoolean(sdr["DonotShowOnStats"]) == true)
                                strOutPut = strOutPut + ":" + "1";
                            else
                                strOutPut = strOutPut + ":" + "0";
                          

                        }

                    }
                }
            }
            conn.Close();

            return strOutPut;
        }

        public String getLoginNameFromID(String LoginID)
        {
            String LoggedUserName = String.Empty;


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select FirstName + ' ' + LastName AS FullName from dbo.Logins where LoginID=" + LoginID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        LoggedUserName = sdr["FullName"].ToString();
                    }
                }
            }
            conn.Close();

            return LoggedUserName;
        }


        public String getLoginUserEmailFromID(String LoginID)
        {
            String LoggedUserName = String.Empty;


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select EmailAddress from dbo.Logins where LoginID=" + LoginID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        LoggedUserName = sdr["EmailAddress"].ToString();
                    }
                }
            }
            conn.Close();

            return LoggedUserName;
        }

        public String getLoginUserEmailFromFirstName(String firstName)
        {
            String LoggedUserName = String.Empty;


            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            String strSqlOrderStmt = "select EmailAddress from dbo.Logins where FirstName=@firstName";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        LoggedUserName = sdr["EmailAddress"].ToString();
                    }
                }
            }
            conn.Close();

            return LoggedUserName;
        }

        //Return's the Monthly Commission Without Taking Today into Account
        public float getMonthlyCommissionWithoutToday(String RepID)
        {
            float RepCommission = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VALUE) AS TOTALCOM FROM dbo.Commision WHERE UserLoginID = " + RepID + " AND DAY(CreateDateTime) BETWEEN '1' AND DAY(getDate() -1) AND MONTH(CreateDateTime) = MONTH(getdate()) AND YEAR(CreateDateTime) = YEAR(getDate())";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTALCOM"] == DBNull.Value)
                                {
                                    RepCommission = 0;
                                }
                                else
                                {
                                    String theValue = sdr["TOTALCOM"].ToString();
                                    float PreCalc = float.Parse(theValue); ;
                                    RepCommission = PreCalc;
                                }

                            }
                        }
                        else
                        {

                        }

                    }
                    conn.Close();

                }

            }
            return RepCommission;
        }

        //Return's the Monthly Volume Without Taking Today in Account
        public float getMonthVolume(String repname)
        {
            float MVWithoutSplit = 0;
            float MVWithSplitAO = 0;
            float MVWithSplitSW = 0;

            float MCWithoutSplit = 0;
            float MCWithSplitAO = 0;
            float MCWithSplitSW = 0;

            float MonthlyVolume = 0;
            float MonthlySales = 0;
            float MonthlyCredits = 0;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname + "' AND DAY(CreatedDateTime) BETWEEN '1' AND DAY(getDate() -1) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    MVWithoutSplit = 0;
                                }
                                else
                                {
                                    MVWithoutSplit = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            MVWithoutSplit = 0;
                        }

                    }
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname + "' AND DAY(CreatedDateTime) BETWEEN '1' AND DAY(getDate() -1) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    MVWithSplitAO = 0;
                                }
                                else
                                {
                                    MVWithSplitAO = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            MVWithSplitAO = 0;
                        }

                    }
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE OrderEnteredBy = '" + repname + "' AND DAY(CreatedDateTime) BETWEEN '1' AND DAY(getDate() -1) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    MVWithSplitSW = 0;
                                }
                                else
                                {
                                    MVWithSplitSW = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            MVWithSplitSW = 0;
                        }

                    }
                    conn.Close();
                }

                MonthlySales = MVWithoutSplit + MVWithSplitAO + MVWithSplitSW;


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname + "' AND DAY(DateCreated) BETWEEN '1' AND DAY(getDate() -1) AND  MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 0";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    MCWithoutSplit = 0;
                                }
                                else
                                {
                                    MCWithoutSplit = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            MCWithoutSplit = 0;
                        }
                    }
                    conn.Close();

                }


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SplitVolumeAmount) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname + "' AND DAY(DateCreated) BETWEEN '1' AND DAY(getDate() -1) AND  MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    MCWithSplitAO = 0;
                                }
                                else
                                {
                                    MCWithSplitAO = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            MCWithSplitAO = 0;
                        }
                    }
                    conn.Close();

                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SplitVolumeAmount) AS TOTAL FROM dbo.CreditNotes WHERE Salesperson = '" + repname + "' AND DAY(DateCreated) BETWEEN '1' AND DAY(getDate() -1) AND  MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    MCWithSplitSW = 0;
                                }
                                else
                                {
                                    MCWithSplitSW = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            MCWithSplitSW = 0;
                        }
                    }
                    conn.Close();

                }

                //MonthlyCredits = MonthlyCredits - ((MonthlyCredits * 10) / 100);

                MonthlyCredits = MCWithoutSplit + MCWithSplitAO + MCWithSplitSW;

                MonthlyVolume = MonthlySales - MonthlyCredits;

                return MonthlyVolume;
            }
        }

        // Return's the Daily Commission
        public float getDailyCommission(String repID)
        {
            float RepCommission = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VALUE) AS TOTALCOM FROM dbo.Commision WHERE UserLoginID = " + repID + " AND DAY(CreateDateTime) = DAY(getDate()) AND MONTH(CreateDateTime) = MONTH(getdate()) AND YEAR(CreateDateTime) = YEAR(getDate()) AND STATUS NOT IN ('CANCELLED')";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTALCOM"] == DBNull.Value)
                                {
                                    RepCommission = 0;
                                }
                                else
                                {
                                    RepCommission = float.Parse(sdr["TOTALCOM"].ToString());
                                }

                            }
                        }
                        else
                        {
                            RepCommission = 0;
                        }

                    }
                    conn.Close();

                }

            }
            return RepCommission;

        }

        // Return's the Daily Volume
        public float getDailyVolume(String repname)
        {

            float DVWithoutSplit = 0;
            float DVWithSplitAO = 0;
            float DVWithSplitSW = 0;

            float DCWithoutSplit = 0;
            float DCWithSplitAO = 0;
            float DCWithSplitSW = 0;

            float DailyVolume = 0;
            float DailySales = 0;
            float DailyCredits = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = CONNSTRING;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname + "' AND DAY(CreatedDateTime) = DAY(getDate()) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DVWithoutSplit = 0;
                                }
                                else
                                {
                                    DVWithoutSplit = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            DVWithoutSplit = 0;
                        }

                    }
                    conn.Close();

                }


                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname + "' AND DAY(CreatedDateTime) = DAY(getDate()) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DVWithSplitAO = 0;
                                }
                                else
                                {
                                    DVWithSplitAO = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            DVWithSplitAO = 0;
                        }

                    }
                    conn.Close();

                }

                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE OrderEnteredBy = '" + repname + "' AND DAY(CreatedDateTime) = DAY(getDate()) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DVWithSplitSW = 0;
                                }
                                else
                                {
                                    DVWithSplitSW = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            DVWithSplitSW = 0;
                        }

                    }
                    conn.Close();

                }

                DailySales = DVWithoutSplit + DVWithSplitAO + DVWithSplitSW;

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname + "' AND DAY(DateCreated) = DAY(getDate()) AND MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DCWithoutSplit = 0;
                                }
                                else
                                {
                                    DCWithoutSplit = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            DCWithoutSplit = 0;
                        }
                    }
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname + "' AND DAY(DateCreated) = DAY(getDate()) AND MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DCWithSplitAO = 0;
                                }
                                else
                                {
                                    DCWithSplitAO = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            DCWithSplitAO = 0;
                        }
                    }
                    conn.Close();
                }


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE Salesperson = '" + repname + "' AND DAY(DateCreated) = DAY(getDate()) AND MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    DCWithSplitSW = 0;
                                }
                                else
                                {
                                    DCWithSplitSW = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            DCWithSplitSW = 0;
                        }
                    }
                    conn.Close();
                }

            }

            DailyCredits = DCWithoutSplit + DCWithSplitAO + DCWithSplitSW;

            //DailyCredits = DailyCredits - DailyCredits;

            DailyVolume = DailySales - DailyCredits;

            return DailyVolume;
        }

        public class DisplayRepAttendance
        {
            public string RepName { get; set; }
            public DateTime StarDateTime { get; set; }
            public DateTime EndDateTime { get; set; }
            public string StartTimeFlag { get; set; }
            public string EndTimeFlag { get; set; }
            public double NumberOfMinutesWorked { get; set; }
            public double NumberOfHoursWorked { get; set; }
            public double BreakInMinutes { get; set; }
            public double FinalNumberOfMinutesWorked { get; set; }
            public double FinalNumberOfHoursWorked { get; set; }
            public string RepStartTimeChanged { get; set; }
            public string RepEndTimeChanged { get; set; }
            public string RepLunchTimeChanged { get; set; }
        }

    }
}
