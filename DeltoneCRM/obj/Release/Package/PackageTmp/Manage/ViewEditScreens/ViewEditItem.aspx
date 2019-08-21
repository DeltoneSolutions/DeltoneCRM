<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEditItem.aspx.cs" Inherits="DeltoneCRM.Manage.ViewEditScreens.ViewEditItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View/Edit Item</title>
    <link href="../../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <script src="../../js/jquery-2.1.3.js"></script>
    <script src="../../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../../css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="../../css/tinytools.toggleswitch.css" rel="stylesheet" />
    <script src="../../js/jquery.validate.js"></script>
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
    <script src="../../js/tinytools.toggleswitch.js" type="text/javascript"></script>
    <link href="../../css/ManageCSS.css" rel="stylesheet" />

    <script type="text/javascript">


        function blockChar(ev) {
            var keynum;
            if (window.event) keynum = ev.keyCode;// IE
            else if (ev.which) keynum = ev.which; // Other browsers
            else keynum = ev.charCode;
            if (keynum == 44) { return false; }

        }

        $(document).ready(function () {

            addOneToQuantity();
            minusOneToQuantity();
        });




        function addOneToQuantity() {
            var count = 0;

            $("#oneaddimage").click(function () {
                alert("Please Add through WareHouse");
                if ($('#QuantityText').val())
                    count = parseInt($('#QuantityText').val());
                count = count + 1;
                $('#QuantityText').val(count);
            });
        }

        var cogPrice = "";


        function minusOneToQuantity() {
            var count = 0;
            $("#oneminusimage").click(function () {
                alert("Please Add through WareHouse");
                count = parseInt($('#QuantityText').val());

                count = count - 1;
                $('#QuantityText').val(count);

            });
        }

        $(document).ready(function () {
            $('#Save').hide();
            $('#DropDownList1').hide();

            var managerpageClicked = parent.managerpageClicked;
            var CogControl = parent.CogControl;
            var unitPriceControl = parent.unitPriceControl;
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
                $('#chk_bestprice').prop('disabled', false);
                $('#chk_faulty').prop('disabled', false);
                $('#QuantityText').prop('readonly', false);
                $('#DSBPrice').prop('readonly', false);

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

                $('#QuantityText').removeClass("txtbox-200-style");
                $('#QuantityText').addClass("txtbox-200-style-edit");

                $('#DSBPrice').removeClass("txtbox-200-style");
                $('#DSBPrice').addClass("txtbox-200-style-edit");
            });

            $("#<%=form1.ClientID%>").validate({
                onfocusout: false,
                onkeyup: false,
                rules: {
                    NewDescription: {
                        required: true,
                    },
                    NewItemCode: {
                        required: true,
                    },
                    NewCOG: {
                        required: true,
                    },
                    NewResellPrice: {
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
                        url: "../Process/ProcessEditItem.aspx",
                        data: {
                            ItemID: $('#hdnEditItemID').val(),
                            SupplierItemCode: $('#NewItemCode').val(),
                            Description: $('#NewDescription').val(),
                            COG: $('#NewCOG').val(),
                            OEM: $('#NewOEMCode').val(),
                            ManagedUnitPrice: $('#NewResellPrice').val(),
                            SupplierID: $("#DropDownList1 option:selected").val(),
                            ActInact: $('#activechkbox').is(':checked'),
                            BestPrice: $('#chk_bestprice').prop('checked'),
                            Faulty: $('#chk_faulty').prop('checked'),
                            Quantity: $('#QuantityText').val(),
                            DSBPrice: $('#DSBPrice').val(),
                        },
                        success: function (msg) {
                            alert("Item has been edited successfully");
                           
                            window.parent.closeIframe();

                            if (managerpageClicked)
                                window.parent.location.reload(false);
                            else {
                                console.log($('#NewCOG').val());
                                CogControl.val($('#NewCOG').val());
                               // unitPriceControl.val($('#NewResellPrice').val());
                                parent.updatePriceAfterProductPriceChange();
                            }
                        },
                        error: function (xhr, err) {
                            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                            alert("responseText: " + xhr.responseText);
                        },
                    });
                }

            });



            $('#DropDownList1').change(function () {
                $('#hdnSupplier').val($(this).val());
                $('#hdnEditSupplier').val($(this).val());
                $('#hdnEditSupplier').val($("#DropDownList1 option:selected").val());
            });


            $("#hideSpan").click(function () {
                $("#hideSpan").hide();
                $("#showSpan").show();
                $("#historytr").hide();
                $("#historyheader").hide();
            });

            callHistoryFunction();

        });
        var table = "";
        function callHistoryFunction() {

            $("#showSpan").click(function () {
                $("#showSpan").hide();
                $("#hideSpan").show();
                $("#historytr").show();
                $("#historyheader").show();
                if (table)
                    table.destroy();

                table =
                $('#tblhistory').dataTable({

                    "ajax": "../Process/FetchProductAudit.aspx?itemId=" + $('#hdnEditItemID').val(),
                    "columnDefs": [
                     { className: 'align_left', "targets": [1, 2] },
                     { className: 'align_center', "targets": [3] },

                    ],
                    "order": [[0, "desc"]],
                    "iDisplayLength": 25,
                });

            });


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
            outline: none;
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

        .addoneSpan {
            border-bottom: 1px solid #e9e9e9;
            border-right: 1px solid #e9e9e9;
            border-top: 1px solid #e9e9e9;
            /* color: #6f6f6f; */
            cursor: pointer;
            display: block;
            font-size: 7px;
            height: 16px;
            line-height: 15px;
            position: relative;
            text-align: center;
            width: 27px;
        }

        .spanStyleLink {
            cursor: pointer;
            color: blue;
            text-decoration: underline;
            font-style: normal;
            font-size: 16px;
            text-align: center;
        }
    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="EditScreen" title="EDIT ITEM INFORMATION">

                <table width="680" border="0" align="center" cellpadding="0" cellspacing="0" class="tbl-bg">

                    <tr>
                        <td class="height-15px-style">&nbsp</td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF">
                            <table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td class="height-15px-style">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="300">

                                                    <table width="315 " border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">SUPPLIER</td>
                                                            <td width="215">
                                                                <input name="txtSupplier" type="text" class="txtbox-200-style" id="txtSupplier" value="" readonly runat="server" />
                                                                <asp:DropDownList ID="DropDownList1" runat="server" ClientIDMode="Static" class="drpdwn-200-style-edit"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </td>
                                                <td width="20">&nbsp;</td>
                                                <td width="315">

                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">DESCRIPTION&nbsp;</td>
                                                            <td width="215">
                                                                <input name="NewDescription" type="text" class="txtbox-200-style" id="NewDescription" onkeypress="return blockChar(event)" readonly="true" runat="server" /></td>
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
                                <tr>
                                    <td height="15">
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">PRODUCT CODE&nbsp;&nbsp;</td>
                                                            <td width="215">
                                                                <input name="NewItemCode" type="text" class="txtbox-200-style" id="NewItemCode" readonly="true" runat="server" /></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="20">&nbsp;</td>
                                                <td width="315">
                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="70" class="labels-style">Quantity</td>
                                                            <td width="100">
                                                                <input name="QuantityText" type="text" class="txtbox-200-style-edit" style="width: 70px;" id="QuantityText" readonly="true" runat="server" />

                                                            </td>
                                                            <td>
                                                                <span id="plusspan" class="addoneSpan">
                                                                    <img class="manImg" src="../../Images/details_open.png" id="oneaddimage" />
                                                                </span>
                                                                <span id="minusspan" class="addoneSpan">

                                                                    <img class="manImg" src="../../Images/details_close.png" id="oneminusimage" />
                                                                </span>
                                                            </td>
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
                                    <td class="height-15px-style">
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="315">

                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">COST $</td>
                                                            <td width="215">
                                                                <input name="NewCOG" type="text" class="txtbox-200-style" id="NewCOG" readonly="true" runat="server" /></td>
                                                        </tr>
                                                    </table>

                                                </td>
                                                <td width="20">&nbsp;</td>
                                                <td width="315">

                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">RESELL PRICE&nbsp;&nbsp;$</td>
                                                            <td width="215">
                                                                <input name="NewResellPrice" type="text" class="txtbox-200-style" id="NewResellPrice" readonly="true" runat="server" /></td>
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
                                    <td class="height-15px-style">
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="315">

                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">BEST PRICE</td>
                                                            <td width="215">
                                                                <input type="checkbox" id="chk_bestprice" disabled="disabled" runat="server" /></td>
                                                        </tr>
                                                    </table>

                                                </td>
                                                <td width="20">&nbsp;</td>
                                                <td width="315">

                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">OFTEN FAULTY&nbsp;&nbsp;$</td>
                                                            <td width="215">
                                                                <input type="checkbox" id="chk_faulty" disabled="disabled" runat="server" /></td>
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
                                    <td class="height-15px-style">
                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="315">

                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td width="100" class="labels-style">DSB</td>
                                                            <td width="215">
                                                                <input type="text" class="txtbox-200-style" id="DSBPrice" readonly="true" runat="server" /></td>
                                                        </tr>
                                                    </table>

                                                </td>
                                                <td width="20">&nbsp;</td>
                                                <td width="315">

                                                    <table width="315" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <%-- <td width="100" class="labels-style">OFTEN FAULTY&nbsp;&nbsp;$</td>
                                                            <td width="215">
                                                                <input type="checkbox" id="Checkbox2" disabled="disabled" runat="server" /></td>--%>
                                                        </tr>
                                                    </table>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="height-15px-style">

                                        <table width="650" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="300"></td>
                                                <td width="20">
                                                    <input type="hidden" name="hdnSupplier" id="hdnSupplier" runat="server" /></td>
                                                <td>
                                                    <input type="hidden" name="hdnEditSupplier" id="hdnEditSupplier" runat="server" /></td>
                                                <td>
                                                    <input type="hidden" name="hdnEditItemID" id="hdnEditItemID" runat="server" /></td>
                                                <td width="300">&nbsp;</td>
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
                        <td>



                            <table cellpadding="0" cellspacing="0" class="auto-style1">
                                <tr>
                                    <td width="380px">
                                        <input type="checkbox" id="activechkbox" runat="server" /></td>
                                    <td width="270px">
                                        <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                            <tr>
                                                <td width="125px">
                                                    <input type="button" id="SwitchToEdit" class="btn-red-style" value="EDIT" /></td>
                                                <td width="15px">&nbsp;</td>
                                                <td width="125px">
                                                    <input type="button" id="BtnCloseWindow" class="btn-green-style" value="OK" onclick="parent.closeEditWindow();" />
                                                    <input type="submit" id="Save" class="btn-red-style" value="SAVE" />
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
                            <table width="680" border="0" cellspacing="0" cellpadding="0">
                                <tr style="display: none;">
                                    <td width="430">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="250" style="display: none;">
                                        <input name="createrecord" type="submit" class="clk_button_01" id="createrecord" value="UPDATE ITEM" /><input name="button" type="reset" class="settingsbutton_gray" id="button" style="display: none;" value="CLEAR FORM" /></td>
                                </tr>

                            </table>
                        </td>
                    </tr>

                </table>

            </div>

            <table>
                <tr>
                    <td><span id="showSpan" class="spanStyleLink">Show History</span>
                        <span id="hideSpan" class="spanStyleLink" style="display: none;">Hide History</span>
                    </td>
                </tr>
                <tr id="historyheader" style="display: none;">
                    <td class="section_headings">History</td>
                </tr>
                <tr id="historytr" style="display: none;">
                    <td >
                        <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">

                            <tr>
                                <td height="20px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table class="display" id="tblhistory">
                                        <thead>
                                            <tr>
                                                <th>ID</th>
                                                <th>Action</th>
                                                <th>Created Date</th>
                                                <th>Action By</th>
                                                <th>Before Change</th>
                                                 <th>After Change</th>
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
        </div>
    </form>

</body>
</html>
