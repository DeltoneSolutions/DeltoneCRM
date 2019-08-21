using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Data;
using DeltoneCRM_DAL;


namespace DeltoneCRM.Reports.Queries
{
    public partial class getMonthlySalesTotal : System.Web.UI.Page
    {
        LoginDAL loginDAL = new LoginDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

        float totalCom = 0;

        private void ChangeDate(string startDate, string endDate, out string dateStart, out string dateEnd)
        {
            var conStartDate = Convert.ToDateTime(startDate);
            dateStart = conStartDate.ToString("yyyy-MM-dd");

            var conEndDate = Convert.ToDateTime(endDate);
            conEndDate = conEndDate.AddDays(1);
            dateEnd = conEndDate.ToString("yyyy-MM-dd");

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String convertOpName = getOperatorFullName(Request.QueryString["RepName"]);

            String EnteredRepName = convertOpName;
            // String EnteredMonth = Request.QueryString["Month"];
            //  String EnteredYear = Request.QueryString["Year"];
            String strOutput = String.Empty;

            var startDate = Request.QueryString["startDate"];
            var endDate = Request.QueryString["endDate"];
            if (string.IsNullOrEmpty(endDate))
                endDate = startDate;

            var stDate = startDate;
            var enDate = endDate;

            ChangeDate(startDate, endDate, out stDate, out enDate);

            float RunningVolume = 0;
            float RunningCommission = 0;
            float RunningCreditTotal = 0;
            float RunningCreditCommission = 0;

            // RunningVolume = getSalesTotal(EnteredRepName, stDate, EnteredYear);
            RunningVolume = getSalesTotalStartEnd(EnteredRepName, stDate, enDate);
            //  RunningCommission = getCommissionTotal(Request.QueryString["RepName"], EnteredMonth, EnteredYear);
            RunningCommission = getCommissionTotalStartEnd(Request.QueryString["RepName"], stDate, enDate);
            // RunningCreditTotal = getCreditTotal(Request.QueryString["RepName"], EnteredMonth, EnteredYear);
            RunningCreditTotal = getCreditTotalStartEnd(Request.QueryString["RepName"], stDate, enDate);
            // RunningCreditCommission = getCreditCommissionTotal(Request.QueryString["RepName"], EnteredMonth, EnteredYear);

            RunningCreditCommission = getCreditCommissionTotalStartEnd(Request.QueryString["RepName"], stDate, enDate);


            strOutput = strOutput + RunningVolume + "|";
            strOutput = strOutput + RunningCommission + "|";
            strOutput = strOutput + RunningCreditTotal + "|";
            strOutput = strOutput + RunningCreditCommission;

            Response.Write(strOutput);

        }

        public String getOperatorFullName(String OID)
        {
            String OperatorName = String.Empty;

            if (OID.Equals("9999"))
            {
                OperatorName = "ALL";
            }
            else
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "SELECT FirstName + ' ' + LastName AS FullName FROM dbo.Logins WHERE LoginID = " + OID;
                        cmd.Connection = conn;
                        conn.Open();

                        using (SqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                OperatorName = sdr["FullName"].ToString();
                            }

                        }
                    }
                }
            }

            return OperatorName;

        }

        public float getCommissionTotalStartEnd(String RepName, String startDate, String endDate)
        {
            String RunningVolume = String.Empty;
            float RunningVolumefloat = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (RepName == "9999")
                    {
                        cmd.CommandText = @"SELECT SUM(Value) AS Total FROM dbo.Commision WHERE  CreateDateTime between  "
                              + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                          + " AND Type = 'ORDER' AND STATUS NOT IN ('CANCELLED', 'PENDING')";
                    }
                    else
                    {


                        cmd.CommandText = @"SELECT SUM(Value) AS Total FROM dbo.Commision WHERE UserLoginID = '" + RepName + "'"
                            + " AND CreateDateTime between  "
                                + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Type = 'ORDER' AND STATUS NOT IN ('CANCELLED', 'PENDING')";
                    }
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolume = sdr["Total"].ToString();
                            if (RunningVolume == "")
                            {
                                RunningVolume = "0";
                            }
                            RunningVolumefloat = RunningVolumefloat + float.Parse(RunningVolume);
                            totalCom = totalCom + float.Parse(RunningVolume);
                        }

                    }
                }
            }


            return RunningVolumefloat;
        }

        public float getCommissionTotal(String RepName, String Month, String Year)
        {
            String RunningVolume = String.Empty;
            float RunningVolumefloat = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(Value) AS Total FROM dbo.Commision WHERE UserLoginID = '" + RepName + "' AND MONTH(CreateDateTime) = '" + Month + "' AND YEAR(CreateDateTime) = '" + Year + "' AND Type = 'ORDER' AND STATUS NOT IN ('CANCELLED', 'PENDING')";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolume = sdr["Total"].ToString();
                            if (RunningVolume == "")
                            {
                                RunningVolume = "0";
                            }
                            RunningVolumefloat = RunningVolumefloat + float.Parse(RunningVolume);
                            totalCom = totalCom + float.Parse(RunningVolume);
                        }

                    }
                }
            }


            return RunningVolumefloat;
        }


        public float getCreditCommissionTotalStartEnd(String RepName, String startDate, String endDate)
        {
            String RunningVolume = String.Empty;
            float RunningVolumefloat = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "9999")
                    {
                        cmd.CommandText = @"SELECT SUM(Value) AS Total FROM dbo.Commision WHERE  CreateDateTime between  "
                               + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                           + " AND Type = 'CREDITNOTE' AND STATUS NOT IN ('CANCELLED')";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT SUM(Value) AS Total FROM dbo.Commision WHERE UserLoginID = '" + RepName + "'"
                            + " AND CreateDateTime between  "
                                + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Type = 'CREDITNOTE' AND STATUS NOT IN ('CANCELLED')";
                    }
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["TOTAL"].Equals(DBNull.Value))
                            {
                                RunningVolumefloat = 0;
                            }
                            else
                            {
                                RunningVolume = sdr["Total"].ToString();
                                RunningVolumefloat = RunningVolumefloat + float.Parse(RunningVolume);
                            }

                        }

                    }
                }
            }

            return RunningVolumefloat;
        }

        public float getCreditCommissionTotal(String RepName, String Month, String Year)
        {
            String RunningVolume = String.Empty;
            float RunningVolumefloat = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(Value) AS Total FROM dbo.Commision WHERE UserLoginID = '" + RepName + "' AND MONTH(CreateDateTime) = '" + Month + "' AND YEAR(CreateDateTime) = '" + Year + "' AND Type = 'CREDITNOTE' AND STATUS NOT IN ('CANCELLED')";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr["TOTAL"].Equals(DBNull.Value))
                            {
                                RunningVolumefloat = 0;
                            }
                            else
                            {
                                RunningVolume = sdr["Total"].ToString();
                                RunningVolumefloat = RunningVolumefloat + float.Parse(RunningVolume);
                            }

                        }

                    }
                }
            }

            return RunningVolumefloat;
        }

        public float getSalesTotalStartEnd(String RepName, String startDate, String endDate)
        {
            String RunningVolumeWithoutCS = String.Empty;
            String RunningVolimeWithCSAO = String.Empty;
            String RunningVolumeWithCSSW = String.Empty;
            float RunningVolumefloatWithoutCS = 0;
            float RunningVolumefloatWithCSAO = 0;
            float RunningVolumefloatWithCSSW = 0;
            float RunningVolumefloat = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT SUM(SubTotal) AS Total FROM dbo.Orders WHERE  CreatedDateTime between  "
                                + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 0";
                    }
                    else
                    {

                        cmd.CommandText = @"SELECT SUM(SubTotal) AS Total FROM dbo.Orders WHERE CreatedBy = '" + RepName + "'"
                            + " AND  CreatedDateTime between  "
                                + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 0";
                    }
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolumeWithoutCS = sdr["Total"].ToString();
                            if (!string.IsNullOrEmpty(RunningVolumeWithoutCS))
                                RunningVolumefloatWithoutCS = RunningVolumefloatWithoutCS + float.Parse(RunningVolumeWithoutCS);
                        }

                    }
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT SUM(VolumeSplitAmount) AS Total FROM dbo.Orders WHERE   CreatedDateTime between  "
                               + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                           + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT SUM(VolumeSplitAmount) AS Total FROM dbo.Orders WHERE AccountOwner = '" + RepName + "'"
                           + " AND  CreatedDateTime between  "
                                + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    }
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolimeWithCSAO = sdr["Total"].ToString();
                            if (RunningVolimeWithCSAO == "")
                            {
                                RunningVolumefloatWithCSAO = 0;
                            }
                            else
                            {
                                RunningVolumefloatWithCSAO = RunningVolumefloatWithCSAO + float.Parse(RunningVolimeWithCSAO);
                            }

                        }

                    }
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT SUM(VolumeSplitAmount) AS Total FROM dbo.Orders WHERE  CreatedDateTime between  "
                               + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                           + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT SUM(VolumeSplitAmount) AS Total FROM dbo.Orders WHERE SplitWith = '" + RepName + "'"
                             + " AND  CreatedDateTime between  "
                                + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    }
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolumeWithCSSW = sdr["Total"].ToString();
                            if (RunningVolumeWithCSSW == "")
                            {
                                RunningVolumefloatWithCSSW = 0;
                            }
                            else
                            {
                                RunningVolumefloatWithCSSW = RunningVolumefloatWithCSSW + float.Parse(RunningVolumeWithCSSW);
                            }

                        }

                    }
                    conn.Close();
                }
            }

            RunningVolumefloat = RunningVolumefloatWithoutCS + RunningVolumefloatWithCSAO + RunningVolumefloatWithCSSW;

            return RunningVolumefloat;
        }

        public float getSalesTotal(String RepName, String Month, String Year)
        {
            String RunningVolumeWithoutCS = String.Empty;
            String RunningVolimeWithCSAO = String.Empty;
            String RunningVolumeWithCSSW = String.Empty;
            float RunningVolumefloatWithoutCS = 0;
            float RunningVolumefloatWithCSAO = 0;
            float RunningVolumefloatWithCSSW = 0;
            float RunningVolumefloat = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS Total FROM dbo.Orders WHERE CreatedBy = '" + RepName + "' AND MONTH(CreatedDateTime) = '" + Month + "' AND YEAR(CreatedDateTime) = '" + Year + "' AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 0";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolumeWithoutCS = sdr["Total"].ToString();
                            RunningVolumefloatWithoutCS = RunningVolumefloatWithoutCS + float.Parse(RunningVolumeWithoutCS);
                        }

                    }
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(VolumeSplitAmount) AS Total FROM dbo.Orders WHERE AccountOwner = '" + RepName + "' AND MONTH(CreatedDateTime) = '" + Month + "' AND YEAR(CreatedDateTime) = '" + Year + "' AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolimeWithCSAO = sdr["Total"].ToString();
                            if (RunningVolimeWithCSAO == "")
                            {
                                RunningVolumefloatWithCSAO = 0;
                            }
                            else
                            {
                                RunningVolumefloatWithCSAO = RunningVolumefloatWithCSAO + float.Parse(RunningVolimeWithCSAO);
                            }

                        }

                    }
                    conn.Close();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(VolumeSplitAmount) AS Total FROM dbo.Orders WHERE SplitWith = '" + RepName + "' AND MONTH(CreatedDateTime) = '" + Month + "' AND YEAR(CreatedDateTime) = '" + Year + "' AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolumeWithCSSW = sdr["Total"].ToString();
                            if (RunningVolumeWithCSSW == "")
                            {
                                RunningVolumefloatWithCSSW = 0;
                            }
                            else
                            {
                                RunningVolumefloatWithCSSW = RunningVolumefloatWithCSSW + float.Parse(RunningVolumeWithCSSW);
                            }

                        }

                    }
                    conn.Close();
                }
            }

            RunningVolumefloat = RunningVolumefloatWithoutCS + RunningVolumefloatWithCSAO + RunningVolumefloatWithCSSW;

            return RunningVolumefloat;
        }

        public float getCreditTotal(String RepName, String Month, String Year)
        {
            String RunningVolume = String.Empty;
            float RunningVolumefloat = 0;
            String RepFullName = String.Empty;

            RepFullName = loginDAL.getLoginNameFromID(RepName);

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS Total FROM dbo.CreditNotes WHERE AccountOwner = '" + RepFullName + "' AND MONTH(DateCreated) = '" + Month + "' AND YEAR(DateCreated) = '" + Year + "' AND Status NOT IN ('CANCELLED')";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolume = sdr["Total"].ToString();
                            if (RunningVolume == "")
                            {
                                RunningVolumefloat = 0;
                            }
                            else
                            {
                                RunningVolumefloat = RunningVolumefloat + float.Parse(RunningVolume);
                            }

                        }

                    }
                }
            }

            //RunningVolumefloat = RunningVolumefloat - ((RunningVolumefloat * 10) / 100);

            return RunningVolumefloat;
        }

        public float getCreditTotalStartEnd(String RepName, String startDate, String endDate)
        {
            String RunningVolume = String.Empty;
            float RunningVolumefloat = 0;
            String RepFullName = String.Empty;

            RepFullName = loginDAL.getLoginNameFromID(RepName);

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "9999")
                    {
                        cmd.CommandText = @"SELECT SUM(SubTotal) AS Total FROM dbo.CreditNotes WHERE  DateCreated between  "
                               + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                           + " AND Status NOT IN ('CANCELLED')";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT SUM(SubTotal) AS Total FROM dbo.CreditNotes WHERE AccountOwner = '" + RepFullName + "'"
                            + " AND DateCreated between  "
                                + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Status NOT IN ('CANCELLED')";
                    }
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RunningVolume = sdr["Total"].ToString();
                            if (RunningVolume == "")
                            {
                                RunningVolumefloat = 0;
                            }
                            else
                            {
                                RunningVolumefloat = RunningVolumefloat + float.Parse(RunningVolume);
                            }

                        }

                    }
                }
            }

            //RunningVolumefloat = RunningVolumefloat - ((RunningVolumefloat * 10) / 100);

            return RunningVolumefloat;
        }
    }

}