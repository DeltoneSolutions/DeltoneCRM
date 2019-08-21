<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceSheet.aspx.cs"
    MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.AttendanceSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>
    <script src="js/jquery-1.11.1.min.js"></script>
    <link href="css/NewCSS.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.js"></script>

    <%-- <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>--%>
    <script src="js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.dataTables_new.css" />

    <%-- <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jscolor.js"></script>
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>
    <%--  <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
	</script>--%>
    <%--	<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>--%>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script src="Scripts/date-uk.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />



    <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />

    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-sliderAccess.js"></script>
    <%--<script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>--%>
    <title>Attendance List</title>

    <style type='text/css'>
        /* css for timepicker */


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

        #gridDiv {
            max-width: 70%;
            margin: 0 auto;
            width: 70%;
            min-height: 150px;
            text-align: center;
            font-weight: bold;
        }

        .messagecss {
            margin-left: 300px;
            color: green;
            text-align: center;
            font-weight: bold;
        }


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

        #gridDiv {
            max-width: 70%;
            margin: 0 auto;
            width: 70%;
            min-height: 150px;
            text-align: center;
            font-weight: bold;
        }

        .messagecss {
            margin-left: 300px;
            color: green;
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

        .moveRight {
            float: right;
            margin-bottom: 30px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }

        .buttons-print {
            float: left;
            background-color: #ffffff;
            height: 40px;
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 20px;
            cursor: pointer;
            margin-bottom: 10px;
            color: blue;
            text-align: center;
            font-weight: bold;
            margin-left: 10px;
            padding-top: 4px;
        }

        .buttons-excel {
            float: left;
            background-color: #ffffff;
            height: 40px;
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 20px;
            cursor: pointer;
            margin-bottom: 10px;
            color: blue;
            text-align: center;
            font-weight: bold;
            margin-left: 10px;
            padding-top: 4px;
        }

        .undoButtonStyle {
            margin-left: 30px;
        }

        .inputColorGreen {
            border-top-color: #4fc2bd;
            border-right-color: #4fc2bd;
            border-left-color: #4fc2bd;
            border-bottom-color: #4fc2bd;
            border-top-width: 1px;
            border-right-width: 1px;
            border-left-width: 1px;
            border-bottom-width: 1px;
            border-top-style: solid;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            outline: none;
            float: right;
        }

        .inputTextStyle {
            float: right;
            text-align: right;
            color: #4fc2bd !important;
        }

        #drop_zone {
            max-width: 40%;
            margin: 0 auto;
            width: 40%;
            min-height: 150px;
            text-align: center;
            text-transform: uppercase;
            font-weight: bold;
            font-size: 30px;
            border: 8px dashed #3E3E42;
            height: 160px;
        }

        #spanConatt {
            display: block;
            vertical-align: middle;
            line-height: normal;
            margin-top: 50px;
            opacity: 0.3;
        }

        td.highlight {
            font-weight: bold;
            color: red !important;
        }

        td.highlightclicked {
            font-weight: bold;
            color: #4274f4 !important;
        }

        /*tr.odd { background-color: blue; } 
 tr.even { background-color: green; }

table.dataTable tr.odd .sorting_1 { background-color: blue; } 
table.dataTable tr.even .sorting_1 { background-color: green; }*/


        table.dataTable tbody tr.myeven {
            background-color: #f2dede !important;
        }

        table.dataTable tbody tr.myodd {
            background-color: #bce8f1 !important;
        }
    </style>

    <script type="text/javascript">


        var table;
        var repNameselected = "";
        var startTimeselected = "";
        var startTimeChangted = "";
        var endTimeselected = "";
        var endTimeChangted = "";
        var lunchTimeselected = "";
        var lunchTimeChangted = "";


        function handleDragOver(event) {
            event.stopPropagation();
            event.preventDefault();
            var dropZone = document.getElementById('spanConatt');
            dropZone.innerHTML = "Drop now";
        }

        function handleDnDFileSelect(event) {
            event.stopPropagation();
            event.preventDefault();

            /* Read the list of all the selected files. */
            files = event.dataTransfer.files;

            /* Consolidate the output element. */
            var form = document.getElementById('form1');
            var data = new FormData(form);

            for (var i = 0; i < files.length; i++) {

                s = files[i].name;
                data.append(files[i].name, files[i]);
            }
            if (s != null) {
                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    
                    if (xhr.readyState == 4 && xhr.status == 200 && xhr.responseText) {
                        alert("Upload done!");
                        window.location.reload();
                    } else {
                        //alert("upload failed!");
                    }
                };
                xhr.open('POST', "AttendanceSheet.aspx");
                // xhr.setRequestHeader("Content-type", "multipart/form-data");
                xhr.send(data);
            }
            else {
                var dropZone = document.getElementById('spanConatt');
                dropZone.innerHTML = "Drop files here";

            }

        }


        $(document).ready(function () {
            $('#findcustomerorder').autocomplete({
                source: "Fetch/FetchSearchOrder.aspx",
                select: function (event, ui) {
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    // $('#CompanyContactTR').show();
                    // $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    var valsSPLIT = ui.item.id.split(',');;

                    var url = 'order.aspx?Oderid=' + (valsSPLIT[0]) + '&cid=' + (valsSPLIT[2])
                            + '&Compid=' + (valsSPLIT[1]);
                    window.open(url, "_blank");
                    // $(this).val(''); return false;


                }
            });
        });



        function readAttendanceFile() {

            var re = $('#RepNameDropDownList').val();;

            createTableData(re);
        }

        function createTableData(re) {
            console.log('tyss' + re);
            if (table)
                table.destroy();


            table = $('#attendancetablelist').DataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response...</img>'  //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                "ajax": "../Fetch/FetchAttendanceFileData.aspx?n=1",
                "columnDefs": [
                        {
                            className: 'align_center', "targets": [0, 1, 2, 3, 4,5,6,7],
                        },
                          
                

                        { "width": "10%", "targets": 0 },
                         { "width": "8%", "targets": [6,7] },
                         { "visible": false, "targets": [ 8,9,10,11,12 ]},
                         { "type": 'date-uk', "targets": 1 }
                ],
                "aaSorting": [[1, "asc"]],
                "iDisplayLength": 25,
                "fnServerParams": function (aoData) {
                    aoData.push(
                        
                    {
                        "name": "rep", "value": re,
                       
                    },
                    {
                        "name": "repNameAlter", "value": repNameselected,
                        
                    },
                    {
                        "name": "repNameAlterStartTime", "value": startTimeselected
                    },
                    {
                        "name": "startTimeValue", "value": startTimeChangted
                    },
                    {
                        "name": "repNameAlterEndTime", "value": endTimeselected
                    },
                    {
                        "name": "endTimeValue", "value": endTimeChangted
                    },
                    {
                        "name": "repNameAlterLunchTime", "value": lunchTimeselected
                    },
                    {
                        "name": "lunchTimeValue", "value": lunchTimeChangted
                    }
                    );
                },
                dom: 'lBfrtip',
               
                buttons: [{ extend: 'print', footer: true },
                    { extend: 'excelHtml5', footer: true, text: 'Export Excel', },
                    
                ],
                
                "footerCallback": function ( row, data, start, end, display ) {
                    var api = this.api(), data;
 
                    // Remove the formatting to get integer data for summation
                    var intVal = function ( i ) {
                        return typeof i === 'string' ?
                            i.replace(/[\$,]/g, '')*1 :
                            typeof i === 'number' ?
                            i : 0;
                    };
 
                    //// Total over all pages
                    //total = api
                    //    .column( 4 )
                    //    .data()
                    //    .reduce( function (a, b) {
                    //        return intVal(a) + intVal(b);
                    //    }, 0 );
 
                    // Total over this page
                    pageTotal = api
                        .column( 7, { page: 'current'} )
                        .data()
                        .reduce( function (a, b) {
                            return intVal(a) + intVal(b);
                        }, 0);

                     
 
                    // Update footer
                    //$( api.column( 4 ).footer() ).html(
                    //    +pageTotal +' ( '+ total +' total)'
                    //);

                    $( api.column(7 ).footer() ).html(pageTotal 
                    );
                },
                "createdRow": function (row, data, index) {

                    

                    // $('tr', row).addClass('myodd');
                    if (data[8] == "1") {
                         
                        $('td', row).eq(1).addClass('highlight');
                        $('td', row).eq(3).addClass('highlight');
                        //$('td', row).eq(0).addClass('highlight');
                    }

                    if (data[9] == "1") {
                        $('td', row).eq(2).addClass('highlight');
                        $('td', row).eq(4).addClass('highlight');
                        //$('td', row).eq(0).addClass('highlight');
                    }
                    if (data[10] == "1") {
                        $('td', row).eq(1).addClass('highlightclicked');
                        //$('td', row).eq(0).addClass('highlight');
                    }
                    if (data[11] == "1") {
                        $('td', row).eq(2).addClass('highlightclicked');
                        //$('td', row).eq(0).addClass('highlight');
                    }
                    if (data[12] == "1") {
                        $('td', row).eq(6).addClass('highlightclicked');
                        //$('td', row).eq(0).addClass('highlight');
                    }
                    // $(row).css('background-color', 'Orange !important');

                },
                "rowCallback": function (row, data, index) {
                    
                    if (index % 2 == 0) {
                        $(row).removeClass('myodd myeven');
                        $( row).addClass('myodd');
                    } else {
                        $(row).removeClass('myodd myeven');
                        $(row).addClass('myeven');
                    }
                }
            
            });


        }

       

        $(document).ready(function () {
            readAttendanceFile();
            repNameChange();
            // GetData();
            initstartDateDialogWindow();
            initBreakTimeDialogWindow();
            initendDateDialogWindow();


            var myTable = $('#attendancetablelist').DataTable();

            $("#attendancetablelist").on('click', 'td', function () {
               // alert('clicked!');
            });

            $('#attendancetablelist').on('click', 'tbody td', function () {
                // myTable.cell(this).edit();

                 //$('#addForstartDate').dialog('open');
            });
            $('#attendancetablelist tbody').on('click', 'tr', function () {
                repNameselected = "";
                startTimeselected = "";
                startTimeChangted = "";
                var ccc = table.row(this).data();
                // $(this).css("background-color", "#4274f4");
               // $('td', this).eq(1).addClass('highlightclicked');
               // $('#addForstartDate').dialog('open');
                repNameselected = ccc[0];
                startTimeselected = ccc[1];
                //console.log(ccac);
                //console.log(ccac2);
                //  $("#nameCom").text(ccc[2] + "-->" + ccc[4] + "-->" + ccc[5]);
            });

            $('#starttimeOnly').timepicker({
                controlType: 'select',
                stepMinute: 10,
                hourMin: 8,
                hourMax: 17,
                hour: 8,
                minute: 30,
                showSecond: false,
                showMillisec: false,
                showTimezone: false,
                showMicrosec: false,
                defaultValue:'08:30'

            });

            $('#endtimeOnly').timepicker({
                controlType: 'select',
                stepMinute: 10,
                hourMin: 8,
                hourMax: 17,
                hour: 8,
                minute: 30,
                showSecond: false,
                showMillisec: false,
                showTimezone: false,
                showMicrosec: false,
                defaultValue: '08:30'

            });

        });


        function initstartDateDialogWindow() {

            $('#addForstartDate').dialog({
                autoOpen: false,
                width: 350,
                modal: true,
                buttons: [

            //{
            //    text: "Cancel",
            //    "class": 'ui-button ui-corner-all ui-widget',
            //    click: function () {
            //        $(this).dialog("close");
            //    }
            //},
            {
                text: "Update",
                "class": 'ui-button ui-corner-all ui-widget',

                click: function () {

                    // if (validateInput()) {
                    //alert("sending " + eventToAdd.title);
                    // manageUrgencyChange();
                    // PageMethods.addEvent(eventToAdd, addSuccess);
                    startTimeChangted = $("#starttimeOnly").val();
                    readAttendanceFile();
                    $(this).dialog("close");

                    startTimeselected = "";
                    startTimeChangted = "";

                    // }
                }
            }
                ]
            });


           
        }


        function initendDateDialogWindow() {

            $('#addForendDate').dialog({
                autoOpen: false,
                width: 350,
                modal: true,
                buttons: [

            //{
            //    text: "Cancel",
            //    "class": 'ui-button ui-corner-all ui-widget',
            //    click: function () {
            //        $(this).dialog("close");
            //    }
            //},
            {
                text: "Update",
                "class": 'ui-button ui-corner-all ui-widget',

                click: function () {

                    // if (validateInput()) {
                    //alert("sending " + eventToAdd.title);
                    // manageUrgencyChange();
                    // PageMethods.addEvent(eventToAdd, addSuccess);
                    endTimeChangted = $("#endtimeOnly").val();
                    readAttendanceFile();
                    $(this).dialog("close");

                    endTimeChangted = "";
                    endTimeselected = "";

                    // }
                }
            }
                ]
            });



        }


        function repNameChange() {

            $('#RepNameDropDownList').change(
    function () {

        readAttendanceFile();
    });
        }



        function EditStartTime(repname, startDateTime) {
            console.log('int start' + repname);
            repNameselected = "";
            startTimeselected = "";
            startTimeChangted = "";

            endTimeChangted = "";
            endTimeselected = "";

            lunchTimeselected = "";
            lunchTimeChangted = "";

            repNameselected = repname;
            startTimeselected = startDateTime;

            $('#addForstartDate').dialog('open');
        }

        function EditEndTime(repname, startDateTime) {
            console.log('int end' + repname);
            repNameselected = "";
            endTimeChangted = "";
            endTimeselected = "";

            startTimeselected = "";
            startTimeChangted = "";

            lunchTimeselected = "";
            lunchTimeChangted = "";

            repNameselected = repname;
            endTimeselected = startDateTime;
            $('#addForendDate').dialog('open');
        }

        function EditBreakTime(repname, startDateTime) {
            console.log('int break' + repname);
            repNameselected = "";

            lunchTimeselected = "";
            lunchTimeChangted = "";

            endTimeChangted = "";
            endTimeselected = "";

            startTimeselected = "";
            startTimeChangted = "";


            repNameselected = repname;
            lunchTimeselected = startDateTime;
            $('#addbreaktime').dialog('open');
        }


     

        function initBreakTimeDialogWindow() {

            $('#addbreaktime').dialog({
                autoOpen: false,
                width: 350,
                modal: true,
                buttons: [

            //{
            //    text: "Cancel",
            //    "class": 'ui-button ui-corner-all ui-widget',
            //    click: function () {
            //        $(this).dialog("close");
            //    }
            //},
            {
                text: "Update",
                "class": 'ui-button ui-corner-all ui-widget',

                click: function () {

                   
                    lunchTimeChangted = $("#minutesText").val();
                    readAttendanceFile();
                    $(this).dialog("close");
                    lunchTimeselected = "";
                    lunchTimeChangted = "";
                    // }
                }
            }
                ]
            });
        }

        
        function ValidateNumbers(){

            var numberMin = $("#minutesText").val();

            if (numberMin == "")
                return false;

            return true;
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="gridDiv">
        <h2>ATTENDANCE LOG</h2>
        <br />
        <br />
        <br />
        <asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false" />
        <asp:Button OnClick="btnupload_Click" ID="Button1" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />


        <div id="main">
            <br />

            <div id="drop_zone">
                <span id="spanConatt">Drop files here </span>
            </div>

            <br />


            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
                <tr>
                    <td>
                        <span style="float: left; width: 120px;">STAFF NAME :</span>
                        <asp:DropDownList ID="RepNameDropDownList" ClientIDMode="Static" runat="server" Width="240px" Height="30px">
                           <%-- <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <asp:ListItem Text="AIDAN" Value="AIDAN"></asp:ListItem>
                            <asp:ListItem Text="BAILEY" Value="BAILEY"></asp:ListItem>
                            <asp:ListItem Text="BRENDAN" Value="BRENDAN"></asp:ListItem>
                            <asp:ListItem Text="DIMITRI" Value="DIMITRI"></asp:ListItem>
                            <asp:ListItem Text="EMMA" Value="EMMA"></asp:ListItem>
                            <asp:ListItem Text="JOHN" Value="JOHN"></asp:ListItem>
                            <asp:ListItem Text="KRIT" Value="KRIT"></asp:ListItem>
                            <asp:ListItem Text="TARAS" Value="TARAS"></asp:ListItem>
                            <asp:ListItem Text="TRENT" Value="TRENT"></asp:ListItem>
                            <asp:ListItem Text="SAM" Value="SAM"></asp:ListItem> 
                            <asp:ListItem Text="SOPHIE" Value="SOPHIE"></asp:ListItem>
                            <asp:ListItem Text="JAMES" Value="JAMES"></asp:ListItem>
                            <asp:ListItem Text="HENRY" Value="HENRY"></asp:ListItem>
                              <asp:ListItem Text="CHRISTINE" Value="CHRISTINE"></asp:ListItem>
                             <asp:ListItem Text="ALEC" Value="ALEC"></asp:ListItem>
                             <asp:ListItem Text="JOE" Value="JOE"></asp:ListItem>
                             <asp:ListItem Text="JOSIAH" Value="JOSIAH"></asp:ListItem>
                             <asp:ListItem Text="LACHLAN" Value="LACHLAN"></asp:ListItem>
                             <asp:ListItem Text="PHIL" Value="PHIL"></asp:ListItem>
                             <asp:ListItem Text="FARAH" Value="FARAH"></asp:ListItem>
                             <asp:ListItem Text="ACCOUNTS" Value="ACCOUNTS"></asp:ListItem>
                            <asp:ListItem Text="PARTI" Value="PARTI"></asp:ListItem>--%>

                        </asp:DropDownList>
                    </td>
                </tr>



                <tr>

                    <%-- <td height="25px" class="section_headings">Companies List</td>--%>
                    <td height="25px">



                        <%--   <input type="button"  id="allocateRepButton" value="CREATE LINK"  onclick="return callUpdateCompany()"
                            class="buttonClass moveRight"  />--%>

                        <div id="dvProgressBar" style="float: inherit; visibility: hidden;">
                            <img src="/images/loadingimage1.gif" />
                            <strong style="color: red;">Updating, Please Wait...</strong>
                        </div>
                    </td>

                </tr>




            </table>

        </div>

        <div id="maindatatableMain">


            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

                <tr>
                    <td height="25px" class="section_headings">ATTENDANCE DETAILS</td>
                </tr>


                <tr>
                    <td class="white-box-outline">
                        <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                            <tr>
                                <td height="20px">&nbsp;</td>
                            </tr>
                            <%-- <tr>
                                <td height="20px" style="margin-bottom:12px;"> <input class="inputColorGreen" type="text" id="linkedCom" /> <span class="inputTextStyle" style="margin-right: 24px; ">Search By Linked Y/N  : </span>
                                </td>
                            </tr>--%>
                            <tr>
                                <td height="20px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>

                                    <div class="container">
                                        <table cellpadding="0" width="1000" cellspacing="0" border="0" class="display" id="attendancetablelist" style="cursor: pointer">
                                            <thead>
                                                <tr>
                                                    <th align="left">STAFF NAME</th>
                                                    <th align="left">START DATE </th>
                                                    <th align="left">END DATE </th>
                                                    <th align="left">START TIME </th>
                                                    <th align="left">END TIME </th>
                                                    <th align="left">TOTAL HOURS</th>
                                                    <th align="left">LUNCH BREAK IN MINUTES</th>
                                                    <th align="left">HOURS WORKED</th>
                                                  <th align="left" >HOURS WORKED</th>
                                                     <th align="left" >HOURS WORKED</th>
                                                     <th align="left" >HOURS WORKED</th>
                                                    <th align="left" >HOURS WORKED</th>
                                                     <th align="left" >HOURS WORKED</th>
                                                       <th align="left">EDIT START TIME</th>
                                                    <th align="left">EDIT END TIME</th>
                                                    <th align="left">EDIT LUNCH BREAK</th>
                                                </tr>

                                            </thead>

                                            <tfoot>
                                                <tr>
                                                    <th></th>
                                                    <th></th>
                                                    <th></th>
                                                    <th></th>
                                                    <th></th>
                                                    <th></th>
                                                    <th style="text-align: right">Total:</th>
                                                    <th></th>
                                                     <th></th>
                                                    <th></th>
                                                     <th></th>
                                                    <th></th>
                                                    <th></th>
                                                   <th></th>
                                                    <th></th>
                                                    <th></th>
                                                </tr>
                                            </tfoot>


                                        </table>

                                    </div>


                                </td>
                            </tr>
                            <tr>
                                <td height="25px">&nbsp;</td>
                            </tr>

                        </table>
                    </td>
                </tr>


            </table>

        </div>


    </div>


    <div id="addForstartDate" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Update Start Time">
        <table class="style1">

            <tr>
                <td class="alignRight">START TIME :</td>
                <td class="alignLeft">
                    <input id="starttimeOnly" type="text" size="10" />
                </td>
            </tr>


        </table>

    </div>

    <div id="addForendDate" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Update End Time">
        <table class="style1">

            <tr>
                <td class="alignRight">END TIME :</td>
                <td class="alignLeft">
                    <input id="endtimeOnly" type="text" size="10" />
                </td>
            </tr>


        </table>

    </div>

     <div id="addbreaktime" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Change Lunch Break">
        <table class="style1">

            <tr>
                <td class="alignRight">Lunch Break Time(Minutues) :</td>
                <td class="alignLeft">

                    <input name="minutesText" type="text" style="width: 70px; margin-bottom: 15px;" id="minutesText"  />

                   

                </td>
            </tr>


        </table>

    </div>

    <script>
        if (window.File && window.FileList && window.FileReader) {
            /************************************ 
             * All the File APIs are supported. * 
             * Entire code goes here.           *
             ************************************/


            /* Setup the Drag-n-Drop listeners. */
            var dropZone = document.getElementById('drop_zone');
            dropZone.addEventListener('dragover', handleDragOver, false);
            dropZone.addEventListener('drop', handleDnDFileSelect, false);

        }
        else {
            alert('Sorry! this browser does not support HTML5 File APIs.');
        }
    </script>
</asp:Content>
