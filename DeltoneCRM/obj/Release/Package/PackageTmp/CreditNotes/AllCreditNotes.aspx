<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllCreditNotes.aspx.cs" Inherits="DeltoneCRM.CreditNotes.AllCreditNotes" MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">


    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="//cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>

    <style type="text/css">
        body {
            background-color: #FFFFCC !important;
        }

        .inner-table-size {
            width: 940px;
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
            float: left;
            text-align: right;
            color: #4fc2bd !important;
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


        .awaitrma {
        }

        .button3 {
            background-color: #4CAF50;
        }
        /* Green */
        .button2 {
            background-color: #008CBA;
        }
        /* Blue */
        .button4 {
            background-color: #f44336;
        }
        /* Red */
        .button1 {
            background-color: #e7e7e7;
        }
        /* Gray */
        .button5 {
            background-color: #555555;
        }
        /* Black */
        .button0 {
            background-color: white;
        }
        /* Black */

        .tab01-01 {
            font-family: 'Raleway', sans-serif;
            color: #000;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #f44336;
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





        .tabclicked {
            font-family: 'Raleway', sans-serif;
            color: #000;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #66ccff;
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
            color: #000;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #e7e7e7;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            background-color: #FFFF00;
        }

            .tab01-01Noclicked:hover {
                color: #000;
                background-color: #c4dde6;
                cursor: pointer;
            }
    </style>
    <script type="text/javascript">


        function openWin(CreditNoteID, ContID, CompID) {
            var url = '../UpdateCredit.aspx?CreditNoteID=' + encodeURIComponent(CreditNoteID) + '&cid=' + encodeURIComponent(ContID) + '&Compid=' + encodeURIComponent(CompID);
            window.open(url);
        }

        var table;
        var selecteditems = [];
        function getSelectedCheckBoxValues() {
            selecteditems = []
            $("#example").find("input:checked").each(function (i, ob) {
                selecteditems.push($(ob).val());
            });

          //  console.log(selecteditems.length);
            if (selecteditems.length == 0) {
                alert("Please tick the checkbox");
                return;
            }

            PageMethods.CreateCSVANDEMAILSupplier(selecteditems, createCSVEmail);
        }

        function createCSVEmail() {

            alert("Email has been sent successfully.");

        }


        function selectCheckBox() {

            $('#select-all').on('click', function () {
                // Check/uncheck all checkboxes in the table
                var rows = table.rows({ 'search': 'applied' }).nodes();
                console.log(rows);
                $('input[type="checkbox"]', rows).prop('checked', this.checked);
            });
        }

        $(document).ready(function () {

            selectCheckBox();

            rmaAll();

            searchCustomerFaultyMe();

            searchCustomerSuppMe();

            $('#findcustomerorder').autocomplete({
                source: "../Fetch/FetchSearchOrder.aspx",
                select: function (event, ui) {
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    // $('#CompanyContactTR').show();
                    // $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    var valsSPLIT = ui.item.id.split(',');;

                    var url = '../order.aspx?Oderid=' + (valsSPLIT[0]) + '&cid=' + (valsSPLIT[2])
                            + '&Compid=' + (valsSPLIT[1]);
                    window.open(url, "_blank");
                    $(this).val(''); return false;


                }
            });

        });


        function callTableData(param) {

            // var param = getParameterByName("s");
            if (param == null)
                param = "5";
            if (table)
                table.destroy();
            table = $('#example').DataTable({
                processing: true,
                "language": {
                    "processing": "Hang on. Waiting for response..." //add a loading image,simply putting <img src="loader.gif" /> tag.
                },

                "ajax": "../Fetch/FetchAllCreditNotes.aspx",
                "order": [[0, "desc"]],
                fnServerParams: function (aoData) {
                    aoData.push(
                        { "name": "s", "value": param }
                    );
                },
                dom: 'lBfrtip',
                buttons: [
                    'print'
                ],
                "iDisplayLength": 25,
                'columnDefs': [
        //hide the second  column
        { 'visible': false, 'targets': [1] },
        { "width": "15%", "targets": 2 },
        { "width": "7%", "targets": 3 },
                { "width": "8%", "targets": 4 },
                { type: 'date', targets: [7] }
                ]

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




        function searchCustomerSuppMe() {

            $('#suppsearch').on('keyup', function () {
                table
                    .columns(10)                       // supp name index
                    .search(this.value)
                    .draw();
            });
        }

        function searchCustomerFaultyMe() {


            $('#faultType').on('keyup', function () {
                table
                    .columns(8)
                    .search(this.value)
                    .draw();
            });
        }



        function searchOption() {

            $('#example tfoot th').each(function () {
                var title = $(this).text();
                $(this).html('<input type="text" placeholder="Search ' + title + '" />');
            });

            // DataTable
            var table = $('#example').DataTable();

            // Apply the search
            table.columns().every(function () {
                var that = this;

                $('input', this.footer()).on('keyup change', function () {
                    if (that.search() !== this.value) {
                        that
                            .search(this.value)
                            .draw();
                    }
                });
            });
        }

        function removeTabClicked() {

            $('#awaitrma').removeClass("tabclicked");
            $('#reievedrma').removeClass("tabclicked");
            $('#creditpending').removeClass("tabclicked");
            $('#creditxero').removeClass("tabclicked");
            $('#credittracking').removeClass("tabclicked");
            $('#trackingreceived').removeClass("tabclicked");
            $('#rmaCompleted').removeClass("tabclicked");
            $('#rmaall').removeClass("tabclicked");
            $('#notcompleterma').removeClass("tabclicked");
        }

        function aWaitRMA() {

            // CreateTableData("so");

            removeTabClicked();

            $('#awaitrma').addClass("tabclicked");

            $('#reievedrma').addClass("tab01-01");
            $('#creditpending').addClass("tab01-01");
            $('#creditxero').addClass("tab01-01");
            $('#credittracking').addClass("tab01-01");
            $('#trackingreceived').addClass("tab01-01");
            $('#rmaCompleted').addClass("tab01-01");
            $('#rmaall').addClass("tab01-01");
            $('#notcompleterma').addClass("tab01-01");
            callTableData(2);
        }
        function reievedRma() {

            // CreateTableData("so");

            removeTabClicked();

            $('#reievedrma').addClass("tabclicked");

            $('#awaitrma').addClass("tab01-01");
            $('#creditpending').addClass("tab01-01");
            $('#creditxero').addClass("tab01-01");
            $('#credittracking').addClass("tab01-01");
            $('#trackingreceived').addClass("tab01-01");
            $('#rmaCompleted').addClass("tab01-01");
            $('#rmaall').addClass("tab01-01");
            $('#notcompleterma').addClass("tab01-01");
            callTableData(7)
        }
        function creditPending() {

            // CreateTableData("so");

            removeTabClicked();

            $('#creditpending').addClass("tabclicked");

            $('#reievedrma').addClass("tab01-01");
            $('#awaitrma').addClass("tab01-01");
            $('#creditxero').addClass("tab01-01");
            $('#credittracking').addClass("tab01-01");
            $('#trackingreceived').addClass("tab01-01");
            $('#rmaCompleted').addClass("tab01-01");
            $('#rmaall').addClass("tab01-01");
            $('#notcompleterma').addClass("tab01-01");
            callTableData(1)
        }



        function creditXero() {

            removeTabClicked();

            $('#creditxero').addClass("tabclicked");

            $('#reievedrma').addClass("tab01-01");
            $('#creditpending').addClass("tab01-01");
            $('#awaitrma').addClass("tab01-01");
            $('#credittracking').addClass("tab01-01");
            $('#trackingreceived').addClass("tab01-01");
            $('#rmaCompleted').addClass("tab01-01");
            $('#rmaall').addClass("tab01-01");
            $('#notcompleterma').addClass("tab01-01");
            callTableData(4)
        }
        function awaitingTracking() {

            // CreateTableData("so");

            removeTabClicked();

            $('#credittracking').addClass("tabclicked");

            $('#reievedrma').addClass("tab01-01");
            $('#creditpending').addClass("tab01-01");
            $('#creditxero').addClass("tab01-01");
            $('#awaitrma').addClass("tab01-01");
            $('#trackingreceived').addClass("tab01-01");
            $('#rmaCompleted').addClass("tab01-01");
            $('#rmaall').addClass("tab01-01");
            $('#notcompleterma').addClass("tab01-01");
            callTableData(3);
        }
        function trackingReceived() {

            // CreateTableData("so");

            removeTabClicked();

            $('#trackingreceived').addClass("tabclicked");

            $('#reievedrma').addClass("tab01-01");
            $('#creditpending').addClass("tab01-01");
            $('#creditxero').addClass("tab01-01");
            $('#credittracking').addClass("tab01-01");
            $('#awaitrma').addClass("tab01-01");
            $('#rmaCompleted').addClass("tab01-01");
            $('#rmaall').addClass("tab01-01");
            $('#notcompleterma').addClass("tab01-01");
            callTableData(8);
        }
        function rmaCompleted() {

            // CreateTableData("so");

            removeTabClicked();

            $('#rmaCompleted').addClass("tabclicked");

            $('#reievedrma').addClass("tab01-01");
            $('#creditpending').addClass("tab01-01");
            $('#creditxero').addClass("tab01-01");
            $('#credittracking').addClass("tab01-01");
            $('#trackingreceived').addClass("tab01-01");
            $('#awaitrma').addClass("tab01-01");
            $('#rmaall').addClass("tab01-01");
            $('#notcompleterma').addClass("tab01-01");
            callTableData(6);
        }
        function rmaAll() {

            // CreateTableData("so");

            removeTabClicked();

            $('#rmaall').addClass("tabclicked");

            $('#reievedrma').addClass("tab01-01");
            $('#creditpending').addClass("tab01-01");
            $('#creditxero').addClass("tab01-01");
            $('#credittracking').addClass("tab01-01");
            $('#trackingreceived').addClass("tab01-01");
            $('#rmaCompleted').addClass("tab01-01");
            $('#awaitrma').addClass("tab01-01");
            $('#notcompleterma').addClass("tab01-01");
            callTableData(5);
        }

        function noComplete() {

            // CreateTableData("so");

            removeTabClicked();

            $('#notcompleterma').addClass("tabclicked");

            $('#rmaall').addClass("tab01-01");
            $('#reievedrma').addClass("tab01-01");
            $('#creditpending').addClass("tab01-01");
            $('#creditxero').addClass("tab01-01");
            $('#credittracking').addClass("tab01-01");
            $('#trackingreceived').addClass("tab01-01");
            $('#rmaCompleted').addClass("tab01-01");
            $('#awaitrma').addClass("tab01-01");

            callTableData(9);
        }
    </script>
</asp:Content>



<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagerupdateReportCredit" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div>

        <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

            <tr>
                <td height="25px">&nbsp;</td>
            </tr>

            <tr>

                <td class="section_headings">
                    <%-- <a class="awaitrma" href="AllCreditNotes.aspx?s=2">AWAITING RMA</a>
                     |<a class="reievedrma" href="AllCreditNotes.aspx?s=7">RMA RECEIVED</a>
                    |<a class="creditpending" href="AllCreditNotes.aspx?s=1">CREDIT PENDING</a>
                     | <a class="creditxero" href="AllCreditNotes.aspx?s=4">CREDIT IN XERO</a>
                    | <a class="credittracking" href="AllCreditNotes.aspx?s=3">AWAITING TRACKING</a>
                   | <a class="trackingreceived" href="AllCreditNotes.aspx?s=8">TRACKING RECEIVED</a>
                    |<a class="rmaCompleted" href="AllCreditNotes.aspx?s=6">COMPLETED</a>
                    |<a class="rmaall" href="AllCreditNotes.aspx?s=5">ALL</a>--%>





                    <table>
                        <tr>
                            <td width="200px" class="tab01-01" id="notcompleterma" onclick="noComplete();">NOT COMPLETED</td>
                            <td width="200px" class="tab01-01" id="awaitrma" onclick="aWaitRMA();">AWAITING RMA</td>
                            <td width="200px" class="tab01-01" id="reievedrma" onclick="reievedRma();">RMA RECEIVED</td>
                            <td width="200px" class="tab01-01" id="credittracking" onclick="awaitingTracking();">AWAITING TRACKING</td>
                            <td width="200px" class="tab01-01" id="trackingreceived" onclick="trackingReceived()">TRACKING RECEIVED</td>
                            <td width="200px" class="tab01-01" id="creditpending" onclick="creditPending();">CREDIT PENDING</td>
                            <td width="200px" class="tab01-01" id="creditxero" onclick="creditXero();">CREDIT IN XERO</td>
                            <td width="200px" class="tab01-01" id="rmaCompleted" onclick="rmaCompleted()">COMPLETED</td>
                            <td width="200px" class="tab01-01" id="rmaall" onclick="rmaAll()">ALL</td>
                        </tr>
                    </table>
                </td>

            </tr>

            <tr>
                <td class="white-box-outline">
                    <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                        <tr>
                            <td height="10px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td height="20px" width="40px;"><span class="inputTextStyle" style="margin-right: 6px;">Search By Supplier Name  :  </span>
                                <input class="inputColorGreen" type="text" id="suppsearch" />
                            </td>
                        </tr>
                        <tr>
                            <td height="10px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td height="20px" width="40px;">
                                <span class="inputTextStyle" style="margin-right: 24px;">Search By Faulty Type  : </span>
                                <input class="inputColorGreen" type="text" id="faultType" />
                                <input type="button" value="EXPORT CSV & EMAIL TO SUPPLIER" onclick="getSelectedCheckBoxValues();" style="color: red; width: 280px; height: 35px; margin-left: 20px;" />
                            </td>

                        </tr>
                        <tr>
                            <td height="10px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>

                                <div class="container">
                                    <table cellpadding="0" cellspacing="0" border="0" class="display" id="example" style="cursor: pointer">
                                        <thead>
                                            <tr>
                                                <th align="left">
                                                    <input name="select_all" id="select-all" value="1" type="checkbox" /></th>
                                                <th align="left">CREDIT_NOTE NO</th>
                                                <th align="left">COMPANY</th>
                                                <th align="left">CONCTACT PERSON</th>
                                                <th align="left">ORDER NO</th>
                                                <th align="left">INVOICE NO</th>
                                                <th align="left">TOTAL</th>
                                                <th align="left">CREATED DATE</th>
                                                <th align="left">TYPE OF CREDIT</th>
                                                <th align="left">CREATED BY</th>
                                                <th align="left">SUPPLIER NAME</th>
                                                <th align="left">STATUS</th>
                                                <th align="left">EDIT</th>
                                                <th align="left">VIEW RMA</th>
                                            </tr>

                                        </thead>

                                        <tbody>
                                        </tbody>

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

            <tr>
                <td height="25px">&nbsp;</td>
            </tr>
        </table>

    </div>



    <div id="EditRMA" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="RMA STATUS">

                   <strong> <span id="nameCom" style="font-size:16px;"></span></strong>

        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <thead>
                <tr style="height: 7px;">
                <br />

                    <td style="width: 25px;"></td>
                    <td>&nbsp;</td>
                    <td style="width: 25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="display: none;">
                    <td>Supplier Name:</td>
                    <td>
                        <asp:DropDownList ID="DropDownListRMAEdit" ClientIDMode="Static" runat="server" Width="200px" Height="30px"></asp:DropDownList>
                        <br />
                    </td>

                </tr>


                <tr>
                    <td style="font-size: 16px; text-align: center; width: 150px;">Sent To Supplier</td>

                    <td style="font-size: 16px; text-align: center; word-wrap: break-word; width: 120px;">RMA has been approved</td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center;">Credited in Xero</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center;">In House</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center; width: 150px;">RMA Sent to Customer</td>
                    <td style="width: 20px;">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center; word-wrap: break-word; width: 190px;">Adjusted Note Received From Supplier</td>
                     <td style="width: 20px;">&nbsp;</td>
                    <td style="font-size: 16px; text-align: center; word-wrap: break-word; width: 190px;">Completed</td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <div class="squaredThree" style="margin-left: 45px;">
                            <input type="checkbox" id="chk_S2S" style="vertical-align: middle; display: inline;" /><label for="chk_S2S" />
                        </div>
                    </td>

                    <td>
                        <div class="squaredThree" style="margin-left: 45px;">
                            <input type="checkbox" id="chk_AR" style="vertical-align: middle;" /><label for="chk_AR" />
                        </div>
                    </td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 25px;">
                            <input type="checkbox" id="chk_CIX" style="vertical-align: middle;" /><label for="chk_CIX" />
                        </div>
                    </td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 45px;">
                            <input type="checkbox" id="chk_InH" style="vertical-align: middle;" /><label for="chk_InH" />
                        </div>
                    </td>
                    <td style="width: 10px;">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 45px;">
                            <input type="checkbox" id="chk_R2C" style="vertical-align: middle;" /><label for="chk_R2C" />
                        </div>
                    </td>
                    <td style="width: 20px;">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 55px;">
                            <input type="checkbox" id="chk_ANFS" style="vertical-align: middle;" /><label for="chk_ANFS" />
                        </div>
                    </td>
                     <td style="width: 20px;">&nbsp;</td>
                    <td>
                        <div class="squaredThree" style="margin-left: 55px;">
                            <input type="checkbox" id="chk_Completed" style="vertical-align: middle;" /><label for="chk_Completed" /></div>
                    </td>
                </tr>

            </tbody>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" align="center" style="width: 1000px;" align="center">
            <tr>
                <td>&nbsp;</td>
                <td>
                    <input type="text" id="CreditNoteID" hidden="hidden" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 250px;">&nbsp;</td>
                <td style="width: 500px; text-align: center; font-size: 16px;">Supplier RMA Number</td>
                <td style="width: 250px;">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center;">
                    <input type="text" id="RMANumber" name="RMANumber" class="txtbox_format" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center; font-size: 16px;">Tracking Number</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center;">
                    <input type="text" id="TrackingNumber" name="TrackingNumber" class="txtbox_format" /></td>
                <td>&nbsp;</td>
            </tr>
              <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center; font-size: 16px;">Notes</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center;">​<textarea id="rmaNotes" name="rmaNotes" rows="10" cols="70"></textarea>
                </td>
                <td>&nbsp;</td>
            </tr>
            
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px; text-align: center; font-size: 16px;">Notes History</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width: 500px;">​<div id="displayDiv" 
                    runat="server" style=" border:1px solid gray; font: medium -moz-fixed; font: -webkit-small-control;
    height: 120px;
    overflow: auto;
    padding: 2px;
    resize: both;
    width: 500px;word-wrap:break-word;display:inline-block;"></div>
                </td>
                <td>&nbsp;</td>
            </tr>


        </table>
        <br />
        <br />
        <table cellpadding="0" cellspacing="0" border="0" align="center" style="width: 1000px;" align="center">
            <tr>
                <td style="text-align: center;">
                    <input type="button" id="btn_SaveRMAChanges" value="UPDATE" class="btn" /></td>
            </tr>
        </table>

        <br />
        <br />
    </div>


    <script type="text/javascript">

        $(document).ready(function () {

            showEditRMAWindow();
            updateRMAAData();
        });


        function showEditRMAWindow() {

            $('#EditRMA').dialog({
                resizable: false,
                modal: true,
                title: 'RMA REPORT STATUS',
                width: 1200,
                autoOpen: false,
            });
        }

        



        function getval(cel) {

            alert(cel.innerHTML);

        }

        function showRMAWindow(creID) {


            $('#example tbody').on('click', 'tr', function () {
                var ccc = table.row(this).data();
               // ccc = ccc[2];
               // console.log(ccc);
                $("#nameCom").text(ccc[2] + "-->" + ccc[4] + "-->" + ccc[5]);
            });

           
            var orderIdQstring = creID;
            $('#CreditNoteID').val(orderIdQstring);
            // showRMASupplier(creID, suNames)
            // var suppNameEdit = $("#DropDownListRMAEdit").val();
            EditRMAData(orderIdQstring);
            $('#EditRMA').dialog('open');
            return false;

        }

        function updateRMAAData() {
            $('#btn_SaveRMAChanges').click(function () {
                $.ajax({
                    url: '/Process/EditRMA.aspx',
                    data: {
                        QID: $('#CreditNoteID').val(),
                        STS: $('#chk_S2S').is(':checked'),
                        ARMA: $('#chk_AR').is(':checked'),
                        CIX: $('#chk_CIX').is(':checked'),
                        RTC: $('#chk_R2C').is(':checked'),
                        ANFS: $('#chk_ANFS').is(':checked'),
                        INHouse: $('#chk_InH').is(':checked'),
                        SRMAN: $('#RMANumber').val(),
                        TN: $('#TrackingNumber').val() ,
                        Notes: $('#rmaNotes').val(),
                        chk_Completed: $('#chk_Completed').is(':checked')

                    },
                    success: function (response) {
                        alert("Successfully Updated.");
                        $('#EditRMA').dialog('close');
                        location.reload();
                    }
                });



                //alert("sending " + eventToAdd.title);
                //var firstDropVal = $('#DropDownListRMAEdit').val();
                //var orderIdQstring = getParameterByName("CreditNoteID");
                //var RMAUpdate = {
                //    CreId: orderIdQstring,
                //    STS: $('#chk_S2S').is(':checked'),
                //    SuppName: firstDropVal,
                //    ARMA: $('#chk_AR').is(':checked'),
                //    RTC: $('#chk_R2C').is(':checked'),
                //    CIX: $('#chk_CIX').is(':checked'),
                //    ANFS: $('#chk_ANFS').is(':checked'),
                //    INHouse: $('#chk_InH').is(':checked'),
                //    SRMAN: $('#RMANumber').val(),
                //    TN: $('#TrackingNumber').val()

                //};
                //PageMethods.RMAUpdateMe(RMAUpdate, addRMASuccess);



            });
        }

        function addRMASuccess(res) {
            alert("RMA Successfully Updated.");
           // location.reload();
            table.ajax.reload();
        }

        function showRMASupplier(cId, suNames) {

            var splitSNames = suNames.split(',');
            for (i = 0; i < splitSNames.length; i++) {
                var newOption = "<option value='" + splitSNames[i] + "'>" + splitSNames[i] + "</option>";
                $("#DropDownListRMAEdit").append(newOption);
            }




        }


        function EditRMAData(CID) {
            $('#CreditNoteID').val(CID);
            $.ajax({
                url: '/Fetch/FetchRMADetails.aspx',
                data: {
                    RID: CID,
                },
                success: function (response) {
                    var splitString = response.split('|');
                    if (splitString[0] == '1') {
                        $('#chk_S2S').prop('checked', true);
                    }
                    else {
                        $('#chk_S2S').prop('checked', false);
                    }

                    if (splitString[1] == '1') {
                        $('#chk_AR').prop('checked', true);
                    }
                    else {
                        $('#chk_AR').prop('checked', false);
                    }

                    if (splitString[2] == '1') {
                        $('#chk_CIX').prop('checked', true);
                    }
                    else {
                        $('#chk_CIX').prop('checked', false);
                    }

                    if (splitString[3] == '1') {
                        $('#chk_R2C').prop('checked', true);
                    }
                    else {
                        $('#chk_R2C').prop('checked', false);
                    }

                    if (splitString[4] == '1') {
                        $('#chk_ANFS').prop('checked', true);
                    }
                    else {
                        $('#chk_ANFS').prop('checked', false);
                    }



                    $('#RMANumber').val(splitString[5]);
                    $('#TrackingNumber').val(splitString[6]);

                    if (splitString[7] == '1') {
                        $('#chk_InH').prop('checked', true);
                    }
                    else {
                        $('#chk_InH').prop('checked', false);

                    }

                    if (splitString[8] != "")
                        $('#ContentPlaceHolder1_displayDiv').html(splitString[8]);
                    else
                        $('#ContentPlaceHolder1_displayDiv').text('');
                    if (splitString[9] == "COMPLETED") {
                        $('#chk_Completed').prop('checked', true);
                    }
                    else {
                        $('#chk_Completed').prop('checked', false);
                    }
                }
            })

        }

        function format(d) {
            // `d` is the original data object for the row
            return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                '<tr>' +
                    '<td>Sent To Supplier Date Time:</td>' +
                    '<td>' + d.SentToSupplierDateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Approved RMA Date Time:</td>' +
                    '<td>' + d.ApprovedRMADateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Credit In Xero Date Time:</td>' +
                    '<td>' + d.CreditInXeroDateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Sent To Customer Date Time:</td>' +
                    '<td>' + d.RMAToCustomerDateTime + '</td>' +
                '</tr>' +
                 '<tr>' +
                    '<td>Adjustment Notice Date Time:</td>' +
                    '<td>' + d.AdjustedNoteFromSupplierDateTime + '</td>' +
                '</tr>' +
            '</table>';
        }

    </script>

</asp:Content>

