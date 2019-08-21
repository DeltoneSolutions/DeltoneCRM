<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="CreateCreditNote.aspx.cs" Inherits="DeltoneCRM.CreateCreditNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/CreditNote.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/jquery-ui.js"></script>
    <style type="text/css">
        body {
            background-color: #FFFFCC !important;
        }
         </style>
    <script type="text/javascript">

        function SubmitDialog(Message, url, PrintScreenUrl) {
            $('#SubmitMessage').html(Message);
            //Set the Navogation URL
            $('#navigateURL').val(url);

            Dialog = $('#Dialog-Submit-Confirmation').dialog({
                resizable: false,
                modal: true,
                title: 'SUBMIT CONFIRMATION',
                height: 400,
                width: 710
            });

            $('#Dialog-Submit-Confirmation').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");

            });

            return false;
        }

        $(document).ready(function () {
            $(window).keydown(function (event) {
                if (event.keyCode == 13) {
                    event.preventDefault();
                    return false;
                }
            });

            if ($('#<%=hdnORDERID.ClientID%>').val() != "") {
                $.ajax({
                    url: 'Fetch/VerifyIfCommishIsSplit.aspx',
                    data: {
                        OID: $('#<%=hdnORDERID.ClientID%>').val(),
                    },
                    success: function (data) {
                        var indidata = data.split('|');
                        if (indidata[0] == "1") {
                            $('#accountowner').html(indidata[2].toUpperCase() + " - ");
                            $('#salesperson').html(indidata[1].toUpperCase() + " - ");
                        }
                    }
                });
            }



            //$('.blackout').css("display", "block");

            Dialog_TypeCredit = $('#Dialog_TypeOfCredit').dialog({
                resizable: false,
                modal: true,
                title: 'TYPE OF CREDIT',
                height: 400,
                width: 710
            });

            $('#Dialog_TypeOfCredit').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            $('#slTypeOfCredit').on('change', function () {

                if (this.value != '0') {

                    $('#<%=txtCreditReason.ClientID%>').val($("#slTypeOfCredit option:selected").text());
                    Row_Edit(this.value);
                    Dialog_TypeCredit.dialog('close');
                }
            });

            $('#btnReturnDashBoard').click(function () {
                window.location.href = $('#navigateURL').val();
            });

            /*This  function make Credit rows  editable given by value*/
            function Row_Edit(value) {
                $('#tblLineItems  tr').each(function (i, row) {
                  //  console.log('aaaa');
                    var $row = $(row);
                  //  console.log('aaaa' + $row.html());
                    $Description = $row.find('input[name*="ItemDesc"]');
                    $QTY = $row.find('input[name*="qty"]');
                    $UnitPrice = $row.find('input[name*="UnitPrice"]');
                    $COG = $row.find('input[name*="COG"]');

                    if (value == '1') {
                        $QTY.removeAttr("disabled")
                        $UnitPrice.attr("disabled", "disabled");
                        $COG.attr("disabled", "disabled");
                    }
                    if (value == '2') {
                        $QTY.removeAttr("disabled");
                        $UnitPrice.attr("disabled", "disabled");
                        $COG.attr("disabled", "disabled");
                    }
                    if (value == '4') {

                        $COG.attr("disabled", "disabled");
                    }

                    if (value == '3') {
                        $COG.val('0');
                        $COG.attr("disabled", "disabled");
                    }
                    //Delivery Items EDit  Mode Changes 

                    if ($Description.val().indexOf("D/L Handling") >= 0) {
                        $COG.val('0');
                        $COG.attr("disabled", "disabled");
                        $QTY.val("1");
                        $QTY.attr("disabled", "disabled");
                        $UnitPrice.removeAttr("disabled");

                    }

                });

                $('#COG').blur(function () {

                    update_price_firstrow();
                });

                $('#qty').blur(function () {
                    update_price_firstrow();
                });

                $('#UnitPrice').blur(function () {
                    update_price_firstrow();
                });

                function fillOrderItems(OrderItems, Order, CNcontactDetails) {

                    var arr_OrderItems = OrderItems.split("|");
                    var arr_Order = Order.split(":");
                    var ContactDetails = CNcontactDetails.split(":");

                    //First Order Item
                    var arr_FirstOrderItem = arr_OrderItems[0].split(",");

                    var QtyFill = 0;
                    //alert($('#<%=txtCreditReason.ClientID%>').val());
                    if ($('#<%=txtCreditReason.ClientID%>').val() == "PRICE REDUCTION") {
                        QtyFill = 0
                    }
                    else {
                        QtyFill = arr_FirstOrderItem[3];
                    }
                    //alert(QtyFill);
                    $('#ItemDesc').val(arr_FirstOrderItem[1]);
                    $('#suppliercode').val(arr_FirstOrderItem[4]);
                    $('#SuppName').val(arr_FirstOrderItem[6]);
                    $('#COG').val(QtyFill);
                    $('#qty').val(arr_FirstOrderItem[5]);
                    $('#UnitPrice').val(arr_FirstOrderItem[2]);
                    $('#hidden_item_code').val(arr_FirstOrderItem[0]);
                    $('#hidden_Supplier_Name').val(arr_FirstOrderItem[6]);

                    //Update  Price For the First Row
                    update_price_firstrow();

                    //Populate Rows for the Subsquent Items
                    for (j = 1; j < arr_OrderItems.length; j++) {
                        if (arr_OrderItems[j]) {
                            //<input  type="text"  name="SuppName" id="SuppName" disabled="disabled" />
                            var arr_OrderItem = arr_OrderItems[j].split(",");
                            //var $row = $('<tr class="item-row"><td class="tbl-auto-row-01">&nbsp;</td><td class="tbl-auto-row-02"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03"><label for="suppliercode" ><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                            var $row = $('<tr class="item-row"><td class="tbl-auto-row-01"><a class="delete" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03"><label for="suppliercode" ><input name="SuppName" type="text" class="tbl-auto-row-03-inside" id="SuppName"></td><td class="tbl-auto-row-04"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");

                            var SubQtyFill = 0;

                            if ($('#<%=txtCreditReason.ClientID%>').val() == "PRICE REDUCTION") {
                                SubQtyFill = 0
                            }
                            else {
                                SubQtyFill = arr_OrderItem[3];
                            }

                            $row.find('#ItemDesc').val(arr_OrderItem[1]);
                            $row.find('#suppliercode').val(arr_OrderItem[4]);
                            $row.find('#COG').val(SubQtyFill);
                            $row.find('#qty').val(arr_OrderItem[5]);
                            $row.find('#UnitPrice').val(arr_OrderItem[2]);
                            $row.find('#hidden_item_code').val(arr_OrderItem[0]);
                            $row.find('#hidden_Supplier_Name').val(arr_OrderItem[6]);
                            $row.find('#SuppName').val(arr_OrderItem[6]);
                            //Modified here Add Supplier Name

                            update_price_row($row);

                            bind($row);

                        }

                    }
                   
                    if ($('#<%=txtCreditReason.ClientID%>').val() == "WRONG GOODS - DELTONE FAULT" ||
                      $('#<%=txtCreditReason.ClientID%>').val() == "DID NOT ORDER") {

                       

                        var $row = $('<tr class="item-row"><td class="tbl-auto-row-01"><a class="delete" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03"><label for="suppliercode" ><input name="SuppName" type="text" class="tbl-auto-row-03-inside" id="SuppName"></td><td class="tbl-auto-row-04"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                        $row.find('#ItemDesc').val("Delivery & Handling");
                        $row.find('#SuppName').val("INHOUSE");
                        $row.find('#suppliercode').val("D & H");
                        $row.find('#COG').val("25");
                        $row.find('#qty').val("0");
                        $row.find('#UnitPrice').val("0");
                        $row.find('#hidden_item_code').val();
                        $row.find('#hidden_Supplier_Name').val("INHOUSE");
                        //Modified here Add Supplier Name instead of Original/Compatible

                        //Update the Price for that Row 
                        update_price_row($row);
                        //Bind Row Functonalities for Austopopulations ,prince updation ,etc
                        bind($row);

                    }

                    //Add Delivery Customer Delivery Handling if Exsists

                    if (arr_Order[2]) {
                        var CusDelCostItems = arr_Order[2];
                        var arr_CusDelCost = arr_Order[2].split("|");

                        if (arr_CusDelCost[0] || arr_CusDelCost[1]) {

                            var $row_deliverycost = $('<tr class="item_DelRow"><td class="tbl-auto-row-01"><a class="deleteDeliveryCost" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbx_item_description" id="Text1" disabled="disabled" value=""/></td><td>&nbsp;</td><td><input name="suppliercode" type="text" class="tbx_supplier_code" id="Text5" disabled="disabled"/></td><td><input name="COG" type="text" class="tbx_cog" id="Text4" disabled="disabled"/></td><td><label for="qty"></label><input name="qty" type="text" class="tbx_qty" id="qty" disabled="disabled"  value="1"/></td><td width="80"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbx_unit_price" id="UnitPrice" disabled="disabled"/></td><td width="99" align="right" class="total_outer"><span class="total">$00.00</span></td><td width="97" align="right" class="cogtotal_outer"><span class="cogtotal">$00.00</span></td><td width="1"><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="Text3" size="1" hidden="hidden"/></td></tr>').insertAfter(".item-row:last");
                            $row_deliverycost.find('#Text1').val("D/L Handling(" + arr_CusDelCost[0] + ")");
                            $row_deliverycost.find('#UnitPrice').val(arr_CusDelCost[1]);

                            update_prive_deliveryCost($row_deliverycost);
                            $row_deliverycost.find('#UnitPrice').blur(function () {
                                update_price_row($row_deliverycost);

                            });

                        }

                        //Add and Empty delivery line if theres no Delivery
                        //Update Price for the Delivery cost
                    }



                    //If Promotionl Items Exsists Append here
                    if ($('#<%=PROCOST.ClientID%>').val() != '0') {
                        var $row_promoitem = $('<tr class="item_DelRow"><td class="tbl-auto-row-01"><a class="deleteDeliveryCost" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbx_item_description" id="Text1" disabled="disabled" value=""/></td><td>&nbsp;</td><td><input name="suppliercode" type="text" class="tbx_supplier_code" id="Text5" disabled="disabled"/></td><td><input name="COG" type="text" class="tbx_cog" id="Text4" disabled="disabled"/></td><td><label for="qty"></label><input name="qty" type="text" class="tbx_qty" id="qty" disabled="disabled"  value="1"/></td><td width="80"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbx_unit_price" id="UnitPrice" disabled="disabled"/></td><td width="99" align="right" class="total_outer"><span class="total">$00.00</span></td><td width="97" align="right" class="cogtotal_outer"><span class="cogtotal">$00.00</span></td><td width="1"><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="Text3" size="1" hidden="hidden"/></td></tr>').insertAfter(".item-row:last");
                        $row_promoitem.find('#Text1').val("Promotional Item+Shipping Cost");
                        $row_promoitem.find('#UnitPrice').val($('#<%=PROCOST.ClientID%>').val());
                    update_Price_PromoCost($row_promoitem);
                }

                    //End Append Promotional rows If exsists
            }

                //Populate the Order Details

            if ($('#<%=hdnEditOrderItems.ClientID%>').val() != '') {

                    fillOrderItems($('#<%=hdnEditOrderItems.ClientID%>').val(), $('#<%=hdnEditOrder.ClientID%>').val(), $('#<%=hdnCreditNoteContactDetails.ClientID%>').val());
            }


            $('#<%=btnOrderSubmit.ClientID%>').click(function () {
                    $('#<%=hdnTotal.ClientID%>').val($('#profitFullTotal').html().replace("$", "")); //Get the Total and write to the Hidden Field
                $('#<%=hdnSubTotal.ClientID%>').val($('#ProfitExGST').html().replace("$", "")); //Get the subtotal and write to the Hidden Field
                //ADD A TYPE OF CREDIT TO HIDDEN FIELD
                $('#<%=TYPEOFCREDIT.ClientID%>').val($('#<%=txtCreditReason.ClientID%>').val());

                var CreditItems = '';
                var CusDelCostItems = '';
                $('#tblLineItems tr').each(function (i, row) {

                    var $row = $(row);

                    //Modification done  here Add SupplierName here 


                    CreditItems = CreditItems + $row.find('input[name*="ItemDesc"]').val() + ',' + $row.find('input[name*="suppliercode"]').val() + ',' + $row.find('input[name*="COG"]').val() + ',' + $row.find('input[name*="qty"]').val() + ',' + $row.find('input[name*="UnitPrice"]').val() + ',' + $row.find('input[name*="SuppName"]').val();
                    CreditItems = CreditItems + "|";
                    var description = $row.find('input[name*="ItemDesc"]').val();
                    if (description.indexOf("D/L Handling") > -1) {
                        CusDelCostItems = description + "|" + $row.find('input[name*="UnitPrice"]').val();
                    }
                });

                $('#<%=OrderItems.ClientID%>').val(CreditItems);
                $('#<%=CusDelCostItems.ClientID%>').val(CusDelCostItems);

            });


            $(document).on('click', '.deleteDeliveryCost', function () {

                var $tr = $(this).closest('.item_DelRow');
                //$('#<%=CusDelCostItems.ClientID%>').val($tr.find('input[id="Text1"]').val() + "|" + $tr.find('input[name="UnitPrice"]').val());
                $tr.remove();
                update_price();
            });


            $(document).on('click', '.delete', function () {
                var $tr = $(this).closest('.item-row');
                $tr.remove();
                update_price();//Update the Price When Removing 

            });

            function bind($row) {
                //$row.find('input[name="suppliercode"]').blur(update_price);
                $row.find('input[name="qty"]').blur(update_price);
                $row.find('input[name="UnitPrice"]').blur(update_price);
                $row.find('input[name="COG"]').blur(update_price);
            };

            function update_Price_PromoCost($row) {

                var newrow = $row;
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            }


            function update_prive_deliveryCost($row) {
                var newrow = $row;
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            }

                //Update the Price for the First Fixed Row
            function update_price_firstrow() {

                //Acess the First Row of the Table 
                var $row = $('#tblLineItems tr:first');


                var price = ($('#qty').val()) * ($('#UnitPrice').val());
                //Modified here SK
                // $('.total').html("$" + parseFloat(price).toFixed(2));
                $row.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = ($('#qty').val()) * ($('#COG').val());
                //Modified here SK
                //$('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                $row.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            }




                //Custome row function to  Update row
            function update_price_row($row) {
                var newrow = $row;
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            };

            function update_price() {

                var newrow = $(this).parents('.item-row');
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            };

                //UPDATE THE ORDER TOTAL AND DISPLAY TOTAL 
            function update_total() {
                var subtotal = 0;
                var lineprofittotal = 0;

                //Profit Item Total Calculation
                $('.total').each(function (i) {
                    linesubtotal = $(this).html().replace("$", "");
                    if (!isNaN(subtotal)) subtotal += Number(linesubtotal);
                });

                //COG Item Total Calculation
                $('.cogtotal').each(function (o) {
                    coglinesubtotal = $(this).html().replace("$", "");
                    if (!isNaN(lineprofittotal)) lineprofittotal += Number(coglinesubtotal);
                });

                //GST for the Profit Total
                gst = (parseFloat(subtotal) * 10) / 100;

                //GST for the COG Total
                gst_COG = (parseFloat(lineprofittotal) * 10) / 100;

                //COG Full Total 
                fulltotal_COG = parseFloat(lineprofittotal) + parseFloat(gst_COG);

                //Profit Sub Total ExGST
                //ORIGINAL - var ProfitSubTotalExGST = parseFloat(Number(subtotal));
                var ProfitSubTotalExGST = ((parseFloat(Number(subtotal)).toFixed(2)) / 1.1);

                //COG SubTotal EXGST
                //ORIGINAL - var COGSubTotalExGST = parseFloat(Number(lineprofittotal));
                var COGSubTotalExGST = parseFloat(Number(lineprofittotal)).toFixed(2);

                //Commission EXGST 
                exgsttotal = parseFloat(ProfitSubTotalExGST - COGSubTotalExGST).toFixed(2);

                //GST Calcualtions
                var ProfitTotalGSTAmount = parseFloat(ProfitSubTotalExGST * 0.1).toFixed(2);
                var COGTotalGSTAmount = parseFloat(COGSubTotalExGST * 0.1).toFixed(2);

                //FullTotal INC GST
                TotalProfitINCGST = Number(ProfitSubTotalExGST) + Number(ProfitTotalGSTAmount);
                TotalCOGINCGST = Number(COGSubTotalExGST) + Number(COGTotalGSTAmount);

                //Profit Calculation
                TOTAL_Profit = parseFloat(TotalProfitINCGST - TotalCOGINCGST).toFixed(2);

                //Add Delivery Cost to the COG Total
                lineprofittotal = parseFloat(lineprofittotal);

                //Add Promotional Item Cost here If any

                lineprofittotal = parseFloat(lineprofittotal);
                //+ parseFloat(promotionalItemCost);

                //Profit Full Total
                fulltotal = parseFloat(subtotal) + parseFloat(gst);

                //Add Customer Delivery Cost
                //customerDeliveryCost = Number($('#delcost').val());
                subtotal = parseFloat(subtotal);
                //+ parseFloat(Number($('#delcost').val()));


                $('#ProfitExGST').html("$" + parseFloat(ProfitSubTotalExGST).toFixed(2));
                $('#profitGST').html("$" + parseFloat(ProfitTotalGSTAmount).toFixed(2));
                $('#profitFullTotal').html("$" + parseFloat(TotalProfitINCGST).toFixed(2));

                //Cost of Good Total Display

                $('#subtotal').html("$" + parseFloat(COGSubTotalExGST).toFixed(2));
                $('#gst').html("$" + parseFloat(COGTotalGSTAmount).toFixed(2));//GST COG 
                $('#fulltotal').html("$" + parseFloat(TotalCOGINCGST).toFixed(2));

                ordertotal = fulltotal;


                //Calculate the Rep Commission

                var repCommission = 0;

                if ($('#<%=COMMISH_SPLIT.ClientID%>').val() == "0") {

                    $('#TR_SalespersonCommission').hide();
                    $.ajax({
                        url: 'fetch/getoperatorcommission.aspx',
                        async: false,
                        data: {
                            repid: $('#<%=ACCOUNT_OWNER_ID.ClientID%>').val(),
                        },
                        success: function (data) {
                            repCommission = data;

                            var CommishPerc = repCommission / 100;
                            var Commission = (parseFloat(exgsttotal) * CommishPerc).toFixed(2);
                            $('#totalprofit').html("$" + parseFloat(Commission).toFixed(2))//Comission   
                            $('#<%=hdnProfit.ClientID %>').val(TOTAL_Profit);
                           $('#<%=hdnCommision.ClientID%>').val(parseFloat(Commission).toFixed(2));
                           $('#<%=ACCOUNT_OWNER_COMMISH.ClientID%>').val(parseFloat(Commission).toFixed(2));
                       },

                        error: function (message) {
                            alert('Unable to retrieve commission for logged user. Please contact your administartor');
                        }
                    });
               }
               else {
                   $('#TR_SalespersonCommission').show();
                    //Split profits into two
                   var SplitProfits = exgsttotal / 2;
                   var SplitVolume = parseFloat(ProfitSubTotalExGST).toFixed(2) / 2;
                   $('#<%=VOLUME_SPLIT_AMOUNT.ClientID%>').val(SplitVolume);

                    //Split profits into two
                    var SplitProfits = exgsttotal / 2;

                    //Get Commission For Account Owner
                    $.ajax({
                        url: 'fetch/getoperatorcommission.aspx',
                        async: false,
                        data: {
                            repid: $('#<%=ACCOUNT_OWNER_ID.ClientID%>').val(),
                        },
                        success: function (data) {
                            repCommission = data;

                            var CommishPerc = repCommission / 100;
                            var Commission = (parseFloat(SplitProfits) * CommishPerc).toFixed(2);
                            $('#CD-accountownername').html($('#<%=ACCOUNT_OWNER_TXT.ClientID%>').val().toUpperCase() + " - ");
                        $('#totalprofit').html("$" + parseFloat(Commission).toFixed(2))//Comission  
                        $('#<%=ACCOUNT_OWNER_COMMISH.ClientID%>').val(parseFloat(Commission).toFixed(2));
                    },
                        error: function (message) {
                            alert('Unable to retrieve commission for logged user. Please contact your administartor');
                        }
                    });

                    //Get Commission For Salesperon
                $.ajax({
                    url: 'fetch/getoperatorcommission.aspx',
                    async: false,
                    data: {
                        repid: $('#<%=SALESPERON_ID.ClientID%>').val(),
                    },
                    success: function (data) {
                        repCommission = data;

                        var CommishPerc = repCommission / 100;
                        var Commission = (parseFloat(SplitProfits) * CommishPerc).toFixed(2);
                        $('#CD-salespersonname').html($('#<%=SALESPERSON_TXT.ClientID%>').val().toUpperCase() + " - ");
                        $('#totalprofitsalesperson').html("$" + parseFloat(Commission).toFixed(2))//Comission 
                        $('#<%=SALESPERSON_COMMISH.ClientID%>').val(parseFloat(Commission).toFixed(2));
                    },
                    error: function (message) {
                        alert('Unable to retrieve commission for logged user. Please contact your administartor');
                    }
                });
            }




                //End Calculatting the Rep Commisssion
        };
    }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="blackout">
    </div>
    <table align="center" cellpadding="0" cellspacing="0" class="btm_01">
        <tr>
            <td>

                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                    <tr>
                        <td class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td id="OrderTitle" runat="server" class="all-headings-style" style="display: none;">CREATE CREDIT NOTES</td>
                        <td id="QorN" runat="server">&nbsp;</td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                                <tr>
                                    <td class="white-box-outline-top">
                                        <table align="center" cellpadding="0" cellspacing="0" class="width-770-style">
                                            <tr>
                                                <td height="20px">&nbsp;</td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style3">
                                                    <table align="left" cellpadding="0" cellspacing="0" class="width-470-style">
                                                        <tr>
                                                            <td class="company-name-style" height="30px">
                                                                <div id="CompanyInfo" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="ContactInfo" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="BillingAddress" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-style">
                                                                <div id="StreetAddressLine2" runat="server"></div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="company-details-tel-style">
                                                                <div id="ContactandEmail" runat="server"></div>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td class="">
                                                    <table align="center" cellpadding="0" cellspacing="0" class="auto-style4" style="display: none;">
                                                        <tr>
                                                            <td class="delivery-add-heading-style">DELIVERY ADDRESS</td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <table align="center" cellpadding="0" cellspacing="0" class="width-250-style">
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="company-details-delivery-style">
                                                                            <div id="DeliveryContact" runat="server"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="company-details-delivery-style">
                                                                            <div id="DeliveryCompany" runat="server"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="company-details-delivery-style">
                                                                            <div id="DeliveryAddressLine1" runat="server"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="company-details-delivery-style">
                                                                            <div id="DeliveryAddressLine2" runat="server"></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;

                                                    
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="20px">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="20px">&nbsp;</td>
                                    <td width="150px" class="align-vertical">
                                        <table align="center" cellpadding="0" cellspacing="0" class="auto-style5">
                                            <tr>
                                                <td class="top-payment-terms-heading" style="display: none;">ORDER NUMBER</td>
                                            </tr>
                                            <tr>
                                                <td style="display: none;">
                                                    <div id="OrderNumber" runat="server"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="16px"><b>TYPE OF CREDIT</b></td>
                                            </tr>
                                            <tr>
                                                <td height="16px">
                                                    <asp:TextBox ID="txtCreditReason" runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="16px" class="top-payment-terms-heading">&nbsp;<b> ORDER NUMBER</b>&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="16px">
                                                    <div id="dtsnumber" runat="server"></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px" style="display: none;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td height="16px" style="display: none;">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td class="top-payment-terms-heading">&nbsp;&nbsp;&nbsp;&nbsp;<b>CREATED BY</b></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlUsersList" runat="server" CssClass="account-owner-drop">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td id="tdOrderCreateDate" class="top-payment-terms-heading" runat="server" style="display: none;">&nbsp;&nbsp;&nbsp;&nbsp;<b>CREATED DATE</b></td>
                                            </tr>

                                            <tr>
                                                <td height="16px">
                                                    <input name="datecreated" type="date" class="FormDateValues" id="datecreated" disabled="disabled" value="" runat="server" style="display: none;" />
                                                </td>
                                            </tr>


                                            <tr>
                                                <td height="16px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
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


    <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">

        <tr>
            <td class="white-bg-style" height="50px">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td class="all-subheadings-style">Products</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                <tr>
                                    <td class="tbl01-clm01-style">X<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm02-style">ITEM DESCRIPTION<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm03-style">SUPPLIER NAME<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm04-style">SUPPLIER<br />
                                        CODE</td>
                                    <td class="tbl01-clm05-style">COG
                                        <br />
                                        EX GST</td>
                                    <td class="tbl01-clm06-style">QTY<br />
                                        &nbsp;</td>
                                    <td class="tbl01-clm07-style">UNIT PRICE<br />
                                        INC GST</td>
                                    <td class="tbl01-clm08-style">TOTAL<br />
                                        INC GST</td>
                                    <td class="tbl01-clm09-style">COG TOTAL<br />
                                        EX GST</td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <table id="tblLineItems" width="940" border="0" align="center" cellpadding="0" cellspacing="0" id="lineitems">
                                <tr class="item-row">
                                    <td class="tbl-auto-row-01"><a class="delete" title="Remove row">
                                        <img src="Images/x.png" width="16" height="16" /></td>
                                    <td class="tbl-auto-row-02">
                                        <label for="ItemDesc"></label>
                                        <input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td>
                                    <td class="tbl-auto-row-03">
                                        <label for="suppliercode">
                                            <input name="SuppName" type="text" class="tbl-auto-row-03-inside" id="SuppName"></td>
                                    <td class="tbl-auto-row-04">
                                        <label for="suppliercode"></label>
                                        <input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td>
                                    <td class="tbl-auto-row-05">
                                        <label for="COG"></label>
                                        <input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td>
                                    <td class="tbl-auto-row-06">
                                        <label for="qty"></label>
                                        <input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td>
                                    <td class="tbl-auto-row-07">
                                        <label for="UnitPrice"></label>
                                        <input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td>
                                    <td align="right" class="tbl-auto-row-08"><span class="total">$00.00</span></td>
                                    <td align="right" class="tbl-auto-row-09"><span class="cogtotal">$00.00</span>
                                        <label for="hidden_item_code"></label>
                                        <input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden" />
                                        <input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name" value="" />
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

                            <input name="addnewitem" type="button" class="add-btn" id="addnewitem" value="ADD NEW ITEM" />

                        </td>
                    </tr>
                    <tr>
                        <td height="20px">&nbsp;</td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td height="20px">&nbsp;</td>
        </tr>
        <tr>
            <td class="white-bg-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style" style="display: none;">Delivery</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>



                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                <tr>
                                    <td class="tbl02-clm01-style">CUSTOMER DELIVERY TYPE<br />
                                        &nbsp;</td>
                                    <td class="tbl02-clm02-style">COST<br />
                                        INC GST</td>
                                    <td class="tbl02-clm03-style">&nbsp;</td>
                                </tr>
                            </table>



                        </td>
                    </tr>
                    <tr>
                        <td>


                            <table width="940" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr class="del-row">

                                    <td class="tbl-delivery-01-outside">
                                        <input name="deldet" type="text" class="tbl-delivery-01-inside" id="deldet" />
                                    </td>
                                    <td class="tbl-delivery-02-outside">
                                        <input name="delcost" type="text" class="tbl-delivery-02-inside" id="delcost" />
                                    </td>
                                    <td class="tbl-delivery-03-outside">N/A<input name="hidden_delivery_item_id" type="text" id="hidden_delivery_item_id" size="1" hidden="hidden" />
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

                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                <tr>
                                    <td class="tbl02-clm01-style-supp">DELIVERY FROM SUPPLIER<br />
                                        &nbsp;</td>
                                    <td class="tbl02-clm02-style-supp">&nbsp;</td>
                                    <td class="tbl02-clm03-style-supp">COST<br />
                                        EX GST</td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="tblSupplierDeliveryCost" width="940" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr class="supp-del-row">
                                    <td class="tbl-delivery-04-outside">
                                        <input name="suppdeldet" type="text" class="tbl-delivery-04-inside" id="suppdeldet" disabled="disabled"></td>
                                    <td class="tbl-delivery-05-outside">
                                        <input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="hidden" /><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" size="1" hidden="hidden" />N/A</td>
                                    <td class="tbl-delivery-06-outside">
                                        <input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost" /></td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <tr class="item-supprow">
                                <td>
                                    <table id="tblSupplierNotes">
                                    </table>
                                </td>
                            </tr>

                        </td>
                    </tr>
                    <tr>
                        <td height="20px">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20px">&nbsp;</td>
        </tr>
        <tr>
            <td class="white-bg-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style" style="display: none;">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style">Promotional</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td>

                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                <tr>
                                    <td class="tbl04-promo00-style">X<br />
                                        &nbsp;</td>
                                    <td class="tbl04-promo01-style">ITEM DESCRIPTION<br />
                                        &nbsp;</td>

                                    <td class="tbl04-promo03-style">ITEM CODE<br />
                                        &nbsp;</td>

                                    <td class="tbl04-promo02-style">QTY<br />
                                        &nbsp;</td>
                                    <td class="tbl04-promo03-style">SHIPPING<br />
                                        INC GST</td>
                                    <td class="tbl04-promo04-style">UNIT PRICE<br />
                                        INC GST</td>
                                </tr>
                            </table>


                        </td>
                    </tr>
                    <tr class="Item_ProRow">
                        <td>

                            <table id="tblProItems" width="940" border="0" align="center" cellpadding="0" cellspacing="0">

                                <tr class="promo-row1">
                                    <td class="tbl04-promo-00-outside">&nbsp;</td>
                                    <td class="tbl04-promo-01-outside">
                                        <input type="text" name="promoitem" class="tbl04-promo-01-inside" id="promoitem" />
                                        <input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" size="1" hidden="hidden" />
                                    </td>
                                    <td class="tbl04-promo-03-outside">
                                        <input type="text" name="promoCode" id="promoCode" class="tbl04-promo-03-inside" /></td>
                                    <td class="tbl04-promo-02-outside">
                                        <input type="text" name="promoqty" class="tbl04-promo-02-inside" id="promoqty" /></td>
                                    <td class="tbl04-promo-03-outside">
                                        <input type="text" name="shippingCost" class="tbl04-promo-03-inside" id="shippingCost" /></td>
                                    <td class="tbl04-promo-04-outside">
                                        <input type="text" name="promocost" class="tbl04-promo-04-inside" id="promocost" /></td>
                                </tr>

                            </table>
                        </td>
                    </tr>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>

                <input type="button" value="ADD NEW ITEM" id="addnewProItem" class="add-btn" name="addnewProItem" />

            </td>
        </tr>
        <tr>
            <td height="20px">&nbsp;</td>
        </tr>
    </table>
    </td>
        </tr>
        <tr>
            <td height="20px">&nbsp;</td>
        </tr>
    <tr>
        <td class="white-bg-style">
            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                <tr>
                    <td height="20px">&nbsp;</td>
                </tr>

                <tr>
                    <td>

                        <table width="940" border="0" align="right" cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="tbl-totals-01">TOTAL EX GST</td>
                                <td class="tbl-totals-02">
                                    <div id="ProfitExGST"></div>
                                </td>
                                <!--Profit Total-->
                                <td class="tbl-totals-03">
                                    <div id="subtotal">$00.00</div>
                                </td>
                                <!--COG Total-->
                            </tr>
                            <tr>
                                <td class="tbl-totals-04">GST</td>
                                <td class="tbl-totals-05">
                                    <div id="profitGST"></div>
                                </td>
                                <td class="tbl-totals-06">
                                    <div id="gst">$00.00</div>
                                </td>
                            </tr>
                            <tr hidden="true">
                                <td width="760" bgcolor="#CCCCCC">Delivery Total</td>
                                <td bgcolor="#CCCCCC">&nbsp;</td>
                                <td bgcolor="#CCCCCC">
                                    <div id="deltotal">$00.00</div>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style6">TOTAL INC GST</td>
                                <td class="auto-style7">
                                    <div id="profitFullTotal"></div>
                                </td>
                                <!--Profit Sub Total-->
                                <td class="auto-style8">
                                    <div id="fulltotal">$00.00</div>
                                </td>
                                <!--COG sub Total-->
                            </tr>

                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="20px">&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>

    <tr>
        <td height="20px">&nbsp;</td>
    </tr>

    <tr>
        <td>
            <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">

                <tr>
                    <td width="748" class="comm-01-style"><span id="CD-accountownername"></span>COMMISION &nbsp; DEDUCTION</td>
                    <td class="comm-02-style">
                        <div id="totalprofit">$00.00</div>
                    </td>
                </tr>
                <tr id="TR_SalespersonCommission">
                    <td width="748" class="comm-01-style"><span id="CD-salespersonname"></span>COMMISION &nbsp; DEDUCTION</td>
                    <td class="comm-02-style">
                        <div id="totalprofitsalesperson">$00.00</div>
                    </td>
                </tr>

            </table>
        </td>
    </tr>
    <tr>
        <td height="20px">&nbsp;</td>
    </tr>
    <tr>
        <td class="white-bg-style">
            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="all-subheadings-style">Notes</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>

                        <asp:TextBox ID="OrderNotes" TextMode="multiline" class="notes-textbox-style" runat="server"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr hidden="hidden">
        <td class="auto-style9">
            <input runat="server" name="hdnCreditNoteContactDetails" id="hdnCreditNoteContactDetails" />
            &nbsp;
                <input runat="server" name="PROCOST" id="PROCOST" />
            &nbsp;
                <input id="TYPEOFCREDIT" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="align_right">
            <asp:Button ID="btnOrderSubmit" runat="server" Text="SAVE" OnClick="btnOrderSubmit_Click" class="submit-btn" ClientIDMode="Static" />
        </td>
    </tr>

    </table>

    <table id="Table1" width="980" border="0" align="center" cellpadding="0" cellspacing="0" runat="server">



        <tr class="item-supprow">
            <td>
                <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
                </table>
            </td>
        </tr>

        <tr hidden="hidden">
            <td height="20px">TEST ID
                    <input type="text" id="testid" runat="server" />
                SuppDelCost
                    <input type="text" id="hdnSupplierDeliveryCost" name="hdnSupplierDeliveryCost" runat="server" />
                CustDelCost
                    <input type="text" id="hdnCustomerDeliveryCost" name="hdnCustomerDeliveryCost" runat="server" />
                PromoCost
                    <input type="text" id="hdnProCost" name="hdnProCost" runat="server" />
                Profit
                    <input type="text" id="hdnProfit" name="hdnProfit" runat="server" />
                ContactID
                    <input type="text" id="hdnContactID" name="hdnContactID" runat="server" />
                CompanyID
                    <input type="text" id="hdnCompanyID" name="hdnCompanyID" runat="server" />
                Ordered Items
                    <input type="text" runat="server" name="OrderItems" id="OrderItems" />
                COG Total
                    <input type="text" runat="server" name="hdnCOGTotal" id="hdnCOGTotal" />
                COG SubTotal
                    <input type="text" runat="server" name="hdnCOGSubTotal" id="hdnCOGSubTotal" />
                Total
                    <input type="text" runat="server" name="hdnTotal" id="hdnTotal" />
                SubTotal
                    <input type="text" runat="server" name="hdnSubTotal" id="hdnSubTotal" />
                ProfitTotal
                    <input type="text" runat="server" name="ProfitTotal" id="ProfitTotal" />
                Edit Ordered Items
                    <input type="text" runat="server" name="hdnEditOrderItems" id="hdnEditOrderItems" />
                Edit Order
                    <input type="text" runat="server" name="hdnEditOrder" id="hdnEditOrder" />
                Suppl Del Cost Items
                    <input type="text" runat="server" name="hdnSupplierDelCostItems" id="hdnSupplierDelCostItems" />
                Promo Items
                    <input type="text" runat="server" name="hdnProItems" id="hdnProItems" />
                Cust Del Cost Items
                    <input type="text" runat="server" name="CusDelCostItems" id="CusDelCostItems" />
                Edit Supp Cost Iems
                    <input type="text" runat="server" name="EditSuppCostItems" id="EditSuppCostItems" />
                Edit Promo Items
                    <input type="text" runat="server" name="EditProItems" id="EditProItems" />
                Edit Cust Deli Cost Items
                    <input type="text" runat="server" name="CusDelCostItems" id="EditCusDelCostItems" />
                Supplier Notes
                    <input type="text" runat="server" name="hdnSupplierNotes" id="hdnSupplierNotes" />
                Edit Supplier Notes
                    <input type="text" runat="server" name="hdnEditSupplietNotes" id="hdnEditSupplietNotes" />
                Edit Promo Items
                    <input type="text" runat="server" name="hdnEditproitpems" id="hdnEditproitpems" />
                Promo Items
                    <input type="text" runat="server" name="hdnPromotionalItems" id="hdnPromotionalItems" />
                All Suppliers
                    <input type="text" runat="server" name="hdnAllSuppliers" id="hdnAllSuppliers" />
                Account Owner
                    <input type="text" name="hdnAccountOwner" id="hdnAccountOwner" runat="server" />
                Navigate URL
                    <input type="text" name="navigateURL" id="navigateURL" />
                Print Screen URL
                    <input type="text" name="printscreenURL" id="printscreenURL" />
                Order ID
                    <input type="text" name="hdnORDERID" id="hdnORDERID" runat="server" />
                Commission
                    <input type="text" name="hdnCommision" id="hdnCommision" runat="server" />
                Status
                    <input type="text" name="hdnSTATUS" id="hdnSTATUS" runat="server" />
                Notes
                    <input type="text" name="NOTES" id="NOTES" runat="server" />
                Temp ID
                    <input type="text" name="TempID" id="TempID" runat="server" />
                Order Status
                    <input type="text" name="ORDER_STATUS" id="ORDER_STATUS" runat="server" />
                Order Date
                    <input type="text" name="ORDER_DATE" id="ORDER_DATE" runat="server" />
                Order Create Date
                    <input type="text" name="ORDER_CREATE_DATE" id="ORDER_CREATE_DATE" runat="server" />
                Credit Note ID
                    <input type="text" name="hdnCreditNoteID" id="hdnCreditNoteID" runat="server" />
                AccountOwner
                    <input type="text" name="ACCOUNT_OWNER_TXT" id="ACCOUNT_OWNER_TXT" runat="server" />
                SalesPerson
                    <input type="text" name="SALESPERSON_TXT" id="SALESPERSON_TXT" runat="server" />
                AccountOwnerID
                    <input type="text" name="ACCOUNT_OWNER_ID" id="ACCOUNT_OWNER_ID" runat="server" />
                SalesPerson ID
                    <input type="text" name="SALESPERON_ID" id="SALESPERON_ID" runat="server" />
                AccountOwnerCommissionAmount
                    <input type="text" name="ACCOUNT_OWNER_COMMISH" id="ACCOUNT_OWNER_COMMISH" runat="server" />
                SalesPersonCommissionAmount
                    <input type="text" name="SALESPERSON_COMMISH" id="SALESPERSON_COMMISH" runat="server" />
                CommishSplit
                    <input type="text" name="COMMISH_SPLIT" id="COMMISH_SPLIT" runat="server" />
                VolumeSplitAmount
                    <input type="text" name="VOLUME_SPLIT_AMOUNT" id="VOLUME_SPLIT_AMOUNT" runat="server" />
            </td>

        </tr>

    </table>


    <div id="Dialog-Submit-Confirmation" title="SubmitConfirmation" style="display: none;">

        <table>
            <tr>
                <td>
                    <span id="SubmitMessage"></span>
                </td>
            </tr>

            <tr>

                <td>
                    <input type="button" id="btnReturnDashBoard" value="RETURN TO CREDIT NOTES" /></td>
                <td>
                    <input type="button" id="btnPrintOrder" name="btnPrintOrder" value="PRINT" style="display: none;" /></td>

            </tr>

        </table>

    </div>









    <div id="Dialog_TypeOfCredit" style="display: none;">

        <table>
            <tr>
                <td>TYPE OF CREDIT</td>
            </tr>

            <tr>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>
                    <select id="slTypeOfCredit" name="slTypeOfCredit">
                        <option value="0" selected="selected">-SELECT-</option>
                        <option value="1">FAULTY - NOT REG</option>
                          <option value="2">FAULTY - PRINT QUALITY</option>
                          <option value="3">FAULTY - DAMAGED</option>
                        <option value="4">WRONG GOODS - DELTONE FAULT</option>
                        <option value="5">WRONG GOODS - SUPPLIER FAULT</option>
                         <option value="13">WRONG GOODS - CUSTOMER FAULT</option>
                         <option value="14">SUPPLIER DOES NOT HAVE STOCK</option>
                        <option value="6">PRICE REDUCTION</option>
                        <option value="7">CHANGED PRINTER</option>
                        <option value="8">DID NOT ORDER</option>
                        <option value="9">LOST IN TRANSIT</option>
                        <option value="10">LIQUIDATION  </option>
                        <option value="11">UNECONOMICAL</option>
                       <option value="12">NO AUTH</option>
                        <option value="13">CHANGED MIND</option>
                    </select>

                </td>

            </tr>

        </table>

    </div>







</asp:Content>
