<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AllQuotes.aspx.cs" Inherits="DeltoneCRM.AllQuotes" %>
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
  <%--  <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>--%>
    <script src="js/datatableup/datatables.min.js"></script>
    
     <%--  <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
	</script>--%>
    <%--	<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>--%>
   <%-- <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js">
    </script>--%>
 <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.html5.min.js"></script>
     <script src="https://cdn.datatables.net/buttons/1.5.2/js/dataTables.buttons.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/1.5.2/js/buttons.print.min.js"></script>
    <script type="text/javascript">

        function Edit(QuoteID, CompanyID, ContactID) {
            $.ajax({
                url: 'Fetch/FetchDBType.aspx',
                data: {
                    Flag: QuoteID,
                },
                success: function(response) {
                    window.open("quote.aspx?OderID=" + QuoteID + "&CompID=" + CompanyID + "&cid=" + ContactID + "&DB=" + response + "&Flag=Y", '_blank');
                }
            })
        }

        //$(document).ready(function () {
        //    var livetable = $('#livedbtbl').DataTable({
        //        columns: [
        //            { 'data': 'QuoteID' },
        //            { 'data': 'QuoteDate' },
        //            { 'data': 'CompanyName' },
        //            { 'data': 'ContactName' },
        //            { 'data': 'QuoteTotal' },
        //            { 'data': 'QuotedBy' },
        //            { 'data': 'CustomerType' },
        //            { 'data': 'QuoteStatus' },
        //            { 'data': 'View' },
                    
        //        ],
        //        "iDisplayLength": 25,
        //        "aaSorting": [[ 1, "desc" ]],
        //        "bServerSide": true,
        //        sAjaxSource: 'DataHandlers/AllQuotesDataHandler.ashx',
        //        "fnServerParams": function (aoData) {
        //            aoData.push(
        //                {"name": "sStatus", "value": "ACTIVE"}
        //            );
        //        }
        //    });

            
        //});
        var table;
        var callBacktable;
        $.noConflict();
        $(document).ready(function () {
            CreateTableData("ini");
          

        });


        function openNew() {
           
            CreateTableData("ne");
            $('#alltab').removeClass("tab01-01");
            $('#alltab').addClass("tab01-01Noclicked");
            $('#newtab').removeClass("tab01-02");
            $('#newtab').addClass("tabclicked");
            $('#callbacktab').removeClass("tabclicked");
            $('#callbacktab').addClass("tab01-03");
            $('#maybetab').removeClass("tabclicked");
            $('#maybetab').addClass("tab01-04");
            $('#notab').removeClass("tabclicked");
            $('#notab').addClass("tab01-05");
            $('#soldtab').removeClass("tabclicked");
            $('#soldtab').addClass("tab01-06");
        }


        function openCallBack() {
            CreateTableCallBackData("cb");
            $('#alltab').removeClass("tab01-01");
            $('#alltab').addClass("tab01-01Noclicked");
            $('#newtab').removeClass("tabclicked");
            $('#newtab').addClass("tab01-02");
            $('#callbacktab').removeClass("tab01-03");
            $('#callbacktab').addClass("tabclicked");
            $('#maybetab').removeClass("tabclicked");
            $('#maybetab').addClass("tab01-04");
            $('#notab').removeClass("tabclicked");
            $('#notab').addClass("tab01-05");
            $('#soldtab').removeClass("tabclicked");
            $('#soldtab').addClass("tab01-06");
        }
        function openCallMayBe() {

            CreateTableData("me");
            $('#alltab').removeClass("tab01-01");
            $('#alltab').addClass("tab01-01Noclicked");
            $('#newtab').removeClass("tabclicked");
            $('#newtab').addClass("tab01-02");
            $('#callbacktab').removeClass("tabclicked");
            $('#callbacktab').addClass("tab01-03");
            $('#maybetab').removeClass("tab01-04");
            $('#maybetab').addClass("tabclicked");
            $('#notab').removeClass("tabclicked");
            $('#notab').addClass("tab01-05");
            $('#soldtab').removeClass("tabclicked");
            $('#soldtab').addClass("tab01-06");
        }
        function openNo() {

            CreateTableData("no");
            $('#alltab').removeClass("tab01-01");
            $('#alltab').addClass("tab01-01Noclicked");
            $('#newtab').removeClass("tabclicked");
            $('#newtab').addClass("tab01-02");
            $('#callbacktab').removeClass("tabclicked");
            $('#callbacktab').addClass("tab01-03");
            $('#maybetab').removeClass("tabclicked");
            $('#maybetab').addClass("tab01-04");
            $('#notab').removeClass("tab01-05");
            $('#notab').addClass("tabclicked");
            $('#soldtab').removeClass("tabclicked");
            $('#soldtab').addClass("tab01-06");
        }
        function openSold() {

            CreateTableData("so");

            $('#alltab').removeClass("tab01-01");
            $('#alltab').addClass("tab01-01Noclicked");
            $('#newtab').removeClass("tabclicked");
            $('#newtab').addClass("tab01-02");
            $('#callbacktab').removeClass("tabclicked");
            $('#callbacktab').addClass("tab01-03");
            $('#maybetab').removeClass("tabclicked");
            $('#maybetab').addClass("tab01-04");
            $('#notab').removeClass("tabclicked");
            $('#notab').addClass("tab01-05");
            $('#soldtab').removeClass("tab01-06");
            $('#soldtab').addClass("tabclicked");
        }

        function openAll() {

            CreateTableData("ini");
            $('#alltab').removeClass("tab01-04ClickMe");
            $('#alltab').addClass("tab01-03");
            $('#ExistingCustomertab').addClass("tab01-04ClickMe");
            $('#ExistingCustomertab').removeClass("tab01-03");
            $('#newCustomertab').addClass("tab01-04ClickMe");
            $('#newCustomertab').removeClass("tab01-03");
            $("#typespan").text("ALL CUSTOMER");
            
        }

        function newCustomer() {

            CreateTableData("newc");
            $('#alltab').addClass("tab01-04ClickMe");
            $('#alltab').removeClass("tab01-03");
            $('#ExistingCustomertab').addClass("tab01-04ClickMe");
            $('#ExistingCustomertab').removeClass("tab01-03");
            $('#newCustomertab').removeClass("tab01-04ClickMe");
            $('#newCustomertab').addClass("tab01-03");
            $("#typespan").text("NEW CUSTOMER");
        }

        function existingCustomer() {

            CreateTableData("existC");
            $('#alltab').addClass("tab01-04ClickMe");
            $('#alltab').removeClass("tab01-03");
            $('#ExistingCustomertab').removeClass("tab01-04ClickMe");
            $('#ExistingCustomertab').addClass("tab01-03");
            $('#newCustomertab').addClass("tab01-04ClickMe");
            $('#newCustomertab').removeClass("tab01-03");
            $("#typespan").text("EXISTING CUSTOMER");

        }

        function CreateTableData(catValue) {
            $('#callbacktableDiv ').hide();
            $('#notcallbacktable').show();
            if (table)
                table.destroy();

            table = $('#livedbtbl').DataTable({
                "ajax": "../Fetch/FetchAllQuotes.aspx?n=1",
                "aaSorting": [[0, "desc"]],
                "iDisplayLength": 25,
                "fnServerParams": function (aoData) {
                    aoData.push(
                        { "name": "category", "value": catValue }
                    );
                },
                dom: 'lBfrtip',
                buttons: [
             'excel',  'print'
                ]

            });
        }

        function CreateTableCallBackData(catValue) {
            $('#callbacktableDiv ').show();
            $('#notcallbacktable').hide();
             if (callBacktable)
                 callBacktable.destroy();

             callBacktable = $('#callbackTable').DataTable({
                "ajax": "../Fetch/FetchAllQuotes.aspx?n=1",
                "aaSorting": [[0, "desc"]],
                "iDisplayLength": 25,
                "fnServerParams": function (aoData) {
                    aoData.push(
                        { "name": "category", "value": catValue }
                    );
                }
            });
        }

        </script>
    <style type="text/css">
                body {
    background-color: #A0E5A0 !important;
}  
        .auto-style1 {
            background-color: #FFF;
            border: 1px solid #CCCCCC;
            width: 978px;
            height: 63px;
        }

        /* Style the tab */
div.tab {
    overflow: hidden;
    border: 1px solid #ccc;
    background-color: #f1f1f1;
}

/* Style the buttons inside the tab */
div.tab button {
   
    float: left;
    border: none;
    outline: none;
    cursor: pointer;
    padding: 14px 16px;
    transition: 0.3s;
}

/* Change background color of buttons on hover */
div.tab a:hover {
    background-color: #ddd;
}

/* Create an active/current tablink class */
div.tab a.active {
    background-color: #ccc;
}

/* Style the tab content */
.tabcontent {
    display: none;
    padding: 6px 12px;
    border: 1px solid #ccc;
    border-top: none;
}


.button {
    display: block;
    width: 115px;
    height: 25px;
    background: #4E9CAF;
    padding: 10px;
    text-align: center;
    border-radius: 5px;
    color: white;
    font-weight: bold;
    float:left;
    color: black;
    cursor:pointer;
}

.button3 {background-color: #4CAF50;} /* Green */
.button2 {background-color: #008CBA;} /* Blue */
.button4 {background-color: #f44336;} /* Red */ 
.button1 {background-color: #e7e7e7; } /* Gray */ 
.button5 {background-color: #555555;} /* Black */
.button0 {background-color: white;} /* Black */

       .tab01-01 {
             font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #fff;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab01-01:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab01-02 {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #bcaa85;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab01-02:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }

        .tab01-03 {
            font-family: 'Raleway', sans-serif;
            color: white;
            text-align:center;
            font-size:14px;
            font-weight:600;
            background-color: blue;
            height: 35px;
            padding-left: 15px;
            border-left-color: #ffffff;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab01-03:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
         .tab01-04 {
            font-family: 'Raleway', sans-serif;
            color: white;
            text-align:center;
            font-size:14px;
            font-weight:600;
            background-color: #4CAF50;
            height: 35px;
            padding-left: 15px;
            border-left-color: #ffffff;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

          .tab01-04ClickMe {
            font-family: 'Raleway', sans-serif;
            color: black;
            text-align:center;
            font-size:14px;
            font-weight:600;
            background-color: #c4dde6;
            height: 35px;
            padding-left: 15px;
            border-left-color: #ffffff;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            cursor:pointer;
        }
        .tab01-04:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
         .tab01-05 {
            font-family: 'Raleway', sans-serif;
            color: white;
            text-align:center;
            font-size:14px;
            font-weight:600;
            background-color: #f44336;
            height: 35px;
            padding-left: 15px;
            border-left-color: #ffffff;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab01-05:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
         .tab01-06 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #555555;
            height: 35px;
            padding-left: 15px;
            border-left-color: #ffffff;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab01-06:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab02-01 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e7e7e7;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab02-01:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab02-02 {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #bcaa85;
            padding-left: 15px;
            border-top-color: #cccccc;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
        }
        .tab02-03 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab02-03:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab03-01 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab03-01:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab03-02 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
                        border-left-color: #ffffff;
            border-left-style: solid;
            border-left-width: 1px;
        }
        .tab03-02:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab03-03 {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #ffffff;
            padding-left: 15px;
            border-top-color: #cccccc;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
        }


         .tabclicked {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #ffffff;
            padding-left: 15px;
            border-top-color: #cccccc;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
        }

         .tab01-01Noclicked {
           font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e7e7e7;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            background-color: #FFFF00;
          
        }
            .tab01-01Noclicked:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">ALL QUOTES </td>
        </tr>

        <tr>
            <td  class="section_headings"><a href="AllQuotesPending.aspx">PENDING</a>
        </tr>
         

 <tr>
            <td  class="section_headings">
                <table >
                     <tr>
                        <td width="300px" class="tab01-03" id="alltab" onclick="openAll();">All</td>
                         <td width="300px" class="tab01-04ClickMe" id="ExistingCustomertab" onclick="existingCustomer();">Existing Customer</td>
                         <td width="300px" class="tab01-04ClickMe" id="newCustomertab" onclick="newCustomer();">New Customer</td>
                         

                         <td width="250px" class="tab01-02" id="newtab" onclick="openNew();" style="display:none;">New</td>
                         <td width="250px" class="tab01-03" id="callbacktab" onclick="openCallBack();" style="display:none;">Call Back</td>
                         <td width="250px" class="tab01-04" id="maybetab" onclick="openCallMayBe();" style="display:none;">May Be</td>
                          <td width="250px" class="tab01-05" id="notab" onclick="openNo();" style="display:none;">No</td>
                         <td width="250px" class="tab01-06" id="soldtab" onclick="openSold()" style="display:none;">Sold</td>
                          
                     </tr>
                   
                 </table>
     </td>
     <td height="25px">&nbsp;</td>
     

        </tr>

          <tr> <td height="25px" class="section_headings"><span id="typespan"> ALL CUSTOMER</span> </td></tr>
        <tr>
           <td class="white-box-outline">
              <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                  <tr>
                      <td height="20px">&nbsp;</td>
                  </tr>

                  <tr>
                      <td>

<div id="notcallbacktable"  class="container">
                        
<table cellpadding="0" width="900" cellspacing="0" border="0" class="display" id="livedbtbl" style="cursor:pointer">
	<thead>
		<tr>
            <th>QUOTE ID</th>
                                                    <th>CREATED DATE</th>
            <th>COMPANY NAME</th>
                                                    <th>CONTACT NAME</th>
            <th>EMAIL</th>
			                                        <th>QUOTE TOTAL</th>
             <th>SOLD</th>
            <th>QUOTED BY</th>
            <th>CUSTOMER TYPE</th>
                                                    <th>STATUS</th>
                                                    <th>VIEW</th>
            
		</tr>
        
	</thead>
                 
	<tbody>
		 
	</tbody>

</table>

    </div>
              <div id="callbacktableDiv"  class="container">
<table cellpadding="0" width="900" cellspacing="0" border="0" class="display" id="callbackTable" style="cursor:pointer">
	<thead>
		<tr>
            <th>QUOTE ID</th>
                                                    <th>CREATED DATE</th>
            <th>COMPANY NAME</th>
                                                    <th>CONTACT NAME</th>
             <th >CALL BackDate</th>
           
			                                        <th>QUOTE TOTAL</th>
            <th>QUOTED BY</th>
            <th>CUSTOMER TYPE</th>
                                                    <th>STATUS</th>
                                                    <th>VIEW</th>
            
		</tr>
        
	</thead>
                 
	<tbody>
		 
	</tbody>

</table></div>
			
            </td>
        </tr>
   </table>
            </td>
        </tr>
        <tr>
          <td height="25px">&nbsp;</td>
        </tr>



        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
      </table>
</asp:Content>
