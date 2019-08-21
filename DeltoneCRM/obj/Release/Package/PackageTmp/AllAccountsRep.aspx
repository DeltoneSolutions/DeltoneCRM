<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllAccountsRep.aspx.cs" MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.AllAccountsRep" %>


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
  <%--  <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>--%>
    <script src="js/datatableup/datatables.min.js"></script>
    
     <%--  <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
	</script>--%>
    <%--	<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>--%>
   <%-- <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js">
    </script>--%>

     <script src="js/datatableup/dataTables.buttons.min.js"></script>
    <script src="js/datatableup/buttons.print.min.js"></script>

    <script src="Scripts/date-uk.js"></script>
    <title>Company List</title>

    <style type='text/css'>
        /* css for timepicker */
         td.details-control {
            background: url('Images/details_open.png') no-repeat center center;
            cursor: pointer;
        }

        tr.shown td.details-control {
            background: url('Images/details_close.png') no-repeat center center;
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
            margin-left:12px;
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
            color: #4fc2bd !important;
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

    </style>

    <script type="text/javascript">
        var table;
        var colNu = 4;
        var sort = "desc"
        function callSortByType() {
            var sortY = $("#sortinputdropdir").val();
            var orderselect = $("#sortinputdrop").val();
            var sort = sortY;

            if (orderselect == "dateorder") {
                colNu = 4;
                table
          .order([4, sortY])
                       .draw();
            }
            else {
                colNu = 2;
                table
         .order([2, sortY])
                      .draw();
            }
        }

        $(document).ready(function () {

            $('#sortinputdrop').change(
   function () {

       callSortByType();
   });

            $('#sortinputdropdir').change(
    function () {

        callSortByType();
    });


            callMe("desc");
            repNameChange();


            $('#noordercompany').on('mousemove', 'td.notestest', function (e) {
                // console.log($(this));

                var tr = $(this).closest('tr');
                var row = table.row(tr);
                // var td (this).find('td:first').show()
                var rowData = row.data()[6];
                if (rowData) {
                    $("#tooltip").text(rowData).animate({ left: e.pageX, top: e.pageY }, 1);
                    if (!$("#tooltip").is(':visible')) {
                        $("#tooltip").show();
                    }
                }
            })
            $('#noordercompany').on('mouseleave', 'td.notestest', function (e) {
                $("#tooltip").hide();
            });

            //jQuery.extend(jQuery.fn.dataTableExt.oSort, {
            //    "extract-date-pre": function(value) {
            //        var date = $(value, 'span')[0].innerHTML;
            //        date = date.split('/');
            //        return Date.parse(date[1] + '/' + date[0] + '/' + date[2])
            //    },
            //    "extract-date-asc": function(a, b) {
            //        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
            //    },
            //    "extract-date-desc": function(a, b) {
            //        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
            //    }

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
           

            $('#noordercompany').on('click', 'thead th', function () {
                if (sort == "desc")
                    sort = "asc";
                else if (sort == "asc")
                    sort = "desc";


                var i = table.column(this).index();
                console.log(i);
                if (i == 3) {


                    table
       .order([1, sort])
                    .draw();


                }


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

                    row.child(format(row.data()[12])).show();
                    tr.addClass('shown');
                }
            });
            });

            function callMe(par) {

                var re = $("#RepNameDropDownList").val();

                CreateTableData(re, par,'','');
            }


            function CreateTableData(re, par,star,end) {
                console.log('tet');
                var catValue = "a";
                if (table)
                    table.destroy();

                table = $('#noordercompany').DataTable({
                    processing: true,
                    "language": {
                        "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response..."</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                    },
                    "ajax": "../Fetch/FetchAllAccountsForAdmin.aspx?n=1",
                    "createdRow": function (row, data, index) {

                        //console.log(row);
                        if (data.CanFlag == "1") {
                            $('td', row).eq(6).addClass('highlight');
                            $('td', row).eq(0).addClass('highlight');
                        }
                        // $(row).css('background-color', 'Orange !important');

                    },
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
                                className: 'align_left', "targets": [0, 1, 2, 3, 4, 5, 6],
                                className: 'notestest', "targets": [5],
                            },
                           
                              { "visible": false, "targets": 12 },
                               { "visible": false, "targets": 6 },
                            { "width": "5%", "targets": 0 },
                    { "width": "20%", "targets": 2 },
                    { "type": 'time-date-sort', "targets": 4 },
                    ],
                    "aaSorting": [[4, par]],
                    "iDisplayLength": 50,
                    "fnServerParams": function (aoData) {
                        aoData.push(
                        { "name": "rep", "value": re },
                        {
                            "name": "startdate", "value": star
                        },
                        {
                            "name": "enddate", "value": end
                        }
                        );
                    },
                    dom: 'lBfrtip',
                    buttons: [
                        'print'
                    ]

                });
            }

            function format(d) {
                // `d` is the original data object for the row   
                console.log(d);
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

            function repNameChange() {

                $('#RepNameDropDownList').change(function () {

                    callMe('desc');

                });
            }


            function ViewCompany(comId) {
                // localStorage.setItem("selectedRep", $('#RepNameDropDownList').val());
                // localStorage.setItem("selectedpage", pageClicked);
                //  localStorage.setItem("selectedtableData", table);
                window.open('ConpanyInfo.aspx?companyid=' + comId + "&lP=1", "blank");
            }
            function senddata() {

                var re = $("#RepNameDropDownList").val();

                //  CreateTableData(re);
                var stDate = $("#ContentPlaceHolder1_StartDateSTxt").val();
                var enDate = $("#ContentPlaceHolder1_EndDateSTxt").val();
                if (stDate != "dd/mm/yyyy" && enDate != "dd/mm/yyyy")
                    CreateTableData(re, "desc", stDate, enDate);
            }

            function resetAlll() {

                $("#ContentPlaceHolder1_StartDateSTxt").val('dd/mm/yyyy');
                $("#ContentPlaceHolder1_EndDateSTxt").val('dd/mm/yyyy');
                callMe('desc');
            }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="gridDiv">
        <br />


        <asp:Label ID="messagelable" runat="server" CssClass="messagecss" Font-Size="Medium"></asp:Label>
        

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
                    <td class="screen-heading">
                        <span style="float:left;margin-right:12px;" class="inputTextStyle" >REP NAME :</span>
                          <span style="float:left;margin-left:6px;" >
                        <asp:DropDownList ID="RepNameDropDownList" ClientIDMode="Static" runat="server" CssClass="inputColorGreen" Height="32px">
                            <asp:ListItem Text="All" Value="0"></asp:ListItem>
                            <%-- <asp:ListItem Text="House Account" Value="7"></asp:ListItem>
                              <asp:ListItem Text="Dimitri" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Aidan" Value="17"></asp:ListItem>
                            <asp:ListItem Text="Bailey" Value="14"></asp:ListItem>
                            <asp:ListItem Text="John" Value="10"></asp:ListItem>
                            <asp:ListItem Text="Krit" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Taras" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Trent" Value="15"></asp:ListItem>
                            <asp:ListItem Text="William" Value="18"></asp:ListItem>
                            <asp:ListItem Text="TestOne" Value="24"></asp:ListItem>--%>

                        </asp:DropDownList></span>


                    </td>
                </tr>
               
                 <tr>
                    <td class="screen-heading">

                        <span style="float:left;margin-right:12px;" class="inputTextStyle">START DATE: </span>

                        <span>
                            <input id="StartDateSTxt" type="date" name="StartDateTxt" class="inputColorGreen" style="float:left;margin-right:12px;height: 22px;" size="24" runat="server" />
                        </span>

                        <span style="float:left;margin-right:12px;" class="inputTextStyle">END DATE: </span>

                        <span>
                            <input id="EndDateSTxt" type="date" name="EndDateTxt" class="inputColorGreen" style="float:left;margin-right:12px;height: 22px;" size="24" runat="server" />
                        </span>
                        <span>
                            <input type="button" value="APPLY FILTER"  onclick="return senddata();" style="float:left;margin-right:12px;width: 160px; height: 30px; border: 1.5px solid blue; background-color: white; cursor: pointer;" />
                        </span>
                        <span>
                            <input type="button" value="RESET"  onclick="return resetAlll();" style="float:left;margin-right:12px;width: 160px; height: 30px; border: 1.5px solid red; background-color: white; cursor: pointer;" />
                        </span>
                    </td>
                </tr>
                 <tr height="25px">
                    <td height="25px" class="screen-heading"></td>
                </tr>
                <tr style="margin-top:12px;">
                    <td class="screen-heading">
                         <span class="inputTextStyle" style="float:left;margin-right:12px; padding-bottom: 20px;">SORT BY : </span>
                         <select id="sortinputdrop" class="inputColorGreen" style="float:left;margin-right:12px;margin-left:18px;width: 150px; height: 30px;">
                            <option value="dateorder">Ordered Date  </option>
                            <option value="comname">Company Name         </option>

                        </select>

                        <select id="sortinputdropdir" class="inputColorGreen" style="float:left;width: 150px; height: 30px; margin-left: 34px;">
                            <option value="desc">Desc </option>
                            <option value="asc">Asc       </option>

                        </select>
                    </td>
                </tr>
              

            </table>

        </div>

        <br />
       
        <div id="maindatatableMain">
             
            <br />
           
                        <table width="1500" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
                <tr>
                    <td height="25px"></td>
                </tr>


                <tr>
                    <td height="25px" class="section_headings">COMPANIES</td>
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
                                         <div id="tooltip"></div>
                                        <table cellpadding="0" width="1500" cellspacing="0" border="0" class="display" id="noordercompany" style="cursor: pointer">
                                            <thead>
                                                <tr>

                                                    <th align="left"></th>
                                                    <th align="left">View</th>
                                                    <th align="left">Company Name</th>
                                                    <th align="left">Contact Name</th>
                                                    <th align="left">Last Order Date</th>
                                                      <th align="left">Notes</th>
                                                    <th align="left"></th>
                                                     <th align="left">Telephone</th>
                                                    <th align="left">Owner</th>
                                                    <th align="left">Active</th>
                                                        <th align="left">Locked</th>  
                                                    <th align="left">Allocated Rep</th>
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
