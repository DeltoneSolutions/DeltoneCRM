using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data.OleDb;

using DeltoneCRM_DAL;

using System.Text;
using System.IO;

namespace DeltoneCRM.Reports
{
    public partial class ReportMonthlyRep : System.Web.UI.Page
    {
        public static string RepName;
        public static int dateRange;
        Dictionary<String, DelReport_Contact> di_getStat = new Dictionary<String, DelReport_Contact>();
        DelReport_Contact obj_Contact;
        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("..\\dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            {
                Response.Redirect("Reports.aspx");
            }
            else
                Response.Redirect("Reports.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillRepsList();
                enddate.Value = DateTime.Now.Year.ToString();
                var dateNow = DateTime.Now;
                StartDateTxt.Value = new DateTime(dateNow.Year, dateNow.Month, 1).ToString("dd-MM-yyyy");
                var days = DateTime.DaysInMonth(dateNow.Year, dateNow.Month);
                var endate = DateTime.Now;
                EndDateTxt.Value = endate.ToString("dd-MM-yyyy");
            }

            if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            {
                repdropTr.Visible = false;
                Button2.Visible = false;
            }

            var stDate11 = StartDateTxt.Value;
            var enDate11 = EndDateTxt.Value;
            var oneDayBefore = enDate11;


        }

        public void fillRepsList()
        {
            if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            {
            }
            else
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    String strLoggedUsers = String.Empty;
                    DataTable subjects = new DataTable();

                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    SqlDataAdapter adapter = new SqlDataAdapter("select FirstName + ' ' + LastName AS FullName, LoginID from dbo.Logins WHERE LoginID NOT IN (4, 9, 8, 19,20,21,22) AND Active = 'Y' ", conn);
                    adapter.Fill(subjects);
                    ddlRepList.DataSource = subjects;
                    ddlRepList.DataTextField = "FullName";
                    ddlRepList.DataValueField = "LoginID";
                    ddlRepList.DataBind();
                    ddlRepList.Items.Insert(0, new ListItem("ALL", "9999"));
                }
            }
        }

        public string SetNewColumn()
        {
            return ",";
        }

        protected void GR_Click(object sender, EventArgs e)
        {
            //if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
            //{
            //    RepName = Session["LoggedUserID"].ToString();
            //}
            //else
            //    RepName = ddlRepList.SelectedValue.ToString();
            //String Month = DropDownList1.SelectedValue.ToString();
            //String Year = enddate.Value.ToString();
            //// Response.Redirect("MonthlySalesFigures.aspx?LoginID=" + RepName + "&Month=" + Month + "&Year=" + Year);
            //var startDate = StartDateTxt.Value;
            //var endDate = EndDateTxt.Value;

            //Response.Redirect("MonthlySalesFigures.aspx?LoginID=" + RepName + "&start=" + startDate + "&end=" + endDate);
            //  CallPivotTalbeExcel();

            DataTable subjects = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlDataAdapter adapter = new SqlDataAdapter("select FirstName + ' ' + LastName AS FullName, LoginID from dbo.Logins WHERE LoginID NOT IN (4, 9, 8, 19,20,21,22) AND Active = 'Y' ", conn);
            adapter.Fill(subjects);

            DataTable dataTable = null;//GetData(connStr);

            var stDate11 = StartDateTxt.Value;
            var enDate11 = EndDateTxt.Value;
            var oneDayBefore = enDate11;

            string dateStart, dateEnd, onedayBefore;
            getRepName();
            // ChangeDate(stDate11, enDate11, out dateStart, out dateEnd, out onedayBefore);

            var list = FillCommissionForRep(stDate11, enDate11);



            var targetMonth = Convert.ToDateTime(stDate11).Month;
            var targetYear = Convert.ToDateTime(stDate11).Year;
            var filenameStart = DateTime.Now.Ticks;
            var filenameStartna = "result1" + filenameStart + ".csv";

            var filePath = "c:\\Temp\\" + filenameStartna;
            StreamWriter SW = File.CreateText(filePath);
            if (ddlRepList.SelectedValue == "9999")
            {
                CreateCSVFIle2(list, targetMonth, targetYear, filePath, SW);
                CreateCSVFIle(list, targetMonth, targetYear, filePath, SW);
            }
            else
            {
                var repName = ddlRepList.SelectedItem.Text.Split(' ')[0].Trim();

                list = (from s in list where s.RepName == repName select s).ToList();
                // StreamWriter SW = File.CreateText(filePath);
                SW.Write("CommissionReport,Date,,\r\n");
                string str = ",,";


                str = str + repName + ",Total" + "\r\n";

                SW.Write(str); ;
                var target = ",";
                var total = Math.Truncate(Convert.ToDecimal(getTargetForMonth(obj_Contact.LoginId.ToString(), targetMonth, targetYear)));
                target = target + "Monthly Target," + total;
                target = target + "\r\n";
                SW.Write(target);
                SW.Write("\r\n");
                string comm = "";
                foreach (var item in list)
                {
                    comm = "," + item.DateRange + "," + Math.Truncate(Convert.ToDecimal(item.Commission)) + "," + Math.Truncate(Convert.ToDecimal(item.Commission)) + "\r\n";
                    SW.Write(comm);
                }
                SW.Close();


            }
            //Save and Launch
            // workbook.SaveToFile(@"c:\Temp\result.xlsx", ExcelVersion.Version2010);
            // System.Diagnostics.Process.Start(filePath);

            //System.Web.HttpResponse response = System.Web.HttpResponse.Response;
            Response.ClearContent();
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.AddHeader("Content-Disposition",
                               "attachment; filename=" + filenameStartna + ";");
            Response.TransmitFile(filePath);
            Response.Flush();
            Response.End();

            String stroutput = String.Empty;
        }




        private void CreateCSVFIle(List<RepPivotTable> list, int targetMonth, int targetYear, string filePath, StreamWriter SW)
        {

            var gropList = list.GroupBy(p => p.DateRange).ToList();
            String stringBuilder = String.Empty;

            SW.Write("\r\n");
            // SW.Write("CommissionReport,Date,,,,,,,,\r\n");
            SW.Write("CommissionReport,Date,,,,\r\n");
            string str = ",";
            var target = ",Monthly Target";
            var dailyRequired = ",Daily Target";
            var titleForSub = ",,D/Com,Com+/-,M/Com,Budget,%,D/Req,D/Com,Com+/-,M/Com,Budget,%,D/Req,D/Com,Com+/-,M/Com,Budget,%,D/Req";
            var dailyTotal = 0.0m;
            decimal calTotalMonthly = 0.0m;
            var j = 0;
            var listRequired = new List<decimal>();
            var listworkingDays = new List<int>();
            var listMonthBudget = new List<decimal>();
            var repIds = new List<int>();
            foreach (var pair in di_getStat)
            {

                if (j > 2)
                {
                    obj_Contact = (DelReport_Contact)pair.Value;
                    String repName = obj_Contact.FirstName;//+ " " + obj_Contact.LastName;

                    str = str + "," + repName;
                    var total = Math.Truncate(Convert.ToDecimal((getTargetForMonth(obj_Contact.LoginId.ToString(), targetMonth, targetYear))));
                    var dailyTota = total / (int)getWorkingDays(obj_Contact.LoginId.ToString());
                    listworkingDays.Add(getWorkingDays(obj_Contact.LoginId.ToString()));
                    listRequired.Add(dailyTota);
                    calTotalMonthly = calTotalMonthly + total;

                    dailyTotal = dailyTotal + dailyTota;
                    target = target + "," + total;
                    listMonthBudget.Add(total);
                    dailyRequired = dailyRequired + "," + Math.Truncate(dailyTota);

                    str = str + ",,,,,";
                    dailyRequired = dailyRequired + ",,,,,";
                    target = target + ",,,,,";
                    repIds.Add(obj_Contact.LoginId);

                }
                j = j + 1;
            }

            str = str + ",Total" + "\r\n";
            SW.Write(str);

            target = target + "," + calTotalMonthly + "\r\n";
            SW.Write(target);

            dailyRequired = dailyRequired + "," + Math.Truncate(dailyTotal) + "\r\n";
            SW.Write(dailyRequired);

            SW.Write("\r\n");
            SW.Write(titleForSub);
            SW.Write("\r\n");
            SW.Write("\r\n");
            var salesrep = 0.0m;
            var listcommision = new List<decimal>();
            var dimComm = 0.0m;
            var tarasComm = 0.0m;
            var johnComm = 0.0m;
            var trentComm = 0.0m;
            var aidanComm = 0.0m;
            var samComm = 0.0m;

            var dimdayRequiredAcc = 0.0m;
            var tarasdayRequiredAcc = 0.0m;
            var johndayRequiredAcc = 0.0m;
            var trentayRequiredAcc = 0.0m;
            var aidandayRequiredAcc = 0.0m;
            var samdayRequiredAcc = 0.0m;
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
            List<DateTime> bankHolidays = new List<DateTime>();

            var listDayOffs = repDayOffDal.GetAusHoliday();

            foreach (var item in listDayOffs)
                bankHolidays.Add(item.HolidayDate);

            foreach (var pair in gropList)
            {
                var i = 0;
                // int Length = StringBuilder.Length;
                //StringBuilder = StringBuilder.Substring(0, (Length - 1));
                var comm = "," + Convert.ToDateTime(pair.Key).Day + "--" + Convert.ToDateTime(pair.Key).Month + "-" + Convert.ToDateTime(pair.Key).ToString("yy");
                var totaCom = 0.0m;
                var count = 0;
                foreach (var item in pair)
                {
                    totaCom = totaCom + Math.Truncate(Convert.ToDecimal(item.Commission));

                    salesrep = Math.Truncate(Convert.ToDecimal(item.Commission)) - listRequired[i];
                    var IndiComm = Math.Truncate(Convert.ToDecimal(item.Commission));
                    if (count > 2)
                    {
                        List<DateTime> repDaysoffas = new List<DateTime>();
                        if (i == 0)
                        {
                            trentComm = trentComm + IndiComm;


                            trentayRequiredAcc = trentayRequiredAcc + listRequired[i];
                            var calPercentageRequ = (trentComm / trentayRequiredAcc - 1) * 100;

                            var dailyTarget = Math.Truncate(Convert.ToDecimal(item.DailyRequired));

                            //  counn = BusinessDaysUntil(startDate, end, bankHolidays, repDaysoffas);

                            comm = comm + "," + Math.Truncate(Convert.ToDecimal(item.Commission))
                               + "," + Math.Truncate(salesrep) + "," + Math.Truncate(trentComm)
                               + "," + Math.Truncate(trentayRequiredAcc) + "," + Math.Truncate(calPercentageRequ) + "%" + "," + dailyTarget;
                        }
                        else
                            if (i == 1)
                            {
                                aidanComm = aidanComm + IndiComm;

                                aidandayRequiredAcc = aidandayRequiredAcc + listRequired[i];
                                var calPercentageRequ = (aidanComm / aidandayRequiredAcc - 1) * 100;

                                var dailyTarget = Math.Truncate(Convert.ToDecimal(item.DailyRequired));

                                comm = comm + "," + Math.Truncate(Convert.ToDecimal(item.Commission))
                                  + "," + Math.Truncate(salesrep) + "," + Math.Truncate(aidanComm)
                                  + "," + Math.Truncate(aidandayRequiredAcc) + "," + Math.Truncate(calPercentageRequ) + "%" + "," + dailyTarget;
                            }
                            else
                            {
                                samComm = samComm + IndiComm;

                                samdayRequiredAcc = samdayRequiredAcc + listRequired[i];
                                var calPercentageRequ = (samComm / samdayRequiredAcc - 1) * 100;

                                var dailyTarget = Math.Truncate(Convert.ToDecimal(item.DailyRequired));

                                comm = comm + "," + Math.Truncate(Convert.ToDecimal(item.Commission)) + ","
                                  + Math.Truncate(salesrep) + "," + Math.Truncate(samComm)
                                  + "," + Math.Truncate(samdayRequiredAcc) + "," + Math.Truncate(calPercentageRequ) + "%" + "," + dailyTarget;
                            }

                        i = i + 1;
                    }
                    count = count + 1;

                }
                comm = comm + "," + totaCom + "\r\n";
                SW.Write(comm);
            }
            SW.Close();
        }
        private void CreateCSVFIle2(List<RepPivotTable> list, int targetMonth,
            int targetYear, string filePath, StreamWriter SW)
        {

            var gropList = list.GroupBy(p => p.DateRange).ToList();
            String stringBuilder = String.Empty;


            // SW.Write("CommissionReport,Date,,,,,,,,\r\n");
            SW.Write("CommissionReport,Date,,,,\r\n");
            string str = ",";
            var target = ",Monthly Target";
            var dailyRequired = ",Daily Target";
            var titleForSub = ",,D/Com,Com+/-,M/Com,Budget,%,D/Req,D/Com,Com+/-,M/Com,Budget,%,D/Req,D/Com,Com+/-,M/Com,Budget,%,D/Req";
            var dailyTotal = 0.0m;
            decimal calTotalMonthly = 0.0m;
            var listRequired = new List<decimal>();
            var j = 0;
            var listworkingDays = new List<int>();
            var listMonthBudget = new List<decimal>();
            var repIds = new List<int>();
            foreach (var pair in di_getStat)
            {

                if (j < 3)
                {
                    obj_Contact = (DelReport_Contact)pair.Value;
                    String repName = obj_Contact.FirstName;//+ " " + obj_Contact.LastName;

                    str = str + "," + repName;
                    var total = Math.Truncate(Convert.ToDecimal((getTargetForMonth(obj_Contact.LoginId.ToString(), targetMonth, targetYear))));
                    var dailyTota = total / (int)getWorkingDays(obj_Contact.LoginId.ToString());
                    listRequired.Add(dailyTota);
                    calTotalMonthly = calTotalMonthly + total;

                    dailyTotal = dailyTotal + dailyTota;
                    target = target + "," + total;
                    dailyRequired = dailyRequired + "," + Math.Truncate(dailyTota);

                    str = str + ",,,,,";
                    dailyRequired = dailyRequired + ",,,,,";
                    target = target + ",,,,,";
                    listworkingDays.Add(getWorkingDays(obj_Contact.LoginId.ToString()));
                    listMonthBudget.Add(total);
                    j = j + 1;
                    repIds.Add(obj_Contact.LoginId);
                }
            }

            str = str + ",Total" + "\r\n";
            SW.Write(str);

            target = target + "," + calTotalMonthly + "\r\n";
            SW.Write(target);

            dailyRequired = dailyRequired + "," + Math.Truncate(dailyTotal) + "\r\n";
            SW.Write(dailyRequired);

            SW.Write("\r\n");

            SW.Write(titleForSub);
            SW.Write("\r\n");
            SW.Write("\r\n");
            var salesrep = 0.0m;
            var listcommision = new List<decimal>();
            var dimComm = 0.0m;
            var tarasComm = 0.0m;
            var johnComm = 0.0m;
            var trentComm = 0.0m;
            var aidanComm = 0.0m;
            var samComm = 0.0m;

            var dimdayRequiredAcc = 0.0m;
            var tarasdayRequiredAcc = 0.0m;
            var johndayRequiredAcc = 0.0m;
            var trentayRequiredAcc = 0.0m;
            var aidandayRequiredAcc = 0.0m;
            var samdayRequiredAcc = 0.0m;
            foreach (var pair in gropList)
            {
                var i = 0;
                // int Length = StringBuilder.Length;
                //StringBuilder = StringBuilder.Substring(0, (Length - 1));
                var comm = "," + Convert.ToDateTime(pair.Key).Day + "--" + Convert.ToDateTime(pair.Key).Month + "-" + Convert.ToDateTime(pair.Key).ToString("yy");
                var totaCom = 0.0m;
                var count = 0;
                foreach (var item in pair)
                {
                    if (count < 3)
                    {

                        totaCom = totaCom + Math.Truncate(Convert.ToDecimal(item.Commission));

                        salesrep = Math.Truncate(Convert.ToDecimal(item.Commission)) - listRequired[i];
                        var IndiComm = Math.Truncate(Convert.ToDecimal(item.Commission));

                        if (i == 0)
                        {
                            dimComm = dimComm + IndiComm;

                            dimdayRequiredAcc = dimdayRequiredAcc + listRequired[i];

                            var calPercentageRequ = ((dimComm) / dimdayRequiredAcc - 1) * 100;

                            var dailyTarget = Math.Truncate(Convert.ToDecimal(item.DailyRequired));

                            comm = comm + "," + Math.Truncate(Convert.ToDecimal(item.Commission)) + ","
                               + Math.Truncate(salesrep) + "," + Math.Truncate(dimComm) + ","
                               + Math.Truncate(dimdayRequiredAcc) + "," + Math.Truncate(calPercentageRequ) + "%" + "," + dailyTarget;
                        }
                        else
                            if (i == 1)
                            {
                                tarasComm = tarasComm + IndiComm;

                                tarasdayRequiredAcc = tarasdayRequiredAcc + listRequired[i];
                                var calPercentageRequ = (tarasComm / tarasdayRequiredAcc - 1) * 100;

                                var dailyTarget = Math.Truncate(Convert.ToDecimal(item.DailyRequired));

                                comm = comm + "," + Math.Truncate(Convert.ToDecimal(item.Commission))
                                   + "," + Math.Truncate(salesrep) + "," + Math.Truncate(tarasComm) + ","
                                   + Math.Truncate(tarasdayRequiredAcc) + "," + Math.Truncate(calPercentageRequ) + "%" + "," + dailyTarget;
                            }
                            else
                                if (i == 2)
                                {
                                    johnComm = johnComm + IndiComm;

                                    johndayRequiredAcc = johndayRequiredAcc + listRequired[i];

                                    var calPercentageRequ = (johnComm / johndayRequiredAcc - 1) * 100;

                                    var dailyTarget = Math.Truncate(Convert.ToDecimal(item.DailyRequired));

                                    comm = comm + "," + Math.Truncate(Convert.ToDecimal(item.Commission))
                                       + "," + Math.Truncate(salesrep) + "," + Math.Truncate(johnComm)
                                       + "," + Math.Truncate(johndayRequiredAcc) + "," + Math.Truncate(calPercentageRequ) + "%" + "," + dailyTarget;
                                }

                        i = i + 1;
                        count = count + 1;
                    }



                }
                comm = comm + "," + totaCom + "\r\n";
                SW.Write(comm);
            }

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




        public String getTargetForMonth(String repid, int month, int year)
        {
            string RepTarget = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "SELECT TargetCommission FROM dbo.Targets WHERE LoginID = " + repid + " AND TargetMonth = " + month + " AND TargetYear = " + year;
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

        public int getWorkingDays(String repid)
        {
            int RepWorkingDays = 0;
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
                                RepWorkingDays = Convert.ToInt32(sdr["WorkingDays"].ToString());
                            }
                        }



                    }
                    conn.Close();

                }

            }
            return RepWorkingDays;
        }



        private List<RepPivotTable> FillCommissionForRep(string starD, string endD)
        {
            var listobj = new List<RepPivotTable>();

            var conStartDate = Convert.ToDateTime(starD).ToShortDateString();
            //  var  ssdateStart = conStartDate.ToString("dd-MM-YYYY");
            List<DateTime> bankHolidays = new List<DateTime>();
            List<DateTime> repDaysoffas = new List<DateTime>();
            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
            var listDayOffs = repDayOffDal.GetAusHoliday();

            foreach (var item in listDayOffs)
                bankHolidays.Add(item.HolidayDate);
            var endDateCon = Convert.ToDateTime(endD);
            var startDateCon = Convert.ToDateTime(starD);
            for (var startDate = startDateCon; startDate <= endDateCon; startDate = startDate.AddDays(1))
            {
                foreach (var pair in di_getStat)
                {
                    if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
                    {

                    }
                    else
                    {
                        starD = startDate.ToString("yyyy-MM-dd");
                        endD = startDate.AddDays(1).ToString("yyyy-MM-dd");


                        var res = getDailyCommissionStartEnd(pair.Key, starD, endD);
                        obj_Contact = (DelReport_Contact)pair.Value;
                        String repName = obj_Contact.FirstName;//+ " " + obj_Contact.LastName;

                        var total = Math.Truncate(Convert.ToDecimal((getTargetForMonth(obj_Contact.LoginId.ToString(),
                         startDate.Month, startDate.Year))));
                        var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                        var repListOffs = repDayOffDal.GetRepDayOffByRepId(obj_Contact.LoginId);
                        foreach (var itemday in repListOffs)
                            repDaysoffas.Add(itemday.DayOff);

                        var counn = BusinessDaysUntil(firstDayOfMonth, Convert.ToDateTime(endD).AddDays(-1), bankHolidays, repDaysoffas);
                        int WorkingDays = getWorkingDays(obj_Contact.LoginId.ToString());
                        var totalCo = (from rpCo in listobj where rpCo.RepName == repName select rpCo).ToList();
                        var tota = 0.0m;
                        int RemainingDays = (WorkingDays) - counn;
                        foreach (var item in totalCo)
                        {
                            tota = tota + Convert.ToDecimal(item.Commission);
                        }
                        tota = tota + Convert.ToDecimal(res);

                        if (RemainingDays > 0)
                            tota = (total - tota) / RemainingDays;
                        else
                            tota = (total - tota);
                        var obj = new RepPivotTable()
                        {
                            Commission = res,
                            RepName = repName,
                            DailyRequired = tota.ToString(),
                            DateRange = startDate.ToShortDateString()
                        };

                        listobj.Add(obj);
                    }
                }
            }

            return listobj;
        }

        private void UpdaetEt(List<RepPivotTable> tan,int taa)
        {

            var groupList = tan.GroupBy(p => p.DateRange);
            string str = ",";
            var totaComm=0.0m;
            foreach (var itmem in groupList)
            {
                foreach (var ite in itmem)
                {
                    var ttt = ite.Commission;
                    totaComm = totaComm + 22; 
                }
            }
        }

        private void CreateVolumeFile(List<RepPivotTable> list, int targetMonth,
            int targetYear, string filePath, StreamWriter SW)
        {
            var gropList = list.GroupBy(p => p.DateRange).ToList();
            String stringBuilder = String.Empty;


            // SW.Write("CommissionReport,Date,,,,,,,,\r\n");
            SW.Write("CommissionReport,Volume,,,\r\n");
            string str = ",";
          
            var dailyTotal = 0.0m;
            decimal calTotalMonthly = 0.0m;
            var listRequired = new List<decimal>();
            var j = 0;
            var listworkingDays = new List<int>();
            var listMonthBudget = new List<decimal>();
            var repIds = new List<int>();
            foreach (var pair in di_getStat)
            {
                    obj_Contact = (DelReport_Contact)pair.Value;
                    String repName = obj_Contact.FirstName;//+ " " + obj_Contact.LastName;

                    str = str + "," + repName;
            }

            str = str + ",Total" + "\r\n";
            SW.Write(str);

            SW.Write("\r\n");

           
            foreach (var pair in gropList)
            {
                var i = 0;
                // int Length = StringBuilder.Length;
                //StringBuilder = StringBuilder.Substring(0, (Length - 1));
                var comm = "," + Convert.ToDateTime(pair.Key).Day + "--" + Convert.ToDateTime(pair.Key).Month + "-" + Convert.ToDateTime(pair.Key).ToString("yy");
                var totaCom = 0.0m;
                var count = 0;
                foreach (var item in pair)
                {

                    totaCom = totaCom + Math.Truncate(Convert.ToDecimal(item.Commission));
                    var IndiComm = Math.Truncate(Convert.ToDecimal(item.Commission));

                    comm = comm + "," + IndiComm;
                }
                comm = comm + "," + totaCom + "\r\n";
                SW.Write(comm);
            }
        }

        public List<RepPivotTable> FillVolumeForRep(string starD, string endD)
        {
            var listobj = new List<RepPivotTable>();
            var conStartDate = Convert.ToDateTime(starD).ToShortDateString();
            //  var  ssdateStart = conStartDate.ToString("dd-MM-YYYY");

            var endDateCon = Convert.ToDateTime(endD);
            var startDateCon = Convert.ToDateTime(starD);

            for (var startDate = startDateCon; startDate <= endDateCon; startDate = startDate.AddDays(1))
            {
                foreach (var pair in di_getStat)
                {
                    if (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday)
                    {

                    }
                    else
                    {
                        starD = startDate.ToString("yyyy-MM-dd");
                        endD = startDate.AddDays(1).ToString("yyyy-MM-dd");
                        obj_Contact = (DelReport_Contact)pair.Value;
                        String repName = obj_Contact.FirstName;//+ " " + obj_Contact.LastName;
                        String repNameFullName = obj_Contact.FirstName + " " + obj_Contact.LastName;
                        var res = getMonthlyVolumeWithoutTodayStartEnd(repNameFullName, starD, endD).ToString();
                        var obj = new RepPivotTable()
                        {
                            Commission = res,
                            RepName = repName,
                            DateRange = startDate.ToShortDateString()
                        };

                        listobj.Add(obj);
                    }
                }
            }

            return listobj;
        }

        private void ChangeDate(string startDate, string endDate, out string dateStart, out string dateEnd, out string onedayBefore)
        {
            var conStartDate = Convert.ToDateTime(startDate);
            dateStart = conStartDate.ToString("yyyy-MM-dd");

            var conEndDate = Convert.ToDateTime(endDate);
            onedayBefore = conEndDate.ToString("yyyy-MM-dd");
            conEndDate = conEndDate.AddDays(1);
            dateEnd = conEndDate.ToString("yyyy-MM-dd");

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
                        + " AND convert(varchar(10),CreateDateTime, 120) = "
                            + "'" + starD + "'"
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

        public float getMonthlyVolumeWithoutTodayStartEnd(String repname, string starD, string endD)
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
                       + "' AND convert(varchar(10),CreatedDateTime, 120) = "
                            + "'" + starD + "'"
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
                     + "' AND convert(varchar(10),CreatedDateTime, 120) = "
                            + "'" + starD + "'"
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
                        + "' AND convert(varchar(10),CreatedDateTime, 120) = "
                            + "'" + starD + "'"
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
                       + "' AND convert(varchar(10),DateCreated, 120) = "
                            + "'" + starD + "'"
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
                         + "' AND convert(varchar(10),DateCreated, 120) = "
                            + "'" + starD + "'"
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
                       + "' AND convert(varchar(10),DateCreated, 120) = "
                            + "'" + starD + "'"
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
                          WHERE LoginID NOT IN (4, 9,18, 8, 19,3,7,29 ,31 ,34 ,36,32,20,21,22,14,30) AND Active = 'Y'  ";
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

                                        {
                                            RepsName.Add(sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString());

                                            obj_contact = new DelReport_Contact();
                                            obj_contact.DepartmentId = "3";// null avoid
                                            obj_contact.FirstName = sdr["FirstName"].ToString();
                                            obj_contact.LastName = sdr["LastName"].ToString();
                                            obj_contact.LoginId = Convert.ToInt32(sdr["LoginID"].ToString());
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





        static private DataTable GetData(string connStr)
        {
            //Connect Database
            OleDbConnection conn = new OleDbConnection(connStr);
            conn.Open();
            string cmdStr = "select * from parts";
            OleDbCommand command = new OleDbCommand(cmdStr, conn);
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            conn.Close();
            return dataTable;
        }



        public class RepPivotTable
        {
            public string RepName { get; set; }
            public string DateRange { get; set; }
            public string Commission { get; set; }
            public string DailyRequired { get; set; }
        }

        public List<RepPivotTable> SetData()
        {
            var list = new List<RepPivotTable>();

            list.Add(new RepPivotTable
            {
                Commission = "200",
                DateRange = "02-Desc",
                RepName = "John"

            });

            list.Add(new RepPivotTable
            {
                Commission = "300",
                DateRange = "02-Desc",
                RepName = "Dim"
            });
            list.Add(new RepPivotTable
            {
                Commission = "500",
                DateRange = "02-Desc",
                RepName = "Trent"

            });

            list.Add(new RepPivotTable
            {
                Commission = "300",
                DateRange = "03-Desc",
                RepName = "John"
            });
            list.Add(new RepPivotTable
            {
                Commission = "700",
                DateRange = "03-Desc",
                RepName = "Trent"
            });
            list.Add(new RepPivotTable
            {
                Commission = "800",
                DateRange = "03-Desc",
                RepName = "Dim"
            });

            list.Add(new RepPivotTable
            {
                Commission = "400",
                DateRange = "04-Desc",
                RepName = "John"
            });
            list.Add(new RepPivotTable
            {
                Commission = "500",
                DateRange = "04-Desc",
                RepName = "Dim"
            });
            list.Add(new RepPivotTable
            {
                Commission = "500",
                DateRange = "04-Desc",
                RepName = "Trent"
            });
            list.Add(new RepPivotTable
            {
                Commission = "",
                DateRange = "04-Desc",
                RepName = ""
            });

            return list;
        }

        protected void VL_Click(object sender, EventArgs ee)
        {
            var stDate11 = StartDateTxt.Value;
            var enDate11 = EndDateTxt.Value;

            var targetMonth = Convert.ToDateTime(stDate11).Month;
            var targetYear = Convert.ToDateTime(stDate11).Year;
            var filenameStart = DateTime.Now.Ticks;
            var filenameStartna = "result1vol" + filenameStart + ".csv";
            getRepName();
            var filePath = "c:\\Temp\\" + filenameStartna;
            StreamWriter SW = File.CreateText(filePath);

            var list = FillVolumeForRep(stDate11, enDate11);
            if (ddlRepList.SelectedValue == "9999")
            {
                CreateVolumeFile(list, targetMonth, targetYear, filePath, SW);
               
            }
            SW.Close();

            Response.ClearContent();
            Response.Clear();
            Response.ContentType = "text/plain";
            Response.AddHeader("Content-Disposition",
                               "attachment; filename=" + filenameStartna + ";");
            Response.TransmitFile(filePath);
            Response.Flush();
            Response.End();
        }
    }
}