<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NoNav.Master" CodeBehind="MonthlyVolumeGraphDisplay.aspx.cs" Inherits="DeltoneCRM.Reports.MonthlyVolumeGraphDisplay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <title></title>
     <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>

<link href="amcharts/style.css" rel="stylesheet" />
    <script src="amcharts/amcharts.js"></script>
    <script src="amcharts/serial.js"></script>
    <script src="amcharts/plugins/export/export.min.js"></script>
    <%--<script src="amcharts/amstock.js"></script>--%>
    <link href="amcharts/plugins/export/export.css" rel="stylesheet" />
    <script src="amcharts/plugins/light.js"></script>

    <style type="text/css">
        body {
            background-color: #FFFFCC !important;
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
           #gridDiv {
            max-width: 70%;
            margin: 0 auto;
            width:70%; 
       min-height:150px; 
       text-align:center;
       font-weight:bold;
      
        }
           .messagecss{
    margin-left:300px;
    color:green;
    text-align:center;
       font-weight:bold;
}

            .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 40px;
            cursor: pointer;
              color: blue;
            text-align: center;
            font-weight: bold;
        }

            .buttonClass:hover {
                border: solid 1px Black;
                background-color: #ffffff;
            }

        .moveRight {
            float: right;
            margin-right: 100px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }

        .moverigRight{
              float: right;
            margin-right: 40px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }

        .marginRight{
            margin-right:35px;


        }

        </style>
    <script>

     

        $(document).ready(function () {

            var dateNow = new Date();
            $("#ContentPlaceHolder1_startdate").datepicker({ dateFormat: 'dd/mm/yy' });
            $("#ContentPlaceHolder1_enddate").datepicker({ dateFormat: 'dd/mm/yy' });

            var setDateForm = dateNow.getDate() + "/" + dateNow.getMonth() + "/" + dateNow.getFullYear();
            $("#ContentPlaceHolder1_startdate").val(setDateForm);
            $("#ContentPlaceHolder1_enddate").val(setDateForm);

            // callLineTest();
          //  callLineMultipleTest();

            var saleFigures = $("#graphDataSales").val();
          //  console.log(saleFigures);
            var repName = $("#repNameSales").val();

            var repGraph = [];
            var scropll = [];

            var splitRep = repName.split(',');
            for (var i = 0; i < splitRep.length -1; i++) {
                if (splitRep[i] != ",") {

                   var eachEle= createGraphForRep(splitRep[i])
                   repGraph.push(eachEle);
                   var eleScroll = callCahrtScroll(splitRep[i]);
                   scropll.push(eleScroll);
                }

            }

           

            setSalesFigures(saleFigures, repGraph, scropll);

        });

        function createGraphForRep(repName) {

            var grapgFormat = {
                "id": repName,
                "balloon": {
                    "drop": true,
                    "adjustBorderColor": false,
                    "color": "#ffffff"
                },
                "bullet": "round",
                "bulletBorderAlpha": 1,
                "bulletColor": "#FFFFFF",
                "bulletSize": 5,
                "hideBulletsCount": 50,
                "lineThickness": 2,
                "title": repName,
                "useLineColorForBulletBorder": true,
                "valueField": repName,
                "balloonText": "<span style='font-size:18px;'>[[ " + repName + "]]</span>"
            };


            return grapgFormat;

        }

        function callCahrtScroll(repName) {

            var chartScroll = {
                "chartScrollbar": {
                    "graph": repName,
                    "oppositeAxis": false,
                    "offset": 30,
                    "scrollbarHeight": 80,
                    "backgroundAlpha": 0,
                    "selectedBackgroundAlpha": 0.1,
                    "selectedBackgroundColor": "#888888",
                    "graphFillAlpha": 0,
                    "graphLineAlpha": 0.5,
                    "selectedGraphFillAlpha": 0,
                    "selectedGraphLineAlpha": 1,
                    "autoGridCount": true,
                    "color": "#AAAAAA"
                }
            };


        }

        function setSalesFigures(graphdata, repGraph,scrollRe) {

            var conjsonData = JSON.parse(graphdata)
           // console.log(graphdata);

            var chart = AmCharts.makeChart("chartdiv", {
                "type": "serial",
                "theme": "light",
                "marginRight": 40,
                "marginLeft": 40,
                "autoMarginOffset": 20,
                "mouseWheelZoomEnabled": true,
                "dataDateFormat": "YYYY-MM-DD",
                "valueAxes": [{
                    "id": "v1",
                    "axisAlpha": 0,
                    "position": "left",
                    "ignoreAxisWidth": true
                }],
                "balloon": {
                    "borderThickness": 1,
                    "shadowAlpha": 0
                },
                "graphs": repGraph,
                "chartScrollbar": {
                    "graph": "g1",
                    "oppositeAxis": false,
                    "offset": 30,
                    "scrollbarHeight": 80,
                    "backgroundAlpha": 0,
                    "selectedBackgroundAlpha": 0.1,
                    "selectedBackgroundColor": "#888888",
                    "graphFillAlpha": 0,
                    "graphLineAlpha": 0.5,
                    "selectedGraphFillAlpha": 0,
                    "selectedGraphLineAlpha": 1,
                    "autoGridCount": true,
                    "color": "#AAAAAA"
                },
                "chartCursor": {
                    "pan": true,
                    "valueLineEnabled": true,
                    "valueLineBalloonEnabled": true,
                    "cursorAlpha": 1,
                    "cursorColor": "#258cbb",
                    "limitToGraph": "g1",
                    "valueLineAlpha": 0.2,
                    "valueZoomable": true
                },
                "valueScrollbar": {
                    "oppositeAxis": false,
                    "offset": 50,
                    "scrollbarHeight": 10
                },
                "categoryField": "date",
                "categoryAxis": {
                    "parseDates": true,
                    "dashLength": 1,
                    "minorGridEnabled": true
                },
                "legend": {},
                "export": {
                    "enabled": true
                },
                "dataProvider": conjsonData

                //  chart.addListener("rendered", zoomChart);

                // zoomChart();
            });
            }



       
       
</script>

   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagersalesvisualdata" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>

    <asp:HiddenField ID="graphDataSales" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="repNameSales" runat="server" ClientIDMode="Static" />
    <h2 style="text-align:center">MONTHLY VOLUME IN VISUALIZATION</h2>
   
      <asp:Button OnClick="btnbackupload_Click" ID="Buttonback" Text="Back" ForeColor="Blue" Width="6%"
                    runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />
            <asp:Button OnClick="btnupload_Click" ID="btnDashboard" Text="Dashboard" ForeColor="Blue" Width="6%"
                    runat="server" CssClass="buttonClass moverigRight" CausesValidation="false" />
    
   
        <div style="padding-left:180px;padding-bottom:12px;"> 

            <span style="width:90px;height:30px;font-weight:bold;margin-right:30px;">Rep Name:</span> <asp:DropDownList ID="RepNameDropDownList" runat="server" Height="30px" Width="200px">

                                    </asp:DropDownList> <span style="font-weight:bold;margin-right:30px;margin-left:30px;"> Start Date:</span> <input type="datetime" id="startdate" runat="server"/> 
            <span style="font-weight:bold;margin-right:30px;margin-left:30px;"> End Date:</span> <input type="datetime" id="enddate" runat="server"/> 
            <asp:Button ID="filterButton" runat="server" OnClick="callFilerSales" Text="Filter"  ForeColor="Blue" Width="6%"/>
           <%--  <input type="button" value="Filter" style="color:blue;width:160px;font-weight:bold;margin-left:30px;"/>--%>
           
        </div>
    <div style="padding-left:240px;">
         
        <asp:Button ID="dailyButton" Text="Daily" Visible="false" runat="server" OnClick="callDailySales" CssClass="buttonClass marginRight" />
        
         <asp:Button ID="monthly" Text="Monthly" runat="server" OnClick="callmonthlySales" CssClass="buttonClass marginRight" />
      
         <asp:Button ID="yearly" Text="Yearly" runat="server" OnClick="callyearlySales" CssClass="buttonClass marginRight" />

    </div>
   
    <div id="chartdiv" style="width:100%; height:700px;"></div>
    
    

`</asp:Content>
