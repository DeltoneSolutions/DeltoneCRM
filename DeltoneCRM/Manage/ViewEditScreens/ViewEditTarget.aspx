<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEditTarget.aspx.cs" Inherits="DeltoneCRM.Manage.ViewEditScreens.ViewEditTarget" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../js/jquery-2.1.3.js"></script>
    <script src="../../js/jquery-ui-1.10.3.custom.js"></script>
    <script src="../../js/jquery.validate.js"></script>
    <link href="../../css/tinytools.toggleswitch.css" rel="stylesheet" />
    <script src="../../js/tinytools.toggleswitch.js" type="text/javascript"></script>
    <link href="../../css/ManageCSS.css" rel="stylesheet" />
    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
    <script src="../../js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../css/jquery.dataTables_new.css" />
    <script type="text/javascript">
        var dayOff = "";
        $(document).ready(function () {

            $('#activechkbox').toggleSwitch({
                onLabel: "ACTIVE",
                offLabel: "INACTIVE",
                width: "115px",
                height: "30px"
            });

            //// var hourset = dateNow.getHours();
            // $('#dayofftextbox').datetimepicker({
            //     controlType: 'select',
            //     timeFormat: 'hh:mm tt',
            //     stepMinute: 30,
            //     hour: hourset,
            //     minute: 30,
            //     hourMin: 8,
            //     hourMax: 17,
            //     dateFormat: 'dd/mm/yy'

            // });

            $('#Save').hide();
            $('#DropDownList1').hide();
            $('#DropDownList2').hide();

            $('#SwitchToEdit').click(function () {
                $('#SwitchToEdit').hide();
                $('#BtnCloseWindow').hide();
                $('#Save').show();
                $('#DropDownList1').show();
                $('#DropDownList2').show();
                $('#NewMonth').hide();
                $('#NewUser').hide();

                $('#NewCommission').prop("readonly", false);
                $('#NewDays').prop("readonly", false);
                $('#NewYear').prop("readonly", false);
                $('#dayofftextbox').prop("readonly", false);

                $('#NewCommission').removeClass('txtbox-200-style');
                $('#NewCommission').addClass('txtbox-200-style-edit');

                $('#NewDays').removeClass('txtbox-200-style');
                $('#NewDays').addClass('txtbox-200-style-edit');

                $('#NewYear').removeClass('txtbox-200-style');
                $('#NewYear').addClass('txtbox-200-style-edit');

                $('#dayofftextbox').removeClass('txtbox-200-style');
                $('#dayofftextbox').addClass('txtbox-200-style-edit');
            });

            $("#<%=form1.ClientID%>").validate({
                onfocusout: false,
                onkeyup: false,
                rules: {
                    NewCommission: {
                        required: true,
                    },
                    NewDays: {
                        required: true,
                    },
                    NewYear: {
                        required: true,
                    }
                },
                highlight: function (element) {
                    $(element).closest("input")
                    .addClass("textbox_001_err")
                    .removeClass("txtbox-200-style-edit");
                },
                unhighlight: function (element) {
                    $(element).closest("input")
                    .removeClass("textbox_001_err")
                    .addClass("txtbox-200-style-edit");
                },
                errorPlacement: function (error, element) {

                    return true;
                },
                success: function (label) {


                },
                submitHandler: function (form) {
                    stDate = $('#dayofftextbox').val();
                    if (stDate != "dd/mm/yyyy")
                        dayOff = stDate;
                    var approved = false;
                    if ($("#approvedcchk").is(':checked'))
                        approved = true;

                    $.ajax({
                        type: "POST",
                        url: "../Process/Process_EditTarget.aspx",
                        data: {
                            targetid: $('#hdnEditTarget').val(),
                            targetcommision: $('#NewCommission').val(),
                            targetmonth: $('#DropDownList2 option:selected').val(),
                            workingdays: $('#NewDays').val(),
                            targetyears: $('#NewYear').val(),
                            user: $('#DropDownList1 option:selected').val(),
                            isapproeved:approved,
                            dayOff: dayOff,
                        },
                        success: function (msg) {
                            alert("Target has been edited successfully");
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
        var linkTableComDatatable = "";

        $(document).ready(function () {

             linkTableComDatatable = $('#exampledayOff').dataTable({
                 "ajax": "../Process/FetchRepDayOff.aspx?repId=" + $('#DropDownList1 option:selected').val() + "&tarId=" + $('#hdnEditTarget').val(),
                "columnDefs": [

                    { className: 'align_center', "targets": [0, 1, 2,3] },
                ],
                "order": [[0, "desc"]],
                "iDisplayLength": 10,
            });
        });

        function Delete(dayOffId) {

            var r = confirm("Do you want to delete this record?");
            if (r == true) {
                $.ajax({
                    url: "../Process/DeleteRepDayOff.aspx?dayoffId=" + dayOffId,

                    success: function (response) {
                        alert('Successfully deleted.');
                       
                        location.reload();
                    }
                });
            }
        }

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
    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" name="hdnEditItemID" id="hdnEditItemID" runat="server" /><input type="hidden" name="hdnEditSupplier" id="hdnEditSupplier" runat="server" /><input type="hidden" name="hdnSupplier" id="hdnSupplier" runat="server" />
        <div>
            <div id="EditScreen" title="EDIT ITEM INFORMATION">

                <table width="680" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="height-15px-style">&nbsp;</td>
                    </tr>

                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="height-15px-style">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="15">
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">USER&nbsp;&nbsp;</td>
                                                            <td width="215">
                                                                <input name="NewUser" type="text" class="txtbox-200-style" id="NewUser" value="" readonly="true" runat="server" /><asp:DropDownList ID="DropDownList1" runat="server" class="drpdwn-200-style-edit" ClientIDMode="Static"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20">&nbsp;</td>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100">&nbsp;</td>
                                                            <td width="200">&nbsp;</td>
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
                                    <td height="15">
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">TARGET COMM&nbsp;&nbsp;</td>
                                                            <td width="215">
                                                                <input name="NewCommission" type="text" class="txtbox-200-style" id="NewCommission" readonly="true" runat="server" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20">&nbsp;</td>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">WORKING DAYS</td>
                                                            <td width="200">
                                                                <input name="NewDays" type="text" class="txtbox-200-style" id="NewDays" readonly="true" runat="server" /></td>
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
                                    <td height="15">
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">TARGET MONTH</td>
                                                            <td width="215">
                                                                <input name="NewMonth" type="text" class="txtbox-200-style" id="NewMonth" readonly="true" runat="server" /><asp:DropDownList ID="DropDownList2" class="drpdwn-200-style-edit" runat="server">
                                                                    <asp:ListItem  Text="Select Month" Value="-1"></asp:ListItem>
                                                                    <asp:ListItem Text="January" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="February" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="March" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="April" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="May" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="June" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="July" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="August" Value="8"></asp:ListItem>

                                                                    <asp:ListItem Text="September" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20">&nbsp;</td>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">TARGET YEAR</td>
                                                            <td width="200">
                                                                <input name="NewYear" type="text" class="txtbox-200-style" id="NewYear" readonly="true" runat="server" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="height-15px-style"></td>
                                </tr>

                                <tr>
                                    <td height="15">
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style"> DAY OFF</td>
                                                            <td width="215">
                                                                <input name="dayofftextbox" type="date" class="txtbox-200-style" id="dayofftextbox" readonly="true" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20">&nbsp;</td>
                                                  <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">APPROVED DAY</td>
                                  <td width="200"><input name="approvedcchk" type="checkbox" class="txtbox-200-style" id="approvedcchk"   readonly="true" runat="server"/></td>
                                </tr>
                              </table></td>
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
                        <td height="15">
                            <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                                <tr>
                                    <td>
                                        <input type="checkbox" id="activechkbox" runat="server" /></td>
                                    <td>
                                        <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                            <tr>
                                                <td width="125px">
                                                    <input type="button" class="btn-red-style" id="SwitchToEdit" value="EDIT" /></td>
                                                <td width="15px">&nbsp;</td>
                                                <td width="125px">
                                                    <input type="button" id="BtnCloseWindow" class="btn-green-style" value="OK" onclick="parent.closeEditWindow();" />
                                                    <input type="submit" id="Save" class="btn-red-style" value="SAVE" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td height="15">&nbsp;</td>

                    </tr>


                    <tr>
                        <td height="15">&nbsp;</td>

                    </tr>

                    <tr>
                        <td>
                            <table width="680" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="430">&nbsp;

                              <input type="hidden" id="hdnEditTarget" name="hdnEditTarget" runat="server" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td width="250" style="display: none;">
                                        <input name="createrecord" type="submit" class="clk_button_01" id="createrecord" value="UPDATE ITEM" /><input name="button" type="reset" class="settingsbutton_gray" id="button" style="display: none;" value="CLEAR FORM" /></td>
                                </tr>
                                <tr>
                                    <td height="15">&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>


                    <tr>
                        <td class="section_headings"> DAY OFF LIST
                    <asp:Label ID="lblMonthName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="white-box-outline">
                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                <tr>
                                    <td height="20px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>



                                        <table id="exampledayOff">

                                            <thead>
                                                <tr>
                                                    <th align="left">ID</th>
                                                    <th align="left">DAY</th>
                                                    <th align="left">APPROVED</th>
                                                    <th align="left">DELETE</th>

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

                </table>

            </div>
        </div>
    </form>
</body>
</html>
