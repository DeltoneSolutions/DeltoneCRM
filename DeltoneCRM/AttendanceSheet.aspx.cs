using DeltoneCRM_DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM
{
    public partial class AttendanceSheet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                UploadFile(sender, e);
            }

            if (!IsPostBack)
            {
                var resl=SetRepList();
                foreach(var itme in resl)
                    RepNameDropDownList.Items.Add(new ListItem(itme,itme));
               
            }

            //TestMe();
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }

        protected void btnUploadBack(object sender, EventArgs e)
        {
            Response.Redirect("Manage\\ManageCentral.aspx");
        }

        protected void UploadFile(object sender, EventArgs e)
        {

            HttpFileCollection fileCollection = Request.Files;

            for (int i = 0; i < fileCollection.Count; i++)
            {
                HttpPostedFile upload = fileCollection[i];
                var filDirctory = Server.MapPath("~/UploadsAttendance/");
                if (!Directory.Exists(filDirctory))
                {
                    Directory.CreateDirectory(filDirctory);
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(filDirctory);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }





                Session["filedata"] = "yes";
                string fileName = Path.GetFileName(upload.FileName);

                upload.SaveAs(filDirctory + fileName);

                ReadFileUploaded(filDirctory + fileName);
            }
        }

        private void ReadFileUploaded(string filePath)
        {



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

            var listRepNAme = SetRepList();
            StringBuilder sb = new StringBuilder();
            var list = new List<RepAtt>();
            foreach (DataRow row in dt.Rows)
            {
                var obj = new RepAtt();
                var repContent = row[8].ToString().Trim();
                if (!string.IsNullOrEmpty(repContent))
                {
                    if (listRepNAme.Contains(repContent))
                    {
                        if (row[2].ToString() == "System")
                        {
                            obj.RepName = repContent;
                            obj.TimeDay = row[1].ToString();
                            list.Add(obj);
                        }
                    }
                }
            }

            ReAssignFileData(list);
        }

        public string callFileData()
        {
            String strOutput = "{\"aaData\":[";

            strOutput = strOutput + "]}";
            return strOutput;
        }

        private List<string> SetRepList()
        {
            var listRep = new List<string>();
            //listRep.Add("AIDAN");
            //listRep.Add("BAILEY"); listRep.Add("BRENDAN"); listRep.Add("DIMITRI"); listRep.Add("EMMA");
            //listRep.Add("JOHN"); listRep.Add("KRIT"); listRep.Add("TARAS");
            //listRep.Add("CHRISTINE"); listRep.Add("ALEC"); listRep.Add("JOE");
            //listRep.Add("JOSIAH"); listRep.Add("LACHLAN"); listRep.Add("PHIL"); listRep.Add("FARAH"); listRep.Add("ACCOUNTS");
            //listRep.Add("TRENT"); listRep.Add("PARTI");
            //listRep.Add("SOPHIE");

            var connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            RepDayOffDAL dal = new RepDayOffDAL(connString);
            var listRepa = dal.GetAttendanceReps();

            foreach (var item in listRepa)
                listRep.Add(item.Name);

            return listRep;
        }

        public class RepAtt
        {
            public string RepName { get; set; }
            public string TimeDay { get; set; }
            public string TimeONly { get; set; }
            public string DayONly { get; set; }
            public string ValidDay { get; set; }
            public DateTime DayValidCom { get; set; }
            //  public DateTime 
            public string Duration { get; set; }
        }

        public class RepAttendance
        {
            public string RepName { get; set; }
            public List<RepAtt> RepDeta { get; set; }
            public int NumberOfHoursWorked { get; set; }
        }


        private List<string> GetDayNameList()
        {
            var dayList = new List<string>();
            dayList.Add("Mon");
            dayList.Add("Tue");
            dayList.Add("Wed");
            dayList.Add("Thu");
            dayList.Add("Fri");
            dayList.Add("Sat");
            dayList.Add("Sun");

            return dayList;

        }

        private List<string> GetYearNameList()
        {
            var monthList = new List<string>();
            monthList.Add("Jan");
            monthList.Add("Feb");
            monthList.Add("Mar");
            monthList.Add("Apr");
            monthList.Add("May");
            monthList.Add("Jun");
            monthList.Add("Jul");
            monthList.Add("Aug");
            monthList.Add("Sep");
            monthList.Add("Oct");
            monthList.Add("Nov");
            monthList.Add("Dec");

            return monthList;

        }

        private void ReAssignFileData(List<RepAtt> listRepData)
        {
            foreach (var item in listRepData)
            {
                item.DayONly = item.TimeDay.Split('-')[0].Trim();
                item.TimeONly = item.TimeDay.Split('-')[1].Trim();
                var comTime = item.DayONly.Split(new[] { ' ' }, 2)[1] + " " + item.TimeONly;
                item.DayValidCom = Convert.ToDateTime(comTime);
                item.ValidDay = item.DayONly.Split(new[] { ' ' }, 2)[1];
            }

            var groupReps = from res in listRepData
                            group res by new
                            {
                                res.RepName,
                                res.ValidDay

                            } into gcs
                            select new RepAttendance
                            {
                                RepName = gcs.Key.RepName,
                                RepDeta = gcs.ToList()
                            };

            var orderListrep = new List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance>();

            foreach (var item in groupReps)
            {
                var key = item.RepName;
                var repDisplayAtten = new DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance();
                repDisplayAtten.RepName = key;
                if (item.RepDeta != null)
                {
                    if (item.RepDeta.Count() > 0)
                        repDisplayAtten.StarDateTime = item.RepDeta[0].DayValidCom;
                    if (item.RepDeta.Count() > 1)
                        repDisplayAtten.EndDateTime = item.RepDeta[1].DayValidCom;

                }

                orderListrep.Add(repDisplayAtten);
            }

            CalculateTimeHours(orderListrep);

            Session["attendanceLog"] = orderListrep;

        }

        private void TestMe()
        {
            var listttt = TestFile();
            var nameDaylist = GetDayNameList();
            var yearNameList = GetYearNameList();

            var listStarDateList = new List<RepStartAndEnd>();
            var listEndDateList = new List<RepStartAndEnd>();

            foreach (var item in listttt)
            {
                item.DayONly = item.TimeDay.Split('-')[0].Trim();
                item.TimeONly = item.TimeDay.Split('-')[1].Trim();
                var comTime = item.DayONly.Split(new[] { ' ' }, 2)[1] + " " + item.TimeONly;
                item.DayValidCom = Convert.ToDateTime(comTime);
                item.ValidDay = item.DayONly.Split(new[] { ' ' }, 2)[1];
                // TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(startTime));

            }

            var listGroup = from res in listttt
                            group res by new
                            {
                                res.RepName,
                                res.ValidDay
                            } into gcs
                            select new RepAttendance()
                            {
                                RepName = gcs.Key.RepName,
                                RepDeta = gcs.ToList()

                            };

            var listDisplayRepAttendance = new List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance>();

            foreach (var item in listGroup)
            {
                var key = item.RepName;
                var repDisplayAtten = new DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance();
                repDisplayAtten.RepName = key;
                if (item.RepDeta != null)
                {
                    if (item.RepDeta.Count() > 0)
                    {
                        repDisplayAtten.StarDateTime = item.RepDeta[0].DayValidCom;
                    }
                    if (item.RepDeta.Count() > 1)
                    {
                        repDisplayAtten.EndDateTime = item.RepDeta[1].DayValidCom;
                    }
                }

                listDisplayRepAttendance.Add(repDisplayAtten);

            }

            CalculateTimeHours(listDisplayRepAttendance);

            //  Session["attendanceLog"] = listDisplayRepAttendance;

            //  var gropList = listttt.GroupBy(p => p.RepName).ToList();
            //  var listFilterAtt = new List<RepAttendance>();
            //foreach (var grItem in gropList)
            //{
            //    var key = grItem.Key;
            //   var  grItemSS = grItem.OrderBy(x => x.DayValidCom).ToList();

            //  // var counter = 0;
            //   //for (var i=0;i <  item in grItemSS)
            //   // {
            //   //     //var aaa=item.
            //   //     var firstEele = grItemSS[0];


            //   //     var objRe = new RepAttendance();
            //   //  //   objRe.RepName = key;


            //   // }
            //}
        }


        private void CalculateTimeHours(List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance> listreps)
        {
            foreach (var item in listreps)
            {
                if (item.StarDateTime > item.EndDateTime)
                {
                    var swap = item.StarDateTime;
                    item.StarDateTime = item.EndDateTime;
                    item.EndDateTime = swap;

                }

                if (item.StarDateTime == DateTime.MinValue)
                {
                    if (item.EndDateTime.ToString().Contains("AM"))
                    {
                        item.StarDateTime = item.EndDateTime;
                        var strDate = item.StarDateTime.Date.ToShortDateString() + " 05:00:00 PM";
                        item.EndDateTime = Convert.ToDateTime(strDate);
                        item.EndTimeFlag = "1";
                    }
                    else
                    {
                        var strDate = item.EndDateTime.Date.ToShortDateString() + " 08:30:00 AM";
                        if (item.RepName == "PARTI")
                            strDate = item.EndDateTime.Date.ToShortDateString() + " 09:00:00 AM";
                        item.StarDateTime = Convert.ToDateTime(strDate);
                        item.StartTimeFlag = "1";
                    }
                }
                if (item.EndDateTime == DateTime.MinValue)
                {
                    if (item.StarDateTime.ToString().Contains("PM"))
                    {
                        item.EndDateTime = item.StarDateTime;
                        var strDate = item.EndDateTime.Date.ToShortDateString() + " 08:30:00 AM";
                        if (item.RepName == "PARTI")
                            strDate = item.EndDateTime.Date.ToShortDateString() + " 09:00:00 AM";
                        item.StarDateTime = Convert.ToDateTime(strDate);
                        item.StartTimeFlag = "1";
                    }
                    else
                    {
                        var strDate = item.StarDateTime.Date.ToShortDateString() + " 05:00:00 PM";
                        item.EndDateTime = Convert.ToDateTime(strDate);
                        item.EndTimeFlag = "1";
                    }
                }

                item.NumberOfMinutesWorked = item.EndDateTime.Subtract(item.StarDateTime).TotalMinutes;
                if (item.RepName == "PARTI" || item.RepName == "SOPHIE")
                    item.BreakInMinutes = 30;
                else
                    item.BreakInMinutes = 60;
                item.NumberOfHoursWorked = item.EndDateTime.Subtract(item.StarDateTime).TotalHours;
                item.FinalNumberOfMinutesWorked = item.NumberOfMinutesWorked - item.BreakInMinutes;

                item.FinalNumberOfHoursWorked = item.FinalNumberOfMinutesWorked / 60;
            }
        }



        public class RepStartAndEnd
        {
            public DateTime AccessDateOne { get; set; }
            public DateTime AccessDateTwo { get; set; }
        }

        private List<RepAtt> TestFile()
        {
            var list = new List<RepAtt>();

            var obj = new RepAtt();
            obj.RepName = "PARTI";
            obj.TimeDay = "Wed 07 Feb 2018 - 05:01:32 PM";
            list.Add(obj);

            var obj2 = new RepAtt();
            obj2.RepName = "PARTI";
            obj2.TimeDay = "Wed 07 Feb 2018 - 09:05:52 AM";
            list.Add(obj2);

            var obj3 = new RepAtt();
            obj3.RepName = "PARTI";
            obj3.TimeDay = "Thu 08 Feb 2018 - 04:50:32 PM";
            list.Add(obj3);

            var obj4 = new RepAtt();
            obj4.RepName = "PARTI";
            obj4.TimeDay = "Thu 08 Feb 2018 - 09:10:52 AM";
            list.Add(obj4);

            var obj5 = new RepAtt();
            obj5.RepName = "PARTI";
            obj5.TimeDay = "Fri 10 Feb 2018 - 05:14:32 PM";
            list.Add(obj5);

            var obj6 = new RepAtt();
            obj6.RepName = "PARTI";
            obj6.TimeDay = "Fri 10 Feb 2018 - 08:50:52 AM";
            list.Add(obj6);



            var objd1 = new RepAtt();
            objd1.RepName = "DIMITRI";
            objd1.TimeDay = "Wed 07 Feb 2018 - 05:01:32 PM";
            list.Add(objd1);

            var objd2 = new RepAtt();
            objd2.RepName = "DIMITRI";
            objd2.TimeDay = "Wed 07 Feb 2018 - 08:30:52 AM";
            list.Add(objd2);

            var objd3 = new RepAtt();
            objd3.RepName = "DIMITRI";
            objd3.TimeDay = "Thu 08 Feb 2018 - 05:20:32 PM";
            list.Add(objd3);

            var objd4 = new RepAtt();
            objd4.RepName = "DIMITRI";
            objd4.TimeDay = "Thu 08 Feb 2018 - 09:08:52 AM";
            list.Add(objd4);

            return list;
        }
    }
}