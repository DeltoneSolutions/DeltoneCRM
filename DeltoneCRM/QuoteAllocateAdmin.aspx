<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteAllocateAdmin.aspx.cs" MasterPageFile="~/NoNav.Master"  Inherits="DeltoneCRM.QuoteAllocateAdmin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>
    <script src="js/jquery-1.11.1.min.js"></script>
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
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js">
    </script>
    <title>Company List</title>

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
    </style>

    <script type="text/javascript">
        var table;

        $(document).ready(function () {
            callMe();
            repNameChange();
        });

        function callMe() {

        //    var se = $("#monthdropDown").val();
            var re = $("#RepNameDropDownList").val();

            CreateTableData(re);
        }


        function CreateTableData(re) {
            var catValue = "a";
            if (table)
                table.destroy();
            if (re == "-1") {
                table = $('#livedbtbl').DataTable({
                    processing: true,
                    "language": {
                        "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response..."</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                    },
                    "ajax": "../Fetch/FetchAllocateQuote.aspx?n=1",
                    "columnDefs": [
                            {
                                className: 'align_left', "targets": [0, 1, 2, 3, 4],
                            },
                            { type: 'date', targets: [4] },
                            { "width": "10%", "targets": 0 },
                            { "visible": false, "targets": 6 }
                    ],
                    "aaSorting": [[1, "asc"]],
                    "iDisplayLength": 25,
                    "fnServerParams": function (aoData) {
                        aoData.push(
                        { "name": "rep", "value": re }
                        );
                    },
                    dom: 'lBfrtip',
                    buttons: [
                        'print'
                    ]

                });
            }
            else {

                table = $('#livedbtbl').DataTable({
                    processing: true,
                    "language": {
                        "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response..."</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                    },
                    "ajax": "../Fetch/FetchAllocateQuote.aspx?n=1",
                    "columnDefs": [
                            {
                                className: 'align_left', "targets": [0, 1, 2, 3, 4],
                            },
                            { type: 'date', targets: [4] },
                            { "width": "10%", "targets": 0 }
                    ],
                    "aaSorting": [[1, "asc"]],
                    "iDisplayLength": 25,
                    "fnServerParams": function (aoData) {
                        aoData.push(
                        { "name": "rep", "value": re }
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

        callMe();
    });
        }



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="gridDiv">
      
        <asp:Label ID="messagelable1" runat="server" CssClass="messagecss" Font-Size="Medium"></asp:Label>
        

        <%--  <asp:TextBox ID="searchTextBox" runat="server" Width="60%"></asp:TextBox>
        <asp:Button OnClick="SearchButton_Click" ID="btnsearch" Text="Search" ForeColor="Blue" Width="20%" runat="server" CausesValidation="false" />--%>
        <div id="main">
          

            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

                <tr>
                     <asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false" />
                    <asp:Button OnClick="btnupload_Click" ID="Button1" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />
                   

                </tr>
                <tr>
                    <td height="25px"></td>
                </tr>
                <tr>
                    <td height="25px">
                        <span style="float: left; width: 200px;">REP NAME :</span>
                        <asp:DropDownList ID="RepNameDropDownList" ClientIDMode="Static" runat="server">
                            <asp:ListItem Text="Last 6 Month Quotes" Value="-1"></asp:ListItem>
                             <asp:ListItem Text="All Assigned Quotes" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Aidan" Value="17"></asp:ListItem>
                            <asp:ListItem Text="Bailey" Value="14"></asp:ListItem>
                            <asp:ListItem Text="John" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Krit" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Taras" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Trent" Value="15"></asp:ListItem>
                            <asp:ListItem Text="William" Value="18"></asp:ListItem>
                            <asp:ListItem Text="TestOne" Value="24"></asp:ListItem>

                        </asp:DropDownList>


                    </td>
                </tr>
               <tr>
                    <td height="25px">


                        <span style="float: left; width: 200px;">ALLOCATION PERIOD:</span>

                        <asp:DropDownList ID="AllocationDropDownList" runat="server">
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


                        <span style="float: left; width: 200px;">NUMBER OF QUOTES:</span>


                        <asp:DropDownList ID="NumberAccountDropDownList" runat="server">
                             <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="150" Value="150"></asp:ListItem>
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            <asp:ListItem Text="250" Value="250"></asp:ListItem>
                            
                        </asp:DropDownList>



                        <%-- <input type="button" value="Show" onclick="callMe();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                        <%--<input type="button" value="Print" onclick="printDiv();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                    </td>
                </tr>
                <tr>
                   
                    <%-- <td height="25px" class="section_headings">Companies List</td>--%>
                    <td height="25px">

                          <asp:Button ID="undoButton" runat="server" Visible="false" Text="UNDO" OnClick="btn_undoClickEvent" OnClientClick="javascript:unClientEvent()"
                            ForeColor="Blue" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false"  />

                        <asp:Button ID="allocateRepButton" runat="server" Text="ASSIGN" OnClick="btn_allocate_Click" OnClientClick="javascript:ShowProgressBar()"
                            ForeColor="Blue" CssClass="buttonClass moveRight" CausesValidation="false" />
                      
                         <div ID="dvProgressBar" style="float:inherit;visibility: hidden;" >
        <img src="/images/loadingimage1.gif" /> <strong style="color:red;">  Updating, Please Wait...</strong>
  </div>
                    </td>

                </tr>


               


            </table>

        </div>

       
        <asp:Label ID="messagelable" runat="server" CssClass="messagecss"></asp:Label>
        <br />
       
        <div id="maindatatableMain">
           
           
                        <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
                <tr>
                    <td height="25px"></td>
                </tr>


                <tr>
                    <td height="25px" class="section_headings">LAST 6 MONTH QUOTES</td>
                </tr>


                <tr>
                    <td class="white-box-outline">
                      <table cellpadding="0" width="900" cellspacing="0" border="0" class="display" id="livedbtbl" style="cursor:pointer">
	<thead>
		<tr>
            <th>QUOTE ID</th>
                                                    <th>CREATED DATE</th>
            <th>COMPANY NAME</th>
                                                    <th>CONTACT NAME</th>
           
			                                        <th>QUOTE TOTAL</th>
            <th>QUOTED BY</th>
             <th>QUOTE ASSIGNED</th>
            <th>CUSTOMER TYPE</th>
                                                    <th>STATUS</th>
                                                    
            
		</tr>
        
	</thead>
                 
	<tbody>
		 
	</tbody>

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