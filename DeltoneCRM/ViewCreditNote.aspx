<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="ViewCreditNote.aspx.cs" Inherits="DeltoneCRM.ViewCreditNote" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <link href="css/jquery.dataTables_new.css" rel="stylesheet" />
     <script type="text/javascript">

     $(document).ready(function () 
     {

         //hdnCreditNoteID, hdnEditCreditNote, hdnEditCreditNoteItems
        if ($('#<%=hdnEditCreditNoteItems.ClientID%>').val() != '') {

            fillCreditNoteItems($('#<%=hdnEditCreditNoteItems.ClientID%>').val(), $('#<%=hdnEditCreditNote.ClientID%>').val());
         }

         //This function Popualte the Credit Note Items
         function fillCreditNoteItems(CreditNoteItems, CreditNote)
         {
             var arr_OrderItems = CreditNoteItems.split("|");
             var arr_CreditNote = CreditNote.split(":");
             var arr_FirstOrderItem = arr_OrderItems[0].split(":");
             //Fill Contact Details
             $('#<%=OrderTitle.ClientID%>').html("CREDIT NOTE: " + $('#<%=hdnCreditNoteID.ClientID%>').val());
             $('#<%=OrderNumber.ClientID%>').html("ORDER: " + arr_CreditNote[1]);
             $('#<%=ContactInfo.ClientID%>').html(arr_CreditNote[4]);
             $('#<%=CompanyInfo.ClientID%>').html(arr_CreditNote[5]);
             $('#<%=BillingAddress.ClientID%>').html(arr_CreditNote[7]);
             $('#<%=DateCreated.ClientID%>').html(arr_CreditNote[6]);


             $('#ItemDesc').val(arr_FirstOrderItem[1]);
             $('#suppliercode').val(arr_FirstOrderItem[4]);
             $('#COG').val(arr_FirstOrderItem[5]);
             $('#qty').val(arr_FirstOrderItem[2]);
             $('#UnitPrice').val(arr_FirstOrderItem[3]);
             update_price_firstrow();

             for (j = 1; j < arr_OrderItems.length; j++) {
                 if (arr_OrderItems[j]) {

                     var arr_OrderItem = arr_OrderItems[j].split(":");
                     var $row = $('<tr class="item-row"><td><label for="ItemDesc"></label><input type="text" name="ItemDesc" id="ItemDesc" class="tbx_item_description" disabled="disabled"></td><td><select name="oc" id="oc" class="tbx_o_c" disabled="disabled"><option value="Original">Original</option> <option value="Compatible">Compatible</option> </select></td><td><label for="suppliercode"></label><input type="text" name="suppliercode" id="suppliercode" class="tbx_supplier_code" disabled="disabled"></td> <td><label for="COG"></label><input type="text" name="COG" id="COG" class="tbx_cog" disabled="disabled"></td><td><label for="qty"></label><input type="text" name="qty" id="qty" class="tbx_qty" disabled="disabled"></td> <td><label for="UnitPrice"></label><input type="text" name="UnitPrice" id="UnitPrice" class="tbx_unit_price" disabled="disabled"></td><td class="total_outer" align="right"><span class="total">$00.00</span></td><td class="cogtotal_outer" align="right"><span class="cogtotal">$00.00</span></td><td><label for="hidden_item_code"></label><input type="text" name="hidden_item_code" id="hidden_item_code" hidden="true"></td><td><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                     $row.find('#ItemDesc').val(arr_OrderItem[1]);
                     $row.find('#suppliercode').val(arr_OrderItem[4]);
                     $row.find('#COG').val(parseFloat(arr_OrderItem[5]).toFixed(2));
                     $row.find('#qty').val(arr_OrderItem[2]);
                     $row.find('#UnitPrice').val(parseFloat(arr_OrderItem[3]).toFixed(2));
                     //$row.find('#hidden_item_code').val(arr_OrderItem[0]);
                     //$row.find('#hidden_Supplier_Name').val(arr_OrderItem[6]);
                     //Add Hidden Credit Item ID
                     update_price_row($row);
                 }
             }
         }

      function update_price_firstrow() {
               
                var price = ($('#qty').val()) * ($('#UnitPrice').val());
                $('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = ($('#qty').val()) * ($('#COG').val());
                $('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();

            }


            function update_price_row($row) {
                var newrow = $row;
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            };

       function update_price() 
       {

                var newrow = $(this).parents('.item-row');
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
       };

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

           var ProfitSubTotalExGST = parseFloat(Number(subtotal));
         
           var COGSubTotalExGST = parseFloat(Number(lineprofittotal));
        
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
           lineprofittotal = parseFloat(lineprofittotal);
           fulltotal = parseFloat(subtotal) + parseFloat(gst);
           subtotal = parseFloat(subtotal);
      
           //Profit Total Display

           $('#ProfitExGST').html("$" + parseFloat(ProfitSubTotalExGST).toFixed(2));
           $('#profitGST').html("$" + parseFloat(ProfitTotalGSTAmount).toFixed(2));
           $('#profitFullTotal').html("$" + parseFloat(TotalProfitINCGST).toFixed(2));

           //Cost of Good Total Display

           $('#subtotal').html("$" + parseFloat(COGSubTotalExGST).toFixed(2));
           $('#gst').html("$" + parseFloat(COGTotalGSTAmount).toFixed(2));//GST COG 
           $('#fulltotal').html("$" + parseFloat(TotalCOGINCGST).toFixed(2));

           ordertotal = fulltotal;
       };

    });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <table id="Table1" width="980" border="0" align="center" cellpadding="0" cellspacing="0"   runat="server" >
        <tr>
          <td height="50" bgcolor="#be9190"><table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td id="OrderTitle"  runat="server" class="all_titles">CREDIT NOTE:</td>
              <td id="OrderNumber" runat="server" class="all_titles" >ORDER:DTS</td>
            </tr>
           
          </table></td>
        </tr>
        <tr>
          <td height="25" bgcolor="#F7F5F2">&nbsp;</td>
        </tr>
        <tr>
          <td height="25" class="main_outline_top">&nbsp;</td>
        </tr>
        
        <!--Modification done Add Payemnt Terms-->
        <tr>
           <td>

                <asp:DropDownList ID="ddlPaymentTerms" runat="server" Width="200px" Visible="false">
                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                <asp:ListItem Text="21" Value="21" Selected="True"></asp:ListItem>
                <asp:ListItem Text="30" Value="30"></asp:ListItem>
                <asp:ListItem Text="45" Value="45"></asp:ListItem>

            </asp:DropDownList>
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              
               <asp:DropDownList ID="ddlUsers" runat="server" Width="100px" Visible="false">
                    </asp:DropDownList>
               
               <br />  
           </td>
           
        </tr>

        <!--End Modification Payemnt Terms-->

        <tr>
          <td class="main_outline_middle"><table width="930" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td width="293" bgcolor="#F7F5F2" class="details_labels_01"><span>CONTACT</span></td>
              <td width="293" bgcolor="#F7F5F2" class="details_labels_01" ><span>ORGANIZATION</span></td>
              <td width="293" bgcolor="#F7F5F2" class="details_labels_01"><span>BILLING ADDRESS</span></td>
              <td>&nbsp;</td>
              <td width="293" bgcolor="#F7F5F2" class="details_labels_01"  style:><span>DATE CREATED</span></td>
            </tr>
            <tr>
              <td width="293" class="details_labels_02"><div id="ContactInfo" runat="server"></div></td>
              <td width="293" class="details_labels_02" ><div id="CompanyInfo" runat="server"></div></td>
              <td width="293" class="details_labels_02"><div id="BillingAddress" runat="server"></div></td>
              <td>&nbsp;</td>
              <td width="293" class="details_labels_02"><div id="DateCreated" runat="server"></div></td>

             

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
            <tr class="item-row"    >

              <td><label for="ItemDesc"></label>
                <input name="ItemDesc" type="text" class="tbx_item_description" id="ItemDesc" disabled="disabled"/></td>
              <td width="100"><select name="oc" class="tbx_o_c" id="oc" disabled="disabled">
                <option value="Original" >Original</option>
                <option value="Compatible" >Compatible</option>
              </select></td>
              <td width="100"><label for="suppliercode"></label>
                <input name="suppliercode" type="text" class="tbx_supplier_code" id="suppliercode" disabled="disabled"></td>
              <td width="80"><label for="COG"></label>
                <input name="COG" type="text" class="tbx_cog" id="COG" disabled="disabled"></td>
              <td width="50"><label for="qty"></label>
                <input name="qty" type="text" class="tbx_qty" id="qty" disabled="disabled"></td>
              <td width="80"><label for="UnitPrice"></label>
                <input name="UnitPrice" type="text" class="tbx_unit_price" id="UnitPrice" disabled="disabled"></td>
              <td width="99" align="right" class="total_outer"><span class="total">$00.00</span></td>
              <td width="97" align="right" class="cogtotal_outer"><span class="cogtotal">$00.00</span></td>
              <td width="1"><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/></td>
              <td><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td>
            </tr>
          </table>
         
          </td>
        </tr>

        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="5" class="order_last_line" style="display:none">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
               <td><input name="addnewitem" type="button" class="btn_01" id="addnewitem" value="ADD NEW ITEM" style="display:none" /></td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
        </tr>
        <tr>
          <td><table  width="929" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none"  >
            <tr>
              <td width="769" class="tbx_headings_left_01">SUPPLIER DELIVERY COST</td>
              <td width="77" class="tbx_headings_right">&nbsp;</td>
              <td width="77" class="tbx_headings_right">COST $</td>
              </tr>
            </table></td>
        </tr>

       
        <tr class="item-supprow">
          <td>
               <table width="930" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none">
                    <tr>
                      <td>
                         <table id="tblSupplierDeliveryCost" width="930" border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr class="supp-del-row">
                                <td><input name="suppdeldet" type="text" class="tbx_cust_delivery" id="suppdeldet"  disabled="disabled"></td>
                                <td width="76" align="right" class="tbx_supp_delivery_cost_na"> N/A</td>
                                <td width="76" align="right"><input name="suppdelcost" type="text" class="tbx_supp_delivery_cost" id="suppdelcost"/></td>
                                <td width="1"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="hidden"/></td>
                                <td width="1"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /></td>

                                </tr>
                          </table>
                      </td>
                      </tr>
              </table>
          </td>
        </tr>


        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none">
            <tr>
              <td height="5" class="order_last_line" style="display:none">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
            </tr>
            <tr>
              <td><input name="addnewdeliverycost"  hidden="hidden" type="button" class="btn_01" id="addnewdeliverycost" value="ADD NEW DELIVERY COST"/></td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
        </tr>
        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none">
            <tr>
              <td width="769" class="tbx_headings_left_01">CUSTOMER DELIVERY COST</td>
              <td width="77" class="tbx_headings_right">COST $ </td>
              <td width="77" class="tbx_headings_right">&nbsp;</td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td><table width="930" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none">
            <tr class="del-row">
              <td><input name="deldet" type="text" class="tbx_cust_delivery" id="deldet"/></td>
              <td align="right"><input name="delcost" type="text" class="tbx_cust_delivery_cost" id="delcost"/></td>
              <td width="76" align="right" class="tbx_cust_delivery_cost_na">N/A</td>
              <td width="1"><input name="hidden_delivery_item_id" type="text" id="hidden_delivery_item_id" size="1" hidden="hidden"/></td>

                

            </tr>
          </table></td>
        </tr>
        <tr>
          <td><table width="929" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td class="order_last_line" style="display:none">&nbsp;</td>
            </tr>
          </table></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
        </tr>

         <tr>
           <td>
              <table width="930" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none">
                <tr >
                  <td><b>PROMOTION ITEM</b></td>
                  <td><b>COST</b></td>
                  <td><b>QUANTITY</b></td>
                  <td><b>SHIPPING COST</b></td>
                  <td>&nbsp;</td>
                </tr>
             </table>
           </td>
        </tr>

        <tr class="Item_ProRow">
            <td>

                <table id="tblProItems"   width="930" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none" >

                    <tr class="promo-row1">
                  
                          <td><input type="text" name="promoitem" id="promoitem"/></td>
                          <td><input type="text" name="promocost" id="promocost"/></td>
                          <td><input type="text" name="promoqty" id="promoqty"/></td>
                          <td><input type="text" name="shippingCost" id="shippingCost" /></td>
                          <td><input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" hidden="hidden"/></td>

                    </tr>

               </table>


            </td>
        </tr>


        <tr>
          <td>&nbsp;</td>
        </tr>

        <tr>
            <td><input type="button" value="ADD NEW" id="addnewProItem" name="addnewProItem" style="display:none" /></td>
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
                  <td width="77" bgcolor="#F9D9DE" class="totals_01"><div id="ProfitExGST"></div></td><!--Profit Sub Total-->
                  <td width="77" bgcolor="#F2B0BA" class="totals_02"><div id="subtotal">$00.00</div></td><!--COG sub Total-->
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
                  <td bgcolor="#F9D9DE" class="totals_05"><div id="profitFullTotal"></div></td><!--Profit Total-->
                  <td bgcolor="#F2B0BA" class="totals_06"><div id="fulltotal">$00.00</div></td><!--COG Total-->
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
              <td width="748" class="totals_profit_01" style="display:none">Commision $</td>
              <td class="totals_profit_02" style="display:none"><div id="totalprofit">$00.00</div></td>
              </tr>
            </table></td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
        </tr>

         <!--Supplier Notes Section-->
        
             <!--Supplier Notes heading-->
             <tr>
              <td><table  width="929" border="0" align="center" cellpadding="0" cellspacing="0" style="display:none">
                <tr>
                  <td width="769" class="tbx_headings_left_01">SUPPLIER NOTES</td>
                  <td width="77" class="tbx_headings_right">&nbsp;</td>
                  <td width="77" class="tbx_headings_right">&nbsp;</td>
                  </tr>
                </table></td>
            </tr>
            <!--End Supplier Notes heaing-->

             <tr  class="item-supprow">
                <td>
                    <table id="tblSupplierNotes">

                    </table>
                </td>
             </tr>
   

         <!--End Supplier Notes Section-->

        <tr>
                <td><input type="hidden" id="testid" runat="server" /></td>

                <td><input type="hidden" id="hdnSupplierDeliveryCost" name="hdnSupplierDeliveryCost" runat="server" /></td>
                <td><input type="hidden" id="hdnCustomerDeliveryCost" name="hdnCustomerDeliveryCost" runat="server" /></td>
                <td><input type="hidden"  id="hdnProCost" name="hdnProCost"  runat="server"/></td>
                <td><input type="hidden" id="hdnProfit" name="hdnProfit"  runat="server"/></td>
                <td><input type="hidden" id="hdnContactID" name="hdnContactID" runat="server" /></td>
                <td><input type="hidden" id="hdnCompanyID" name="hdnCompanyID" runat="server" /></td>
                <td><input type="hidden" runat="server" name="OrderItems" id="OrderItems" /></td>

                <td><input type="hidden" runat="server" name="hdnCOGTotal" id="hdnCOGTotal" /></td>
                <td><input type="hidden" runat="server" name="hdnCOGSubTotal" id="hdnCOGSubTotal" /></td>


                <td><input type="hidden" runat="server" name="hdnTotal" id="hdnTotal" /></td>
                <td><input type="hidden" runat="server" name="hdnSubTotal" id="hdnSubTotal" /></td>

                <td><input type="hidden" runat="server" name="ProfitTotal" id="ProfitTotal" /></td>


              
                <td><input type="hidden" runat="server" name="hdnEditOrderItems" id="hdnEditOrderItems" /></td>
                <td><input type="hidden" runat="server" name="hdnEditOrder" id="hdnEditOrder" /></td>

                <td><input type="hidden" runat="server" name="hdnSupplierDelCostItems"    id="hdnSupplierDelCostItems" /></td>
                
              
                <td><input type="hidden"  runat="server" name="hdnProItems"  id="hdnProItems" /></td>

                <td><input type="hidden" runat="server" name="CusDelCostItems"  id="CusDelCostItems" /></td>


                <td><input type="hidden" runat="server" name="EditSuppCostItems" id="EditSuppCostItems" /></td>
                <td><input type="hidden" runat="server" name="EditProItems" id="EditProItems" /></td>
                <td><input type="hidden" runat="server" name="CusDelCostItems" id="EditCusDelCostItems" /></td>
                
                <td><input type="hidden"  runat="server"  name="hdnSupplierNotes" id="hdnSupplierNotes" /></td>

                <td><input type="hidden"  runat="server" name="hdnEditSupplietNotes" id="hdnEditSupplietNotes" /></td>
                <td><input type="hidden" runat="server" name="hdnEditproitpems" id="hdnEditproitpems" /></td>

                
                <td><input type="hidden" runat="server" name="hdnPromotionalItems" id="hdnPromotionalItems" /></td>


                <td><input type="hidden" runat="server" name="hdnCreditNoteContactDetails" id="hdnCreditNoteContactDetails" /></td>


                <td><input type="hidden" runat="server" name="hdnCreditNoteID" id="hdnCreditNoteID" /></td>
                <td><input type="hidden" runat="server" name="hdnEditCreditNoteItems" id="hdnEditCreditNoteItems" /></td>
                <td><input type="hidden" runat="server" name="hdnEditCreditNote" id="hdnEditCreditNote" /></td>
                <td><input type="hidden" runat="server" name="hdnEditContactDetails" id="hdnEditContactDetails" /></td>
              
                
                <td >
                      <asp:Button ID="btnOrderSubmit" runat="server" Text="CLOSE"  OnClick="btnOrderSubmit_Click"  ClientIDMode="Static"/>            
                </td>
                 <td >
                      <asp:Button ID="btnPublish" runat="server" Text="Approve"  OnClick="btnPublish_Click"  ClientIDMode="Static" Visible="false"/>            
                </td>

                <td>
                     <asp:Button ID="btnInvoiceApprove" runat="server" Text="Approve"   Visible="false"/>
                </td>
                  
        </tr>

      </table>

</asp:Content>
