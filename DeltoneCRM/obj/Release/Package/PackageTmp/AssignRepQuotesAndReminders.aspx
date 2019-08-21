<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignRepQuotesAndReminders.aspx.cs" MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.AssignRepQuotesAndReminders" %>

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
        var Quotetable;
        $(document).ready(function () {
            callMe();
           repNameChange();
        });

        function callMe() {

            //  var se = $("#RepNameDropDownList").val();
            var re = $("#RepNameDropDownList").val();

            //  CreateTableData(re);

            CreateTableData(re);
        }


        function CreateTableData(catValue) {
           // $('#callbacktableDiv ').hide();
            $('#notcallbacktable').show();
            if (Quotetable)
                Quotetable.destroy();

            Quotetable = $('#livedbtbl').DataTable({
                "ajax": "../Fetch/FetchAllQuotesForRepAssign.aspx?n=1",
                "aaSorting": [[0, "desc"]],
                "iDisplayLength": 25,
                "fnServerParams": function (aoData) {
                    aoData.push(
                        { "name": "reId", "value": catValue }
                    );
                }
            });
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


            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

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
                           <asp:ListItem Text="All" Value="0"></asp:ListItem>
                          <%--  <asp:ListItem Text="Aidan" Value="17"></asp:ListItem>
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


                        <span style="float: left; width: 200px;">NUMBER OF QUOTES:</span>


                        <asp:DropDownList ID="NUmberQuotesDropDownList"  ClientIDMode="Static" runat="server">
                             <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                            <asp:ListItem Text="50" Value="50"></asp:ListItem>
                            <asp:ListItem Text="100" Value="100"></asp:ListItem>
                            <asp:ListItem Text="200" Value="200"></asp:ListItem>
                            
                        </asp:DropDownList>



                        <%-- <input type="button" value="Show" onclick="callMe();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                        <%--<input type="button" value="Print" onclick="printDiv();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                    </td>
                </tr>
                <tr>
                    <td height="25px">


                        <span style="float: left; width: 200px;">ALLOCATE REP:</span>


                        <asp:DropDownList ID="NumberAccountDropDownList" ClientIDMode="Static" runat="server">
                        <%--   <asp:ListItem Text="Aidan" Value="17"></asp:ListItem>
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
                   
                    <%-- <td height="25px" class="section_headings">Companies List</td>--%>
                    <td height="25px">

                          <asp:Button ID="undoButton" runat="server" Visible="false" Text="UNDO" OnClick="btn_undoClickEvent" OnClientClick="javascript:unClientEvent()"
                            ForeColor="Blue" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false"  />

                        <asp:Button ID="allocateRepButton" runat="server" Text="ALLOCATE" OnClientClick="return callUpdateQuotes()"
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
       
       <div id="notcallbacktable"  class="container">
                        
<table cellpadding="0" width="1000" cellspacing="0" border="0" class="display" id="livedbtbl" style="cursor:pointer">
	<thead>
		<tr>
            <th>QUOTE ID</th>
                                                    <th>CREATED DATE</th>
            <th>COMPANY NAME</th>
                                                    <th>CONTACT NAME</th>
           
			                                        <th>QUOTE TOTAL</th>
            <th>QUOTED BY</th>
            <th>CUSTOMER TYPE</th>
                                                    <th>STATUS</th>
            <th>ALLOCATED</th>
             <th>ALLOCATED REP</th>
            <th>SELECT</th>
                                                   
            
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
            selecteditems = [];
            if (alloMode == "2") {

                $("#livedbtbl").find("input[name='selectchk']:checked").each(function (i, ob) {

                    var comId = $(ob).val();
                    var coN = new QuoteSelect();
                    coN.QuoteId = comId;
                    coN.selected = true;
                    selecteditems.push(coN);

                });

                if (selecteditems.length == 0) {
                    alert("Please tick checkbox");
                    return false;
                }

                ShowProgressBar();
                PageMethods.UpdateQuote(selecteditems, alloMode, OriRepName, alloRepName, allocationPeriod, numberQuotes, updateCompanyResult);
            }
            else
            {
                ShowProgressBar();
                PageMethods.UpdateQuote(selecteditems, alloMode, OriRepName, alloRepName, allocationPeriod, numberQuotes, updateCompanyResult);
            }


            

            return false;
        }


        function updateCompanyResult(res) {
            HideProgressBar();
            alert("Updated Successfully.");
            //location.reload();
            // table.ajax.reload();
        }

        function QuoteSelect() {

            this.QuoteId = 0;
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
