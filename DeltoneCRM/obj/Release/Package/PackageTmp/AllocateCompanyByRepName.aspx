<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NoNav.Master" CodeBehind="AllocateCompanyByRepName.aspx.cs" Inherits="DeltoneCRM.AllocateCompanyByRepName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>

    <link href="css/NewCSS.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.js"></script>

    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.dataTables_new.css" />

    <%-- <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jscolor.js"></script>
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
  <%--  <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>--%>
    <script src="js/datatableup/datatables.min.js"></script>
    <%--  <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
	</script>--%>
    <%--	<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>--%>
 <%--   <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>--%>
   <%-- <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js">
    </script>--%>
    <script src="js/datatableup/dataTables.buttons.min.js"></script>
    <script src="js/datatableup/buttons.print.min.js"></script>

    <title>Company List</title>

    <style type='text/css'>
        /* css for timepicker */
        td.details-control {
            background: url('Images/details_open.png') no-repeat center center;
            cursor: pointer;
        }

        tr.shown td.details-control {
            background: url('Images/details_close.png') no-repeat center center;
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

        #livedbtbl tr:hover {
          background-color: #ffff99;
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
            max-width: 100%;
            margin: 0 auto;
            width: 90%;
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
        }

        .undoButtonStyle {
            margin-left: 30px;
        }

        .innertableta tr {
            background-color: #BEBEBE;
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
        }

        .inputTextStyle {
            text-align: center;
            color: #4fc2bd !important;
        }
        #tooltip {
  position: absolute;
  z-index: 1001;
  display: none;
  border: 2px solid #ebebeb;
  border-radius: 5px;
  padding: 10px;
  background-color: #fff;
  height:80px;
  max-height:300px;
  color:blue;
  font-size:14px;
  word-wrap: break-word;
  -webkit-border-radius: 5px; 
    -moz-border-radius: 5px; 
    border-radius: 5px;
}

        /*#tooltip:after {
	content: '';
	display: block;  
	position: absolute;
	right: 100%;
	top: 50%;
	margin-top: -10px;
	width: 0;
	height: 0;
     background-color: #fff;
	border-top: 10px solid transparent;
	border-right: 10px solid grey;
	border-bottom: 10px solid transparent;
	border-left: 10px solid transparent;
}*/

        td.highlight {
            font-weight: bold;
            color: blue !important;
        }

          tr.highlight {
            font-weight: bold;
            color: blue !important;
        }
    </style>

    <script type="text/javascript">
        var table;
        var Quotetable;
        var sort = "desc";
        var colNu = 4;
        var pageClicked = 0;
        $(document).ready(function () {

            callMe();
            repNameChange();

            $('#sortinputdrop').change(
    function () {

        callSortByType();
    });

            $('#sortinputdropdir').change(
    function () {

        callSortByType();
    });

            $('#livedbtbl tbody').on('click', 'td.details-control', function () {
                var tr = $(this).closest('tr');
                var row = table.row(tr);

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    // Open this row

                    row.child(format(row.data()[13])).show();
                    tr.addClass('shown');
                }
            });

            $('#livedbtbl').on('mousemove', 'td.notestest', function (e) {
               // console.log($(this));

                var tr = $(this).closest('tr');
                var row = table.row(tr);
              // var td (this).find('td:first').show()
                var rowData = row.data()[6];
                if (rowData) {
                    $("#tooltip").text(rowData).animate({ left: e.pageX, top: e.pageY }, 1);
                    if (!$("#tooltip").is(':visible')) {
                        $("#tooltip").show();
                    }
                }
            })
            $('#livedbtbl').on('mouseleave', 'td.notestest', function (e) {
                $("#tooltip").hide();
            });


            $('#livedbtbl').on('page.dt', function () {
                var info = table.page.info();
                pageClicked = info.page;
                console.log(info.page);
                // if (info.page==0)
                //table.page(1).draw('page');
                //$('#pageInfo').html('Showing page: ' + info.page + ' of ' + info.pages);
            });

        });

        function callMe() {

            //  var se = $("#RepNameDropDownList").val();
            var re = $("#RepNameDropDownList").val();

            //  CreateTableData(re);

            CreateTableData(re, sort, colNu, "", "");


        }


        function CreateTableData(catValue, dirc, colNumber, startDate, endDate) {
            // $('#callbacktableDiv ').hide();
            // var catValue = "a";
            if (table) {

                table.destroy();
            }

            //var repName = localStorage.getItem("selectedRep");
            //if (repName) {

            //    $("#RepNameDropDownList").val(repName);
            //    var pageNUmber = localStorage.getItem("selectedpage");
            //    table = localStorage.getItem("selectedtableData");

            //    //$('#livedbtbl').DataTable({
            //    //    data: table
            //    //});
            //    console.log(table);
            //    table.page(pageNUmber).draw('page');
            //}
            //else 
            {

                table = $('#livedbtbl').DataTable({
                    processing: true,
                    "deferRender": true,
                    "language": {
                        "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response...</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                    },
                    //stateSave: true,
                    //stateSaveCallback: function(settings,data) {
                    //    localStorage.setItem( 'DataTables_' + settings.sInstance, JSON.stringify(data) )
                    //},
                    //stateLoadCallback: function (settings) {
                    //    console.log('in sss stateLoadCallback');
                    //    return JSON.parse( localStorage.getItem( 'DataTables_' + settings.sInstance ) )
                    //},
                    "ajax": "../Fetch/FetchCompanyForRepAllocation.aspx?n=1",
                    "createdRow": function (row, data, index) {

                        //  console.log(row );
                        if (data[9] == "Y") {
                            // $('tr', row).addClass('highlight');
                            $('td', row).eq(7).addClass('highlight');
                            $('td', row).eq(2).addClass('highlight');

                            $('td', row).eq(3).addClass('highlight');
                            $('td', row).eq(4).addClass('highlight');
                            $('td', row).eq(6).addClass('highlight');
                            $('td', row).eq(8).addClass('highlight');
                        }
                        // $(row).css('background-color', 'Orange !important');

                    },
                    columns: [
                       {
                           "className": 'details-control',
                           "orderable": false,
                           "data": null,
                           "defaultContent": ''
                       },

                    ],
                    "columnDefs": [
                            {
                                className: 'align_left', "targets": [0, 1, 2, 3, 4, 5, 6,7,8,9],
                                className: 'notestest', "targets": [ 5],
                            },

                            { "width": "3%", "targets": 0 },
                    { "width": "2%", "targets": 1 },
                     { "width": "2%", "targets": 2 },
                     { "width": "8%", "targets": 5 },
                     { "visible": false, "targets": 13 },
                    { "visible": false, "targets": 6 }

                    ],
                    "aaSorting": [[colNumber, dirc]],
                    "iDisplayLength": 100,
                    "fnServerParams": function (aoData) {
                        aoData.push(
                        {
                            "name": "rep", "value": catValue

                        }, {
                            "name": "startdate", "value": startDate
                        },
                        {
                            "name": "enddate", "value": endDate
                        }
                        );
                    },
                    dom: 'lBfrtip',
                    buttons: [
                        'print'
                    ]

                });
            }
        }

        function repNameChange() {

            $('#RepNameDropDownList').change(
    function () {

        sort = "desc";
        colNu = 4;
        $("#sortinputdropdir").val(sort);
        $("#sortinputdrop").val("dateorder");
        localStorage.removeItem('selectedRep');
        callMe();
    });
        }

        function ViewCompany(comId) {
            // localStorage.setItem("selectedRep", $('#RepNameDropDownList').val());
            // localStorage.setItem("selectedpage", pageClicked);
            //  localStorage.setItem("selectedtableData", table);
            window.open('ConpanyInfo.aspx?companyid=' + comId + "&lP=1", "blank");
        }


        function format(d) {
            // `d` is the original data object for the row   

            if (d) {

                var orderSplit = d.split(';');

                var finalTr = "";
                for (var i = 0; i < orderSplit.length ; i++) {
                    var orderDetyails = orderSplit[i].split(',');

                    var temTr = '<tr class="bgnnercolor">' +
                          '<td>Ordered Date:</td>' +
                          '<td>' + orderDetyails[0] + '</td>' +

                          '<td>Order Number:</td>' +
                          '<td>' + orderDetyails[2] + '</td>' +

                          '<td>Total:</td>' +
                          '<td>' + parseFloat(orderDetyails[1]).toFixed(2) + '</td>' +
                      '</tr>';

                    finalTr = finalTr + temTr;
                }
            }

            if (finalTr) {

            }
            else {

                finalTr = '<tr class="bgnnercolor">' +
                                          '<td>No Order Found</td>' +
                                      '</tr>';
            }

            var innerTalbeTr = '<table class="innertableta" cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' + finalTr + '</table>';

            return innerTalbeTr;
        }

        function callSortByType() {
            var sortY = $("#sortinputdropdir").val();
            var orderselect = $("#sortinputdrop").val();
            var sort = sortY;

            if (orderselect == "dateorder") {
                colNu = 4;
                table
          .order([4, sortY])
                       .draw();
            }
            else {
                colNu = 2;
                table
         .order([2, sortY])
                      .draw();
            }
        }

        function senddata() {

            var re = $("#RepNameDropDownList").val();

            //  CreateTableData(re);
            var stDate = $("#ContentPlaceHolder1_StartDateSTxt").val();
            var enDate = $("#ContentPlaceHolder1_EndDateSTxt").val();
            if (stDate != "dd/mm/yyyy" && enDate != "dd/mm/yyyy")
                CreateTableData(re, sort, colNu, stDate, enDate);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagerupdateRepQuote" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="gridDiv">
        <br />


        <asp:Label ID="messagelable1" runat="server" CssClass="messagecss" Font-Size="Medium"></asp:Label>


        <%--  <asp:TextBox ID="searchTextBox" runat="server" Width="60%"></asp:TextBox>
        <asp:Button OnClick="SearchButton_Click" ID="btnsearch" Text="Search" ForeColor="Blue" Width="20%" runat="server" CausesValidation="false" />--%>
        <div id="main">
            <br />
            <br />


            <table width="1500" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

                <tr>
                    <asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false" />
                    <asp:Button OnClick="btnupload_Click" ID="Button1" Text="Dashboard" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />


                </tr>
                <tr>
                    <td height="25px"></td>
                </tr>
                <tr>
                    <td height="25px">
                        <span style="float: left; width: 200px;">REP NAME :</span>
                        <asp:DropDownList ID="RepNameDropDownList" ClientIDMode="Static" runat="server">
                            <%--  <asp:ListItem Text="Dim" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Aidan" Value="17"></asp:ListItem>
                            <asp:ListItem Text="Bailey" Value="14"></asp:ListItem>
                            <asp:ListItem Text="John" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Krit" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Taras" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Trent" Value="15"></asp:ListItem>
                            <asp:ListItem Text="William" Value="18"></asp:ListItem>
                            <asp:ListItem Text="TestOne" Value="24"></asp:ListItem>--%>
                        </asp:DropDownList>


                    </td>
                </tr>
                <tr>
                    <td height="25px">


                        <span style="float: left; width: 200px;">ALLOCATION MODE:</span>

                        <asp:DropDownList ID="AllocationDropDownList" ClientIDMode="Static" runat="server">
                            <asp:ListItem Text="None" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Random" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Manual" Value="2"></asp:ListItem>

                        </asp:DropDownList>



                        <%-- <input type="button" value="Show" onclick="callMe();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                        <%--<input type="button" value="Print" onclick="printDiv();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                    </td>
                </tr>

                <tr>
                    <td height="25px">


                        <span style="float: left; width: 200px;">ALLOCATION PERIOD:</span>

                        <asp:DropDownList ID="ALLOCATIONPERIODDropDownList" ClientIDMode="Static" runat="server">
                            <asp:ListItem Text="1 Week" Value="1"></asp:ListItem>
                            <asp:ListItem Text="2 Week" Value="2"></asp:ListItem>
                            <asp:ListItem Text="3 Week" Value="3"></asp:ListItem>
                            <asp:ListItem Text="4 Week" Value="4"></asp:ListItem>
                        </asp:DropDownList>



                        <%-- <input type="button" value="Show" onclick="callMe();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                        <%--<input type="button" value="Print" onclick="printDiv();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                    </td>
                </tr>

                <tr>
                    <td height="25px">


                        <span style="float: left; width: 200px;">NUMBER OF COMPANIES:</span>

                        <asp:TextBox ID="NUmberQuotesDropDownList" runat="server" ClientIDMode="Static">

                        </asp:TextBox>


                        <%-- <asp:DropDownList ID="NUmberQuotesDropDownList1"  ClientIDMode="Static" runat="server">
                             <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            
                        </asp:DropDownList>--%>



                        <%-- <input type="button" value="Show" onclick="callMe();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                        <%--<input type="button" value="Print" onclick="printDiv();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                    </td>
                </tr>
                <tr>
                    <td height="25px"></td>
                </tr>
                <tr>
                    <td height="25px">


                        <span style="float: left; width: 200px;">ALLOCATE REP:</span>


                        <asp:DropDownList ID="NumberAccountDropDownList" ClientIDMode="Static" runat="server">
                            <%-- <asp:ListItem Text="Aidan" Value="17"></asp:ListItem>
                            <asp:ListItem Text="Bailey" Value="14"></asp:ListItem>
                            <asp:ListItem Text="John" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Krit" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Taras" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Trent" Value="15"></asp:ListItem>
                            <asp:ListItem Text="William" Value="18"></asp:ListItem>
                            <asp:ListItem Text="TestOne" Value="24"></asp:ListItem>--%>
                        </asp:DropDownList>



                        <%-- <input type="button" value="Show" onclick="callMe();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                        <%--<input type="button" value="Print" onclick="printDiv();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                    </td>
                </tr>
                <tr>
                    <td height="25px">
                          <span style="float: left; width: 200px;">SELECT FILTERED COMPANIES:</span>
                          <input id="filteredcompanychk" type="checkbox" style="width:100px; height: 25px;"/>

                    </td>
                </tr>
                <tr>

                    <%-- <td height="25px" class="section_headings">Companies List</td>--%>
                    <td height="25px">

                        <asp:Button ID="undoButton" runat="server" Visible="false" Text="UNDO" OnClick="btn_undoClickEvent" OnClientClick="javascript:unClientEvent()"
                            ForeColor="Blue" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false" />

                        <asp:Button ID="allocateRepButton" runat="server" Text="ALLOCATE" OnClientClick="return callUpdateQuotes()"
                            ForeColor="Blue" CssClass="buttonClass moveRight" CausesValidation="false" />

                      

                        <div id="dvProgressBar" style="float: inherit; visibility: hidden;">
                            <img src="/images/loadingimage1.gif" />
                            <strong style="color: red;">Updating, Please Wait...</strong>
                        </div>
                    </td>

                </tr>
                <tr>
                    <td >

                        <span style="float:left;margin-right:12px;" class="inputTextStyle">START DATE: </span>

                        <span style="float:left;margin-right:12px;">
                            <input id="StartDateSTxt" type="date" name="StartDateTxt" class="inputColorGreen" size="24" runat="server" />
                        </span>

                        <span style="float:left;margin-right:12px;" class="inputTextStyle">END DATE: </span>

                        <span style="float:left;margin-right:12px;">
                            <input id="EndDateSTxt" type="date" class="inputColorGreen" name="EndDateTxt" size="24" runat="server" />
                        </span>
                        <span style="float:left;margin-right:12px;">
                            <input type="button" value="APPLY FILTER"  onclick="return senddata();" style="width: 160px; height: 30px; border: 1.5px solid blue; background-color: white; cursor: pointer;" />
                        </span>
                        <span style="float:left;margin-right:12px;">
                            <input type="button" value="RESET" onclick="return resetAlll();" style="width: 160px; height: 30px; border: 1.5px solid red; background-color: white; cursor: pointer;" />
                        </span>
                    </td>
                </tr>



            </table>

        </div>


        <asp:Label ID="messagelable" runat="server" CssClass="messagecss"></asp:Label>
        <br />

        <div id="notcallbacktable" class="container">
            <table>
                <tr>
                    <td width="60px;">
                        <span class="inputTextStyle" style="width: 100px; height: 30px; margin-right: 24px; padding-bottom: 20px;">Sort By  : </span>

                    </td>
                    <td>
                        <select id="sortinputdrop" class="inputColorGreen" style="width: 150px; height: 30px;">
                            <option value="dateorder">Ordered Date  </option>
                            <option value="comname">Company Name         </option>

                        </select></td>
                    <td>
                        <select id="sortinputdropdir" class="inputColorGreen" style="width: 150px; height: 30px; margin-left: 12px;">
                            <option value="desc">Desc </option>
                            <option value="asc">Asc       </option>

                        </select></td>

                </tr>
            </table>
            <div id="tooltip"></div>
            <table cellpadding="0" width="1500" cellspacing="0" border="0" class="display" id="livedbtbl" style="cursor: pointer">

                <thead>
                    <tr>
                        <th align="left"></th>
                        <th align="left">View</th>
                        <th align="left">Company Name</th>
                        <th align="left">Contact Name</th>
                        <th align="left">Last Order Date</th>
                        <th align="left">Notes</th>
                         <th align="left"></th>
                        <th align="left">Owner</th>
                        <th align="left">Active</th>
                        <th align="left">Locked</th>
                        <th align="left">Allocated</th>
                        <th align="left">Allocated Rep </th>
                        <th align="left">Select <input name="select_all" id="select-all" value="1" type="checkbox" /></th>
                        <th align="left">View</th>
                        

                    </tr>

                </thead>

                <tbody>
                </tbody>

            </table>

        </div>

    </div>

    <script type="text/javascript">
        function showLoadingImage() {
            $('#ContentPlaceHolder1_allocateRepButton').prop('disabled', false);
            $('#loadingiamge').show();
        }

        function hideLoadingImage() {
            $('#loadingiamge').hide();
        }

        function ShowProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
        }

        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
        }



        var selecteditems = [];
        function callUpdateQuotes() {

            var alloMode = $("#AllocationDropDownList").val();
            var alloRepName = $("#NumberAccountDropDownList").val();
            var OriRepName = $("#RepNameDropDownList").val();
            var allocationPeriod = $("#ALLOCATIONPERIODDropDownList").val();
            var numberQuotes = $("#NUmberQuotesDropDownList").val();

            if (alloMode == "2") {
                if (alloRepName == OriRepName) {
                    alert("Please change  Allocate Rep");
                    return false;
                }
            }

            if (alloMode == "0") {
                alert("Please select  mode");
                return false;
            }
            if (alloMode == "1") {

                if (numberQuotes == "") {
                    alert("Please enter number of companies");
                    return false;
                }
            }

            selecteditems = [];
            if (alloMode == "2") {

                $("#livedbtbl").find("input[name='selectchk']:checked").each(function (i, ob) {

                    var comId = $(ob).val();
                    var coN = new CompanyAlloSelect();
                    coN.CompanyId = comId;
                    coN.selected = true;
                    selecteditems.push(coN);

                });

                if (selecteditems.length == 0) {
                    alert("Please tick checkbox");
                    return false;
                }

                ShowProgressBar();
                PageMethods.UpdateAllocateCompany(selecteditems, alloMode, OriRepName, alloRepName, allocationPeriod, numberQuotes, updateCompanyResult);
            }
            else {
                ShowProgressBar();
                if ($("#filteredcompanychk").is(':checked')) {
                    alloMode = "3";
    //                var data = table
    //.rows()
    //.data();
                    table.$('input[name="selectchk"]').each(function (i, ob) {

                        var comId = $(ob).val();
                        var coN = new CompanyAlloSelect();
                        coN.CompanyId = comId;
                        coN.selected = true;
                        selecteditems.push(coN);
                    });
                    
                }
                console.log(selecteditems);
                PageMethods.UpdateAllocateCompany(selecteditems, alloMode, OriRepName, alloRepName, allocationPeriod, numberQuotes, updateCompanyResult);
            }




            return false;
        }


        function updateCompanyResult(res) {
            HideProgressBar();
            alert("Updated Successfully.");
            //location.reload();
             table.ajax.reload();
        }

        function CompanyAlloSelect() {

            this.CompanyId = 0;
            this.selected = false;

        }

        function unClientEvent() {

            var r = confirm("Are you sure?");
            if (r == true) {
                ShowProgressBar();
                return true;
            } else {
                return false;
            }

            return false;
        }

        function resetAlll() {

            $("#ContentPlaceHolder1_StartDateSTxt").val('dd/mm/yyyy');
            $("#ContentPlaceHolder1_EndDateSTxt").val('dd/mm/yyyy');
            callMe();
        }

        $(document).ready(function () {
            var dateNow = new Date();
            //  $("#ContentPlaceHolder1_StartDateSTxt").datepicker({ dateFormat: 'dd-mm-yy' });
            //$("#ContentPlaceHolder1_EndDateSTxt").datepicker({ dateFormat: 'dd-mm-yy' });

            selectCheckBox();
        });

        function selectCheckBox() {

            $('#select-all').on('click', function () {
                // Check/uncheck all checkboxes in the table
                var rows = table.rows({ 'search': 'applied' }).nodes();
              
                $('input[type="checkbox"][name="selectchk"]', rows).prop('checked', this.checked);
            });
        }

        function TestAccounts() {
            if ($("#filteredcompanychk").is(':checked')) {
                console.log("aaa in");
            }
            selecteditems = [];
            $("#livedbtbl").find("input[name='selectchk']").each(function (i, ob) {
                
                var comId = $(ob).val();
                var coN = new CompanyAlloSelect();
                coN.CompanyId = comId;
                coN.selected = true;
                selecteditems.push(coN);
            });

            console.log(selecteditems);
        }

    </script>

    <style type="text/css">
        #loadingiamge {
            position: absolute;
            top: -140px;
            width: 100%;
            height: 100%;
            background: url(Images/loadingimage1.gif) no-repeat center center;
        }
    </style>

</asp:Content>
