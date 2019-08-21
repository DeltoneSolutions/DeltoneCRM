<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallBackLeadCompanyList.aspx.cs" MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.CallBackLeadCompanyList" %>


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
           // repNameChange();
        });

        function callMe() {

            var se = $("#monthdropDown").val();
            var re = "0";

            CreateTableData(re);
        }


        function CreateTableData(re) {
            var catValue = "a";
            if (table)
                table.destroy();

            table = $('#noordercompany').DataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response..."</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                "ajax": "../Fetch/FetchLeadCompanyByRepCallBack.aspx?n=1",
                "columnDefs": [
                        {
                            className: 'align_left', "targets": [0, 1, 2,3,4,5],
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

        function repNameChange() {

            $('#RepNameDropDownList').change(
    function () {

        callMe();
    });
        }



    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagercallbackaresssign" runat="server" EnablePageMethods="true">
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
                        <span style="float: left; width: 200px;">MONTH :</span>
                       <select id="MonthSelect" style="width:300px;height:30px;" runat="server">
                           <option value="0" >  ALL  </option>
 <option value="1" >  JAN  </option>
 <option value="2" >  FEB         </option>
                           <option value="3" >  MAR         </option>
                           <option value="4" >  APR         </option>
                           <option value="5" >  MAY         </option>
                           <option value="6" >  JUN         </option>
                           <option value="7" >  JUL         </option>
                           <option value="8" >  AUG         </option>
                           <option value="9" >  SEP         </option>
                           <option value="10" >  OCT         </option>
                           <option value="11" >  NOV         </option>
                           <option value="12" >  DEC         </option>
                           </select>
                    </td>

                </tr>
                 <tr>
                    <td height="25px"></td>
                </tr>
                 <tr>
                    <td height="25px">
                        <span style="float: left; width: 200px;">YEAR :</span>
                       <select id="YEARSelect" style="width:300px;height:30px;" runat="server">
                           <option value="2017" >  2017  </option>
 <option value="2018" >  2018  </option>
 <option value="2019" >  2019         </option>
                         
                           </select>
                    </td>

                </tr>

                  <tr>
                    <td height="25px"></td>
                </tr>

                <tr>
                    <td height="25px">
                        <span style="float: left; width: 200px;">REP NAME :</span>
                        <asp:DropDownList ID="RepNameDropDownList" ClientIDMode="Static" runat="server">
                          
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


                        <span style="float: left; width: 200px;">ALLOCATION MODE:</span>

                        <asp:DropDownList ID="AllocationModeDrop" ClientIDMode="Static" runat="server">
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


                        <span style="float: left; width: 200px;">NUMBER OF ACCOUNTS:</span>

                        <asp:TextBox ID="NumberAccountDropDownList" runat="server"></asp:TextBox>
                      <%--  <asp:DropDownList ID="NumberAccountDropDownList" runat="server">
                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                             <asp:ListItem Text="25" Value="25"></asp:ListItem>
                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                        </asp:DropDownList>--%>



                        <%-- <input type="button" value="Show" onclick="callMe();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                        <%--<input type="button" value="Print" onclick="printDiv();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                    </td>
                </tr>
                <tr>
                   
                    <%-- <td height="25px" class="section_headings">Companies List</td>--%>
                    <td height="25px">

                          <asp:Button ID="undoButton" runat="server" Visible="false" Text="UNDO" OnClick="btn_undoClickEvent" OnClientClick="javascript:unClientEvent()"
                            ForeColor="Blue" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false"  />

                        <asp:Button ID="allocateRepButton" runat="server" Text="REALLOCATE"  OnClientClick="return callAllocation()"
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
            <br />
           
                        <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
                <tr>
                    <td height="25px"></td>
                </tr>


                <tr>
                    <td height="25px" class="section_headings">REASSIGN LEADS</td>
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
                                                    <th align="left">Company Id</th>
                                                    <th align="left">Company Name</th>
                                                    <th align="left">Contact Name</th>
                                                    <th align="left">Rep</th>
                                                     <th align="left">Expired Date</th>
                                                    <th align="left">Message</th>
                                                     <th>SELECT</th>
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


        function callAllocation() {

            if (ValidateInput()) {

                var alloMode = $("#AllocationModeDrop").val();
                var alloRepName = $("#RepNameDropDownList").val();
                var allocationPeriod = $("#ContentPlaceHolder1_AllocationDropDownList").val();
                var numberaccounts = $("#ContentPlaceHolder1_NumberAccountDropDownList").val();
                var month=$("#ContentPlaceHolder1_MonthSelect").val();
                var year=$("#ContentPlaceHolder1_YEARSelect").val();

                selecteditems = [];
                console.log(alloMode);
                if (alloMode == "2") {

                    $("#noordercompany").find("input[name='selectlock']:checked").each(function (i, ob) {

                        var comId = $(ob).val();
                        var coN = new ComReAssign();
                        coN.ComId = comId;
                        coN.selected = true;
                        selecteditems.push(coN);

                    });

                    if (selecteditems.length == 0) {
                        alert("Please tick checkbox");
                        return false;
                    }

                    ShowProgressBar();
                    numberaccounts = selecteditems.length;
                    PageMethods.AllocateAccounts(selecteditems, alloMode, numberaccounts, allocationPeriod, alloRepName,month ,year,updateCompanyResult);
                }
                else
                {
                    ShowProgressBar();
                  
                    PageMethods.AllocateAccounts(selecteditems, alloMode, numberaccounts, allocationPeriod, alloRepName, month, year, updateCompanyResult);
                }

            }
           
            return false;
        }

        function updateCompanyResult(res) {
            HideProgressBar();
            alert("Updated Successfully.");
            location.reload();
            // table.ajax.reload();
        }

        function ValidateInput() {

            var monthSelct = $("#ContentPlaceHolder1_MonthSelect").val();

            if (monthSelct == "0") {
                alert("Please select month");
                return false;

            }

            var alocationMode = $("#AllocationModeDrop").val();
            if (alocationMode == "0") {
                alert("Please select allocation mode");
                return false;
            }

            
            if (alocationMode == "1") {
                var accountNUmberCount = $("#ContentPlaceHolder1_NumberAccountDropDownList").val();
                if (accountNUmberCount == "") {
                    alert("Please enter number of account");
                    return false;
                }
            }


            return true;
        }

        function ComReAssign() {

            this.ComId = 0;
            this.selected = false;

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
