<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true"  CodeBehind="Stats.aspx.cs" Inherits="DeltoneCRM.Stats" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <meta http-equiv="refresh" content="60">--%>
    <script src="js/jquery-1.9.1.js"></script>
    <script type="text/javascript">

        var scrollingUp = 0;

        var heightScroll = 0;
        window.setInterval(scrollit, 6000);

        function scrollit() {

           // console.log("before:" + $("#scroller").scrollTop());
            var hash = this.hash;
            if (scrollingUp == 0) {
                $('#scroller').animate({ scrollTop: $("#scroller").scrollTop() + 90 }, 7500, function(){
   
                    // Add hash (#) to URL when done scrolling (default click behavior)
                    window.location.hash = hash;
                });
                heightScroll = $("#scroller").scrollTop() + 90;
                console.log(1);
                //$('#scroller').animate({
                //    scrollTop: target.offset().top,
                //    scrollBottom: target.offset().bottom
                //}, 6000);

            }
            console.log(heightScroll);
            if (heightScroll > 310) {
                console.log(2);
                heightScroll = 0;
                $('#scroller').animate({ scrollTop: 0 }, 8000);

                //$('#scroller').animate({
                //    scrollTop: target.offset().top,
                //    scrollBottom: target.offset().bottom
                //}, 6000);

            }
        }

        window.setInterval(function () {
            var url = window.location.href.split('?')[0];

            window.location.href = url;
        }, 60000);

        var templateRow = '<tr> <td class="colmn-00-style">'
        + '#Name#</td> <td class="colmn-01-style">#NoSales#'
                             + '</td><td class="colmn-02-style">#dailyComm#</td> '
                           + '<td class="colmn-03-style">#monComm#'
                            + '</td><td class="colmn-05-style-red">#ReqDaily#'
                            + '</td><td class="colmn-04-style">#Budget#'
                            + '</td> <td class="colmn-06-style">#NewAcc#'
                             + '</td><td class="colmn-06-style">#daiyVolume#'
                             + '</td> <td class="colmn-08-style">#monthVolume#'
                              + '</td> <td class="colmn-06-style">#cscount#'
                              + '</td> <td class="colmn-06-style">#callcount#'
                             + '</td> </tr>';

        var rowReorders = '<tr> <td class="colmn-00-style" ><FONT COLOR="blue">REORDERS</FONT>'
        + '</td> <td class="colmn-01-style">'
                             + '</td><td class="colmn-02-style"></td> '
                           + '<td class="colmn-03-style">'
                            + '</td><td class="colmn-05-style-red">'
                            + '</td><td class="colmn-04-style">'
                            + '</td> <td class="colmn-06-style">'
                             + '</td><td class="colmn-06-style">'
                             + '</td> <td class="colmn-08-style">'
                             + '</td> <td class="colmn-06-style">'
                              + '</td> <td class="colmn-06-style">'
                             + '</td> </tr>';
        var rowcoldies = '<tr> <td class="colmn-00-style"> <FONT COLOR="blue">COLDIES</FONT>'
        + '</td> <td class="colmn-01-style">'
                             + '</td><td class="colmn-02-style"></td> '
                           + '<td class="colmn-03-style">'
                            + '</td><td class="colmn-05-style-red">'
                            + '</td><td class="colmn-04-style">'
                            + '</td> <td class="colmn-06-style">'
                             + '</td><td class="colmn-07-style">'
                             + '</td> <td class="colmn-08-style">'
                             + '</td> <td class="colmn-06-style">'
                              + '</td> <td class="colmn-06-style">'
                             + '</td> </tr>';
        var rowManagers = '<tr> <td class="colmn-00-style"><FONT COLOR="blue">MANAGERS</FONT>'
        + '</td> <td class="colmn-01-style">'
                             + '</td><td class="colmn-02-style"></td> '
                           + '<td class="colmn-03-style">'
                            + '</td><td class="colmn-05-style-red">'
                            + '</td><td class="colmn-04-style">'
                            + '</td> <td class="colmn-06-style">'
                             + '</td><td class="colmn-07-style">'
                             + '</td> <td class="colmn-08-style">'
                             + '</td> <td class="colmn-06-style">'
                              + '</td> <td class="colmn-06-style">'
                             + '</td> </tr>';

        var rowReordersTotal = '<tr> <td class="colmn-00-style"><FONT COLOR="#657383">Total</FONT>'
        + '</td> <td class="colmn-01-style"> #NoSales#'
                             + '</td><td class="colmn-02-style">$ #dailyComm#</td> '
                           + '<td class="colmn-03-style">$ #monComm#'
                            + '</td><td class="colmn-05-style-red">$ #ReqDaily#'
                            + '</td><td class="colmn-04-style">$ #Budget#'
                            + '</td> <td class="colmn-06-style">#NewAcc#'
                             + '</td><td class="colmn-07-style">$#daiyVolume#'
                             + '</td> <td class="colmn-08-style">$ #monthVolume#'
                              + '</td> <td class="colmn-06-style">#cscount#'
                               + '</td> <td class="colmn-06-style">#callcount#'
                             + '</td> </tr>';
        var rowcoldiesTotal = '<tr> <td class="colmn-00-style"><FONT COLOR="#657383">Total</FONT>'
        + '</td> <td class="colmn-01-style"> #NoSales#'
                             + '</td><td class="colmn-02-style">$ #dailyComm#</td> '
                           + '<td class="colmn-03-style">#monComm#'
                            + '</td><td class="colmn-05-style-red">$ #ReqDaily#'
                            + '</td><td class="colmn-04-style">$ #Budget#'
                            + '</td> <td class="colmn-06-style">#NewAcc#'
                             + '</td><td class="colmn-07-style">$ #daiyVolume#'
                             + '</td> <td class="colmn-08-style">$ #monthVolume#'
                              + '</td> <td class="colmn-06-style">#cscount#'
                              + '</td> <td class="colmn-06-style">#callcount#'
                             + '</td> </tr>';

        var rowManagersTotal = '<tr> <td class="colmn-00-style"><FONT COLOR="#657383">Total</FONT>'
        + '</td> <td class="colmn-01-style"> #NoSales#'
                             + '</td><td class="colmn-02-style">$ #dailyComm#</td> '
                           + '<td class="colmn-03-style">$ #monComm#'
                            + '</td><td class="colmn-05-style-red">$ #ReqDaily#'
                            + '</td><td class="colmn-04-style">$ #Budget#'
                            + '</td> <td class="colmn-06-style">#NewAcc#'
                             + '</td><td class="colmn-07-style">$ #daiyVolume#'
                             + '</td> <td class="colmn-08-style">$ #monthVolume#'
                              + '</td> <td class="colmn-06-style">#cscount#'
                              + '</td> <td class="colmn-06-style">#callcount#'
                             + '</td> </tr>';

        var isReorderHeaderAdded = false;
        var totalReorders = 0.0;
        var totalManagers = 0.0;
        var totalcoldeis = 0.0;

        var totalSalesReorder = 0;
        var totalDailyCommReorder = 0.0;
        var totalMonComReorder = 0.0;
        var totalRequiredDailyReorder = 0.00;
        var totalBudgetReorder = 0.0;
        var totalNewAcctReorder = 0.0;
        var totaldailyVolReorder = 0.0;
        var totalMonVolReorder = 0.0;

        var totalSalesColdies = 0;
        var totalDailyCommColdies = 0.0;
        var totalMonComColdies = 0.0;
        var totalRequiredDailyColdies = 0.0;
        var totalBudgetColdies = 0.0;
        var totalNewAcctColdies = 0.0;
        var totaldailyVolColdies = 0.0;
        var totalMonVolColdies = 0.0;

        var totalSalesManager = 0;
        var totalDailyCommManager = 0.0;
        var totalMonComManager = 0.0;
        var totalRequiredDailyManager = 0.0;
        var totalBudgetManager = 0.0;
        var totalNewAcctManager = 0.0;
        var totaldailyVolManager = 0.0;
        var totalMonVolManager = 0.0;

        var totalcsColdies = 0;
        var totalcsreorders = 0;
        var totalcsmanagers = 0;

        var totalcallColdies = 0;
        var totalcallreorders = 0;
        var totalcallmanagers = 0;


        

        $(document).ready(function () {
            var someVarName = "value";
            localStorage.setItem("someVarName", someVarName);
            manageTargetDisplay();
            callStatsData();

            $("#searchtd").hide();

            //$.ajax({
            //    url: 'fetch/getStatsTotals.aspx',
            //    success: function (values) {
            //        var result = values.split('|');
            //        $('#divnosales').html(result[0]);
            //        $('#divdailycomm').html("$ " + result[1]);
            //        $('#divmonthcomm').html("$ " + result[2]);
            //        $('#divreqdaily').html(result[3]);
            //        $('#divbudget').html("$ " + result[4]);
            //        $('#divnewaccounts').html(result[5]);
            //        $('#divdailyvol').html("$ " + result[6]);
            //        $('#divmonthvol').html("$ " + result[7]);
            //    }
            //});

        });

        function showFireWorks() {
            setInterval(function () { $(".pyro").show(); }, 3000);

        }

        function manageTargetDisplay() {


            $.ajax({

                url: "fetch/FetchTargetConfig.aspx",

                success: function (data) {
                    if (data) {
                        console.log(data);
                         $("#displaytargettd").show();
                        var splitData = data.split(',');
                        var title = splitData[0];
                        var amount = splitData[1];
                        var dailyVolume = splitData[2];
                        var targetID = splitData[3];
                        var reachded = splitData[4];
                        $("#configtargetTitle").text(title);
                        var calNeeded = parseFloat(amount) - parseFloat(dailyVolume);
                        $("#configamount").text(calNeeded.toFixed(0));
                          if (parseFloat(dailyVolume) > parseFloat(amount)) {
                        if (reachded != "True") {
                            $(".pyro").show();
                            updatestatusTarget(targetID);
                        }
                        }
                    }
                    else {
                        $("#displaytargettd").hide();
                        $(".pyro").hide();
                    }

                },
                error: function (xhr, err) {
                    console.log("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                    console.log("responseText: " + xhr.responseText);
                },
            });
        }

        function updatestatusTarget(targetId) {
            $.ajax({
                url: "fetch/updatestatusTarget.aspx",
                data: {
                    configId: targetId
                },
                success: function (data) {

                },
                error: function (xhr, err) {
                    console.log("responseText: " + xhr.responseText);
                },
            });
        }

        window.onbeforeunload = function () {
            // alert("callig");

            localStorage.setItem("stDate", $("#ContentPlaceHolder1_StartDateSTxt").val());
            localStorage.setItem("enDate", $("#ContentPlaceHolder1_EndDateSTxt").val());

        }

        window.onload = function () {

            var name = localStorage.getItem("stDate");
            console.log(name);
            if (name !== null)
                console.log(name);

            // ...
        }

        function callStatsData() {
            $.ajax({
                url: 'fetch/getStats.aspx',
                data: {
                    stDate: $("#ContentPlaceHolder1_StartDateSTxt").val(),
                    enDate: $("#ContentPlaceHolder1_EndDateSTxt").val(),
                },
                success: function (data) {
                    var result = data.split('~');
                    result.sort();

                    $.each(result, function (index, value) {
                        var splitdata = result[index].split('|');

                        if (splitdata[1] == "1") {
                            //  row = row + templateThReOrders;
                            var newtablerow = templateRow;
                            newtablerow = newtablerow.replace("#Name#", splitdata[0]);
                            newtablerow = newtablerow.replace("#NoSales#", splitdata[2]);
                            newtablerow = newtablerow.replace("#dailyComm#", splitdata[3]);
                            newtablerow = newtablerow.replace("#monComm#", splitdata[4]);
                            newtablerow = newtablerow.replace("#ReqDaily#", splitdata[5]);
                            newtablerow = newtablerow.replace("#Budget#", splitdata[6]);
                            newtablerow = newtablerow.replace("#NewAcc#", splitdata[7]);
                            newtablerow = newtablerow.replace("#daiyVolume#", splitdata[8]);
                            newtablerow = newtablerow.replace("#monthVolume#", splitdata[9]);
                            newtablerow = newtablerow.replace("#cscount#", splitdata[10]);
                            newtablerow = newtablerow.replace("#callcount#", splitdata[11]);
                            rowManagers = rowManagers + newtablerow;

                            totalSalesManager = totalSalesManager + parseInt(splitdata[2]);
                            totalDailyCommManager = parseFloat(totalDailyCommManager) + parseFloat(splitdata[3].replace('$ ', ''));
                            totalDailyCommManager = totalDailyCommManager.toFixed(2);
                            totalMonComManager = parseFloat(totalMonComManager) + parseFloat(splitdata[4].replace('$ ', ''));
                            totalMonComManager = totalMonComManager.toFixed(2);
                            totalRequiredDailyManager = parseFloat(totalRequiredDailyManager) + parseFloat(splitdata[5].replace('$ ', ''));
                            totalRequiredDailyManager = totalRequiredDailyManager.toFixed(2);
                            totalBudgetManager = parseFloat(totalBudgetManager) + parseFloat(splitdata[6].replace('$ ', ''));
                            totalBudgetManager = totalBudgetManager.toFixed(2);
                            totalNewAcctManager = totalNewAcctManager + parseInt(splitdata[7]);
                            totaldailyVolManager = parseFloat(totaldailyVolManager) + parseFloat(splitdata[8].replace('$ ', ''));
                            totaldailyVolManager = totaldailyVolManager.toFixed(2);
                            totalMonVolManager = parseFloat(totalMonVolManager) + parseFloat(splitdata[9].replace('$ ', ''));
                            totalMonVolManager = totalMonVolManager.toFixed(2);

                            totalcsmanagers = totalcsmanagers + parseInt(splitdata[10]);
                            totalcallmanagers = totalcallmanagers + parseInt(splitdata[11]);

                        }

                        if (splitdata[1] == "2") {

                            var newtablerowOrder = templateRow;
                            newtablerowOrder = newtablerowOrder.replace("#Name#", splitdata[0]);
                            newtablerowOrder = newtablerowOrder.replace("#NoSales#", splitdata[2]);
                            newtablerowOrder = newtablerowOrder.replace("#dailyComm#", splitdata[3]);
                            newtablerowOrder = newtablerowOrder.replace("#monComm#", splitdata[4]);
                            newtablerowOrder = newtablerowOrder.replace("#ReqDaily#", splitdata[5]);
                            newtablerowOrder = newtablerowOrder.replace("#Budget#", splitdata[6]);
                            newtablerowOrder = newtablerowOrder.replace("#NewAcc#", splitdata[7]);
                            newtablerowOrder = newtablerowOrder.replace("#daiyVolume#", splitdata[8]);
                            newtablerowOrder = newtablerowOrder.replace("#monthVolume#", splitdata[9]);
                            newtablerowOrder = newtablerowOrder.replace("#cscount#", splitdata[10]);
                            newtablerowOrder = newtablerowOrder.replace("#callcount#", splitdata[11]);

                            rowReorders = rowReorders + newtablerowOrder;

                            totalSalesReorder = totalSalesReorder + parseInt(splitdata[2]);
                            totalDailyCommReorder = parseFloat(totalDailyCommReorder) + parseFloat(splitdata[3].replace('$ ', ''));
                            totalDailyCommReorder = totalDailyCommReorder.toFixed(2);
                            totalMonComReorder = parseFloat(totalMonComReorder) + parseFloat(splitdata[4].replace('$ ', ''));
                            totalMonComReorder = totalMonComReorder.toFixed(2);

                            totalRequiredDailyReorder = parseFloat(totalRequiredDailyReorder) + parseFloat(splitdata[5].replace('$ ', ''));
                            totalRequiredDailyReorder = totalRequiredDailyReorder.toFixed(2);
                            totalBudgetReorder = parseFloat(totalBudgetReorder) + parseFloat(splitdata[6].replace('$ ', ''));
                            totalBudgetReorder = totalBudgetReorder.toFixed(2);
                            totalNewAcctReorder = (totalNewAcctReorder) + parseInt(splitdata[7]);

                            totaldailyVolReorder = parseFloat(totaldailyVolReorder) + parseFloat(splitdata[8].replace('$ ', ''));
                            totaldailyVolReorder = totaldailyVolReorder.toFixed(2);
                            totalMonVolReorder = parseFloat(totalMonVolReorder) + parseFloat(splitdata[9].replace('$ ', ''));
                            totalMonVolReorder = totalMonVolReorder.toFixed(2);

                            totalcsreorders = totalcsreorders + parseInt(splitdata[10]);
                            totalcallreorders = totalcallreorders + parseInt(splitdata[11]);

                        }

                        if (splitdata[1] == "3") {

                            var newtablerow = templateRow;
                            newtablerow = newtablerow.replace("#Name#", splitdata[0]);
                            newtablerow = newtablerow.replace("#NoSales#", splitdata[2]);
                            newtablerow = newtablerow.replace("#dailyComm#", splitdata[3]);
                            newtablerow = newtablerow.replace("#monComm#", splitdata[4]);
                            newtablerow = newtablerow.replace("#ReqDaily#", splitdata[5]);
                            newtablerow = newtablerow.replace("#Budget#", splitdata[6]);
                            newtablerow = newtablerow.replace("#NewAcc#", splitdata[7]);
                            newtablerow = newtablerow.replace("#daiyVolume#", splitdata[8]);
                            newtablerow = newtablerow.replace("#monthVolume#", splitdata[9]);
                            newtablerow = newtablerow.replace("#cscount#", splitdata[10]);
                            newtablerow = newtablerow.replace("#callcount#", splitdata[11]);
                            rowcoldies = rowcoldies + newtablerow;

                            totalSalesColdies = totalSalesColdies + parseInt(splitdata[2]);
                            totalDailyCommColdies = parseFloat(totalDailyCommColdies) + parseFloat(splitdata[3].replace('$ ', ''));
                            totalDailyCommColdies = totalDailyCommColdies.toFixed(2);
                            totalMonComColdies = parseFloat(totalMonComColdies) + parseFloat(splitdata[4].replace('$ ', ''));
                            totalMonComColdies = totalMonComColdies.toFixed(2);
                            totalRequiredDailyColdies = parseFloat(totalRequiredDailyColdies) + parseFloat(splitdata[5].replace('$ ', ''));
                            totalRequiredDailyColdies = totalRequiredDailyColdies.toFixed(2);
                            totalBudgetColdies = parseFloat(totalBudgetColdies) + parseFloat(splitdata[6].replace('$ ', ''));
                            totalBudgetColdies = totalBudgetColdies.toFixed(2);
                            totalNewAcctColdies = (totalNewAcctColdies) + parseInt(splitdata[7]);

                            totaldailyVolColdies = parseFloat(totaldailyVolColdies) + parseFloat(splitdata[8].replace('$ ', ''));
                            totaldailyVolColdies = totaldailyVolColdies.toFixed(2);
                            totalMonVolColdies = parseFloat(totalMonVolColdies) + parseFloat(splitdata[9].replace('$ ', ''));
                            totalMonVolColdies = totalMonVolColdies.toFixed(2);

                            totalcsColdies = totalcsColdies + parseInt(splitdata[10]);
                            totalcallColdies = totalcallColdies + parseInt(splitdata[11]);

                        }

                    });

                    rowReordersTotal = rowReordersTotal.replace("#NoSales#", totalSalesReorder);
                    rowReordersTotal = rowReordersTotal.replace("#dailyComm#", totalDailyCommReorder);
                    rowReordersTotal = rowReordersTotal.replace("#monComm#", totalMonComReorder);
                    rowReordersTotal = rowReordersTotal.replace("#ReqDaily#", totalRequiredDailyReorder);
                    rowReordersTotal = rowReordersTotal.replace("#Budget#", totalBudgetReorder);
                    rowReordersTotal = rowReordersTotal.replace("#NewAcc#", totalNewAcctReorder);
                    rowReordersTotal = rowReordersTotal.replace("#daiyVolume#", totaldailyVolReorder);
                    rowReordersTotal = rowReordersTotal.replace("#monthVolume#", totalMonVolReorder);
                    rowReordersTotal = rowReordersTotal.replace("#cscount#", totalcsreorders);
                    rowReordersTotal = rowReordersTotal.replace("#callcount#", totalcallreorders);

                    rowcoldiesTotal = rowcoldiesTotal.replace("#NoSales#", totalSalesColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#dailyComm#", totalDailyCommColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#monComm#", totalMonComColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#ReqDaily#", totalRequiredDailyColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#Budget#", totalBudgetColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#NewAcc#", totalNewAcctColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#daiyVolume#", totaldailyVolColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#monthVolume#", totalMonVolColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#cscount#", totalcsColdies);
                    rowcoldiesTotal = rowcoldiesTotal.replace("#callcount#", totalcallColdies);

                    rowManagersTotal = rowManagersTotal.replace("#NoSales#", totalSalesManager);
                    rowManagersTotal = rowManagersTotal.replace("#dailyComm#", totalDailyCommManager);
                    rowManagersTotal = rowManagersTotal.replace("#monComm#", totalMonComManager);
                    rowManagersTotal = rowManagersTotal.replace("#ReqDaily#", totalRequiredDailyManager);
                    rowManagersTotal = rowManagersTotal.replace("#Budget#", totalBudgetManager);
                    rowManagersTotal = rowManagersTotal.replace("#NewAcc#", totalNewAcctManager);
                    rowManagersTotal = rowManagersTotal.replace("#daiyVolume#", totaldailyVolManager);
                    rowManagersTotal = rowManagersTotal.replace("#monthVolume#", totalMonVolManager);
                    rowManagersTotal = rowManagersTotal.replace("#cscount#", totalcsmanagers);
                    rowManagersTotal = rowManagersTotal.replace("#callcount#", totalcallmanagers);

                    var granttotalSales = totalSalesReorder + totalSalesColdies + totalSalesManager;
                    var granttotalDailyComm = parseFloat(totalDailyCommReorder) + parseFloat(totalDailyCommColdies) + parseFloat(totalDailyCommManager);
                    var granttotalMonCom = parseFloat(totalMonComReorder) + parseFloat(totalMonComColdies) + parseFloat(totalSalesManager);
                    var granttotalRequiredDaily = parseFloat(totalRequiredDailyReorder) + parseFloat(totalRequiredDailyColdies) + parseFloat(totalRequiredDailyManager);
                    var granttotalBudget = parseFloat(totalBudgetReorder) + parseFloat(totalBudgetColdies) + parseFloat(totalBudgetManager);
                    var granttotalNewAcct = totalNewAcctReorder + totalNewAcctColdies + totalNewAcctManager;
                    var granttotaldailyVol = parseFloat(totaldailyVolReorder) + parseFloat(totaldailyVolColdies) + parseFloat(totaldailyVolManager);
                    var granttotalMonVol = parseFloat(totalMonVolReorder) + parseFloat(totalMonVolColdies) + parseFloat(totalMonVolManager);

                    var csGrantTottal = totalcsmanagers + totalcsColdies + totalcsreorders;

                    var callGrantTottal = totalcallmanagers + totalcallColdies + totalcallreorders;

                    rowReorders = rowReorders + rowReordersTotal;
                    rowcoldies = rowcoldies + rowcoldiesTotal;
                    rowManagers = rowManagers + rowManagersTotal;

                    $("#stattable").append(rowReorders);
                    $("#stattable").append(rowcoldies);
                    $("#stattable").append(rowManagers);

                    $('#divnosales').html(granttotalSales);
                    $('#divdailycomm').html("$ " + granttotalDailyComm.toFixed(2));
                    $('#divmonthcomm').html("$ " + granttotalMonCom.toFixed(2));
                    $('#divreqdaily').html("$ " + granttotalRequiredDaily.toFixed(2));
                    $('#divbudget').html("$ " + granttotalBudget.toFixed(2));
                    $('#divnewaccounts').html(granttotalNewAcct);
                    $('#divdailyvol').html("$ " + granttotaldailyVol.toFixed(2));
                    $('#divmonthvol').html("$ " + granttotalMonVol.toFixed(2));
                    $('#divcscount').html(csGrantTottal);
                    $('#divcallcount').html(callGrantTottal);

                },
                error: function (error) {
                    alert("An error occured " + error);
                }

            });

        }

        function myFunction() {
            alert("test");
        }

        function senddata() {
            $("#ContentPlaceHolder1_checkClick").val("true");
            var url = window.location.href.split('?')[0];
            var sDates = $("#ContentPlaceHolder1_StartDateSTxt").val()
            var enDates = $("#ContentPlaceHolder1_EndDateSTxt").val();
            window.location.href = url + "?sDates=" + sDates + "&eDates=" + enDates;
            // window.location.reload();
        }

        function resetAlll() {
            var url = window.location.href.split('?')[0];
            window.location.href = url + "?res=s";
        }

        function printMe() {
            var divElements = document.getElementById("scroller").innerHTML;
            var divtotal = document.getElementById("totalDiv").innerHTML;
            var headertalbe = document.getElementById("alltabledata").innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;
            var joinElemn = headertalbe + divElements + divtotal;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              joinElemn + "</body>";

            //Print Page
            window.print();

            //Restore orignal HTML
            document.body.innerHTML = oldPage;

            // window.print();
        }
    </script>

    <link href='https://fonts.googleapis.com/css?family=Raleway:400,100,100italic,200,200italic,300,300italic,400italic,500,500italic,600,600italic,700,700italic,800,800italic,900,900italic' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Ubuntu:400,300,300italic,400italic,500,500italic,700,700italic' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Varela+Round' rel='stylesheet' type='text/css' />

    <style type="text/css">
        .scroller{
            scroll-behavior: smooth;
        }

        body {
  margin: 0;
  padding: 0;
  background: #000;
  overflow: hidden; }
        div.vertical {
            margin-left: -85px;
            position: absolute;
            width: 215px;
            transform: rotate(-90deg);
            -webkit-transform: rotate(-90deg); /* Safari/Chrome */
            -moz-transform: rotate(-90deg); /* Firefox */
            -o-transform: rotate(-90deg); /* Opera */
            -ms-transform: rotate(-90deg); /* IE 9 */
        }

        th.vertical {
            max-width: 50px;
            height: 85px;
            line-height: 14px;
            padding-bottom: 20px;
            text-align: inherit;
        }

        .ctr {
            margin-left: 5%;
            /*background-color: aqua;*/
            margin-right: 5%;
        }

        .screen-heading {
            font-size: 20px !important;
            color: #666666;
        }

        .heading-01-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 10%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-top: 10px;
            padding-left: 25px;
            padding-bottom: 10px;
            color: #666;
            font-weight: 400;
            height: 30px;
        }

        .heading-02-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 5%;
            background-color: #fff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .heading-03-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 7%;
            background-color: #fff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .heading-04-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 7%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .heading-05-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 7%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .heading-05-style-red {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 7%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #eb473d;
            font-weight: 400;
            height: 30px;
        }

        .heading-06-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 5%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .heading-06-styleTwo {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 5%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .heading-07-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 7%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .heading-07-styleTwo {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 6%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .heading-08-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 7%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            margin-left: 8px;
            font-weight: 400;
            height: 30px;
        }

        .heading-09-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 8%;
            background-color: #ffffff;
            border-bottom-color: #ccc;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333333;
            font-weight: 400;
            height: 30px;
        }

        .spacer-01-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 10px;
            background-color: #f5f5f5;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            border-bottom-width: 5px;
            text-align: left;
            color: #333333;
            font-weight: 400;
        }

        .colmn-00-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 11%;
            background-color: #ffffff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            padding-left: 25px;
            text-align: left;
            color: #333333;
            height: 26px;
        }

        .colmn-01-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 5%;
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333;
        }

        .colmn-02-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 7%;
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333;
        }

        .colmn-03-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 7%;
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333;
        }

        .colmn-04-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 8%;
            /*background-color: #a7a900;*/
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333;
        }

        .colmn-05-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 9%;
            /*background-color: #efa204;*/
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333;
        }

        .colmn-05-style-red {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 8%;
            /*background-color: #efa204;*/
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #eb473d;
            font-weight: 800;
        }

        .colmn-06-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 5%;
            /*background-color: #ffa050;*/
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333;
        }

        .colmn-07-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 8%;
            /*background-color: #ff9186;*/
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333;
        }

        .colmn-08-style {
            font-family: 'Varela Round', sans-serif;
            font-size: 21px;
            width: 7%;
            /*background-color: #e592d6;*/
            background-color: #e8edff;
            border-bottom-color: #f5f5f5;
            border-bottom-style: solid;
            padding-bottom: 10px;
            padding-top: 10px;
            text-align: center;
            color: #333;
        }

        .pwddiv {
            width: 300px;
            height: 300px;
            border: 1px rgb(196, 196, 196) solid;
            border-top-color: #eb473d;
            background: #eb473d;
            position: absolute;
            top: 30px;
            right: 22%;
            z-index: 120;
        }

        .auto-style1 {
            border-bottom: 10px solid #a7a900;
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 8%;
            background-color: #ffffff;
            padding-bottom: 25px;
            padding-top: 25px;
            text-align: center;
            color: #eb473d;
            font-weight: 800;
        }

        .auto-style2 {
            border-bottom: 10px solid #63b43b;
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 8%;
            background-color: #ffffff;
            padding-bottom: 25px;
            padding-top: 25px;
            text-align: center;
            color: #333333;
            font-weight: 500;
        }

        .auto-style3 {
            border-bottom: 10px solid #07b3a3;
            font-family: 'Varela Round', sans-serif;
            font-size: 14px;
            width: 8%;
            background-color: #fff;
            padding-bottom: 25px;
            padding-top: 25px;
            text-align: center;
            color: #333333;
            font-weight: 500;
        }

        @media print {
            #scroller {
                height: auto;
                overflow-y: auto;
            }
        }


        .pyro > .before, .pyro > .after {
            position: absolute;
            width: 10px;
            height: 10px;
            border-radius: 50%;
            box-shadow: -120px -218.66667px blue, 248px -16.66667px #00ff84, 190px 16.33333px #002bff, -113px -308.66667px #ff009d, -109px -287.66667px #ffb300, -50px -313.66667px #ff006e, 226px -31.66667px #ff4000, 180px -351.66667px #ff00d0, -12px -338.66667px #00f6ff, 220px -388.66667px #99ff00, -69px -27.66667px #ff0400, -111px -339.66667px #6200ff, 155px -237.66667px #00ddff, -152px -380.66667px #00ffd0, -50px -37.66667px #00ffdd, -95px -175.66667px #a6ff00, -88px 10.33333px #0d00ff, 112px -309.66667px #005eff, 69px -415.66667px #ff00a6, 168px -100.66667px #ff004c, -244px 24.33333px #ff6600, 97px -325.66667px #ff0066, -211px -182.66667px #00ffa2, 236px -126.66667px #b700ff, 140px -196.66667px #9000ff, 125px -175.66667px #00bbff, 118px -381.66667px #ff002f, 144px -111.66667px #ffae00, 36px -78.66667px #f600ff, -63px -196.66667px #c800ff, -218px -227.66667px #d4ff00, -134px -377.66667px #ea00ff, -36px -412.66667px #ff00d4, 209px -106.66667px #00fff2, 91px -278.66667px #000dff, -22px -191.66667px #9dff00, 139px -392.66667px #a6ff00, 56px -2.66667px #0099ff, -156px -276.66667px #ea00ff, -163px -233.66667px #00fffb, -238px -346.66667px #00ff73, 62px -363.66667px #0088ff, 244px -170.66667px #0062ff, 224px -142.66667px #b300ff, 141px -208.66667px #9000ff, 211px -285.66667px #ff6600, 181px -128.66667px #1e00ff, 90px -123.66667px #c800ff, 189px 70.33333px #00ffc8, -18px -383.66667px #00ff33, 100px -6.66667px #ff008c;
            -moz-animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards;
            -webkit-animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards;
            -o-animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards;
            -ms-animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards;
            animation: 1s bang ease-out infinite backwards, 1s gravity ease-in infinite backwards, 5s position linear infinite backwards;
        }

        .pyro > .after {
            -moz-animation-delay: 1.25s, 1.25s, 1.25s;
            -webkit-animation-delay: 1.25s, 1.25s, 1.25s;
            -o-animation-delay: 1.25s, 1.25s, 1.25s;
            -ms-animation-delay: 1.25s, 1.25s, 1.25s;
            animation-delay: 1.25s, 1.25s, 1.25s;
            -moz-animation-duration: 1.25s, 1.25s, 6.25s;
            -webkit-animation-duration: 1.25s, 1.25s, 6.25s;
            -o-animation-duration: 1.25s, 1.25s, 6.25s;
            -ms-animation-duration: 1.25s, 1.25s, 6.25s;
            animation-duration: 1.25s, 1.25s, 6.25s;
        }

        @-webkit-keyframes bang {
            from {
                box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;
            }
        }

        @-moz-keyframes bang {
            from {
                box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;
            }
        }

        @-o-keyframes bang {
            from {
                box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;
            }
        }

        @-ms-keyframes bang {
            from {
                box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;
            }
        }

        @keyframes bang {
            from {
                box-shadow: 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white, 0 0 white;
            }
        }

        @-webkit-keyframes gravity {
            to {
                transform: translateY(200px);
                -moz-transform: translateY(200px);
                -webkit-transform: translateY(200px);
                -o-transform: translateY(200px);
                -ms-transform: translateY(200px);
                opacity: 0;
            }
        }

        @-moz-keyframes gravity {
            to {
                transform: translateY(200px);
                -moz-transform: translateY(200px);
                -webkit-transform: translateY(200px);
                -o-transform: translateY(200px);
                -ms-transform: translateY(200px);
                opacity: 0;
            }
        }

        @-o-keyframes gravity {
            to {
                transform: translateY(200px);
                -moz-transform: translateY(200px);
                -webkit-transform: translateY(200px);
                -o-transform: translateY(200px);
                -ms-transform: translateY(200px);
                opacity: 0;
            }
        }

        @-ms-keyframes gravity {
            to {
                transform: translateY(200px);
                -moz-transform: translateY(200px);
                -webkit-transform: translateY(200px);
                -o-transform: translateY(200px);
                -ms-transform: translateY(200px);
                opacity: 0;
            }
        }

        @keyframes gravity {
            to {
                transform: translateY(200px);
                -moz-transform: translateY(200px);
                -webkit-transform: translateY(200px);
                -o-transform: translateY(200px);
                -ms-transform: translateY(200px);
                opacity: 0;
            }
        }

        @-webkit-keyframes position {
            0%, 19.9% {
                margin-top: 10%;
                margin-left: 50%;
            }

            20%, 39.9% {
                margin-top: 40%;
                margin-left: 30%;
            }

            40%, 59.9% {
                margin-top: 20%;
                margin-left: 70%;
            }

            60%, 79.9% {
                margin-top: 30%;
                margin-left: 20%;
            }

            80%, 99.9% {
                margin-top: 30%;
                margin-left: 80%;
            }
        }

        @-moz-keyframes position {
            0%, 19.9% {
                margin-top: 10%;
                margin-left: 40%;
            }

            20%, 39.9% {
                margin-top: 40%;
                margin-left: 30%;
            }

            40%, 59.9% {
                margin-top: 20%;
                margin-left: 70%;
            }

            60%, 79.9% {
                margin-top: 30%;
                margin-left: 20%;
            }

            80%, 99.9% {
                margin-top: 30%;
                margin-left: 80%;
            }
        }

        @-o-keyframes position {
            0%, 19.9% {
                margin-top: 10%;
                margin-left: 40%;
            }

            20%, 39.9% {
                margin-top: 40%;
                margin-left: 30%;
            }

            40%, 59.9% {
                margin-top: 20%;
                margin-left: 70%;
            }

            60%, 79.9% {
                margin-top: 30%;
                margin-left: 20%;
            }

            80%, 99.9% {
                margin-top: 30%;
                margin-left: 80%;
            }
        }

        @-ms-keyframes position {
            0%, 19.9% {
                margin-top: 10%;
                margin-left: 40%;
            }

            20%, 39.9% {
                margin-top: 40%;
                margin-left: 30%;
            }

            40%, 59.9% {
                margin-top: 20%;
                margin-left: 70%;
            }

            60%, 79.9% {
                margin-top: 30%;
                margin-left: 20%;
            }

            80%, 99.9% {
                margin-top: 30%;
                margin-left: 80%;
            }
        }

        @keyframes position {
            0%, 19.9% {
                margin-top: 10%;
                margin-left: 40%;
            }

            20%, 39.9% {
                margin-top: 40%;
                margin-left: 30%;
            }

            40%, 59.9% {
                margin-top: 20%;
                margin-left: 70%;
            }

            60%, 79.9% {
                margin-top: 30%;
                margin-left: 20%;
            }

            80%, 99.9% {
                margin-top: 30%;
                margin-left: 80%;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="pyro" style="display: none;">
        <div class="before"></div>
        <div class="after"></div>
    </div>
    <br />
    <input type="hidden" id="checkClick" runat="server" />
    <table id="noid" cellpadding="0" cellspacing="0" width="90%" height="100%" class="ctr">
        <tr>
            <td class="screen-heading">
                <input type="button" onclick="printMe();" value="PRINT" style="width: 60px; height: 30px; border: 1.5px solid blue; background-color: white; cursor: pointer;" />
                DELTONE PERFORMANCE SUMMARY   </td>
            <td class="screen-heading">

                <span>START DATE: </span>

                <span>
                    <input id="StartDateSTxt" type="text" name="StartDateTxt" size="24" runat="server" />
                </span>

                <span>END DATE: </span>

                <span>
                    <input id="EndDateSTxt" type="text" name="EndDateTxt" size="24" runat="server" />
                </span>
                <span>
                    <input type="button" value="APPLY FILTER" onclick="return senddata();" style="width: 160px; height: 30px; border: 1.5px solid blue; background-color: white; cursor: pointer;" />
                </span>
                <span>
                    <input type="button" value="DEFAULT VIEW" onclick="return resetAlll();" style="width: 160px; height: 30px; border: 1.5px solid red; background-color: white; cursor: pointer;" />
                </span>
            </td>
        </tr>
    </table>
    <div id="alltabledata">
        <table id="noid" cellpadding="0" cellspacing="0" width="92%" height="100%" class="ctr">
            <tr>
                <td class="heading-01-style">NAME</td>
                <td class="heading-02-style">NO OF SALES</td>
                <td class="heading-03-style">DAILY COMM</td>
                <td class="heading-04-style">MONTHLY COMM</td>
                <td class="heading-05-style">REQUIRED DAILY</td>

                <td class="heading-04-style" style="margin-left: 12px !important;">BUDGET</td>
                <td class="heading-07-styleTwo">NEW ACCOUNTS</td>
                <td class="heading-07-style">DAILY VOL</td>
                <td class="heading-08-style">MONTHLY VOL</td>
                <td class="heading-06-styleTwo">CS</td>
                <td class="heading-06-style">NO OF CALLS</td>
            </tr>
        </table>
    </div>
    <div id="scroller" style="height: 500px; overflow: auto;">
        <table id="stattable" width="93%" cellpadding="0" cellspacing="0" class="ctr">
        </table>
    </div>
    <div id="totalDiv">
        <table id="totaltable" cellpadding="0" cellspacing="0" width="93%" class="ctr">
            <tr >
                <td class="colmn-00-style" style="color:green;font-weight:bold">COMPANY</td>
                <td id="divnosales" class="colmn-01-style" style="color:green;font-weight:bold">&nbsp;</td>
                <td id="divdailycomm" class="colmn-02-style"style="color:green;font-weight:bold" >&nbsp;</td>
                <td id="divmonthcomm" class="colmn-03-style" style="color:green;font-weight:bold">&nbsp;</td>
                <td id="divreqdaily" class="colmn-04-style" style="color:green;font-weight:bold">&nbsp;</td>
                <td id="divbudget" class="colmn-05-style" style="color:green;font-weight:bold">&nbsp;</td>
                <td id="divnewaccounts" class="colmn-06-style" style="color:green;font-weight:bold">&nbsp;</td>
                <td id="divdailyvol" class="colmn-07-style" style="color:green;font-weight:bold">&nbsp;</td>
                <td id="divmonthvol" class="colmn-08-style" style="color:green;font-weight:bold">&nbsp;</td>
                <td id="divcscount" class="colmn-06-style" style="color:green;font-weight:bold">&nbsp;</td>
                <td id="divcallcount" class="colmn-06-style" style="color:green;font-weight:bold">&nbsp;</td>
            </tr>
        </table>
    </div>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

      var dateNow = new Date();
      $("#ContentPlaceHolder1_StartDateSTxt").datepicker({ dateFormat: 'dd-mm-yy' });
      $("#ContentPlaceHolder1_EndDateSTxt").datepicker({ dateFormat: 'dd-mm-yy' });

  });
                  </script>
</asp:Content>
