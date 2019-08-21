<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/NoNav.Master" CodeBehind="SalesAllusersGraphDisplay.aspx.cs" Inherits="DeltoneCRM.Reports.SalesAllusersGraphDisplay" %>
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

        function callLineTest() {
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
                "graphs": [{
                    "id": "g1",
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
                    "title": "red line",
                    "useLineColorForBulletBorder": true,
                    "valueField": "value",
                    "balloonText": "<span style='font-size:18px;'>[[value]]</span>"
                }],
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
                "export": {
                    "enabled": true
                },
                "dataProvider": [{
                    "date": "2012-07-27",
                    "value": 13
                }, {
                    "date": "2012-07-28",
                    "value": 11
                }, {
                    "date": "2012-07-29",
                    "value": 15
                }, {
                    "date": "2012-07-30",
                    "value": 16
                }, {
                    "date": "2012-07-31",
                    "value": 18
                }, {
                    "date": "2012-08-01",
                    "value": 13
                }, {
                    "date": "2012-08-02",
                    "value": 22
                }, {
                    "date": "2012-08-03",
                    "value": 23
                }, {
                    "date": "2012-08-04",
                    "value": 20
                }, {
                    "date": "2012-08-05",
                    "value": 17
                }, {
                    "date": "2012-08-06",
                    "value": 16
                }, {
                    "date": "2012-08-07",
                    "value": 18
                }
               
                   , {
                    "date": "2012-09-29",
                    "value": 69
                }, {
                    "date": "2012-09-30",
                    "value": 71
                }, {
                    "date": "2012-10-01",
                    "value": 67
                }, {
                    "date": "2012-10-02",
                    "value": 63
                }, {
                    "date": "2012-10-03",
                    "value": 46
                }, {
                    "date": "2012-10-04",
                    "value": 32
                }, {
                    "date": "2012-10-05",
                    "value": 21
                }, {
                    "date": "2012-10-06",
                    "value": 18
                }, {
                    "date": "2012-10-07",
                    "value": 21
                }, {
                    "date": "2012-10-08",
                    "value": 28
                }, {
                    "date": "2012-10-09",
                    "value": 27
                }, {
                    "date": "2012-10-10",
                    "value": 36
                }, {
                    "date": "2012-10-11",
                    "value": 33
                }, {
                    "date": "2012-10-12",
                    "value": 31
                }, {
                    "date": "2012-10-13",
                    "value": 30
                }, {
                    "date": "2012-10-14",
                    "value": 34
                }, {
                    "date": "2012-10-15",
                    "value": 38
                }, {
                    "date": "2012-10-16",
                    "value": 37
                }, {
                    "date": "2012-10-17",
                    "value": 44
                }, {
                    "date": "2012-10-18",
                    "value": 49
                }, {
                    "date": "2012-10-19",
                    "value": 53
                }, {
                    "date": "2012-10-20",
                    "value": 57
                }, {
                    "date": "2012-10-21",
                    "value": 60
                },
                {
                    "date": "2012-12-13",
                    "value": 77
                }, {
                    "date": "2012-12-14",
                    "value": 67
                }, {
                    "date": "2012-12-15",
                    "value": 62
                }, {
                    "date": "2012-12-16",
                    "value": 64
                }, {
                    "date": "2012-12-17",
                    "value": 61
            
                }, {
                    "date": "2013-01-16",
                    "value": 71
                }, ]
            });

          //  chart.addListener("rendered", zoomChart);

           // zoomChart();
        }



       
        

        function zoomChart() {
            chart.zoomToIndexes(chart.dataProvider.length - 40, chart.dataProvider.length - 1);
        }

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



        function callLineMultipleTest() {
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
                "graphs": [{
                    "id": "g1",
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
                    "title": "First Name",
                    "useLineColorForBulletBorder": true,
                    "valueField": "value1",
                    "balloonText": "<span style='font-size:18px;'>[[value1]]</span>"
                },
                {
                    "id": "g2",
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
                    "title": "Second Name",
                    "useLineColorForBulletBorder": true,
                    "valueField": "value2",
                    "balloonText": "<span style='font-size:18px;'>[[value2]]</span>"
                },
                {
                    "id": "g3",
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
                    "title": "Third Name",
                    "useLineColorForBulletBorder": true,
                    "valueField": "value3",
                    "balloonText": "<span style='font-size:18px;'>[[value3]]</span>"
                }

                ],
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
                "dataProvider": [{
                    "date": "2012-07-27",
                    "value1": 13,
                    "value2": 16,
                    "value3": 18,
                }, {
                    "date": "2012-07-28",
                    "value1": 16,
                    "value2": 17,
                    "value3": 12,
                }, {
                    "date": "2012-07-29",
                    "value1": 13,
                    "value2": 16,
                    "value3": 18,
                }, {
                    "date": "2012-07-30",
                    "value1": 13,
                    "value2": 16,
                    "value3": 18,
                }, {
                    "date": "2012-07-31",
                    "value": 18,
                    "value2": 17,
                    "value3": 12,
                }, {
                    "date": "2012-08-01",
                    "value1": 16,
                    "value2": 17,
                    "value3": 12,
                }, {
                    "date": "2012-08-02",
                    "value1": 16,
                    "value2": 17,
                    "value3": 12,
                }, {
                    "date": "2012-08-03",
                    "value1": 11,
                    "value2": 12,
                    "value3": 13,
                }, {
                    "date": "2012-08-04",
                    "value1": 16,
                    "value2": 17,
                    "value3": 18,
                }, {
                    "date": "2012-08-05",
                    "value1": 11,
                    "value2": 17,
                    "value3": 16,
                }, {
                    "date": "2012-08-06",
                    "value1": 12,
                    "value2": 15,
                    "value3": 18,
                }, {
                    "date": "2012-08-07",
                    "value1": 15,
                    "value2": 16,
                    "value3": 19,
                }

                   , {
                       "date": "2012-09-29",
                       "value1": 19,
                       "value2": 17,
                       "value3": 18,
                   }, {
                       "date": "2012-09-30",
                       "value1": 14,
                       "value2": 18,
                       "value3": 19,
                   }, {
                       "date": "2012-10-01",
                       "value1": 10,
                       "value2": 17,
                       "value3": 17,
                   }, {
                       "date": "2012-10-02",
                       "value1": 16,
                       "value2": 17,
                       "value3": 12,
                   }, {
                       "date": "2012-10-03",
                       "value1": 11,
                       "value2": 10,
                       "value3": 18,
                   }, {
                       "date": "2012-10-04",
                       "value1": 16,
                       "value2": 15,
                       "value3": 15,
                   }, {
                       "date": "2012-10-05",
                       "value1": 16,
                       "value2": 17,
                       "value3": 12,
                   }, {
                       "date": "2012-10-06",
                       "value1": 16,
                       "value2": 13,
                       "value3": 16,
                   }, {
                       "date": "2012-10-07",
                       "value1": 12,
                       "value2": 11,
                       "value3": 13,
                   }, {
                       "date": "2012-10-08",
                       "value1": 15,
                       "value2": 16,
                       "value3": 17,
                   }, {
                       "date": "2012-10-09",
                       "value1": 11,
                       "value2": 18,
                       "value3": 14,
                   }, {
                       "date": "2012-10-10",
                       "value1": 16,
                       "value2": 17,
                       "value3": 12,
                   }, {
                       "date": "2012-10-11",
                       "value1": 19,
                       "value2": 17,
                       "value3": 18,
                   }, {
                       "date": "2012-10-12",
                       "value1": 11,
                       "value2": 14,
                       "value3": 15,
                   }, {
                       "date": "2012-10-13",
                       "value1": 11,
                       "value2": 12,
                       "value3": 12,
                   }, {
                       "date": "2012-10-14",
                       "value1": 16,
                       "value2": 17,
                       "value3": 12,
                   }

                ]
            });

            //  chart.addListener("rendered", zoomChart);

            // zoomChart();
        }


        function callMultipleLines() {

            var chart = AmCharts.makeChart("chartdiv", {
                "type": "serial",
                "theme": "light",
                "dataProvider": [{
                    "date": "2013-01-25",
                    "value1": 2025,
                    "value2": 1800
                }, {
                    "category": "Tuesday:\nWeek 20\nWeek 21",
                    "value1": 1863,
                    "value2": 1951
                }, {
                    "category": "Wednesday:\nWeek 20\nWeek 21",
                    "value1": 1610,
                    "value2": 950
                }, {
                    "category": "Thursday:\nWeek 20\nWeek 21",
                    "value1": 1732,
                    "value2": 1250
                }, {
                    "category": "Friday:\nWeek 20\nWeek 21",
                    "value1": 1123,
                    "value2": 765
                }, {
                    "category": "Saturday:\nWeek 20\nWeek 21",
                    "value1": 985,
                    "value2": 841
                }, {
                    "category": "Sunday:\nWeek 20\nWeek 21",
                    "value1": 1400,
                    "value2": 1300
                }],
                "valueAxes": [{
                    "gridColor": "#FFFFFF",
                    "gridAlpha": 0.2,
                    "dashLength": 0
                }],
                "gridAboveGraphs": true,
                "startDuration": 1,
                "graphs": [{
                    "title": "Week #20",
                    "balloonText": "[[title]]: <b>[[value]]</b>",
                    "bullet": "round",
                    "bulletSize": 10,
                    "bulletBorderColor": "#ffffff",
                    "bulletBorderAlpha": 1,
                    "bulletBorderThickness": 2,
                    "valueField": "value1"
                }, {
                    "title": "Week #22",
                    "balloonText": "[[title]]: <b>[[value]]</b>",
                    "bullet": "round",
                    "bulletSize": 10,
                    "bulletBorderColor": "#ffffff",
                    "bulletBorderAlpha": 1,
                    "bulletBorderThickness": 2,
                    "valueField": "value2"
                }],
                "chartCursor": {
                    "categoryBalloonEnabled": false,
                    "cursorAlpha": 0,
                    "zoomable": false
                },
                "categoryField": "category",
                "categoryAxis": {
                    "gridPosition": "start",
                    "gridAlpha": 0
                },
                "legend": {}
            });
        }
       
</script>

   </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagersalesvisualdata" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>

    <asp:HiddenField ID="graphDataSales" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="repNameSales" runat="server" ClientIDMode="Static" />
    <h2 style="text-align:center">SALES COMMISSION IN VISUALIZATION</h2>
   
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