using DeltoneCRM_DAL;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Xero.Api.Core;
using XeroApi;
namespace DeltoneCRM
{
    public partial class UploadExcelFile : System.Web.UI.Page
    {
        String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            // checkHypenString();

            //  DateTime senddate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 30, 0);

            // TestOrderEmail();
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            // var obj = new CallNRequest();
            // new CallNDataHandler(conn).GetCallNData(obj);

            // UpdateStateContact();
            //var aasss = Convert.ToDateTime("22/11/2017");
            //var sss = CalculateBusinessDaysFromInputDate(aasss, 3);
            //// TestEmail();

            //var str = DateTime.Now;
            //var end = DateTime.Now.AddDays(14);
            //List<DateTime> bankHolidays = new List<DateTime>();
            //var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //RepDayOffDAL repDayOffDal = new RepDayOffDAL(connString);
            //var monCon = 10;
            //var yerCon = 2018;
            //var listDayOffs = repDayOffDal.GetAusHoliday();
            //var repListOffs = repDayOffDal.GetRepDayOffByRepId(10);

            //List<DateTime> repDaysoffas = new List<DateTime>();
            //foreach (var item in repListOffs)
            //    repDaysoffas.Add(item.DayOff);
            ////listDayOffs = (from li in listDayOffs where li.Month == monCon && li.Year == yerCon select li).ToList();
            //foreach (var item in listDayOffs)
            //    bankHolidays.Add(item.HolidayDate);
           // var counn = BusinessDaysUntil(str, end, bankHolidays, repDaysoffas);

           

            SmtpHostName = smtpHostName;
            SmtpUserName = smtpUserName;
            SmtpPassword = smtpuserPassword;

            TestEmail();

        }

        public int BusinessDaysUntil(DateTime firstDay, DateTime lastDay, List<DateTime> bankHolidays, List<DateTime> dayOffs)
        {
            firstDay = firstDay.Date;
            lastDay = lastDay.Date;
            if (firstDay > lastDay)
                throw new ArgumentException("Incorrect last day " + lastDay);

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

        private void TestEmail()
        {

            var sendEmail = new EmailSender();
            var fromAddress = "info@deltonesolutions.com.au";
            var fromName = "Deltonesolutions";
            var toAddress = "paruthi001@gmail.com";
            var subject = "tsert";
            var body = "Test";
            try
            {
                var resultSend = sendEmail.SendEmail(fromAddress, fromName, toAddress, "", "", subject, body, true, null);
            }
            catch (Exception ex)
            {

            }
        }

        string smtpHostName = "smtp.office365.com";
        string smtpUserName = "info@deltonesolutions.com.au";
        String smtpuserPassword = "deltonerep1#";


        public string SmtpHostName { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }


             public bool SendEmail(string from, string from_name, string to,
            string cc, string bcc, string subject, string body, bool isHtml,
            System.Collections.Generic.List<Attachment> attachmentsList)
        {
            ILog _logger = LogManager.GetLogger(typeof(EmailSender));
            var fromInfo = "info@deltonesolutions.com.au";
            var fromAddress = new MailAddress(fromInfo, "DELTONE SOLUTIONS PTY LTD");
            const String fromPassword = "deltonerep1#";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);
            var smClient = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = netWorkCrdential
            };
            var contactName = "au";
            var toAddress = new MailAddress(to, contactName);

            MailMessage message = new MailMessage(fromAddress, toAddress);
            //if (!string.IsNullOrEmpty(from_name))
            //    message.From = new MailAddress(from, from_name);
            //else
            //    message.From = new MailAddress(from);

          

           // message.To.Add(new MailAddress(to));
            if (!string.IsNullOrEmpty(cc))
            {
                var splitEmailByComma = cc.Split(',');
                foreach (var item in splitEmailByComma)
                {
                    if (!string.IsNullOrEmpty(item))
                        message.CC.Add(item);
                }
            }

            if (!string.IsNullOrEmpty(bcc))
            {
                var splitEmailComma = bcc.Split(',');
                foreach (var item in splitEmailComma)
                {
                    if (!string.IsNullOrEmpty(item))
                        message.Bcc.Add(item);
                }
            }

            if (attachmentsList != null)
                if (attachmentsList.Count() > 0)
                    foreach (var item in attachmentsList)
                        message.Attachments.Add(item);

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            try
            {
                smClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(" Error Occurred Email Sender :" + ex);
                return false;
            }



        }
        





        public System.DateTime CalculateBusinessDaysFromInputDate
     (System.DateTime StartDate, int NumberOfBusinessDays)
        {
            //Knock the start date down one day if it is on a weekend.
            if (StartDate.DayOfWeek == DayOfWeek.Saturday |
                StartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                NumberOfBusinessDays -= 1;
            }

            int index = 0;

            for (index = 1; index <= NumberOfBusinessDays; index++)
            {
                switch (StartDate.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        StartDate = StartDate.AddDays(2);
                        break;
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        StartDate = StartDate.AddDays(1);
                        break;
                    case DayOfWeek.Saturday:
                        StartDate = StartDate.AddDays(3);
                        break;
                }
            }

            //check to see if the end date is on a weekend.
            //If so move it ahead to Monday.
            //You could also bump it back to the Friday before if you desired to. 
            //Just change the code to -2 and -1.
            if (StartDate.DayOfWeek == DayOfWeek.Saturday)
            {
                StartDate = StartDate.AddDays(2);
            }
            else if (StartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                StartDate = StartDate.AddDays(1);
            }

            return StartDate;
        }


        private void UpdateStateContact()
        {
            DataTable dt = new DataTable();
            var listObj = new List<StreetConatct>();
            //String strOutput = "{\"data\":[";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = @"select * from [dbo].[Contacts] where POSTAL_Region not in ('VIC','QLD','NSW','ACT','TAS','WA','NT','SA')";

                    cmd.Connection = conn;
                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    dt.Load(dr);

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow drow in dt.Rows)
                        {

                            var contactId = drow["ContactID"].ToString();
                            var region = drow["POSTAL_Region"].ToString();
                            var postal = drow["POSTAL_PostalCode"].ToString();

                            var obj = new StreetConatct();
                            obj.contactID = Convert.ToInt32(contactId);
                            obj.region = region;
                            obj.postal = postal;

                            listObj.Add(obj);

                        }

                    }


                    conn.Close();
                }

            }

            var resultList = SetRealValue(listObj);

            UpdateContactState(resultList);
        }

        private void UpdateContactState(List<StreetConatct> objList)
        {
            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            foreach (var item in objList)
            {
                if (!string.IsNullOrEmpty(item.newRegion))
                {

                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = CONNSTRING;
                    var strSqlContactStmt = @"UPDATE Contacts SET POSTAL_Region=@title,  AlteredDateTime=CURRENT_TIMESTAMP WHERE ContactID=@cId";


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandText = strSqlContactStmt;
                        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = item.newRegion;
                        cmd.Parameters.Add("@cId", SqlDbType.Int).Value = item.contactID;
                        cmd.CommandText = strSqlContactStmt;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
                if (!string.IsNullOrEmpty(item.newPostal))
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = CONNSTRING;
                    var strSqlContactStmt = @"UPDATE Contacts SET POSTAL_PostalCode=@title,  AlteredDateTime=CURRENT_TIMESTAMP WHERE ContactID=@cId";


                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        conn.Open();
                        cmd.CommandText = strSqlContactStmt;
                        cmd.Parameters.Add("@title", SqlDbType.VarChar).Value = item.newPostal;
                        cmd.Parameters.Add("@cId", SqlDbType.Int).Value = item.contactID;
                        cmd.CommandText = strSqlContactStmt;
                        cmd.ExecuteNonQuery();
                    }
                    conn.Close();
                }
            }
        }

        private List<StreetConatct> SetRealValue(List<StreetConatct> objList)
        {
            int post;
            foreach (var item in objList)
            {
                if (!string.IsNullOrEmpty(item.region))
                {
                    if (int.TryParse(item.region, out post))
                    {

                        item.newRegion = item.postal.Trim();
                        item.newPostal = item.region;
                    }

                    if (item.region.Trim() == "VICTORIA")
                    {
                        item.newRegion = "VIC";
                    }

                    if (item.region.Trim() == "NEW SOUTH WALES")
                    {
                        item.newRegion = "NSW";
                    }

                    if (item.region.Trim() == "WESTERN AUSTRALIA")
                    {
                        item.newRegion = "WA";
                    }

                    if (item.region.Trim() == "QUEENSLAND")
                    {
                        item.newRegion = "QLD";
                    }
                }
            }

            return objList;
        }

        protected class StreetConatct
        {
            public int contactID { get; set; }
            public string region { get; set; }
            public string postal { get; set; }
            public string newRegion { get; set; }
            public string newPostal { get; set; }
        }

        private void TestOrderEmail()
        {
            //var dateNow = DateTime.Now;
            //var sendateTimeHour = 12;
            //var sendateTimeMin = 30;
            //DateTime senddate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, sendateTimeHour, sendateTimeMin, 0);
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var obj = new OrderSendEmailDAL(conn);
            var listEmail = obj.GetOrderSendEmail(1);

            var dateNow = DateTime.Now;
            var sendateTimeHour = 14;
            var sendateTimeMin = 20;
            var minSendateHour = 12;


            if (dateNow.Hour < minSendateHour)
            {
                sendateTimeHour = minSendateHour;
            }

            else
                if (dateNow.Hour == minSendateHour)
                {
                    sendateTimeHour = dateNow.Hour;
                    sendateTimeMin = dateNow.Minute;
                }
                else if (dateNow.Hour > minSendateHour && dateNow.Hour < sendateTimeHour)
                {
                    sendateTimeHour = dateNow.Hour;
                    sendateTimeMin = dateNow.Minute;
                }
                else
                    if (dateNow.Hour > sendateTimeHour)
                    {
                        dateNow = dateNow.AddDays(1);
                        sendateTimeHour = minSendateHour;

                    }
                    else
                        if (dateNow.Hour == 14)
                        {
                            if (dateNow.Minute > sendateTimeMin)
                            {
                                dateNow = dateNow.AddDays(1);
                                sendateTimeHour = minSendateHour;
                            }
                            else
                            {
                                sendateTimeHour = dateNow.Hour;
                                sendateTimeMin = dateNow.Minute;
                            }

                        }
            DateTime senddate = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day, sendateTimeHour, sendateTimeMin, 0);

            //  obj.RunPurchaseOrderEmail(1);
            //foreach (var item in listEmail)
            //{
            //    var aaa = obj.GetSupplierEmails(item.SupplierName, 1);
            //   // var res = obj.SendEmailPurchaseOrder(item);

            //    obj.RunPurchaseOrderEmail(listEmail);
            //}

            //    var objddd = obj.GetOrderSendEmailById(26);

            //  var res = obj.SendEmailPurchaseOrder(objddd);


            //obj.RunPurchaseOrderEmailTTTT();


            //obj.RunPurchaseOrderEmail();

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {

            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload1.PostedFile.SaveAs(filePath);



            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;


            oda.Fill(dt);
            connExcel.Close();



            StringBuilder sb = new StringBuilder();
            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (DataRow row in dt.Rows)
            {
                var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                obj.SupplierItemCode = row[0].ToString().Trim();
                obj.PriceUpdate = row[2].ToString();
                obj.Description = row[1].ToString();
                obj.DSB = row[3].ToString();
                obj.PrinterCompatibility = row[9].ToString();
                obj.PrinterCompatibility = obj.PrinterCompatibility.Replace(",", " ");
                list.Add(obj);
            }
            var supplierId = 15;  // suppliuerId 15 for dynamic

            //new DeltoneCRM_DAL.ItemDAL(strConn).UpdateItemPrinterCompatibility(list, supplierId);


            // UpdateContactState()

            // var str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCode(list, supplierId);
            // var fileNamesav = "dynamic0811serv";
            //  WriteFile(str, supplierId, fileNamesav);

            //Bind Data to GridView

            //GridView1.DataSource = dt;
            //GridView1.DataBind();

            // ProcessExcel(filePath);
        }

        private void WriteFile(StringBuilder str, int supplierID, string fileNamep)
        {
            var datetimeticks = DateTime.Now.Ticks;
            var fileName = "filepro" + datetimeticks + supplierID + fileNamep + ".txt";
            string path = @"C:\temp\" + fileName;
            string text2write = str.ToString();

            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            writer.Write(text2write);
            writer.Close();
        }


        protected void UploadAusjetButton_Click(object sender, EventArgs e)
        {
            var supplierId = 3;  // suppliuerId 3 for AusJet
            //  ReadCsvFile();
            var list = ReadCsvFile();

            StringBuilder str = new StringBuilder();

            var processList = PrcesssCheck(list);
            var fileName = "ausJetCheck";
            //   WriteFile(str, supplierId, fileName);
            //   var strUp = "";

            str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForAusJet(processList, supplierId);
            var fileNameUp = "ausJetUpdate";
            //   WriteFile(str, supplierId, fileNameUp);

        }
        private void SetCode()
        {
            var list = ReadCsvFile();

            StringBuilder str = new StringBuilder();

            foreach (var item in list)
            {

                str = str.Append("Code From File :" + item.SupplierItemCode);


                var obj = new DeltoneCRM_DAL.ItemDAL(strConn).GetItemsBySupplierItemCode(item.SupplierItemCode);
                if (obj.Count() > 0)
                {
                    str.Append(" Code Found :" + obj[0].Description);
                    str.Append(Environment.NewLine);

                }
                else
                {
                    str.Append(" Code Not Found :");
                    str.Append(Environment.NewLine);
                }


            }

        }


        private List<DeltoneCRM_DAL.ItemDAL.SupplierItem> ReadCsvFile()
        {

            string fileName = Path.GetFileName(FileUpload2.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload2.PostedFile.SaveAs(filePath);
            var connString = string.Format(
                @"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties='text;HDR=Yes;FMT=Delimited(,)';",
                 Path.GetDirectoryName(filePath)
            );
            var ds = new DataSet("MyTable");
            using (var conn = new OleDbConnection(connString))
            {

                conn.Open();
                var query = "SELECT * FROM [" + fileName + "]";
                using (var adapter = new OleDbDataAdapter(query, conn))
                {

                    adapter.Fill(ds);
                }
            }
            var dt = ds.Tables[0];
            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (DataRow row in dt.Rows)
            {
                var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                obj.SupplierItemCode = row[0].ToString();
                obj.SupplierOEMCODE = row[0].ToString();
                obj.PriceUpdate = row[2].ToString();
                obj.Description = row[1].ToString();
                obj.EnterStart = row[3].ToString();
                obj.EnterEnd = row[4].ToString();
                list.Add(obj);
            }

            return list;


        }

        private List<DeltoneCRM_DAL.ItemDAL.SupplierItem> PrcesssCheck(List<DeltoneCRM_DAL.ItemDAL.SupplierItem> lst)
        {

            foreach (var item in lst)
            {
                //  item.Description = item.Description.Replace("Premium", "");
                item.Description = item.Description.Replace("Generic", string.Empty);
                // item.Description = Regex.Replace(item.Description, @"\s+", "");
                // item.Description=
                // item.Description = item.Description.Replace("Toner", "");
                //  item.Description = item.Description.Replace("for", "");
                item.Description = item.Description.Replace(",", string.Empty);

                string pattern = "\\s+";
                string replacement = " ";
                Regex rgx = new Regex(pattern);
                item.Description = rgx.Replace(item.Description, replacement);
                //  item.Description = item.Description.Replace("  ", string.Empty);
                var removeTextDesc = item.Description;
                removeTextDesc = removeTextDesc.Replace("Toner", string.Empty);
                removeTextDesc = removeTextDesc.Replace("for", string.Empty);
                removeTextDesc = removeTextDesc.Replace("Premium", string.Empty);
                item.SupplierOEMCODE = removeTextDesc.Trim();

                if (!string.IsNullOrWhiteSpace(item.EnterStart))
                    item.Description = item.EnterStart + " " + item.Description;

                if (!string.IsNullOrWhiteSpace(item.EnterEnd))
                    item.Description = item.Description + " " + item.EnterEnd;
            }

            return lst;
        }

        private List<DeltoneCRM_DAL.ItemDAL.SupplierItem> CreateAusJet()
        {
            string fileName = Path.GetFileName(FileUpload3.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload3.PostedFile.SaveAs(filePath);

            //var filePath = "";

            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx or xls

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;


            oda.Fill(dt);
            connExcel.Close();

            String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            StringBuilder sb = new StringBuilder();
            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (DataRow row in dt.Rows)
            {
                var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                obj.SupplierItemCode = row[0].ToString();
                obj.PriceUpdate = row[2].ToString();
                obj.Description = row[1].ToString();
                list.Add(obj);
            }

            return list;

            //   GetItemsBySupplierItemCode

        }


        private void checkHypenString()
        {
            var abc = "60-AK011-1P";
            var pos = abc.LastIndexOf('-');

            var lastString = abc.Substring(pos);
            if (lastString == "-1P")
            {
                var lastSubstring = abc.Substring(pos + 1);
            }

            char last_char = abc[abc.Length - 1];

            var sec = "60-AK043M";
            char last_charaa = sec[sec.Length - 1];

            // var descriptionSample=sec.repl
        }


        protected void UploadButtonXero_Click(object se, EventArgs e)
        {
            // createContactINXero();
            GetContactXero();
        }


        private void GetContactXero()
        {
            var xero = new XeroIntergration();
            var name = "Nick Bromley";
            Repository res = xero.CreateRepository();
            XeroCoreApi resss = xero.CreateAPI();
            var sss = "TWIN CITY MOTOR INN";
            XeroApi.Model.Contact deltoneContact = new XeroApi.Model.Contact();

            //  var contactss = resss.Contacts.Where(x=>x.);

            var ccc = (from cond in res.Contacts
                       where cond.Name.Contains(sss)
                       select cond).ToList();
            var list = new List<string>();

            list.Add("DELORAINE RADIO CABS");
            list.Add("MOWBRAY INDOOR SPORT N SKATE");
            list.Add("G & K CABINETS");
            list.Add("LITHGOW GLASS SERVICE");
            list.Add("HI-WAY MOTOR INN");
            list.Add("GALVIN ENGINEERING");
            list.Add("BIGA + LAURIETON");
            list.Add("MULLUM POOL PROPRIETARY LIMITED");
            list.Add("COMPANY REPORTING SERVICE");
            list.Add("MURGON MOTOR INN");
            list.Add("TONY EVANS ELECTRICAL");
            list.Add("VAUGHN WOOD VIDEO PRODUCTIONS");
            list.Add("TWIN CITY MOTOR INN");
            list.Add("SCOTTSDALE HOTEL-MOTEL");
            list.Add("GLYNDE RADIATORS");

            var xeroList = new List<XeroApi.Model.Contact>();


            foreach (var item in list)
            {
                var cccd = (from cond in res.Contacts
                            where cond.Name.Contains(item)
                            select cond).ToList();

                foreach (var y in cccd)
                {
                    xeroList.Add(y);
                }
            }


            var fileName = "accoutXeroGuid" + 1 + 1 + ".txt";
            string path = @"C:\temp\" + fileName;
            var sb = new StringBuilder();
            foreach (var item in xeroList)
            {

                sb.Append(String.Format("Name = {0}: Code = {1}  :", item.Name, item.ContactID));
                sb.Append(Environment.NewLine);


            }

            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            writer.Write(sb);
            writer.Close();
            //  var contact = xero.findContactByName(res, name);

            // var allCOntact=xero.con
        }

        private void createContactINXero()
        {
            String CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var contactdal = new ContactDAL(CONNSTRING);
            var xero = new XeroIntergration();

            var comId = 22036;
            var contactId = 18437;
            var companyname = "COMPANY REPORTING SERVICE";
            var conatc = contactdal.GetContactByContactId(contactId);

            string FirstName = conatc.FirstName;
            string LastName = conatc.LastName;
            string DefaultAreaCode = conatc.DEFAULT_AreaCode;
            string DefaultNumber = conatc.DEFAULT_Number;
            string MobileNumber = conatc.MOBILE_Number;
            string EmailAddy = conatc.Email;
            string ShipLine1 = conatc.STREET_AddressLine1;
            string ShipLine2 = conatc.STREET_AddressLine2;
            string ShipCity = conatc.STREET_City;
            string ShipState = conatc.STREET_Region;
            string ShipPostcode = conatc.STREET_PostalCode;
            string BillLine1 = conatc.POSTAL_AddressLine1;
            string BillLine2 = conatc.POSTAL_AddressLine2;
            string BillCity = conatc.POSTAL_City;
            string BillState = conatc.POSTAL_Region;
            string BillPostcode = conatc.POSTAL_PostalCode;
            // String PrimaryContact = Request.Form["PrimaryContact"].ToString();

            String strDefaultCountryCode = "+61"; //Should have entries in AddContactUI
            String strDefaultCountry = "Australia"; //Should have entries in AddContactUI


            Repository res = xero.CreateRepository();
            //  xero.u
            XeroApi.Model.Contact delContact = xero.CreateContact(res, companyname, FirstName, LastName, DefaultAreaCode,
                strDefaultCountryCode, DefaultNumber, String.Empty, String.Empty, String.Empty, String.Empty,
                strDefaultCountryCode, MobileNumber, EmailAddy, ShipLine1, ShipCity,
                strDefaultCountry, ShipPostcode, ShipState,
                BillLine1, BillCity, strDefaultCountry, BillPostcode, BillState);

            if (delContact != null)
            {
                //Update the ContactGuid in the DataBase Table 
                contactdal.UpdateWithXeroDetails(comId, delContact.ContactID.ToString());
            }
            else
            {
            }
        }


        protected void UploadNewClick(object sender, EventArgs e)
        {
            CreateTODPriceUpload();
        }

        private void CreateTODPriceUpload()
        {
            string fileName = Path.GetFileName(FileUpload4.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload4.PostedFile.SaveAs(filePath);

            //var filePath = "";

            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx or xls

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //string SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();

            //String[] excelSheets = new String[dtExcelSchema.Rows.Count];
            //int i = 0;

            //// Add the sheet name to the string array.
            //foreach (DataRow row in dtExcelSchema.Rows)
            //{
            //    excelSheets[i] = row["TABLE_NAME"].ToString();
            //    i++;
            //}

            //// Loop through all of the sheets if you want too...
            //for (int j = 0; j < excelSheets.Length; j++)
            //{
            //    // Query each excel sheet.
            //}
            connExcel.Close();

            var sheetName = "FULL LIST$";

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
            oda.SelectCommand = cmdExcel;


            oda.Fill(dt);
            connExcel.Close();

            //String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            //StringBuilder sb = new StringBuilder();
            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (DataRow row in dt.Rows)
            {

                if (!string.IsNullOrEmpty(row[0].ToString()) &&
                    !string.IsNullOrEmpty(row[1].ToString()) &&
                    !string.IsNullOrEmpty(row[2].ToString()) &&
                    !string.IsNullOrEmpty(row[3].ToString()) &&
                    !string.IsNullOrEmpty(row[4].ToString()) &&
                    !string.IsNullOrEmpty(row[6].ToString()))
                {
                    if (!list.Any(x => x.SupplierItemCode == row[1].ToString()))
                    {
                        var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                        obj.SupplierName = row[0].ToString();
                        obj.SupplierItemCode = row[1].ToString();
                        obj.SupplierOEMCODE = row[2].ToString();
                        obj.SupplierOEMCODE = obj.SupplierOEMCODE.Replace(",", "");
                        obj.Description = row[0].ToString() + " " + row[2].ToString() + " " + row[3].ToString();
                        obj.Description = obj.Description.Replace(",", " ");
                        obj.PrinterCompatibility = row[4].ToString();
                        obj.PrinterCompatibility = obj.PrinterCompatibility.Replace(",", " ");
                        obj.PriceUpdate = row[6].ToString();
                        list.Add(obj);
                    }

                }
            }

            var finalListProductPrice = CheckUNiProduct(list);



            var supplierId = 4;  // suppliuerId 4 for TOD

            var str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForTOD(finalListProductPrice, supplierId);
            var fileNamesav = "todPriceUploads";
            WriteFile(str, supplierId, fileNamesav);



        }

        private List<DeltoneCRM_DAL.ItemDAL.SupplierItem> CheckUNiProduct(List<DeltoneCRM_DAL.ItemDAL.SupplierItem> listItems)
        {
            var filteredList = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();

            var listUniPro = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            var listIDMAtchUNI = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (var item in listItems)
            {
                if (item.SupplierItemCode.EndsWith("UNI"))
                {
                    listUniPro.Add(item);
                }
            }

            foreach (var item in listUniPro)
            {
                var itemCode = item.SupplierItemCode.Replace("UNI", "");
                if (listItems.Any(x => x.SupplierItemCode == itemCode))
                {
                    listIDMAtchUNI.Add(item);
                    var selectItem = (from e in listItems where e.SupplierItemCode == itemCode select e).SingleOrDefault();
                    if (selectItem != null)
                        listItems.Remove(selectItem);
                }
            }

            foreach (var item in listItems)
            {
                if (!filteredList.Any(x => x.SupplierItemCode == item.SupplierItemCode))
                {
                    filteredList.Add(item);
                }
            }
            //foreach (var item in filteredList)
            //{
            //    var listItem = (from e in listUniPro where e.SupplierItemCode == item.SupplierItemCode select e).ToList();
            //    var desc = item.SupplierOEMCODE;
            //    var itemDescOri = item.Description;
            //    if (listItem.Count() > 0)
            //    {

            //        var temDesc = "";
            //        foreach (var yyime in listItem)
            //        {
            //            if (temDesc == "")
            //                temDesc = yyime.SupplierOEMCODE;
            //            else
            //                temDesc = temDesc + " " + yyime.SupplierOEMCODE + " ";
            //        }
            //        item.Description = desc + " " + temDesc + " " + itemDescOri;
            //    }
            //    else
            //        item.Description = desc + " " + itemDescOri;
            //}

            return filteredList;
        }



        protected void UploadNewMicroClick(object se, EventArgs e)
        {


            string fileName = Path.GetFileName(FileUpload5.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload5.PostedFile.SaveAs(filePath);

            //var filePath = "";

            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx or xls

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[1]["TABLE_NAME"].ToString();

            //String[] excelSheets = new String[dtExcelSchema.Rows.Count];
            //int i = 0;

            //// Add the sheet name to the string array.
            //foreach (DataRow row in dtExcelSchema.Rows)
            //{
            //    excelSheets[i] = row["TABLE_NAME"].ToString();
            //    i++;
            //}

            //// Loop through all of the sheets if you want too...
            //for (int j = 0; j < excelSheets.Length; j++)
            //{
            //    // Query each excel sheet.
            //}
            connExcel.Close();

            var listSheet = new List<string>();
            listSheet.Add("Inkjet Cartridges$");
            // listSheet.Add("Paper$");
            //listSheet.Add("Brother P-Touch  Tape$");
            //listSheet.Add("Thermal Fax Rolls$");
            // listSheet.Add("Toner Cartridges$");

            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (var item in listSheet)
            {
                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + item + "]";
                oda.SelectCommand = cmdExcel;


                oda.Fill(dt);
                connExcel.Close();

                //String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                //StringBuilder sb = new StringBuilder();
                //  dt.Rows.RemoveAt(0);
                //  dt.Rows.RemoveAt(1);
                // dt.Rows.RemoveAt(2);
                // dt.Rows.RemoveAt(3);
                // dt.Rows.RemoveAt(4);
                var listTitle = new List<string>();

                var count = 0;
                var titleString = "";
                foreach (DataRow row in dt.Rows)
                {
                    var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                    var rowValue = row[0].ToString();

                    if (!string.IsNullOrEmpty(rowValue) && string.IsNullOrEmpty(row[1].ToString()))
                    {
                        titleString = rowValue.Split(' ')[0] + " ";
                    }

                    // for Toner Cartridges$
                    //if (!string.IsNullOrEmpty(row[0].ToString()) &&
                    //    !string.IsNullOrEmpty(row[1].ToString()) &&
                    //    !string.IsNullOrEmpty(row[5].ToString()))
                    //{
                    //    obj.SupplierOEMCODE = row[1].ToString();
                    //    obj.PrinterCompatibility = "";
                    //    //   obj.SupplierName = row[0].ToString();
                    //    obj.SupplierItemCode = row[0].ToString();
                    //    obj.SupplierItemCode = obj.SupplierItemCode.Replace("*", "");
                    //    obj.SupplierItemCode = obj.SupplierItemCode.Trim();
                    //    // obj.SupplierOEMCODE = row[2].ToString(); 
                    //    obj.PrinterCompatibility = row[2].ToString();
                    //    obj.PrinterCompatibility = obj.PrinterCompatibility.Replace(",", "");
                    //    //// obj.SupplierOEMCODE = obj.SupplierItemCode.Replace(",", "");
                    //    // obj.SupplierOEMCODE = obj.SupplierItemCode.Trim();
                    //    obj.Description = titleString + row[1].ToString();
                    //    obj.Description = obj.Description.Replace(",", "");
                    //    obj.PriceUpdate = row[5].ToString();
                    //    list.Add(obj);

                    //}


                    // for Toner Cartridges$
                    if (!string.IsNullOrEmpty(row[0].ToString()) &&
                        !string.IsNullOrEmpty(row[1].ToString()) &&
                        !string.IsNullOrEmpty(row[4].ToString()))
                    {
                        obj.SupplierOEMCODE = row[1].ToString();
                        obj.PrinterCompatibility = "";
                        //   obj.SupplierName = row[0].ToString();
                        obj.SupplierItemCode = row[0].ToString();
                        obj.SupplierItemCode = obj.SupplierItemCode.Replace("*", "");
                        obj.SupplierItemCode = obj.SupplierItemCode.Trim();
                        // obj.SupplierOEMCODE = row[2].ToString(); 
                        obj.PrinterCompatibility = row[2].ToString();
                        obj.PrinterCompatibility = obj.PrinterCompatibility.Replace(",", "");
                        //// obj.SupplierOEMCODE = obj.SupplierItemCode.Replace(",", "");
                        // obj.SupplierOEMCODE = obj.SupplierItemCode.Trim();
                        obj.Description = titleString + row[1].ToString();
                        obj.Description = obj.Description.Replace(",", "");
                        obj.PriceUpdate = row[4].ToString();
                        list.Add(obj);

                    }

                }

            }
            var supplierId = 1;  //microjet  // suppliuerId 4 for TOD
            list.RemoveAt(0);
            var str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForTOD(list, supplierId);
            var fileNamesav = "micorjetfile";
            WriteFile(str, supplierId, fileNamesav);
        }




        protected void EmailSendFiles(object sender, EventArgs e)
        {

        }

        private void WriteAccounts()
        {
            var xero = new XeroIntergration();
            var apiXero = xero.CreateAPI();
            var accounts = apiXero.Accounts.Find().ToList();
            var fileName = "accoutXero" + 1 + 1 + ".txt";
            string path = @"C:\temp\" + fileName;
            var sb = new StringBuilder();
            foreach (var item in accounts)
            {

                sb.Append(String.Format("Name = {0}: Code = {1}  : ReportingCodeName = {2}: TaxType = {3}:", item.Name, item.Code, item.ReportingCodeName, item.TaxType));
                sb.Append(Environment.NewLine);


            }

            System.IO.StreamWriter writer = new System.IO.StreamWriter(path);
            writer.Write(sb);
            writer.Close();
        }

        protected void uploadPromo(object send, EventArgs e)
        {
            string fileName = Path.GetFileName(FileUpload6.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload6.PostedFile.SaveAs(filePath);



            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            var SheetName = "CODES NOT FOUND$";
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;

            //String[] excelSheets = new String[dtExcelSchema.Rows.Count];
            //int i = 0;

            //// Add the sheet name to the string array.
            //foreach (DataRow row in dtExcelSchema.Rows)
            //{
            //    excelSheets[i] = row["TABLE_NAME"].ToString();
            //    i++;
            //}

            //// Loop through all of the sheets if you want too...
            //for (int j = 0; j < excelSheets.Length; j++)
            //{
            //    // Query each excel sheet.
            //}


            oda.Fill(dt);
            connExcel.Close();



            StringBuilder sb = new StringBuilder();
            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (DataRow row in dt.Rows)
            {
                var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                obj.SupplierItemCode = "DS" + row[1].ToString().Trim();
                obj.PriceUpdate = row[2].ToString();
                obj.Description = row[0].ToString();
                obj.Quantity = row[3].ToString();

                obj.Description = obj.Description + " " + obj.SupplierItemCode;
                list.Add(obj);
            }
            var supplierId = 8;  // suppliuerId 15 for dynamic

            //var str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeInHouse(list, supplierId);
            // var fileNamesav = "dynamic0811serv";
            // WriteFile(str, supplierId, fileNamesav);
        }


        protected void uploadQimage(object sed, EventArgs e)
        {
            string fileName = Path.GetFileName(FileUpload7.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload7.PostedFile.SaveAs(filePath);



            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            var SheetName = "Q-Imaging Price List$";
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;




            oda.Fill(dt);
            connExcel.Close();



            StringBuilder sb = new StringBuilder();
            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row[0].ToString()) &&
                       !string.IsNullOrEmpty(row[2].ToString()) &&
                       !string.IsNullOrEmpty(row[4].ToString()))
                {
                    var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                    obj.SupplierItemCode = row[0].ToString().Trim();
                    obj.PriceUpdate = row[4].ToString();
                    obj.Description = row[2].ToString();
                    obj.SupplierOEMCODE = row[0].ToString().Trim();
                    obj.Description = obj.Description.Replace("Compatible", "");
                    obj.Description = obj.Description.Replace("Remanufactured", "");
                    // if (!list.Contains(obj))
                    list.Add(obj);
                }
            }
            var supplierId = 2;  // supplierId 2 for Qimage


            var listDataItems = new DeltoneCRM_DAL.ItemDAL(strConn).GetItemBYSupplier(supplierId);
            var noDataList = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();

            foreach (var item in list)
            {
                // if (!listDataItems.Any(x => x.SupplierItemCode == item.SupplierItemCode))
                var seleObj = (from sss in listDataItems where sss.SupplierItemCode == item.SupplierItemCode select sss).ToList();
                if (seleObj.Count() == 0)
                    noDataList.Add(item);
            }

            // var str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForAusJet(list, supplierId);
            //  var fileNamesav = "Qimage";
            //  WriteFile(str, supplierId, fileNamesav);

            var stringBuil = new StringBuilder();

            foreach (var item in noDataList)
            {

                sb.Append(String.Format("Not Found  Code = {0}: Price = {1} Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                sb.Append(Environment.NewLine);
            }

            var fileNamesav = "QimageNoFound";
            WriteFile(stringBuil, supplierId, fileNamesav);

        }



        //RTS Supplier

        protected void uploadRts(object sed, EventArgs e)
        {
            string fileName = Path.GetFileName(FileUpload8.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload8.PostedFile.SaveAs(filePath);



            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx or xls

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            var SheetName = "Price List$";
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;




            oda.Fill(dt);
            connExcel.Close();



            StringBuilder sb = new StringBuilder();
            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            foreach (DataRow row in dt.Rows)
            {
                if (!string.IsNullOrEmpty(row[0].ToString()) &&
                       !string.IsNullOrEmpty(row[1].ToString()) &&
                         !string.IsNullOrEmpty(row[2].ToString()) &&
                      !string.IsNullOrEmpty(row[4].ToString()) &&
                       !string.IsNullOrEmpty(row[5].ToString()))
                {
                    if (row[2].ToString() != "OEM Code")
                    {
                        var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();
                        obj.SupplierItemCode = row[4].ToString().Trim();
                        obj.PriceUpdate = row[5].ToString();
                        obj.Description = row[0].ToString() + " " + row[1].ToString() + " " + row[2].ToString();
                        obj.SupplierOEMCODE = row[1].ToString().Trim();

                        // if (!list.Contains(obj)) 
                        list.Add(obj);
                    }
                }
            }
            var supplierId = 27;  // supplierId 26 on local abut server 27




            //   var str = new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForRTS(list, supplierId);


            var fileNamesav = "rtsproducts";
            // WriteFile(str, supplierId, fileNamesav);

        }


        public void UploadCartidges(object se, EventArgs e)
        {
            string fileName = Path.GetFileName(FileUpload9.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload9.PostedFile.SaveAs(filePath);

            //var filePath = "";

            // you have to change column data , change the logic

            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx or xls

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            // var sheetName = "Toner$";
            //  var sheetName = "Inkjets$";
            var sheetName = "Drum$";

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
            oda.SelectCommand = cmdExcel;


            oda.Fill(dt);
            connExcel.Close();

            String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            StringBuilder sb = new StringBuilder();
            var list = new List<DeltoneCRM_DAL.ItemDAL.SupplierItem>();
            var title = "";

            foreach (DataRow row in dt.Rows)
            {
                var obj = new DeltoneCRM_DAL.ItemDAL.SupplierItem();


                if (row[1].ToString() == "Printer Model")
                {
                    title = row[0].ToString();

                }
                if (row[1].ToString() != "Printer Model")
                {

                    obj.SupplierItemCode = row[0].ToString().Trim().Split()[1];
                    if (title == "SAMSUNG")
                        obj.SupplierItemCode = row[0].ToString().Trim().Split()[0] + row[0].ToString().Trim().Split()[1];
                    if (title == "HP")
                        obj.SupplierItemCode = row[0].ToString().Trim().Split()[1];

                    obj.PriceUpdate = row[4].ToString();
                    if (title != "HP")
                        obj.Description = title + row[0].ToString();
                    else
                        obj.Description = row[0].ToString();
                    obj.Description = obj.Description.Replace(",", "");
                    obj.Description = obj.Description.Replace(";", "");
                    obj.PrinterCompatibility = row[1].ToString();
                    obj.PrinterCompatibility = obj.PrinterCompatibility.Replace(",", "");
                    obj.PrinterCompatibility = obj.PrinterCompatibility.Replace(";", "");
                    obj.SupplierOEMCODE = "";
                    if (!string.IsNullOrEmpty(obj.SupplierItemCode))
                        list.Add(obj);
                }


            }
            var supplierId = 29;
            new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForTOD(list, supplierId);

            // return list;
        }

        public void UploadWareHouse(object sender, EventArgs e)
        {
            string fileName = Path.GetFileName(FileUpload10.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload10.PostedFile.SaveAs(filePath);

            //var filePath = "";

            // you have to change column data , change the logic

            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx or xls

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            var sheetName = "Sheet1$";

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
            oda.SelectCommand = cmdExcel;


            oda.Fill(dt);
            connExcel.Close();

            String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            StringBuilder sb = new StringBuilder();

            var title = "";
            var list = new List<DisplayWShelfItemDetails>();
            foreach (DataRow row in dt.Rows)
            {
                DisplayWShelfItemDetails obj = new DisplayWShelfItemDetails();
                if (!string.IsNullOrEmpty(row[0].ToString()))
                {
                    obj.SupplierItemCode = row[0].ToString().Trim();
                    obj.Type = row[17].ToString().Trim();
                    obj.OEMCode = row[18].ToString().Trim();
                    obj.COG = row[6].ToString().Trim();
                    obj.Brand = row[11].ToString().Trim();
                    obj.Name = row[0].ToString().Trim().Replace("TW-", "");
                    obj.Quantity = row[3].ToString().Trim();
                    var dsb = Convert.ToDouble(row[6].ToString().Trim()) * 1.8;
                    obj.DSB = dsb.ToString();
                    var ressPric = Convert.ToDouble(row[6].ToString().Trim()) * 4;
                    obj.RepUnitPrice = ressPric.ToString();
                    obj.ManagerUnitPrice = ressPric.ToString();
                    obj.Description = obj.Brand + " " + obj.Type + " " + obj.Name;
                    obj.LocationId = 1;
                    obj.Boxing = "";
                    obj.Notes = "";
                    list.Add(obj);
                }
            }
            var supplierId = 29;
            //new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForTOD(list, supplierId);
            // foreach (var itme in list)
            // CreateShelfItems(itme);
            // return list;
        }


        public void UploadWareINHouse(object sender, EventArgs e)
        {
            string fileName = Path.GetFileName(FileUpload10.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload10.PostedFile.SaveAs(filePath);

            //var filePath = "";

            // you have to change column data , change the logic

            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx or xls

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            // string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            var sheetName = "Sheet2$";

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
            oda.SelectCommand = cmdExcel;


            oda.Fill(dt);
            connExcel.Close();

            String strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            StringBuilder sb = new StringBuilder();
            var shelSer = new WareShelfDAL(strConn);

            var title = "";
            var list = new List<DisplayWShelfItemDetails>();
            foreach (DataRow row in dt.Rows)
            {
                DisplayWShelfItemDetails obj = new DisplayWShelfItemDetails();
                if (!string.IsNullOrEmpty(row[0].ToString()))
                {

                    obj.SupplierItemCode = "DS" + row[0].ToString().Trim();
                    obj.SupplierId = 8;
                    obj.Type = row[3].ToString().Trim();
                    obj.OEMCode = "COMPATIBLE";
                    obj.COG = row[6].ToString().Trim();
                    obj.Brand = row[2].ToString().Trim();
                    obj.Name = row[4].ToString().Trim();
                    obj.Quantity = row[1].ToString().Trim();
                    //var dsb = Convert.ToDouble(row[6].ToString().Trim()) * 1.8;
                    obj.DSB = row[18].ToString().Trim();
                    var ressPric = row[7].ToString().Trim();
                    obj.RepUnitPrice = ressPric.ToString();
                    obj.ManagerUnitPrice = ressPric.ToString();
                    obj.Description = obj.Brand + " " + obj.Type + " " + obj.Name;
                    obj.LocationId = 1;
                    obj.Boxing = row[19].ToString().Trim();
                    obj.LocationColumnName = row[10].ToString().Trim();
                    obj.LocationRowNumber = row[11].ToString().Trim();

                    var locationID = 1;
                    var shelfRow = shelSer.ISRowAndColumnExist(obj.LocationColumnName, obj.LocationRowNumber);
                    if (shelfRow.Id > 0)
                        locationID = shelfRow.Id;

                    obj.LocationId = locationID;

                    if (!string.IsNullOrEmpty(row[19].ToString().Trim()))
                        obj.Notes = row[20].ToString().Trim();
                    if (obj.Boxing == "TRONWAY" || obj.Boxing == "RTS")
                    {
                        obj.SupplierItemCode = row[0].ToString().Trim();
                        obj.SupplierId = 30;
                    }
                    list.Add(obj);
                }
            }
            var supplierId = 29;
            //new DeltoneCRM_DAL.ItemDAL(strConn).UpdateOrInsertItemBySupplierCodeForTOD(list, supplierId);
            foreach (var itme in list)
            {
                if (!string.IsNullOrEmpty(itme.Quantity))
                {
                    // if (Convert.ToInt32(itme.Quantity) > 0)
                    //   CreateShelfItems(itme, itme.SupplierId);
                }
            }
            // return list;
        }


        public void CreateShelfItems(DisplayWShelfItemDetails obj, int supplierIdForInhouse)
        {
            if (obj != null)
            {
                var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                var itemDal = new ItemDAL(connString);
                var listItems = obj.DisplayWItemQuantity;
                WShelfItemDetailsDAL shelfDAl = new WShelfItemDetailsDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

                obj.CreatedUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());


                var itemHouse = shelfDAl.GetItemBySupplierIdAndItemCode(obj.SupplierItemCode, supplierIdForInhouse);
                if (string.IsNullOrEmpty(obj.Length))
                    obj.Length = "0";
                if (string.IsNullOrEmpty(obj.Weight))
                    obj.Weight = "0";
                if (string.IsNullOrEmpty(obj.Height))
                    obj.Height = "0";
                if (string.IsNullOrEmpty(obj.Width))
                    obj.Width = "0";

                if (itemHouse.ItemID > 0)
                {
                    var cog = float.Parse(obj.COG);
                    var managerPrice = float.Parse(obj.RepUnitPrice);
                    int qty = 0;
                    if (!string.IsNullOrEmpty(obj.Quantity))
                        qty = Convert.ToInt32(obj.Quantity);
                    double? dsb = null;
                    if (!string.IsNullOrEmpty(obj.DSB))
                        dsb = Convert.ToDouble(obj.DSB);
                    shelfDAl.UpdateItem(itemHouse.ItemID, obj.Description, cog, managerPrice, supplierIdForInhouse,
                        obj.SupplierItemCode, "Y", "Y", "N", qty, dsb, obj.OEMCode);
                    var itemIdINShelf = shelfDAl.CheckItemIdExistOrNotInShelf(itemHouse.ItemID);
                    obj.ItemId = itemHouse.ItemID;
                    if (itemIdINShelf > 0)
                    {

                        obj.Id = itemIdINShelf;
                        shelfDAl.UpdateWShelfItemDetailsAndWItemQuantity(obj);

                    }
                    else
                    {
                        shelfDAl.CreateWShelfItemDetailsAndWItemQuantity(obj);
                    }
                }
                else
                {
                    int qty = 0;
                    var cog = float.Parse(obj.COG);
                    var managerPrice = float.Parse(obj.RepUnitPrice);
                    if (!string.IsNullOrEmpty(obj.Quantity))
                        qty = Convert.ToInt32(obj.Quantity);
                    double? dsb = null;
                    if (!string.IsNullOrEmpty(obj.DSB))
                        dsb = Convert.ToDouble(obj.DSB);
                    shelfDAl.AddNewItem(supplierIdForInhouse.ToString(), obj.SupplierItemCode, obj.Description,
                        cog, managerPrice, "Y", "N", qty, dsb, obj.OEMCode);
                    var user = "SYSTEM";
                    var itemId = shelfDAl.GetLastInsertedItemId(obj.SupplierItemCode, supplierIdForInhouse, user);
                    //insert item and create itemshelf
                    obj.ItemId = itemId;
                    shelfDAl.CreateWShelfItemDetailsAndWItemQuantity(obj);

                }

                itemDal.UpdateItemActiveStatus("Y", obj.SupplierId, obj.SupplierItemCode);
            }
        }


        private void CreateCompanyAndOrderItem(List<CompanyUploadLead> listOrdercom)
        {

            var connectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var strCompany = "";
            var strWebSite = "";
            var AccountOwner = 38;
            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            DeltoneCRMDAL dal = new DeltoneCRMDAL();
            var contactdal = new ContactDAL(connectionString);
            var xero = new XeroIntergration();
            foreach (var item in listOrdercom)
            {
                strCompany = item.companyName;
                String Comp_Count = dal.CompanyNameCount(strCompany);
                if (Int32.Parse(Comp_Count) > 0)
                {

                }
                else
                {
                    var comId = dal.AddNewCompanyNotExists(strCompany, strWebSite, AccountOwner, loggedInUserId);

                    if (!string.IsNullOrEmpty(comId))
                    {

                        String FirstName = item.FirstName;
                        String LastName = item.LastName;
                        String DefaultAreaCode = item.PhoneAreaCode;
                        String DefaultNumber = item.PhoneDefault;
                        String MobileNumber = string.Empty;
                        String EmailAddy = string.Empty;
                        String ShipLine1 = item.AddressLine1;
                        String ShipLine2 = item.AddressLine2;
                        String ShipCity = item.City;
                        String ShipState = item.State;
                        String ShipPostcode = item.Postcode;
                        String BillLine1 = ShipLine1;
                        String BillLine2 = ShipLine2;
                        String BillCity = ShipCity;
                        String BillState = ShipState;
                        String BillPostcode = ShipPostcode;
                        String finalPrimaryContact = "Y";
                        var CompID = Convert.ToInt32(comId);

                        String str = dal.AddNewContactNotExists(CompID, FirstName, LastName, DefaultAreaCode, DefaultNumber, MobileNumber, EmailAddy, ShipLine1, ShipLine2,
                     ShipCity, ShipState, ShipPostcode, BillLine1, BillLine2, BillCity, BillState, BillPostcode, finalPrimaryContact, loggedInUserId);
                        if (!String.IsNullOrEmpty(str))
                        {
                            String[] arr = str.Split(':');
                            String contactID = arr[1].ToString();
                            String companyname = arr[2].ToString();
                            String companywebsite = arr[3].ToString();

                            var spilitWith = "NOSPLIT";
                            var spllitWithId = "0";
                            var TypeOfCall = "Reorder";
                            var PaymentTerms = "21";
                            var CommishSplit = false;
                            var duedate = DateTime.Now.AddDays(21);
                            var orderDate = DateTime.Now;
                            var status = "COMPLETED";
                            var strOrderedBy = "Dim Lead";
                            var accountId = "38";
                            var ProItems = "||";
                            var strCusDelItems = "|";
                            var SuppDelItems = "Ausjet,10,8,|";
                            var SupplierDelCost = 0;
                            var CusDelCost = 0;
                            float COGTotal = 50.39f;
                            var COGSubTotal = 45.81f;
                            var ProfitTotal = 0;
                            var ProfitSubtotal = 0;
                            var ProItemCost = 0;
                            var OrderNotesStr = "";
                            var Reference = "";
                            var Urgency = "Standard";
                            var VolumeSplitAmount = "0.00";
                            var SupplierAdj = "Ausjet,0,|";
                            var SuppNotes = "";

                          
                            var itemSuppName = "Ausjet";
                            var itemSuppNameId = "3";
                           
                            CreateOrder(comId, contactID, COGTotal, COGSubTotal, ProfitTotal, ProfitSubtotal, SupplierDelCost, CusDelCost,
                                ProItemCost, strOrderedBy, SuppDelItems, ProItems, strCusDelItems, status, duedate, strOrderedBy,
                                OrderNotesStr, PaymentTerms, Reference, TypeOfCall, orderDate, CommishSplit, spllitWithId, spilitWith, strOrderedBy, accountId, VolumeSplitAmount,
                                Urgency, SuppNotes, SupplierAdj);
                            int OrderID = LastOrderID(strOrderedBy);
                            foreach (var itmeList in item.OrderItemList)
                            {
                                var itemQty = 0;
                                if (!string.IsNullOrEmpty(itmeList.ItemQty))
                                    itemQty = Convert.ToInt32(itmeList.ItemQty);
                                var itemCode = itmeList.SuppCode;
                                var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                                var itemDal = new ItemDAL(conn);
                                var itemsBySuppCode = itemDal.GetItemsBySupplierItemCode(itemCode);
                                var ausItme = (from its in itemsBySuppCode where its.SupplierName == itemSuppNameId select its).ToList();
                                if (ausItme.Count() > 0)
                                {
                                    var itemDesc = ausItme[0].Description;
                                    CreateOrderItems(OrderID, itemDesc, ausItme[0].ItemId, float.Parse(ausItme[0].PriceUpdate), itemQty,
                                       float.Parse(ausItme[0].PriceUpdate), strOrderedBy, itemCode, itemSuppName);
                                }
                            }
                        }
                    }
                }
            }

        }

        protected int LastOrderID(String strCratedBy)
        {
            int intOrderID = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "select Top 1 * from dbo.OrdersNotExists where CreatedBy='" + strCratedBy + "'Order by  CreatedDateTime Desc";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        intOrderID = Int32.Parse(sdr["OrderID"].ToString());
                    }
                }

            }
            conn.Close();

            return intOrderID;
        }

        public void CreateTestOrder()
        {

            var spilitWith = "NOSPLIT";
            var spllitWithId = "0";
            var TypeOfCall = "Reorder";
            var PaymentTerms = "21";
            var CommishSplit = false;
            var duedate = DateTime.Now.AddDays(21);
            var orderDate = DateTime.Now;
            var status = "COMPLETED";
            var strOrderedBy = "Dimitri Jayaratne";
            var accountId = "1";
            var ProItems = "||";
            var strCusDelItems = "|";
            var SuppDelItems = "Ausjet,10,8,|";
            var SupplierDelCost = 0;
            var CusDelCost = 0;
            float COGTotal = 50.39f;
            var COGSubTotal = 45.81f;
            var ProfitTotal = 0;
            var ProfitSubtotal = 0;
            var ProItemCost = 0;
            var OrderNotesStr = "";
            var Reference = "";
            var Urgency = "Standard";
            var VolumeSplitAmount = "0.00";
            var SupplierAdj = "Ausjet,0,|";
            var SuppNotes = "";
            var comId = "22109";
            var contactID = "18454";
            var itemDesc = "LC-233 Magenta Compatible Inkjet Cartridge";
            var itemCode = "PB-233M";
            var itemSuppName = "Ausjet";
            var itemSuppNameId = "3";
            var itemQty = 2;
            CreateOrder(comId, contactID, COGTotal, COGSubTotal, ProfitTotal, ProfitSubtotal, SupplierDelCost, CusDelCost,
                ProItemCost, strOrderedBy, SuppDelItems, ProItems, strCusDelItems, status, duedate, strOrderedBy,
                OrderNotesStr, PaymentTerms, Reference, TypeOfCall, orderDate, CommishSplit, spllitWithId, spilitWith, strOrderedBy, accountId, VolumeSplitAmount,
                Urgency, SuppNotes, SupplierAdj);
            int OrderID = LastOrderID(strOrderedBy);
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var itemDal = new ItemDAL(conn);
            var itemsBySuppCode = itemDal.GetItemsBySupplierItemCode(itemCode);
            var ausItme = (from its in itemsBySuppCode where its.SupplierName == itemSuppNameId select its).ToList();
            if (ausItme.Count() > 0)
                CreateOrderItems(OrderID, itemDesc, ausItme[0].ItemId, float.Parse(ausItme[0].PriceUpdate), itemQty,
                   float.Parse(ausItme[0].PriceUpdate), strOrderedBy, itemCode, itemSuppName);
        }

        protected int CreateOrder(String strCompanyID, String strContactId, float COGTotal, float COGSubTotal,
           float ProfitTotal, float ProfitSubtotal, float SuppDelCost, float CusDelCost, float ProItemCost,
           String strCreatedBy, String SuppDelItems, String ProItems, String strCusDelItems, String status, DateTime duedate,
           String strOrderedBy, String OrderNotesStr, String PaymentTerms, String Reference, String TypeOfCall, DateTime orderDate,
           Boolean CommishSplit, String SplitWithID, String SplitWithName, String AccountOwner, String AccountOwnerID,
           String SplitVolume, String Urgency, String SuppNotes, string strSuppdelAdj)
        {
            int rowsEffected = -1;
            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //Modification done here add Order DueDate  and OrderedBy(ownership) 30/04/2015
            String strSqlStmt = @"insert into dbo.OrdersNotExists(CompanyID,ContactID,COGTotal,COGSubTotal,Total,SubTotal,
           SupplierDelCost,CustomerDelCost,ProItemCost,OrderedDateTime,CreatedDateTime,CreatedBy,SuppDeltems,ProItems,
              CusDelCostItems,Status,DueDate,OrderedBy, Notes, PaymentTerms, Reference, TypeOfCall, CommishSplit, SplitWith, 
                SplitWithID, OrderEnteredBy, OrderEnteredByID, AccountOwner, AccountOwnerID, VolumeSplitAmount, Urgency, SupplierNotes,SupplierAdj) values 
   (@CompanyID,@ContactID,@COGtotal,@CogSubtotal,@ProfitTotal,@ProfitSubTotal,@SuppDelCost,@CusDelCost,@ProItemCost,@OrderDate,CURRENT_TIMESTAMP,@CreatedBy,
@SuppDelItems,@Proitems,@CusDelItems,@Status,@duedate,@orderedby, @OrderNotes, @PaymentTerms, @Reference, @TypeOfCall, @CommishSplit, 
@SplitWith, @SplitWithID, @OrderEnteredBy, @OrderEnteredByID, @AccountOwner, @AccountOwnerID, @VolumeSplitAmount, @Urgency, @SupplierNotes,@SupplierAdj);";

            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@CompanyID", strCompanyID);
            cmd.Parameters.AddWithValue("@ContactID", strContactId);
            cmd.Parameters.AddWithValue("@COGtotal", COGTotal);
            cmd.Parameters.AddWithValue("@CogSubtotal", COGSubTotal);
            cmd.Parameters.AddWithValue("@ProfitTotal", ProfitTotal);
            cmd.Parameters.AddWithValue("@ProfitSubTotal", ProfitSubtotal);
            cmd.Parameters.AddWithValue("@SuppDelCost", SuppDelCost);
            cmd.Parameters.AddWithValue("@CusDelCost", CusDelCost);
            cmd.Parameters.AddWithValue("@ProItemCost", ProItemCost);
            cmd.Parameters.AddWithValue("@CreatedBy", strCreatedBy);
            //Modification done Add SuppDelCost and Promotional Item Cost
            cmd.Parameters.AddWithValue("@SuppDelItems", SuppDelItems);
            cmd.Parameters.AddWithValue("@Proitems", ProItems);
            cmd.Parameters.AddWithValue("@CusDelItems", strCusDelItems);
            cmd.Parameters.AddWithValue("@Status", status);
            //Modificaton done here Add Order due date pramater and Orderedby Parameter
            cmd.Parameters.AddWithValue("@duedate", duedate);
            cmd.Parameters.AddWithValue("@orderedby", strOrderedBy);
            cmd.Parameters.AddWithValue("@OrderNotes", OrderNotesStr);
            //Modification to record payment terms, reference number and type of call
            cmd.Parameters.AddWithValue("@PaymentTerms", PaymentTerms);
            cmd.Parameters.AddWithValue("@Reference", Reference);
            cmd.Parameters.AddWithValue("@TypeOfCall", TypeOfCall);
            //Modified here Sumudu Kodikar Add user Defined Date time
            //OrderDate
            cmd.Parameters.AddWithValue("@OrderDate", orderDate);
            //cmd.Parameters.AddWithValue("@ClientNotified", NotifyClient);
            cmd.Parameters.AddWithValue("@CommishSplit", CommishSplit);
            cmd.Parameters.AddWithValue("@SplitWith", SplitWithName);
            cmd.Parameters.AddWithValue("@SplitWithID", SplitWithID);
            cmd.Parameters.AddWithValue("@OrderEnteredBy", SplitWithName);
            cmd.Parameters.AddWithValue("@OrderEnteredByID", SplitWithID);

            cmd.Parameters.AddWithValue("@AccountOwner", AccountOwner);
            cmd.Parameters.AddWithValue("@AccountOwnerID", AccountOwnerID);
            cmd.Parameters.AddWithValue("@VolumeSplitAmount", SplitVolume);
            cmd.Parameters.AddWithValue("@Urgency", Urgency);
            cmd.Parameters.AddWithValue("@SupplierNotes", SuppNotes);
            cmd.Parameters.AddWithValue("@SupplierAdj", strSuppdelAdj);
            try
            {
                conn.Open();
                rowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }


            return rowsEffected;
        }

        //This Function Insert Order Items given by Values
        protected int CreateOrderItems(int OrderID, String strItemDesc, String strItemCode, float UnitAmout, int quantity,
            float COGAmount, String strCratedBy, String strSupplierItemCode, String strSuppName)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlOrderedItemStmt = "insert into dbo.Ordered_ItemsNotExists(OrderID,Description,ItemCode,UnitAmount,Quantity,CreatedDateTime,CreatedBy,COGamount,SupplierCode,SupplierName)values (@OrderID,@ItemDescription,@ItemCode,@UnitAmout,@qty,CURRENT_TIMESTAMP,@CreatedBy,@COGAmout,@SupplierCode,@SuppName);";
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@ItemDescription", strItemDesc);
            cmd.Parameters.AddWithValue("@ItemCode", strItemCode);
            cmd.Parameters.AddWithValue("@UnitAmout", UnitAmout);
            cmd.Parameters.AddWithValue("@qty", quantity);
            cmd.Parameters.AddWithValue("@CreatedBy", strCratedBy);
            cmd.Parameters.AddWithValue("@COGAmout", COGAmount);
            cmd.Parameters.AddWithValue("@SupplierCode", strSupplierItemCode);
            cmd.Parameters.AddWithValue("@SuppName", strSuppName);

            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();


            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
                /*If an Exception Occurres  Write the Exception details to the Database including FileName,Method,Detail Error and and LoggedUser if want ,Create a Table and Write to Table if fails Email*/

            }

            return rowEffected;
        }

        public void CreateUploadLead(object send, EventArgs a)
        {

           // CreateTestOrder();


            string fileName = Path.GetFileName(FileUpload11.PostedFile.FileName);
            var filePath = "C:/Temp/" + fileName;
            FileUpload11.PostedFile.SaveAs(filePath);



            string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=Excel 12.0;"; // xlsx

            OleDbConnection connExcel = new OleDbConnection(connStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;


            oda.Fill(dt);
            connExcel.Close();



            StringBuilder sb = new StringBuilder();
            var list = new List<CompanyUploadLead>();
            foreach (DataRow row in dt.Rows)
            {
                var comName = row[2].ToString().Trim();

                var ifComAdded = (from cs in list where cs.companyName == comName select cs).SingleOrDefault();
                if (ifComAdded == null)
                {
                    var obj = new CompanyUploadLead();
                    obj.FirstName = row[0].ToString().Trim();
                    obj.LastName = row[1].ToString().Trim();
                    obj.companyName = row[2].ToString().Trim();
                    obj.AddressLine1 = row[3].ToString();
                    obj.AddressLine2 = row[4].ToString();
                    obj.State = row[5].ToString();
                    obj.City = row[6].ToString();
                    obj.Postcode = row[7].ToString();
                    if (!string.IsNullOrEmpty(row[9].ToString()))
                    {
                        var phoneNum = row[9].ToString().Trim();
                        if (phoneNum.Contains(" "))
                        {
                            obj.PhoneAreaCode = phoneNum.Split(' ')[0];
                            obj.PhoneDefault = phoneNum.Replace(obj.PhoneAreaCode, string.Empty).Trim().ToString();
                        }
                        else
                        {
                            obj.PhoneAreaCode = "";
                            obj.PhoneDefault = row[9].ToString().Trim();
                        }
                    }
                    obj.OrderItemList = new List<OrderItemLeadUpload>();
                    var orderItem = new OrderItemLeadUpload();
                    orderItem.SuppCode = row[12].ToString().Trim();
                    orderItem.ItemQty = row[11].ToString().Trim();
                    orderItem.ItemDesc = row[16].ToString().Trim();
                    obj.OrderItemList.Add(orderItem);

                    list.Add(obj);
                }
                else
                {
                   // (from cs in list where cs.companyName == comName select cs).SingleOrDefault();

                    foreach (var itme in list)
                    {
                        if (itme.companyName == comName)
                        {
                            var orderItem = new OrderItemLeadUpload();
                            orderItem.SuppCode = row[12].ToString().Trim();
                            orderItem.ItemQty = row[11].ToString().Trim();
                            orderItem.ItemDesc = row[14].ToString().Trim();
                            itme.OrderItemList.Add(orderItem);
                        }
                    }
                   
                   
                }

            }

            var finalist = list;


            CreateCompanyAndOrderItem(finalist);
        }


        public class CompanyUploadLead
        {
            public int userid { get; set; }
            public string companyName { get; set; }
            public string strWebSite { get; set; }
            public int accountowner { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Postcode { get; set; }
            public string PhoneAreaCode { get; set; }
            public string PhoneDefault { get; set; }
            public IList<OrderItemLeadUpload> OrderItemList { get; set; }

        }

        public class OrderItemLeadUpload
        {
            public string ItemQty { get; set; }
            public string SuppCode { get; set; }
            public string Brand { get; set; }
            public string ItemDesc { get; set; }
        }
    }
}