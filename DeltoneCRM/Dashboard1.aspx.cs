using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using DeltoneCRM_DAL;

namespace DeltoneCRM
{
    public partial class Dashboard1 : System.Web.UI.Page
    {
        String strUserProfile = String.Empty;
        String strUserID = String.Empty;
        String strUserName = String.Empty;
        String hasTargetData = "";

        String TargetComission = "";
        String WorkingDays = "";

        int WorkingDaysOuter = 0;
        Decimal TargetComm = 0;
        Decimal MonthComm = 0;
        public string userName = "";
        LoginDAL login = new LoginDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        OrderDAL odrs = new OrderDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        QuoteDAL qts = new QuoteDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        CreditNotesDAL cn = new CreditNotesDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

           // Alert("test", this);

            if (String.IsNullOrEmpty(Session["LoggedUserID"] as String))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User session has expired. You will be redirected to the login page.')", true);
                Response.Redirect("http://delcrm");
            }


            if (!(String.IsNullOrEmpty(Session["USERPROFILE"] as String)))
            {
                strUserProfile = Session["USERPROFILE"].ToString();
                strUserID = Session["LoggedUserID"].ToString();
                strUserName = Session["LoggedUser"].ToString();
                userName = strUserName;
                if (strUserProfile.Equals("ADMIN"))
                {
                    //Admin User make Pending Approval Panel Visible
                    //pnlPendingApproval.Visible = true;
                    //Quotes_Headers.Visible = true;
                    //Quotes_DataTable.Visible = true;
                    tr_statsboard.Visible = true;
                }
            }
            setMonthlyPerformanceLabel();
            //getMonthlyCommission();
            float monComm = login.getMonthlyCommissionWithoutToday(strUserID);
            ComissionDIV.InnerText = String.Format("{0:C2}", monComm);

            float monVol = login.getMonthVolume(strUserName);
            monthlyvolume.InnerText = String.Format("{0:C2}", monVol);

            getTargets();
            int WorkedDays = calcWorkedDays();
            if (hasTargetData == "No")
            {
                //workingdaysDIV.InnerText = "0";
            }
            else
            {
                int RemainingWorkingDays = WorkingDaysOuter - WorkedDays;
                //workingdaysDIV.InnerText = RemainingWorkingDays.ToString();
            }
            targetDIV.InnerText = "$" + TargetComm.ToString();

            Decimal DailyNeed = getDailyTarget(WorkingDaysOuter, TargetComm, MonthComm);
            dailyneedDIV.InnerText = "$" + DailyNeed.ToString();
            setNumberOfNewAccounts();

            float DComm = login.getDailyCommission(strUserID);
            DayComm.InnerText = String.Format("{0:C2}", DComm);

            float DVol = login.getDailyVolume(strUserName);
            DayVol.InnerText = String.Format("{0:C2}", DVol);

            setDashNumbers();
            //DayComm.InnerHtml = "$" + setDailyCommission();
            // Create a timer
            System.Timers.Timer myTimer = new System.Timers.Timer();
            // Tell the timer what to do when it elapses
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(myEvent);
            // Set it to go off every five seconds
            myTimer.Interval = 30000;
            // And start it        
            myTimer.Enabled = true;
        }




//public static void Alert(string message,Page page)
//{
//       ScriptManager.RegisterStartupScript(page, page.GetType(),
//      "err_msg",
//      "alert('" + message + "');",
//      true);
//}

        public  void Alert(string message, Page page)
        {
            ScriptManager.RegisterStartupScript(page, page.GetType(),
           "err_msg",
           "alert('" + message + "');",
           true);
        }

        private void getMonthlyCommission()
        {
            String thisMonth = DateTime.Now.Month.ToString();
            Decimal MonthlyVolume = 0;
            Decimal MonthlyCOGTotal = 0;
            String MVString = "";
            float MVInt = 0;
            String COGString = "";
            Decimal COGTotal = 0;
            Decimal MVintNoGST = 0;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlStmt = "SELECT * FROM dbo.Orders WHERE Month(CreatedDateTime) = Month(getDate()) AND Year(CreatedDateTime) = Year(getDate()) AND CreatedBy = '" + Session["LoggedUser"] + "'";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            MVString = sdr["Total"].ToString();
                            MVInt = float.Parse(MVString);

                            MVintNoGST = Math.Round((decimal)(MVInt / 1.1),2);
                            COGString = sdr["COGTotal"].ToString();
                            
                            COGTotal = Math.Round(Convert.ToDecimal(COGString), 2);
                            MonthlyVolume = MonthlyVolume + MVintNoGST;
                            MonthlyCOGTotal = MonthlyCOGTotal + COGTotal;
                        }
                    }
                }
                conn.Close();
            }

            Decimal MonthlyGrossProfit = 0;
            Decimal MonthlyComission = 0;
            MonthlyComission = Math.Round((((MonthlyVolume - MonthlyCOGTotal) * 40) / 100), 2);
            monthlyvolume.InnerText = "$" + MonthlyVolume.ToString();
            ComissionDIV.InnerText = "$" + MonthlyComission.ToString();
            MonthComm = MonthlyComission;

        }

        private void myEvent(object source, System.Timers.ElapsedEventArgs e) { 
            setDashNumbers();
        }

        private void setDashNumbers()
        {
            spn_PendingOrders.InnerText = odrs.getNumberPendingOrders();
            spn_PendingQuotes.InnerText = qts.getNumberPendingQuotes();
            spn_PendingCredits.InnerText = cn.getNumberCreditPending();
            spn_OngoingCredits.InnerText = cn.getNumberPendingRMA();
        }

        private void getTargets()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlStmt = "SELECT * FROM dbo.Targets WHERE LoginID = " + Session["LoggedUserID"] + " AND TargetMonth = MONTH(getDate()) AND TargetYear = YEAR(getDate())";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        hasTargetData = "Yes";
                        while (sdr.Read())
                        {
                            TargetComission = sdr["TargetCommission"].ToString();
                            WorkingDays = sdr["WorkingDays"].ToString();
                            hidden_target_trigger.Text = "HasRows";
                        }
                    }
                    else
                    {
                        hasTargetData = "No";
                        WorkingDays = "0";
                        TargetComission = "0";
                        hidden_target_trigger.Text = "Empty";
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You do not have any targets for this month. Please enter your targets in the Manage Section.')", true);
                    }
                }
                conn.Close();
            }

            WorkingDaysOuter = Convert.ToInt16(WorkingDays);
            TargetComm = Math.Round(Convert.ToDecimal(TargetComission), 2);
        }

        private int calcWorkedDays()
        {

            int WorkedDays = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlStmt = "SELECT COUNT(DISTINCT DAY(LoggedDateTime)) AS Count FROM dbo.LoginAudit WHERE MONTH(LoggedDateTime) = MONTH(getDate()) AND YEAR(LoggedDateTime) = YEAR(getDate()) AND DATENAME(weekday, LoggedDateTime) NOT IN ('Saturday', 'Sunday') AND UserID = " + Session["LoggedUserID"].ToString();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            WorkedDays = Int32.Parse(sdr["Count"].ToString());
                        }
                    }
                    else
                    {
                        WorkedDays = 0;
                    }
                }
                conn.Close();
            }

            return WorkedDays;
        }

        private Decimal getDailyTarget(int WD, Decimal TC, Decimal MC)
        {


            int WorkedDays = calcWorkedDays();
            

            Decimal DailyNeed = 0;
            if (WD != 0)
            {

                if ((WD - WorkedDays) > 0)
                {

                    DailyNeed = (TC - MC) / (WD - WorkedDays);
                }
            }
            else
            {
                DailyNeed = 0;
            }
            

            return Math.Round(DailyNeed,2);
        }

        private void setNumberOfNewAccounts()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlStmt = "SELECT Count(CompanyID) AS CompTotal FROM dbo.Companies WHERE OwnershipAdminID = " + Session["LoggedUserID"] + " AND Month(CreatedDateTime) = MONTH(getDate()) AND YEAR(CreatedDateTime) = YEAR(getDate()) AND EXISTS (SELECT 1 FROM dbo.Orders WHERE Orders.CompanyID = Companies.CompanyID)";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            newaccountDIV.InnerText = sdr["CompTotal"].ToString();
                        }
                    }
                    else
                    {
                        newaccountDIV.InnerText = "0";
                    }
                }
                conn.Close();
            }
        }

        private String setDailyCommission()
        {
            Decimal TotalSales = 0;
            float TotalSalesInFloat = 0;
            String TotalSalesInString = "";
            Decimal TotalSalesEXGST = 0;
            Decimal TotalCOG = 0;

            Decimal DayCommission = 0;



            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlStmt = "SELECT * FROM dbo.Orders WHERE DAY(CreatedDateTime) = DAY(getDate()) AND Month(CreatedDateTime) = Month(getDate()) AND Year(CreatedDateTime) = Year(getDate()) AND CreatedBy = '" + Session["LoggedUser"] + "'";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = sqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            TotalSalesInString = sdr["Total"].ToString();
                            TotalSalesInFloat = float.Parse(TotalSalesInString);
                            TotalSalesEXGST = (decimal)(TotalSalesInFloat / 1.1);
                            TotalSales = Math.Round(TotalSales + TotalSalesEXGST, 2);


                            TotalCOG = TotalCOG + Decimal.Parse(sdr["COGTotal"].ToString());
                        }

                        DayCommission = Math.Round((((TotalSales - TotalCOG) * 40) / 100), 2);

                    }
                    else
                    {
                        DayCommission = 0;
                    }
                }

                conn.Close();
            }

            //DayVol.InnerHtml = "$" + TotalSales.ToString();
            return DayCommission.ToString();
        }

        private void setMonthlyPerformanceLabel()
        {

            int thisMonth = System.DateTime.Now.Month;
            int thisYear = System.DateTime.Now.Year;
            int thisDay = System.DateTime.Now.Day;

            String MonthName = "";

            if (thisMonth == 1)
            {
                MonthName = "January";
            }
            else if (thisMonth == 2)
            {
                MonthName = "February";
            }
            else if (thisMonth == 3)
            {
                MonthName = "March";
            }
            else if (thisMonth == 4)
            {
                MonthName = "April";
            }
            else if (thisMonth == 5)
            {
                MonthName = "May";
            }
            else if (thisMonth == 6)
            {
                MonthName = "June";
            }
            else if (thisMonth == 7)
            {
                MonthName = "July";
            }
            else if (thisMonth == 8)
            {
                MonthName = "August";
            }
            else if (thisMonth == 9)
            {
                MonthName = "September";
            }
            else if (thisMonth == 10)
            {
                MonthName = "October";
            }
            else if (thisMonth == 11)
            {
                MonthName = "November";
            }
            else if (thisMonth == 12)
            {
                MonthName = "December";
            }
            else
            {
                MonthName = "";
            }

            MonthYearlbl.InnerHtml = "Monthly Performance - " + MonthName + " " + thisYear;
            DailyPerflbl.InnerHtml = "Daily Performance - " + thisDay + " " + MonthName;
        }

    }
}