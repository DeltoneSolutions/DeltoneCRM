<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_items.aspx.cs" Inherits="DeltoneCRM.Manage.edit_items" MasterPageFile="~/SiteInternalNavLevel1.Master" %>

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
            width:1200px;
        }
        .width-940-style {
            width: 1100px;
        }
        .width-960-style {
            width: 960px;
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
        var managerpageClicked;
        //Edit Item Window
        function Edit(ItemID) {
            managerpageClicked = "yes";
            $('#editiframe').attr('src','ViewEditScreens/ViewEditItem.aspx?ItemID=' + ItemID);
            ViewEditDialog= $('#EditWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT ITEM',
                width: 810,
                height: 650
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

            $('#example').dataTable({
                processing: true,
                "language": {
                    "processing": "Hang on. Waiting for response..." //add a loading image,simply putting <img src="loader.gif" /> tag.
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
                    { 'data': 'ViewEdit' },
                ],
                
                sAjaxSource: '../ItemsDataHandler.ashx',
               
                "iDisplayLength": 25,
                "columnDefs": [
                    { className: 'align_center', "targets": [0, 1, 4, 5, 6, 7] },
                    { className: 'align_left', "targets": [2, 3] },
                ],
                dom: 'lBfrtip',
                buttons: [
                    'print'
                ]
            });

            
        });
    </script>
    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
        <tr>
            <td height="25px">&nbsp;</td>
        </tr>
        <tr>
            <td class="top-links-box">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
    <asp:Button ID="Button1" runat="server" OnClientClick="return CreateAddWindow();" Text="ADD NEW PRODUCT" class="top-links-button" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="tbl-top-bg">&nbsp;</td>
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
                            <th width="50px">VIEW/EDIT</th>
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
        <tr>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td height="25px">&nbsp;</td>
        </tr>
    </table>
    
    <div id="CreateAddWindow" style="display:none">
   <iframe id="createiframe" width="710" height="350"style="border:0px;"></iframe>   
</div>


 <div id="EditWindow" style="display:none">
         <iframe id="editiframe" width="800" height="650"style="border:0px;"></iframe>  
 </div>



    </asp:Content>