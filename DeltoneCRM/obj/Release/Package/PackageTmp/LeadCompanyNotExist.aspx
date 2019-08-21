<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeadCompanyNotExist.aspx.cs" MasterPageFile="~/SiteInternalNavLevel1.Master" Inherits="DeltoneCRM.LeadCompanyNotExist" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
   <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>
   
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
	<script type="text/javascript"  src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
	</script>
	<script type="text/javascript"  src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js">
        </script>

    <title>Company List</title>

    <style type='text/css'>
        /* css for timepicker */
       

          body {
           background-color: #66a3ff !important;
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
            max-width: 100%;
            margin: 0 auto;
            width: 100%;
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
                 .buttonClass
{
    padding: 2px 20px;
    text-decoration: none;
    border: solid 1px #3E3E42;
    height:40px;
    cursor:pointer;
}
.buttonClass:hover
{
    border: solid 1px Black;
    background-color: #ffffff;
}
.moveRight{
    float:right;
    margin-bottom:30px;
    color:blue;
    text-align:center;
       font-weight:bold;
}

.buttons-print{
     float:left;
      background-color: #ffffff;
      height:40px;
      padding: 2px 20px;
    text-decoration: none;
    border: solid 1px #3E3E42;
    height:20px;
    cursor:pointer;
     margin-bottom:10px;
    color:blue;
    text-align:center;
       font-weight:bold;
        margin-left:10px;

}

  #tooltip {
  position: absolute;
  z-index: 1001;
  display: none;
  border: 2px solid #ebebeb;
  border-radius: 5px;
  padding: 10px;
  background-color: #fff;
  height:80px;
  max-height:300px;
  color:blue;
  font-size:14px;
  word-wrap: break-word;
  -webkit-border-radius: 5px; 
    -moz-border-radius: 5px; 
    border-radius: 5px;
}

     td.details-control {
            background: url('Images/details_open.png') no-repeat center center;
            cursor: pointer;
        }

        tr.shown td.details-control {
            background: url('Images/details_close.png') no-repeat center center;
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
        }

        .inputTextStyle {
            text-align: center;
            color: white !important;
        }
         table.display tbody tr:nth-child(even):hover td {
            background-color: #99c2ff !important;
        }

        table.display tbody tr:nth-child(odd):hover td {
            background-color: #99c2ff !important;
        }
    </style>

    <script type="text/javascript">
        $.noConflict();
        var table;
        var secondColumnVisible = false;
        var canCall = '<%= userlevel %>'; 
        var sort = "desc";
        var colNu = 7;
        var pageClicked = 0;
        $(document).ready(function () {

            $('#findcustomerorder').autocomplete({
                source: "/Fetch/FetchSearchOrder.aspx",
                select: function (event, ui) {
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    // $('#CompanyContactTR').show();
                    // $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    var valsSPLIT = ui.item.id.split(',');;

                    var url = 'order.aspx?Oderid=' + (valsSPLIT[0]) + '&cid=' + (valsSPLIT[2])
                            + '&Compid=' + (valsSPLIT[1]);
                    window.open(url, "blank");
                    $(this).val(''); return false;


                }
            });
          

            if (canCall == "1")
                secondColumnVisible = true;


            callMe();

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
            var sort = "desc";



            $('#noordercompany').on('mousemove', 'td.notestest', function (e) {
                // console.log($(this));

                var tr = $(this).closest('tr');
                var row = table.row(tr);
                // var td (this).find('td:first').show()
                var rowData = row.data()[9];
                if (rowData) {
                    $("#tooltip").text(rowData).animate({ left: e.pageX, top: e.pageY }, 1);
                    if (!$("#tooltip").is(':visible')) {
                        $("#tooltip").show();
                    }
                }
            });
            $('#noordercompany').on('mouseleave', 'td.notestest', function (e) {
                $("#tooltip").hide();
            });

       


            $('#noordercompany tbody').on('click', 'td.details-control', function () {
                var tr = $(this).closest('tr');
                var row = table.row(tr);

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    // Open this row

                    row.child(format(row.data()[11])).show();
                    tr.addClass('shown');
                }
            });


            $('#sortinputdrop').change(
  function () {

      callSortByType();
  });

            $('#sortinputdropdir').change(
    function () {

        callSortByType();
    });

            
        });

        function callMe() {

          
            CreateTableData();
            


        }
        function format(d) {
            // `d` is the original data object for the row   

            if (d) {

                var orderSplit = d.split(';');

                var finalTr = "";
                for (var i = 0; i < orderSplit.length ; i++) {
                    var orderDetyails = orderSplit[i].split(',');

                    var temTr = '<tr class="bgnnercolor">' +
                          '<td>Ordered Date:</td>' +
                          '<td>' + orderDetyails[0] + '</td>' +

                          '<td>Order Number:</td>' +
                          '<td>' + orderDetyails[2] + '</td>' +

                          '<td>Total:</td>' +
                          '<td>' + parseFloat(orderDetyails[1]).toFixed(2) + '</td>' +
                      '</tr>';

                    finalTr = finalTr + temTr;
                }
            }

            if (finalTr) {

            }
            else {

                finalTr = '<tr class="bgnnercolor">' +
                                          '<td>No Order Found</td>' +
                                      '</tr>';
            }

            var innerTalbeTr = '<table class="innertableta" cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' + finalTr + '</table>';

            return innerTalbeTr;
        }

        function CreateTableData(){

            if (table)
                table.destroy();

            table = $('#noordercompany').DataTable({
                processing: true,
                "language": {
                    "processing": "Hang on. Waiting for response..." //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                "ajax": "../Fetch/FetchLeadRepNotExistCompany.aspx",
                columns: [
                     {
                         "className": 'details-control',
                         "orderable": false,
                         "data": null,
                         "defaultContent": ''
                     },

                ],
                "columnDefs": [
                       {

                           className: 'notestest', "targets": [8],
                       },
                       

                        { "width": "3%", "targets": 0 },
                        { "width": "1%", "targets": 1 },
                        { "width": "10%", "targets": 8 },
                        { "type": 'time-date-sort', "targets": 5 },
                        
                          { "visible": false, "targets": 9 },
                          { "visible": false, "targets": 5 },
                           { "visible": secondColumnVisible, "targets": 2 },
                            { "visible": false, "targets": 11 },
                ],
                "aaSorting": [[colNu, sort]],
                "iDisplayLength": 25,
               

            });
        }

        function OpenCompany(CompanyID) {

            window.open('NotExistsConpanyInfo.aspx?companyid=' + CompanyID, '_blank');
        }

        function printDiv() {
            var divToPrint = document.getElementById("noordercompany");
            newWin = window.open("");
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }

        function callSortByType() {
            var sortY = $("#sortinputdropdir").val();
            var orderselect = $("#sortinputdrop").val();
            var sort = sortY;

            if (orderselect == "dateorder") {
                colNu = 5;
                table
          .order([5, sortY])
                       .draw();
            }
            else {
                if (orderselect == "comname") {
                    colNu = 1;
                    table
             .order([1, sortY])
                          .draw();
                }
                else {
                    colNu = 7;
                    table
             .order([7, sortY])
                          .draw();
                }
            }
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

        <div id="main">
            <br />
            <br />

             <div id="tooltip"></div>
            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
                <tr>
                    <td height="25px"></td>
                </tr>
              <tr>
                    <td width="60px;">
                        <span class="inputTextStyle" style="width: 100px; height: 30px; margin-right: 24px; padding-bottom: 20px;float: left;">Sort By Column:</span>

                   
                        <select id="sortinputdrop" class="inputColorGreen" style="width: 150px; height: 30px;float: left;">
                             <option value="dayrem">Remaining Days         </option>
                            <option value="dateorder">Ordered Date  </option>
                            <option value="comname">Company Name         </option>
                            
                        </select>
                  
                        <select id="sortinputdropdir" class="inputColorGreen" style="width: 150px; height: 30px; margin-left: 12px;float: left;">
                            <option value="desc">Desc </option>
                            <option value="asc">Asc       </option>

                        </select></td>

                    
                </tr>
              
        <tr>
            <td height="25px" class="section_headings">NON EXISTING LEADS</td>
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
                                        <table cellpadding="0" width="1200" cellspacing="0" border="0" class="display" id="noordercompany" style="cursor: pointer">
                                            <thead>
                                                <tr>
                                                     <th  ></th>
                                                    <th  >Company Name</th>
                                                     <th  >Company From</th>
                                                    <th  >Contact Name</th>
                                                    <th >Telephone</th>
                                                     <th >Last Order Date</th> 
                                                     <th >Allocated Date</th>
                                                     <th >Remaining Days</th>
                                                      
                                                      <th >Notes</th>
                                                      <th></th>
                                                    <th >View</th>
                                                     <th ></th>
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