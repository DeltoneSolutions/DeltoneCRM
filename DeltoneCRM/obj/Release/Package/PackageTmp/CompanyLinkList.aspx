<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyLinkList.aspx.cs" MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.CompanyLinkList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />
     <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js" ></script>
  
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>
  <%--  <script src="js/jquery-1.11.1.min.js"></script>--%>
    <link href="css/NewCSS.css" rel="stylesheet" />
   <%--  <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>--%>

   
    <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>
    
    <link rel="stylesheet" type="text/css" href="css/jquery.dataTables_new.css" />

    <%-- <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jscolor.js"></script>
 
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
            float:right;
        }

        .inputTextStyle {
            float: right;
            text-align: right;
            color: #4fc2bd !important;
        }
    </style>

    <script type="text/javascript">
        var table;
        var alldatatable ;

        $(document).ready(function () {
            selectCheckBox();
            callMe();
            searchCustomerLinkedMe();
           
        });

        function callMe() {

            var re = "-1";

            CreateTableData(re);
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
                "ajax": "../Fetch/FetchLinkCompanyAdminLoad.aspx?n=1",
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
                    { "name": "rep", "value": re }
                    );
                },
                dom: 'lBfrtip',
                buttons: [
                    'print'
                ]

            });



           
        }

        function searchCustomerLinkedMe() {


            $('#linkedCom').on('keyup', function () {
                table
                    .columns(4)
                    .search(this.value)
                    .draw();
            });
        }

        

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManagerupdatecomlinkList" runat="server" EnablePageMethods="true">
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
                    <td>
                        
                    </td>
                </tr>

                <tr>
                     <asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false" />
                    <asp:Button OnClick="btnupload_Click" ID="Button1" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />
                   

                </tr>
                 <tr>
                    <td>
                         <span>SEARCH BY COMPANY NAME : </span>  <input type="text"  style="width:500px;height:24px;" name="findcompanyLinkcustomer" id="findcompanyLinkcustomer" />
                    </td>
                </tr>
              
                <tr>
                   
                    <%-- <td height="25px" class="section_headings">Companies List</td>--%>
                    <td height="25px">

                          <asp:Button ID="undoButton" runat="server" Visible="false" Text="UNDO"  OnClientClick="javascript:unClientEvent()"
                            ForeColor="Blue" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false"  />

                        <input type="button"  id="allocateRepButton" value="CREATE LINK"  onclick="return callUpdateCompany()"
                            class="buttonClass moveRight"  />
                      
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
                                        <table cellpadding="0" width="1000" cellspacing="0" border="0" class="display" id="noordercompany" style="cursor: pointer">
                                            <thead>
                                                <tr>
                                                    <th align="left">
                                                    <input name="select_all" id="select-all" value="1" type="checkbox" /></th>
                                                    <th align="left">Company Id</th>
                                                    <th align="left">Company Name</th>
                                                    <th align="left">Active</th>
                                                    <th align="left">Linked</th>
                                                    <th align="left">Select Link</th>

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
    
    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
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
                var statu = getlinkCompanyStatus(comId);
                var coN = new LinkComSelect();
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

        function LinkComSelect() {
           
            this.companyId = 0;
            this.selected = false;

        }

        function autoCompleteCompanySearchText() {

            $('#findcompanyLinkcustomer').autocomplete({

                source: "Fetch/FetchAllCompanies.aspx",
                select: function (event, ui) {
                    //console.log("test" + ui.item.value);
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);

                    var Link = "<input name='selectchk'  value='" + ui.item.id + "' type='checkbox' />";

                    var LinkLocked = "<input id='" + ui.item.id + "' name='selectlock'  value='" + ui.item.id + "' type='checkbox' />";

                    table.row.add([
               Link,
                 ui.item.id,
              ui.item.value,
              'Y',
                'N',
             LinkLocked
                    ]).draw();

                    $('#findcompanyLinkcustomer').text('');
                }
                
            });

            
        }


        function getlinkCompanyStatus(comID) {
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
            location.reload();
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


        $(document).ready(function () {
            
            autoCompleteCompanySearchText();
        });

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
