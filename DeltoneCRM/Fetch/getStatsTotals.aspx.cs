using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;

namespace DeltoneCRM.Fetch
{
    public partial class getStatsTotals : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            float DailySalesTotal = TotalSalesThisMonth();
            float DailyCommissionTotal = TotalDailyCommissionThisMonth();
            float MonthlyCommissionTotal = TotalMonthlyCommissionThisMonth();
            float TotalBudget = TotalBudgetThisMonth();
            float TotalNewAccounts = TotalNewAccountsThisMonth();
            float TotalDailyVolume = TotalDailyVolumeThisMonth();
            float TotalMonthlyVolume = TotalMonthlyVolumeThisMonth();

            Response.Write(DailySalesTotal + "|" + DailyCommissionTotal + "|" + MonthlyCommissionTotal + "|0|" + TotalBudget + "|" + TotalNewAccounts + "|" + TotalDailyVolume + "|" + TotalMonthlyVolume);

        }

        protected float TotalSalesThisMonth()
        {
            float MonthlyTotSales = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT COUNT(*) AS TOTCOUNT FROM dbo.Orders " +
                    " WHERE DAY(CreatedDateTime) = DAY(getDate()) AND MONTH(CreatedDateTime) = MONTH(getDate()) " +
                    " AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED')";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                MonthlyTotSales = float.Parse(sdr["TOTCOUNT"].ToString());
                            }
                        }
                        else
                        {
                            MonthlyTotSales = 0;
                        }

                    }
                    conn.Close();

                }

            }
            return MonthlyTotSales;
        }

        protected float TotalDailyCommissionThisMonth()
        {
            float RepCommission = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(c.VALUE) AS TOTALCOM FROM dbo.Commision as c join Logins as l on c.userloginid= l.LoginID " +
                    " WHERE DAY(c.CreateDateTime) = DAY(getDate()) AND MONTH(c.CreateDateTime) = MONTH(getdate()) " +
                    "AND YEAR(c.CreateDateTime) = YEAR(getDate()) AND c.STATUS <> ('CANCELLED') and l.active='Y'";
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
                                    RepCommission = float.Parse("0");
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

        protected float TotalMonthlyCommissionThisMonth()
        {
            float RepCommission = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(c.VALUE) AS TOTALCOM FROM dbo.Commision as c join Logins as l on c.userloginid= l.LoginID " +
                    " WHERE DAY(c.CreateDateTime) BETWEEN '1' AND DAY(getDate() -1) " +
                    " AND MONTH(c.CreateDateTime) = MONTH(getdate()) AND YEAR(c.CreateDateTime) = YEAR(getDate()) and l.active='Y' and l.LoginID NOT IN ( 7 )";
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

        protected float TotalBudgetThisMonth()
        {
            float RepTarget = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(TargetCommission) AS TOTAL FROM dbo.Targets WHERE TargetMonth = MONTH(getDate()) AND TargetYear = YEAR(getDate())";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                if (sdr["Total"].ToString() == "")
                                {
                                    RepTarget = 0;
                                }
                                else
                                {
                                    RepTarget = float.Parse(sdr["Total"].ToString());
                                }

                            }
                        }
                        else
                        {
                            RepTarget = 0;
                        }


                    }
                    conn.Close();

                }

            }
            return RepTarget;
        }

        protected float TotalNewAccountsThisMonth()
        {
            float NewAccounts = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT Count(CompanyID) AS CompTotal FROM dbo.Companies WHERE Month(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND EXISTS (SELECT 1 FROM dbo.Orders WHERE Orders.CompanyID = Companies.CompanyID)";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                NewAccounts = float.Parse(sdr["CompTotal"].ToString());
                            }
                        }
                        else
                        {
                            NewAccounts = 0;
                        }

                    }
                    conn.Close();

                }

            }
            return NewAccounts;
        }

        protected float TotalDailyVolumeThisMonth()
        {
            float TotalSales = 0;
            float DailyCredits = 0;
            float DailyVolume = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(o.SubTotal) AS TOTAL FROM dbo.Orders as o join logins as l on o.AccountOwnerid=l.loginid " +
                   " WHERE DAY(o.CreatedDateTime) = DAY(getDate()) AND MONTH(o.CreatedDateTime) = MONTH(getDate()) " +
                    " AND YEAR(o.CreatedDateTime) = YEAR(getDate()) AND o.Status <> ('CANCELLED') AND l.active='Y' and l.LoginID NOT IN ( 7 )";
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
                                    TotalSales = 0;
                                }
                                else
                                {
                                    TotalSales = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            TotalSales = 0;
                        }

                    }
                    conn.Close();

                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(c.SubTotal) AS TOTAL FROM dbo.CreditNotes as c join logins as l on c.SalespersonID=l.loginid " +
                    " WHERE DAY(c.DateCreated) = DAY(getDate()) AND MONTH(c.DateCreated) = MONTH(getDate()) " +
                   " AND YEAR(DateCreated) = YEAR(getDate()) AND c.Status <>  ('CANCELLED') AND l.active='Y' and l.LoginID NOT IN ( 7 )";
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
                                    DailyCredits = 0;
                                }
                                else
                                {
                                    DailyCredits = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            DailyCredits = 0;
                        }
                    }
                    conn.Close();
                }
            }


            DailyVolume = TotalSales - DailyCredits;


            return DailyVolume;
        }

        protected float TotalMonthlyVolumeThisMonth()
        {
            float MonthlySales = 0;
            float MonthlyCredits = 0;
            float TotalMonthly = 0;

           // This code seems not working properly 

            //using (SqlConnection conn = new SqlConnection())
            //{
            //    
            //    using (SqlCommand cmd = new SqlCommand())
            //    {

            //        cmd.CommandText = "SELECT SUM(o.SubTotal) AS TOTAL FROM dbo.Orders as o join logins as l on o.AccountOwnerid=l.loginid " +
            //        " WHERE DAY(o.CreatedDateTime) BETWEEN '1' AND DAY(getDate() -1) AND " +
            //            " MONTH(o.CreatedDateTime) = MONTH(getDate()) AND YEAR(o.CreatedDateTime) = YEAR(getDate()) AND o.Status <> ('CANCELLED') AND l.active='Y'";
            //        cmd.Connection = conn;
            //        conn.Open();
            //        using (SqlDataReader sdr = cmd.ExecuteReader())
            //        {
            //            if (sdr.HasRows)
            //            {

            //                while (sdr.Read())
            //                {
            //                    if (sdr["TOTAL"] == DBNull.Value)
            //                    {
            //                        MonthlySales = 0;
            //                    }
            //                    else
            //                    {
            //                        MonthlySales = float.Parse(sdr["TOTAL"].ToString());
            //                    }

            //                }
            //            }
            //            else
            //            {
            //                MonthlySales = 0;
            //            }

            //        }
            //        conn.Close();
            //    }


            //    using (SqlCommand cmd = new SqlCommand())
            //    {
            //        cmd.CommandText = "SELECT SUM(c.SubTotal) AS TOTAL FROM dbo.CreditNotes c join logins as l on c.SalespersonID=l.loginid " +
            //        " WHERE DAY(c .DateCreated) BETWEEN '1' AND DAY(getDate() -1) " +
            //        " AND  MONTH(c .DateCreated) = MONTH(getDate()) AND YEAR(c.DateCreated) = YEAR(getDate()) AND c.STATUS <> ('CANCELLED') AND l.active='Y'";
            //        cmd.Connection = conn;
            //        conn.Open();
            //        using (SqlDataReader sdr = cmd.ExecuteReader())
            //        {
            //            if (sdr.HasRows)
            //            {
            //                while (sdr.Read())
            //                {
            //                    if (sdr["TOTAL"] == DBNull.Value)
            //                    {
            //                        MonthlyCredits = 0;
            //                    }
            //                    else
            //                    {
            //                        MonthlyCredits = float.Parse(sdr["TOTAL"].ToString());
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                MonthlyCredits = 0;
            //            }
            //        }
            //        conn.Close();

            //    }
            //}

            //TotalMonthly = MonthlySales - MonthlyCredits;


            // used monthlyvolume code from getstats page.

            float MVWithoutSplit = 0;
            float MVWithSplitAO = 0;
            float MVWithSplitSW = 0;

            float MCWithoutSplit = 0;
            float MCWithSplitAO = 0;
            float MCWithSplitSW = 0;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(o.SubTotal) AS TOTAL FROM dbo.Orders as o join logins as l on o.AccountOwnerid=l.loginid WHERE " +
                    "  DAY(o.CreatedDateTime) BETWEEN '1' AND DAY(getDate() -1) AND MONTH(o.CreatedDateTime) = MONTH(getDate()) " +
                    " AND YEAR(o.CreatedDateTime) = YEAR(getDate()) AND o.Status NOT IN ('CANCELLED') AND o.CommishSplit = 0 AND l.active='Y'  and l.LoginID NOT IN ( 7 )";
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

                    cmd.CommandText = "SELECT SUM(o.VolumeSplitAmount) AS TOTAL FROM dbo.Orders as o join logins as l on o.AccountOwnerid=l.loginid WHERE " +
                   " DAY(o.CreatedDateTime) BETWEEN '1' AND DAY(getDate() -1) AND MONTH(o.CreatedDateTime) = MONTH(getDate()) " +
                    " AND YEAR(o.CreatedDateTime) = YEAR(getDate()) AND o.Status NOT IN ('CANCELLED') AND o.CommishSplit = 1 AND l.active='Y'  and l.LoginID NOT IN ( 7 )";
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

                    cmd.CommandText = "SELECT SUM(o.VolumeSplitAmount) AS TOTAL FROM dbo.Orders  as o join logins as l on o.OrderEnteredByID=l.loginid WHERE " +
                    "  DAY(o.CreatedDateTime) BETWEEN '1' AND DAY(getDate() -1) AND MONTH(o.CreatedDateTime) " +
                   " = MONTH(getDate()) AND YEAR(o.CreatedDateTime) = YEAR(getDate()) AND o.Status NOT IN ('CANCELLED') AND o.CommishSplit = 1 AND l.active='Y'  and l.LoginID NOT IN ( 7 )";
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
                    cmd.CommandText = "SELECT SUM(c.SubTotal) AS TOTAL FROM dbo.CreditNotes as c join logins as l on c.AccountOwnerID=l.loginid " +
                    "  WHERE  DAY(c.DateCreated) " +
                    " BETWEEN '1' AND DAY(getDate() -1) AND  MONTH(c.DateCreated) = MONTH(getDate()) AND YEAR(c.DateCreated) = YEAR(getDate()) " +
                    " AND c.STATUS NOT IN ('CANCELLED') AND c.CommishSplit = 0 AND l.active='Y' and l.LoginID NOT IN ( 7 )";
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
                    cmd.CommandText = "SELECT SUM(c.SplitVolumeAmount) AS TOTAL FROM dbo.CreditNotes as c join logins as l on c.AccountOwnerID=l.loginid  " +
                    "  WHERE " +
                    " DAY(c.DateCreated) BETWEEN '1' AND DAY(getDate() -1) AND  MONTH(c.DateCreated) = MONTH(getDate()) " +
                    " AND YEAR(c.DateCreated) = YEAR(getDate()) AND c.STATUS NOT IN ('CANCELLED') AND c.CommishSplit = 1 AND l.active='Y' and l.LoginID NOT IN ( 7 )";
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

               

                //MonthlyCredits = MonthlyCredits - ((MonthlyCredits * 10) / 100);

                MonthlyCredits = MCWithoutSplit + MCWithSplitAO;

                TotalMonthly = MonthlySales - MonthlyCredits;

                return TotalMonthly;

            }
        }
    }
}