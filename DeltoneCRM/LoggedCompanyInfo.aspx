<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoggedCompanyInfo.aspx.cs" Inherits="DeltoneCRM.LoggedCompanyInfo" MasterPageFile="~/BackButtonNav.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <title>CompanyInfo</title>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <link href="css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="js/jquery-1.11.1.min.js"></script>
    <link href="css/NewCSS.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.js"></script>

    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.dataTables_new.css" />

    <%-- <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jscolor.js"></script>
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />


    <!--Jquery UI References-->
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <style type="text/css">
        td {
            font-family: 'PT Sans Narrow', sans-serif;
            font-size: 12px;
            color: #333;
        }

        .Label_CompanyName {
            font-family: 'Droid Sans', sans-serif;
            font-size: 16px;
            font-weight: 400;
            letter-spacing: -0.05em;
        }

        .width-980-style {
            width: 980px;
        }

        .width-940-style {
            width: 940px;
        }

        .auto-style1 {
            background-color: #FFF;
            border: 1px solid #CCCCCC;
            width: 978px;
            height: 98px;
        }

        .auto-style2 {
            height: 27px;
        }

        .table_overall {
            background-color: #f6f8fb;
            border-top-width: 1px;
            border-right-width: 1px;
            border-left-width: 1px;
            border-bottom-width: 1px;
            border-top-style: solid;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            border-top-color: #CCCCCC;
            border-right-color: #CCCCCC;
            border-left-color: #CCCCCC;
            border-bottom-color: #CCCCCC;
            font-family: 'Open Sans', sans-serif;
            font-size: 12px;
            text-align: left;
        }

        .table_overall_heading {
            font-family: 'Open Sans', sans-serif;
            font-size: 12px;
            text-align: left;
            color: #eb473d;
            height: 35px;
            padding-left: 20px;
            font-weight: 600;
        }

        .table_overall_spacer {
            height: 25px;
        }

        .spanStyleLink {
            cursor: pointer;
            color: blue;
            text-decoration: underline;
            font-style: normal;
            font-size: 16px;
            text-align: center;
        }

        .submit_class_name {
            background-color: red !important;
            color: black !important;
        }
    </style>

    <script type="text/javascript">
        var canCall = '<%= SampleVariable %>';
        function LoadView(ContactID) {

            dialog.dialog("open");
            PopulateViewContact(ContactID);
        }

        function ViewCreditNote(CreditNoteID, ContactID) {

            window.open("UpdateCredit.aspx?CreditNoteID=" + CreditNoteID + "&cid=" + ContactID + "&CompID=" + $('#<%=hdnCompany.ClientID%>').val());
        }

        function ViewCal(EveId) {
            window.open("CalendarFull.aspx?evId=" + EveId);
        }


        //This function Load CompanyOrders 
        function LoadOrders(CompanyID, ContactID) {

            window.open("CompanyOrders.aspx?CompanyID=" + CompanyID + "&ContactID=" + ContactID);

        }

        //Function to create order from JTable link
        function CreateOrder(ContactID) {
            window.open("order.aspx?cid=" + ContactID + "&Compid=" + $('#<%=hdnCompany.ClientID%>').val());
        }

        function ViewQuote(QuoteID, ContactID, CompanyID, WhichDB) {

            var dbVal = "";
            if (WhichDB == 1)
                dbVal = "LiveDB";
            else
                dbVal = "QuoteDB";

            window.open("quote.aspx?Oderid=" + QuoteID + "&Compid=" + CompanyID + "&cid=" + ContactID + "&DB=" + dbVal);
        }

        function CreateQuote(ContactID) {
            window.open("quote.aspx?cid=" + ContactID + "&Compid=" + $('#<%=hdnCompany.ClientID%>').val() + "&FLAG=Y&EXISTS=Y&DB=LiveDB");
        }

        function CreateOrderDummy(ContactID, CompanyID) {
            window.open("DummyOrder.aspx?cid=" + ContactID + "&Compid=" + CompanyID);
        }


        //This function populate the Contact Details given by CompanyID
        function PopulateViewContact(contactID) {
            $.ajax({

                url: 'process/ProcessContactView.aspx',
                data: {
                    contactid: contactID,
                },
                success: function (result) {

                    fillContact(result);

                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });
        }

        function fillContact(data) {
            //spcontact, spfirstname, splastname, spTele, spMobile, spfax, spPostalAddress, spPhysicalAddress
            //alert(data);
            var Result = data.split("|");
            $('#spcontact').html(Result[0]);
            $('#spfirstname').html(Result[1]);
            $('#splastname').html(Result[2]);
            $('#spTele').html(Result[3]);
            $('#spMobile').html(Result[4]);
            $('#spfax').html(Result[5]);
            $('#spEmail').html(Result[6]);
            $('#spPostalAddress').html(Result[7]);
            $('#spPhysicalAddress').html(Result[8]);
        }

        //End Function populate Contact Details 

        function EditOrder(OrderID, CompanyID, ContactID) {
            window.open("Order.aspx?Oderid=" + OrderID + "&cid=" + ContactID + "&Compid=" + CompanyID);
        }

        function createQuote() {
            window.open("Quote.aspx");
        }

        function DismissAlarm(AID) {
            $.ajax({
                url: 'process/ProcessDeleteAlarm.aspx',
                data: {
                    AID: AID,
                },
                success: function (response) {
                    alert("Alarm successfully deleted");
                    location.reload();
                }
            });
        }

        $(document).ready(function () {


            $('#SaveCompNotes').click(function () {
                $.ajax({
                    type: "POST",
                    url: "Process/Process_AddCompanyNotes.aspx",
                    data: {
                        CompanyID: $('#<%=theCompID.ClientID%>').val(),
                        CompNotes: $('#<%=CompNotes.ClientID%>').val()
                    },
                    success: function (msg) {
                        alert('Notes have been successfully saved');
                        location.reload();
                    },
                    error: function (xhr, err) {
                        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                        alert("responseText: " + xhr.responseText);
                    }
                });
            });

            //View-Contact Dialog initialization
            dialog = $("#divViewContact").dialog({
                autoOpen: false,
                width: 800,
                modal: true,

                close: function () {

                    $('.blackout').css("display", "none");
                    dialog.dialog("close");
                }
            });
            //End View-Contact Dialog Initialization

            alarmdialog = $('#addalarm').dialog({
                autoOpen: false,
                width: 600,
                modal: true,
            });

            $.ajax({
                url: 'Fetch/getAlarmListForCompany.aspx',
                data: {
                    CID: $('#<%=hdnCompany.ClientID%>').val(),
                },
                success: function (response) {
                    if (response == "NONE") {
                        $('#alarmlist').hide();
                    }
                    else {
                        $('#alarmlist').show();
                        var overall = response.split('~');
                        $.each(overall, function (index, value) {
                            var eachitem = overall[index].split('|');
                            var newtablerow = '<tr><td width="20" height="20">&nbsp;</td><td>' + eachitem[1] + '</td><td>' + eachitem[2] + '</td><td>' + eachitem[3] + '</td><td width="20" onclick="DismissAlarm(' + eachitem[0] + ')"><img alt="" src="images/x.png" /></td><td width="20">&nbsp;</td></tr>';
                            $('#alarmlisttable').append(newtablerow);
                        });
                    }
                }
            });

            $('#btn_ProcessAddAlarm').click(function () {
                $.ajax({
                    url: 'process/ProcessAddAlarm.aspx',
                    data: {
                        CID: $('#<%=hdnCompany.ClientID%>').val(),
                        Notes: $('#alarmtext').val(),
                        StartDate: $('#AlarmDate').val(),
                    },
                    success: function (response) {
                        alert('Alarm successfully added.');
                        alarmdialog.dialog('close');
                        location.reload();
                    }
                });
            });

            $('#btn_AddAlarm').click(function () {
                alarmdialog.dialog('open');
            });

            //Fetch the Hidden value 
            company = $('#<%=hdnCompany.ClientID%>').val();

            $('#QuotesTbl').dataTable({
                "columnDefs": [
          {
              "targets": [5],
              "visible": false,
              "searchable": false
          }],
                "ajax": "Fetch/FetchQuotesForCompany.aspx?cid=" + company,

            });

            $('#example').dataTable({

                "ajax": "Fetch/FetchContacts.aspx?cid=" + company,
                "columnDefs": [
            {
                "targets": [0],
                "visible": false,
                "searchable": false
            },
            {
                "targets": [3],
                "visible": false,
                "searchable": false
            },
             {
                 "targets": [4],
                 "visible": false,
                 "searchable": false
             },
              {
                  "targets": [5],
                  "visible": false,
                  "searchable": false
              },
               {
                   "targets": [6],
                   "visible": false,
                   "searchable": false
               },
               {
                   "targets": [7],
                   "visible": false,
                   "searchable": false
               },

            { className: 'align_left', "targets": [1, 2, 3] },
            { className: 'align_center', "targets": [0, 4, 5, 6, 7] },


                ],
                "order": [[1, "asc"]],

            });

            $('#example').on('click', 'tbody tr', function (e) {
                var contactID = $('td', this).eq(0).text();
                //window.open("order.aspx?cid=" + contactID);
            });

            $('#OrderList').dataTable({
                "ajax": "Fetch/FetchAllOrdersForCompany.aspx?companyid=" + $('#<%=hdnCompany.ClientID%>').val(),
                "columnDefs": [
                     {
                         "targets": [9],
                         "visible": false,
                         "searchable": false
                     },
                    { className: 'align_left', "targets": [1, 2, 3, 4] },
                    { className: 'align_center', "targets": [0, 5, 6, 7, 8, 9] },

                ],
                "order": [[0, "desc"]],

            });

            //Load the DataTable of CreditNoteList
            $('#tblCreditNoteList').dataTable({

                "ajax": "Fetch/FetchCreditNotes.aspx?companyid=" + $('#<%=hdnCompany.ClientID%>').val(),
                "columnDefs": [
                    
          {
              "targets": [8],
              "visible": false,
              "searchable": false
          },

                // { className: 'align_left', "targets": [1, 2, 3] },
                // { className: 'align_center', "targets": [0, 4, 5, 6, 7, 8] },

                ],
                "order": [[0, "desc"]],
            });

            if (canCall) {

                $("#showSpan").show();

            }


            var tableschedule = $('#tblscheduleEvent').dataTable({
                "ajax": "Fetch/FetchScheduleForCompany.aspx?companyid=" + $('#<%=hdnCompany.ClientID%>').val(),

                "columnDefs": [
                     {
                         "targets": [4],
                         "visible": false,
                         "searchable": false
                     },
                  { className: 'align_left', "targets": [0, 1, 2, 3] },
                  { className: 'align_center', "targets": [4] },
                  { type: 'date-dd/mm/yyyy', targets: 2 }

               ],
               "order": [[2, "desc"]],
            
               "iDisplayLength": 10,
               
           });

            var table = "";

            $("#showSpan").click(function () {
                $("#showSpan").hide();
                $("#hideSpan").show();
                $("#historytr").show();
                $("#historyheader").show();
                if (table)
                    table.destroy();

                table =
                $('#tblhistory').dataTable({

                    "ajax": "Fetch/FetchCompanyAudit.aspx?companyid=" + $('#<%=hdnCompany.ClientID%>').val(),
                    "columnDefs": [
                     { className: 'align_left', "targets": [1, 2] },
                     { className: 'align_center', "targets": [3] },

                    ],
                    "order": [[1, "desc"]],
                    "iDisplayLength": 25,
                });

            });

            $("#hideSpan").click(function () {
                $("#hideSpan").hide();
                $("#showSpan").show();
                $("#historytr").hide();
                $("#historyheader").hide();
            });

        });




    </script>

</asp:Content>

<asp:Content ID="TheMainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <asp:ScriptManager ID="ScriptManagercaom" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>


    <div>
        <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
            <tr>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td>
                    <table id="titleheader" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td> <h3>  Account is Being Locked </h3> </td>
                        </tr>
                        <tr>
                            <td align="left" width="500">
                                <span class="Label_CompanyName" align="left">COMPANY NAME:
                                    <asp:Label ID="lbl_CompName" runat="server" Text="Label"></asp:Label></span>
                            </td>
                            <td width="250">&nbsp;</td>
                            <td width="100" align="right">&nbsp;</td>
                            <td width="30">&nbsp;</td>
                            <td width="100" align="right">
                                <input id="btn_AddAlarm" type="button" style="display:none;" value="ADD ALARM" class="AddAlarmBTN" /></td>
                        </tr>
                        <tr>
                            <td style="font-size:14px;"> 
                                <span>Contact :</span>
                                <asp:Label ID="contactName" runat="server"></asp:Label>

                            </td>
                            
                        </tr>
                         <tr>
                            <td style="font-size:14px;"> 
                                <span>Telephone :</span>
                                 <asp:Label ID="ContactTelephone" runat="server"></asp:Label>

                            </td>
                            
                        </tr>
                         <tr>
                            <td style="font-size:14px;"> 
                                <span>Mobile :</span>
                               
                                 <asp:Label ID="contactPhone" runat="server"></asp:Label>
                            </td>
                            
                        </tr>
                         <tr>
                            <td style="font-size:14px;"> 
                                <span>Email :</span> 
                                <asp:Label ID="email" runat="server"></asp:Label></td>
                            
                        </tr>
                         <tr>
                            <td style="font-size:14px;"> 
                                <span>Address :</span> 
                                <asp:Label ID="address" runat="server"></asp:Label></td>
                            
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td height="25px">&nbsp;</td>
            </tr>
            <tr>
                <td height="25px">
                    <table style="width: 100%">
                        <tr>
                            <td>COMPANY SETTINGS</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Payment Terms:
                                     <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="payment-terms-drop">
                                         <asp:ListItem Text="7 days" Value="7"></asp:ListItem>
                                         <asp:ListItem Text="14 days" Value="14"></asp:ListItem>
                                         <asp:ListItem Text="21 days" Value="21"></asp:ListItem>
                                         <asp:ListItem Text="30 days" Value="30"></asp:ListItem>
                                         <asp:ListItem Text="days 45" Value="45"></asp:ListItem>

                                     </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <%--<asp:Button ID="btn_SaveSettings" runat="server" Text="SAVE" OnClick="btn_SaveSettings_Click" />

                                <input type="button" id="btn_schedulteEvent" style="margin-left: 20px;color:red;" onclick="openDialog();" value="Schedule Event" />--%>
                            </td>
                        </tr>

                    </table>

                </td>
            </tr>
            <tr>
                <td height="25px">&nbsp;</td>
            </tr>
            <tr id="alarmlist">
                <td>
                    <table id="alarmlisttable" cellspacing="0" cellpadding="0" width="980" class="table_overall">
                        <tr>
                            <td class="table_overall_heading" colspan="6">ALARM LIST</td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20" height="30">&nbsp;</td>
                            <td>ALARM DATE</td>
                            <td>DESCRIPTION</td>
                            <td>CREATED BY</td>
                            <td width="20">&nbsp;</td>
                            <td width="20">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="25px">&nbsp;</td>
            </tr>
            <tr>
                <td class="section_headings">COMPANY NOTES<input type="text" name="theCompID" id="theCompID" hidden="hidden" runat="server" /></td>
            </tr>
            <tr>
                <td class="white-box-outline">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>

                                <asp:TextBox ID="CompNotes" TextMode="multiline" Width="100%" Rows="8" runat="server"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td height="20px">
                                
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
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td class="section_headings">SCHEDULED EVENT LIST</td>
            </tr>
             <tr>
                <td class="white-box-outline">
              <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">

                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table class="display" id="tblscheduleEvent">
                                    <thead>
                                        <tr>
                                           <th>Id</th>
                                             <th>Created Date</th>
                                            <th>Event Date</th>
                                            <th>Description</th>
                                          <th style="display:none;">View</th>

                                        </tr>

                                    </thead>
                                    <tbody>
                                    </tbody>



                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            
            
             <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="section_headings">CONTACT LIST</td>
            </tr>
            <tr>
                <td class="white-box-outline">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>

                                <table class="display" id="example">
                                    <thead>
                                        <tr>
                                            <th>CONTACT ID</th>
                                            <th>CONTACT NAME</th>
                                            <th>CONTACT ADDRESS</th>
                                            <th>CONTACT PHONE</th>
                                            <th width="50px" style="display:none;">VIEW</th>
                                            <th width="50px" style="display:none;">NEW ORDER</th>
                                            <th width="50px" style="display:none;">NEW QUOTE</th>
                                            <th width="50px" style="display:none;">CDO</th>
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
                <td height="25px">&nbsp;</td>
            </tr>
            <tr>
                <td class="section_headings">PAST ORDERS LIST</td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>

                                <table class="display" id="OrderList">
                                    <thead>
                                        <tr>
                                            <th>ORDER ID</th>
                                            <th>INVOICE ID</th>
                                            <th>ORDER DATE</th>
                                            <th>CONTACT NAME</th>
                                            <th>CONTACT PHONE</th>
                                            <th>TOTAL</th>
                                            <th>COMMISION</th>
                                            <th>STATUS</th>
                                            <th>CREATED BY</th>
                                            <th style="display:none;">VIEW</th>
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
                <td class="section_headings">QUOTES</td>
            </tr>

            <tr>
                <td class="auto-style1">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">

                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table class="display" id="QuotesTbl">
                                    <thead>
                                        <tr>
                                            <th>QUOTE ID</th>
                                            <th>CREATED DATE</th>
                                            <th>CONTACT NAME</th>
                                            <th>QUOTE TOTAL</th>
                                            <th>STATUS</th>
                                            <th style="display:none;">VIEW</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                    </tbody>



                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td class="section_headings">CREDIT NOTE LIST</td>

            </tr>
            <tr>
                <td class="auto-style1">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">

                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table class="display" id="tblCreditNoteList">
                                    <thead>
                                        <tr>
                                            <th>SYSTEM ID</th>
                                            <th>CREDIT NOTE ID</th>
                                            <th>ORDERID</th>
                                            <th>CREATED DATE</th>
                                            <th>CONTACT NAME</th>
                                            <th>TOTAL</th>
                                            <th>CREATED BY</th>
                                            <th>STATUS</th>
                                            <th style="display:none;">VIEW</th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                    </tbody>



                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><span id="showSpan" class="spanStyleLink" style="display: none;">Show History</span>
                    <span id="hideSpan" class="spanStyleLink" style="display: none;">Hide History</span>
                </td>
            </tr>
            <tr id="historyheader" style="display: none;">
                <td class="section_headings">History</td>

            </tr>
            <tr id="historytr" style="display: none;">
                <td class="auto-style1">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">

                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table class="display" id="tblhistory">
                                    <thead>
                                        <tr>
                                            <th>Action</th>
                                            <th>Created Date</th>
                                            <th>Action By</th>
                                            <th>Data Changes</th>

                                        </tr>

                                    </thead>
                                    <tbody>
                                    </tbody>



                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
        </table>

        <!--Company Details Panel-->
        <div id="divCompany">

            <input value="" id="hdnCompany" type="hidden" runat="server" />

        </div>
        <!--End Company Details Panel-->



        <!--Notes Panel-->
        <div id="divNotes">
        </div>
        <!--End Notes Panel-->

        <!--OutStandingpayments panel-->
        <div id="divOutPayments">
        </div>
        <!--End OutStandingpayments panel-->
    </div>

    

    <!--Dialog View Contact Info-->
    <div id="divViewContact" title="ViewContact">
        <table>
            <tr>
                <td>Contact</td>
                <td><span id="spcontact"></span></td>
            </tr>
            <tr>
                <td>FirstName</td>
                <td><span id="spfirstname"></span></td>
            </tr>
            <tr>
                <td>LastName</td>
                <td><span id="splastname"></span></td>

            </tr>
            <tr>
                <td>Telephone</td>
                <td><span id="spTele"></span></td>
            </tr>
            <tr>
                <td>Mobile</td>
                <td><span id="spMobile"></span></td>
            </tr>
            <tr>
                <td>Fax</td>
                <td><span id="spfax"></span></td>
            </tr>
            <tr>
                <td>Email</td>
                <td><span id="spEmail"></span></td>
            </tr>

            <tr>
                <td>Postal address</td>
                <td><span id="spPostalAddress"></span></td>
            </tr>
            <tr>
                <td>Physical Address</td>
                <td><span id="spPhysicalAddress"></span></td>
            </tr>

        </table>
    </div>
    <!--End View Contact Info-->



    <!--Dialog Edit Contact Info-->
    <div id="divEditContact" title="EditContact">
        <table>
        </table>
    </div>
    <!--End Dialog Edit ContactInfo-->

    <div id="addalarm" title="ADD ALARM">
        <table width="600" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="15">&nbsp;</td>
                <td>&nbsp;</td>
                <td width="15">&nbsp;</td>
            </tr>
            <tr>
                <td width="15">&nbsp;</td>
                <td>Pick a date for your alarm:
                    <input id="AlarmDate" type="date" class="AlarmDatePicker" /></td>
                <td width="15">&nbsp;</td>
            </tr>
            <tr>
                <td width="15">&nbsp;</td>
                <td>&nbsp;</td>
                <td width="15">&nbsp;</td>
            </tr>
            <tr>
                <td width="15">&nbsp;</td>
                <td align="center">
                    <textarea id="alarmtext" cols="20" rows="2" style="width: 570px; height: 100px;" class="AlarmTextArea"></textarea></td>
                <td width="15">&nbsp;</td>
            </tr>
            <tr>
                <td width="15">&nbsp;</td>
                <td>&nbsp;</td>
                <td width="15">&nbsp;</td>
            </tr>
            <tr>
                <td width="15">&nbsp;</td>
                <td align="right">
                    <input id="btn_ProcessAddAlarm" type="button" value="SAVE" class="DialogSaveBTN" /></td>
                <td width="15">&nbsp;</td>
            </tr>
            <tr>
                <td width="15">&nbsp;</td>
                <td>&nbsp;</td>
                <td width="15">&nbsp;</td>
            </tr>
        </table>

    </div>

    <div id="addDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Add Event">
        <table class="style1">
            <tr>
                <td class="alignRight">Name:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" size="60" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="60" rows="16"></textarea></td>
            </tr>
                <tr>
                <td class="alignRight">
                   Background Color:</td>
                <td class="alignLeft">
                  
<%--<input value="ffcc00" type="text" id="addbgcolor" class="jscolor {width:243, height:150, position:'right',
    borderColor:'#FFF', insetColor:'#FFF', backgroundColor:'#666'}"/>--%>
                              <select id="addbgcolor">
 <option value="#FF0000" style="background-color:#FF0000;"">   Customer Service  </option>
 <option value="#0000FF" style="background-color:#0000FF">  Reorder         </option>
 <option value="#00FF00" style="background-color:#00FF00">   New Quote          </option>
 <option value="#800080" style="background-color:#800080">    Call Back     </option> 
                        <option value="#008000" style="background-color:#008000"> Quote Follow Up </option>

                        <option value="#808080" style="background-color:#808080"> Other   </option>
                        <option value="#800000" style="background-color:#800000">  Follow Up Cs     </option>
                        <option value="#FFFF00" style="background-color:#FFFF00">  Auth Name   </option>
                        <option value="#FA8072" style="background-color:#FA8072">  Sale Follow Up     </option>
                        
</select>

                </td>
            </tr>
            <tr>
                <td class="alignRight">Call Date:</td>
                <td class="alignLeft">
                    <input id="addEventStartDate" type="text" size="33" /><br />
                </td>

            </tr>
          <%--  <tr>
                <td class="alignRight">End:</td>
                <td class="alignLeft">
                    <input id="addEventEndDate" type="text" size="33" />
                </td>
            </tr>--%>
        </table>

    </div>
    <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />

    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-sliderAccess.js"></script>
    <script type="text/javascript">

        function openDialog() {

            $("#addEventName").val($('#ContentPlaceHolder1_lbl_CompName').text());
            $('#addDialog').dialog('open');
        }

        $(document).ready(function () {
            var dateNow = new Date();
            var hourset = dateNow.getHours();
            $('#addEventStartDate').datetimepicker({
                controlType: 'select',
                timeFormat: 'hh:mm tt',
                stepMinute: 30,
                hour: hourset,
                minute: 30,
                dateFormat: 'dd/mm/yy',
            });

          
            var monthAdd = dateNow.getMonth() + 1;
            var setDateForm = dateNow.getDate() + "/" + monthAdd + "/" + dateNow.getFullYear();


            $("#addEventStartDate").datepicker({
                dateFormat: 'dd/mm/yy'
            }).datepicker('setDate', dateNow);

            //$('#addEventEndDate').datetimepicker({
            //    controlType: 'select',
            //    timeFormat: 'hh:mm tt',
            //    stepMinute: 30,
            //    hour: hourset,
            //    minute: 30
            //});

            $('#addDialog').dialog({
                autoOpen: false,
                width: 700,
                modal: true,
                buttons: [

            {
                text: "Cancel",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "Add",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
                    var eventToAdd = {
                        title: $("#addEventName").val(),
                        description: $("#addEventDesc").val(),
                        start: $("#addEventStartDate").val(),
                        color: $("#addbgcolor").val()
                    };

                    if (validateInput()) {
                        //alert("sending " + eventToAdd.title);

                        PageMethods.addEvent(eventToAdd, addSuccess);
                        $(this).dialog("close");
                    }
                }
            }
                ]
            });

        });

        function addSuccess(addResult) {

            alert("Event Successfully Scheduled.");
        }

        function validateInput() {

            if ($("#addEventName").val() == "") {
                alert("Please enter name");
                return false;
            }
            if ($("#addEventDesc").val() == "") {
                alert("Please enter description");
                return false;
            }
            if ($("#addEventStartDate").val() == "") {
                alert("Please select start date and time");
                return false;
            }
            //if ($("#addEventEndDate").val() == "") {
            //    alert("Please select end date and time , should be grater than start date.");
            //    return false;
            //}

            //var startTime = new Date($("#addEventStartDate").val());
            //var endTime = new Date($("#addEventEndDate").val());

            //if (endTime.getMilliseconds() < startTime.getMilliseconds()) {
            //    alert("End date must be greater than start date");
            //    return false;
            //}

            //var from = $("#addEventStartDate").val().split("/");
            //var f = new Date(from[2], from[1] - 1, from[0]);

            return true;
        }

        function checkForSpecialChars(stringToCheck) {
            var pattern = /[^A-Za-z0-9 ]/;
            return pattern.test(stringToCheck);
        }

    </script>
</asp:Content>
