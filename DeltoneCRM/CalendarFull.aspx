<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteInternalNavLevel1.Master" Title="Events" EnableEventValidation="false" CodeBehind="CalendarFull.aspx.cs" Inherits="DeltoneCRM.CalendarFull" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Calendar Events</title>
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.1.0/fullcalendar.min.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/qtip2/3.0.3/jquery.qtip.min.css" rel="stylesheet" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="Scripts/jscolor.js"></script>
    <style type='text/css'>
        /* css for timepicker */
        .ui-timepicker-div dl {
            text-align: left;
        }

            .ui-timepicker-div dl dt {
                height: 25px;
            }

            .ui-timepicker-div dl dd {
                margin: -25px 0 10px 65px;
            }

        .style1 {
            width: 100%;
        }

        /* table fields alignment*/
        .alignRight {
            text-align: right;
            padding-right: 10px;
            padding-bottom: 10px;
        }

        .alignLeft {
            text-align: left;
            padding-bottom: 10px;
        }

        body {
            margin: 40px 10px;
            padding: 0;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
            font-size: 22px;
        }

        #calendar {
            max-width: 1000px;
            margin: 0 auto;
        }

        .fc-event {
            font-size: 1.4em !important;
        }

        .fc button {
            font-size: 1.4em !important;
        }

        .different-tip-color {
            background-color: #CAED9E;
            border-color: #90D93F;
            color: #3F6219;
        }

        .colorBlack {
            color: black;
        }

        th.ui-datepicker-week-end,
        td.ui-datepicker-week-end {
            display: none;
        }

        .searchDiv {
            max-width: 70%;
            margin: 0 auto;
            width: 70%;
            min-height: 100px;
            text-align: center;
            font-weight: bold;
        }

        .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 40px;
            cursor: pointer;
        }

            .buttonClass:hover {
                border: solid 1px Black;
                background-color: #ffffff;
            }
            .fc-agendaWeek-view tr {
    height: 32px;
}
            .fc-day-number{
                font-size:medium;
                font-weight:bolder;
            }

.fc-agendaDay-view tr {
    height: 32px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
   <br />
    <div class="searchDiv">
    
      <input type="text"  style="width:500px;height:24px;" name="searchme" id="searchme" />

    <input type="button" value="Search" class="buttonClass"  style="margin-left:20px;width:180px;height:32px;color:blue;display:none;" onclick="checkFilter();" />
         <input type="button" value="Reset" class="buttonClass" style="margin-left:20px;width:180px;height:32px;color:blue" onclick="resetPage();" />
        </div>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="calendar">
    </div>
   
    <div id="updatedialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;width" 
        title="Update or Delete Event">
        <table class="style1">
            <tr id="printsectionrowTr">
                 <td class="alignRight" ></td>
                <td class="alignRight">
                    <input type="button" id="printeventUp" style="width: 50px; height: 30px; border: 1.5px solid blue;background-color: white;cursor: pointer;" value="PRINT" onclick="PrintElem();" />
                </td>
                
            </tr>
            <tr id="alinktr" style="display:none;">
                <td class="alignRight" > </td>
                <td class="alignRight" >
                    
                    <a id="urlLink" style="color:red">View</a></td>

            </tr>
            <tr>
                <td class="alignRight">Subject:</td>
                <td class="alignLeft">
                    <input id="eventName" type="text" size="60" /><br />
                </td>
            </tr>
            <tr id="contacttr" style="display:none;">

               <td class="alignRight">First and Last names:</td>
                <td class="alignLeft">
                    <span id="contactspan"> </span><br />
                </td>
            </tr>
            <tr id="telphonetr" style="display:none;">
                   <td class="alignRight">Telephone and Mobile and Email:</td>
                <td class="alignLeft">
                    <span id="telephonespan"> </span><br />
                </td>
               
            </tr>
            <tr id="addresstr" style="display:none;">
                   <td class="alignRight">Address:</td>
               <td class="alignLeft">
                    <span id="addressspan"> </span><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">Description:</td>
                <td class="alignLeft">
                    <textarea id="eventDesc" cols="60" rows="16"></textarea>
                    <span id="divDesc" style="display:none;"> 
                      <div id="displaynotesHistoryDiv" runat="server" style=" border:1px solid gray; font: medium -moz-fixed; font: -webkit-small-control;
    height: 120px;
    overflow: auto;
    padding: 2px;
    resize: both;
    width: 450px;word-wrap:break-word;display:inline-block;"></div></span>
                </td>
            </tr>
            <tr>
                <td class="alignRight">Background Color:</td>
                <td class="alignLeft">

                    <%--<input value="ffcc00" type="text" id="upbgcolor" class="jscolor {width:243, height:150, position:'right',
    borderColor:'#FFF', insetColor:'#FFF', backgroundColor:'#666'}" />--%>

                    <select id="upbgcolor" style="width:300px;height:30px;">
                        <option value="#169C9C" style="background-color:#169C9C;color:white;font-size:16px" selected="selected">   Select  </option>
 <option value="#FF0000" style="background-color:#FF0000;color:white;font-size:16px">   Customer Service  </option>
 <option value="#0000FF" style="background-color:#0000FF;color:white;font-size:16px">  Reorder         </option>
 <option value="#00FF00" style="background-color:#00FF00;color:white;font-size:16px">   New Quote          </option>
 <option value="#800080" style="background-color:#800080;color:white;font-size:16px">    Call Back     </option> 
                       <%-- <option value="#008000" style="background-color:#008000"> Quote Follow Up </option>--%>

                        <option value="#808080" style="background-color:#808080;color:white;font-size:16px"> Other   </option>
                        <option value="#EE7600" style="background-color:#EE7600;color:white;font-size:16px">  Referral     </option>
                        <option value="#FFFF00" style="background-color:#FFFF00;font-size:16px" >  Auth Call Back    </option>
                        <option value="#FA8072" style="background-color:#FA8072;color:white;font-size:16px">  Sale Follow Up     </option>
                        <option value="#4D9916" style="background-color:#4D9916;color:white;font-size:16px">  Leads     </option>
</select>

                </td>
            </tr>
              <tr >
                  <td class="alignRight"> Notify Me: </td>
                  <td class="alignLeft">
                   <%--   <input type="button" id="changebuttondate" style="color:red;" value="Change Start and End dates" onclick="showStartEndContent();"/>
                        <input type="button" id="hidebuttondate" value="Hide Start and End dates" onclick="hideStartEndContent();" style="display:none;color:red;"/>--%>
                      <input type="checkbox" id="isReminderCheckUpdate" title="Reminder"  />
                 
                  </td>
             </tr>
            <tr id="displaystarttr">
                <td class="alignRight">Start:</td>
                <td class="alignLeft">
                    <span id="eventStart" ></span></td>
            </tr>
            <tr id="displayendtr">
                <td class="alignRight">End: </td>
                <td class="alignLeft">
                    <span id="eventEnd" ></span>
                    <input type="hidden" id="eventId" /></td>
            </tr>
             <tr id="changedatestartendDateButton">
                  <td class="alignRight"> Change Start and End dates </td>
                  <td class="alignLeft">
                   <%--   <input type="button" id="changebuttondate" style="color:red;" value="Change Start and End dates" onclick="showStartEndContent();"/>
                        <input type="button" id="hidebuttondate" value="Hide Start and End dates" onclick="hideStartEndContent();" style="display:none;color:red;"/>--%>
                      <input type="checkbox" id="ischangedate" title="Change Start and End dates" onclick="validateInputDates();" />
                 
                  </td>
             </tr>
             <tr id="changestartDate" style="display:none;">
                <td class="alignRight">Start: </td>
                <td class="alignLeft">
                    <input id="changeEventStartDate" type="text" size="24"/>
                     <input type="button" value="Day" class="ui-button ui-corner-all ui-widget" onclick="addADay('1');"/>
                     <input type="button" value="Week" class="ui-button ui-corner-all ui-widget" onclick="addAWeek('1');"/>
                     <input type="button" value="Month" class="ui-button ui-corner-all ui-widget" onclick="addAMonth('1');"/>

                </td>
                
                   
            </tr>
        
            
             <tr id="changeendDate" style="display:none;">
                <td class="alignRight">End: </td>
                <td class="alignLeft">
                    <input id="changeEventEndDate" type="text" size="24"/>

                     <input type="button" value="Day" class="ui-button ui-corner-all ui-widget" onclick="addADay('2');"/>
                     <input type="button" value="Week" class="ui-button ui-corner-all ui-widget" onclick="addAWeek('2');"/>
                     <input type="button" value="Month" class="ui-button ui-corner-all ui-widget" onclick="addAMonth('2');"/>
                </td>
                   
            </tr>
             <tr id="createdDatertr" style="display:none;">
                <td class="alignRight">Created Date:</td>
                <td class="alignLeft">
                    <span id="createdDate"></span></td>
            </tr>
             <tr id="modifiedDatertr" style="display:none;">
                <td class="alignRight">Modified Date:</td>
                <td class="alignLeft">
                    <span id="modifiedDate"></span></td>
            </tr>
            
        </table>
    </div>
    <div id="addDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Add Event">
        <table class="style1">
            
            <tr>
                <td class="alignRight">Subject:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" size="60" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="60" rows="16"></textarea></td>
            </tr>
            <tr>
                <td class="alignRight">Background Color:</td>
                <td class="alignLeft">

                   <%-- <input value="ffcc00" type="text" id="addbgcolor" class="jscolor {width:243, height:150, position:'right',
    borderColor:'#FFF', insetColor:'#FFF', backgroundColor:'#666'}" />--%>

                       <select id="addbgcolor" style="width:300px;height:30px;">
 <option value="#169C9C" style="background-color:#169C9C;color:white;font-size:16px" selected="selected">   Select  </option>
 <option value="#FF0000" style="background-color:#FF0000;color:white;font-size:16px">   Customer Service  </option>
 <option value="#0000FF" style="background-color:#0000FF;color:white;font-size:16px">  Reorder         </option>
 <option value="#00FF00" style="background-color:#00FF00;color:white;font-size:16px">   New Quote          </option>
 <option value="#800080" style="background-color:#800080;color:white;font-size:16px">    Call Back     </option> 
                       <%-- <option value="#008000" style="background-color:#008000"> Quote Follow Up </option>--%>

                        <option value="#808080" style="background-color:#808080;color:white;font-size:16px"> Other   </option>
                         <option value="#EE7600" style="background-color:#EE7600;color:white;font-size:16px">  Referral     </option>
                        <option value="#FFFF00" style="background-color:#FFFF00;font-size:16px" >  Auth Call Back   </option>
                        <option value="#FA8072" style="background-color:#FA8072;color:white;font-size:16px">  Sale Follow Up     </option>
                           <option value="#4D9916" style="background-color:#4D9916;color:white;font-size:16px">  Leads     </option>
                        
</select>
                </td>
            </tr>

             <tr >
                  <td class="alignRight"> Notify Me :</td>
                  <td class="alignLeft">
                   <%--   <input type="button" id="changebuttondate" style="color:red;" value="Change Start and End dates" onclick="showStartEndContent();"/>
                        <input type="button" id="hidebuttondate" value="Hide Start and End dates" onclick="hideStartEndContent();" style="display:none;color:red;"/>--%>
                      <input type="checkbox" id="isReminderCheckAdd" title="Reminder"  />
                 
                  </td>
             </tr>
            <tr id="newdisplaystarttr">
                <td class="alignRight">Start:</td>
                <td class="alignLeft">
                    <span id="addEventStartDate"></span></td>
            </tr>
            <tr id="newdisplayendtr">
                <td class="alignRight">End:</td>
                <td class="alignLeft">
                    <span id="addEventEndDate"></span></td>
            </tr>

                       <tr id="newchangedatestartendDateButton">
                  <td class="alignRight"> Change Start and End dates </td>
                  <td class="alignLeft">
                   <%--   <input type="button" id="changebuttondate" style="color:red;" value="Change Start and End dates" onclick="showStartEndContent();"/>
                        <input type="button" id="hidebuttondate" value="Hide Start and End dates" onclick="hideStartEndContent();" style="display:none;color:red;"/>--%>
                      <input type="checkbox" id="newischangedate" title="Change Start and End dates" onclick="addNewvalidateInputDates();" />
                 
                  </td>
             </tr>
             <tr id="newchangestartDate" style="display:none;">
                <td class="alignRight">Start: </td>
                <td class="alignLeft">
                    <input id="newchangeEventStartDate" type="text" size="24"/>
                     <input type="button" value="Day" class="ui-button ui-corner-all ui-widget" onclick="addNewADay('1');"/>
                     <input type="button" value="Week" class="ui-button ui-corner-all ui-widget" onclick="addNewAWeek('1');"/>
                     <input type="button" value="Month" class="ui-button ui-corner-all ui-widget" onclick="addNewAMonth('1');"/>

                </td>
                
                   
            </tr>
        
            
             <tr id="newchangeendDate" style="display:none;">
                <td class="alignRight">End: </td>
                <td class="alignLeft">
                    <input id="newchangeEventEndDate" type="text" size="24"/>

                     <input type="button" value="Day" class="ui-button ui-corner-all ui-widget" onclick="addNewADay('2');"/>
                     <input type="button" value="Week" class="ui-button ui-corner-all ui-widget" onclick="addNewAWeek('2');"/>
                     <input type="button" value="Month" class="ui-button ui-corner-all ui-widget" onclick="addNewAMonth('2');"/>
                </td>
                   
            </tr>
           
        </table>

    </div>
    <div runat="server" id="jsonDiv" />
    <input type="hidden" id="hdClient" runat="server" enableeventvalidation="false" />


    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.17.1/moment.min.js"></script>

    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/qtip2/3.0.3/jquery.qtip.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/fullcalendar/3.1.0/fullcalendar.min.js"></script>
    <script src="Scripts/calendarscript.js" type="text/javascript"></script>

       <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />

    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-sliderAccess.js"></script>

    <script type="text/javascript">
        var canCall = "";
        canCall = '<%= clickeddateEvent %>';

        var dateNow = new Date();

        function addADay(ty) {


            if (ty == "1") {
                var aDayDate = $("#changeEventStartDate").datetimepicker('getDate');;
               
                aDayDate.setDate(aDayDate.getDate() + 1);
                validateDateWeekEnd(aDayDate);
                $("#changeEventStartDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aDayDate);
            }

            else {
                var aeDayDate = $("#changeEventEndDate").datetimepicker('getDate');
                aeDayDate.setDate(aeDayDate.getDate() + 1);
                validateDateWeekEnd(aeDayDate);
                $("#changeEventEndDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aeDayDate);
            }


        }

        function addAWeek(ty) {

            var aWeekDate = $("#changeEventStartDate").datetimepicker('getDate');;
           
            if (ty == "1") {

                aWeekDate.setDate(aWeekDate.getDate() + 7);
                validateDateWeekEnd(aWeekDate);
                $("#changeEventStartDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aWeekDate);
            }

            else {

                var aeDayDate = $("#changeEventEndDate").datetimepicker('getDate');

                aeDayDate.setDate(aeDayDate.getDate() + 7);
                validateDateWeekEnd(aeDayDate);
                $("#changeEventEndDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aeDayDate);
            }

        }

        function addAMonth(ty) {

            var aMonthDate = $("#changeEventStartDate").datetimepicker('getDate');;

            if (ty == "1") {

                aMonthDate.setDate(aMonthDate.getDate() + 30);
                validateDateWeekEnd(aMonthDate);
                $("#changeEventStartDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aMonthDate);
            }

            else {
                var aeDayDate = $("#changeEventEndDate").datetimepicker('getDate');
                aeDayDate.setDate(aeDayDate.getDate() + 30);
                validateDateWeekEnd(aeDayDate);
                $("#changeEventEndDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aeDayDate);
            }

        }


        function addNewADay(ty) {


            if (ty == "1") {
                var aDayDate = $("#newchangeEventStartDate").datetimepicker('getDate');;

                aDayDate.setDate(aDayDate.getDate() + 1);
                validateDateWeekEnd(aDayDate);
                $("#newchangeEventStartDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aDayDate);
            }

            else {
                var aeDayDate = $("#newchangeEventEndDate").datetimepicker('getDate');
                aeDayDate.setDate(aeDayDate.getDate() + 1);
                validateDateWeekEnd(aeDayDate);
                $("#newchangeEventEndDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aeDayDate);
            }


        }

        function addNewAWeek(ty) {

            var aWeekDate = $("#newchangeEventStartDate").datetimepicker('getDate');;

            if (ty == "1") {

                aWeekDate.setDate(aWeekDate.getDate() + 7);
                validateDateWeekEnd(aWeekDate);
                $("#newchangeEventStartDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aWeekDate);
            }

            else {

                var aeDayDate = $("#newchangeEventEndDate").datetimepicker('getDate');

                aeDayDate.setDate(aeDayDate.getDate() + 7);
                validateDateWeekEnd(aeDayDate);
                $("#newchangeEventEndDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aeDayDate);
            }

        }

        function PrintElem() {
            var mywindow = window.open('', 'PRINT', 'height=900,width=1200');
            var elem = "updatedialog";
           // document.getElementById("printsectionrowTr").style = "display:none";
            var textTitle = document.getElementById("eventName").value;
            document.getElementById("eventName").placeholder = textTitle;
            var textDesc = document.getElementById("eventDesc").value;
            document.getElementById("eventDesc").placeholder = textDesc;


            mywindow.document.write('<html><head><title>' + document.title + '</title>');
            mywindow.document.write('</head><body >');
           // mywindow.document.write('<h1>' + document.title + '</h1>');
            mywindow.document.write(document.getElementById(elem).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();
           // document.getElementById("printeventUp").style = "display:block";
            return true;
        }

        function addNewAMonth(ty) {

            var aMonthDate = $("#newchangeEventStartDate").datetimepicker('getDate');;

            if (ty == "1") {

                aMonthDate.setDate(aMonthDate.getDate() + 30);
                validateDateWeekEnd(aMonthDate);
                $("#newchangeEventStartDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aMonthDate);
            }

            else {
                var aeDayDate = $("#newchangeEventEndDate").datetimepicker('getDate');
                aeDayDate.setDate(aeDayDate.getDate() + 30);
                validateDateWeekEnd(aeDayDate);
                $("#newchangeEventEndDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', aeDayDate);
            }

        }




        function validateDateWeekEnd(selecDate) {
            var daySe = selecDate.getDay();
          
            if(daySe==0 || daySe==6)
            {
                alert("Your selected date is weekend.Please change");
            }
        }

        function showStartEndContent() {

            $('#changestartDate').show();
            $('#changeendDate').show();
            $('#hidebuttondate').show();
            $('#changebuttondate').hide();

        }

        function hideStartEndContent() {
            $('#changebuttondate').show();
            $('#changestartDate').hide();
            $('#changeendDate').hide();
            $('#hidebuttondate').hide();

        }

        function validateInputDates() {
            if ($("#ischangedate").is(':checked')) {
                showStartEndContent();
                $('#displaystarttr').hide();
                $('#displayendtr').hide();

            }
            else {
                hideStartEndContent();
                $('#displaystarttr').show();
                $('#displayendtr').show();
            }

        }

        function addNewshowStartEndContent() {

            $('#newchangestartDate').show();
            $('#newchangeendDate').show();
            $('#newhidebuttondate').show();
            $('#newchangebuttondate').hide();

        }

        function addNewhideStartEndContent() {
            $('#newchangebuttondate').show();
            $('#newchangestartDate').hide();
            $('#newchangeendDate').hide();
            $('#newhidebuttondate').hide();

        }

        function addNewvalidateInputDates() {
            if ($("#newischangedate").is(':checked')) {
                addNewshowStartEndContent();
                $('#newdisplaystarttr').hide();
                $('#newdisplayendtr').hide();

            }
            else {
                addNewhideStartEndContent();
                $('#newdisplaystarttr').show();
                $('#newdisplayendtr').show();
            }
            
        }

        $(document).ready(function () {


            $('#changeEventStartDate').blur(function () {

              
                    var startDateAndEndDate = $("#changeEventStartDate").datetimepicker('getDate');
                    startDateAndEndDate.setDate(startDateAndEndDate.getDate());
                   // validateDateWeekEnd(startDateAndEndDate);
                    $("#changeEventEndDate").datepicker({
                        dateFormat: 'dd/mm/yy'
                    }).datepicker('setDate', startDateAndEndDate);
                
            });

            validateInputDates();

            // $("#changeEventStartDate").datepicker({ beforeShowDay: $.datepicker.noWeekends });
            addNewvalidateInputDates();

            var hourset = dateNow.getHours();
            $('#changeEventStartDate').datetimepicker({
                timeInput: true,
                controlType: 'select',
                timeFormat: 'hh:mm tt',
                stepMinute: 15,
                hour: hourset,
                minute: 30,
                dateFormat: 'dd/mm/yy',
                hourMin: 8,
                hourMax: 17,
            });
            var monthAdd = dateNow.getMonth() + 1;
            var setDateForm = dateNow.getDate() + "/" + monthAdd + "/" + dateNow.getFullYear();

            $("#changeEventStartDate").datepicker({
                dateFormat: 'dd/mm/yy'
            }).datepicker('setDate', dateNow);


            // $("#changeEventStartDate").datepicker("setDate", dateNow);
            //  $("#changeEventEndDate").datepicker({ beforeShowDay: $.datepicker.noWeekends });


            $('#changeEventEndDate').datetimepicker({
                timeInput: true,
                controlType: 'select',
                timeFormat: 'hh:mm tt',
                stepMinute: 15,
                hour: hourset,
                minute: 30,
                dateFormat: 'dd/mm/yy',
                hourMin: 8,
                hourMax: 17,
                beforeShowDay: false
            });

            //$("#changeEventEndDate").datepicker({ beforeShowDay: $.datepicker.noWeekends });

            $("#changeEventEndDate").datepicker({
                dateFormat: 'dd/mm/yy'
            }).datepicker('setDate', dateNow);


            $('#newchangeEventStartDate').datetimepicker({
                timeInput: true,
                controlType: 'select',
                timeFormat: 'hh:mm tt',
                stepMinute: 15,
                hour: hourset,
                minute: 30,
                dateFormat: 'dd/mm/yy',
                hourMin: 8,
                hourMax: 17,
            });
            var monthAdd = dateNow.getMonth() + 1;
            var setDateForm = dateNow.getDate() + "/" + monthAdd + "/" + dateNow.getFullYear();

            $("#newchangeEventStartDate").datepicker({
                dateFormat: 'dd/mm/yy'
            }).datepicker('setDate', dateNow);


            // $("#changeEventStartDate").datepicker("setDate", dateNow);
            //  $("#changeEventEndDate").datepicker({ beforeShowDay: $.datepicker.noWeekends });


            $('#newchangeEventEndDate').datetimepicker({
                timeInput: true,
                controlType: 'select',
                timeFormat: 'hh:mm tt',
                stepMinute: 15,
                hour: hourset,
                minute: 30,
                dateFormat: 'dd/mm/yy',
                hourMin: 8,
                hourMax: 17,
                beforeShowDay: false
            });

            //$("#changeEventEndDate").datepicker({ beforeShowDay: $.datepicker.noWeekends });

            $("#newchangeEventEndDate").datepicker({
                dateFormat: 'dd/mm/yy'
            }).datepicker('setDate', dateNow);




            if (canCall) {
                //  $('#calendar').fullCalendar('changeView', 'agendaDay');
                //alert(canCall);
                var eventsdata = canCall.split('#rr#');

                $('#calendar').fullCalendar('gotoDate', eventsdata[10]);
                // $('#calendar').fullCalendar('gotoDate', canCall);
                var selectedEvent = new eventSelected();


                selectedEvent.id = parseInt(eventsdata[0]);
                selectedEvent.title = eventsdata[1];
                selectedEvent.description = eventsdata[2];
                selectedEvent.start = eventsdata[3];
                selectedEvent.end = eventsdata[4];
                selectedEvent.allDay = eventsdata[5];
                selectedEvent.color = eventsdata[6];
                selectedEvent.url = eventsdata[7];
                selectedEvent.createdDate = eventsdata[8];
                selectedEvent.modifiedDate = eventsdata[9];
                selectedEvent.companyId = eventsdata[11];
                selectedEvent.contactbuilder = eventsdata[12];
                selectedEvent.telephonebuilder = eventsdata[13];
                selectedEvent.addressbuilder = eventsdata[14];
                updateEvent(selectedEvent, "");

            }

            autoCompleteSearchText();

        });

        function eventSelected() {
            this.id = 0;
            this.companyId = 0;
            this.title = "";
            this.description = "";
            this.start = "";
            this.end = "";
            this.allDay = false;
            this.color = "";
            this.url = "";
            this.createdDate = "";
            this.modifiedDate = "";
            this.contactbuilder = "";
            this.telephonebuilder = "";
            this.addressbuilder = "";
        }


        function checkFilter() {


            //var events = $('#calendar').fullCalendar('clientEvents');

            //var events=   $('#calendar').fullCalendar('clientEvents', function (event) {
            //       if (event.title == sear) return true;
            //       else return false;
            //   });
            //console.log(events);
            //   //SubmitEmail();
            //$("#calendar").fullCalendar('addEventSource', events);

            //  var moment = $('#calendar').fullCalendar('getDate').format();
            // alert(moment);
            recallTest();

        }
        function getCalendarEvents(filter) {

            var events = new Array();
            if (filter == null) {
                events = $('#calendar').fullCalendar('clientEvents');
            }
            else {
                events = getEventsByFilter(filter);
            }
            return events;
        }

        function getEventsByFilter(filter) {
            var allevents = new Array();
            var filterevents = new Array();
            allevents = getCalendarEvents(null);

            for (var j in allevents) {
                if (allevents[j].title == filter) {
                    filterevents.push(allevents[j]);
                }
            }

            return filterevents;
        }

        function resetPage() {
            location.reload();
        }


        function autoCompleteSearchText() {

            $('#searchme').autocomplete({

                source: "Fetch/FetchUserCalendarEvents.aspx",
                select: function (event, ui) {

                   recallTest();
                }

            });
        }


        function recallTest() {
            var sear = $("#searchme").val();
            console.log(sear);
            var dateToBeSet = "";
            var uLink = "DataHandlers/DisplayEventHandler.ashx?qqq=" + sear;
            $('#calendar').fullCalendar('destroy');
            $('#calendar').fullCalendar({
                theme: true,
                header: {
                    left: 'prevYear,nextYear,prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                displayEventTime: false,
                defaultView: 'month',
                eventClick: updateEvent,
                eventOrder: "-start",
                weekends: false,
                selectable: true,
                selectHelper: true,
                select: selectDate,
                minTime: "08:00:00",
                maxTime: "18:00:00",
                editable: true,
                events: "DataHandlers/DisplayEventHandler.ashx?qqq=" + sear,
                eventDrop: eventDropped,
                eventResize: eventResized,

                eventRender: function (event, element) {
                    // alert(event.color);
                    // element.style.color = "red";


                    if (event.color == "#FFFF00")
                        element.find('.fc-title').css("color", "black");
                    element.find('.fc-title').append("<br/>" + event.description);
                    //alert(event.title);
                    element.qtip({
                        content: {
                            text: qTipText(event.start, event.end, event.description),
                            title: '<strong>' + event.title + '</strong>'
                        },
                        position: {
                            my: 'bottom left',
                            at: 'top right'
                        },
                        style: { classes: 'qtip-default  qtip qtip-tipped qtip-shadow qtip-rounded' }
                    });
                },


            });

            //console.log();
            //var events = $('#calendar').fullCalendar('clientEvents');

            // console.log(eventsList);
            // if (eventsList.length > 0) {
            //   $('#calendar').fullCalendar('gotoDate', eventsList[0].start);
            // }

          //  getEventList();
        }

        function getEventList() {

            //var events = $('#calendar').fullCalendar('clientEvents');
            //console.log(events);
            //console.log(events[0]);
            //if (events.length > 0) {
            //    console.log(events[0].start);
            //    $('#calendar').fullCalendar('gotoDate', events[0].start);
            ////}

        }


        function filter(events) {
            var sear = $("#searchme").val();
            for (i = 0 ; i < events.length; i++) {
                if (events.title != sear || events.description != sear)
                    events.splice(i, 1);
            }
            return events;
        }

    </script>
    <style type="text/css">
        
        .ui-widget-content.ui-dialog {
            border: 1px solid #000 !important;
        }

        .ui-button {
            background-color: lightblue !important;
        }
    </style>
</asp:Content>

