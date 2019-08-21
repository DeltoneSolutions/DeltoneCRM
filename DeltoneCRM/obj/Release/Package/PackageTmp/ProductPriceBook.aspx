<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductPriceBook.aspx.cs"  MasterPageFile="~/SiteInternalNavLevel1.Master" Inherits="DeltoneCRM.ProductPriceBook" %>



<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-2.1.3.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="../css/Overall.css" rel="stylesheet" />
    
  <%--  <script src="../js/jquery.dataTables.min.js"></script>--%>

     <script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js">
    </script>


    <style type="text/css">

        .width-980-style {
            width: 1600px;
        }
        .width-940-style {
            width: 1400px;
        }
        .width-960-style {
            width: 1500px;
        }
div.dataTables_length {
    float: left;
    background-color: cccccc;
}
 
div.dataTables_filter {
    float: right;
    background-color: cccccc;
}
 
div.dataTables_info {
    float: left;
    background-color: cccccc;
}
 
div.dataTables_paginate {
    float: right;
    background-color: cccccc;
}
 
div.dataTables_length,
div.dataTables_filter,
div.dataTables_paginate,
div.dataTables_info {
    padding: 6px;
}
/* Footer cells */ 
table.pretty tfoot th {
    background: #cccccc;
    text-align: left;
}
 
table.pretty tfoot td {
    background: #cccccc;
    text-align: center;
    font-weight: bold;
}
div.dataTables_wrapper {
    background-color: #e2e1e1;
}
        .tbl-top-bg {
            background-color: #e2e1e1;
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

         .section_headings{
             background-color:#E2E1E1 !important;
           
              border-top-style:none !important;
              margin-bottom:6px !important;


         }

         table.dataTable tbody th, table.dataTable tbody td{
                 border-top-color: black !important; 


         }
    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>

    <script type="text/javascript">


        var ViewEditDialog;

        function closeIframe() {
            $('#EditWindow').dialog('close');
            return false;
        }

        function closeAddFrame() {
            $('#CreateAddWindow').dialog('close');
            location.reload();
        }

        //Edit Item Window
        function Edit(ItemID) {
            $('#editiframe').attr('src', 'ViewEditScreens/ViewEditItem.aspx?ItemID=' + ItemID);
            ViewEditDialog = $('#EditWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT ITEM',
                width: 710,
            });


            return false;
        }

        function closeEditWindow() {
            ViewEditDialog.dialog("close");
        }

        function CreateAddWindow() {
            $('#createiframe').attr('src', 'additem.aspx');
            $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'NEW ITEM',
                width: 710,
            });

            return false;
        }

        $(document).ready(function () {

            callFirstTableData();
            callSecondTableData();


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
                    $(this).val(''); return false;


                }
            });
        });

        function callFirstTableData() {

            $('#example').dataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response...</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                columns: [
                    { 'data': 'ItemID' },
                    { 'data': 'SupplierName' },
                    { 'data': 'ProductCode' },
                    { 'data': 'Description' },
                    { 'data': 'PrinterCompatibility' },
                    { 'data': 'COG' },
                     { 'data': 'DSB' },
                    { 'data': 'ManagerUnitPrice' },
                    { 'data': 'Active' },
                     { 'data': 'Qty' },

                ],

                sAjaxSource: '../ItemsDataHandler.ashx?r=1',

                "iDisplayLength": 10,
                "columnDefs": [
                    { className: 'align_center', "targets": [0, 1, 2, 5, 6, 7, 8] },
                    { className: 'align_left', "targets": [3, 4] },
                ],
                dom: 'lBfrtip',
                buttons: [
                    'print'
                ]
            });
        }

        function callSecondTableData() {

            $('#pricebooktwo').dataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response...</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                columns: [
                    { 'data': 'ItemID' },
                    { 'data': 'SupplierName' },
                    { 'data': 'ProductCode' },
                    { 'data': 'Description' },
                    { 'data': 'PrinterCompatibility' },
                    { 'data': 'COG' },
                     { 'data': 'DSB' },
                    { 'data': 'ManagerUnitPrice' },
                    { 'data': 'Active' },
                     { 'data': 'Qty' },

                ],

                sAjaxSource: '../ItemsDataHandler.ashx?r=1',
                "aaSorting": [[4, "desc"]],
                "iDisplayLength": 10,
                "columnDefs": [
                    { className: 'align_center', "targets": [0, 1, 2, 5, 6, 7, 8] },
                    { className: 'align_left', "targets": [3, 4] },
                ],
                dom: 'lBfrtip',
                buttons: [
                    'print'
                ]
            });


        }
    </script>
    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
       
      
        <tr>
            <td height="25px">&nbsp;</td>
        </tr>
      
        <tr>
            <td class="tbl-top-bg">&nbsp;</td>
        </tr>
         <tr >
            <td height="25px" class="section_headings">PRICE BOOK</td>
        </tr>
      
        <tr class="tbl-top-bg">
            <td>


                <table align="center" cellpadding="0" cellspacing="0" class="width-960-style">

                    <tr>
                        <td>

                            <table id="example" >

                    <thead>
                        <tr>
                            <th>Item ID</th>
                            <th>SUPPLIER</th>
			                <th>PRODUCT CODE</th>
                            <th>DESCRIPTION</th>
                              <th>MODEL</th>
                            <th>COST</th>
                             <th>DSB</th>
                            <th>RESELL PRICE</th>
			                <th>ACTIVE</th>
                            <th>Quantity</th>
                           
                            <!--Get the Hidden Order ID and Contact ID-->

                        </tr>
                    </thead>

                    <tbody>
		 
	                </tbody>
            </table>

                        </td>
                    </tr>
                    <tr>
                        <td height="20px">&nbsp;</td>
                    </tr>
                </table>




            </td>
        </tr>
      
    </table>
    
   

     <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
        <tr>
            <td height="25px">&nbsp;</td>
        </tr>
      
        <tr>
            <td class="tbl-top-bg">&nbsp;</td>
        </tr>
         <tr >
            <td height="25px" class="section_headings">PRICE BOOK</td>
        </tr>
      
        <tr class="tbl-top-bg">
            <td>




                <table align="center" cellpadding="0" cellspacing="0" class="width-960-style">

                    <tr>
                        <td>

                            <table id="pricebooktwo" >

                    <thead>
                        <tr>
                            <th>Item ID</th>
                            <th>SUPPLIER</th>
			                <th>PRODUCT CODE</th>
                            <th>DESCRIPTION</th>
                              <th>MODEL</th>
                            <th>COST</th>
                             <th>DSB</th>
                            <th>RESELL PRICE</th>
			                <th>ACTIVE</th>
                            <th>Quantity</th>
                           
                            <!--Get the Hidden Order ID and Contact ID-->

                        </tr>
                    </thead>

                    <tbody>
		 
	                </tbody>
            </table>

                        </td>
                    </tr>
                    
                </table>




            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td height="25px">&nbsp;</td>
        </tr>
    </table>

    </asp:Content>
