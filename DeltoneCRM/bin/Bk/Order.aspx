<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="DeltoneCRM.Order" MasterPageFile="~/Site1.Master" %>


<asp:Content ID="MainHeader" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.9.1.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>


    <script type="text/javascript">



        $(document).ready(function () {
            
            $('#<%=btnOrderSubmit.ClientID%>').click(function () {
               
                var myData = [];
                var ErrField = '';

                $('#tblLineItems tr').each(function (i, row) {

                    var $row = $(row);
                    myData.push($row.find('input[name*="ItemDesc"]').val());
                    myData.push($row.find('input[name*="suppliercode"]').val());
                    myData.push($row.find('input[name*="COG"]').val());
                    myData.push($row.find('input[name*="qty"]').val());
                    myData.push($row.find('input[name*="UnitPrice"]').val());
                    myData.push('|');
                   
                });
               
                //$('#ProfitExGST').html, $('#profitGST').html, $('#subtotal').html, $('#fulltotal').html, $('#totalprofit').html
                //thedeltotal = $('#deltotal').html().replace("$", "");

                


             

               
                 
            });
           
            $('#addnewitem').click(function ()
            {
                var $row = $('<tr class="item-row"><td><a class="delete" title="Remove row"><img src="/Images/x.png" width="16" height="16" /></a><label for="ItemDesc"></label><input type="text" name="ItemDesc" id="ItemDesc" class="tbx_item_description"></td><td><select name="oc" id="oc" class="tbx_o_c"><option value="Original">Original</option> <option value="Compatible">Compatible</option> </select></td><td><label for="suppliercode"></label><input type="text" name="suppliercode" id="suppliercode" class="tbx_supplier_code"></td> <td><label for="COG"></label><input type="text" name="COG" id="COG" class="tbx_cog"></td><td><label for="qty"></label><input type="text" name="qty" id="qty" class="tbx_qty"></td> <td><label for="UnitPrice"></label><input type="text" name="UnitPrice" id="UnitPrice" class="tbx_unit_price"></td><td class="total_outer" align="right"><span class="total">$00.00</span></td><td class="cogtotal_outer" align="right"><span class="cogtotal">$00.00</span></td><td><label for="hidden_item_code"></label><input type="text" name="hidden_item_code" id="hidden_item_code" hidden="true"></td><td><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                bind($row);
            });

            //Delete the Row
            $(document).on('click', '.delete', function () {
                var $tr = $(this).closest('.item-row');

                var name = $tr.find('input[name="hidden_Supplier_Name"]').val();
                if (CheckSupplierNameAssociation(name) > 1) {
                   
                }
                else
                {
                    $('#tblSupplierDeliveryCost tr').each(function () {

                        if ($(this).find('input[name="suppdeldet"]').val() == name) {
                            $(this).remove();
                        }
                    });
                    
                } 
                
                $tr.remove();
               
                update_total();

            });
            //End Delete row

            //This function checks SupplierName Associates with Other Rows
            function CheckSupplierNameAssociation(name)
            {
                var count = 0;
                $('#tblLineItems tr').each(function () {

                    if ($(this).find('input[name="hidden_Supplier_Name"]').val() == name) {
                        count++;
                    }
                });

                return count;
            }
            //End Function SupplierNameAssociation


            //Check wether Supplier Exsists or not
            function findRowExsists(name) {

                flag = false;
                $('#tblSupplierDeliveryCost tr').each(function () {

                    if ($(this).find('input[name="suppdeldet"]').val() == name) {
                        flag = true;
                    }
                });
                return flag;
            }
            //End Function Supplier Exsists or not

           
            //This function Bind the AutoCompletion and Updtate Total Cells
            function bind($row)
            {
                //Define the SupplierCost Row here 
              
                $row.find('input[name="ItemDesc"]').blur(function () {
                update_price();
                
                });

                //Trigger calculations for subsequent rows
             
                $row.find('input[name="suppliercode"]').blur(update_price);
                $row.find('input[name="qty"]').blur(update_price);
                $row.find('input[name="UnitPrice"]').blur(update_price);
                
                //Binding Auto Completion for the Row

                $row.find('#ItemDesc').autocomplete({
                    source: "/Fetch/FetchItembyInput.aspx",
                    focus: function (event, ui) {
                        $row.find('#COG').val(ui.item.COG);
                        $row.find('#suppliercode').val(ui.item.SupplierItemCode);
                        return false;
                    },
                    select: function (event, ui) {
                        $row.find('#ItemDesc').val(ui.item.Description);
                        $row.find('#suppliercode').val(ui.item.SupplierItemCode);
                        $row.find('#hidden_item_code').val(ui.item.ItemID);
                        //Set the Hidden supplier Name for the Row
                        $('#hdn_Supp_Name').val(ui.item.SupplierName);
                        $row.find('#hidden_Supplier_Name').val(ui.item.SupplierName);
                        
                        if (!(findRowExsists(ui.item.SupplierName)))
                        {
                            var $row_suppCost = $('<tr class="supp-del-row"><td><input name="suppdeldet" type="text" class="tbx_cust_delivery" id="suppdeldet"></td><td width="76" align="right" class="tbx_supp_delivery_cost_na"> N/A</td><td width="76" align="right"><input name="suppdelcost" type="text" class="tbx_supp_delivery_cost" id="suppdelcost"></td><td width="1"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"></td><td width="1"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /></td></tr>').insertAfter(".supp-del-row:last");
                            $row_suppCost.find('#suppdeldet').val(ui.item.SupplierName);
                            BindSupplierDelivery($row_suppCost);

                        }
                      
                        $row.find('#COG').val(ui.item.COG);
                        $row.find('#UnitPrice').val(ui.item.ManagerUnitPrice);
                        $row.find('#qty').val(1);
                        return false;
                    }

                }).data("ui-autocomplete")._renderItem = function (ul, item) {
                    return $("<li>")
                    .append("<a>" + item.Description + " - " + item.SupplierName + "</a>")
                    .appendTo(ul);
                };

            }
            //End Function Bind AutoCompletion and Uppate Cells


            //Supplier Delivery Cost Bind function 
            function BindSupplierDelivery($row)
            {
                $row.find('input[name="suppdelcost"]').blur(update_price);
            }
            //End Suypplier Delivery Cost Bind function here


            //Supplier
            $('#suppdeldet').autocomplete({
                source: "/Fetch/FetchSupplierFees.aspx",
                select: function (event, ui) {
                    $('#suppdelcost').val(ui.item.StandardDeliveryCost);
                    
                },
            });

            //Promotion Items 
            $('#promoitem').autocomplete({
                source: "/Fetch/FetchPromoItems.aspx",
                create: function () {
                    $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                        return $('<li>')
                        .append('<a>' + item.promodesc + '</a>')
                        .appendTo(ul);
                    }
                },
                select: function (event, ui) {
                    $('#promoitem').val(ui.item.promodesc);
                    $('#promocost').val(ui.item.promocost);
                    $('#promoqty').val(ui.item.qty);
                    $('#hidden_promo_item_id').val(ui.item.id);
                    $('#hidden_Supplier_Name').val(ui.item.SupplierName);
                    event.preventDefault();
                }
            });

            //Delivey Fees
            $('#deldet').autocomplete({
                source: "/Fetch/FetchDeliveryFees.aspx",
                create: function () {
                    $(this).data('ui-autocomplete')._renderItem = function (ul, item) {
                        return $('<li>')
                        .append('<a>' + item.deliverydetails + '</a>')
                        .appendTo(ul);
                    }
                },
                select: function (event, ui) {
                    $('#deldet').val(ui.item.deliverydetails);
                    $('#hidden_delivery_item_id').val(ui.item.id);
                    $('#delcost').val(ui.item.deliverycost);
                    event.preventDefault();
                }
            });

            //AutoCompletion for the First Item
            $('#ItemDesc').autocomplete({
                source: "/Fetch/FetchItembyInput.aspx", 
                focus:function(event,ui)
                {
                    $('#COG').val(ui.item.COG);
                    $('#suppliercode').val(ui.item.SupplierItemCode);
                    return false;
                },
                select:function(event,ui)
                {
                    $('#ItemDesc').val(ui.item.Description);
                    $('#suppliercode').val(ui.item.SupplierItemCode);
                    $('#hidden_item_code').val(ui.item.ItemID);
                    //Set the Hidden supplier Name
                    $('#hdn_Supp_Name').val(ui.item.SupplierName);
                    $('#hidden_Supplier_Name').val(ui.item.SupplierName);
                    $('#COG').val(ui.item.COG);
                    $('#UnitPrice').val(ui.item.ManagerUnitPrice);
                    $('#qty').val(1);
                    return false;
                }

            }).data( "ui-autocomplete" )._renderItem = function( ul, item ) {
                return $("<li>")
                .append("<a>" + item.Description + " - " + item.SupplierName + "</a>")
                .appendTo( ul );
            };
            //End Auto Completion Find Item
           
            //Trigger the Supplier Name in the SupplierDeliveryCost for the first time

            $('#ItemDesc').blur(function ()
            {
                //Set the Supplier Name
                $('#suppdeldet').val($('#hdn_Supp_Name').val());
                
            });
          
            //Trigger calculation for the first line item
            $('#ItemDesc').blur(update_price);
            $('#suppliercode').blur(update_price);
            $('#qty').blur(update_price);
            $('#UnitPrice').blur(update_price);
            $('#suppdelcost').blur(update_price);

            //Customer Delivery Cost update price 
            $('#delcost').blur(update_price);

            //Promotional Item Cost update price
            $('#promoqty').blur(update_price);

          
            // Calculate individual line total
            function update_price() {
                var newrow = $(this).parents('.item-row');
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            };

            //Calculate order total
            function update_total() {
                var subtotal = 0;
                var lineprofittotal = 0;


                $('.total').each(function (i) {
                    linesubtotal = $(this).html().replace("$", "");
                    if (!isNaN(subtotal)) subtotal += Number(linesubtotal);
                });

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

                //Add Supplier Delivery Cost here and Promotional Item here
                var SupplierDeliverCost = 0;
                $('#tblSupplierDeliveryCost tr').each(function () {

                    if (!isNaN(SupplierDeliverCost)) SupplierDeliverCost += Number($(this).find('input[name="suppdelcost"]').val());
                });

                //Add Delivery Cost to the COG Total
                lineprofittotal = parseFloat(lineprofittotal) + parseFloat(SupplierDeliverCost);

                //Add Promotional Item Cost here If any
                promotionalItemCost = parseFloat(Number($('#promocost').val()) * Number($('#promoqty').val()));

                lineprofittotal = parseFloat(lineprofittotal) + parseFloat(promotionalItemCost);

                //Profit Full Total
                fulltotal = parseFloat(subtotal) + parseFloat(gst);

                //Add Customer Delivery Cost
                customerDeliveryCost=Number($('#delcost').val());
                subtotal = parseFloat(subtotal) + parseFloat(Number($('#delcost').val()));


              


                    //Set the Hidden Field values 
                $('#<%= hdnSupplierDeliveryCost.ClientID %>').val(SupplierDeliverCost);
                //$('#hdnCustomerDeliveryCost').val(customerDeliveryCost);
                $('#<%=hdnCustomerDeliveryCost.ClientID %>').val(customerDeliveryCost);
                //$('#hdnProCost').val(promotionalItemCost);
                $('#<%=hdnProCost.ClientID %>').val(promotionalItemCost);

               



                  //End Setting the Hidden values
                

              
               

                //Profit Total Display
              
                $('#ProfitExGST').html("$" + parseFloat(subtotal).toFixed(2));
                $('#profitGST').html("$" + parseFloat(gst).toFixed(2));
                $('#profitFullTotal').html("$" + parseFloat(fulltotal).toFixed(2));

                //Cost of Good Total Display
                $('#subtotal').html("$" + parseFloat(lineprofittotal).toFixed(2));//Sub COG Total
                $('#gst').html("$" + parseFloat(gst_COG).toFixed(2));//GST COG 
                $('#fulltotal').html("$" + parseFloat(fulltotal_COG).toFixed(2));//FULL COG Total


                thedeltotal = $('#deltotal').html().replace("$", "");
              

                ordertotal = parseFloat(thedeltotal) + fulltotal;//Oreder COG Total

              
                profitTotal = parseFloat(subtotal - lineprofittotal);

                $('#ordertotal').html("$" + parseFloat(ordertotal).toFixed(2));//Ordet COG Total
                $('#totalprofit').html("$" + parseFloat(profitTotal).toFixed(2));//Profit Total

                //Set the Hidden Value for Profit Total
                  //$('#hdnProfit').val(profitTotal);
                $('#<%=hdnProfit.ClientID %>').val(profitTotal);
            };
        });
        //End Calculating Order Total

    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
<table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td height="50" bgcolor="#be9190"><table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td class="all_titles">NEW ORDER:</td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td height="25" bgcolor="#F7F5F2">&nbsp;</td>
        </tr>
        <tr>
          <td height="25" class="main_outline_top">&nbsp;</td>
        </tr>
        <tr>
          <td class="main_outline_middle"><table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td width="293" bgcolor="#F7F5F2" class="details_labels_01">CONTACT</td>
              <td>&nbsp;</td>
              <td width="293" bgcolor="#F7F5F2" class="details_labels_01">BILLING ADDRESS</td>
              <td>&nbsp;</td>
              <td width="293" bgcolor="#F7F5F2" class="details_labels_01">SHIPPING ADDRESS</td>
            </tr>
            <tr>
              <td width="293" class="details_labels_02"><div id="ContactInfo" runat="server"></div></td>
              <td>&nbsp;</td>
              <td width="293" class="details_labels_02"><div id="BillingAddress" runat="server"></div></td>
              <td>&nbsp;</td>
              <td width="293" class="details_labels_02"><div id="ShippingAddress" runat="server"></div></td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td height="25" class="main_outline_middle">&nbsp;</td>
        </tr>
        <tr>
          <td height="25" class="main_outline_middle">&nbsp;</td>
        </tr>
        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td width="50"></td>
              <td width="340" class="tbx_headings_left_01">ITEM DESCRIPTION</td>
              <td width="110" class="tbx_headings_left_02">O/G</td>
              <td width="110" class="tbx_headings_left_02">SUPPLIER CODE</td>
              <td width="80" class="tbx_headings_right">COG $</td>
              <td width="50" class="tbx_headings_right">QTY</td>
              <td width="80" class="tbx_headings_right">UNIT PRICE $</td>
              <td width="80" class="tbx_headings_right">TOTAL $</td>
              <td width="80" class="tbx_headings_right">COG TOTAL $</td>

            
              </tr>
            </table></td>
        </tr>
        <tr>
          <td><table id="tblLineItems" width="930" border="0" align="center" cellpadding="0" cellspacing="0" id="lineitems">
            <tr class="item-row">

              


              <td width="200"><label for="ItemDesc"></label>
                <input name="ItemDesc" type="text" class="tbx_item_description" id="ItemDesc"></td>
              <td width="100"><select name="oc" class="tbx_o_c" id="oc">
                <option value="Original">Original</option>
                <option value="Compatible">Compatible</option>
              </select></td>
              <td width="100"><label for="suppliercode"></label>
                <input name="suppliercode" type="text" class="tbx_supplier_code" id="suppliercode"></td>
              <td width="80"><label for="COG"></label>
                <input name="COG" type="text" class="tbx_cog" id="COG"></td>
              <td width="50"><label for="qty"></label>
                <input name="qty" type="text" class="tbx_qty" id="qty"></td>
              <td width="80"><label for="UnitPrice"></label>
                <input name="UnitPrice" type="text" class="tbx_unit_price" id="UnitPrice"></td>
              <td width="99" align="right" class="total_outer"><span class="total">$00.00</span></td>
              <td width="97" align="right" class="cogtotal_outer"><span class="cogtotal">$00.00</span></td>
              <td width="1"><label for="hidden_item_code"></label>

              <input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"></td>

              <!--Hidden SupplierName-->
                <td><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td>
              <!--End Hidden Supplier Name-->

            </tr>
          </table></td>
        </tr>
        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="5" class="order_last_line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
              <td><input name="addnewitem" type="button" class="btn_01" id="addnewitem" value="ADD NEW ITEM"></td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
        </tr>
        <tr>
          <td><table  width="929" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td width="769" class="tbx_headings_left_01">SUPPLIER DELIVERY COST</td>
              <td width="77" class="tbx_headings_right">&nbsp;</td>
              <td width="77" class="tbx_headings_right">COST $</td>
              </tr>
            </table></td>
        </tr>

        
        <tr class="item-supprow">
          <td>
               <table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                  <td>
                    <table id="tblSupplierDeliveryCost" width="930" border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr class="supp-del-row">
                        <td><input name="suppdeldet" type="text" class="tbx_cust_delivery" id="suppdeldet"></td>
                        <td width="76" align="right" class="tbx_supp_delivery_cost_na"> N/A</td>
                        <td width="76" align="right"><input name="suppdelcost" type="text" class="tbx_supp_delivery_cost" id="suppdelcost"></td>
                        <td width="1"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"></td>
                        <td width="1"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /></td>

                        </tr>
                      </table>
                  </td>
                  </tr>
              </table>
          </td>
        </tr>


        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="5" class="order_last_line">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
              <td><input name="addnewdeliverycost"  hidden="hidden" type="button" class="btn_01" id="addnewdeliverycost" value="ADD NEW DELIVERY COST"></td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
        </tr>
        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td width="769" class="tbx_headings_left_01">CUSTOMER DELIVERY COST</td>
              <td width="77" class="tbx_headings_right">COST $ </td>
              <td width="77" class="tbx_headings_right">&nbsp;</td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td><table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr class="del-row">
              <td><input name="deldet" type="text" class="tbx_cust_delivery" id="deldet"></td>
              <td align="right"><input name="delcost" type="text" class="tbx_cust_delivery_cost" id="delcost"></td>
              <td width="76" align="right" class="tbx_cust_delivery_cost_na">N/A</td>
              <td width="1"><input name="hidden_delivery_item_id" type="text" id="hidden_delivery_item_id" size="1" hidden="true"></td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td class="order_last_line">&nbsp;</td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr >
              <td>Promotion Item</td>
              <td>Cost</td>
              <td>Quantity</td>
              <td>&nbsp;</td>
            </tr>
            

            <tr class="promo-row">
              <td><input type="text" name="promoitem" id="promoitem"></td>
              <td><input type="text" name="promocost" id="promocost"></td>
              <td><input type="text" name="promoqty" id="promoqty"></td>
              <td><input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" hidden="true"></td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
        </tr>
    
        <tr>
          <td><table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td><table width="929" border="0" align="right" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="760" bgcolor="#FFFFFF" class="totals_labels_01">TOTAL EX GST $</td>
                  <td width="77" bgcolor="#F9D9DE" class="totals_01"><div id="ProfitExGST"></div></td>
                  <td width="77" bgcolor="#F2B0BA" class="totals_02"><div id="subtotal">$00.00</div></td>
                  </tr>
                <tr>
                  <td width="760" bgcolor="#CCCCCC" class="totals_labels_02">GST $</td>
                  <td bgcolor="#F9D9DE" class="totals_03"><div id="profitGST"></div></td>
                  <td bgcolor="#F2B0BA" class="totals_04"><div id="gst">$00.00</div></td>
                  </tr>
                <tr hidden="true">
                  <td width="760" bgcolor="#CCCCCC">Delivery Total</td>
                  <td bgcolor="#CCCCCC">&nbsp;</td>
                  <td bgcolor="#CCCCCC"><div id="deltotal">$00.00</div></td>
                  </tr>
                <tr>
                  <td width="760" bgcolor="#CCCCCC" class="totals_labels_03">TOTAL INC GST $</td>
                  <td bgcolor="#F9D9DE" class="totals_05"><div id="profitFullTotal"></div></td>
                  <td bgcolor="#F2B0BA" class="totals_06"><div id="fulltotal">$00.00</div></td>
                  </tr>
              </table></td>
              </tr>
          </table></td>
        </tr>
        <tr>
          <td height="25" align="center">&nbsp;</td>
        </tr>
        <tr>
          <td align="center"><table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td width="748" class="totals_profit_01">TOTAL PROFIT $</td>
              <td class="totals_profit_02"><div id="totalprofit">$00.00</div></td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
        </tr>
   
        <tr>
                <td><input type="hidden" id="hdnSupplierDeliveryCost" name="hdnSupplierDeliveryCost" runat="server" /></td>
                <td><input type="hidden" id="hdnCustomerDeliveryCost" name="hdnCustomerDeliveryCost" runat="server" /></td>
                <td><input type="hidden"  id="hdnProCost" name="hdnProCost"  runat="server"/></td>
                <td><input type="hidden" id="hdnProfit" name="hdnProfit"  runat="server"/></td>
                <td><input type="hidden" id="hdnContactID" name="hdnContactID" runat="server" /></td>
                <td><input type="hidden" runat="server" name="OrderItems" id="OrderItems" /></td>

                <td><input type="hidden" runat="server" name="hdnCOGTotal" id="hdnCOGTotal" /></td>
                <td><input type="hidden" runat="server" name="hdnCOGSubTotal" id="hdnCOGSubTotal" /></td>

                <td><input type="hidden" runat="server" name="hdnTotal" id="hdnTotal" /></td>
                <td><input type="hidden" runat="server" name="hdnSubTotal" id="hdnSubTotal" /></td>

                <td><input type="hidden" runat="server" name="ProfitTotal" id="ProfitTotal" /></td>

                <td colspan="2">
                       
                      <asp:Button ID="btnOrderSubmit" runat="server" Text="SUBMIT"  OnClick="btnOrderSubmit_Click" />
                          
                      
                </td>
          
        </tr>

      </table>

</asp:Content>
