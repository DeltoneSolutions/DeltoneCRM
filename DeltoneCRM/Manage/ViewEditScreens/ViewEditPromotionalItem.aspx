<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEditPromotionalItem.aspx.cs" Inherits="DeltoneCRM.Manage.ViewEditScreens.ViewEditPromotionalItem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View/Edit Promotional Item</title>
    
     <link href="../../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../../js/jquery-2.1.3.js"></script>
	<script src="../../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../../css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="../../css/tinytools.toggleswitch.css" rel="stylesheet" />
    <script src="../../js/jquery.validate.js"></script>
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
    <script src="../../js/tinytools.toggleswitch.js" type="text/javascript"></script>
    <link href="../../css/ManageCSS.css" rel="stylesheet" />

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

         $('#createrecord').hide();
         $('#button').hide();


         //function Clear button
         $('#button').click(function () {
             $('#NewItem').val('');
             $('#NewCost').val('');
             $('#NewDefaultQty').val('');
             $('#NewShippingCost').val('');
         });

         
         $('#SwitchToEdit').click(function () {
             $('#SwitchToEdit').hide();
             $('#BtnCloseWindow').hide();
             $('#createrecord').show();
             $('#NewItem').prop("readonly", false);
             $('#NewCost').prop("readonly", false);
             $('#NewDefaultQty').prop("readonly", false);
             $('#NewShippingCost').prop("readonly", false);
             $('#activechkbox').prop('disabled', false);

             $('#NewItem').removeClass("txtbox-200-style");
             $('#NewItem').addClass("txtbox-200-style-edit");

             $('#NewCost').removeClass("txtbox-200-style");
             $('#NewCost').addClass("txtbox-200-style-edit");

             $('#NewDefaultQty').removeClass("txtbox-200-style");
             $('#NewDefaultQty').addClass("txtbox-200-style-edit");

             $('#NewShippingCost').removeClass("txtbox-200-style");
             $('#NewShippingCost').addClass("txtbox-200-style-edit");

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
                 },
                 NewDefaultQty: {
                     required: true,
                 },
                 NewShippingCost:{
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
                 $.post("../Process/Process_UpdatePromoItem.aspx",
                     {
                         ItemID: $('#hdnEditPromoItemID').val(),
                         NewItem: $('#NewItem').val(),
                         NewCost: $('#NewCost').val(),
                         NewDefaultQty: $('#NewDefaultQty').val(),
                         NewShippingCost: $('#NewShippingCost').val(),
                         ActInact: $('#activechkbox').is(':checked')

                     },
                     function (data, status) {

                         //alert(data);

                         if (Number(data) > 0) {
                             alert("Promotional Item has been edited successfully");
                             window.parent.closeEditWindow();
                             window.parent.location.reload(false);
                         }
                         else {
                             alert('Error Creating Promotional Item');
                         }
                     });
                  },
         });


     });

 </script>

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
                                  <td width="100" class="labels-style">ITEM NAME&nbsp;&nbsp;</td>
                                  <td width="215"><input name="NewItem" type="text" class="txtbox-200-style" id="NewItem"  readonly="true" runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">COST</td>
                                  <td width="200"><input name="NewCost" type="text" class="txtbox-200-style" id="NewCost" readonly="true" runat="server" /></td>
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
                                  <td width="100" class="labels-style">DEFAULT QTY&nbsp;</td>
                                  <td width="215"><input name="NewDefaultQty" type="text" class="txtbox-200-style" id="NewDefaultQty"  readonly="true" runat="server" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">SHIPPING COST</td>
                                  <td width="200"><input name="NewShippingCost" type="text" class="txtbox-200-style" id="NewShippingCost" readonly="true" runat="Server" /></td>
                                </tr>
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
                      <td class="height-15px-style">&nbsp</td>
                      </tr>
                    <tr>
                      <td>
                          <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                              <tr>
                                  <td><input type="checkbox" id="activechkbox" runat="server"/></td>
                                  <td>
                                      <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                          <tr>
                                              <td width="125px">
                              <input type="button" class="btn-red-style" id="SwitchToEdit" value="EDIT" /></td>
                                              <td width="15px">&nbsp;</td>
                                              <td width="125px"><input type="button" id="BtnCloseWindow" class="btn-green-style" value="OK" onclick="parent.closeEditWindow();" /><input name="createrecord" id="createrecord" class="btn-red-style" type="submit" class="clk_button_01"  value="SAVE"  /></td>
                                          </tr>
                                      </table>
                                  </td>
                              </tr>
                          </table>
                        </td>
                      </tr>
                                  <tr>
                      <td class="height-15px-style"><input type="hidden" name="hdnEditPromoItemID" id="hdnEditPromoItemID" runat="server" /></td>
                      </tr>
                                  

                                  </table>
    </div>
    </form>
</body>
</html>
