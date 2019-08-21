<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoOrderCompanies.aspx.cs" MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.NoOrderCompanies" %>



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
    <script src="Scripts/date-uk.js"></script>

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
    </style>

    <script type="text/javascript">
        var table;


        $(document).ready(function () {
            //  callMe();
            //  repNameChange();


            $.fn.dataTableExt.oSort['time-date-sort-pre'] = function (value) {

                return Date.parse(value);
            };
            $.fn.dataTableExt.oSort['time-date-sort-asc'] = function (a, b) {
                console.log('sss');
                return a - b;
            };
            $.fn.dataTableExt.oSort['time-date-sort-desc'] = function (a, b) {
                console.log('tttt');
                return b - a;
            };
            var sort = "desc"

            $('#noordercompany').on('click', 'thead th', function () {
                if (sort == "desc")
                    sort = "asc";
                else if (sort == "asc")
                    sort = "desc";


                var i = table.column(this).index();
                console.log(i);
                if (i == 6) {
                    

                    table
       .order([1, sort])
                    .draw();
                  

                }


            });

          


        });

        function callMe(paramSort) {

            var se = $("#monthdropDown").val();
            var re = $("#RepDropDownList").val();
            if (se != "-1")
                CreateTableData(se, re, paramSort);
        }


        function CreateTableData(catValue, re, paramSort) {

            if (table)
                table.destroy();

            table = $('#noordercompany').DataTable({
                processing: true,
                "language": {
                    "processing": "Hang on. Waiting for response..." //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                "ajax": "../Fetch/FetchNoOrderCompanyForAdmin.aspx?n=1",
                "columnDefs": [
                        {
                            className: 'align_left', "targets": [0, 1, 2, 3, 4, 5, 6],
                        },

                        { "width": "10%", "targets": 0 },

                         { "type": 'time-date-sort', "targets": 6 }
                ],
                "aaSorting": [[1, paramSort]],
                "iDisplayLength": 25,
                "fnServerParams": function (aoData) {
                    aoData.push(
                        { "name": "mon", "value": catValue },
                    { "name": "rep", "value": re }
                    );
                },
                dom: 'lBfrtip',

                buttons: [{ extend: 'print', },
                    { extend: 'excelHtml5', text: 'Export Excel', },

                ],


            });
        }

        function OpenCompany(CompanyID) {

            window.open('ConpanyInfo.aspx?companyid=' + CompanyID, '_blank');
        }

        function printDiv() {
            var divToPrint = document.getElementById("noordercompany");
            newWin = window.open("");
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="gridDiv">
        <br />


        <br />
        <asp:Label ID="messagelable" runat="server" CssClass="messagecss"></asp:Label>
        <br />
        <br />



        <br />
        <%--  Select Rep:
      <asp:DropDownList ID="RepDropDownList" runat="server">
           <asp:ListItem Text="Select" Value="-1"></asp:ListItem>
           <asp:ListItem Text="Trent" Value="15"></asp:ListItem>
          <asp:ListItem Text="Bailey" Value="14"></asp:ListItem>
          <asp:ListItem Text="John" Value="10"></asp:ListItem>
          <asp:ListItem Text="Aidan" Value="17"></asp:ListItem>
           <asp:ListItem Text="William" Value="18"></asp:ListItem>
           <asp:ListItem Text="Ross" Value="19"></asp:ListItem>
      </asp:DropDownList>--%>

        <div id="main">
            <br />
            <br />


            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
                <tr>
                    <td height="25px"></td>
                </tr>
                <tr>
                    <td height="25px">
                        <span style="float: left;">Select Account Owner:</span>
                        <select id="RepDropDownList" style="float: left;">
                            <option value="3">Krit</option>
                            <option value="1">Dimitri</option>
                            <option value="2">Taras</option>
                            <option value="15">Trent</option>
                            <option value="14">Bailey</option>
                            <option value="10">John</option>
                            <option value="17">Aidan</option>
                            <option value="18">William</option>
                            <option value="19">Ross</option>

                        </select>
                        <asp:Button OnClick="btnupload_Click" ID="Button1" Text="Dashboard" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />

                    </td>
                </tr>
                <tr>
                    <td height="25px">


                        <span style="float: left;">Select Month:</span>
                        <select id="monthdropDown" style="float: left;">
                            <option value="-1">Select</option>
                            <option value="1">Last Month</option>
                            <option value="2">Last 2 Months</option>
                            <option value="3">Last 3 Months</option>
                            <option value="6">Last 6 Months</option>
                            <option value="12">Last One Year</option>

                        </select>

                        <input type="button" value="Show" onclick="callMe('desc');" class="buttonClass moveRight" style="color: blue; float: right;" />
                        <%--<input type="button" value="Print" onclick="printDiv();" class="buttonClass moveRight" style="color:blue;float:right;"/>--%>
                    </td>
                </tr>
                <tr>
                    <td height="25px" class="section_headings">Companies List</td>
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
                                                    <th align="left">Order Id</th>

                                                    <th align="left" style="width: 160px; word-wrap: break-word;">Company Name</th>
                                                    <th align="left" style="width: 80px; word-wrap: break-word;">Contact Name</th>
                                                    <th align="left">Telephone</th>
                                                    <th align="left">Mobile</th>
                                                    <th align="left">Last Order Date</th>
                                                    <th align="left" style="width: 240px; word-wrap: break-word;">Event Date And Message</th>
                                                    <th align="left">View</th>
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
</asp:Content>
