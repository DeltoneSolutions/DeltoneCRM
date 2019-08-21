<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEditcontact.aspx.cs" Inherits="DeltoneCRM.Manage.edit_contact"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View/Edit Item</title>
    <link href="../../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../../js/jquery-2.1.3.js"></script>
	<script src="../../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../../css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="../../css/tinytools.toggleswitch.css" rel="stylesheet" />
    <script src="../../js/jquery.validate.js"></script>
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
    <script src="../../js/tinytools.toggleswitch.js" type="text/javascript"></script>

     <script type="text/javascript">

         $.validator.addMethod("validateComma", function (value, element) {
             var returnvalue = true;

             if (value.indexOf(',') > -1) {

                 returnvalue = false;
             }

             return returnvalue;
         }, "Invalid input please remove comma.");

         function copyAddressToBelow() {
             $('#NewBillLine1').val($('#NewShipLine1').val());
             $('#NewBillLine2').val($('#NewShipLine2').val());
             $('#NewBillCity').val($('#NewShipCity').val());
             $('#NewBillState').val($('#NewShipState').val());
             $('#NewBillPostcode').val($('#NewShipPostcode').val());
         }

         $(document).ready(function () {
             $('#Save').hide();

             $('#activechkbox').toggleSwitch({
                 onLabel: "ACTIVE",
                 offLabel: "INACTIVE",
                 width: "115px",
                 height: "30px"
             });

             $('#activechkbox').toggleCheckedState(false);

             $('#activechkbox').prop('disabled', true);

             $('#SwitchToEdit').click(function () {
                 $('#SwitchToEdit').hide();
                 $('#BtnCloseWindow').hide();
                 $('#Save').show();
                 $('#NewFirstName').prop("readonly", false);
                 $('#NewLastName').prop("readonly", false);
                 $('#NewDefaultAreaCode').prop("readonly", false);
                 $('#NewDefaultNumber').prop("readonly", false);
                 $('#NewMobileNumber').prop("readonly", false);
                 $('#NewEmailAddy').prop("readonly", false);
                 $('#NewShipLine1').prop("readonly", false);
                 $('#NewShipLine2').prop("readonly", false);
                 $('#NewShipCity').prop("readonly", false);
               //  $('#NewShipState').prop("readonly", false);
                 $('#NewShipPostcode').prop("readonly", false);
                 $('#NewBillLine1').prop("readonly", false);
                 $('#NewBillLine2').prop("readonly", false);
                 $('#NewBillCity').prop("readonly", false);
                 //$('#NewBillState').prop("readonly", false);
                 $('#NewBillPostcode').prop("readonly", false);
                 $('#PrimaryContactchkbox').prop("Enabled", true);
                 $('#CopyAddy').show();

                 $('#txtSupplier').hide();
                 $('#DropDownList1').show();
                 $('#activechkbox').prop('disabled', false);

                 $('#NewFirstName').removeClass("txtbox-200-style");
                 $('#NewFirstName').addClass("txtbox-200-style-edit");

                 $('#NewLastName').removeClass("txtbox-200-style");
                 $('#NewLastName').addClass("txtbox-200-style-edit");

                 $('#NewDefaultAreaCode').removeClass("txtbox-200-style");
                 $('#NewDefaultAreaCode').addClass("txtbox-200-style-edit");

                 $('#NewDefaultNumber').removeClass("txtbox-200-style");
                 $('#NewDefaultNumber').addClass("txtbox-200-style-edit");

                 $('#NewMobileNumber').removeClass("txtbox-200-style");
                 $('#NewMobileNumber').addClass("txtbox-200-style-edit");

                 $('#NewEmailAddy').removeClass("txtbox-200-style");
                 $('#NewEmailAddy').addClass("txtbox-200-style-edit");

                 $('#NewShipLine1').removeClass("txtbox-200-style");
                 $('#NewShipLine1').addClass("txtbox-200-style-edit");

                 $('#NewShipLine2').removeClass("txtbox-200-style");
                 $('#NewShipLine2').addClass("txtbox-200-style-edit");

                 $('#NewShipCity').removeClass("txtbox-200-style");
                 $('#NewShipCity').addClass("txtbox-200-style-edit");

                 $('#NewShipState').removeClass("txtbox-200-style");
                 $('#NewShipState').addClass("txtbox-200-style-edit");

                 $('#NewShipPostcode').removeClass("txtbox-200-style");
                 $('#NewShipPostcode').addClass("txtbox-200-style-edit");

                 $('#NewBillLine1').removeClass("txtbox-200-style");
                 $('#NewBillLine1').addClass("txtbox-200-style-edit");

                 $('#NewBillLine2').removeClass("txtbox-200-style");
                 $('#NewBillLine2').addClass("txtbox-200-style-edit");

                 $('#NewBillCity').removeClass("txtbox-200-style");
                 $('#NewBillCity').addClass("txtbox-200-style-edit");

                 $('#NewBillState').removeClass("txtbox-200-style");
                 $('#NewBillState').addClass("txtbox-200-style-edit");

                 $('#NewBillPostcode').removeClass("txtbox-200-style");
                 $('#NewBillPostcode').addClass("txtbox-200-style-edit");


             });

                 //Validate Fields 

                 $("#<%=form1.ClientID%>").validate({
                     onfocusout: false,
                     onkeyup: false,
                     rules: {
                         NewFirstName: {
                             required: true,
                         },
                         NewLastName: {
                             required: true,
                         },
                         NewShipLine1: {
                             required: true,
                             validateComma: true,
                         },
                         NewShipCity: {
                             required: true,
                             validateComma: true,
                         },
                        
                         NewShipPostcode: {
                             required: true,
                             validateComma: true,
                         },
                         NewBillLine1: {
                             required: true,
                         },
                         NewBillCity: {
                             required: true,
                         },
                         
                         NewBillPostcode: {
                             required: true
                         },

                     },
                     messages: {
                         NewShipLine1: {
                             required: "Please Enter SHIPPING ADDRESS LINE 1",
                             validateComma: "SHIPPING ADDRESS LINE 1 Must not contain comma"
                         },
                         NewShipCity: {
                             required: "Please Enter SHIPPING ADDRESS CITY",
                             validateComma: "SHIPPING ADDRESS CITY Must not contain comma"
                         },
                        
                         NewShipPostcode: {
                             required: "Please Enter SHIPPING POSTCODE",
                             validateComma: "SHIPPING POSTCODE Must not contain comma"
                         },
                     },
                     highlight: function (element) {
                             $(element).closest("input")
                             .addClass("textbox_001_err")
                             .removeClass("txtbox-200-style");
                     },
                     unhighlight: function (element) {
                         $(element).closest("input")
                         .removeClass("txtbox-200-style")
                         .addClass("textbox_001");
                     },
                     errorPlacement: function (error, element) {

                         error.insertAfter(element);
                     },
                     success: function (label) {
                         //alert('Triggered');

                     },
                     submitHandler: function (form) {
                         
                         $.ajax({
                             type: "POST",
                             url: "../Process/Process_EditContact.aspx",
                             data: {
                                 ContactID: $('#NewContactID').val(),
                                 NewFirstName: $('#NewFirstName').val(),
                                 NewLastName: $('#NewLastName').val(),
                                 NewDefaultAreaCode: $('#NewDefaultAreaCode').val(),
                                 NewDefaultNumber: $('#NewDefaultNumber').val(),
                                 NewMobileNumber: $('#NewMobileNumber').val(),
                                 NewEmailAddy: $('#NewEmailAddy').val(),
                                 NewShipLine1: $('#NewShipLine1').val(),
                                 NewShipLine2: $('#NewShipLine2').val(),
                                 NewShipCity: $('#NewShipCity').val(),
                                 NewShipState: $('#NewShipState').val(),
                                 NewShipPostcode: $('#NewShipPostcode').val(),
                                 NewBillLine1: $('#NewBillLine1').val(),
                                 NewBillLine2: $('#NewBillLine2').val(),
                                 NewBillCity: $('#NewBillCity').val(),
                                 NewBillState: $('#NewBillState').val(),
                                 NewBillPostcode: $('#NewBillPostcode').val(),
                                 PrimaryContact: $('#PrimaryContactchkbox').is(':checked'),
                                 ActInact: $('#activechkbox').is(':checked'),
                             },
                             success: function (msg) {
                                 alert('This contact has been successfully edited');
                                 window.parent.reloadContactiFrame($('#NewContactID').val());
                                 //window.parent.location.reload(false);
                             },
                             error: function (xhr, err) {
                                 alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                                 alert("responseText: " + xhr.responseText);
                             },
                         });
                     },
                 });

             $('#DropDownList1').change(function () {
                 $('#hdnSupplier').val($(this).val());
                 $('#hdnEditSupplier').val($(this).val());
                 $('#hdnEditSupplier').val($("#DropDownList1 option:selected").val());
             });


         });

    </script>

    <style type="text/css">
        body {
            margin-top: 0px;
            background-color: #eeeeee;
        }
        .tbl-bg {
            background-color: #eeeeee;
        }
        .top-heading {
            height: 30px;
            background-color: #CCCCCC;
            border-bottom-color: #ffffff;
            border-bottom-style: solid;
            border-bottom-width: 2px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 16px;
            color: #ffffff;
            font-weight: 400;
            letter-spacing: -1px;
        }
        .labels-style {
            width: 93px;
            height: 30px;
            color: #666666;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            letter-spacing: -1px;
            border-top-color: #666666;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #666666;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #666666;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #cccccc;
            padding-left: 5px;
        }
        .txtbox-200-style {
            width: 208px;
            height: 30px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #666666;
            letter-spacing: -1px;
            border-top-color: #999999;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-color: #999999;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: #666666;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #999999;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #eeeeee;
            padding-left: 5px;
        }
        .txtbox-200-style-edit {
            width: 208px;
            height: 30px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #666666;
            letter-spacing: -1px;
            border-top-color: #999999;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-color: #999999;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: #666666;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #999999;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #ffffff;
            padding-left: 5px;
        }
        .drpdwn-200-style-edit {
            width: 215px;
            height: 34px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #666666;
            letter-spacing: -1px;
            border-top-color: #999999;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-color: #999999;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-left-color: #666666;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #999999;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #ffffff;
            padding-left: 1px;
        }
        .height-15px-style {
            height: 15px;
            font-size: 5px;
        }
        .btn-green-style {
            width: 125px;
            height: 30px;
            color: #ffffff;
            text-align: center;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            border-top-color: #92c053;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #92c053;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #92c053;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-right-color: #92c053;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #92c053;
        }
        .btn-red-style {
            width: 125px;
            height: 30px;
            color: #ffffff;
            text-align: center;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            border-top-color: #f26d4e;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #f26d4e;
            border-left-style: solid;
            border-left-width: 1px;
            border-bottom-color: #f26d4e;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            border-right-color: #f26d4e;
            border-right-style: solid;
            border-right-width: 1px;
            background-color: #f26d4e;
        }
        .btn-red-style:hover {
            background-color: #fc3b36;
            cursor: pointer;
        }
        .btn-green-style:hover {
            background-color: #6ebb04;
            cursor: pointer;
        }
        .auto-style1 {
            width: 680px;
        }

        .auto-style2 {
            width: 265px;
        }
         .urgDropCss{
                width: 150px;
    height: 30px;
    margin-bottom: -1px;
    font-weight: 400;
    letter-spacing: -0.05em;
        }
    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>

</head>
<body>
    <form id="form1" runat="server">
    <div>
          <div id="EditScreen" title ="EDIT ITEM INFORMATION">

             <table width="680" border="0" align="center" cellpadding="0" cellspacing="0" class="tbl-bg">

                <tr>
                    <td class="height-15px-style">&nbsp</td>
                </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">FIRST NAME&nbsp;&nbsp;</td>
                                  <td width="215"><input name="NewFirstName" type="text" class="txtbox-200-style" id="NewFirstName"  readonly="true" runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">LAST NAME</td>
                                  <td width="200"><input name="NewLastName" type="text" class="txtbox-200-style" id="NewLastName"   readonly="true" runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp</td>
                        </tr>
                        <tr>
                          <td height="15"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DEFAULT AREA CODE&nbsp;&nbsp;</td>
                                  <td width="215"><input name="NewDefaultAreaCode" type="text" class="txtbox-200-style" id="NewDefaultAreaCode"  readonly="true" runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DEFAULT NUMBER</td>
                                  <td width="200"><input name="NewDefaultNumber" type="text" class="txtbox-200-style" id="NewDefaultNumber"   readonly="true" runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">MOBILE NUMBER</td>
                                        <td width="215"><input name="NewMobileNumber" type="text" class="txtbox-200-style" id="NewMobileNumber"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">EMAIL ADDRESS</td>
                                        <td width="215"><input name="NewEmailAddy" type="text" class="txtbox-200-style" id="NewEmailAddy"  readonly="true"  runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">SHIPPING ADDRESS LINE 1</td>
                                        <td width="215"><input name="NewShipLine1" type="text" class="txtbox-200-style" id="NewShipLine1"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr hidden="hidden">
                                        <td width="100" class="labels-style">SHIPPING ADDRESS LINE 2</td>
                                        <td width="215"><input name="NewShipLine2" type="text" class="txtbox-200-style" id="NewShipLine2"  readonly="true"  runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">SHIPPING CITY</td>
                                        <td width="215"><input name="NewShipCity" type="text" class="txtbox-200-style" id="NewShipCity"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">SHIPPING STATE</td>
                                        <td width="215">
                                            <%--<input name="NewShipState" type="text" class="txtbox-200-style" id="NewShipState"  readonly="true"  runat="server" />--%>
                                            

                                              <asp:DropDownList ID="NewShipState" runat="server" ClientIDMode="Static" CssClass="urgDropCss">
                        <asp:ListItem Text="VIC" Value="VIC" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="QLD" Text="QLD" ></asp:ListItem>
                                                   <asp:ListItem Value="NSW" Text="NSW" ></asp:ListItem>
                        <asp:ListItem Value="ACT" Text="ACT"></asp:ListItem>
                                                   <asp:ListItem Value="TAS" Text="TAS"></asp:ListItem>
                                                   <asp:ListItem Value="WA" Text="WA"></asp:ListItem>
                                                   <asp:ListItem Value="NT" Text="NT"></asp:ListItem>
                                                  <asp:ListItem Value="SA" Text="SA"></asp:ListItem>
                    </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>

                              </td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">SHIPPING POSTCODE</td>
                                        <td width="215"><input name="NewShipPostcode" type="text" class="txtbox-200-style" id="NewShipPostcode"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><input name='CopyAddy' type='button' id='CopyAddy' value='COPY ADDRESS TO BELOW' hidden="hidden" onclick="copyAddressToBelow();"/></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">BILLING ADDRESS LINE 1</td>
                                        <td width="215"><input name="NewBillLine1" type="text" class="txtbox-200-style" id="NewBillLine1"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr hidden="hidden">
                                        <td width="100" class="labels-style">BILLING ADDRESS LINE 2</td>
                                        <td width="215"><input name="NewBillLine2" type="text" class="txtbox-200-style" id="NewBillLine2"  readonly="true"  runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">BILLING CITY</td>
                                        <td width="215"><input name="NewBillCity" type="text" class="txtbox-200-style" id="NewBillCity"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">BILLING STATE</td>
                                        <td width="215">
                                           
                                             <asp:DropDownList ID="NewBillState" runat="server" ClientIDMode="Static" CssClass="urgDropCss">
                         <asp:ListItem Text="VIC" Value="VIC" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="QLD" Text="QLD" ></asp:ListItem>
                                                   <asp:ListItem Value="NSW" Text="NSW" ></asp:ListItem>
                        <asp:ListItem Value="ACT" Text="ACT"></asp:ListItem>
                                                   <asp:ListItem Value="TAS" Text="TAS"></asp:ListItem>
                                                   <asp:ListItem Value="WA" Text="WA"></asp:ListItem>
                                                   <asp:ListItem Value="NT" Text="NT"></asp:ListItem>
                                                  <asp:ListItem Value="SA" Text="SA"></asp:ListItem>
                    </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>

                              </td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">BILLING&nbsp; POSTCODE</td>
                                        <td width="215"><input name="NewBillPostcode" type="text" class="txtbox-200-style" id="NewBillPostcode"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                  &nbsp;</td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">PRIMARY CONTACT</td>
                                        <td width="215">
                                            <asp:CheckBox ID="PrimaryContactchkbox" Enabled="false" runat="server" />
                                        </td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                  <input name="NewContactID" type="text" class="txtbox-200-style" id="NewContactID" hidden="hidden"  readonly="true" runat="server" />
                                  <input name="OldCompanyID" type="text" class="txtbox-200-style" id="OldCompanyID" hidden="hidden"  readonly="true" runat="server" />
                                </td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">

                            <table width="650" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="300"></td>
                                    <td width="20"><input type="hidden" name="hdnSupplier" id="hdnSupplier"  runat="server" /></td>
                                    <td><input type="hidden" name="hdnEditSupplier" id="hdnEditSupplier"  runat="server" /></td>
                                    <td><input type="hidden" name="hdnEditItemID" id="hdnEditItemID" runat="server" /></td>
                                    <td width="300">&nbsp;</td>
                                </tr>
                            </table>


                          </td>
                        </tr>
                       
                      </table></td>
                    </tr>

                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                      </tr>
                    <tr>
                      <td>

                          

                          <table cellpadding="0" cellspacing="0" class="auto-style1">
                              <tr>
                                  <td width="380px"><input type="checkbox" id="activechkbox" runat="server"/></td>
                                  <td width="270px">
                                      <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                          <tr>
                                              <td width="125px"><input type="button" id="SwitchToEdit" class="btn-red-style" value="EDIT" /></td>
                                              <td width="15px">&nbsp;</td>
                                              <td width="125px">
                                                  <input type="button" id="BtnCloseWindow" class="btn-green-style" value="OK" onclick="parent.closeEditWindow();" />
                                                  <input type="submit" id="Save" class="btn-red-style" value="SAVE"  />
                                              </td>
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                          </table>

                          

                      </td>
                    </tr>
                    <tr>
                      <td><table width="680" border="0" cellspacing="0" cellpadding="0">
                        <tr style="display:none;">
                          <td width="430">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td width="250" style="display:none;"><input name="createrecord" type="submit" class="clk_button_01" id="createrecord" value="UPDATE ITEM" /><input name="button" type="reset" class="settingsbutton_gray" id="button" style="display:none;" value="CLEAR FORM" /></td>
                          </tr>

                        </table></td>
                    </tr>
                    
                    </table>

          </div>
    </div>
    </form>
</body>
</html>
