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


namespace DeltoneCRM.Reports.Queries
{
    public partial class getMonthlySalesFigures : System.Web.UI.Page
    {

        private void ChangeDate(string startDate,string endDate,out string dateStart,out string dateEnd)
        {
            var conStartDate = Convert.ToDateTime(startDate);
            dateStart = conStartDate.ToString("yyyy-MM-dd");

            var conEndDate = Convert.ToDateTime(endDate);
            conEndDate = conEndDate.AddDays(1);
            dateEnd = conEndDate.ToString("yyyy-MM-dd");

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String strOutput = String.Empty;
            float RunningSales = 0;

            String OperatorName = String.Empty;
            String OperatorID = Request.QueryString["repname"];
            OperatorName = getOperatorFullName(Request.QueryString["repname"]);
            var startDate = Request.QueryString["startDate"];
            var endDate = Request.QueryString["endDate"];
            if (string.IsNullOrEmpty(endDate))
                endDate = startDate;

            var stDate = startDate;
            var enDate = endDate;

            ChangeDate(startDate, endDate, out stDate, out enDate);
            var startDateConvert = Convert.ToDateTime(stDate);

            //List<String> FullOrderList = getOrderList(OperatorName, Request.QueryString["month"], Request.QueryString["year"]);
            var FullOrderList = getOrderListStartAndEnd(OperatorName, stDate, enDate);
            foreach(String SingleOrderNumber in FullOrderList)
            {
                String CompID = getCompanyID(SingleOrderNumber);

                String OrderDate = getOrderDate(SingleOrderNumber);
                strOutput = strOutput + OrderDate + "|";
                String CompanyName = getCompanyName(SingleOrderNumber);
                strOutput = strOutput + CompanyName + "|";
                String Volume = getOrderVolume(SingleOrderNumber);
                RunningSales = float.Parse(Volume);
                strOutput = strOutput + String.Format("{0:C2}", Volume) + "|";
                //String CreditVolume = getCreditVolume(SingleOrderNumber);
                //strOutput = strOutput + CreditVolume + "|";
                var repId=Request.QueryString["repname"];
                if(repId=="9999"){
                    repId = getRealAccountOwnerID(Convert.ToInt32(SingleOrderNumber));
                }
                String Commission = getCommission(SingleOrderNumber, repId);
                strOutput = strOutput + String.Format("{0:C2}", Commission) + "|";
                String NewAccount = getNewAccountCount(CompID, startDateConvert);
                strOutput = strOutput + NewAccount + "|";
                strOutput = strOutput + "|";
                strOutput = strOutput + "|";
                String RepName = getRepNameByOrder(SingleOrderNumber);
                strOutput = strOutput + RepName + "~";
            }

           // List<String> OrderWithCSAccountOwner = getOrderListWithCSAccountOwner(OperatorName, Request.QueryString["month"], Request.QueryString["year"]);
            List<String> OrderWithCSAccountOwner = getOrderListWithCSAccountOwnerStartEnd(OperatorName,
                stDate, enDate);

            foreach (String SingleOrderNumber in OrderWithCSAccountOwner)
            {
                String CompID = getCompanyID(SingleOrderNumber);

                String OrderDate = getOrderDate(SingleOrderNumber);
                strOutput = strOutput + OrderDate + "|";
                String CompanyName = getCompanyName(SingleOrderNumber);
                strOutput = strOutput + CompanyName + "|";
                String Volume = getOrderVolumeSplit(SingleOrderNumber);
                RunningSales = float.Parse(Volume);
                strOutput = strOutput + String.Format("{0:C2}", Volume) + "|";
                //String CreditVolume = getCreditVolume(SingleOrderNumber);
                //strOutput = strOutput + CreditVolume + "|";
                var repId = Request.QueryString["repname"];
                if (repId == "9999")
                {
                    repId = getRealAccountOwnerID(Convert.ToInt32(SingleOrderNumber));
                }
                String Commission = getCommission(SingleOrderNumber, repId);
                strOutput = strOutput + String.Format("{0:C2}", Commission) + "|";
                String NewAccount = getNewAccountCount(CompID, startDateConvert);
                strOutput = strOutput + NewAccount + "|";
                strOutput = strOutput + "|";
                strOutput = strOutput + "|";
                String RepName = getRepNameByOrder(SingleOrderNumber);
                strOutput = strOutput + RepName + "~";
            }

          //  List<String> OrderWithCommishSplit = getOrderListWithCommissionSplit(OperatorName, Request.QueryString["month"], Request.QueryString["year"]);



            List<String> OrderWithCommishSplit = getOrderListWithCommissionSplitStartEnd(OperatorName, stDate, enDate);
            foreach (String SingleOrderNumber in OrderWithCommishSplit)
            {
                String CompID = getCompanyID(SingleOrderNumber);

                String OrderDate = getOrderDate(SingleOrderNumber);
                strOutput = strOutput + OrderDate + "|";
                String CompanyName = getCompanyName(SingleOrderNumber);
                strOutput = strOutput + CompanyName + "|";
                String Volume = getOrderVolumeSplit(SingleOrderNumber);
                RunningSales = float.Parse(Volume);
                strOutput = strOutput + String.Format("{0:C2}", Volume) + "|";
                //String CreditVolume = getCreditVolume(SingleOrderNumber);
                //strOutput = strOutput + CreditVolume + "|";
                var repId = Request.QueryString["repname"];
                if (repId == "9999")
                {
                    repId = getRealAccountOwnerID(Convert.ToInt32(SingleOrderNumber));
                }
                String Commission = getCommission(SingleOrderNumber, repId);
                strOutput = strOutput + String.Format("{0:C2}", Commission) + "|";
                String NewAccount = getNewAccountCount(CompID, startDateConvert);
                strOutput = strOutput + NewAccount + "|";
                strOutput = strOutput + "|";
                strOutput = strOutput + "|";
                String RepName = getRepNameByOrder(SingleOrderNumber);
                strOutput = strOutput + RepName + "~";
            }

           // List<String> FullCreditListWithoutCS = getCreditList(OperatorName, Request.QueryString["month"], Request.QueryString["year"]);

            List<String> FullCreditListWithoutCS = getCreditListStartEnd(OperatorName, stDate, enDate);
            if (FullCreditListWithoutCS.Count != 0)
            {
                foreach (String SingleCreditNumber in FullCreditListWithoutCS)
                {
                    String CreditDate = getCreditDate(SingleCreditNumber);
                    strOutput = strOutput + CreditDate + "|";
                    String CompanyName = getCompanyNameByCredit(SingleCreditNumber);
                    strOutput = strOutput + CompanyName + "|";
                    strOutput = strOutput + "|";
                    strOutput = strOutput + "|";
                    strOutput = strOutput + "|";
                    var repId = Request.QueryString["repname"];
                    if (repId == "9999")
                    {
                        repId = getRealAccountOwnerID(Convert.ToInt32(SingleCreditNumber));
                    }
                    String CreditCommission = getCreditCommission(SingleCreditNumber, repId);
                    strOutput = strOutput + String.Format("{0:C2}", CreditCommission) + "|";
                    String CreditVolume = getCreditVolume(SingleCreditNumber);
                    Double CreditVolumeinFloat= Double.Parse(CreditVolume);
                    strOutput = strOutput + "-" + String.Format("{0:C2}", CreditVolumeinFloat) + "|";
                    String RepName = getRepNameByCredit(SingleCreditNumber);
                    strOutput = strOutput + RepName + "~";

                }
            }

         //   List<String> FullCreditListWithCSAO = getCreditListWithCSAO(OperatorName, Request.QueryString["month"], Request.QueryString["year"]);

            List<String> FullCreditListWithCSAO = getCreditListWithCSAOStartEnd(OperatorName, stDate, enDate);
            if (FullCreditListWithCSAO.Count != 0)
            {
                foreach (String SingleCreditNumber in FullCreditListWithCSAO)
                {
                    String CreditDate = getCreditDate(SingleCreditNumber);
                    strOutput = strOutput + CreditDate + "|";
                    String CompanyName = getCompanyNameByCredit(SingleCreditNumber);
                    strOutput = strOutput + CompanyName + "|";
                    strOutput = strOutput + "|";
                    strOutput = strOutput + "|";
                    strOutput = strOutput + "|";
                    var repId = Request.QueryString["repname"];
                    if (repId == "9999")
                    {
                        repId = getRealAccountOwnerID(Convert.ToInt32(SingleCreditNumber));
                    }
                    String CreditCommission = getCreditCommission(SingleCreditNumber, repId);
                    strOutput = strOutput + String.Format("{0:C2}", CreditCommission) + "|";
                    String CreditVolume = getCreditVolumeSplit(SingleCreditNumber);
                    Double CreditVolumeinFloat = Double.Parse(CreditVolume);
                    strOutput = strOutput + "-" + String.Format("{0:C2}", CreditVolumeinFloat) + "|";
                    String RepName = getRepNameByCredit(SingleCreditNumber);
                    strOutput = strOutput + RepName + "~";

                }
            }

          //  List<String> FullCreditListWithCSSW = getCreditListWithCSSW(OperatorName, Request.QueryString["month"], Request.QueryString["year"]);

            List<String> FullCreditListWithCSSW = getCreditListWithCSSWStartEnd(OperatorName, stDate, enDate);
           
            if (FullCreditListWithCSSW.Count != 0)
            {
                foreach (String SingleCreditNumber in FullCreditListWithCSSW)
                {
                    String CreditDate = getCreditDate(SingleCreditNumber);
                    strOutput = strOutput + CreditDate + "|";
                    String CompanyName = getCompanyNameByCredit(SingleCreditNumber);
                    strOutput = strOutput + CompanyName + "|";
                    strOutput = strOutput + "|";
                    strOutput = strOutput + "|";
                    strOutput = strOutput + "|";
                    var repId = Request.QueryString["repname"];
                    if (repId == "9999")
                    {
                        repId = getRealAccountOwnerID(Convert.ToInt32(SingleCreditNumber));
                    }
                    String CreditCommission = getCreditCommission(SingleCreditNumber, repId);
                    strOutput = strOutput + String.Format("{0:C2}", CreditCommission) + "|";
                    String CreditVolume = getCreditVolumeSplit(SingleCreditNumber);
                    Double CreditVolumeinFloat = Double.Parse(CreditVolume);
                    strOutput = strOutput + "-" + String.Format("{0:C2}", CreditVolumeinFloat) + "|";
                    String RepName = getRepNameByCredit(SingleCreditNumber);
                    strOutput = strOutput + RepName + "~";

                }
            }
            

            int Length = strOutput.Length;
            if (Length != 0)
            {
                strOutput = strOutput.Substring(0, (Length - 1));
            }

            Session["RunningSales"] = RunningSales;
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

        public string getNewAccountCount(string CompanyID, DateTime startDate)
        {
            String  FirstOrdedDateTime =String.Empty;
            String Response = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT TOP 1 CreatedDateTime FROM dbo.Orders WHERE CompanyID = " + CompanyID + " ORDER BY CreatedDateTime ASC";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            FirstOrdedDateTime =  sdr["CreatedDateTime"].ToString();
                        }

                    }
                }
            }

            /*Code Snippet*/
            if (!String.IsNullOrEmpty(FirstOrdedDateTime))
            {
                DateTime dtFirstOrderDateTime = Convert.ToDateTime(FirstOrdedDateTime);
                if (dtFirstOrderDateTime.Month == startDate.Month)
                {
                    Response = "YES";
                }
                else
                {
                    Response = "";
                }

            }
            /*End CodeSnippet*/

            return Response;
           
        }

        public string getRepNameByOrder(String OID)
        {
            String RepName = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CreatedBy FROM dbo.Orders WHERE OrderID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RepName = sdr["CreatedBy"].ToString();
                        }

                    }
                }
            }

            return RepName;
        }

        public String getRealAccountOwnerID(int OID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strQuery = "SELECT AccountOwnerID FROM dbo.Orders WHERE OrderID = " + OID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strQuery;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["AccountOwnerID"].ToString();
                    }
                }
            }
            conn.Close();
            return output;
        }


        public string getRepNameByCredit(String CID)
        {
            String RepName = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CreatedBy FROM dbo.CreditNotes WHERE CreditNote_ID = " + CID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            RepName = sdr["CreatedBy"].ToString();
                        }

                    }
                }
            }

            return RepName;
        }

        public string getCreditCommission(String CID, String UID)
        {
            String CreditCommission = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Value FROM dbo.Commision WHERE OrderID = " + CID + " AND UserLoginID = " + UID + " AND Type='CREDITNOTE'";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CreditCommission = sdr["Value"].ToString();
                        }

                    }
                }
            }

            return CreditCommission;

        }


        public string getCreditDate(String CID)
        {
            String CreditDate = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CONVERT(varchar(10), DateCreated, 103) AS Cdate FROM dbo.CreditNotes WHERE CreditNote_ID = " + CID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CreditDate = sdr["Cdate"].ToString();
                        }

                    }
                }
            }

            return CreditDate;
        }



        public String getCommission(String OID, String LoginID)
        {
            String Commission = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(Value) AS TCOM FROM dbo.Commision WHERE OrderID = " + OID + " AND UserLoginID = " + LoginID + " AND TYPE = 'ORDER'";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        
                        while (sdr.Read())
                        {
                            Commission = sdr["TCOM"].ToString();
                        }

                    }
                }
            }

            return Commission;
        }

        public string getOrderDate(String OID)
        {
            String OrderDate = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CONVERT(varchar(10), CreatedDateTime, 103) AS Cdate FROM dbo.Orders WHERE OrderID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                            while (sdr.Read())
                            {
                                OrderDate = sdr["Cdate"].ToString();
                            }

                    }
                }
            }

            return OrderDate;
        }

        public string getCreditVolume(String OID)
        {
            String CreditVolume = String.Empty;
            float FloatCreditVolume = 0;
            float FinalCreditVolume = 0;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SubTotal) AS Total FROM dbo.CreditNotes WHERE CreditNote_ID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                CreditVolume = sdr["Total"].ToString();
                                FinalCreditVolume = float.Parse(CreditVolume);
                            }
                        }
                        else
                        {
                            FinalCreditVolume = 0;
                        }
                        
                    }
                }
            }



            return FinalCreditVolume.ToString();
        }

        public string getCreditVolumeSplit(String OID)
        {
            String CreditVolume = String.Empty;
            float FloatCreditVolume = 0;
            float FinalCreditVolume = 0;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SUM(SplitVolumeAmount) AS Total FROM dbo.CreditNotes WHERE CreditNote_ID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                CreditVolume = sdr["Total"].ToString();
                                FinalCreditVolume = float.Parse(CreditVolume);
                            }
                        }
                        else
                        {
                            FinalCreditVolume = 0;
                        }

                    }
                }
            }



            return FinalCreditVolume.ToString();
        }

        public string getOrderVolume(String OID)
        {
            String OrderVolume = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT SubTotal FROM dbo.Orders WHERE OrderID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderVolume = sdr["SubTotal"].ToString();
                        }
                    }
                }
            }

            return OrderVolume;
        }

        public string getOrderVolumeSplit(String OID)
        {
            String OrderVolume = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT VolumeSplitAmount FROM dbo.Orders WHERE OrderID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderVolume = sdr["VolumeSplitAmount"].ToString();
                        }
                    }
                }
            }

            return OrderVolume;
        }

        public String getCompanyNameByCredit(String CID)
        {

            String companyName = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CompanyName FROM dbo.Companies WHERE CompanyID IN (SELECT CompID FROM dbo.CreditNotes WHERE CreditNote_ID = " + CID + ")";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            companyName = sdr["CompanyName"].ToString();
                        }
                    }
                }
            }

            return companyName;
        }

        public String getCompanyName(String OID)
        {

            String companyName = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CompanyName FROM dbo.Companies WHERE CompanyID IN (SELECT CompanyID FROM dbo.Orders WHERE orderID = " + OID + ")";
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            companyName = sdr["CompanyName"].ToString();
                        }
                    }
                }
            }

            return companyName;
        }

        public String getCompanyID(String OID)
        {
            String CompanyID = String.Empty;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CompanyID FROM dbo.Orders WHERE orderID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CompanyID = sdr["CompanyID"].ToString();
                        }
                    }
                }
            }

            return CompanyID;
        }

        public List<String> getOrderListStartAndEnd(String RepName, String startDate, String endDate)
        {

            List<String> OrderList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT OrderID FROM dbo.Orders WHERE CreatedDateTime between  "
                            + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" 
                            + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 0";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT OrderID FROM dbo.Orders 
                                   WHERE AccountOwner = '" + RepName + "' AND CreatedDateTime between  " 
                                                           + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" + 
                                                           " AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderList.Add(sdr["orderID"].ToString());
                        }
                    }
                }
            }

            return OrderList;
        }

        public List<String> getOrderList(String RepName, String SelMonth, String SelYear)
        {

            List<String> OrderList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = "SELECT OrderID FROM dbo.Orders WHERE MONTH(CreatedDateTime) = " + SelMonth + " AND YEAR(CreatedDateTime) = " + SelYear + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 0";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT OrderID FROM dbo.Orders WHERE AccountOwner = '" + RepName + "' AND MONTH(CreatedDateTime) = " + SelMonth + " AND YEAR(CreatedDateTime) = " + SelYear + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 0";
                    }
                    
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderList.Add(sdr["orderID"].ToString());
                        }
                    }
                }
            }

            return OrderList;
        }

        public List<String> getOrderListWithCSAccountOwnerStartEnd(String RepName, String startDate, String endDate)
        {

            List<String> OrderList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT OrderID FROM dbo.Orders WHERE CreatedDateTime between  "
                            + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" 
                        + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT OrderID FROM dbo.Orders WHERE AccountOwner = '" + RepName + "' AND CreatedDateTime between  "
                                                           + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" 
                            + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderList.Add(sdr["orderID"].ToString());
                        }
                    }
                }
            }

            return OrderList;
        }

        public List<String> getOrderListWithCSAccountOwner(String RepName, String SelMonth, String SelYear)
        {

            List<String> OrderList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = "SELECT OrderID FROM dbo.Orders WHERE MONTH(CreatedDateTime) = " + SelMonth + " AND YEAR(CreatedDateTime) = " + SelYear + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT OrderID FROM dbo.Orders WHERE AccountOwner = '" + RepName + "' AND MONTH(CreatedDateTime) = " + SelMonth + " AND YEAR(CreatedDateTime) = " + SelYear + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderList.Add(sdr["orderID"].ToString());
                        }
                    }
                }
            }

            return OrderList;
        }

        public List<String> getOrderListWithCommissionSplitStartEnd(String RepName, String startDate, String endDate)
        {

            List<String> OrderList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT OrderID FROM dbo.Orders WHERE  CreatedDateTime between  "
                            + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" 
                           + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT OrderID FROM dbo.Orders WHERE SplitWith = '" + RepName + "' AND CreatedDateTime between  "
                                                           + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderList.Add(sdr["orderID"].ToString());
                        }
                    }
                }
            }

            return OrderList;
        }

        public List<String> getOrderListWithCommissionSplit(String RepName, String SelMonth, String SelYear)
        {

            List<String> OrderList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = "SELECT OrderID FROM dbo.Orders WHERE MONTH(CreatedDateTime) = " + SelMonth + " AND YEAR(CreatedDateTime) = " + SelYear + " AND Status NOT IN ('CANCELLED', 'PENDING') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT OrderID FROM dbo.Orders WHERE SplitWith = '" + RepName + "' AND MONTH(CreatedDateTime) = " + SelMonth + " AND YEAR(CreatedDateTime) = " + SelYear + " AND Status NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderList.Add(sdr["orderID"].ToString());
                        }
                    }
                }
            }

            return OrderList;
        }

        public List<String> getCreditListStartEnd(String RepName, String startDate, String endDate)
        {
            List<String> CreditList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT CreditNote_ID FROM dbo.CreditNotes WHERE DateCreated between  "
                            + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"  
                            + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 0";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT CreditNote_ID FROM dbo.CreditNotes WHERE AccountOwner = '" + RepName + "' AND DateCreated between  "
                                                           + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" 
                            + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 0";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CreditList.Add(sdr["CreditNote_ID"].ToString());
                        }
                    }
                }
            }

            return CreditList;
        }
        
        public List<String> getCreditList(String RepName, String SelMonth, String SelYear)
        {
            List<String> CreditList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = "SELECT CreditNote_ID FROM dbo.CreditNotes WHERE MONTH(DateCreated) = " + SelMonth + " AND YEAR(DateCreated) = " + SelYear + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 0";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT CreditNote_ID FROM dbo.CreditNotes WHERE AccountOwner = '" + RepName + "' AND MONTH(DateCreated) = " + SelMonth + " AND YEAR(DateCreated) = " + SelYear + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 0";
                    }
                    
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CreditList.Add(sdr["CreditNote_ID"].ToString());
                        }
                    }
                }
            }

            return CreditList;
        }


        public List<String> getCreditListWithCSAOStartEnd(String RepName, String startDate, String endDate)
        {
            List<String> CreditList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT CreditNote_ID FROM dbo.CreditNotes WHERE DateCreated between  "
                            + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" 
                            + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT CreditNote_ID FROM dbo.CreditNotes WHERE AccountOwner = '" + RepName + "' AND DateCreated between  "
                                                           + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"
                            + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CreditList.Add(sdr["CreditNote_ID"].ToString());
                        }
                    }
                }
            }

            return CreditList;
        }

        public List<String> getCreditListWithCSAO(String RepName, String SelMonth, String SelYear)
        {
            List<String> CreditList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = "SELECT CreditNote_ID FROM dbo.CreditNotes WHERE MONTH(DateCreated) = " + SelMonth + " AND YEAR(DateCreated) = " + SelYear + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT CreditNote_ID FROM dbo.CreditNotes WHERE AccountOwner = '" + RepName + "' AND MONTH(DateCreated) = " + SelMonth + " AND YEAR(DateCreated) = " + SelYear + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CreditList.Add(sdr["CreditNote_ID"].ToString());
                        }
                    }
                }
            }

            return CreditList;
        }

        public List<String> getCreditListWithCSSWStartEnd(String RepName, String startDate, String endDate)
        {
            List<String> CreditList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = @"SELECT CreditNote_ID FROM dbo.CreditNotes WHERE DateCreated between  "
                            + "'" + startDate + "'" + " AND   " + "'" + endDate + "'"  
                            + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT CreditNote_ID FROM dbo.CreditNotes WHERE Salesperson = '" + RepName + "' AND DateCreated between  "
                                                           + "'" + startDate + "'" + " AND   " + "'" + endDate + "'" 
                            + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CreditList.Add(sdr["CreditNote_ID"].ToString());
                        }
                    }
                }
            }

            return CreditList;
        }

        public List<String> getCreditListWithCSSW(String RepName, String SelMonth, String SelYear)
        {
            List<String> CreditList = new List<String>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (RepName == "ALL")
                    {
                        cmd.CommandText = "SELECT CreditNote_ID FROM dbo.CreditNotes WHERE MONTH(DateCreated) = " + SelMonth + " AND YEAR(DateCreated) = " + SelYear + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }
                    else
                    {
                        cmd.CommandText = "SELECT CreditNote_ID FROM dbo.CreditNotes WHERE Salesperson = '" + RepName + "' AND MONTH(DateCreated) = " + SelMonth + " AND YEAR(DateCreated) = " + SelYear + " AND STATUS NOT IN ('CANCELLED') AND CommishSplit = 1";
                    }

                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            CreditList.Add(sdr["CreditNote_ID"].ToString());
                        }
                    }
                }
            }

            return CreditList;
        }
    }
}