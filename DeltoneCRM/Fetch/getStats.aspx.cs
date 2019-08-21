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

namespace DeltoneCRM.Fetch
{
    public partial class getStats : System.Web.UI.Page
    {
        //<key=LoginID,value=DelReport_Contact object>
        public Dictionary<String, DelReport_Contact> di_getStat = new Dictionary<String, DelReport_Contact>();

        private void ChangeDate(string startDate, string endDate, out string dateStart, out string dateEnd, out string onedayBefore)
        {
            var conStartDate = Convert.ToDateTime(startDate);
            dateStart = conStartDate.ToString("yyyy-MM-dd");

            var conEndDate = Convert.ToDateTime(endDate);
            onedayBefore = conEndDate.ToString("yyyy-MM-dd");
            conEndDate = conEndDate.AddDays(1);
            dateEnd = conEndDate.ToString("yyyy-MM-dd");

        }
        DelReport_Contact obj_Contact; String stroutput = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {



            var startDate = Request.QueryString["stDate"].ToString();
            var endDate = Request.QueryString["enDate"].ToString();
            var noFilter = "y";
            if (Session["nofiler"] != null)
            {
                noFilter = Session["nofiler"].ToString();
            }
            if (string.IsNullOrEmpty(endDate))
                endDate = startDate;

            var stDate = startDate;
            var enDate = endDate;
            var oneDayBefore = endDate;

            var currentDate = DateTime.Now;
            var callNStartDate = currentDate.Date.ToString("yyyy-MM-dd");
            var callNEndDateNext = currentDate.AddDays(1);
            var callNEndDate = callNEndDateNext.Date.ToString("yyyy-MM-dd");





            ChangeDate(startDate, endDate, out stDate, out enDate, out oneDayBefore);

            getRepName();//populate the Dictionary

            if (noFilter == "y")
            {
                stroutput = callNoFilterData(stroutput, callNStartDate, callNEndDate);
            }
            else
            {
                stroutput = callFilterData(stroutput, stDate, enDate, oneDayBefore);
            }

            Response.Write(stroutput);
        }


        private IList<CallNResponse> GetCallNData(string startMinDate, string startMaxDate, string name = "", bool callByName = false)
        {
            var callnRequest = new CallNRequest();
            callnRequest.StartDate = startMinDate;
            callnRequest.EndDate = startMaxDate;
            var resultCallN = new List<CallNResponse>();

            var callerData = new CallNDataHandler();
            if (callByName)
            {
                callnRequest.IsDateFilterApplied = true;
                callnRequest.RepName = name;
            }

            resultCallN = callerData.GetCallNData(callnRequest);

            return resultCallN;
        }

        private string callFilterData(string stroutput, string startDa, string endaate, string oneDayBefore)
        {

            var listCallnData = GetCallNData(startDa, endaate);

            foreach (var pair in di_getStat)
            {
                obj_Contact = (DelReport_Contact)pair.Value;
                String repName = obj_Contact.FirstName + " " + obj_Contact.LastName;
                stroutput = stroutput + repName + "|";
                String departmentId = obj_Contact.DepartmentId;
                stroutput = stroutput + departmentId + "|";
                String MonthTotSales = getSalesForThisMonthStartEnd(repName, startDa, endaate);
                stroutput = stroutput + MonthTotSales + "|";
                String DailyCommission = getDailyCommissionStartEnd(pair.Key.ToString(), startDa, endaate);
                stroutput = stroutput + "$ " + Math.Round(float.Parse(DailyCommission), 2) + "|";
                float MonthlyCommission = getMonthlyCommissionWithoutTodayStartEnd(pair.Key.ToString(), startDa, endaate);
                stroutput = stroutput + "$ " + Math.Round(MonthlyCommission, 2) + "|";
                String TargetCommission = getTargetForMonthFilter(pair.Key.ToString(), startDa, endaate);
                String WorkingDays = getWorkingDaysFilter(pair.Key.ToString(), startDa, endaate);
                String WorkedDays = getWorkedDaysFilter(pair.Key.ToString(), startDa, endaate);


                float MonthlyCommissionMinusOne = getMonthlyCommissionWithoutTodayStartEnd(pair.Key.ToString(), startDa, endaate);

                float TCommission = float.Parse(TargetCommission);
                float MCommission = MonthlyCommissionMinusOne;
                int WDays = int.Parse(WorkingDays);
                int WdDays = int.Parse(WorkedDays);

                int RemainingDays = (WDays + 1) - WdDays;

                float RemainingAmount = TCommission - MCommission;


                float DailyRequired = 0;
                if (TCommission == 0)
                {
                    DailyRequired = 0;
                }
                else
                {
                    DailyRequired = (float)RemainingAmount / (float)RemainingDays;
                    //DailyRequired = ((float)RemainingAmount / (float)RemainingDays) - float.Parse(DailyCommission);
                }

                stroutput = stroutput + "$ " + Math.Round(DailyRequired, 2) + "|";
                stroutput = stroutput + "$ " + Math.Round(TCommission, 2) + "|";

                String NewAccounts = getNewAccountsCountStartEnd(pair.Key.ToString(), startDa, endaate);
                stroutput = stroutput + NewAccounts + "|";

                float DailyVolume = getDailyVolumeStartEnd(repName, startDa, endaate);
                stroutput = stroutput + "$ " + Math.Round(DailyVolume, 2) + "|";

                float MonthlyVolume = getMonthlyVolumeWithoutTodayStartEnd(repName, startDa, endaate);
                stroutput = stroutput + "$ " + Math.Round(MonthlyVolume, 2) + "|";
                var csCount = GetSalesRepCSCount(pair.Key.ToString());
                if (repName == "Trent Knight")
                    repName = "Trent Kinght";
                // if (repName == "Aidan Atkins")
                //  repName = "Aiden Atkins";
                var callNCount = GetSalesRepCallCount(listCallnData, repName);
                stroutput = stroutput + csCount + "|";


                stroutput = stroutput + callNCount + "~";


            }

            int Length = stroutput.Length;
            stroutput = stroutput.Substring(0, (Length - 1));

            return stroutput;

        }

        private string GetSalesRepCSCount(string salesrepId)
        {
            string count = "0";
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strSqlContactStmt = @"SELECT count(rs.Id) as cs  from 
                       RaiseCSSalesRep rs inner join Companies  c on rs.CompanyId=c.CompanyID 
                 where (rs.CreatedUserId=@userId or  c.OwnershipAdminID =@userId )  and rs.status <>'Y'";
                    cmd.Parameters.AddWithValue("@userId", SqlDbType.Int).Value = salesrepId;
                    cmd.CommandText = strSqlContactStmt;



                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                count = sdr["cs"].ToString();
                            }
                        }
                    }
                }
            }

            return count;
        }


        private int GetSalesRepCallCount(IList<CallNResponse> listCall, string repName)
        {

            var count = 0;
            repName = repName.ToLower();
            count = listCall.Where(x => x.callername.ToLower() == repName).Count();

            return count;

        }

        private string callNoFilterData(string stroutput, string callNSartDate, string callNEndDate)
        {
            var listCallnData = GetCallNData(callNSartDate, callNEndDate);

            foreach (var pair in di_getStat)
            {
                obj_Contact = (DelReport_Contact)pair.Value;
                String repName = obj_Contact.FirstName + " " + obj_Contact.LastName;
                stroutput = stroutput + repName + "|";
                String departmentId = obj_Contact.DepartmentId;
                stroutput = stroutput + departmentId + "|";
                String MonthTotSales = getSalesForThisMonth(repName);
                stroutput = stroutput + MonthTotSales + "|";
                String DailyCommission = getDailyCommission(pair.Key.ToString());
                stroutput = stroutput + "$ " + Math.Round(float.Parse(DailyCommission), 2) + "|";
                float MonthlyCommission = getMonthlyCommissionWithoutToday(pair.Key.ToString());
                stroutput = stroutput + "$ " + Math.Round(MonthlyCommission, 2) + "|";
                String TargetCommission = getTargetForMonth(pair.Key.ToString());
                String WorkingDays = getWorkingDays(pair.Key.ToString());
                //String WorkedDays = getWorkedDays(pair.Key.ToString());

                
                var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
                var counn = 0;
                var WorkedDays = "0";
                if (DateTime.Now.Date != new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1))
                {
                    var listDayOffs = repDayOffDal.GetAusHoliday();
                    var repListOffs = repDayOffDal.GetRepDayOffByRepId(Convert.ToInt32(pair.Key));
                    var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    var startDate = firstDayOfMonth;
                    var end = DateTime.Now.AddDays(-1);
                    List<DateTime> bankHolidays = new List<DateTime>();
                    List<DateTime> repDaysoffas = new List<DateTime>();
                    foreach (var item in repListOffs)
                        repDaysoffas.Add(item.DayOff);
                    //listDayOffs = (from li in listDayOffs where li.Month == monCon && li.Year == yerCon select li).ToList();
                    foreach (var item in listDayOffs)
                        bankHolidays.Add(item.HolidayDate);
                    counn = BusinessDaysUntil(startDate, end, bankHolidays, repDaysoffas);
                }
                WorkedDays = counn.ToString();

                float MonthlyCommissionMinusOne = getMonthlyCommissionWithoutToday(pair.Key.ToString());

                float TCommission = float.Parse(TargetCommission);
                float MCommission = MonthlyCommissionMinusOne;
                int WDays = int.Parse(WorkingDays);
                int WdDays = int.Parse(WorkedDays);

                int RemainingDays = (WDays ) - WdDays;
                float RemainingAmount = TCommission;

                var currentDay = DateTime.Now;
                if (currentDay.Day == 1 && currentDay.Hour < 16)
                {
                    RemainingAmount = TCommission;
                }
                else
                    RemainingAmount = TCommission - MCommission;

                float DailyRequired = 0;
                if (TCommission == 0)
                {
                    DailyRequired = 0;
                }
                else
                {
                    DailyRequired = (float)RemainingAmount / (float)RemainingDays;
                    //DailyRequired = ((float)RemainingAmount / (float)RemainingDays) - float.Parse(DailyCommission);
                }


                /*
                if ( RemainingAmount <= 0)
                {
                    DailyRequired = 0;
                }
                else
                {
                    DailyRequired = (float)RemainingAmount / (float)RemainingDays;
                }
                */

                stroutput = stroutput + "$ " + Math.Round(DailyRequired, 2) + "|";
                stroutput = stroutput + "$ " + Math.Round(TCommission, 2) + "|";

                String NewAccounts = getNewAccountsCount(pair.Key.ToString());
                stroutput = stroutput + NewAccounts + "|";

                float DailyVolume = getDailyVolume(repName);
                stroutput = stroutput + "$ " + Math.Round(DailyVolume, 2) + "|";

                float MonthlyVolume = getMonthlyVolumeWithoutToday(repName);
                stroutput = stroutput + "$ " + Math.Round(MonthlyVolume, 2) + "|";
                var csCount = GetSalesRepCSCount(pair.Key.ToString());
                if (repName == "Trent Knight")
                    repName = "Trent Kinght";
                //if (repName == "Aidan Atkins")
                //    repName = "Aiden Atkins";



                var callNCount = GetSalesRepCallCount(listCallnData, repName);
                stroutput = stroutput + csCount + "|";


                stroutput = stroutput + callNCount + "~";

            }

            int Length = stroutput.Length;
            stroutput = stroutput.Substring(0, (Length - 1));

            return stroutput;
        }

        public int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, List<DateTime> bankHolidays, List<DateTime> dayOffs)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            //if (firstDay > lastDay)
            //    throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = (int)firstDay.DayOfWeek;
                int lastDayOfWeek = (int)lastDay.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay)
                    --businessDays;
            }

            foreach (DateTime dasy in dayOffs)
            {
                DateTime bh = dasy.Date;
                if (firstDay <= bh && bh <= lastDay)
                    --businessDays;
            }

            return businessDays;
        }

        public float getMonthlyVolumeWithoutTodayStartEnd(String repname, string startD, string endD)
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
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT SUM(SubTotal) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname
                       + "' AND CreatedDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
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

                    cmd.CommandText = @"SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname
                      + "' AND CreatedDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
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

                    cmd.CommandText = @"SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE OrderEnteredBy = '" + repname
                       + "' AND CreatedDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
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
                    cmd.CommandText = @"SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname
                       + "' AND DateCreated between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 0";
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
                    cmd.CommandText = @"SELECT SUM(SplitVolumeAmount) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname
                       + "' AND DateCreated between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
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
                    cmd.CommandText = @"SELECT SUM(SplitVolumeAmount) AS TOTAL FROM dbo.CreditNotes WHERE Salesperson = '" + repname
                       + "' AND DateCreated between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
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

        public float getMonthlyVolumeWithoutToday(String repname)
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
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
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

        public float getMonthVolume(String repname)
        {
            float MonthlyVolume = 0;
            float MonthlySales = 0;
            float MonthlyCredits = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname + "' AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED')";
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
                                    MonthlySales = 0;
                                }
                                else
                                {
                                    MonthlySales = float.Parse(sdr["TOTAL"].ToString());
                                }

                            }
                        }
                        else
                        {
                            MonthlySales = 0;
                        }

                    }
                    conn.Close();

                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE CreatedBy = '" + repname + "' AND MONTH(DateCreated) = MONTH(getDate()) AND YEAR(DateCreated) = YEAR(getDate()) AND STATUS NOT IN ('CANCELLED')";
                    cmd.Connection = conn;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (sdr["TOTAL"] == DBNull.Value)
                                {
                                    MonthlyCredits = 0;
                                }
                                else
                                {
                                    MonthlyCredits = float.Parse(sdr["TOTAL"].ToString());
                                }
                            }
                        }
                        else
                        {
                            MonthlyCredits = 0;
                        }
                    }

                }

                MonthlyCredits = MonthlyCredits - MonthlyCredits;

                MonthlyVolume = MonthlySales - MonthlyCredits;

                return MonthlyVolume;
            }
        }

        public float getDailyVolumeStartEnd(String repname, string startD, string endD)
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
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT SUM(SubTotal) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname
                       + "' AND CreatedDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                   + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
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

                    cmd.CommandText = @"SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE AccountOwner = '" + repname
                       + "' AND CreatedDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                    + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
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

                    cmd.CommandText = @"SELECT SUM(VolumeSplitAmount) AS TOTAL FROM dbo.Orders WHERE OrderEnteredBy = '" + repname
                        + "' AND CreatedDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
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
                    cmd.CommandText = @"SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname
                      + "' AND DateCreated between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
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
                    cmd.CommandText = @"SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE AccountOwner = '" + repname
                       + "' AND DateCreated between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
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
                    cmd.CommandText = @"SELECT SUM(SubTotal) AS TOTAL FROM dbo.CreditNotes WHERE Salesperson = '" + repname
                        + "' AND DateCreated between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                        + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
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
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
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

        public string getNewAccountsCountStartEnd(String repid, string startD, string endD)
        {

            string NewAccounts = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT Count(CompanyID) AS CompTotal FROM dbo.Companies WHERE OwnershipAdminID = " + repid
                    + " AND CreatedDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                   + " AND EXISTS (SELECT 1 FROM dbo.Orders WHERE Orders.CompanyID = Companies.CompanyID)";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                NewAccounts = sdr["CompTotal"].ToString();
                            }
                        }
                        else
                        {
                            NewAccounts = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return NewAccounts;
        }


        public string getNewAccountsCount(String repid)
        {

            string NewAccounts = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT Count(CompanyID) AS CompTotal FROM dbo.Companies WHERE OwnershipAdminID = " + repid + " AND Month(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND EXISTS (SELECT 1 FROM dbo.Orders WHERE Orders.CompanyID = Companies.CompanyID)";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                NewAccounts = sdr["CompTotal"].ToString();
                            }
                        }
                        else
                        {
                            NewAccounts = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return NewAccounts;
        }

        public String getWorkedDaysFilter(String repid,string stardate,string endDate)
        {
            string RepWorkedDays = String.Empty;
             var month = Convert.ToDateTime(stardate).Month;
            var year = Convert.ToDateTime(stardate).Year;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT COUNT(DISTINCT(DAY(LoggedDateTime))) AS TotalLogged FROM dbo.LoginAudit WHERE MONTH(LoggedDateTime) = "+month+" AND YEAR(LoggedDateTime) = "+year+" AND UserID =" + repid;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                RepWorkedDays = sdr["TotalLogged"].ToString();
                            }
                        }
                        else
                        {
                            RepWorkedDays = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return RepWorkedDays;
        }

        public String getWorkedDays(String repid)
        {
            string RepWorkedDays = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT COUNT(DISTINCT(DAY(LoggedDateTime))) AS TotalLogged FROM dbo.LoginAudit WHERE MONTH(LoggedDateTime) = MONTH(getDate()) AND YEAR(LoggedDateTime) = YEAR(getDate()) AND UserID =" + repid;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                RepWorkedDays = sdr["TotalLogged"].ToString();
                            }
                        }
                        else
                        {
                            RepWorkedDays = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return RepWorkedDays;
        }

        public String getWorkingDaysFilter(String repid,string stardate,string endDate)
        {
            string RepWorkingDays = String.Empty;
            var month = Convert.ToDateTime(stardate).Month;
            var year = Convert.ToDateTime(stardate).Year;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT WorkingDays FROM dbo.Targets WHERE LoginID = " + repid + " AND TargetYear = " + year + " AND TargetMonth = " + month;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                RepWorkingDays = sdr["WorkingDays"].ToString();
                            }
                        }
                        else
                        {
                            RepWorkingDays = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return RepWorkingDays;
        }

        public String getWorkingDays(String repid)
        {
            string RepWorkingDays = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT WorkingDays FROM dbo.Targets WHERE LoginID = " + repid + " AND TargetYear = YEAR(getDate()) AND TargetMonth = MONTH(getDate())";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                RepWorkingDays = sdr["WorkingDays"].ToString();
                            }
                        }
                        else
                        {
                            RepWorkingDays = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return RepWorkingDays;
        }

        public String getTargetForMonth(String repid)
        {
            string RepTarget = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT TargetCommission FROM dbo.Targets WHERE LoginID = " + repid + " AND TargetMonth = MONTH(getDate()) AND TargetYear = YEAR(getDate())";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                RepTarget = sdr["TargetCommission"].ToString();
                            }
                        }
                        else
                        {
                            RepTarget = "0";
                        }

                    }
                    conn.Close();
                }

            }
            return RepTarget;
        }

        public String getTargetForMonthFilter(String repid,string startD, string endD)
        {
            string RepTarget = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var month = Convert.ToDateTime(startD).Month;
                    var year = Convert.ToDateTime(startD).Year;
                    cmd.CommandText = "SELECT TargetCommission FROM dbo.Targets WHERE LoginID = " + repid + " AND TargetMonth = "+ month +" AND TargetYear = "+year;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                RepTarget = sdr["TargetCommission"].ToString();
                            }
                        }
                        else
                        {
                            RepTarget = "0";
                        }

                    }
                    conn.Close();
                }

            }
            return RepTarget;
        }
        public String populateDailyNoofSales()
        {
            String MonthTotSales = String.Empty;
            DelReport_Contact obj_Contact;

            foreach (var pair in di_getStat)
            {
                obj_Contact = (DelReport_Contact)pair.Value;
                String repName = obj_Contact.FirstName + " " + obj_Contact.LastName;
                MonthTotSales = getSalesForThisMonth(repName);

            }

            return MonthTotSales;

        }

        public float getMonthlyCommissionWithoutTodayStartEndWork(String RepID, string startD, string endD)
        {
            float RepCommission = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT SUM(VALUE) AS TOTALCOM FROM dbo.Commision WHERE UserLoginID = " + RepID
                         + " AND CreateDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                    + " AND STATUS NOT IN ('CANCELLED')";
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

        public float getMonthlyCommissionWithoutTodayStartEnd(String RepID, string startD, string endD)
        {
            float RepCommission = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT SUM(VALUE) AS TOTALCOM FROM dbo.Commision WHERE UserLoginID = " + RepID
                         + " AND CreateDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                    + " AND STATUS NOT IN ('CANCELLED')";
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

        public float getMonthlyCommissionWithoutToday(String RepID)
        {
            float RepCommission = 0;



            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VALUE) AS TOTALCOM FROM dbo.Commision WHERE UserLoginID = " + RepID + " AND DAY(CreateDateTime) BETWEEN '1' AND DAY(getDate() -1) AND MONTH(CreateDateTime) = MONTH(getdate()) AND YEAR(CreateDateTime) = YEAR(getDate()) AND STATUS NOT IN ('CANCELLED')";
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

        public float getMonthlyCommission(String RepID)
        {
            float RepCommission = 0;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT SUM(VALUE) AS TOTALCOM FROM dbo.Commision WHERE UserLoginID = " + RepID + " AND MONTH(CreateDateTime) = MONTH(getdate()) AND YEAR(CreateDateTime) = YEAR(getDate()) AND STATUS NOT IN ('CANCELLED')";
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
        public string getDailyCommissionStartEnd(String repID, string starD, string endD)
        {
            string RepCommission = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT SUM(VALUE) AS TOTALCOM FROM dbo.Commision WHERE UserLoginID = " + repID
                        + " AND CreateDateTime between  "
                            + "'" + starD + "'" + " AND   " + "'" + endD + "'"
                   + " AND STATUS NOT IN ('CANCELLED')";
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
                                    RepCommission = "0";
                                }
                                else
                                {
                                    RepCommission = sdr["TOTALCOM"].ToString();
                                }

                            }
                        }
                        else
                        {
                            RepCommission = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return RepCommission;

        }

        public string getDailyCommission(String repID)
        {
            string RepCommission = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
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
                                    RepCommission = "0";
                                }
                                else
                                {
                                    RepCommission = sdr["TOTALCOM"].ToString();
                                }

                            }
                        }
                        else
                        {
                            RepCommission = "0";
                        }

                    }
                    conn.Close();

                }

            }
            return RepCommission;

        }

        public string getSalesForThisMonthStartEnd(string RepName, string startD, string endD)
        {
            string MonthlyTotSales = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT COUNT(*) AS TOTCOUNT FROM dbo.Orders WHERE AccountOwner = '" + RepName
                        + "' AND CreatedDateTime between  "
                            + "'" + startD + "'" + " AND   " + "'" + endD + "'"
                   + " AND Status NOT IN ('CANCELLED')";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                MonthlyTotSales = sdr["TOTCOUNT"].ToString();
                            }
                        }
                        else
                        {
                            MonthlyTotSales = "0";
                        }

                    }

                }
                conn.Close();

            }
            return MonthlyTotSales;
        }

        public string getSalesForThisMonth(string RepName)
        {
            string MonthlyTotSales = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT COUNT(*) AS TOTCOUNT FROM dbo.Orders WHERE AccountOwner = '" + RepName + "' AND DAY(CreatedDateTime) = DAY(getDate()) AND MONTH(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND Status NOT IN ('CANCELLED')";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                MonthlyTotSales = sdr["TOTCOUNT"].ToString();
                            }
                        }
                        else
                        {
                            MonthlyTotSales = "0";
                        }

                    }

                }
                conn.Close();

            }
            return MonthlyTotSales;
        }



        public void getRepName()
        {
            DelReport_Contact obj_contact;

            List<string> RepsName = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {

                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"SELECT LoginID, FirstName, LastName,Department,Commission , DonotShowOnStats FROM dbo.Logins 
                          WHERE LoginID NOT IN (4, 9,18, 8, 19,20,21,22,14,30) AND Active = 'Y'  ";
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {

                            while (sdr.Read())
                            {
                                var comme = 0;
                                if (sdr["Commission"] != DBNull.Value)
                                {
                                    comme = Convert.ToInt32(sdr["Commission"].ToString());
                                    if (comme > 0)
                                    {
                                        var donotShowstat = sdr["DonotShowOnStats"];
                                        if (donotShowstat != DBNull.Value)
                                        {
                                            if (Convert.ToBoolean(sdr["DonotShowOnStats"].ToString()) != true)
                                            {
                                                RepsName.Add(sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString());

                                                obj_contact = new DelReport_Contact();
                                                obj_contact.DepartmentId = "3";// null avoid
                                                obj_contact.FirstName = sdr["FirstName"].ToString();
                                                obj_contact.LastName = sdr["LastName"].ToString();

                                                if (sdr["Department"] != DBNull.Value)
                                                    obj_contact.DepartmentId = sdr["Department"].ToString();
                                                di_getStat.Add(sdr["LoginID"].ToString(), obj_contact);
                                            }
                                        }
                                        else
                                        {
                                            RepsName.Add(sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString());


                                            obj_contact = new DelReport_Contact();
                                            obj_contact.DepartmentId = "3";// null avoid
                                            obj_contact.FirstName = sdr["FirstName"].ToString();
                                            obj_contact.LastName = sdr["LastName"].ToString();

                                            if (sdr["Department"] != DBNull.Value)
                                                obj_contact.DepartmentId = sdr["Department"].ToString();
                                            di_getStat.Add(sdr["LoginID"].ToString(), obj_contact);
                                        }
                                    }
                                }


                            }
                        }

                    }
                    conn.Close();

                }

            }

        }
    }
}