using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Fetch
{
    public partial class FetchAttendanceFileData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var rep = Request["rep"];
            string repName = "";
            string startTime = "";
            string changeStartTimeValue = "";
            if (Request["repNameAlter"] != null)
                repName = Request["repNameAlter"];
            if (Request["repNameAlterStartTime"] != null)
                startTime = Request["repNameAlterStartTime"];
            if (Request["startTimeValue"] != null)
                changeStartTimeValue = Request["startTimeValue"];

            string endTime = "";
            string changeEndTime = "";

            if (Request["repNameAlterEndTime"] != null)
                endTime = Request["repNameAlterEndTime"];
            if (Request["endTimeValue"] != null)
                changeEndTime = Request["endTimeValue"];

            string lunchTime = "";
            string changeLunchTimeValue = "";

            if (Request["repNameAlterLunchTime"] != null)
                lunchTime = Request["repNameAlterLunchTime"];
            if (Request["lunchTimeValue"] != null)
                changeLunchTimeValue = Request["lunchTimeValue"];

            Response.Write(ReturnAttendanceFileData(rep, repName, startTime,
                changeStartTimeValue, endTime, changeEndTime, lunchTime, changeLunchTimeValue));
        }

        private List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance> ResetStartTime(List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance> listLogs,
            string changeRep, string changestartTime, string startTimeVal)
        {
            foreach (var item in listLogs)
            {
                if (item.RepName == changeRep && item.StarDateTime == Convert.ToDateTime(changestartTime))
                {
                    var newStartDate = item.StarDateTime.ToShortDateString() + " " + startTimeVal;
                    item.StarDateTime = Convert.ToDateTime(newStartDate);
                    item.NumberOfMinutesWorked = item.EndDateTime.Subtract(item.StarDateTime).TotalMinutes;
                   
                    item.NumberOfHoursWorked = item.EndDateTime.Subtract(item.StarDateTime).TotalHours;
                    item.FinalNumberOfMinutesWorked = item.NumberOfMinutesWorked - item.BreakInMinutes;

                    item.FinalNumberOfHoursWorked = item.FinalNumberOfMinutesWorked / 60;
                    item.RepStartTimeChanged = "1";
                }
            }

            return listLogs;
        }

        private List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance> ResetEndStartTime(List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance> listLogs,
           string changeRep,  string changeendTime, string endTimeVal
           )
        {
            foreach (var item in listLogs)
            {
                if (item.RepName == changeRep && item.StarDateTime == Convert.ToDateTime(changeendTime))
                {
                    var newEndDate = item.EndDateTime.ToShortDateString() + " " + endTimeVal;
                    item.EndDateTime = Convert.ToDateTime(newEndDate);
                    item.NumberOfMinutesWorked = item.EndDateTime.Subtract(item.StarDateTime).TotalMinutes;
                    //if (item.RepName == "PARTI" || item.RepName == "SOPHIE")
                    //    item.BreakInMinutes = 30;
                    //else
                    //    item.BreakInMinutes = 60;
                    item.NumberOfHoursWorked = item.EndDateTime.Subtract(item.StarDateTime).TotalHours;
                    item.FinalNumberOfMinutesWorked = item.NumberOfMinutesWorked - item.BreakInMinutes;

                    item.FinalNumberOfHoursWorked = item.FinalNumberOfMinutesWorked / 60;
                    item.RepEndTimeChanged = "1";
                }
            }

            return listLogs;
        }

        private List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance> ResetLunchTime(List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance> listLogs,
           string changeRep, 
           string changelunchTime, string lunchTimeVal)
        {
            foreach (var item in listLogs)
            {
                if (item.RepName == changeRep && item.StarDateTime == Convert.ToDateTime(changelunchTime))
                {
                    double lunchTime = 0;
                    if (!string.IsNullOrEmpty(lunchTimeVal))
                        lunchTime = Convert.ToDouble(lunchTimeVal);
                    item.BreakInMinutes = lunchTime;
                    item.NumberOfHoursWorked = item.EndDateTime.Subtract(item.StarDateTime).TotalHours;
                    item.FinalNumberOfMinutesWorked = item.NumberOfMinutesWorked - item.BreakInMinutes;

                    item.FinalNumberOfHoursWorked = item.FinalNumberOfMinutesWorked / 60;
                    item.RepLunchTimeChanged = "1";
                }
            }

            return listLogs;
        }



        private string ReturnAttendanceFileData(string repName, string changeRep,

            string changestartTime, string startTimeValue, string changeendTime, string endTimeVal,
            string changelunchTime, string lunchTimeVal)
        {
            var strOutput = "{\"aaData\":[";

            if (Session["attendanceLog"] != null)
            {
                var listattendanceList = Session["attendanceLog"] as List<DeltoneCRM_DAL.LoginDAL.DisplayRepAttendance>;
                listattendanceList = listattendanceList.OrderBy(x => x.StarDateTime).ToList();
                if (repName != "0")
                {
                    listattendanceList = (from li in listattendanceList where li.RepName == repName select li).ToList();

                }

                if (!string.IsNullOrEmpty(changeRep)
                    && !string.IsNullOrEmpty(changestartTime)
                     && !string.IsNullOrEmpty(startTimeValue))
                {
                    listattendanceList = ResetStartTime(listattendanceList, changeRep, changestartTime, startTimeValue);
                }



                if (!string.IsNullOrEmpty(changeRep) && !string.IsNullOrEmpty(changeendTime)
                     && !string.IsNullOrEmpty(endTimeVal))
                {
                    listattendanceList = ResetEndStartTime(listattendanceList, changeRep, 
                        changeendTime, endTimeVal);
                }

                if (!string.IsNullOrEmpty(changeRep) && !string.IsNullOrEmpty(changelunchTime)
                     && !string.IsNullOrEmpty(lunchTimeVal))
                {
                    listattendanceList = ResetLunchTime(listattendanceList, changeRep, changelunchTime, lunchTimeVal);
                }
               

                foreach (var item in listattendanceList)
                {


                    String strEditStartTime = "<img src='Images/Edit.png'  onclick='EditStartTime(" + string.Format("\\\"{0}\\\"", item.RepName) 
                        + ',' + string.Format("\\\"{0}\\\"", item.StarDateTime)  + ")'/>";
                    String strEditEndTime = "<img src='Images/Edit.png'  onclick='EditEndTime(" + string.Format("\\\"{0}\\\"", item.RepName)
                        + ',' + string.Format("\\\"{0}\\\"", item.StarDateTime) + ")'/>";
                    String strEditBreakTime = "<img src='Images/Edit.png'  onclick='EditBreakTime(" + string.Format("\\\"{0}\\\"", item.RepName)
                        + ',' + string.Format("\\\"{0}\\\"", item.StarDateTime) + ")'/>";

                    strOutput = strOutput + "[\"" + item.RepName + "\","
             + "\"" + item.StarDateTime + "\","
             + "\"" + item.EndDateTime + "\","
              + "\"" + item.StarDateTime.ToShortTimeString() + "\","
              + "\"" + item.EndDateTime.ToShortTimeString() + "\","
              + "\"" + String.Format("{0:0.00}", item.NumberOfHoursWorked) + "\","
             + "\"" + item.BreakInMinutes + "\","
              + "\"" + String.Format("{0:0.00}", item.FinalNumberOfHoursWorked) + "\","
               + "\"" + item.StartTimeFlag + "\","
               + "\"" + item.EndTimeFlag + "\","
                + "\"" + item.RepStartTimeChanged + "\","
                 + "\"" + item.RepEndTimeChanged + "\","
                  + "\"" + item.RepLunchTimeChanged + "\","
               + "\"" + strEditStartTime + "\","
                + "\"" + strEditEndTime + "\","
               + "\"" + strEditBreakTime + "\"],";
                }


                if (listattendanceList.Count() > 0)
                {
                    int Length = strOutput.Length;
                    strOutput = strOutput.Substring(0, (Length - 1));
                }
            }

            strOutput = strOutput + "]}";
            return strOutput;
        }
    }
}