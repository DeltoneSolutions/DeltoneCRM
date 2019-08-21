<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard1.aspx.cs" Inherits="DeltoneCRM.Dashboard1" MasterPageFile="~/Site1.Master" %>

<%@ MasterType VirtualPath="~/Site1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <title>DeltoneCRM - Dashboard</title>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />

    <link href='http://fonts.googleapis.com/css?family=Yanone+Kaffeesatz:400,700,300,200' rel='stylesheet' type='text/css'/>
    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'/>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'/>

    <script src="js/jquery-1.9.1.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>


    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

    <script src="js/jquery-ui-1.10.3.custom.js"></script>

    <script type="text/javascript">
        var dialog = '';

        function Edit(CompanyID) {
            $('#iframeEditCompany').attr('src', 'Manage/ViewEditScreens/ViewEditCustomers.aspx?cid=' + CompanyID + '&loc=DASH');
            $('#editCompanyIframeWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT CUSTOMER',
                width: 710,
            });

            return false;

        }

        function EditContact(ContactID) {
            $('#iframeEditContact').attr('src', 'Manage/ViewEditScreens/ViewEditcontact.aspx?cid=' + ContactID);
            $('#editContactIframeWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT CONTACT',
                width: 710,
            });

            return false;
        }

        function AddContact(CompID, Loc) {
            $('#iframeAddContact').attr('src', 'Manage/addcontact.aspx?cid=' + CompID + "&loc=" + Loc);
            $('#addContactIframeWindow').dialog({
                resizable: false,
                modal: true,
                title: 'ADD CONTACT',
                width: 710,
                height: 670,
            });

            return false;
        }

        function closeEditWindow(CompID) {
            $('#editCompanyIframeWindow').dialog('close');
            $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + CompID);
        }

        function ShowCompPrinters(CompID) {
            $('#hidden_company_id').val(CompID);
            $.ajax({
                url: "Fetch/FetchCompanyPrinters.aspx",
                data: {
                    CompID: CompID,
                },
                success: function (result) {
                    $('textarea[id*=txtPrinterList]').val(result);
                }
            });

            PLdialog = $('#CompanyPrinterList').dialog({
                resizable: false,
                modal: true,
                title: 'COMPANY PRINTER LIST',
                width: 710,
            });

            return false;
        }



        function reloadContactiFrame(CompanyID) {
            $('#editContactIframeWindow').dialog('close');
            $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + CompanyID);

        }

        function reloadAddContactIFrame(CompanyID) {
            $('#addContactIframeWindow').dialog('close');
            $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + CompanyID);
        }

        function reloadCompanyIFrame(CompanyID) {

            $('#editCompanyIframeWindow').dialog('close');
            $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + CompanyID);
        }

        function CreateOrder(ContactID, CompanyID) {
            window.open("order.aspx?cid=" + ContactID + "&Compid=" + CompanyID);
        }
        function CreateOrderDummy(ContactID, CompanyID) {
            window.open("DummyOrder.aspx?cid=" + ContactID + "&Compid=" + CompanyID);
        }
        function CreateOrderSub(ContactID, CompanyID) {
            window.open("../order.aspx?cid=" + ContactID + "&Compid=" + CompanyID);
        }

        function OpenCompany(CompanyID) {

            window.open('ConpanyInfo.aspx?companyid=' + CompanyID, "_self");
        }

        function OpenCompanyNewWindow(CompanyID) {

            window.open('ConpanyInfo.aspx?companyid=' + CompanyID, "_blank");
        }

        function autoResize(id) {
            var newheight;
            var newwidth;

            if (document.getElementById) {
                newheight = document.getElementById(id).contentWindow.document.body.scrollHeight;
                newwidth = document.getElementById(id).contentWindow.document.body.scrollWidth;
            }

            document.getElementById(id).height = (newheight) + "px";
            document.getElementById(id).width = (newwidth) + "px";
        }

        function autoPopUpWindow() {

        }


        function CreateQuote() {
            var win = window.open('CreateQuoteStep1.aspx', '_blank');
            win.focus();
        }

        $(document).ready(function () {

            $('#CompanyContactTR').hide();
            $('#findcontacts').hide();
            $('#findmycustomer').hide();
            $('#ApplyFilterDiv').hide();
            $('#SearchQuoteByCustomerDIV').hide();
            // $('#personalaccounts').accordion();

            $("#personalaccounts").accordion({
                collapsible: true,
                heightStyle: "content",
                active: false,
            });

            $('#persaccountstable').dataTable({
                "ajax": "Fetch/getpersonalaccounts.aspx",
            });


            $('#searchmycustomers').click(function () {
                $('#searchbartitle').text("Customer Name");
                $('#findcustomer').hide();
                $('#findcontacts').hide();
                $('#findmycustomer').show();
                $('#ApplyFilterDiv').hide();
                $('#SearchQuoteByCustomerDIV').hide();

                $('#searchallcustomers').removeClass();
                $('#searchallcustomers').addClass("tab02-01");

                $('#searchmycustomers').removeClass();
                $('#searchmycustomers').addClass("tab02-02");

                $('#searchcontacts').removeClass();
                $('#searchcontacts').addClass("tab02-03");

                $('#searchcustomersfilter').removeClass();
                $('#searchcustomersfilter').addClass("tab02-03");

                $('#searchquotecustomersfilter').removeClass();
                $('#searchquotecustomersfilter').addClass("tab02-03");

            });

            $('#searchallcustomers').click(function () {
                $('#searchbartitle').text("Customer Name");
                $('#findcustomer').show();
                $('#findcontacts').hide();
                $('#findmycustomer').hide();
                $('#ApplyFilterDiv').hide();
                $('#SearchQuoteByCustomerDIV').hide();

                $('#searchallcustomers').removeClass();
                $('#searchallcustomers').addClass("tab01-01");

                $('#searchmycustomers').removeClass();
                $('#searchmycustomers').addClass("tab01-02");

                $('#searchcontacts').removeClass();
                $('#searchcontacts').addClass("tab01-03");

                $('#searchcustomersfilter').removeClass();
                $('#searchcustomersfilter').addClass("tab02-03");

                $('#searchquotecustomersfilter').removeClass();
                $('#searchquotecustomersfilter').addClass("tab02-03");


            });

            $('#searchcontacts').click(function () {
                $('#searchbartitle').text("Contact Name");
                $('#findcustomer').hide();
                $('#findcontacts').show();
                $('#findmycustomer').hide();
                $('#ApplyFilterDiv').hide();
                $('#SearchQuoteByCustomerDIV').hide();

                $('#searchallcustomers').removeClass();
                $('#searchallcustomers').addClass("tab03-01");

                $('#searchmycustomers').removeClass();
                $('#searchmycustomers').addClass("tab03-02");

                $('#searchcontacts').removeClass();
                $('#searchcontacts').addClass("tab03-03");

                $('#searchcustomersfilter').removeClass();
                $('#searchcustomersfilter').addClass("tab02-03");

                $('#searchquotecustomersfilter').removeClass();
                $('#searchquotecustomersfilter').addClass("tab02-03");


            });

            $('#searchcustomersfilter').click(function () {
                $('#searchbartitle').text("");
                $('#findcustomer').hide();
                $('#findcontacts').hide();
                $('#findmycustomer').hide();
                $('#ApplyFilterDiv').show();
                $('#SearchQuoteByCustomerDIV').hide();

                $('#searchallcustomers').removeClass();
                $('#searchallcustomers').addClass("tab03-01");

                $('#searchmycustomers').removeClass();
                $('#searchmycustomers').addClass("tab03-01");

                $('#searchcontacts').removeClass();
                $('#searchcontacts').addClass("tab03-01");

                $('#searchcustomersfilter').removeClass();
                $('#searchcustomersfilter').addClass("tab02-02");

                $('#searchquotecustomersfilter').removeClass();
                $('#searchquotecustomersfilter').addClass("tab02-03");


            });


            $('#searchquotecustomersfilter').click(function () {
                $('#searchbartitle').text("Customer Name");
                $('#SearchQuoteByCustomerDIV').show();
                $('#findcustomer').hide();
                $('#findcontacts').hide();
                $('#findmycustomer').hide();
                $('#ApplyFilterDiv').hide();

                $('#searchquotecustomersfilter').removeClass();
                $('#searchquotecustomersfilter').addClass("tab02-02");

                $('#searchmycustomers').removeClass();
                $('#searchmycustomers').addClass("tab01-03");

                $('#searchallcustomers').removeClass();
                $('#searchallcustomers').addClass("tab03-01");

                $('#searchcontacts').removeClass();
                $('#searchcontacts').addClass("tab01-03");

                $('#searchcustomersfilter').removeClass();
                $('#searchcustomersfilter').addClass("tab02-03");


            });

            $('#findcontacts').autocomplete({
                source: "Fetch/FetchAllContacts.aspx",
                select: function (event, ui) {
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    $('#CompanyContactTR').show();
                    $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    setInterval(function () {
                        $("#CompanyContactiFrame").height($("#CompanyContactiFrame").contents().find("html").height());
                    }, 50);
                }
            })

            $('#findmycustomer').autocomplete({
                source: "Fetch/FetchAllMyCustomers.aspx",
                select: function (event, ui) {
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    $('#CompanyContactTR').show();
                    $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    setInterval(function () {
                        $("#CompanyContactiFrame").height($("#CompanyContactiFrame").contents().find("html").height());
                    }, 50);
                }
            });

            $('#findcustomer').autocomplete({

                source: "Fetch/FetchAllCompanies.aspx",
                select: function (event, ui) {
                    // alert("test" + ui.item.active);
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    $('#CompanyContactTR').show();
                    $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    setInterval(function () {
                        $("#CompanyContactiFrame").height($("#CompanyContactiFrame").contents().find("html").height());
                    }, 50);

                }

            });

            $('#findcustomerquote').autocomplete({

                source: "Fetch/FetchQuotedCompanySearch.aspx",
                select: function (event, ui) {
                   
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    //$('#CompanyContactTR').show();
                   // $('#CompanyContactiFrame').attr('src', 'QuoteInfoCustomer.aspx?CompanyID=' + ui.item.id);
                   
                        //$("#CompanyContactiFrame").height($("#CompanyContactiFrame").contents().find("html").height());
                        window.open("QuoteInfoCustomer.aspx?CompanyID=" + ui.item.id);
                    

                }

            });



            var ttt = "type=" + $("#applyfilterSelect").val();

            $('#applyfilterSelect').change(function () {
                $("#applycontacts").autocomplete("destroy");
                ttt = "type=" + $("#applyfilterSelect").val();

                $('#applycontacts').autocomplete({

                    source: "Fetch/FetchFilterAllCompanies.aspx?" + ttt,
                    minLength: 2,
                    select: function (event, ui) {
                        // alert("test" + ui.item.active);
                        //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                        $('#CompanyContactTR').show();
                        $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                        setInterval(function () {
                            $("#CompanyContactiFrame").height($("#CompanyContactiFrame").contents().find("html").height());
                        }, 50);

                    }

                });
            });

            $('#applycontacts').autocomplete({

                source: "Fetch/FetchFilterAllCompanies.aspx?" + ttt,
                minLength: 2,
                select: function (event, ui) {
                    // alert("test" + ui.item.active);
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    $('#CompanyContactTR').show();
                    $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    setInterval(function () {
                        $("#CompanyContactiFrame").height($("#CompanyContactiFrame").contents().find("html").height());
                    }, 50);

                }

            });




            //Save Printer List Changes
            $('#SavePrinterChanges').click(function () {
                $.ajax({
                    url: "process/processPrinterListChanges.aspx",
                    data: {
                        PrinterValues: $('textarea[id*=txtPrinterList]').val(),
                        CompID: $('#hidden_company_id').val(),
                    },
                    success: function (result) {
                        alert('Changes have been saved');
                        PLdialog.dialog("close");
                    }
                });
            });





        });

        //This Function Approve the Order
        function ApproveOrder(OrderID, CompnayID, ContactID) {

            //Naviagete to the Order form 
            window.open('Order.aspx?Oderid=' + OrderID + '&cid=' + ContactID + '&Compid=' + CompnayID + '&Action=PendingApproval');
        }
        //End Function Approve the Order



    </script>


    <style type="text/css">
        .width-980-style {
            width: 980px;
        }

        .width-940-style {
            width: 940px;
        }

        .width-950-style {
            width: 950px;
        }

        .height-25px-style {
            height: 25px;
        }

        .db1-auto-style5 {
            height: 35px;
            width: 20px;
        }

        .white-box {
            background-color: #ffffff;
        }

        .stats-style-01 {
            background-color: #fcddc7;
            width: 145px;
            height: 100px;
        }

        .stats-font-style-01 {
            font-family: 'Yanone Kaffeesatz', sans-serif;
            color: #808080;
            text-align: center;
            font-size: 16px;
        }

        .stats-font-style-02 {
            font-family: 'Yanone Kaffeesatz', sans-serif;
            color: #ff6a00;
            text-align: center;
            font-size: 36px;
        }

        .height-20-style {
            height: 20px;
        }

        .width-125-style {
            width: 125px;
        }

        .targetEmptyWarning {
            background-color: #F2F2F2;
            border: 5px solid #ccc;
            color: #000;
            width: 30%;
            position: fixed;
            top: 20%;
            left: 15%;
            padding: 25px;
            z-index: 101;
            display: none;
            margin-left: 20%;
        }

        .main-search-style {
            font-family: 'Raleway', sans-serif;
            font-size: 12px;
            color: #333333;
            width: 467px;
            height: 25px;
            border: 1px solid #ccc;
            outline: none;
            padding-left: 8px;
        }

        .performance-heading-style {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align: left;
            font-size: 18px;
            font-weight: 600;
        }

        .brdr-001-style {
            background-color: #ffffff;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
        }

        .brdr-left-right-style {
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #ffffff;
        }

        .brdr-left-right-bottom-style {
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            background-color: #ffffff;
        }

        .search-heading4-style {
            font-family: 'Raleway', sans-serif;
            color: #ccd0d9;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

        .search-details-style {
            font-family: 'Raleway', sans-serif;
            color: #999999;
            text-align: left;
            font-size: 11px;
            font-weight: 400;
            background-color: #ffffff;
            height: 25px;
        }

        .auto-style2 {
            width: 350px;
        }

        .performance-01-head-style {
            font-family: 'Raleway', sans-serif;
            color: #000;
            background-color: #fd6d52;
            height: 25px;
            text-align: center;
            font-size: 11px;
            font-weight: 600;
            border-bottom-color: #fff;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

        .StatsWidget {
            font-family: 'Raleway', sans-serif;
            color: white;
            font-size: 15px;
        }

        .performance-01-style {
            font-family: 'Open Sans', sans-serif;
            color: #ffffff;
            font-size: 16px;
            font-weight: 600;
            background-color: #e9573e;
            height: 35px;
            text-align: center;
        }

        .performance-02-head-style {
            font-family: 'Raleway', sans-serif;
            color: #000;
            background-color: #5d9cec;
            height: 25px;
            text-align: center;
            font-size: 11px;
            font-weight: 600;
            border-bottom-color: #fff;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

        .performance-02-style {
            font-family: 'Open Sans', sans-serif;
            color: #ffffff;
            font-size: 16px;
            font-weight: 600;
            background-color: #4b89dc;
            height: 30px;
            text-align: center;
        }

        .performance-03-head-style {
            font-family: 'Raleway', sans-serif;
            color: #000;
            background-color: #ac92ed;
            height: 25px;
            text-align: center;
            font-size: 11px;
            font-weight: 600;
            border-bottom-color: #fff;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

        .performance-03-style {
            font-family: 'Open Sans', sans-serif;
            color: #ffffff;
            font-size: 16px;
            font-weight: 600;
            background-color: #967bdc;
            height: 30px;
            text-align: center;
        }

        .performance-04-head-style {
            font-family: 'Raleway', sans-serif;
            color: #000;
            background-color: #ffce55;
            height: 25px;
            text-align: center;
            font-size: 11px;
            font-weight: 600;
            border-bottom-color: #fff;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

        .performance-04-style {
            font-family: 'Open Sans', sans-serif;
            color: #ffffff;
            font-size: 16px;
            font-weight: 600;
            background-color: #f6bb43;
            height: 30px;
            text-align: center;
        }

        .performance-05-head-style {
            font-family: 'Raleway', sans-serif;
            color: #000;
            background-color: #a0d468;
            height: 25px;
            text-align: center;
            font-size: 11px;
            font-weight: 600;
            border-bottom-color: #fff;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

        .performance-05-style {
            font-family: 'Open Sans', sans-serif;
            color: #ffffff;
            font-size: 16px;
            font-weight: 600;
            background-color: #8dc153;
            height: 30px;
            text-align: center;
        }

        .performance-06-style {
            background-color: #8dc153;
            height: 30px;
            text-align: center;
        }

        .btn-new-customer-style {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align: left;
            font-size: 11px;
            font-weight: 600;
            background-color: #ccd0d9;
            text-align: center;
            border: 1px solid #cccccc;
            height: 30px;
            width: 150px;
            outline: none;
        }

            .btn-new-customer-style:hover {
                background-color: #aab2bd;
                cursor: pointer;
            }

        .auto-style3 {
            width: 320px;
        }

        .auto-style4 {
            width: 550px;
        }

        .tab01-01 {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #ffffff;
            padding-left: 15px;
            border-top-color: #cccccc;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
        }

        .tab01-02 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

            .tab01-02:hover {
                color: #333333;
                background-color: #c4dde6;
                cursor: pointer;
            }

        .tab01-03 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-left-color: #ffffff;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

            .tab01-03:hover {
                color: #333333;
                background-color: #c4dde6;
                cursor: pointer;
            }

        .tab02-01 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

            .tab02-01:hover {
                color: #333333;
                background-color: #c4dde6;
                cursor: pointer;
            }

        .tab02-02 {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #ffffff;
            padding-left: 15px;
            border-top-color: #cccccc;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
        }

        .tab02-03 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

            .tab02-03:hover {
                color: #333333;
                background-color: #c4dde6;
                cursor: pointer;
            }

        .tab03-01 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }

            .tab03-01:hover {
                color: #333333;
                background-color: #c4dde6;
                cursor: pointer;
            }

        .tab03-02 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: #ffffff;
            border-left-style: solid;
            border-left-width: 1px;
        }

            .tab03-02:hover {
                color: #333333;
                background-color: #c4dde6;
                cursor: pointer;
            }

        .tab03-03 {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align: left;
            font-size: 14px;
            font-weight: 600;
            background-color: #ffffff;
            padding-left: 15px;
            border-top-color: #cccccc;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
        }
    </style>


</asp:Content>



<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
        <tr>
            <td class="height-20-style">&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                    <tr>
                        <td width="550px" class="performance-heading-style">
                            <div id="MonthYearlbl" runat="server"></div>
                        </td>
                        <td>
                            <table align="right" cellpadding="0" cellspacing="0" class="auto-style3">
                                <tr>
                                    <td class="performance-heading-style">
                                        <div id="DailyPerflbl" runat="server">Daily Performance</div>
                                    </td>
                                </tr>
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
            <td>
                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                    <tr>
                        <td>
                            <table align="left" cellpadding="0" cellspacing="0" class="auto-style4">
                                <tr>
                                    <td width="100" class="performance-01-head-style">Commission</td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-02-head-style">Volume</td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-03-head-style">Target Comm</td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-04-head-style">New Accounts</td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-05-head-style">Nb Calls</td>
                                </tr>
                                <tr>
                                    <td width="100" class="performance-01-style">
                                        <div id="ComissionDIV" runat="server"></div>
                                    </td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-02-style">
                                        <div id="monthlyvolume" runat="server"></div>
                                    </td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-03-style">
                                        <div id="targetDIV" runat="server"></div>
                                    </td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-04-style">
                                        <div id="newaccountDIV" runat="server"></div>
                                    </td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-05-style">
                                        <div id="workingdaysDIV" runat="server"></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table align="right" cellpadding="0" cellspacing="0" class="auto-style3">
                                <tr>
                                    <td width="100" class="performance-01-head-style">Commission</td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-02-head-style">Volume</td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-03-head-style">Target Comm</td>
                                </tr>
                                <tr>
                                    <td width="100" class="performance-01-style">
                                        <div id="DayComm" runat="server"></div>
                                    </td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-02-style">
                                        <div id="DayVol" runat="server"></div>
                                    </td>
                                    <td width="10">&nbsp;</td>
                                    <td width="100" class="performance-03-style">
                                        <div id="dailyneedDIV" runat="server"></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr id="tr_statsboard" runat="server" visible="false">
            <td class="height-25px-style">
                <table style="width: 100%;" cellspacing="0" cellspacing="0">
                    <tr>
                        <td style="background-color: #00C0EE; width: 220px; height: 100px">
                            <table style="width: 100%;" cellspacing="0" cellspacing="0">
                                <tr>
                                    <td class="StatsWidget">&nbsp;&nbsp;<span id="spn_PendingOrders" runat="server"></span> PENDING ORDER(S)</td>
                                    <td>
                                        <img alt="" src="images/orders.png" width="50px" style="text-align: center" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">&nbsp;</td>
                        <td style="background-color: #0CA65A; width: 220px">
                            <table style="width: 100%;" cellspacing="0" cellspacing="0">
                                <tr>
                                    <td class="StatsWidget">&nbsp;&nbsp;<span id="spn_PendingQuotes" runat="server"></span> PENDING QUOTE(S)</td>
                                    <td>
                                        <img alt="" src="images/orders.png" width="50px" style="text-align: center" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">&nbsp;</td>
                        <td style="background-color: #F39C12; width: 220px">
                            <table style="width: 100%;" cellspacing="0" cellspacing="0">
                                <tr>
                                    <td class="StatsWidget">&nbsp;&nbsp;<span id="spn_PendingCredits" runat="server"></span> PENDING CREDIT(S)</td>
                                    <td>
                                        <img alt="" src="images/orders.png" width="50px" style="text-align: center" /></td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px">&nbsp;</td>
                        <td style="background-color: #F56954; width: 220px">
                            <table style="width: 100%;" cellspacing="0" cellspacing="0">
                                <tr>
                                    <td class="StatsWidget">&nbsp;&nbsp;<span id="spn_OngoingCredits" runat="server"></span> ONGOING RMA(S)</td>
                                    <td>
                                        <img alt="" src="images/orders.png" width="50px" style="text-align: center" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: #00ADD7; text-align: center; color: white"><a href="orders/allorders.aspx">Go To Page</a></td>
                        <td style="width: 20px">&nbsp;</td>
                        <td style="background-color: #0A9651; text-align: center; color: white"><a href="allQuotesPending.aspx">Go To Page</a></td>
                        <td style="width: 20px">&nbsp;</td>
                        <td style="background-color: #DC912E; text-align: center; color: white"><a href="CreditNotes/AllCreditNotes.aspx">Go To Page</a></td>
                        <td style="width: 20px">&nbsp;</td>
                        <td style="background-color: #DD5F4C; text-align: center; color: white"><a href="CreditNotes/OngoingCredits.aspx">Go To Page</a></td>
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
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                    <tr>
                        <td width="250px" class="tab01-01" id="searchallcustomers">Search All Customers</td>
                        <td width="250px" class="tab01-02" style="display:none;" id="searchmycustomers">Search My Customers</td>
                        <td width="250px" class="tab01-03" id="searchcontacts">Search Contacts</td>
                        <td width="250px" class="tab01-03" id="searchcustomersfilter">Search By Filter</td>
                        <td width="250px" class="tab01-03" id="searchquotecustomersfilter">Search Quote Customers</td>
                        <td width="230px" class="search-heading4-style" style="cursor: pointer" onclick="CreateQuote();">CREATE QUOTE</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table align="left" cellpadding="0" cellspacing="0" class="width-980-style">
                    <tr>
                        <td width="250px" class="brdr-001-style" height="20px">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="brdr-left-right-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td class="search-details-style">
                            <div id="searchbartitle">Company Name</div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="brdr-left-right-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td width="500px">
                            <table align="left" cellpadding="0" cellspacing="0" class="auto-style2">
                                <tr>
                                    <td>
                                        <div id="SearchByCustomerDIV">
                                            <input name="findcustomer" type="text" class="main-search-style" id="findcustomer" size="60" />
                                        </div>
                                        <div id="SearchByMyCustomerDIV">
                                            <input name="findmycustomer" type="text" class="main-search-style" id="findmycustomer" size="60" />
                                        </div>
                                        <div id="SearchByContactDIV">
                                            <input name="findcontacts" type="text" class="main-search-style" id="findcontacts" size="60" />
                                        </div>
                                        <div id="SearchQuoteByCustomerDIV">
                                            <input name="findcustomerquote" type="text" class="main-search-style" id="findcustomerquote" size="60" />
                                        </div>
                                        <div id="ApplyFilterDiv">
                                            <select id="applyfilterSelect" style="width: 300px;">

                                                <option value="3">Shipping Address          </option>
                                                <option value="5">Shipping City     </option>
                                                <option value="4">Shipping Postal Code     </option>
                                                <option value="6">Shipping State     </option>
                                                <option value="7">Phone Number     </option>
                                                <option value="8">Email     </option>
                                            </select>

                                            <input name="applycontacts" type="text" class="main-search-style" id="applycontacts" size="60" />

                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <input type="button" id="xxx" class="btn-new-customer-style" value="ADD NEW CUSTOMER" onclick="window.open('Manage/editcustomers.aspx', '_blank')" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="brdr-left-right-bottom-style" height="20px">&nbsp;</td>
        </tr>
        <tr>
            <td class="height-25px-style">&nbsp;</td>
        </tr>


        <tr id="CompanyContactTR">
            <td class="white-box">
                <div id="CompanyContactDiv">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                        <tr>
                            <td>

                                <iframe id="CompanyContactiFrame" width="980" height="240" style="border: 0px;" onload="autoResize('CompanyContactiFrame');"></iframe>
                            </td>
                        </tr>

                    </table>
                </div>
            </td>

        </tr>


        <tr>
            <td>
                <div id="personalaccounts">
                    <h3>Personal Account</h3>
                    <div>


                        <table id="persaccountstable">

                            <thead>
                                <tr>
                                    <th width="300px">COMPANY NAME</th>
                                    <th>COMPNAY CONTACT</th>
                                    <th width="50px">VIEW</th>



                                </tr>
                            </thead>

                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>


        <tr>
            <td height="25px">
                <asp:TextBox ID="hidden_target_trigger" runat="server" Visible="false"></asp:TextBox>
                <input type="text" id="hidden_company_id" name="hidden_company_id" hidden="hidden" />
                <br />

            </td>
        </tr>
    </table>


    <asp:Panel ID="pnlMsgOrderApproval" runat="server" BorderColor="green" Visible="false">
        <asp:Label ID="lblOrderApproval" runat="server" ForeColor="Green"></asp:Label>
    </asp:Panel>

    <div id="editCompanyIframeWindow" style="display: none">
        <iframe id="iframeEditCompany" width="710" height="250" style="border: 0px;"></iframe>
    </div>

    <div id="editContactIframeWindow" style="display: none">
        <iframe id="iframeEditContact" width="710" height="600" style="border: 0px;"></iframe>
    </div>

    <div id="addContactIframeWindow" style="display: none">
        <iframe id="iframeAddContact" width="710" height="600" style="border: 0px;"></iframe>
    </div>

    <div id="CompanyPrinterList" style="display: none">
        <asp:TextBox ID="txtPrinterList" runat="server" TextMode="MultiLine" Width="700" Height="200"></asp:TextBox>
        <input type="button" id="SavePrinterChanges" name="SavePrinterChanges" value="Save Changes" />
    </div>


    <div id="addDialogCsPopNotify" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="NOTIFICATION">
        <table class="style1">

            <tr>
                <td class="alignRight">Message:</td>
                <td class="alignLeft">
                    <textarea id="complaintMessage" readonly="readonly" cols="70" rows="8"></textarea><br />
                </td>
            </tr>

            <%--<tr>
                <td class="alignRight">QUESTION :</td>
                <td class="alignLeft">
                    <textarea id="questionMessage" cols="70" rows="8"></textarea></td>
            </tr>

            <tr>
                <td class="alignRight">COMPLETED :</td>
                <td class="alignLeft">
                    <input type="checkbox" id="statusCheck" /></td>
            </tr>--%>
        </table>

    </div>

    <script type="text/javascript">
        var username = '<%= userName %>';
        var clickedId = 0;
        var statusCheck = false;
        $(document).ready(function () {

            $('#addDialogCsPopNotify').dialog({
                autoOpen: false,
                width: 700,
                modal: true,
                position: ['center', 'top'],
                show: 'blind',
                hide: 'blind',
                buttons: [

            {
                text: "Close",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },

                ]
            });


        });


        function OpenWindow() {
            $('#addDialogCsPopNotify').dialog('open');
            // alert("test");
        }

        

        function CloseWindow() {
            $('#addDialogCsPopNotify').dialog('close');

        }

        //  window.setTimeout("CloseWindow();", 10000);  // one minute.

        //function callReminderData() {

        //    $.ajax({
        //        url: 'Fetch/FetchCalendarReminder.aspx',

        //        success: function (data) {
        //            // console.log(data);
        //            if (data)
        //                notifyMe(data);

        //        },
        //        error: function (xhr, err) {
        //            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
        //            alert("responseText: " + xhr.responseText);
        //        },
        //    });
        //}

      


     

    </script>

    <style type="text/css">
        .ui-widget-content.ui-dialog {
            border: 1px solid #000 !important;
        }
    </style>

</asp:Content>
