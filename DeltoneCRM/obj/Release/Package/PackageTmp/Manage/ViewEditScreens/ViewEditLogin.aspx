<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEditLogin.aspx.cs" Inherits="DeltoneCRM.Manage.ViewEditScreens.ViewEditLogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View/Edit Login</title>
    <script src="../../js/jquery-2.1.3.js"></script>
	<script src="../../js/jquery-ui-1.10.3.custom.js"></script>
    <script src="../../js/jquery.validate.js"></script>
    <link href="../../css/tinytools.toggleswitch.css" rel="stylesheet" />
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

            $('#activechkbox').toggleCheckedState(false);

            $('#activechkbox').prop('disabled', true);

            $('#Save').hide();
            $('#dropdownAccessLevel').hide();
            $('#UserDepartmentDropDownList').hide();
          
            $('#SwitchToEdit').click(function () {
                $('#SwitchToEdit').hide();
                $('#BtnCloseWindow').hide();
                $('#Save').show();
                $('#dropdownAccessLevel').show();
                $('#UserDepartmentDropDownList').show();
                $('#accessLevel').hide();
                $('#UserDepartment').hide();
                $('#activechkbox').prop('disabled', false);
            
                $('#password').prop("readonly", false);
                $('#UserName').prop("readonly", false);
                $('#LastName').prop("readonly", false);
                $('#FirstName').prop("readonly", false);
                $('#EmailAddy').prop("readonly", false);
                $('#conPassWord').prop("readonly", false);

                $('#FirstName').removeClass("txtbox-200-style");
                $('#FirstName').addClass("txtbox-200-style-edit");

                $('#LastName').removeClass("txtbox-200-style");
                $('#LastName').addClass("txtbox-200-style-edit");

                $('#UserName').removeClass("txtbox-200-style");
                $('#UserName').addClass("txtbox-200-style-edit");

                $('#password').removeClass("txtbox-200-style");
                $('#password').addClass("txtbox-200-style-edit");

                $('#conPassWord').removeClass("txtbox-200-style");
                $('#conPassWord').addClass("txtbox-200-style-edit");

                $('#EmailAddy').removeClass("txtbox-200-style");
                $('#EmailAddy').addClass("txtbox-200-style-edit");
            });

            $("#<%=form1.ClientID%>").validate({
                onfocusout: false,
                onkeyup: false,
                rules: {
                    FirstName: {
                        required: true,
                    },
                    LastName: {
                        required: true,
                    },
                    UserName: {
                        required: true,
                    },
                   
                    EmailAddy: {
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
                    var canCheck = "no";
                    if ($("#donotshowstats").is(':checked')) {
                        canCheck = "on";
                    }

                    $.ajax({
                        type: "POST",
                        url: "../Process/Process_EditLogin.aspx",
                        data: {
                            loginid: $('#EditLoginID').val(),
                            firstname: $('#FirstName').val(),
                            lastname: $('#LastName').val(),
                            accesslevel: $('#dropdownAccessLevel option:selected').val(),
                            ActInact: $('#activechkbox').is(':checked'),
                            EmailAddy: $('#EmailAddy').val(),
                            Username: $('#UserName').val(),
                            password: $('#password').val(),
                            department: $('#UserDepartmentDropDownList option:selected').val(),
                            donotshowstats: canCheck
                        },
                        success: function (msg) {
                            alert("User has been edited successfully");
                            window.parent.closeEditWindow();
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

        .auto-style3 {
            width: 650px;
        }

        </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>

  
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">FIRST NAME</td>
                                  <td width="215"><input name="FirstName" type="text" class="txtbox-200-style" id="FirstName"  readonly="true" runat="server"/></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">LAST NAME</td>
                                  <td width="200"><input name="LastName" type="text" class="txtbox-200-style" id="LastName"  readonly="true" runat="server"/></td>
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
                                  <td width="100" class="labels-style">USERNAME&nbsp;&nbsp;</td>
                                  <td width="215"><input name="UserName" type="text" class="txtbox-200-style" id="UserName" readonly="true" runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">PASSWORD</td>
                                  <td width="200"><input name="password" type="text" class="txtbox-200-style" id="password" readonly="true" runat="server" /></td>
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
                                  <td width="100" class="labels-style">ACCESS LEVEL&nbsp;&nbsp;</td>
                                  <td width="215"><input type="text" id="accessLevel" name="accessLevel"  class="txtbox-200-style" runat="server" readonly="true"  /><asp:DropDownList ID="dropdownAccessLevel" class="drpdwn-200-style-edit" runat="server"   ClientIDMode="Static">

                                          <asp:ListItem Text="Administrator" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="Operator" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Administrator-Sale" Value="3"></asp:ListItem>
                                      </asp:DropDownList>
                                      
                                      <select name="accesslevel" id="accesslevel" class="drpdwn-200-style-edit" hidden="hidden">
                                      <option value="">-- SELECT --</option>
  <option value="1">Administrator</option>
  <option value="2">Standard User</option>
                                          <option value="3">Administrator-Sale</option>
</select></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">CONFIRM PWD</td>
                                  <td width="200"><input name="conPassWord" type="text" class="txtbox-200-style" id="conPassWord"  readonly="true"  runat="server"   /></td>

                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        <tr>
                          <td class="height-15px-style"></td>
                        </tr>

                        <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">EMAIL ADDRESS</td>
                                  <td width="215"><input name="EmailAddy" type="text" class="txtbox-200-style" id="EmailAddy" readonly="true" runat="server" /></td>
                                     <td width="20">&nbsp;</td>
                                                        
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="300" class="labels-style">Department</td>
                                         <td width="215"><input type="text" id="UserDepartment" name="UserDepartment"  class="txtbox-200-style" runat="server" readonly="true"  /><asp:DropDownList ID="UserDepartmentDropDownList" class="drpdwn-200-style-edit" runat="server"   ClientIDMode="Static">
                                             <asp:ListItem Text="None" Value=""></asp:ListItem>
                                          <asp:ListItem Text="Managers" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="ReOrders" Value="2"></asp:ListItem>
                                              <asp:ListItem Text="Coldies" Value="3"></asp:ListItem>
                                      </asp:DropDownList>
                                      
                                      <select name="department" id="department" class="drpdwn-200-style-edit" hidden="hidden">
                                       <option value="">-- SELECT --</option>
  <option value="1">Managers</option>
  <option value="2">ReOrders</option>
                                      <option value="3">Coldies</option>
</select></td>
                          </table></td>
                        </tr>

                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>

                            <tr>
                          <td class="height-15px-style"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">Do Not Show ON Stats</td>
                                  <td width="215"><input name="donotshowstats" type="checkbox" class="txtbox-200-style" id="donotshowstats" readonly="true" runat="server" /></td>
                                     <td width="20">&nbsp;</td>
                                                        
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                             <%-- <td width="300" class="labels-style">Department</td>
                                         <td width="215"><input type="text" id="Text2" name="UserDepartment"  class="txtbox-200-style" runat="server" readonly="true"  /><asp:DropDownList ID="DropDownList1" class="drpdwn-200-style-edit" runat="server"   ClientIDMode="Static">
                                             <asp:ListItem Text="None" Value=""></asp:ListItem>
                                          <asp:ListItem Text="Managers" Value="1"></asp:ListItem>
                                           <asp:ListItem Text="ReOrders" Value="2"></asp:ListItem>
                                              <asp:ListItem Text="Coldies" Value="3"></asp:ListItem>
                                      </asp:DropDownList>
                                      
                                      <select name="department" id="department" class="drpdwn-200-style-edit" hidden="hidden">
                                       <option value="">-- SELECT --</option>
  <option value="1">Managers</option>
  <option value="2">ReOrders</option>
                                      <option value="3">Coldies</option>
</select></td>--%>
                          </table></td>
                        </tr>


                                                                                 <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>

                      </table>



                      </td>
                    </tr>
                    <tr>
                        <td class="height-15px-style">&nbsp;</td>
                    </tr>
                    <tr>
                        <td bgcolor="#ffffff">
                            <table align="center" cellpadding="0" cellspacing="0" class="auto-style3">
                                <tr>
                                    <td class="height-15px-style">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                          <input type="button"    runat="server" value="Reset Password" id="btnResetPassword" name="btnResetPassword" /></td>
                                </tr>
                                <tr>
                                    <td class="height-15px-style">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
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
                                              <td width="125px"><input type="button" id="SwitchToEdit" class="btn-red-style" value="EDIT" /></td>
                                              <td width="15px">&nbsp;</td>
                                              <td width="125px"><input type="button" id="BtnCloseWindow" value="OK" class="btn-green-style" onclick="parent.closeEditWindow();" /><input type="submit" id="Save" class="btn-red-style" value="SAVE"  /></td>
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                          </table>
                                    </td>
                      </tr>
                    <tr>
                        <td style="display:none;" class="height-15px-style">
                            <input type="hidden" id="EditLoginID" name="EditLoginID" runat="server" />
                            <input name="createrecord" type="submit" class="clk_button_01" id="createrecord" value="CREATE LOGIN" />
                            <input name="button" type="reset" class="settingsbutton_gray" id="button" style="display:none;" value="CLEAR FORM" />
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