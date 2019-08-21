<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoOrderCompanyByRep.aspx.cs"  MasterPageFile="~/SiteInternalNavLevel1.Master"  Inherits="DeltoneCRM.NoOrderCompanyByRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://code.jquery.com/jquery-latest.js"></script>
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
 <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>
  <%--  <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
	</script>--%>
<%--	<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>--%>
	 <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
     <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.print.min.js"></script>
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
            max-width: 80%;
            margin: 0 auto;
            width: 80%;
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
                 .buttonClass
{
    padding: 2px 20px;
    text-decoration: none;
    border: solid 1px #3E3E42;
    height:40px;
    cursor:pointer;
}
.buttonClass:hover
{
    border: solid 1px Black;
    background-color: #ffffff;
}
.moveRight{
    float:right;
    margin-bottom:30px;
    color:blue;
    text-align:center;
       font-weight:bold;
}

.buttons-print{
     float:left;
      background-color: #ffffff;
      height:40px;
      padding: 2px 20px;
    text-decoration: none;
    border: solid 1px #3E3E42;
    height:20px;
    cursor:pointer;
     margin-bottom:10px;
    color:blue;
    text-align:center;
       font-weight:bold;
        margin-left:10px;

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

         td.highlight {
            font-weight: bold;
            color: red !important;
        }

          tr.highlight {
            font-weight: bold;
            color: red !important;
        }

           table.display tbody tr:nth-child(even):hover td {
            background-color: #99c2ff !important;
        }

        table.display tbody tr:nth-child(odd):hover td {
            background-color: #99c2ff !important;
        }
    </style>

    <script type="text/javascript">
        $.noConflict();
        var table;
        var Quotetable;
        var sort = "desc";
        var colNu = 4;
        var pageClicked = 0;
        $(document).ready(function () {
            console.log(colNu);
           // callMe();
            //repNameChange();
            //var dt = $('#livedbtbl').DataTable();
            //new $.fn.dataTable.Buttons(dt, {
            //    buttons: [
            //        {
            //            text: '<i class="fa fa-lg fa-print"></i> Print Assets',
            //            extend: 'print',
            //            className: 'btn btn-primary btn-sm m-5 width-140 assets-select-btn export-print'
            //        }
            //    ]
            //});


            $('#findcustomerorder').autocomplete({
                source: "/Fetch/FetchSearchOrder.aspx",
                select: function (event, ui) {
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    // $('#CompanyContactTR').show();
                    // $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    var valsSPLIT = ui.item.id.split(',');;

                    var url = 'order.aspx?Oderid=' + (valsSPLIT[0]) + '&cid=' + (valsSPLIT[2])
                            + '&Compid=' + (valsSPLIT[1]);
                    window.open(url, "_blank");
                    $(this).val(''); return false;


                }
            });

            var se = $("#monthdropDown").val();
            var re = "self";
           
            CreateTableData(re, sort, colNu,"0");
            $('#sortinputdrop').change(
    function () {

        callSortByType();
    });

            $('#sortinputdropdir').change(
    function () {

        callSortByType();
    });

            $('#monthdropDown').change(
   function () {
       var se = $("#monthdropDown").val();
       CreateTableData(re, sort, colNu, se);
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

                    row.child(format(row.data()[10])).show();
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

           
            searchSuper();

        });


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


        function CreateTableData(catValue, dirc, colNumber, startDate) {
            // $('#callbacktableDiv ').hide();
            // var catValue = "a";
            console.log(catValue);
            if (table) {

                table.destroy();
            }

           
            {

                table = $('#livedbtbl').DataTable({
                    processing: true,
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
                    "ajax": "../Fetch/FetchAllNoOrder.aspx?n=1",
                    "createdRow": function (row, data, index) {

                      //  console.log(row );
                        if (data[8] == "Y") {
                           // $('tr', row).addClass('highlight');
                            $('td', row).eq(7).addClass('highlight');
                            $('td', row).eq(2).addClass('highlight');
                            
                            $('td', row).eq(3).addClass('highlight');
                            $('td', row).eq(4).addClass('highlight');
                            $('td', row).eq(6).addClass('highlight');
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
                                
                                className: 'notestest', "targets": [5],
                            },

                            { "width": "3%", "targets": 0 },
                    { "width": "2%", "targets": 1 },
                     { "width": "2%", "targets": 2 },
                     { "width": "8%", "targets": 5 },
                     { "visible": false, "targets": 10},
                    { "visible": false, "targets": 6 }

                    ],
                    "aaSorting": [[colNumber, dirc]],
                    "iDisplayLength": 100,
                    "fnServerParams": function (aoData) {
                        aoData.push(
                        {
                            "name": "rep", "value": catValue

                        }, {
                            "name": "mon", "value": startDate
                        }
                       
                        );
                    },
                    dom: 'lBfrtip',
                    buttons: ['copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5']

                });
            }
        }



        function searchSuper() {


            $('#supersearch').on('keyup', function () {
                table
                    .columns(8)
                    .search(this.value)
                    .draw();
            });
        }
        

        function ViewCompany(CompanyID) {

            window.open('ConpanyInfo.aspx?companyid=' + CompanyID, '_blank');
        }

        function printDiv() {
            var divToPrint = document.getElementById("tabledatadiv");
            newWin = window.open("");
            newWin.document.write(divToPrint.innerHTML);
            newWin.print();
            newWin.close();

            var oldPage = document.body.innerHTML;
            var joinElemn = divToPrint;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML =
              "<html><head><title></title></head><body>" +
              joinElemn + "</body>";

            //Print Page
            window.print();

            //Restore orignal HTML
            document.body.innerHTML = oldPage;


            //var table = $('#livedbtbl').DataTable();

            //var data = table
            //    .rows()
            //    .data();

            //console.log(data);
        }


        function CreateTableDataFilterData(catValue, dirc, colNumber, startDate,EndDate) {
            // $('#callbacktableDiv ').hide();
            // var catValue = "a";
            console.log(catValue);
            if (table) {

                table.destroy();
            }


            {

                table = $('#livedbtbl').DataTable({
                    processing: true,
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
                    "ajax": "../Fetch/FetchAllNoOrder.aspx?n=1",
                    "createdRow": function (row, data, index) {

                        //  console.log(row );
                        if (data[8] == "Y") {
                            // $('tr', row).addClass('highlight');
                            $('td', row).eq(7).addClass('highlight');
                            $('td', row).eq(2).addClass('highlight');

                            $('td', row).eq(3).addClass('highlight');
                            $('td', row).eq(4).addClass('highlight');
                            $('td', row).eq(6).addClass('highlight');
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

                                className: 'notestest', "targets": [5],
                            },

                            { "width": "3%", "targets": 0 },
                    { "width": "2%", "targets": 1 },
                     { "width": "2%", "targets": 2 },
                     { "width": "8%", "targets": 5 },
                     { "visible": false, "targets": 10 },
                    { "visible": false, "targets": 6 }

                    ],
                    "aaSorting": [[colNumber, dirc]],
                    "iDisplayLength": 100,
                    "fnServerParams": function (aoData) {
                        aoData.push(
                        {
                            "name": "rep", "value": catValue

                        }, {
                            "name": "starDate", "value": startDate
                        }, {
                            "name": "endDate", "value": EndDate
                        }


                        );
                    },
                    dom: 'lBfrtip',
                    buttons: ['copyHtml5', 'excelHtml5', 'csvHtml5', 'pdfHtml5']

                });
            }
        }

        function reSetCallData() {
            var se = $("#monthdropDown").val();
            var re = "self";
            $("#ContentPlaceHolder1_StartDateSTxt").val('dd/mm/yyyy');
            $("#ContentPlaceHolder1_EndDateSTxt").val('dd/mm/yyyy');
            CreateTableData(re, sort, colNu, se);
        }

        function senddata() {

            var se = $("#monthdropDown").val();
            var re = "self";
            //  CreateTableData(re);
            var stDate = $("#ContentPlaceHolder1_StartDateSTxt").val();
            var enDate = $("#ContentPlaceHolder1_EndDateSTxt").val();
            if (stDate != "dd/mm/yyyy" && enDate != "dd/mm/yyyy")
                CreateTableDataFilterData(re, sort, colNu, stDate, enDate);
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="gridDiv">
        <br />


        <br />
        <asp:Label ID="messagelable" runat="server" CssClass="messagecss"></asp:Label>
        <br />
        <br />



        <br />
        <%--  Select Rep:
      <asp:DropDownList ID="RepDropDownList" runat="server">
           <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
           <asp:ListItem Text="Trent" Value="15"></asp:ListItem>
          <asp:ListItem Text="Bailey" Value="14"></asp:ListItem>
          <asp:ListItem Text="John" Value="10"></asp:ListItem>
          <asp:ListItem Text="Aidan" Value="17"></asp:ListItem>
           <asp:ListItem Text="William" Value="18"></asp:ListItem>
           <asp:ListItem Text="Ross" Value="19"></asp:ListItem>
      </asp:DropDownList>--%>

        <div id="main">
            <br />
            <br />


            <table width="1500" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
                <tr>
                    <td height="25px"></td>
                </tr>
               <tr>
                  <td>    
                      <a href="RepSuperAccount.aspx" class="buttonClass" style="float:left !important;margin-bottom:12px;line-height: 40px;margin-left:6px;width:236px;" >VIEW SUPER ACCOUNT</a>
                     </td>
                   <td>   <input type="button" onclick="printDiv();" value="PRINT" style="width: 60px; height: 30px; border: 1.5px solid blue; background-color: white; cursor: pointer;" /></td>
                </tr>
               
              
            </table>
              <div id="notcallbacktable" class="container">
            <table>

                <tr>
                    <td width="60px;">
                  <span class="inputTextStyle" style="width: 100px; height: 30px; margin-right: 24px; padding-bottom: 20px;float: left;">Last Sold Month:</span>
                    </td>
                    <td> <select id="monthdropDown"   class="inputColorGreen" style="width: 150px; height: 30px;float: left;">
                         <option value="0" selected="selected">All</option>
                        <option value="-1" >Last Month</option>
                        <option value="-2">Last 2 Months</option>
                        <option value="-3">Last 3 Months</option>
                         <option value="-4">Last 4 Months</option>
                         <option value="-5">Last 5 Months</option>
                        <option value="-6">Last 6 Months</option>
                         <option value="-7">Last 7 Months</option>
                         <option value="-8">Last 8 Months</option>
                         <option value="-9">Last 9 Months</option>
                         <option value="-10">Last 10 Months</option>
                         <option value="-11">Last 11 Months</option>
                        <option value="-12">Last One Year</option>

                    </select></td>
                     </tr>

                  <tr>
                    <td width="60px;">
                        <span class="inputTextStyle" style="float:left;margin-right:12px;">Start Date:</span>

                    </td>
                    <td>
                        <span>
                            <input id="StartDateSTxt" type="date" name="StartDateTxt" class="inputColorGreen" style="float:left;margin-right:12px;height: 22px;" size="24" runat="server" />
                        </span></td>
                    <td width="20px;">
                         <span style="float:left;margin-right:12px;" class="inputTextStyle">End Date: </span>
                         </td>
                       <td>
                        <span>
                            <input id="EndDateSTxt" type="date" name="EndDateTxt" class="inputColorGreen" style="float:left;margin-right:12px;height: 22px;" size="24" runat="server" />
                        </span>


                    </td>

                      <td>
                          <span>
                            <input type="button" value="APPLY FILTER"  onclick="return senddata();" style="float:left;margin-right:12px;width: 160px; height: 30px; border: 1.5px solid blue; background-color: white; cursor: pointer;" />
                        </span>
                      </td>
                      <td>
                           <span>
                            <input type="button" value="RESET"  onclick="return reSetCallData();" style="float:left;margin-right:12px;width: 160px; height: 30px; border: 1.5px solid red; background-color: white; cursor: pointer;" />
                        </span>
                      </td>

                   
                </tr>
                 <tr>
                    <td height="25px"></td>
                </tr>
                <tr>
                    <td width="60px;">
                        <span class="inputTextStyle" style="width: 100px; height: 30px; margin-right: 24px; padding-bottom: 20px;float: left;">Sort By Column:</span>

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

                    <td height="20px" width="180px;"><span class="inputTextStyle" style="margin-right: 6px;">Search By Super  :  </span> </td>
                               
                     <td> <input class="inputColorGreen" type="text" id="supersearch" style="width: 150px; height: 20px;"/></td>
                </tr>
            </table>
            <div id="tooltip"></div>
                  <div id="tabledatadiv">
            <table cellpadding="0" width="1200" cellspacing="0" border="0" class="display" id="livedbtbl" style="cursor: pointer">

                <thead>
                    <tr>
                        
                         <th align="center"></th>
                        <th align="center">View</th>
                        <th align="center">Company Name</th>
                        <th align="center">Contact Name</th>
                        <th align="center">Last Order Date</th>
                        <th align="center">Notes</th>
                         <th align="center"></th>
                        <th align="center">Telephone</th>
                       
                        <th align="center"> Super Account </th>
                        <th align="center"> Allocated Rep</th>
                       <th align="center"></th>
                     

                    </tr>

                </thead>

                <tbody>
                </tbody>

            </table>
                      </div>

        </div>
        </div>

    </div>
</asp:Content>
