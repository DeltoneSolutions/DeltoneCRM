<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEditSupplier.aspx.cs" Inherits="DeltoneCRM.Manage.ViewEditScreens.ViewEditSupplier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View/Edit Supplier</title>
    <link href="../../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../../js/jquery-2.1.3.js"></script>
	<script src="../../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../../css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="../../css/tinytools.toggleswitch.css" rel="stylesheet" />
    <script src="../../js/jquery.validate.js"></script>
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
    <script src="../../js/tinytools.toggleswitch.js" type="text/javascript"></script>
    <link href="../../css/ManageCSS.css" rel="stylesheet" />

    <script type="text/javascript">
        $(document).ready(function () {

            $('#activechkbox').toggleSwitch({
                onLabel: "ACTIVE",
                offLabel: "INACTIVE",
                width: "115px",
                height: "30px"
            });

            $('#Save').hide();

            $('#activechkbox').toggleCheckedState(false);

            $('#activechkbox').prop('disabled', true);

            $('#SwitchToEdit').click(function () {
                $('#SwitchToEdit').hide();
                $('#BtnCloseWindow').hide();
                $('#Save').show();
                $('#NewItem').prop("readonly", false);
                $('#NewCost').prop("readonly", false);
                $('#suppaddress').prop("readonly", false);
                $('#suppcity').prop("readonly", false);
                $('#supppostcode').prop("readonly", false);
                $('#suppsalesemail').prop("readonly", false);  
                $('#suppreturnemail').prop("readonly", false);
                $('#suppphonenumber').prop("readonly", false);
                $('#suppaccountPhonenumber').prop("readonly", false);
                $('#suppusername').prop("readonly", false);
                $('#supppassword').prop("readonly", false);
                $('#suppnotes').prop("readonly", false);
                $('#contactnamesupp').prop("readonly", false);
                $('#contactnameemail').prop("readonly", false);

                $('#NewItem').removeClass("txtbox-200-style");
                $('#NewItem').addClass("txtbox-200-style-edit");
                
                $('#NewCost').removeClass("txtbox-200-style");
                $('#NewCost').addClass("txtbox-200-style-edit");

                $('#activechkbox').prop('disabled', false);


                $('#suppaddress').removeClass("txtbox-200-style");
                $('#suppaddress').addClass("txtbox-200-style-edit");

                $('#suppcity').removeClass("txtbox-200-style");
                $('#suppcity').addClass("txtbox-200-style-edit");

                $('#supppostcode').removeClass("txtbox-200-style");
                $('#supppostcode').addClass("txtbox-200-style-edit");

                $('#suppsalesemail').removeClass("txtbox-200-style");
                $('#suppsalesemail').addClass("txtbox-200-style-edit");
                
                $('#suppreturnemail').removeClass("txtbox-200-style");
                $('#suppreturnemail').addClass("txtbox-200-style-edit");

                $('#suppphonenumber').removeClass("txtbox-200-style");
                $('#suppphonenumber').addClass("txtbox-200-style-edit");

                $('#suppaccountPhonenumber').removeClass("txtbox-200-style");
                $('#suppaccountPhonenumber').addClass("txtbox-200-style-edit");

                $('#suppusername').removeClass("txtbox-200-style");
                $('#suppusername').addClass("txtbox-200-style-edit");

                $('#supppassword').removeClass("txtbox-200-style");
                $('#supppassword').addClass("txtbox-200-style-edit");

                $('#suppnotes').removeClass("txtbox-200-style");
                $('#suppnotes').addClass("txtbox-200-style-edit");

                $('#contactnamesupp').removeClass("txtbox-200-style");
                $('#contactnamesupp').addClass("txtbox-200-style-edit");

                $('#contactnameemail').removeClass("txtbox-200-style");
                $('#contactnameemail').addClass("txtbox-200-style-edit");


            });

            $("#<%=form1.ClientID%>").validate({
                onfocusout: false,
                onkeyup: false,
                rules: {
                    NewItem: {
                        required: true,
                    },
                    NewCost: {
                        required: true,
                    }
                },
                highlight: function (element) {
                    $(element).closest("input")
                    .addClass("txtbox-200-style-err")
                    .removeClass("txtbox-200-style-edit");
                },
                unhighlight: function (element) {
                    $(element).closest("input")
                    .removeClass("txtbox-200-style-err")
                    .addClass("txtbox-200-style-edit");
                },
                errorPlacement: function (error, element) {

                    return true;
                },
                success: function (label) {


                },
                submitHandler: function (form) {
                    $.ajax({
                        type: "POST",
                        url: "../Process/ProcessEditSupplier.aspx",
                        data: {
                            suppid: $('#hdnEditSupplierID').val(),
                            suppname: $('#NewItem').val(),
                            deliverycost: $('#NewCost').val(),
                            ActInact: $('#activechkbox').is(':checked'),
                            Address: $('#suppaddress').val(),
                            ContactName: $('#contactnamesupp').val(),
                            PhoneNumber: $('#suppphonenumber').val(),
                            City: $('#suppcity').val(),
                            State: $('#suppstate').val(),
                            PostCode: $('#supppostcode').val(),
                            SalesEmail: $('#suppsalesemail').val(),
                            ReturnEmail: $('#suppreturnemail').val(),
                            AccountsPhoneNumber: $('#suppaccountPhonenumber').val(),
                            UserName: $('#suppusername').val(),
                            AccountsEmail: $('#contactnameemail').val(),
                            Notes: $('#suppnotes').val(),
                            Password: $('#supppassword').val(),


                        },
                        success: function (msg) {
                            alert("Supplier has been edited successfully");
                            window.parent.closeEditIframe();
                            window.parent.location.reload(false);
                        },
                        error: function (xhr, err) {
                            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                            alert("responseText: " + xhr.responseText);
                        },
                    });
                }
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
        .txtbox-200-style-err {
            width: 208px;
            height: 30px;
            text-align: left;
            font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            color: #666666;
            letter-spacing: -1px;
            border: 1px solid #f00;
            background-color: #ffffff;
            padding-left: 5px;
            outline:none;
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

    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>






</head>
<body>
    <form id="form1" runat="server">
    <div>

         <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
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
                                  <td width="100" class="labels-style">SUPPLIER NAME&nbsp;&nbsp;&nbsp;</td>
                                  <td width="215"><input name="NewItem" type="text" class="txtbox-200-style" id="NewItem"  readonly="true"  runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">DELIVERY COST $</td>
                                  <td width="215"><input name="NewCost" type="text" class="txtbox-200-style" id="NewCost" readonly="true"  runat="server" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>

                            <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                 <td width="100" class="labels-style">ADDRESS</td>
                                  <td width="215"><input name="suppaddress" type="text" class="txtbox-200-style-edit" id="suppaddress" readonly="true"  runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="100" class="labels-style">CITY</td>
                                  <td width="200"><input name="suppcity" type="text" class="txtbox-200-style-edit" id="suppcity" readonly="true"  runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                             <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                 <td width="100" class="labels-style">STATE</td>
                                    <td width="215"> <select id="suppstate" style="width:220px;height:30px;" runat="server">
 <option value="VIC" >   VIC  </option>
 <option value="TAS" >  TAS         </option>
 <option value="NT" >   NT          </option>
 <option value="ACT" >    ACT     </option> 
                       <%-- <option value="#008000" style="background-color:#008000"> Quote Follow Up </option>--%>

                        <option value="SA" > SA   </option>
                     
                         <option value="NSW" > NSW   </option>
                         <option value="QLD" > QLD   </option>
                        <option value="WA" > WA   </option>
</select></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="100" class="labels-style">POSTCODE</td>
                                  <td width="200"><input name="supppostcode" type="text" class="txtbox-200-style-edit" id="supppostcode" readonly="true"  runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                          
                            <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                 <td width="100" class="labels-style">SALES EMAIL</td>
                                  <td width="215"><input name="suppsalesemail" type="text" class="txtbox-200-style-edit" id="suppsalesemail" readonly="true"  runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="100" class="labels-style">RETURN EMAIL</td>
                                  <td width="200"><input name="suppreturnemail" type="text" class="txtbox-200-style-edit" id="suppreturnemail" readonly="true"  runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                            <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                 <td width="100" class="labels-style">PHONE NUMBER</td>
                                  <td width="215"><input name="suppphonenumber" type="text" class="txtbox-200-style-edit" id="suppphonenumber" readonly="true"  runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="100" class="labels-style">ACCOUNT PHONE NUMBER</td>
                                  <td width="200"><input name="suppaccountPhonenumber" type="text" class="txtbox-200-style-edit" id="suppaccountPhonenumber" readonly="true"  runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                           <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                 <td width="100" class="labels-style">USERNAME</td>
                                  <td width="215"><input name="suppusername" type="text" class="txtbox-200-style-edit" id="suppusername" readonly="true"  runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="100" class="labels-style">PASSWORD</td>
                                  <td width="200"><input name="supppassword" type="text" class="txtbox-200-style-edit" id="supppassword" readonly="true"  runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                           <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                 <td width="100" class="labels-style">NOTES</td>
                                  <td width="200"><textarea id="suppnotes" cols="60" class="txtbox-200-style-edit" rows="24" readonly="true"  runat="server"></textarea> </td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                     <td width="100" class="labels-style">CONTACT NAME</td>
                                  <td width="200"><input name="contactnamesupp" type="text" class="txtbox-200-style-edit" id="contactnamesupp" readonly="true"  runat="server"/></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                           <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">ACCOUNTS EMAIL</td>
                                  <td width="200"><input name="contactnameemail" type="text" class="txtbox-200-style-edit" id="contactnameemail" readonly="true"  runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                
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
                        <td>
                            <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                                <tr>
                                    <td><input type="checkbox" id="activechkbox" runat="server"/></td>
                                    <td>
                                        <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                            <tr>
                                                <td width="125px"><input type="button" class="btn-red-style"  id="SwitchToEdit" value="EDIT" /></td>
                                                <td width="15px">&nbsp;</td>
                                                <td width="125px"><input type="button" id="BtnCloseWindow" class="btn-green-style" value="OK" onclick="parent.closeEditWindow();" /><input type="submit" id="Save" class="btn-red-style" value="SAVE"  /></td>
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
                      <tr>
                            <td style="display:none;"><input name="createrecord" id="createrecord" type="button" class="clk_button_01"  value="CREATE SUPPLIER" /><input type="hidden" name="hdnEditSupplierID" id="hdnEditSupplierID" runat="server" /><input name="button" type="reset" class="settingsbutton_gray" id="button" style="display:none;" value="CLEAR FORM" /></td>
                      </tr>
                    
                    </table>
    </div>
    </form>
</body>
</html>
