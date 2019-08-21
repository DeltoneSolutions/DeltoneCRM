<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CreateQuoteStep1.aspx.cs" Inherits="DeltoneCRM.CreateQuoteStep1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>

    <link href='http://fonts.googleapis.com/css?family=Yanone+Kaffeesatz:400,700,300,200' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>
    <link href="https://fonts.googleapis.com/css?family=Ubuntu" rel="stylesheet">

    <script src="js/jquery-1.9.1.js"></script>
	<script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="css/NewCSS.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>


    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

    <script src="js/jquery-ui-1.10.3.custom.js"></script>


    <script type="text/javascript">

        function myFunction() {
            // some stuff
            return true; // important
        }

        function showDialog() {
            ContactFormDIV = $('#QuoteCustomerCreateDIV').dialog({
                resizable: false,
                //modal: true,
                title: 'QUOTE - CREATE CUSTOMER',
                width: 980,
                //autoOpen: false,
            });
            ContactFormDIV.parent().appendTo($("form:first"));

            return false;
        };

        $(document).ready(function () {

            var SelectedFirstName = "";
            var SelectedLastName = "";

            document.getElementById('ContentPlaceHolder1_txt_CompanyName').addEventListener('keypress', function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });
            document.getElementById('ContentPlaceHolder1_txt_ContactFirstName').addEventListener('keypress', function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });
            document.getElementById('ContentPlaceHolder1_txt_ContactLastName').addEventListener('keypress', function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });
            document.getElementById('ContentPlaceHolder1_txt_EmailAddress').addEventListener('keypress', function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });

            document.getElementById('ContentPlaceHolder1_txt_PhoneNumber').addEventListener('keypress', function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });
            document.getElementById('NewShipLine1').addEventListener('keypress', function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });
            document.getElementById('NewShipCity').addEventListener('keypress', function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });

            document.getElementById('NewShipPostcode').addEventListener('keypress', function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                }
            });
            showDialog();
            $('#dropdown_tr_spacer').hide();
            $('#dropdown_tr').hide();
            $('#cp_tr_errmsg').hide();
            $('#em_tr_errmsg').hide();
            

            ContactFormDIV.on('dialogclose', function (event) {
                window.close();
            });

            $('#<%=txt_CompanyName.ClientID%>').keyup(function () {
                if ($('#<%=txt_CompanyName.ClientID%>').val() != '')
                {
                    $('#cp_err_msg').text('');
                    $('#ddl_ExisitingUsers').empty();
                    $('#<%=txt_CompanyName.ClientID%>').removeClass('txtbox_quote_error');
                    $.ajax({
                        url: 'Fetch/VerifyIfCompanyExists.aspx',
                        data: {
                            CompanyName: $('#<%=txt_CompanyName.ClientID%>').val(),
                        },
                        success: function (result) {
                            var data = result.split('|');
                            if (data[0] == "RED") {
                                //alert('This company already exists in the CRM');
                                $('#err_msg').text('This company already exists in the CRM');
                                $('#<%=hdn_CustAlreadyExists.ClientID%>').val("YES");

                            }
                            else {
                                $('#<%=hdn_CustAlreadyExists.ClientID%>').val("NO");
                            }
                            if (data[1] != "0") {
                                $('#<%=hdn_ExistingCompanyID.ClientID%>').val(data[1]);
                                $('#<%=hdn_QueryDB.ClientID%>').val("LiveDB");
                                $('#dropdown_tr_spacer').show();
                                $('#dropdown_tr').show();
                                $('#<%=txt_ContactFirstName.ClientID%>').attr('readonly', 'readonly');
                                $('#<%=txt_ContactLastName.ClientID%>').attr('readonly', 'readonly');
                                $('#ddl_ExisitingUsers').empty();
                                var firstOption = "<option value=''>-- SELECT -- </option>";
                                $('#ddl_ExisitingUsers').append(firstOption);
                                $.ajax({
                                    url: 'Fetch/FetchContactsForCompany.aspx',
                                    data: {
                                        CID: data[1],
                                    },
                                    success: function (response) {
                                        var separate = response.split('~');
                                        for (var i = 0; i < separate.length; i++) {
                                            var option = separate[i].split('|');
                                            consOption = "<option value='" + option[0] + "'>" + option[1] + " " + option[2] + "</option>";
                                            $('#ddl_ExisitingUsers').append(consOption);
                                        }
                                    }
                                });
                            }
                            else {
                                
                                $('#dropdown_tr_spacer').hide();
                                $('#dropdown_tr').hide();
                                $('#<%=txt_ContactFirstName.ClientID%>').attr('readonly', false);
                                $('#<%=txt_ContactLastName.ClientID%>').attr('readonly', false);
                                $('#<%=txt_ContactFirstName.ClientID%>').val('');
                                $('#<%=txt_ContactLastName.ClientID%>').val('');
                                $('#<%=txt_EmailAddress.ClientID%>').val('');
                                $('#<%=txt_PhoneNumber.ClientID%>').val('');
                            }
                            if (data[2] == "RED") {
                                //alert('You have already quoted this customer');
                                $('#cp_tr_errmsg').show();
                                $('#<%=txt_CompanyName.ClientID%>').addClass('txtbox_quote_error');
                                $('#cp_err_msg').text('You have already quoted this customer');
                            }
                            if (data[3] != "0" && data[1] == "0") {
                                $('#<%=hdn_ExistingCompanyID.ClientID%>').val(data[3]);
                                $('#<%=hdn_QueryDB.ClientID%>').val('QuoteDB');
                            }
                        }

                    });
                }
                
            });

            $('#ddl_ExisitingUsers').change(function () {
                $.ajax({
                    url: 'Fetch/FetchContactNamesByID.aspx',
                    data: {
                        cid: $('#ddl_ExisitingUsers').val(),
                    },
                    success: function (response) {
                        var result = response.split('|');
                        $('#<%=txt_ContactFirstName.ClientID%>').val(result[0]);
                        $('#<%=txt_ContactLastName.ClientID%>').val(result[1]);
                        $('#<%=txt_EmailAddress.ClientID%>').val(result[2]);
                        $('#<%=txt_PhoneNumber.ClientID%>').val(result[3]);

                        $('#<%=NewShipLine1.ClientID%>').val(result[4]);
                        $('#<%=NewShipCity.ClientID%>').val(result[5]);
                        $('#<%=NewShipPostcode.ClientID%>').val(result[6]);
                        $('#<%=NewShipState.ClientID%>').val(result[7]);

                        $('#em_tr_errmsg').hide();
                        if ($('#<%=txt_EmailAddress.ClientID%>').hasClass('txtbox_quote_error'))
                        {
                            $('#<%=txt_EmailAddress.ClientID%>').removeClass('txtbox_quote_error')
                        }
                    },
                });
            });

            $('#<%=txt_EmailAddress.ClientID%>').keyup(function () {
                $('#em_err_msg').text('');
                $('#<%=txt_EmailAddress.ClientID%>').removeClass('txtbox_quote_error');
                $.ajax({
                    url: 'Fetch/VerifyIfEmailExists.aspx',
                    data: {
                        Email: $('#<%=txt_EmailAddress.ClientID%>').val(),
                    },
                    success: function (result) {
                        var data = result.split('|');
                        if (data[0] == "RED") {
                            $('#em_tr_errmsg').show();
                            $('#<%=txt_EmailAddress.ClientID%>').addClass('txtbox_quote_error');
                            $('#em_err_msg').text('This contact already exists in the CRM');
                            //alert('This contact already exists in the CRM');
                            $('#<%=hdn_CustAlreadyExists.ClientID%>').val("YES");
                        }
                        else {
                            if ($('#<%=hdn_CustAlreadyExists.ClientID%>').val() != "YES") {
                                $('#<%=hdn_CustAlreadyExists.ClientID%>').val("NO");
                            }
                            
                        }
                        if (data[1] == "RED") {
                            $('#em_tr_errmsg').show();
                            $('#<%=txt_EmailAddress.ClientID%>').addClass('txtbox_quote_error');
                            $('#em_err_msg').text('You have already quoted this customer');
                            //alert('You have already quoted this customer');
                        }
                    }
                });
            });

            //ContactFormDIV.dialog('open');
        });


        function validateComma(value) {

            if (value.indexOf(',') > -1) {

                returnvalue = false;
            }

             returnvalue = true;

        }

        function validateInputForComma() {

            var address1 = $("#NewShipLine1").val();
           
            if (address1 != "") {
                if (address1.indexOf(',') > -1) {
                    alert("SHIPPING ADDRESS LINE 1 contains comma.Please remove that");
                    return false;
                }
            }

            var city = $("#NewShipCity").val();
            if (city != "") {
                if (city.indexOf(',') > -1) {
                    alert("SHIPPING ADDRESS CITY contains comma.Please remove that");
                    return false;
                }
            }
            var state = $("#NewShipState").val();
            if (state != "") {
                if (state.indexOf(',') > -1) {
                    alert("SHIPPING ADDRESS STATE contains comma.Please remove that");
                    return false;
                }
            }
            var postcvode = $("#NewShipPostcode").val();
            if (postcvode != "") {
                if (postcvode.indexOf(',') > -1) {
                    alert("SHIPPING ADDRESS POSTCODE line contains comma.Please remove that");
                    return false;
                }
            }


            return true;

        }

        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        </table>

    <div id="QuoteCustomerCreateDIV" style="background-color:#C1C7C9">
        <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td width="250px">&nbsp;</td>
                            <td style="text-align:center;"><span class="Quote_Header_Title">CREATE NEW CONTACT</span></td>
                            <td width="250px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td width="920px">
                                <asp:TextBox ID="txt_CompanyName" runat="server" CssClass="Quote-Contact-Textbox-style" AutoComplete="Off"
                                     placeholder="COMPANY NAME" required="required"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr id="cp_tr_errmsg">
                            <td width="20px">&nbsp;</td>
                            <td><span id="cp_err_msg" class="Quote_Creation_ErrMsg"></span></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr id="dropdown_tr_spacer">
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr id="dropdown_tr">
                            <td width="20px">&nbsp;</td>
                            <td>
                                <select id="ddl_ExisitingUsers" name="ddl_ExisitingUsers" style="width:100%" class="dropdown_quote_sizing">
                                    <option></option>
                                </select></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txt_ContactFirstName" runat="server" CssClass="Quote-Contact-Textbox-style" placeholder="FIRST NAME" required="required"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txt_ContactLastName" runat="server" CssClass="Quote-Contact-Textbox-style" placeholder="LAST NAME"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txt_EmailAddress" runat="server" CssClass="Quote-Contact-Textbox-style" AutoComplete="Off"  placeholder="EMAIL ADDRESS"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr id="em_tr_errmsg">
                            <td width="20px">&nbsp;</td>
                            <td><span id="em_err_msg" class="Quote_Creation_ErrMsg"></span></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:TextBox ID="txt_PhoneNumber" runat="server" AutoComplete="Off" CssClass="Quote-Contact-Textbox-style" placeholder="PHONE NUMBER"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>

                          <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:TextBox ID="NewShipLine1" runat="server" ClientIDMode="Static" CssClass="Quote-Contact-Textbox-style" placeholder="SHIPPING ADDRESS LINE 1"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                          <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:TextBox ID="NewShipCity" runat="server" ClientIDMode="Static" CssClass="Quote-Contact-Textbox-style" placeholder="SHIPPING ADDRESS CITY"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                          <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <%--<asp:TextBox ID="NewShipState" runat="server" ClientIDMode="Static" CssClass="Quote-Contact-Textbox-style" placeholder="SHIPPING ADDRESS STATE"></asp:TextBox>--%>

                                <asp:DropDownList ID="NewShipState" runat="server" ClientIDMode="Static" CssClass="Quote-Contact-Textbox-style">
                                                               
                                                                <asp:ListItem Text="VIC" Value="VIC"></asp:ListItem>
                                                                <asp:ListItem Text="TAS" Value="TAS"></asp:ListItem>
                                                                <asp:ListItem Text="NT" Value="NT"></asp:ListItem>
                                                                <asp:ListItem Text="ACT" Value="ACT"></asp:ListItem>
                                                                <asp:ListItem Text="SA" Value="SA"></asp:ListItem>
                                                                <asp:ListItem Text="WA" Value="WA"></asp:ListItem>
                                                                <asp:ListItem Text="NSW" Value="NSW"></asp:ListItem>
                                                                <asp:ListItem Text="QLD" Value="QLD"></asp:ListItem>
                                                               </asp:DropDownList>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                         <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                          <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:TextBox ID="NewShipPostcode" runat="server" ClientIDMode="Static" CssClass="Quote-Contact-Textbox-style" placeholder="SHIPPING POSTCODE"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:HiddenField ID="hdn_CustAlreadyExists" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>


                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                <asp:Button ID="btn_CreateQuote" runat="server"  onkeydown = "return (event.keyCode!=13);"
                                    Text="CREATE QUOTE" OnClientClick="return validateInputForComma();" OnClick="btn_CreateQuote_Click" CssClass="Quote_Create_Button" />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">&nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td width="20px">
                                <asp:HiddenField ID="hdn_ExistingCompanyID" runat="server" />
                                <asp:HiddenField ID="hdn_QueryDB" runat="server" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>&nbsp;</td>
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
        </table>
    </div>
</asp:Content>
