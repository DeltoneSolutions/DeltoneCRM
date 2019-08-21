<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DynamicPriceBookUpload.aspx.cs" MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.DynamicPriceBookUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>
    <script src="js/jquery-1.11.1.min.js"></script>
    <link href="css/NewCSS.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.js"></script>

    <%-- <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>--%>
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
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js"></script>
    <script src="Scripts/date-uk.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />



    <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />

    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-sliderAccess.js"></script>
    <%--<script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>--%>
    <title>Attendance List</title>

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


        table.dataTable tbody tr.myeven {
            background-color: #f2dede !important;
        }

        table.dataTable tbody tr.myodd {
            background-color: #bce8f1 !important;
        }
    </style>

    <script type="text/javascript">


        var table;
        var repNameselected = "";
        var startTimeselected = "";
        var startTimeChangted = "";



        $(document).ready(function () {
            createTableData();
            
        });

        function createTableData() {
           
            if (table)
                table.destroy();


            table = $('#prrrrtablelist').DataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response...</img>'  //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
            
                columns: [
                    { 'data': 'SupplierName' },
                    { 'data': 'SupplierItemCode' },
                    { 'data': 'Description' },
                    { 'data': 'PriceUpdate' },
                    { 'data': 'DSB' },
                    { 'data': 'ResellPrice' },
                    { 'data': 'PrinterCompatibility' },

                ],

                sAjaxSource: '../DataHandlers/PriceBookHandle.ashx?r=1',
                "columnDefs": [
                        {
                            className: 'align_center', "targets": [0, 1, 2, 3,4],
                        },



                        { "width": "10%", "targets": 0 },
                        
                ],
                "aaSorting": [[1, "asc"]],
                "iDisplayLength": 25
               
                


            });


        }

        function confirmBox() {

            var result = confirm("Do you want to continue?");
            if (result) {
                return true;
            }
            else
                return false;
        }


    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div id="gridDiv">
    

     <h4>PLEASE CHECK THE FILE FORMAT BEFORE UPLOAD</h4>

     <asp:Image ID="dyImage" runat="server" ImageUrl="~/Images/dynamicPricebook.PNG" />
    
         <br /><br />
          <span>SELECT SUPPLIER:</span>
         <asp:DropDownList ID="suppName" runat="server">
              <asp:ListItem Text="Select" Value="0">

              </asp:ListItem>

                                                    </asp:DropDownList>    File Format: XlSX
         <br /><br />
         <h3> <span style="color:red"> <asp:Literal  ID="messagelabel"  runat="server"></asp:Literal> </span> </h3>
         <br /><br />
           <h3>Select  Supplier file to upload:</h3>
          <br /><br />
       <asp:FileUpload id="FileUpload1"                 
           runat="server">
       </asp:FileUpload>

      
       <asp:Button id="UploadButton" 
           Text="Upload file"
           OnClick="UploadButton_Click"
           runat="server">
       </asp:Button>    
<br /><br />

           <div id="maindatatableMain" runat="server" style="display:none;">


            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

                <tr>
                    <td height="25px" class="section_headings">PRICE BOOK ITEM DETAILS PREVIEW</td>
                </tr>


                <tr>
                    <td class="white-box-outline">
                        <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                            <tr>
                                <td height="20px">&nbsp;</td>
                            </tr>
                            <%-- <tr>
                                <td height="20px" style="margin-bottom:12px;"> <input class="inputColorGreen" type="text" id="linkedCom" /> <span class="inputTextStyle" style="margin-right: 24px; ">Search By Linked Y/N  : </span>
                                </td>
                            </tr>--%>
                            <tr>
                                <td height="20px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>

                                    <div class="container">
                                        <table cellpadding="0" width="1000" cellspacing="0" border="0" class="display" id="prrrrtablelist" style="cursor: pointer">
                                            <thead>
                                                <tr>
                                                    <th align="left">SUPPLIER NAME</th>
                                                    <th align="left">ITEM CODE</th>
                                                    <th align="left">DESCRIPTION </th>
                                                    <th align="left">PRICE </th>
                                                    <th align="left">DSB </th>
                                                    <th align="left">SELL </th>
                                                    <th align="center">MODEL</th>
                                                   

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


               <br /><br />
                <asp:Button id="Button1" 
           Text="SAVE"
           OnClick="SaveButton_Click" OnClientClick="return confirmBox();"
           runat="server">
       </asp:Button>  

        </div>
         </div>
</asp:Content>
