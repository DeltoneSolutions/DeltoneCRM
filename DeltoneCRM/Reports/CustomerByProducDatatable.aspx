<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerByProducDatatable.aspx.cs" MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.Reports.CustomerByProducDatatable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.11.1.min.js"></script>
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

         .moveleft {
            float: left;
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
            selectCheckBox();
            callMe();


            var dateNow = new Date();
           // $("#StartDateSTxt").datepicker({ dateFormat: 'dd-mm-yy' });
           // $("#EndDateSTxt").datepicker({ dateFormat: 'dd-mm-yy' });
           
        });

        function callMe() {

            var re = "-1";
            var sDate="";
            var eDate="";
            CreateTableData(sDate, eDate);
        }


        function selectCheckBox() {

            $('#select-all').on('click', function () {
                // Check/uncheck all checkboxes in the table
                var rows = table.rows({ 'search': 'applied' }).nodes();
                console.log(rows);
                $('input[type="checkbox"][name="selectchk"]', rows).prop('checked', this.checked);
            });
        }

        function CreateTableData(sd,ed) {
         
            if (table)
                table.destroy();


            table = $('#noordercompany').DataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response..."</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                "ajax": "../Fetch/FetchCustomerProductDatatable.aspx?n=1",
                "columnDefs": [
                        {
                            className: 'align_left', "targets": [0, 1, 2],
                        },
                        {
                            className: 'dt-center', "targets": [3, 4, 5],
                        }
                       
                ],
                "aaSorting": [[1, "asc"]],
                "iDisplayLength": 25,
                "fnServerParams": function (aoData) {
                    aoData.push(
                    { "name": "startdate", "value": sd },
                    { "name": "enddate", "value": ed }
                    );
                },
                dom: 'lBfrtip',
                buttons: [
                    'print'
                ]

            });
        }

      


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManagerupdatecustomerEmail" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="gridDiv">
        <br />

          Start Date:
                    <input id="StartDateSTxt" type="date" name="StartDateTxt" size="18"  />
               
         End Date:
                    <input id="EndDateSTxt" type="date" name="EndDateTxt" size="18" />

          <%--<asp:Button OnClick="SearchButton_Click" ID="btnsearch" Text="Search" ForeColor="Blue" Width="20%" runat="server" CausesValidation="false" />
     <asp:Button OnClientClick="reloadPage();" ID="btnreset" Text="Reset" ForeColor="Blue" Width="10%" runat="server" CausesValidation="false" />--%>
              
        <asp:Label ID="messagelable1" runat="server" CssClass="messagecss" Font-Size="Medium"></asp:Label>
        

        <%--  <asp:TextBox ID="searchTextBox" runat="server" Width="60%"></asp:TextBox>
        <asp:Button OnClick="SearchButton_Click" ID="btnsearch" Text="Search" ForeColor="Blue" Width="20%" runat="server" CausesValidation="false" />--%>
        <div id="main">
            <br />
            <br />


            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

                <tr>
                     <asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false" />
                    <asp:Button OnClick="btnupload_Click" ID="Button1" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />
                   

                </tr>
              
                <tr>
                   
                    <%-- <td height="25px" class="section_headings">Companies List</td>--%>
                    <td height="25px">

                          <asp:Button ID="undoButton" runat="server" Visible="false" Text="UNDO"  OnClientClick="javascript:unClientEvent()"
                            ForeColor="Blue" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false"  />

                        <input type="button"  id="allocateRepButton" value="Send Email"  onclick="return callUpdateCompany()"
                            class="buttonClass moveleft"  />
                      
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
                    <td height="25px" class="section_headings">COMPANY LIST</td>
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
                                                    <th align="left">
                                                    <input name="select_all" id="select-all" value="1" type="checkbox" /></th>
                                                    <th align="left">Company Id</th>
                                                    <th align="left">Company Name</th>
                                                    <th align="left">Contact</th>
                                                      <th align="left">Telephone</th>
                                                    <th align="left">Ordered Date</th>
                                                    <th align="left">Product Code</th>
                                                      <th align="left">Description</th>
                                                    <th align="left">Quantity</th>
                                                     <th align="left">Uinit amount</th>
                                                     <th align="left">Created By</th>
                                                     <th align="left">Account Owner</th>
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
                        canFind= true;
                        
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