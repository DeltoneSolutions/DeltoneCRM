<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_contact.aspx.cs" Inherits="DeltoneCRM.Manage.edit_contact"  %>

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

         

         $(document).ready(function () {
             $('#Save').hide();
             $('#DropDownList1').hide();

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
                 $('#NewItem').prop("readonly", false);
                 $('#NewItemCode').prop("readonly", false);
                 $('#NewOEMCode').prop("readonly", false);
                 $('#NewDescription').prop("readonly", false);
                 $('#NewCOG').prop("readonly", false);
                 $('#NewResellPrice').prop("readonly", false);
                 $('#txtSupplier').hide();
                 $('#DropDownList1').show();
                 $('#activechkbox').prop('disabled', false);

                 $('#NewDescription').removeClass("txtbox-200-style");
                 $('#NewDescription').addClass("txtbox-200-style-edit");

                 $('#NewItemCode').removeClass("txtbox-200-style");
                 $('#NewItemCode').addClass("txtbox-200-style-edit");

                 $('#NewOEMCode').removeClass("txtbox-200-style");
                 $('#NewOEMCode').addClass("txtbox-200-style-edit");

                 $('#NewCOG').removeClass("txtbox-200-style");
                 $('#NewCOG').addClass("txtbox-200-style-edit");

                 $('#NewResellPrice').removeClass("txtbox-200-style");
                 $('#NewResellPrice').addClass("txtbox-200-style-edit");
             });

             $('#Save').click(function () {
                 $('#SwitchToEdit').show();
                 $('#BtnCloseWindow').show();
                 $('#Save').hide();
                 $('#NewItem').prop("readonly", true);
                 $('#NewItemCode').prop("readonly", true);
                 $('#NewOEMCode').prop("readonly", true);
                 $('#NewDescription').prop("readonly", true);
                 $('#NewCOG').prop("readonly", true);
                 $('#NewResellPrice').prop("readonly", true);
                 $('#txtSupplier').show();
                 $('#DropDownList1').hide();

                 $('#NewDescription').removeClass("txtbox-200-style-edit");
                 $('#NewDescription').addClass("txtbox-200-style");

                 $('#NewItemCode').removeClass("txtbox-200-style-edit");
                 $('#NewItemCode').addClass("txtbox-200-style");

                 $('#NewOEMCode').removeClass("txtbox-200-style-edit");
                 $('#NewOEMCode').addClass("txtbox-200-style");

                 $('#NewCOG').removeClass("txtbox-200-style-edit");
                 $('#NewCOG').addClass("txtbox-200-style");

                 $('#NewResellPrice').removeClass("txtbox-200-style-edit");
                 $('#NewResellPrice').addClass("txtbox-200-style");


                 //Validate Fields 

                 if (($('#NewItemCode').val() != '') && ($('#NewOEMCode').val() != '') && ($('#NewDescription').val() != '') && ($('#NewCOG').val() != '') && !isNaN($('#NewCOG').val()) && ($('#NewResellPrice').val() != '') && (!isNaN($('#NewResellPrice').val()))) {


                     $.ajax({
                         type: "POST",
                         url: "../Process/ProcessEditItem.aspx",
                         data: {
                             ItemID: $('#hdnEditItemID').val(),
                             SupplierItemCode: $('#NewItemCode').val(),
                             Description: $('#NewDescription').val(),
                             COG: $('#NewCOG').val(),
                             OEM: $('#NewOEMCode').val(),
                             ManagedUnitPrice: $('#NewResellPrice').val(),
                             SupplierID: $("#DropDownList1 option:selected").val(),
                             ActInact: $('#activechkbox').is(':checked')
                         },
                         success: function (msg) {
                             //alert(msg);
                             window.parent.closeIframe();
                             window.parent.location.reload(false);
                         },
                         error: function (xhr, err) {
                             alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                             alert("responseText: " + xhr.responseText);
                         },
                     });

                 }
                 else {
                     $('#SwitchToEdit').hide();
                     $('#BtnCloseWindow').hide();
                     $('#Save').show();
                     $('#NewItem').prop("readonly", false);
                     $('#NewItemCode').prop("readonly", false);
                     $('#NewOEMCode').prop("readonly", false);
                     $('#NewDescription').prop("readonly", false);
                     $('#NewCOG').prop("readonly", false);
                     $('#NewResellPrice').prop("readonly", false);
                     $('#txtSupplier').hide();
                     $('#DropDownList1').show();
                 }

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
                                  <td width="215"><input name="NewItemCode0" type="text" class="txtbox-200-style" id="NewItemCode0"  readonly="true" runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">LAST NAME</td>
                                  <td width="200"><input name="NewOEMCode0" type="text" class="txtbox-200-style" id="NewOEMCode0"   readonly="true" runat="server"/></td>
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
                                  <td width="215"><input name="NewItemCode" type="text" class="txtbox-200-style" id="NewItemCode"  readonly="true" runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DEFAULT NUMBER</td>
                                  <td width="200"><input name="NewOEMCode" type="text" class="txtbox-200-style" id="NewOEMCode"   readonly="true" runat="server"/></td>
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
                                        <td width="215"><input name="NewCOG" type="text" class="txtbox-200-style" id="NewCOG"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">EMAIL ADDRESS</td>
                                        <td width="215"><input name="NewResellPrice" type="text" class="txtbox-200-style" id="NewResellPrice"  readonly="true"  runat="server" /></td>
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
                                        <td width="215"><input name="NewCOG0" type="text" class="txtbox-200-style" id="NewCOG0"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr hidden="hidden">
                                        <td width="100" class="labels-style">SHIPPING ADDRESS LINE 2</td>
                                        <td width="215"><input name="NewResellPrice0" type="text" class="txtbox-200-style" id="NewResellPrice0"  readonly="true"  runat="server" /></td>
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
                                        <td width="215"><input name="NewCOG1" type="text" class="txtbox-200-style" id="NewCOG1"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">SHIPPING STATE</td>
                                        <td width="215"><input name="NewResellPrice1" type="text" class="txtbox-200-style" id="NewResellPrice1"  readonly="true"  runat="server" /></td>
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
                                        <td width="215"><input name="NewCOG2" type="text" class="txtbox-200-style" id="NewCOG2"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315"></td>
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
                                        <td width="215"><input name="NewCOG3" type="text" class="txtbox-200-style" id="NewCOG3"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">BILLING ADDRESS LINE 2</td>
                                        <td width="215"><input name="NewResellPrice2" type="text" class="txtbox-200-style" id="NewResellPrice2"  readonly="true"  runat="server" /></td>
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
                                        <td width="215"><input name="NewCOG4" type="text" class="txtbox-200-style" id="NewCOG4"  readonly="true" runat="server" /></td>
                                    </tr>
                                </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315">

                                <table width="315" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="100" class="labels-style">BILLING STATE</td>
                                        <td width="215"><input name="NewResellPrice3" type="text" class="txtbox-200-style" id="NewResellPrice3"  readonly="true"  runat="server" /></td>
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
                                        <td width="215"><input name="NewCOG5" type="text" class="txtbox-200-style" id="NewCOG5"  readonly="true" runat="server" /></td>
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
                                            <asp:CheckBox ID="PrimaryContactchkbox" runat="server" />
                                        </td>
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
                                                  <input type="button" id="Save" class="btn-red-style" value="SAVE"  />
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
