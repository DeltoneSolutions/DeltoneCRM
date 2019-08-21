<%@ Page Language="C#" MasterPageFile="~/NoNav.Master" validateRequest="false" AutoEventWireup="true" CodeBehind="RepAllocateQuoteDisplay.aspx.cs" Inherits="DeltoneCRM.RepAllocateQuoteDisplay" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/CreditNote.css" rel="stylesheet" type="text/css" />
     <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <link href="css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="js/jquery-1.11.1.min.js"></script>
    <link href="css/NewCSS.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.js"></script>

    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.dataTables_new.css" />

    <%-- <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jscolor.js"></script>
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />

    <!--Jquery UI References-->
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <style type="text/css">
          body {
            background-color:#C0C0C0 !important;
        }
    </style>
    <script type="text/javascript">

        //Submit Dialog for message confirmation
        function SubmitDialog(Message, url, PrintScreenUrl) {
            $('#SubmitMessage').html(Message);
            //Set the Navogation URL
            $('#navigateURL').val(url);
            //Set the Print Screen URL
            $('#printscreenURL').val(PrintScreenUrl);


            Dialog = $('#Dialog-Submit-Confirmation').dialog({
                resizable: false,
                modal: true,
                title: 'DELTONECRM - NOTIFICATION MESSAGE',
                height: 400,
                width: 710
            });

            $('#Dialog-Submit-Confirmation').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
                //location.reload();
            });

            return false;
        }
        // End of Submit Dialog

        $(document).ready(function () {



           

            var now = new Date();

            var day = ("0" + now.getDate()).slice(-2);
            var month = ("0" + (now.getMonth() + 1)).slice(-2);

            var today = now.getFullYear() + "-" + (month) + "-" + (day);

            $('#<%=datereceived.ClientID%>').val(today);

            //Print Order Click Event
            $('#btnPrintOrder').click(function () {

                if ($('#printscreenURL').val()) {
                    window.open($('#printscreenURL').val(), '_blank', "location=no");
                }
            });


            $.widget("custom.catcomplete", $.ui.autocomplete, {
                _create: function () {
                    this._super();
                    this.widget().menu("option", "items", "> :not(.ui-autocomplete-category)");
                },
                _renderMenu: function (ul, items) {
                    var that = this,
                        currentCategory = "";
                    $.each(items, function (index, item) {
                        var li;
                        if (item.SupplierName != currentCategory) {
                            ul.append("<li class='ui-autocomplete-category'>" + item.SupplierName + "</li>")
                            currentCategory = item.SupplierName;
                        }
                        li = that._renderItemData(ul, item);
                        if (item.SupplierName) {
                            li.attr("aria-label", item.SupplierName + " : " + item.Description);
                        }
                    });
                },
                _renderItem: function (ul, item) {
                    return $("<li>")
                    .addClass(item.SupplierName)
                    .attr("data-value", item.ItemID)
                    .append($("<a>").text(item.Description + " - " + item.OEMCode))
                    .appendTo(ul);
                }
            });

            // Create New Line Item when 'Add New Item' is clicked
            $('#addnewitem').click(function () {
                //Validation Check wether First Item has a value if yes then add 

                if (!($('#ItemDesc').val() == '')) {
                    var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                    bind($row);
                }
                else {
                    alert('FIRST LINE ITEM MUST CONTAIN A VALUE');
                }
            });

            //PrintOrder Click Event
            $('#btnPrint').click(function () {
                var url = "PrintQuote.aspx?Oderid=" + $('#<%=hdnORDERID.ClientID%>').val() + "&cid=" + $('#<%=hdnContactID.ClientID%>').val() + "&Compid=" + $('#<%=hdnCompanyID.ClientID%>').val() + "&ExistingCustomer=" + $('#<%=ExistingCustomer.ClientID%>').val();
                window.open(url, '_blank');
            });

            //This function Bind the AutoCompletion and Updtate Total Cells
            function bind($row) {
                //Define the SupplierCost Row here 
                $row.find('input[name="ItemDesc"]').blur(function () {
                    update_price();
                    SuppDelTable();
                    update_price_row($row);
                });

                //Trigger calculations for subsequent rows
                $row.find('input[name="suppliercode"]').blur(update_price);
                $row.find('input[name="qty"]').blur(update_price);
                $row.find('input[name="COG"]').blur(update_price);
                $row.find('input[name="UnitPrice"]').blur(update_price);
                $row.find('input[name="ItemDesc"]').blur(update_price);
                //Binding Auto Completion for the Row

                $.widget("custom.catcomplete", $.ui.autocomplete, {
                    _create: function () {
                        this._super();
                        this.widget().menu("option", "items", "> :not(.ui-autocomplete-category)");
                    },
                    _renderMenu: function (ul, items) {
                        var that = this,
                            currentCategory = "";
                        $.each(items, function (index, item) {
                            var li;
                            if (item.SupplierName != currentCategory) {
                                ul.append("<li class='ui-autocomplete-category'>" + item.SupplierName + "</li>")
                                currentCategory = item.SupplierName;
                            }
                            li = that._renderItemData(ul, item);
                            if (item.SupplierName) {
                                li.attr("aria-label", item.SupplierName + " : " + item.Description);
                            }
                        });
                    },

                    _renderItem: function (ul, item) {
                        return $("<li>")
                        .addClass(item.SupplierName)
                        .attr("data-value", item.ItemID)
                        .append($("<a>").text(item.Description + " - " + item.OEMCode))
                        .appendTo(ul);
                    }
                });

                $row.find('#ItemDesc').catcomplete({
                    source: "Fetch/FetchItembyInput.aspx",
                    delay: 0,
                    focus: function (event, ui) {
                        $row.find('#COG').val(ui.item.COG);
                        $row.find('#suppliercode').val(ui.item.SupplierItemCode);
                        $row.find('#ItemType').val(ui.item.SupplierName);
                        return false;
                    },
                    select: function (event, ui) {
                        $row.find('#ItemDesc').val(ui.item.Description);
                        $row.find('#suppliercode').val(ui.item.SupplierItemCode);
                        $row.find('#hidden_item_code').val(ui.item.ItemID);
                        //Set the Hidden supplier Name for the Row
                        $('#hdn_Supp_Name').val(ui.item.SupplierName);
                        $row.find('#hidden_Supplier_Name').val(ui.item.SupplierName);
                        $row.find('#ItemType').val(ui.item.SupplierName); //Add Supplier Name



                        if (!(findRowExsists(ui.item.SupplierName))) {
                            SuppDelTable();
                        }

                        $row.find('#COG').val(parseFloat(ui.item.COG).toFixed(2));  //MODIFIED HERE  ROUND
                        $row.find('#UnitPrice').val(parseFloat(ui.item.ManagerUnitPrice).toFixed(2)); //MODIFIED HERE ROUND
                        $row.find('#qty').val(1);
                        return false;
                    }

                });

            }
            //End Function Bind AutoCompletion and Uppate Cells

            //Return to DashBoard Click Event
            $('#btnReturnDashBoard').click(function () {
                window.location.href = $('#navigateURL').val();
            });

            //This function checks wether Supplier Exsists or not
            function IsSupplierExsists(suppname) {
                var count = 0;
                $('#tblLineItems tr').each(function (i, row) {

                    var $row = $(row);

                    if ($row.find('input[name*="hidden_Supplier_Name"]').val() == suppname) {
                        count++;
                    }
                });

                return count;

            }
            //End Function Checkes Supplier Exsists or not

            //Populate the Supplier Notes Table according to Actions AutoPopulate ItemDesc,Add New Row,Delete a Row
            function PopulateSupplierNotesTable() {

                var Snotes = '';
                //Capture the Previous Values
                $('#tblSupplierNotes tr').each(function (i, row) {

                    var $notes_row = $(row);
                    Snotes = Snotes + $notes_row.find('#suppName').val() + ":" + $notes_row.find('#taSuppNotes').val();
                    Snotes = Snotes + "|";
                });

                $('#tblSupplierNotes').html(''); //Clear Previous Results

                //Repopulate the Table According to the SupplierDelivery Table
                $('#tblSupplierDeliveryCost tr').each(function (i, row) {
                    var $row = $(row);
                    var sname = $row.find('input[name*="suppdeldet"]').val();
                    //Populate the SupplierNotes Table
                    $row = $('<tr class="supp-notes-row"><td><input id="suppName" name="suppName" type="text" disabled="disabled" /></td><td><textarea id="taSuppNotes"  name="taSuppNotes"  style="width:780px; height:100px;"  ></textarea></td><td><input type="hidden" name="hdnSuppID" id="hdnSuppID" /></td></tr>');
                    $('#tblSupplierNotes').append($row);
                    $row.find('#suppName').val(sname);

                    /*Enable enter key in text areas*/


                    /*End of enabling enter key*/

                    var Items = Snotes.split('|');
                    for (i = 0; i < Items.length; i++) {
                        if (Items[i]) {
                            var Item = Items[i].split(':');
                            if ($row.find('#suppName').val() == Item[0]) {
                                $row.find('#taSuppNotes').val(Item[1]);//Supplier Notes
                            }
                        }
                    }

                });

            }
            //End Populating Supplier Notes Table

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

            /*Function Reposible of repopulation of Supplier Delivery Tables*/
            function SuppDelTable() {

                Suppliers = $(' #<%=hdnAllSuppliers.ClientID %>').val();
                var arr_Suppliers = Suppliers.split(':');

                //Fetch Exsisting Delivery Cost
                var DelCost = '';
                $('#tblSupplierDeliveryCost tr').each(function (i, row) {

                    var $row = $(row);
                    var sname = $row.find('input[name*="suppdeldet"]').val();
                    var sCost = $row.find('input[name*="suppdelcost"]').val();

                    DelCost = DelCost + sname + ":" + sCost + "|";

                });


                //End Fetching Delivery Cost
                $('#tblSupplierDeliveryCost').html('');
                var Count = 0;

                for (k = 0; k < arr_Suppliers.length; k++) {
                    if (arr_Suppliers[k] != '') {
                        if (IsSupplierExsists(arr_Suppliers[k]) > 0) {
                            var RowDelCost = '';
                            //Preserve the Previous Delivery Cost
                            if (DelCost) {
                                var CostItems = DelCost.split('|');
                                for (j = 0; j < CostItems.length; j++) {
                                    if (CostItems[j] != '') {
                                        var item = CostItems[j].split(':');
                                        if (arr_Suppliers[k] == item[0]) {
                                            RowDelCost = item[1];
                                        }
                                    }
                                }

                            }

                            var clientfunction = "LoadSuppNote('" + arr_Suppliers[k] + "');";
                            var $SuppDelCost_Row = $('<tr class="supp-del-row"><td class="tbl-delivery-01-outside-2nd"><input name="suppdeldet" type="text" disabled="disabled" class="tbl-delivery-01-inside" id="suppdeldet" value=' + arr_Suppliers[k] + '></td><td align="right" class="tbl-delivery-05-outside-2nd"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" />N/A</td><td align="right" class="tbl-delivery-06-outside-2nd"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost_' + k + '" value="' + RowDelCost + '"></td></tr>');
                            $('#tblSupplierDeliveryCost').append($SuppDelCost_Row);

                        }

                    }

                }

                //MODIFIED HERE APPLY BIND FUNCTION TO ALL DELIVERY ROWS
                $('#tblSupplierDeliveryCost tr').each(function (i, row) {
                    var $row_test = $(row);

                    $row_test.find('input[name*="suppdelcost"]').bind('blur', function () {
                        if (isNaN($row_test.find('input[name*="suppdelcost"]').val())) {
                            $row_test.find('input[name*="suppdelcost"]').val("0.00"); //SET TO DEFAULT VALUE
                        }
                        update_price();
                    });

                });
                //END MODIFICATION BIND BLUR FUNCTIONS 


                //Populate the Supplier Notes Table
                //   PopulateSupplierNotesTable();

            }
            //End Function Repopulation of Supplier Delivery Table

            function fillOrderItems(OrderItems, Order, SuppNotes) {

                //Modification done here Set the OrderDate
                //var today = now.getFullYear() + "-" + (month) + "-" + (day);

                $('#<%=datereceived.ClientID%>').val($('#<%=ORDER_DATE.ClientID%>').val());
                //Modified here Add Order Created Date
                $('#<%=datecreated.ClientID%>').val($('#<%=ORDER_CREATE_DATE.ClientID%>').val());



                var arr_OrderItems = OrderItems.split("|");
                var arr_Order = Order.split(":");

                //Modification done 1/5/2015  SupplierNotes Population no need while creating quote

                //var arr_suppnotes = SuppNotes.split("|");
                //for (i = 0; i < arr_suppnotes.length; i++) {
                //    if (arr_suppnotes[i]) {
                //        var note = arr_suppnotes[i].split(':');
                //        $row = $('<tr class="supp-notes-row"><td><input id="suppName" name="suppName" type="text" disabled="disabled" /></td><td><textarea id="taSuppNotes"  name="taSuppNotes" style="width:780px; height:100px;"></textarea></td><td><input type="hidden" name="hdnSuppID" id="hdnSuppID" /></td></tr>');
                //        $('#tblSupplierNotes').append($row);
                //        $row.find('#suppName').val(note[0]);
                //        $row.find('#taSuppNotes').val(note[1]);
                //    }

                //}

                //End SupplierNotes 

                //For the First Item Fill the Table First Row
                var arr_FirstOrderItem = arr_OrderItems[0].split(",");

                $('#ItemDesc').val(arr_FirstOrderItem[1]);
                $('#suppliercode').val(arr_FirstOrderItem[4]);
                $('#COG').val(parseFloat(arr_FirstOrderItem[3]).toFixed(2));
                $('#qty').val(arr_FirstOrderItem[5]);
                $('#UnitPrice').val(parseFloat(arr_FirstOrderItem[2]).toFixed(2));
                $('#hidden_item_code').val(arr_FirstOrderItem[0]);
                $('#hidden_Supplier_Name').val(arr_FirstOrderItem[6]);
                //Modified here Add Supplier Name instead of Original/Compatible
                $('#ItemType').val(arr_FirstOrderItem[6]);


                //Update the Price for the First Row
                update_price_firstrow();

                //For the Susquent Elements 
                for (j = 1; j < arr_OrderItems.length; j++) {
                    if (arr_OrderItems[j]) {

                        var arr_OrderItem = arr_OrderItems[j].split(",");
                        var $row = $('<tr class="item-row"><td class="tbl-auto-row-01-btm"><a class="delete" title="Remove row"><img src="Images/x.png" width="16" height="16" /></a></td><td class="tbl-auto-row-02-btm"><label for="ItemDesc"></label><input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td><td class="tbl-auto-row-03-btm"><label for="suppliercode"><input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td><td class="tbl-auto-row-04-btm"><label for="suppliercode"></label><input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td><td class="tbl-auto-row-05-btm"><label for="COG"></label><input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td><td class="tbl-auto-row-06-btm"><label for="qty"></label><input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td><td class="tbl-auto-row-07-btm"><label for="UnitPrice"></label><input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td><td align="right" class="tbl-auto-row-08-btm"><span class="total">$00.00</span></td><td align="right" class="tbl-auto-row-09-btm"><span class="cogtotal">$00.00</span><label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/><input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" /></td></tr>').insertAfter(".item-row:last");
                        $row.find('#ItemDesc').val(arr_OrderItem[1]);
                        $row.find('#suppliercode').val(arr_OrderItem[4]);
                        $row.find('#COG').val(parseFloat(arr_OrderItem[3]).toFixed(2));
                        $row.find('#qty').val(arr_OrderItem[5]);
                        $row.find('#UnitPrice').val(parseFloat(arr_OrderItem[2]).toFixed(2));
                        $row.find('#hidden_item_code').val(arr_OrderItem[0]);
                        $row.find('#hidden_Supplier_Name').val(arr_OrderItem[6]);
                        //Modified here Add Supplier Name instead of Original/Compatible
                        $row.find('#ItemType').val(arr_OrderItem[6]);


                        //Update the Price for that Row 
                        update_price_row($row);
                        //Bind Row Functonalities for Austopopulations ,prince updation ,etc
                        bind($row);
                    }

                }
                //End populating for Susquent Elements

                //EditSuppCostItems,EditProItems,EditCusDelCostItems
                var SuppDelCostItems = arr_Order[0];
                var ProItems = arr_Order[1];
                var CusDelCostItems = arr_Order[2];

                //Set Customer DelCost Items

                var arr_CusDelCost = arr_Order[2].split("|");

                //Customer Delivery Cost 
                $('#deldet').val(arr_CusDelCost[0]);
                //Modification done here format delivery cost for 2 decimal places.
                $('#delcost').val(arr_CusDelCost[1]);

                //Set Promotional Cost Items

                var arr_ProitemCost = ProItems.split("|");

                //Populate Promotional Items  
                if ($('#<%=hdnEditproitpems.ClientID %>').val()) {
                    //populateProItems($('#<%=hdnEditproitpems.ClientID %>').val());
                }

                //Supplier Delivery Cost Population
                arr_SuppDelCost = SuppDelCostItems.split("|");
                //For the Fist Row
                var arr_SuppDelCost_FirstRow = arr_SuppDelCost[0].split(",");

                $('#suppdeldet').val(arr_SuppDelCost_FirstRow[0]);
                $('#suppdelcost').val(arr_SuppDelCost_FirstRow[1]);

                //For SubSquent SuppDelCost Rows 
                for (k = 1; k < arr_SuppDelCost.length; k++) {
                    if (arr_SuppDelCost[k]) {

                        var arr_SuppDelCost_Row = arr_SuppDelCost[k].split(',');
                        var $SuppDelCost_Row = $('<tr class="supp-del-row"><td><input name="suppdeldet" disabled="disabled" type="text" class="tbx_cust_delivery" id="suppdeldet"></td><td width="76" align="right" class="tbx_supp_delivery_cost_na"> N/A</td><td width="76" align="right"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"></td><td width="1"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="true"></td><td width="1"><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" hidden="hidden" /></td></tr>').insertAfter(".supp-del-row:last");
                        $SuppDelCost_Row.find('#suppdeldet').val(arr_SuppDelCost_Row[1]);
                        $SuppDelCost_Row.find('#suppdelcost').val(arr_SuppDelCost_Row[2]);


                        //Modification done here 
                        $SuppDelCost_Row.find('#suppdelcost').blur(function () {

                            if (isNaN($SuppDelCost_Row.find('#suppdelcost').val())) {
                                $SuppDelCost_Row.find('#suppdelcost').val("0.00");
                            }
                        });

                    }
                }
            }

            //Edit Functionalities
            if ($('#<%=hdnEditOrderItems.ClientID%>').val() != '') {


                //Display the Print Button
                //$('#btnPrint').show();

                fillOrderItems($('#<%=hdnEditOrderItems.ClientID%>').val(), $('#<%=hdnEditOrder.ClientID%>').val(), $('#<%=hdnEditSupplietNotes.ClientID%>').val());
                update_price();

            }
            //End Edit functionalities

            //This Function Update price and Total for the First Row
            function update_price_firstrow() {
                var price = ($('#qty').val()) * ($('#UnitPrice').val());
                $('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = ($('#qty').val()) * ($('#COG').val());
                $('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();

            }

            //This function Update price and Total for row Given by row
            function update_price_row($row) {
                var newrow = $row;
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            };

            //End Function Update price and Total for row given by row 

            // Triggers for the ItemDescription field

            $('#ItemDesc').catcomplete({
                source: "Fetch/FetchItembyInput.aspx",
                delay: 0,
                focus: function (event, ui) {
                    $('#COG').val(ui.item.COG);
                    $('#suppliercode').val(ui.item.SupplierItemCode);
                    $('#ItemType').val(ui.item.SupplierName);
                    return false;
                },
                select: function (event, ui) {
                    $('#ItemDesc').val(ui.item.Description);
                    $('#suppliercode').val(ui.item.SupplierItemCode);
                    $('#hidden_item_code').val(ui.item.ItemID);
                    $('#hdn_Supp_Name').val(ui.item.SupplierName); //Test
                    $('#hidden_Supplier_Name').val(ui.item.SupplierName);
                    $('#COG').val(parseFloat(ui.item.COG).toFixed(2));   //MODIFIED HERE ROUND 
                    $('#UnitPrice').val(parseFloat(ui.item.ManagerUnitPrice).toFixed(2)); // MODIFIED HERE ROUND
                    $('#qty').val(1);
                    $('#ItemType').val(ui.item.SupplierName);
                    //Modified Here Add Supplier Name instead of Item Type
                    //  PopulateSupplierNotesTable();
                    return false;
                }

            });

            $('#ItemDesc').blur(update_price);
            $('#COG').blur(update_price);
            $('#UnitPrice').blur(update_price);
            $('#qty').blur(update_price);


            // End Triggers for ItemDescription field

            // Function to update the pricing
            function update_price() {


                var newrow = $(this).parents('.item-row');
                var price = newrow.find('input[name="qty"]').val() * newrow.find('input[name="UnitPrice"]').val();
                newrow.find('.total').html("$" + parseFloat(price).toFixed(2));
                var cogprice = newrow.find('input[name="qty"]').val() * newrow.find('input[name="COG"]').val();
                newrow.find('.cogtotal').html("$" + parseFloat(cogprice).toFixed(2));
                update_total();
            };
            //End Function to update pricing

            //Calculate order total
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

                //Add Supplier Delivery Cost here and Promotional Item here
                var SupplierDeliverCost = 0;
                $('#tblSupplierDeliveryCost tr').each(function () {

                    if (!isNaN(SupplierDeliverCost)) SupplierDeliverCost += Number($(this).find('input[name="suppdelcost"]').val());
                });

                //Supplier Del Cost without GST

                SuppDelCostExGST = parseFloat(SupplierDeliverCost).toFixed(2);

                //Customer Delivery Cost without GST
                CusDelCostEXGST = parseFloat((Number($('#delcost').val()))).toFixed(2);

                //Modified here Add promotionl item Cost
                var promoItemCost = 0;
                $('#tblProItems tr').each(function () {

                    if (!isNaN(promoItemCost)) {

                        promoItemCost = promoItemCost + parseFloat(Number($(this).find('#promocost').val()) * Number($(this).find('#promoqty').val())) + parseFloat(Number($(this).find('#shippingCost').val()));
                    }

                });

                ProItemCost = (parseFloat(promoItemCost) / 1.1);
                //End Calculating Promotional item Cost

                //Profit Sub Total ExGST

                var ProfitSubTotalExGST = ((parseFloat(Number(subtotal) + Number(CusDelCostEXGST)).toFixed(2)) / 1.1);

                //COG SubTotal EXGST

                var COGSubTotalExGST = parseFloat(Number(lineprofittotal) + Number(SuppDelCostExGST) + Number(ProItemCost)).toFixed(2);

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
                lineprofittotal = parseFloat(lineprofittotal) + parseFloat(SupplierDeliverCost);

                //Add Promotional Item Cost here If any
                promotionalItemCost = parseFloat(Number($('#promocost').val()) * Number($('#promoqty').val()));

                lineprofittotal = parseFloat(lineprofittotal) + parseFloat(promotionalItemCost);

                //Profit Full Total
                fulltotal = parseFloat(subtotal) + parseFloat(gst);

                //Add Customer Delivery Cost
                customerDeliveryCost = Number($('#delcost').val());
                subtotal = parseFloat(subtotal) + parseFloat(Number($('#delcost').val()));

                //Set the Hidden Field values 
                $('#<%=hdnSupplierDeliveryCost.ClientID %>').val(SupplierDeliverCost);
                $('#<%=hdnCustomerDeliveryCost.ClientID %>').val(customerDeliveryCost);
                $('#<%=hdnProCost.ClientID %>').val(promotionalItemCost);
                //End Setting the Hidden values

                //Profit Total Display

                $('#ProfitExGST').html("$" + parseFloat(ProfitSubTotalExGST).toFixed(2));
                $('#profitGST').html("$" + parseFloat(ProfitTotalGSTAmount).toFixed(2));
                $('#profitFullTotal').html("$" + parseFloat(TotalProfitINCGST).toFixed(2));

                //Cost of Good Total Display

                $('#subtotal').html("$" + parseFloat(COGSubTotalExGST).toFixed(2));
                $('#gst').html("$" + parseFloat(COGTotalGSTAmount).toFixed(2));//GST COG 
                $('#fulltotal').html("$" + parseFloat(TotalCOGINCGST).toFixed(2));

                thedeltotal = $('#deltotal').html().replace("$", "");

                ordertotal = parseFloat(thedeltotal) + fulltotal;//Oreder COG Total
                $('#ordertotal').html("$" + parseFloat(ordertotal).toFixed(2));//Ordet COG Total

                var repCommission = 0;

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
                        var Commission = (parseFloat(exgsttotal) * CommishPerc).toFixed(2);

                        $('#totalprofit').html("$" + parseFloat(Commission).toFixed(2))//Comission 
                        $('#<%=hdnCommision.ClientID%>').val(parseFloat(Commission).toFixed(2));
                    },
                    error: function (message) {
                        alert('Unable to retrieve commission for logged user. Please contact your administartor');
                    }
                });



            };
            //End Total Calculations

            $('#<%=btnOrderSubmit.ClientID%>').click(function () {

                var myData = [];
                var ErrField = '';
                var OrderItems = '';

                $('#tblLineItems tr').each(function (i, row) {

                    var $row = $(row);
                    myData.push($row.find('input[name*="hidden_item_code"]').val());
                    myData.push($row.find('input[name*="ItemDesc"]').val());
                    myData.push($row.find('input[name*="suppliercode"]').val());
                    myData.push($row.find('input[name*="COG"]').val());
                    myData.push($row.find('input[name*="qty"]').val());
                    myData.push($row.find('input[name*="UnitPrice"]').val());
                    myData.push($row.find('input[name*="hidden_Supplier_Name"]').val());
                    //Modified here SupplieName 
                    myData.push($row.find('input[name*="ItemType"]').val());
                    myData.push("|");

                });

                //Populate the OrderItems 

                for (i = 0; i < myData.length; i++) {
                    OrderItems = OrderItems + myData[i];
                }

                $('#<%=OrderItems.ClientID%>').val(myData);

                //End Writing the Order Items

                //Total Profit
                $('#<%=hdnProfit.ClientID %>').val($('#totalprofit').html().replace("$", ""));

                //Profit Total  for the Customer Invoice
                $('#<%=hdnSubTotal.ClientID %>').val($('#ProfitExGST').html().replace("$", ""));
                $('#<%=hdnTotal.ClientID %>').val($('#profitFullTotal').html().replace("$", ""));

                //COG Total for the Supplier Invoice
                $('#<%=hdnCOGSubTotal.ClientID %>').val($('#subtotal').html().replace("$", ""));
                $('#<%=hdnCOGTotal.ClientID %>').val($('#fulltotal').html().replace("$", ""));


                //Supplier Delivery Cost Items 
                var SuppDelItems = [];
                $('#tblSupplierDeliveryCost tr').each(function (i, row) {
                    var $row = $(row);

                    SuppDelItems.push($row.find('input[name*="suppdeldet"]').val());
                    SuppDelItems.push($row.find('input[name*="suppdelcost"]').val());
                    SuppDelItems.push("|");
                });


                $('#<%=hdnSupplierDelCostItems.ClientID%>').val(SuppDelItems);



                //Promotional Items
                $('#<%=hdnProItems.ClientID%>').val($('#promoitem').val() + "|" + $('#promocost').val() + "|" + $('#promoqty').val());

                //Cmustomer Delivery Cost Items

                $('#<%=CusDelCostItems.ClientID%>').val($('#deldet').val() + "|" + $('#delcost').val());


                //Supplier Notes Section
                var SuppNotes = [];
                $('#tblSupplierNotes tr').each(function (i, row) {

                    var $row_Notes = $(row);
                    SuppNotes.push($row_Notes.find('input[name*="suppName"]').val());
                    SuppNotes.push($row_Notes.find('#taSuppNotes').val());
                    SuppNotes.push("|");
                });

                $('#<%=hdnSupplierNotes.ClientID%>').val(SuppNotes);
                //End Supplier Notes Section


                function getProItemCount() {
                    flag = false;
                    var Count = 0;
                    $('#tblProItems tr').each(function (i, row) {
                        var $row_ProItems = $(row);
                        Count++;
                    });

                    if (($('#promoitem').val() == '') && (Count == 1)) {
                        flag = true; //No Promotional Items
                    }

                    return flag;

                }

                //Promotional Items Section
                var ProItems = [];
                $('#tblProItems tr').each(function (i, row) {

                    var $row_ProItems = $(row);

                    ProItems.push($row_ProItems.find('#promoitem').val());
                    ProItems.push($row_ProItems.find('#promocost').val());
                    ProItems.push($row_ProItems.find('#promoqty').val());
                    ProItems.push($row_ProItems.find('#shippingCost').val());
                    ProItems.push($row_ProItems.find('#promoCode').val());

                    ProItems.push('|');
                });
                //End Promotional Items Section

                if (!getProItemCount()) {
                    $('#<%=hdnPromotionalItems.ClientID%>').val(ProItems);
            }

            });


            CallBackDateChange();
            var isVisible = '<%= IsVisibleDateCallBack %>';
            if (isVisible)
                showCallBackdate();


            //Delete the Row
            $(document).on('click', '.delete', function () {
                var $tr = $(this).closest('.item-row');

                //  var name = $tr.find('input[name="hidden_Supplier_Name"]').val();

                //if (CheckSupplierNameAssociation(name) > 1) {

                //}
                //else {
                //    $('#tblSupplierDeliveryCost tr').each(function () {

                //        if ($(this).find('input[name="suppdeldet"]').val() == name) {
                //            $(this).remove();
                //            PopulateSupplierNotesTable();
                //        }
                //    });

                //}

                $tr.remove();
                update_total();

            });

        });

        function CallBackDateChange() {

            $("#ddlquoteCategory").change(function () {

                var firstDropVal = $('#ddlquoteCategory').val();

                if (firstDropVal == "1")  //call back
                {
                    //$("#callbackCalendarSpanbutton").show();
                    $("#callbackCalendarSpan").show();
                    $("#callbackCalendarSpanDate").show();

                }
                else {

                    $("#callbackCalendarSpan").hide();
                    $("#callbackCalendarSpanDate").hide();
                    // $("#callbackCalendarSpanbutton").hide();
                }
            });
        }
        function showCallBackdate() {

            $("#callbackCalendarSpan").show();
            $("#callbackCalendarSpanDate").show();

            $("#callbackCalendarSpanbutton").show();

        }

        function Validate() {

            var firstDropVal = $('#ddlquoteCategory').val();
            if (firstDropVal == "1")  //call back
            {
                var valu = $('#ContentPlaceHolder1_callbackDate').val();
                if (valu == "") {
                    alert("Please select callback date");
                    return false;
                }
            }

            var result = confirm("Do you want to continue?");
            if (result) {
                return true;
            }
            else
                return false;
        }


        function confirmBox() {

            var result = confirm("Do you want to continue?");
            if (result) {
                return true;
            }
            else
                return false;
        }



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:ScriptManager ID="ScriptManagerQuotesAllocrre" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>

      <table align="center" cellpadding="0" cellspacing="0" class="btm_01">
            <tr>
            <td height="25px">
                 <asp:Button OnClick="btnaccountDash_Click" ID="Buttonaccount" Text="EVENTS" ForeColor="Blue" Width="10%"
                    runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />

                <asp:Button OnClick="btnupload_Click" ID="btnDashboard" Text="DASHBOARD" ForeColor="Blue" Width="10%"
                    runat="server" CssClass="buttonClass moverigRight" CausesValidation="false" />
            </td>
        </tr>
        <tr>
            <td>

                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
        <tr>
            <td class="auto-style1"></td>
        </tr>
        <tr>
            <td id="OrderTitle" runat="server" class="all-headings-style" >ALLOCATED QUOTE :<asp:Label ID="laId" runat="server"></asp:Label></td>
            <td id="QorN" ></td>   
        </tr>
        <tr>
            <td><asp:Label ID="messageLabel" runat="server" ForeColor="Blue"></asp:Label></td>
        </tr>
        <tr>
            <td>
                <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                    <tr>
                        <td class="white-box-outline-top" style="vertical-align:top;">
                            <table align="center" cellpadding="0" cellspacing="0" class="width-770-style">
                                <tr>
                                    <td height="20px">&nbsp;</td>
                                </tr>
                                
                                <tr>
                                    <td class="auto-style3">
                                        <table align="left" cellpadding="0" cellspacing="0" class="width-470-style">
                                            <tr>
                                                <td class="company-name-style" height="30px"><div id="CompanyNameDIV" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-style"><div id="ContactInfo" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-style"><div id="StreetAddressLine1" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-style"><div id="StreetAddressLine2" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-style"><div id="stateDiv" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-tel-style"><div id="ContactandEmail" runat="server"></div></td>
                                            </tr>

                                        </table>
                                    </td>
                                    <td class="">
                                        <table align="center" cellpadding="0" cellspacing="0" class="auto-style4"  style="display:none;">
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
                                                <td class="company-details-delivery-style"><div id="DeliveryContact" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-delivery-style"><div id="DeliveryCompany" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-delivery-style"><div id="DeliveryAddressLine1" runat="server"></div></td>
                                            </tr>
                                            <tr>
                                                <td class="company-details-delivery-style"><div id="DeliveryAddressLine2" runat="server"></div></td>
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
                            <br />
                            <br />
                            <table class="width-770-style"
                                <tr>
                                    <td>&nbsp;</td>

                                </tr>
                                <tr>
                                    <td>&nbsp;</td>

                                </tr>
                                <tr>
                                    <td>
                                        <table style="width:100%;">
                                            <tr><td style="width:10px">&nbsp;</td>
                                                <%--<td style="width:243px">
                                                    <asp:Button ID="editcontactButton" runat="server"  
                                                    class="add-credit-note-btn" Text="EDIT CONTACT" OnClientClick="return openDialogCs();" /></td>--%>

                                                 <td style="width:243px">
                                                    <input type="button"
                                                    class="add-credit-note-btn" value="EDIT CONTACT" onclick="return openDialogCs();" /></td>
                                                
                                                <td style="width:10px">&nbsp;</td>
                                                <td style="width:243px"> <asp:Button ID="btnConvertToOrder" OnClientClick="return CreateAddWindow();" runat="server"  class="add-credit-note-btn" Text=" CONVERT TO ORDER " /></td>
                                                <td style="width:10px">&nbsp;</td>
                                                <td style="width:243px"><asp:Button ID="btnCancelQuote" runat="server"  class="add-credit-note-btn" OnClientClick="return confirmBox();" Text=" CANCEL QUOTE " OnClick="btnCancelQuote_Click" /></td>
                                                <td style="width:10px">&nbsp;</td>
                                                <td style="width:243px"><asp:Button ID="btnResendEmail" runat="server"  class="add-credit-note-btn" OnClientClick="return confirmBox();"
                                                    Text=" RESEND QUOTE EMAIL " OnClick="btnResendQuoteEmail_Click" /></td>
                                                <td style="width:10px">&nbsp;</td>
                                                <td style="width:243px"><asp:Button ID="btnApproveQuote" runat="server" OnClientClick="return confirmBox();"
                                                     class="add-credit-note-btn" Text=" APPROVE QUOTE " OnClick="btnApproveQuote_Click" /></td>
                                                <td style="width:10px">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>


                                </tr>
                            </table>
                        </td>
                        <td width="20px">&nbsp;</td>
                        <td width="150px" class="align-vertical">
                            <table align="center" cellpadding="0" cellspacing="0" class="auto-style5">
                                <tr>
                                    <td class="top-payment-terms-heading">&nbsp;&nbsp;&nbsp;&nbsp;PAYMENT TERMS</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="payment-terms-drop"
                                             AutoPostBack="false" ClientIDMode="Static">
                                        <asp:ListItem Text="21 days" Value="21" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="30 days" Value="30"></asp:ListItem>
                                        <asp:ListItem Text="45 days" Value="45"></asp:ListItem>

                            </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td height="16px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td height="16px" class="top-payment-terms-heading">&nbsp;&nbsp;&nbsp;&nbsp;TYPE OF CALL</td>
                                </tr>
                                <tr>
                                    <td height="16px">
                                        <asp:DropDownList ID="dllTypeOfCall" runat="server" style="width:100%;" CssClass="payment-terms-drop">
                                            <asp:ListItem Selected="True">Cold Call</asp:ListItem>
                                            <asp:ListItem>Referral</asp:ListItem>
                                            <asp:ListItem>Call In</asp:ListItem>
                                            <asp:ListItem>Website</asp:ListItem>
                                            <asp:ListItem>Area</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>

                                    <td height="16px">
                                        </td>
                                </tr>
                                 <tr>
                                    <td height="16px" class="top-payment-terms-heading">&nbsp;&nbsp;&nbsp;&nbsp;Quote Category</td>
                                </tr>
                                <tr>
                                    <td height="16px">
                                        <asp:DropDownList ID="ddlquoteCategory" runat="server" ClientIDMode="Static" style="width:100%;" CssClass="payment-terms-drop">
                                          
                                            <asp:ListItem Text="New" Value="0" ></asp:ListItem>
                                             <asp:ListItem Text="Call Back" Value="1"></asp:ListItem>
                                             <asp:ListItem Text="May Be" Value="2"></asp:ListItem>
                                             <asp:ListItem Text="Sold" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>

                                    <td height="16px">
                                        </td>
                                     
                                </tr>
                                 <tr id="callbackCalendarSpan" style="display:none;">
                                    <td height="16px" class="top-payment-terms-heading">&nbsp;&nbsp;&nbsp;&nbsp;Call Back DATE

                                        
                                    </td>
                                     
                                    
                                </tr>
                                <tr id="callbackCalendarSpanDate" style="display:none;">
                                    <td height="16px">
                                        <input name="callbackDate" type="date" class="FormDateValues" id="callbackDate"   value="" runat="server" style="width:95%" />
                                    </td>
                                </tr>

                                 <tr>
                                     <br />
                                    <td height="16px">
                                       
                                        </td>
                                     <tr id="callbackCalendarSpanbutton" style="display:none;">
                                       <td >
                                         <input  type="button" id="btn_schedulteEventUpdate" style="margin-left: 20px;color:red;" onclick="openDialog();" value="Schedule Event" />
                                        `</td> 

                                     </tr>
                                </tr>
                               
                                    
                                <tr>

                                    <td height="16px">
                                        `</td>
                                </tr>
                                <tr>
                                    <td height="16px" class="top-payment-terms-heading">&nbsp;&nbsp;&nbsp;&nbsp;QUOTE DATE</td>
                                </tr>
                                <tr>
                                    <td height="16px">
                                        <input name="datereceived" type="date" class="FormDateValues" id="datereceived"  value="" runat="server" style="width:95%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td height="16px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="top-payment-terms-heading">&nbsp;&nbsp;&nbsp;&nbsp;QUOTE CREATED BY</td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlUsers" runat="server" CssClass="account-owner-drop">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td  id="tdOrderCreateDate" class="top-payment-terms-heading" runat="server" style="display:none;">&nbsp;&nbsp;&nbsp;&nbsp;QUOTE CREATED DATE</td>
                                </tr>
                                
                                <tr>
                                    <td height="16px">
                                        <input name="datecreated" type="date" class="FormDateValues" id="datecreated"   disabled="disabled" value="" runat="server"  style="display:none;"/>
                                    </td>
                                </tr>

                               
                                <tr>
                                    <td height="16px" class="top-payment-terms-heading">
                                        &nbsp;&nbsp;&nbsp;&nbsp;ACCOUNT OWNER</td>
                                </tr>

                               
                                <tr>
                                    <td height="16px">
                                        <asp:TextBox ID="AccountOwnertxt" ReadOnly="true" runat="server" Width="95%"></asp:TextBox>
                                    </td>
                                </tr>

                               
                                <tr>
                                    <td height="16px">
                                        &nbsp;</td>
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

            <tr>
                <td class="section_headings">ALL QUOTES</td>
            </tr>

            <tr>
                <td class="white-bg-style">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">

                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table class="display" id="QuotesTblRep">
                                    <thead>
                                        <tr>
                                            <th>QUOTE ID</th>
                                            <th>CREATED DATE</th>
                                            <th>CONTACT NAME</th>
                                            <th>QUOTE TOTAL</th>
                                            <th>STATUS</th>
                                              <th>CREATED BY</th>
                                            <th>VIEW</th>
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
                     <tr>
            <td height="20px">&nbsp;</td>
        </tr>

        <tr>
            <td height="20px" class="white-bg-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style">Email Header</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="txt_EmailHeader" cols="20" name="S1" rows="8" style="width:100%;" runat="server"></textarea></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
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
<td class="tbl-auto-row-01">&nbsp;</td>
              <td class="tbl-auto-row-02"><label for="ItemDesc"></label>
                <input name="ItemDesc" type="text" class="tbl-auto-row-02-inside" id="ItemDesc"></td>
              <td class="tbl-auto-row-03"><label for="suppliercode">
                <input name="ItemType" type="text" class="tbl-auto-row-03-inside" id="ItemType"></td>
              <td class="tbl-auto-row-04"><label for="suppliercode"></label>
                <input name="suppliercode" type="text" class="tbl-auto-row-04-inside" id="suppliercode"></td>
              <td class="tbl-auto-row-05"><label for="COG"></label>
                <input name="COG" type="text" class="tbl-auto-row-05-inside" id="COG"></td>
              <td class="tbl-auto-row-06"><label for="qty"></label>
                <input name="qty" type="text" class="tbl-auto-row-06-inside" id="qty"></td>
              <td class="tbl-auto-row-07"><label for="UnitPrice"></label>
                <input name="UnitPrice" type="text" class="tbl-auto-row-07-inside" id="UnitPrice"></td>
              <td align="right" class="tbl-auto-row-08"><span class="total">$00.00</span></td>
              <td align="right" class="tbl-auto-row-09"><span class="cogtotal">$00.00</span>
                <label for="hidden_item_code"></label><input name="hidden_item_code" type="text" id="hidden_item_code" size="1" hidden="hidden"/>
                  <input type="hidden" name="hidden_Supplier_Name" id="hidden_Supplier_Name"  value="" />
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

                            <input name="addnewitem" type="button" class="add-btn" id="addnewitem" value="ADD NEW ITEM"/>

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
                        <td class="all-subheadings-style">Delivery</td>
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
                    <input name="deldet" type="text" class="tbl-delivery-01-inside" id="deldet"/>
                </td>
                <td class="tbl-delivery-02-outside">
                    <input name="delcost" type="text" class="tbl-delivery-02-inside" id="delcost"/>
                </td>
                <td class="tbl-delivery-03-outside">
                    N/A<input name="hidden_delivery_item_id" type="text" id="hidden_delivery_item_id" size="1" hidden="hidden"/>
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
                                <td class="tbl-delivery-04-outside"><input name="suppdeldet" type="text" class="tbl-delivery-04-inside" id="suppdeldet"  disabled="disabled"></td>
                                <td class="tbl-delivery-05-outside"><input name="hidden_supp_delivery_item_id" type="text" id="hidden_supp_delivery_item_id" size="1" hidden="hidden"/><input name="hdn_Supp_Name" type="text" id="hdn_Supp_Name" size="1" hidden="hidden" />N/A</td>
                                <td class="tbl-delivery-06-outside"><input name="suppdelcost" type="text" class="tbl-delivery-06-inside" id="suppdelcost"/></td>
                                </tr>

                          </table>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <tr  class="item-supprow">
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
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style" style="display:none;">
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

                                    <td class="tbl04-promo03-style">ITEM CODE<br />&nbsp;</td>

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

                             <table id="tblProItems"   width="940" border="0" align="center" cellpadding="0" cellspacing="0" >

                                    <tr class="promo-row1">
                                        <td class="tbl04-promo-00-outside">&nbsp;</td>
                                        <td class="tbl04-promo-01-outside"><input type="text" name="promoitem" class="tbl04-promo-01-inside" id="promoitem"/>
                                            <input type="text" name="hidden_promo_item_id" id="hidden_promo_item_id" size="1" hidden="hidden"/>
                                        </td>
                                        <td class="tbl04-promo-03-outside"><input type="text" name="promoCode" id="promoCode" class="tbl04-promo-03-inside" /></td>
                                        <td class="tbl04-promo-02-outside"><input type="text" name="promoqty" class="tbl04-promo-02-inside" id="promoqty"/></td>
                                        <td class="tbl04-promo-03-outside"><input type="text" name="shippingCost" class="tbl04-promo-03-inside" id="shippingCost" /></td>  
                                        <td class="tbl04-promo-04-outside"><input type="text" name="promocost" class="tbl04-promo-04-inside" id="promocost"/></td>
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
                  <td class="tbl-totals-02"><div id="ProfitExGST"></div></td><!--Profit Total-->
                  <td class="tbl-totals-03"><div id="subtotal">$00.00</div></td><!--COG Total-->
                  </tr>
                <tr>
                  <td class="tbl-totals-04">GST</td>
                  <td class="tbl-totals-05"><div id="profitGST"></div></td>
                  <td class="tbl-totals-06"><div id="gst">$00.00</div></td>
                  </tr>
                <tr hidden="true">
                  <td width="760" bgcolor="#CCCCCC">Delivery Total</td>
                  <td bgcolor="#CCCCCC">&nbsp;</td>
                  <td bgcolor="#CCCCCC"><div id="deltotal">$00.00</div></td>
                  </tr>
                <tr>
                  <td class="auto-style6">TOTAL INC GST</td>
                  <td class="auto-style7"><div id="profitFullTotal"></div></td><!--Profit Sub Total-->
                  <td class="auto-style8"><div id="fulltotal">$00.00</div></td><!--COG sub Total-->
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
            <td height="20px">

                &nbsp;</td>
        </tr>

        <tr>
            <td><table width="980" border="0" align="center" cellpadding="0" cellspacing="0">
                          
                <tr>
              <td width="748" class="comm-01-style">COMMISSION</td>
              <td class="comm-02-style"><div id="totalprofit">$00.00</div></td>
              </tr>

            </table></td>
        </tr>
        <tr>
            <td height="20px">&nbsp;</td>
        </tr>
        <tr>
            <td height="20px" class="white-bg-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style">Email Footer</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="txt_EmailFooter"  cols="20" name="S2" rows="8" style="width:100%" runat="server"></textarea></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>

            </td>
        </tr>
        <tr>
            <td height="20px">&nbsp;</td>
        </tr>
        <tr style="display:none;">
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
      <tr>
          <td class="white-bg-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                        <td class="all-subheadings-style">NOTES HISTORY</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <div id="displaynotesHistoryDiv" runat="server" style=" border:1px solid gray; font: medium -moz-fixed; font: -webkit-small-control;
    height: 120px;
    overflow: auto;
    padding: 2px;
    resize: both;
    width: 950px;word-wrap:break-word;display:inline-block;"></div>
                        </td>
                    </tr>
     </table>
            </td>
     
        </tr>


    <tr style="display:none;">
            <td class="white-bg-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style">What Happened</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                                     <asp:TextBox ID="sellstatuswhathappened" TextMode="multiline" class="notes-textbox-style" runat="server"></asp:TextBox>

                                 </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>

     <tr runat="server" visible="false" id="allocatequoteNotes">
            <td class="white-bg-style">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="all-subheadings-style">Allocated Rep Notes</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                                     <asp:TextBox ID="TextBoxNoteAllocatedQuote" Visible="false" TextMode="multiline" class="notes-textbox-style" runat="server"></asp:TextBox>

                                
                            <div id="allocatedReptoes" runat="server" style=" border:1px solid gray; font: medium -moz-fixed; font: -webkit-small-control;
    height: 120px;
    overflow: auto;
    padding: 2px;
    resize: both;
    width: 950px;word-wrap:break-word;display:inline-block;"></div>
                             </td>
                    </tr>
                    <tr>
                        <td> 
                             <input  type="button" id="btn_schedulteEventtw" style="color:red;width:200px;height:30px;" onclick="openDialog();" value="CREATE NOTE" />
                            <input type="button" value="Save Notes" style="width:120px;border: 1.0px solid red;background-color: white;cursor: pointer;display:none;" onclick="return SaveMe();" /> </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; </td>
        </tr>

    <tr>
        <td>&nbsp;</td>
    </tr>
        <tr>
            <td class="align_right" style="display:none;">
                <asp:Button ID="btnOrderSubmit" runat="server" Text="SAVE"  OnClick="btnOrderSubmit_Click"  class="submit-btn" ClientIDMode="Static" OnClientClick="return Validate();"  />   
                <asp:Button ID="btnCloseOrderWindow"   runat="server" Text="CLOSE" CssClass="submit-btn"    ClientIDMode="Static"  OnClick="btnCloseOrderWindow_Click" />  
                
                <input type="button"  id="btnPrint" name="btnPrint" value="PRINT"  class="submit-btn"  />
                <input type="button" id="btnCancelInvoice" name="btnCancelInvoice" value="CANCEL ORDER"
                     class="submit-btn" style="display:none;" runat="server" />
            </td>
        </tr>

    </table>

<table id="Table1" width="980" border="0" align="center" cellpadding="0" cellspacing="0"   runat="server" >
        

       
        <tr class="item-supprow">
          <td>
               <table width="980" border="0" align="center" cellpadding="0" cellspacing="0">

              </table>
          </td>
        </tr>

        <tr hidden="hidden">
                <td height="20px">
                    <input type="text" id="testid" runat="server" /> 
                    <input type="text" id="hdnSupplierDeliveryCost" name="hdnSupplierDeliveryCost" runat="server" />
                    <input type="text" id="hdnCustomerDeliveryCost" name="hdnCustomerDeliveryCost" runat="server" />
                    <input type="text"  id="hdnProCost" name="hdnProCost"  runat="server"/>
                    <input type="text" id="hdnProfit" name="hdnProfit"  runat="server"/>
                    <input type="text" id="hdnContactID" name="hdnContactID" runat="server" />
                    <input type="text" id="hdnCompanyID" name="hdnCompanyID" runat="server" />
                    <input type="text" runat="server" name="OrderItems" id="OrderItems" />
                    <input type="text" runat="server" name="hdnCOGTotal" id="hdnCOGTotal" />
                    <input type="text" runat="server" name="hdnCOGSubTotal" id="hdnCOGSubTotal" />
                    <input type="text" runat="server" name="hdnTotal" id="hdnTotal" />
                    <input type="text" runat="server" name="hdnSubTotal" id="hdnSubTotal" />
                    <input type="text" runat="server" name="ProfitTotal" id="ProfitTotal" />
                    <input type="text" runat="server" name="hdnEditOrderItems" id="hdnEditOrderItems" />
                    <input type="text" runat="server" name="hdnEditOrder" id="hdnEditOrder" />
                    <input type="text" runat="server" name="hdnSupplierDelCostItems"    id="hdnSupplierDelCostItems" />
                    <input type="text"  runat="server" name="hdnProItems"  id="hdnProItems" />
                    <input type="text" runat="server" name="CusDelCostItems"  id="CusDelCostItems" />
                    <input type="text" runat="server" name="EditSuppCostItems" id="EditSuppCostItems" />
                    <input type="text" runat="server" name="EditProItems" id="EditProItems" />
                    <input type="text" runat="server" name="CusDelCostItems" id="EditCusDelCostItems" />
                    <input type="text"  runat="server"  name="hdnSupplierNotes" id="hdnSupplierNotes" />
                    <input type="text"  runat="server" name="hdnEditSupplietNotes" id="hdnEditSupplietNotes" />
                    <input type="text" runat="server" name="hdnEditproitpems" id="hdnEditproitpems" />
                    <input type="text" runat="server" name="hdnPromotionalItems" id="hdnPromotionalItems" />
                    <input type="text" runat="server" name="hdnAllSuppliers" id="hdnAllSuppliers" />
                    <input type="text" name="hdnAccountOwner" id="hdnAccountOwner"  runat="server" /> 
                    <input type="text" name="navigateURL" id="navigateURL" />
                    <input type="text" name="printscreenURL" id="printscreenURL" />
                    <input type="text" name="hdnORDERID" id="hdnORDERID" runat="server" />
                    <input type="text" name="hdnCommision" id="hdnCommision" runat="server" readonly="true" />
                    <input type="text" name="hdnSTATUS" id="hdnSTATUS"  runat="server" />
                    <input type="text" name="NOTES" id="NOTES"  runat="server"/>
                    <input type="text" name="TempID" id="TempID" runat="server" />
                    <input type="text" name="ORDER_STATUS" id="ORDER_STATUS" runat="server"/>
                    <input type="text" name="ORDER_DATE"  id="ORDER_DATE"  runat="server" />
                    <input type="text" name="ORDER_CREATE_DATE" id="ORDER_CREATE_DATE" runat="server" />
                    <input type="text" name="ExisitingCustomer" id="ExistingCustomer" runat="server" />
                    <input type="text" name="QuoteByID" id="QuoteByID" runat="server" />
                    <input type="text" name="EmailSend" id="EmailSend" runat="server" />
                    <input type="text" name="SALESPERON_ID" id="SALESPERON_ID" runat="server" />
                </td>
   
        </tr>

      </table>


      <div  id="Dialog-Submit-Confirmation" title="SubmitConfirmation" style="display:none;">

          <table>
            <tr>
                 <td>
                        <span id="SubmitMessage" ></span>
                 </td>
            </tr>

             <tr>
               
                 <td><input type="button" id="btnReturnDashBoard" value="RETURN TO DASHBOARD" /></td>
                 <td><input type="button" id="btnPrintOrder" name="btnPrintOrder"  value="PRINT"  /></td>
                 <td >
                                         <input  type="button" id="btn_schedulteEvent" style="margin-left: 20px;color:red;" onclick="openDialog();" value="Schedule Event" />
                                        `</td> 
             </tr>
              
             
          </table>

      </div> 

     

      <div id="Dialog-DeleteConfirmation" style="display:none;">

          <table>

              <tr>
                  <td>
                       <span id="cancelMessage" ><b>YOU ARE ABOUT TO CANCEL THIS INVOICE. DO YOU WISH TO CONTINUE?</b></span>
                  </td> 
              </tr>

              <tr>
                  <td><input type="button" id="btnYES" name="btnYES" value="YES" /></td>
                  <td><input type="button" id="btnNO" name="btnNO" value="NO" /></td>

              </tr>

          </table>
      </div>

    <div id="Dialog-SuppNotes" style="display:none;">
        <table>
            <tr>
                    <td><input type="text"   id="spSupplierTitle"  name="spSupplierTitle" disabled="disabled" /></td>
            </tr>
            <tr>
                    <td></td>
            </tr>
            <tr>
                <td><textarea id="taSuppNote" name="taSuppNote"></textarea></td>
            </tr>

            <tr>
                <td><input type="button" id="submitNote" name="submitNote" value="SUBMIT" /></td>
                <td><input type="button" id="cancelNote" name="cancelNote" value="CANCEL" /></td>
            </tr>
        </table>
    </div>

    <div id="Dialog_CloseOrderWindow" style="display:none;">

        <table>
            <tr>
                <td>ARE YOU SURE YOU WANT TO CLOSE THIS ORDER?</td>
            </tr>
            <tr>
              <td><input type="button" id="btn_CloseWindow_YES"  name="btn_CloseWindow_YES"  value="YES"/></td>
              <td><input type="button" id="btn_CloseWindow_NO" name="btn_CloseWindow_NO" value="NO" /></td>

             

            </tr>

        </table>
    </div>



    <div id="DialogCreditType" style="display:none;">

        <table>
            <tr>

                <td>TYPE OF CREDIT</td>
            </tr>

            <tr>
                <td>


                </td>
            </tr>


        </table>

    </div>

     <div id="addDialog" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Add Event">
        <table class="style1">
            <tr>
                <td class="alignRight">Subject:</td>
                <td class="alignLeft">
                    <input id="addEventName" type="text" size="60" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">Description:</td>
                <td class="alignLeft">
                    <textarea id="addEventDesc" cols="60" rows="16"></textarea></td>
            </tr>
            <tr>
                
                     <td class="alignRight">Do Not Create Event:</td>
              
                <td class="alignLeft">
                    <input type="checkbox" id="dosaveNote" title="Do Not Create Event" onclick="NosaveEvent();" />
                </td>
            </tr>

            
                <tr>
                <td class="alignRight">
                   Background Color:</td>
                <td class="alignLeft">
                  
<%--<input value="ffcc00" type="text" id="addbgcolor" class="jscolor {width:243, height:150, position:'right',
    borderColor:'#FFF', insetColor:'#FFF', backgroundColor:'#666'}"/>--%>

                     <select id="addbgcolor">
 <option value="#FF0000" style="background-color:#FF0000;"">   Customer Service  </option>
 <option value="#0000FF" style="background-color:#0000FF">  Reorder         </option>
 <option value="#00FF00" style="background-color:#00FF00">   New Quote          </option>
 <option value="#800080" style="background-color:#800080" selected="selected">    Call Back     </option> 
                       <%-- <option value="#008000" style="background-color:#008000"> Quote Follow Up </option>--%>

                        <option value="#808080" style="background-color:#808080"> Other   </option>
                         <option value="#EE7600" style="background-color:#EE7600;color:white;font-size:16px">  Referral       </option>
                        <option value="#FFFF00" style="background-color:#FFFF00" >  Auth Call Back   </option>
                        <option value="#FA8072" style="background-color:#FA8072">  Sale Follow Up     </option>
                        
</select>


                </td>
            </tr>
            <tr>
                <td class="alignRight">Call Date:</td>
                <td class="alignLeft">
                    <input id="addEventStartDate" type="text" size="33" /><br />
                </td>

            </tr>
          <%--  <tr>
                <td class="alignRight">End:</td>
                <td class="alignLeft">
                    <input id="addEventEndDate" type="text" size="33" />
                </td>
            </tr>--%>
        </table>

    </div>


     <div id="addDialogCt" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Edit Customer Contact">
        <table class="style1">
            
            <tr>
                <td class="alignRight">Company Name:</td>
                <td class="alignLeft">
                     <input id="contactname" type="text" size="60" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">First Name:</td>
                <td class="alignLeft">
                     <input id="contactfirstname" type="text" size="60" /><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">Last Name:</td>
                <td class="alignLeft">
                     <input id="contactlastname" type="text" size="60" /><br />
                </td>
            </tr>
               <tr>
                <td class="alignRight">Email :</td>
                <td class="alignLeft">
                     <input id="contactemail" type="text" size="60" /></td>
            </tr>
               <tr>
                <td class="alignRight">Telephone :</td>
                <td class="alignLeft">
                     <input id="contacttelephone" type="text" size="60" /></td>
            </tr>
            <tr>
                <td class="alignRight">Address Line:</td>
                <td class="alignLeft">
                     <input id="contactaddressline" type="text" size="60" /></td>
            </tr>
       <tr>
                <td class="alignRight">City :</td>
                <td class="alignLeft">
                     <input id="contactaddresscity" type="text" size="60" /></td>
            </tr>
             <tr>
                <td class="alignRight">State :</td>
                <td class="alignLeft">
                    
                    
                    <select id="contactaddressstate" style="width:400px;">
 <option value="VIC" >   VIC  </option>
 <option value="TAS" >  TAS         </option>
 <option value="NT" >   NT          </option>
 <option value="ACT" >    ACT     </option> 
                       <%-- <option value="#008000" style="background-color:#008000"> Quote Follow Up </option>--%>

                        <option value="SA" > SA   </option>
                     
                         <option value="NSW" > NSW   </option>
                         <option value="QLD" > QLD   </option>
                        <option value="WA" > WA   </option>
</select>

                  
                </td>
            </tr>
             <tr>
                <td class="alignRight">Postcode :</td>
                <td class="alignLeft">
                     <input id="contactaddresspostcode" type="text" size="60" /></td>
            </tr>
           
        </table>

    </div>

     <div id="CreateAddWindow" style="display:none">
   <iframe id="createiframe" name="createiframe" width="710" height="400"style="border:0px;"></iframe>   
</div>
    <div id="CreateAddContact" style="display:none">
   <iframe id="addcontactiframe" width="950" height="700"style="border:0px;"></iframe>   
</div>

        <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />

    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-ui-sliderAccess.js"></script>
     <script src="Scripts/editor/AceTheme/assets/js/bootstrap-wysiwyg.js"></script>
    <script src="Scripts/editor/AceTheme/dist/js/bootbox.min.js"></script>
    <script src="Scripts/editor/AceTheme/assets/js/ace/elements.wysiwyg.js"></script>
    <script src="Scripts/editor/imageuploadtinymce/tinymce/tinymce.min.js"></script>
    <script type="text/javascript">

        function CreateOrder(ContactID, CompanyID) {
            $('#CreateAddContact').dialog('close');
            var quoteId = getParameterByName("OderID");
            window.open("../order.aspx?cid=" + ContactID + "&Compid=" + CompanyID + "&quoteId=" + quoteId);
        }

        var strTerm = "We send all goods on an open #terReplace# day account, however a 2% discount will be applied to orders on " +
             "immediate credit card payment.\n\rPlease don't hesitate to contact us if you have any questions/queries. Looking forward to hearing from you, we're here to help.";
        strTerm = strTerm.replace("#terReplace#", $("#ddlPaymentTerms").val());
        var quoteEmailTemplate = '<%= QuoteEmailTemplate %>';
        var quoteconfirmEmailTemplate = '<%= ConfirmationEmailTemplate %>';
        var quoteconfirmEmailTemplateFooter = '<%= ConfirmationEmailTemplateFooter %>';

        function callEmailHeader(vvv) {
            quoteEmailTemplate = quoteEmailTemplate.replace("#nnmm#", 'company\'s');
            if (vvv == 1) {
                //console.log(vvv );
                //  $("#ContentPlaceHolder1_txt_EmailHeader").val(quoteEmailTemplate);
                tinymce.get("ContentPlaceHolder1_txt_EmailHeader").setContent(quoteEmailTemplate);
                // tinyMCE.activeEditor.setContent(quoteEmailTemplate);

                tinymce.get("ContentPlaceHolder1_txt_EmailFooter").setContent(strTerm);
            } else {
                // console.log(vvv );
                //  $("#ContentPlaceHolder1_txt_EmailHeader").val(quoteconfirmEmailTemplate);
                tinymce.get("ContentPlaceHolder1_txt_EmailHeader").setContent(quoteconfirmEmailTemplate);
                tinymce.get("ContentPlaceHolder1_txt_EmailFooter").setContent(quoteconfirmEmailTemplateFooter);
                // tinyMCE.get('txt_EmailHeader').setContent(quoteconfirmEmailTemplate);
                //tinyMCE.activeEditor.setContent("dddd");
            }
        }

        tinyMCE.init({
            selector: "#ContentPlaceHolder1_txt_EmailFooter,#ContentPlaceHolder1_txt_EmailHeader",
            width: '100%',
            height: 220,
            statusbar: false,
            menubar: false,
            relative_urls: false,
            mode: "specific_textareas",
            setup: function (ed) {
                ed.on('init', function () {
                    this.getDoc().body.style.fontSize = '14px';
                    this.getDoc().body.style.fontFamily = '"Helvetica Neue", Helvetica, Arial, sans-serif';
                });
                ed.on('change', function () {
                    ed.save();
                });
            },
            fontsize_formats: "8pt 9pt 10pt 11pt 12pt 26pt 36pt",

            paste_data_images: true,

            plugins: [
                "advlist autolink lists link base64_image charmap hr anchor pagebreak",
                "searchreplace wordcount visualblocks visualchars code",
                "insertdatetime nonbreaking save table contextmenu directionality",
                "emoticons textcolor colorpicker textpattern "
            ],
            valid_elements: "*[*]",
            extended_valid_elements: "a[class|name|href|target|title|onclick|rel],script[type|src],iframe[src|style|width|height|scrolling|marginwidth|marginheight|frameborder],img[class|src|border=0|alt|title|hspace|vspace|width|height|align|onmouseover|onmouseout|name]",

            toolbar: "undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link base64_image  | forecolor backcolor | fontselect | fontsizeselect"
        });

        function getCOmpanyData() {

            var comId = getParameterByName("CompID");
            var dbType = getParameterByName("DB");

            contactObj = PageMethods.ReadDataCompany(comId, dbType, onSucceedCompanyData, onErrorCompany);
            return false;
        }

        function switchToCreateContact(CompID) {


            var contactID = getParameterByName("cid");
            var dbType = getParameterByName("DB");
            if (CompID == null)
                CompID = getParameterByName("CompID");
            $('.blackout').css("display", "none");
            $('#CreateAddWindow').dialog('close');
            $('#addcontactiframe').attr('src', 'Manage/editOrContinueContact.aspx?cid=' + contactID + "&dbt=" + dbType + "&comId=" + CompID);
            $('#CreateAddContact').dialog({
                resizable: false,
                modal: true,
                title: 'CONTACT',
                width: 900,
            });

            $('#CreateAddContact').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            return false;
        }


        function onSucceedCompanyData(response) {
            $('iframe[name=createiframe]').contents().find("#createrecord").hide();
            $('iframe[name=createiframe]').contents().find("#continuerecord").hide();
            var comName = response.split(',')[0];
            var accOwner = response.split(',')[1];
            var dbType = getParameterByName("DB");
            $('iframe[name=createiframe]').contents().find("#NewCompany").val(comName);
            // $('iframe[name=createiframe]').contents().find("#DropDownList1").val(accOwner);

            $('iframe[name=createiframe]').contents().find("#createrecord").show();

            $('iframe[name=createiframe]').contents().find("#DropDownList1 option:contains(" + accOwner + ")").attr('selected', 'selected');
        }

        function onErrorCompany(err) {

            alert(err);
        }

        function CreateAddWindow() {
            getCOmpanyData();

            $('#CreateAddWindow').dialog('open');
            $('#CreateAddWindow').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            return false;
        }

        var dateNow = new Date();

        function openDialog() {

            $("#addEventName").val($('#ContentPlaceHolder1_CompanyNameDIV').text());
            $('#addDialog').dialog('open');

            // $("#addEventStartDate").val($('#ContentPlaceHolder1_callbackDate').val());

            //alert($('#ContentPlaceHolder1_callbackDate').val());

            if ($('#ContentPlaceHolder1_callbackDate').val() != "") {
                var etDateForm = new Date($('#ContentPlaceHolder1_callbackDate').val());
                //  alert(etDateForm.ge);
                $("#addEventStartDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', etDateForm);
            }
            else {

                $("#addEventStartDate").datepicker({
                    dateFormat: 'dd/mm/yy'
                }).datepicker('setDate', dateNow);
            }

        }




        $(document).ready(function () {

            //  $("#addEventStartDate").datepicker({ beforeShowDay: $.datepicker.noWeekends });
            $('#createiframe').attr('src', 'Manage/createOrContinueCustomer.aspx');
            $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'CUSTOMER',
                width: 710,
                autoOpen: false,
            });

            var hourset = dateNow.getHours();

            $('#addEventStartDate').datetimepicker({
                timeInput: true,
                controlType: 'select',
                timeFormat: 'hh:mm tt',
                stepMinute: 30,
                hour: hourset,
                minute: 30,
                dateFormat: 'dd/mm/yy',
                hourMin: 8,
                hourMax: 17,
            });

            var strTerm = "We send all goods on an open #terReplace# day account, however a 2% discount will be applied to orders on " +
                 "immediate credit card payment.\n\rPlease don't hesitate to contact us if you have any questions/queries. Looking forward to hearing from you, we're here to help.";
            strTerm = strTerm.replace("#terReplace#", $("#ddlPaymentTerms").val());
            $('#ContentPlaceHolder1_txt_EmailFooter').val(strTerm);

            ddlPaymentTermsChange();

            //$('#addEventEndDate').datetimepicker({
            //    controlType: 'select',
            //    timeFormat: 'hh:mm tt',
            //    stepMinute: 30,
            //    hour: hourset,
            //    minute: 30
            //});

            $('#addDialog').dialog({
                autoOpen: false,
                width: 700,
                modal: true,
                buttons: {
                    "Add": function () {
                        //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
                        //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
                        var quoteID = -1;
                        var aaa = getParameterByName("Oderid");
                        ///alert("aa" + aaa);
                        if (aaa)
                            quoteID = aaa;
                        else
                            quoteID=getParameterByName("OderID");
                        var comId = getParameterByName("CompID");
                       

                        var eventToAdd = {
                            title: $("#addEventName").val(),
                            description: $("#addEventDesc").val(),
                            start: $("#addEventStartDate").val(),
                            color: $("#addbgcolor").val(),
                            QuoteId: quoteID,
                            companyId: comId
                        };
                        var isEventSave = false;
                        if ($("#dosaveNote").is(':checked')) {
                            isEventSave = true;
                        }
                       

                        if (validateInput()) {
                            //alert("sending " + eventToAdd.title);
                           
                            PageMethods.addEventTest(eventToAdd, isEventSave,addSuccess);
                            $(this).dialog("close");
                        }
                    }
                }
            });

        });

        function ddlPaymentTermsChange() {

            $("#ddlPaymentTerms").change(function () {

                var termValue = this.value;
                var strTerm = "We send all goods on an open #terReplace# day account, however a 2% discount will be applied to orders on " +
                "immediate credit card payment.\n\rPlease don't hesitate to contact us if you have any questions/queries. Looking forward to hearing from you, we're here to help.";
                strTerm = strTerm.replace("#terReplace#", termValue);
                $('#ContentPlaceHolder1_txt_EmailFooter').val(strTerm);
            });
        }



        function addSuccess(addResult) {

            alert("Event Successfully Scheduled.");
            location.reload();
        }

        function validateInput() {

            if ($("#addEventName").val() == "") {
                alert("Please enter name");
                return false;
            }
            if ($("#addEventDesc").val() == "") {
                alert("Please enter description");
                return false;
            }
            if ($("#addEventStartDate").val() == "") {
                alert("Please select start date and time");
                return false;
            }
            //if ($("#addEventEndDate").val() == "") {
            //    alert("Please select end date and time , should be grater than start date.");
            //    return false;
            //}

            //var startTime = new Date($("#addEventStartDate").val());
            //var endTime = new Date($("#addEventEndDate").val());

            //if (endTime.getMilliseconds() < startTime.getMilliseconds()) {
            //    alert("End date must be greater than start date");
            //    return false;
            //}

            //var from = $("#addEventStartDate").val().split("/");
            //var f = new Date(from[2], from[1] - 1, from[0]);

            return true;
        }

        function checkForSpecialChars(stringToCheck) {
            var pattern = /[^A-Za-z0-9 ]/;
            return pattern.test(stringToCheck);
        }

        $(document).ready(function () {

            //var cid = getParameterByName("cid");
            //alert(cid);
            //var dbType = getParameterByName("DB");
            //alert(dbType);

            var companyId = getParameterByName("CompID");
            console.log(companyId);
            $('#QuotesTblRep').dataTable({
                "ajax": "Fetch/FetchQuotesForQuotedCompany.aspx?cid=" + companyId,
                "columnDefs": [
                   { className: 'align_left', "targets": [1, 2, 3, 4] },
                   { className: 'align_center', "targets": [0, 5, 6] },

                ],

            });

            $('#addDialogCt').dialog({
                autoOpen: false,
                width: 700,
                modal: true,
                buttons: [

            {
                text: "Cancel",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "Update",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());

                    var contactId = getParameterByName("cid");
                    var dbType = getParameterByName("DB");

                    var UpdateContact = {
                        ContactId: contactId,
                        Dbtype: dbType,
                        CompanyName: $("#contactname").val(),
                        FirstName: $("#contactfirstname").val(),
                        LastName: $("#contactlastname").val(),
                        Email: $("#contactemail").val(),
                        Phone: $("#contacttelephone").val(),
                        Address1: $("#contactaddressline").val(),
                        City: $("#contactaddresscity").val(),
                        State: $("#contactaddressstate").val(),
                        PostCode: $("#contactaddresspostcode").val(),

                    };

                    if (validateInputC()) {
                        //alert("sending " + eventToAdd.title);

                        PageMethods.UpdateContactDetails(UpdateContact, addSuccessm);
                        $(this).dialog("close");
                    }
                }
            }
                ]
            });

        });


        function ContactUpdate() {
            this.ContactId = 0;
            this.dbType = "";
            this.CompanyName = "";
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address1 = "";
            this.City = "";
            this.State = "";
            this.PostCode = "";
        }

        function getParameterByName(name, url) {
            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return null;
            if (!results[2]) return '';
            return decodeURIComponent(results[2].replace(/\+/g, " "));
        }

        function addSuccessm(addResult) {

            alert("Contact Successfully Updated.");
            location.reload();

        }


        function SaveMe() {
            var quoteID = getParameterByName("OderID");
            var noteMessage = $('#ContentPlaceHolder1_TextBoxNoteAllocatedQuote').val();
            if (noteMessage == "") {
                alert("Please enter notes");
                return false;
            }

            PageMethods.UpdateQuoteNotesCa(quoteID, noteMessage, addSuccessmNot);
        }


        function addSuccessmNot(addRes) {

            alert("Successfully Created.");
            location.reload();
        }

        function openDialogCs() {
            var contactId = getParameterByName("cid");
            var dbType = getParameterByName("DB");
            var contactObj = new ContactUpdate();
            contactObj = PageMethods.ReadContact(contactId, dbType, onSucceed, onError);
            return false;

        }

        function onSucceed(response) {

            $("#contactname").val(response.CompanyName);
            $("#contactfirstname").val(response.FirstName);
            $("#contactlastname").val(response.LastName);
            $("#contactemail").val(response.Email);
            $("#contacttelephone").val(response.Phone);
            $("#contactaddressline").val(response.Address1);
            $("#contactaddresscity").val(response.City);
            if (response.State)
                $("#contactaddressstate").val(response.State);
            $("#contactaddresspostcode").val(response.PostCode);

            $('#addDialogCt').dialog('open');
        }

        function onError(err) {

            alert(err);
        }

        function validateInputC() {

            if ($("#contactname").val() == "") {
                alert("Please enter company name");
                return false;
            }
            if ($("#contactfirstname").val() == "") {
                alert("Please enter first name");
                return false;
            }
            if ($("#contactemail").val() == "") {
                alert("Please enter email");
                return false;
            }
            //if ($("#outcomeMessage").val() == "") {
            //    alert("Please enter outcome");
            //    return false;
            //}



            return true;
        }

        function ViewQuote(QuoteID, ContactID, CompanyID, WhichDB) {

            var dbVal = "";
            if (WhichDB == 1)
                dbVal = "LiveDB";
            else
                dbVal = "QuoteDB";

            window.open("RepAllocateQuoteDisplay.aspx?Oderid=" + QuoteID + "&CompID=" + CompanyID + "&cid=" + ContactID + "&FLAG=Y&DB=" + dbVal, '_self');
        }


    </script>


    <style type="text/css">
        th.ui-datepicker-week-end,
        td.ui-datepicker-week-end {
            display: none;
        }

      
        .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 40px;
            cursor: pointer;
        }

            .buttonClass:hover {
                border: solid 1px Black;
                background-color: #ffffff;
            }

        .moveRight {
            float: right;
            margin-bottom: 10px;
            margin-top: 10px;
            margin-right: 200px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }

        .moverigRight {
            float: right;
            margin-bottom: 10px;
            margin-top: 10px;
            margin-right: 40px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }
    </style>

     <style type="text/css">
         .ui-widget-content.ui-dialog {
             border: 1px solid #000 !important;
         }

         .ui-button {
             background-color: lightblue !important;
         }
     </style>

</asp:Content>
