<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addcontact.aspx.cs" Inherits="DeltoneCRM.Manage.addcontact" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../js/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.validate.js" type="text/javascript"></script>

    <style type="text/css">
        body {
            margin-top: 0px;
            margin-bottom: 0px;
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
        .height-10px-style {
            height: 10px;
            font-size: 5px;
        }
        .height-5px-style {
            height: 5px;
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

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'/>

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

             $('#YesNoCreateOrder').val("");

             $('#CreateOrder').click(function () {
                 $('#YesNoCreateOrder').val("YES");
                 $('#form1').submit();
             });


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
                     //NewBillLine1: {
                     //required: true,
                     //},
                     //NewBillCity: {
                     //required: true,
                     //},
                     //NewBillState: {
                     //required: true,
                     //},
                     //NewBillPostcode: {
                     // required: true
                     //},
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
                     .removeClass("textbox_001");
                 },
                 unhighlight: function (element) {
                     $(element).closest("input")
                     .removeClass("textbox_001_err")
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
                         url: "Process/Process_AddContact.aspx",
                         data: {
                             CompID: $('#CompID').val(),
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
                         },
                         success: function (msg) {
                             //alert(msg);
                             var InfoArray = msg.split(":");
                             if ($('#ReLocation').val() == "DASH") {
                                 if ($('#YesNoCreateOrder').val() == "YES") {
                                     window.parent.CreateOrderSub(InfoArray[1], InfoArray[0]);
                                     window.parent.reloadAddContactIFrame($('#CompID').val());
                                 }
                                 else {
                                     alert('Dash No Section');
                                     window.parent.reloadAddContactIFrame($('#CompID').val());
                                 }
                                 
                             }
                             else {
                                 if ($('#YesNoCreateOrder').val() == "YES") {
                                     window.parent.closeCreateContactAndOpenOrder(InfoArray[1], InfoArray[0]);
                                 }
                                 else {
                                     window.parent.closeCreateContact();
                                 }
                             }
                             //window.parent.reloadContactiFrame($('#NewContactID').val());
                             //window.parent.location.reload(false);
                         },
                         error: function (xhr, err) {
                             alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                             alert("responseText: " + xhr.responseText);
                         },
                     });
                 },


             });




         });



      </script>
</head>
<body>
    <form id="form1" runat="server">
    <div >
        <table width="680" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">FIRST NAME&nbsp;</td>
                                  <td width="215"><input name="NewFirstName" type="text" class="txtbox-200-style-edit" id="NewFirstName" runat="server"/></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">LAST NAME</td>
                                  <td width="200"><input name="NewLastName" type="text" class="txtbox-200-style-edit" id="NewLastName" runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-5px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DEFAULT AREA CODE&nbsp;</td>
                                  <td width="215"><input name="NewDefaultAreaCode" type="text" class="txtbox-200-style-edit" id="NewDefaultAreaCode" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DEFAULT NUMBER</td>
                                  <td width="200"><input name="NewDefaultNumber" type="text" class="txtbox-200-style-edit" id="NewDefaultNumber" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-5px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">MOBILE NUMBER</td>
                                  <td width="215"><input name="NewMobileNumber" type="text" class="txtbox-200-style-edit" id="NewMobileNumber" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">EMAIL ADDRESS</td>
                                  <td width="200"><input name="NewEmailAddy" type="text" class="txtbox-200-style-edit" id="NewEmailAddy" runat="server" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                                                  </table></td>
                    </tr>
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
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
                                  <td width="100" class="labels-style">SHIPPING ADDRESS LINE 1</td>
                                  <td width="215"><input name="NewShipLine1" type="text" class="txtbox-200-style-edit" id="NewShipLine1" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr hidden="hidden">
                                  <td width="100" class="labels-style">SHIPPING ADDRESS LINE 2</td>
                                  <td width="200"><input name="NewShipLine2" type="text" class="txtbox-200-style-edit" id="NewShipLine2" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                                                  <tr>
                          <td class="height-5px-style">&nbsp;</td>
                        </tr>
                                                  <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">SHIPPING ADDRESS CITY</td>
                                  <td width="215"><input name="NewShipCity" type="text" class="txtbox-200-style-edit" id="NewShipCity" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">SHIPPING ADDRESS STATE</td>
                                  <td width="200">

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

                                     <%-- <input name="NewShipState" type="text" class="txtbox-200-style-edit" id="NewShipState" />--%>

                                  </td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                                                  <tr>
                          <td class="height-5px-style">&nbsp;</td>
                        </tr>
                                                  <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">SHIPPING POSTCODE</td>
                                  <td width="215"><input name="NewShipPostcode" type="text" class="txtbox-200-style-edit" id="NewShipPostcode" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><input name='CopyAddy' type='button' id='CopyAddy' value='COPY ADDRESS TO BELOW' onclick="copyAddressToBelow();"/></td>
                            </tr>
                          </table></td>
                        </tr>
                                                  <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                                                  </table></td>
                    </tr>
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                      </tr>
                    <tr>
                      <td bgcolor="#ffffff"><table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">BILLING ADDRESS LINE 1</td>
                                  <td width="215"><input name="NewBillLine1" type="text" class="txtbox-200-style-edit" id="NewBillLine1" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr hidden="hidden"> 
                                  <td width="100" class="labels-style">BILLING ADDRESS LINE 2</td>
                                  <td width="200"><input name="NewBillLine2" type="text" class="txtbox-200-style-edit" id="NewBillLine2" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                            <td class="height-5px-style">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">BILLING ADDRESS CITY</td>
                                  <td width="215"><input name="NewBillCity" type="text" class="txtbox-200-style-edit" id="NewBillCity" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">BILLING STATE</td>
                                  <td width="200">
                                   <%--  <input name="NewBillState" type="text" class="txtbox-200-style-edit" id="NewBillState" />--%>

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
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                            <td class="height-5px-style">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">BILLIING POSTCODE</td>
                                  <td width="215"><input name="NewBillPostcode" type="text" class="txtbox-200-style-edit" id="NewBillPostcode" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><input name="ReLocation" type="text" class="txtbox-200-style-edit" id="ReLocation" hidden="hidden"  runat="server"/></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td>&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">PRIMARY CONTACT</td>
                                  <td width="215">
                                      <asp:CheckBox ID="PrimaryContactchkbox" runat="server" />
                                    </td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><input name="CompID" type="text" class="txtbox-200-style-edit" id="CompID" hidden="hidden" runat="server"/></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td class="height-15px-style"><input name="YesNoCreateOrder" type="text" class="txtbox-200-style-edit" id="YesNoCreateOrder" runat="server"/></td>
                        </tr>
                                                  </table></td>
                      </tr>
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                      </tr>
                    <tr>
                      <td style="display:none;"><input name="button" type="reset" class="settingsbutton_gray" id="button" value="CLEAR FORM" /></td>
                      </tr>
                    <tr>
                      <td>
                          <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>
                                      <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                          <tr>
                                              <td width="125px"><input name="createrecord" type="submit" class="btn-green-style" id="createrecord" value="CREATE" /></td>
                                              <td width="15px">&nbsp;</td>
                                              <td width="125px"><input name="CreateOrder" type="button" class="btn-green-style" id="CreateOrder" value="CREATE ORDER" /></td>
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                          </table>
                        </td>
                      </tr>
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                    </tr>
                    
                    </table>
    </div>
    </form>
</body>
</html>