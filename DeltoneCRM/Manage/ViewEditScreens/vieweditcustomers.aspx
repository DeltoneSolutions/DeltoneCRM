<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="vieweditcustomers.aspx.cs" Inherits="DeltoneCRM.Manage.ViewEditScreens.vieweditcustomers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../../js/jquery-2.1.3.js"></script>
	<script src="../../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../../css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="../../js/jquery.validate.js"></script>
    

    <script type="text/javascript">
        var canCall = "";
        canCall = '<%= superAccount %>';

        $(document).ready(function () {
            $('#Save').hide();
            $('#AccountOwnerList').hide();

            $('#SwitchToEdit').click(function () {
                $('#SwitchToEdit').hide();
                $('#BtnCloseWindow').hide();
                $('#Save').show();
                $('#CompanyName').prop("readonly", false);
                $('#CompanyWebsite').prop("readonly", false);
                $('#OwnershipPeriod').prop("readonly", false);
                $('#AccountOwner').hide();
                $('#AccountOwnerList').show();
            });

            $('#BtnCloseWindow').click(function () {
                parent.closeEditWindow($('#hidden_company_id').val());
            });


            function TestValidate()
            {
                $('#<%=form1.ClientID%>').validate({

                    onfocusout: false,
                    onkeyup: false,
                    rules: {
                        CompanyName: {
                            required: true
                        }
                    },
                    highlight: function (element) {
                        $(element).closest("input")
                        .addClass("textbox_001_err")
                        .removeClass("txtbox-200-style");
                    },
                    unhighlight: function (element) {
                        $(element).closest("input")
                        .removeClass("textbox_001_err")
                        .addClass("txtbox-200-style");
                    },
                    errorPlacement: function (error, element) {

                        return true;
                    },
                    success: function (label) {
                        alert('Triggered');

                    },
                    submitHandler: function (form) {
                        alert('Entered Submit');
                        $.ajax({
                            type: "POST",
                            url: "../Process/ProcessEditCustomer.aspx",
                            data: {
                                CompanyID: $('#hidden_company_id').val(),
                                AccountOwner: $('#AccountOwnerList option:selected').val(),
                                WebSite: $('#CompanyWebsite').val(),
                                CompanyName: $('#CompanyName').val()
                            },
                            success: function (msg) {
                                alert(msg);
                                if ($('#ReLocation').val() == "DASH") {
                                    // window.parent.reloadCompanyIFrame($('#hidden_company_id').val());
                                } else {

                                    // window.parent.closeIframe();
                                    //window.parent.location.reload(false);
                                }
                            },
                            error: function (xhr, err) {
                                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                                alert("responseText: " + xhr.responseText);
                            },
                        });
                    }
                });
            }



            $('#Save').click(function () {
                $('#SwitchToEdit').show();
                $('#BtnCloseWindow').show();
                $('#Save').hide();
                $('#CompanyName').prop("readonly", true);
                $('#CompanyWebsite').prop("readonly", true);
                $('#AccountOwner').show();
                $('#AccountOwnerList').hide();

                //$("#<%=form1.ClientID%>").validate();

                //if (canCall) {
                //   // alert("This account is super");
                //    return;
                //}
               
                if (Validate() == '')
                {
                    $.ajax({
                        type: "POST",
                        url: "../Process/ProcessEditCustomer.aspx",
                        data: {
                            CompanyID: $('#hidden_company_id').val(),
                            AccountOwner: $('#AccountOwnerList option:selected').val(),
                            WebSite: $('#CompanyWebsite').val(),
                            CompanyName: $('#CompanyName').val(),
                            OwnershipPeriod: $('#OwnershipPeriod').val(),
                        },
                        success: function (msg) {
                            //alert(msg);
                            if ($('#ReLocation').val() == "DASH") {
                                window.parent.reloadCompanyIFrame($('#hidden_company_id').val());
                            } else {

                                 window.parent.closeIframe();
                                window.parent.location.reload(false);
                            }
                        },
                        error: function (xhr, err) {
                            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                            alert("responseText: " + xhr.responseText);
                        },
                    });
                    
                }
                else
                {
                    alert(Validate());
                }


                 
            });

            //this Method Validate the Company Name 
            function Validate()
            {
                var Error = '';
                var validatemsg = '';

                if ($('#<%=CompanyName.ClientID%>').val() == '')
                {
                    Error ="COMPANY NAME MUST CONTAIN A  VALUE";
                }
                
                if ($('#<%=CompanyName.ClientID%>').val())
                {
                    $.ajax({
                        type: "POST",
                        url: "../Process/CheckCustomerExsists.aspx",
                        async: false,
                        data: {

                            ID: $('#<%=hidden_company_id.ClientID%>').val(),
                            CompanyName: $('#<%=CompanyName.ClientID%>').val()
                        },
                        success: function (msg) {
                           
                            validatemsg = msg;
                            //alert('Message=' + msg);
                        },
                        error: function (xhr, err) {
                            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                            alert("responseText: " + xhr.responseText);
                        },
                    });
                }

                if (parseInt(validatemsg) > 1) //Compnay Duplicated
                {
                    Error = Error + "COMPANY NAME ALREADY EXSISTS";
                }

                return Error;

            }
            $('#AccountOwnerList').change(function () {
                $('#hidden_owner_id').val($(this).val());
                $('#AccountOwner').val($("#AccountOwnerList option:selected").text());
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
            width: 650px;
        }

        .auto-style2 {
            width: 265px;
        }

    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>


</head>
<body>
    <form id="form1" runat="server" method="post" name="form1">
    <div>
    <div id="EditScreen" title ="EDIT COMPANY INFORMATION">
    <table width="680px" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td class="height-15px-style">&nbsp</td>
        </tr>
        <tr>
            <td align="center" bgcolor="#ffffff">
                <table width="650" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td class="height-15px-style">&nbsp;</td>
                    </tr>
                    
                    <tr>
                        <td >
                            
                            
                                <table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">COMPANY NAME</td>
                                  <td width="215"><input name="CompanyName" type="text" class="txtbox-200-style" id="CompanyName" value="" readonly runat="server" required="required"/></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table border="0" cellspacing="0" cellpadding="0" style="width: 315px">
                                <tr>
                                  <td width="100" class="labels-style">WEBSITE&nbsp;&nbsp;&nbsp;</td>
                                  <td width="215"><input name="CompanyWebsite" type="text" class="txtbox-200-style" id="CompanyWebsite" value="" readonly runat="server" required="required" /></td>
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
                                  <td width="100" class="labels-style">ACCOUNT OWNER&nbsp;&nbsp;&nbsp;</td>
                                  <td width="215"><input name="AccountOwner" type="text" class="txtbox-200-style" id="AccountOwner" value="" readonly runat="server" /><asp:DropDownList ID="AccountOwnerList" runat="server" ClientIDMode="Static" CssClass="drpdwn-200-style-edit">
                                      </asp:DropDownList>
                                    </td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="300" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">OWNER PERIOD</td>
                                  <td width="215">&nbsp;<br />
                                      <input name="OwnershipPeriod" type="date" class="txtbox-200-style" id="OwnershipPeriod" value="" readonly runat="server" /></td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr hidden="hidden">
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr hidden="hidden">
                          <td class="height-15px-style"><input name="hidden_company_id" type="text" class="textbox_001" id="hidden_company_id" value="" hidden="hidden" runat="server"  /><input name="hidden_owner_id" type="text" class="textbox_001" id="hidden_owner_id" value="" hidden="hidden" runat="server"  /></td>
                        </tr>
                        <tr>
                          <td class="height-15px-style">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><input name="ReLocation" type="text" class="textbox_001" id="ReLocation" readonly hidden="hidden"  runat="server" /></td>
                        </tr>
                                    </table>
                            
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td class="15">&nbsp</td>
        </tr>
        <tr>
            <td>
                          <table align="right" cellpadding="0" cellspacing="0" class="auto-style1">
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>
                                      <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                          <tr>
                                              <td width="125px"><input type="button" class="btn-red-style" id="SwitchToEdit" value="EDIT" /></td>
                                              <td width="15px">&nbsp;</td>
                                              <td width="125px"><input type="button" class="btn-green-style" id="BtnCloseWindow" value="OK" /><input class="btn-red-style" type="button" id="Save" value="SAVE"  /></td>
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                          </table>
                                        </td>
        </tr>
        <tr>
            <td class="height-15px-style">&nbsp</td>
        </tr>
    </table>
    </div>
    </div>
    </form>
    <script type="text/javascript">
        $('#form1').validate({
            rules: {
                CompanyName: {
                    required: true
                }
            },
            submitHandler: function(form) {
                //alert('Submitting');

               

            }
        });
    </script>
</body>
</html>
