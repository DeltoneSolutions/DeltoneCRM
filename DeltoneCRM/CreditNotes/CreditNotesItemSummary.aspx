<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditNotesItemSummary.aspx.cs" 
    Inherits="DeltoneCRM.CreditNotes.CreditNotesItemSummary"  MasterPageFile="~/SiteInternalNavLevel1.Master"  %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.11.1.min.js"></script>
  .
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>
    <script src="../js/jquery-1.11.1.min.js"></script>
    <link href="../css/NewCSS.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui.js"></script>

    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>
    <script src="../js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/jquery.dataTables_new.css" />

    <%-- <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
    <script src="../Scripts/jscolor.js"></script>
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>
    <%--  <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
	</script>--%>
    <%--	<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>--%>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.1/js/dataTables.buttons.min.js">
    </script>
    


    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.5.1/js/buttons.print.min.js">
    </script>
    <title>Items Summary</title>

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

        .undoButtonStyle{
            margin-left:30px;
        }

        .width-470-style {
            width: 470px;
        }

        td.highlight {
            font-weight: bold;
            color: red !important;
        }

        td.highlightBlue {
            font-weight: bold;
            color: #0066ff !important;
        }

    </style>

    <script type="text/javascript">
        var table;
        var tabletwo;
        var cName = "<%= ComSName %>";  
        var cBuilder = "<%= ContactBuilder %>"; 

        $(document).ready(function () {
            
            callMe();
            //orderTableGroup();


            //$('#noordercompany').on('page.dt', function () {
            //    var info = table.page.info();
            //    pageClicked = info.page;
            //    console.log(info.page);
                
            //    // if (info.page==0)
            //    //table.page(1).draw('page');
            //    //$('#pageInfo').html('Showing page: ' + info.page + ' of ' + info.pages);
            //});
        });

        function orderTableGroup() {

            $('#noordercompany tbody').on('click', 'tr.group', function () {
                var currentOrder = table.order()[0];
                if (currentOrder[0] === 1 && currentOrder[1] === 'asc') {
                    table.order([1, 'desc']).draw();
                }
                else {
                    table.order([1, 'asc']).draw();
                }
            });
        }

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }


        function callMe() {

            var re = "-1";
            var comId = getParameterByName("coId");
            CreateTableData(comId);
            //CreateTableDataHistory(comId);
        }


        function selectCheckBox() {

            $('#select-all').on('click', function () {
                // Check/uncheck all checkboxes in the table
                var rows = table.rows({ 'search': 'applied' }).nodes();
                console.log(rows);
                $('input[type="checkbox"][name="selectchk"]', rows).prop('checked', this.checked);
            });
        }
        
        function CreateTableData(re) {

            if (table)
                table.destroy();


            table = $('#noordercompany').DataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response...</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                "ajax": "../Fetch/FetchCompanyCreditItems.aspx?n=1",
                "createdRow": function (row, data, index) {

                    //console.log(data[9] );
                    if (data[9] == "2") {
                       // console.log('aa');
                        $('td', row).eq(0).addClass('highlight');
                        $('td', row).eq(1).addClass('highlight');
                        $('td', row).eq(2).addClass('highlight');
                        $('td', row).eq(3).addClass('highlight');
                        $('td', row).eq(4).addClass('highlight');
                        $('td', row).eq(5).addClass('highlight');
                        $('td', row).eq(6).addClass('highlight');
                        $('td', row).eq(7).addClass('highlight');
                    }if (data[9] == "3") {
                       // console.log('aa');
                        $('td', row).eq(0).addClass('highlightBlue');
                        $('td', row).eq(1).addClass('highlightBlue');
                        $('td', row).eq(2).addClass('highlightBlue');
                        $('td', row).eq(3).addClass('highlightBlue');
                        $('td', row).eq(4).addClass('highlightBlue');
                        $('td', row).eq(5).addClass('highlightBlue');
                        $('td', row).eq(6).addClass('highlightBlue');
                        $('td', row).eq(7).addClass('highlightBlue');
                    }
                    
                    // $(row).css('background-color', 'Orange !important');

                },
                "columnDefs": [
                        {
                            className: 'align_left', "targets": [0,1, 2, 3,4,5],
                        },
                        {
                            className: 'dt-center', "targets": [ 6,7,8],
                        },
                { "visible": false, "targets": 1 },
                { "visible": false, "targets": 9}
                        ,
                         { type: 'date', targets: [2] },
                ],
                "aaSorting": [[2, "desc"]],
                "iDisplayLength": 25,
                "fnServerParams": function (aoData) {
                    aoData.push(
                    { "name": "cId", "value": re }
                    );
                },
                drawCallback: function (settings) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;
                    var idcredit = [];
                    var typeArray = [];
                    var lastOrderId = null;
                    //console.log("aaa" + api.column(0, { page: 'current' }).data()[0]);
                    //if (lastOrderId != api.column(0, { page: 'current' }).data()[0])
                    //    lastOrderId = api.column(0, { page: 'current' }).data()[0];

                    api.column(0, { page: 'current' }).data().each(function (orIdSe, i) {
                        lastOrderId = orIdSe;
                        idcredit.push(orIdSe);
                        
                    });
                    api.column(9, { page: 'current' }).data().each(function (typeArr, i) {
                       
                        typeArray.push(typeArr);
                       
                        
                    });

                    api.column(1, { page: 'current' }).data().each(function (group, i) {
                        
                        if (last !== group) {
                            if (typeArray[i] == "3") {
                                var typeOrder = 1;
                                var buttonRow = '<td colspan="1" style="BACKGROUND-COLOR:rgb(0, 102, 255);font-weight:700;color:white;" onclick="callMypageCredit(' + idcredit[i] + ',' + typeOrder + ');"> <a onclick="callMypage(' + group + ');" href="#"/>View</a></td>';
                                $(rows).eq(i).before(
                                    '<tr class="group"><td colspan="7" style="BACKGROUND-COLOR:rgb(0, 102, 255);font-weight:700;color:white;">' + 'Xero : ' + group + '</td>' + buttonRow + '</tr>'
                                );
                            }
                            else {
                                if (typeArray[i] == "1") {
                                    var buttonRow = '<td colspan="1" style="BACKGROUND-COLOR:rgb(183, 172, 168);font-weight:700;color:#3E3A42;" onclick="callMypageCredit(' + idcredit[i] + ',' + typeArray[i] + ');"> <a onclick="callMypage(' + group + ');" href="#"/>View</a></td>';
                                    $(rows).eq(i).before(
                                        '<tr class="group"><td colspan="7" style="BACKGROUND-COLOR:rgb(183, 172, 168);font-weight:700;color:#3E3A42;">' + 'Xero : ' + group + '</td>' + buttonRow + '</tr>'
                                    );
                                }
                                else {
                                    var buttonRow = '<td colspan="1" style="BACKGROUND-COLOR:rgb(255, 102, 102);font-weight:700;color:white;" onclick="callMypageCredit(' + idcredit[i] + ',' + typeArray[i] + ');"> <a onclick="callMypage(' + group + ');" href="#"/>View</a></td>';
                                    $(rows).eq(i).before(
                                        '<tr class="group"><td colspan="7" style="BACKGROUND-COLOR:rgb(255, 102, 102);font-weight:700;color:white;">' + 'Xero : ' + group + '</td>' + buttonRow + '</tr>'
                                    );
                                }
                            }

                            last = group;
                        }
                    });
                },
                dom: 'lBfrtip',
                buttons: [
           {
               extend: 'print',
               title: cName,
               messageTop: cBuilder + '<br/>',
              
               
           }

                ],

                

            });
        }


        function callMypageCredit(dttId,type) {

            var conId = 0;
            var comId = 0;

            if (type == "2") {
                $.ajax({

                    url: "../Fetch/FetchContactandCompanyByCreditId.aspx?orderlinePage=" + dttId,

                    //contentType: 'application/json; charset=utf-8',
                    //dataType: 'json',
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        // alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                    },
                    success: function (result) {

                        var parseresult = $.parseJSON(result);
                        conId = parseresult.ContactId;
                        comId = parseresult.CompanyId;
                        window.open("../UpdateCredit.aspx?CreditNoteID=" + dttId + "&cid=" + conId + "&Compid=" + comId + "&canBack=1", '_self');

                    }
                });
            }
            else
                callMypage(dttId);

            // 
        }

        function showAcccount() {
            var comId = getParameterByName("coId");
            window.open("../ConpanyInfo.aspx?companyid=" + comId,'_self');
        }
        var idORder = [];
        function CreateTableDataHistory(re) {

            if (tabletwo)
                tabletwo.destroy();


            tabletwo = $('#noordercompanyhistory').DataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response...</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                "ajax": "../Fetch/FetchCompanyOrderItems.aspx?n=1",
                "columnDefs": [
                        {
                            className: 'align_left', "targets": [0, 1, 2, 3, 4, 5],
                        },
                        {
                            className: 'dt-center', "targets": [6, 7, 8],
                        },
                { "visible": false, "targets": 1 }
                        ,
                         { type: 'date', targets: [2] },
                ],
                "aaSorting": [[0, "desc"]],
                "iDisplayLength": 25,
                "fnServerParams": function (aoData) {
                    aoData.push(
                    { "name": "cId", "value": re }
                    );
                },
                drawCallback: function (settings) {
                    var api = this.api();
                    var rows = api.rows({ page: 'current' }).nodes();
                    var last = null;
                    var dataId = rows[0];
                    var lastOrderId = null;
                    //console.log("aaa" + api.column(0, { page: 'current' }).data()[0]);
                    //if (lastOrderId != api.column(0, { page: 'current' }).data()[0])
                    //    lastOrderId = api.column(0, { page: 'current' }).data()[0];

                    api.column(0, { page: 'current' }).data().each(function (orIdSe, i) {
                        idORder.push(orIdSe);
                        lastOrderId = orIdSe;
                       
                    });

                    api.column(1, { page: 'current' }).data().each(function (group, i) {
                        //  console.log("ccc" + i);
                        if (last !== group) {
                            //  var datare = table.row(i).data();
                            //  console.log(datare[]);
                            //console.log(api.column(0, { page: 'current' }).data()[0]);
                            var urlLink = "order.aspx?dtId=" + group;
                            var buttonRow = '<td colspan="1" style="BACKGROUND-COLOR:rgb(183, 172, 168);font-weight:700;color:#000;" onclick="callMypage(' + idORder[i] + ');"> <a onclick="callMypage(' + group + ');" href="#"/>View</a></td>';
                            $(rows).eq(i).before(
                                '<tr class="group"><td colspan="7" style="BACKGROUND-COLOR:rgb(183, 172, 168);font-weight:700;color:#3E3A42;">' + ' ORDER : ' + group + '</td>' + buttonRow + '</tr>'
                            );

                            last = group;
                        }
                    });
                },
                dom: 'lBfrtip',
                buttons: [
           {
               extend: 'print',
               title: cName,
               messageTop: cBuilder + '<br/>',


           }

                ],



            });
        }

        function callMypage(dttId) {

            var conId = 0;
            var comId = 0;
            $.ajax({

                url: "../Fetch/FetchContactandCompanyByOrderId.aspx?orderlinePage=" + dttId,

                //contentType: 'application/json; charset=utf-8',
                //dataType: 'json',
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    // alert("Request: " + XMLHttpRequest.toString() + "\n\nStatus: " + textStatus + "\n\nError: " + errorThrown);
                },
                success: function (result) {

                    var parseresult = $.parseJSON(result);
                    conId = parseresult.ContactId;
                    comId = parseresult.CompanyId;
                    window.open("../order.aspx?Oderid=" + dttId + "&cid=" + conId + "&Compid=" + comId + "&canBack=1", '_self');

                }
            });


            // 
        }


        function printMe() {
          //  var divElements = document.getElementById("scroller").innerHTML;
           // var divtotal = document.getElementById("totalDiv").innerHTML;
            var headertalbe = document.getElementById("maindatatableMain").innerHTML;
            //Get the HTML of whole page
            var oldPage = headertalbe;
            var joinElemn = headertalbe;
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

</asp:Content>
<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManagerupdatecomlockList" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="gridDiv">
        <br />

        <input type="button" onclick="printMe();" value="PRINT" style="width: 60px;height:30px; border: 1.5px solid blue; background-color: white;cursor:pointer;display:none;"/> 
         <table align="left" cellpadding="0" cellspacing="0" class="width-470-style">
                                                        <tr>
                                                            <td class="company-name-style" height="30px">
                                                                <div id="CompanyNameDIV" runat="server"></div>

                                                               
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="ContactInfo" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="StreetAddressLine1" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="StreetAddressLine2" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-tel-style">
                                                                <div id="ContactandEmail" runat="server"></div>
                                                            </td>
                                                        </tr>

                                                    </table>
        

        <%--  <asp:TextBox ID="searchTextBox" runat="server" Width="60%"></asp:TextBox>
        <asp:Button OnClick="SearchButton_Click" ID="btnsearch" Text="Search" ForeColor="Blue" Width="20%" runat="server" CausesValidation="false" />--%>
        <div id="main">
           

            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

               
              
                <tr>
                   
                    <%-- <td height="25px" class="section_headings">Companies List</td>--%>
                    <td height="25px">

                       <asp:Button OnClick="btnaccountDash_Click" ID="Buttonaccount" Text="Account" ForeColor="Blue" Width="10%" OnClientClick="showAcccount();"
                    runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />

                      
                         <div ID="dvProgressBar" style="float:inherit;visibility: hidden;" >
        <img src="/images/loadingimage1.gif" /> <strong style="color:red;">  Updating, Please Wait...</strong>
  </div>
                    </td>

                </tr>




            </table>

        </div>
      
        <div id="maindatatableMain">
           
           
                        <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
              
                <tr>
                    <td height="25px" class="section_headings"> ITEMS SUMMARY</td>
                </tr>


                <tr>
                    <td class="white-box-outline">
                        <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                            <tr>
                                <td height="20px">&nbsp;</td>
                            </tr>

                            <tr>
                                <td>

                                    <div class="container">
                                        <table cellpadding="0" width="1000" cellspacing="0" border="0" class="display" id="noordercompany" style="cursor: pointer">
                                            <thead>
                                                <tr>
                                                     <th align="left">Credit Id</th>
                                                     <th align="left">Credit Id</th>
                                                    <th align="left">Created Date</th>
                                                     <th align="left">Supplier Name</th>
                                                    <th align="left">Supplier Code</th>
                                                    <th >Item Description</th>
                                                    <th align="left">QTY</th>
                                                    <th align="left">Unit Price</th>
                                                    <th align="left">COG</th>
                                                </tr>

                                            </thead>




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

       
           
           
                        <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable" style="display:none;">
              
                <tr>
                    <td height="25px" class="section_headings">ORDER ITEMS SUMMARY</td>
                </tr>


                <tr>
                    <td class="white-box-outline">
                        <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                            <tr>
                                <td height="20px">&nbsp;</td>
                            </tr>

                            <tr>
                                <td>

                                    <div class="container">
                                        <table cellpadding="0" width="1000" cellspacing="0" border="0" class="display" id="noordercompanyhistory" style="cursor: pointer">
                                            <thead>
                                                <tr>
                                                     <th align="left">Order Id</th>
                                                     <th align="left">Order Id</th>
                                                    <th align="left">Order Date</th>
                                                     <th align="left">Supplier Name</th>
                                                    <th align="left">Supplier Code</th>
                                                    <th >Item Description</th>
                                                    <th align="left">QTY</th>
                                                    <th align="left">Unit Price</th>
                                                    <th align="left">COG</th>
                                                </tr>

                                            </thead>




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
        function callUpdateCompany() {
            selecteditems = [];


            $("#noordercompany").find("input[name='selectchk']:checked").each(function (i, ob) {

                var comId = $(ob).val();
                var statu = getlockeCompanyStatus(comId);
                var coN = new ComSelect();
                coN.companyId = comId;
                coN.selected = statu
                selecteditems.push(coN);

            });

            if (selecteditems.length == 0) {
                alert("Please tick the checkbox");
                return;
            }
            ShowProgressBar();
            PageMethods.UpdateCompany(selecteditems, updateCompanyResult);

            return false;
        }

        function ComSelect() {

            this.companyId = "0";
            this.selected = false;

        }

        function getlockeCompanyStatus(comID) {
            var canFind = false;
            $("#noordercompany").find("input[name='selectlock']").each(function (i, ob) {

                var objVal = $(ob).val();

                if (objVal == comID) {
                    if ($(this).is(":checked")) {
                        canFind = true;

                    }
                }

            });

            return canFind;
        }

        function updateCompanyResult(res) {
            HideProgressBar();
            alert("Updated Successfully.");
            //location.reload();
            // table.ajax.reload();
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