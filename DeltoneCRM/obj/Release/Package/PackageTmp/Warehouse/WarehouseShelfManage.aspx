<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="WarehouseShelfManage.aspx.cs" Inherits="DeltoneCRM.Warehouse.WarehouseShelfManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <script src="../js/jquery-1.11.1.min.js"></script>
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>
    <script src="../js/jquery-1.11.1.min.js"></script>
    <link href="../css/NewCSS.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui.js"></script>
     <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-2.1.3.js"></script>
	
    <%-- <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>--%>
    <script src="../js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/jquery.dataTables_new.css" />

    <%-- <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
  
  
    <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>
    <%--  <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
	</script>--%>
    <%--	<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>--%>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
   <%-- <script src="Scripts/date-uk.js"></script>--%>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />
     <style type='text/css'>
        /* css for timepicker */

         .ui-widget-overlay {
             background:none !important;
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
            padding-top: 4px;
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

        .undoButtonStyle {
            margin-left: 30px;
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
            float: right;
        }

        .inputTextStyle {
            float: right;
            text-align: right;
            color: #4fc2bd !important;
        }

        #drop_zone {
            max-width: 40%;
            margin: 0 auto;
            width: 40%;
            min-height: 150px;
            text-align: center;
            text-transform: uppercase;
            font-weight: bold;
            font-size: 30px;
            border: 8px dashed #3E3E42;
            height: 160px;
        }

        #spanConatt {
            display: block;
            vertical-align: middle;
            line-height: normal;
            margin-top: 50px;
            opacity: 0.3;
        }

        td.highlight {
            font-weight: bold;
            color: red !important;
        }

        td.highlightclicked {
            font-weight: bold;
            color: #4274f4 !important;
        }

        /*tr.odd { background-color: blue; } 
 tr.even { background-color: green; }

table.dataTable tr.odd .sorting_1 { background-color: blue; } 
table.dataTable tr.even .sorting_1 { background-color: green; }*/


        table.display tbody tr.myeven {
            background-color: #f2dede !important;
        }

        table.display tbody tr.myodd {
            background-color: #bce8f1 !important;
        }

        .marLfet{

            margin-right:30px;
        }
    </style>


    <script type="text/javascript">
        function CreateAddWindow() {
            $('#createiframe').attr('src', 'AddNewShelf.aspx');
            $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'NEW ITEM',
                width: 710,
            });

            return false;
        }

        function closeAddFrame() {
            $('#CreateAddWindow').dialog('close');
            location.reload();
        }

        $(document).ready(function () {

            var table = $('#shelfTable').DataTable({
                "ajax": "Fetch/fetchallShelf.aspx",
                "aaSorting": [[0, "desc"]],
                "columnDefs": [
                        {
                            className: 'align_center', "targets": [0, 1, 2],
                        }


                ],
                "iDisplayLength": 25,
               
            });

        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
        <tr>
            <td height="25px">&nbsp;</td>
        </tr>
         <tr>
          <td height="25px"><asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle setmar" CausesValidation="false" />
                    <asp:Button OnClick="btnupload_Click" ID="Button3" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />
           <asp:Button OnClick="ShelfView" ID="Button4" Text="VIEW ITEMS" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight marLfet" CausesValidation="false" />         
          </td>
        </tr>
        <tr>
            <td class="top-links-box">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
    <asp:Button ID="Button1" runat="server" OnClientClick="return CreateAddWindow();" Text="ADD NEW SHELF" class="top-links-button" />
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

                            <table id="shelfTable" cellpadding="0" width="900" cellspacing="0" border="0" class="display">

                    <thead>
                        <tr>
                            <th align="left">Id</th>
                            <th align="left">Column Name</th>
                            <th align="left">Row Number</th>
                            
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
   <iframe id="createiframe" width="710" height="286"style="border:0px;"></iframe>   
</div>


 <div id="EditWindow" style="display:none">
         <iframe id="editiframe" width="710" height="300"style="border:0px;"></iframe>  
 </div>


</asp:Content>
