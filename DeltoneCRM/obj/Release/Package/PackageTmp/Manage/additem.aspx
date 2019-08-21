<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="additem.aspx.cs" Inherits="DeltoneCRM.Manage.additem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.validate.js" type="text/javascript"></script>
        <link href="../../css/ManageCSS.css" rel="stylesheet" />

    <style type="text/css">
        body {
            margin-top: 0px;
            margin-bottom: 0px;
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

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'/>

     <script type="text/javascript">

         function blockChar(ev) {
             var keynum;
             if (window.event) keynum = ev.keyCode;// IE
             else if (ev.which) keynum = ev.which; // Other browsers
             else keynum = ev.charCode;
             if (keynum == 44) { return false; }

         }


         $.validator.addMethod("validateComma", function (value, element) {
             var returnvalue = true;

             if (value.indexOf(',') > -1) {

                 returnvalue = false;
             }

             return returnvalue;
         }, "Invalid input please remove comma.");


         $(document).ready(function () {

             $('#NewItemCode').blur(function () {
                 $.ajax({
                     url: '../Fetch/checkItemExists.aspx',
                     data: {
                         IC: $('#NewItemCode').val(),
                     },
                     success: function (response) {
                         var splitres = response.split('|');
                         if (splitres[0] == "YES")
                         {
                             alert('This Item already exists in the system as: ' + splitres[1]);
                         }
                     }
                 })
             })

             $("#<%=form1.ClientID%>").validate({
              onfocusout: false,
              onkeyup: false,
              rules: {
                  DropDownList1: {
                      required: true,
                  },
                  NewItemCode: {
                      required: true,
                  },
                  NewDescription: {
                      required: true,
                      validateComma: true,
                  },
                  NewCOG: {
                      required: true,
                  },
                  NewResellPrice: {
                      required: true,
                  },
              },
              messages: {
                  NewDescription: {
                      required: "Please Enter Description",
                      validateComma: "Description Must not contain comma"
                  },
                  
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

                  error.insertAfter(element);
              },
              success: function (label) {
                  //alert('Triggered');

              },

              submitHandler: function (form) {
                  $.ajax({
                      type: "POST",
                      url: "Process/Process_AddItem.aspx",
                      data: {
                          DropDownList1: $('#DropDownList1').val(),
                          NewItemCode: $('#NewItemCode').val(),
                          NewDescription: $('#NewDescription').val().replace(',', ''),
                          NewCOG: $('#NewCOG').val(),
                          NewResellPrice: $('#NewResellPrice').val(),
                          Bestprice: $('#chk_bestprice').prop('checked'),
                          Faulty: $('#chk_faulty').prop('checked'),
                          Quantity: $('#Quantity').val(),
                          DSB: $('#txt_dsb').val(),

                      },
                      success: function (msg) {

                          alert('Product has been successfully created');
                            window.parent.closeAddFrame();
                              window.parent.location.reload(false);
                      },
                      error: function (xhr, err) {
                          alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                          alert("responseText: " + xhr.responseText);
                      },
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
                                  <td width="100" class="labels-style">ITEM CODE&nbsp;&nbsp;</td>
                                  <td width="215"><input name="NewItemCode" type="text" class="txtbox-200-style-edit" id="NewItemCode" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">SUPPLIER</td>
                                  <td width="200">
                                      <asp:DropDownList ID="DropDownList1" class="drpdwn-200-style-edit" runat="server"></asp:DropDownList>
                                    </td>
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
                                  <td width="100" class="labels-style">DESCRIPTION</td>
                                  <td width="215"><input name="NewDescription" type="text" class="txtbox-200-style-edit" id="NewDescription" onkeypress="return blockChar(event)" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">COG</td>
                                  <td width="200"><input name="NewCOG" type="text" class="txtbox-200-style-edit" id="NewCOG" /></td>
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
                              <td width="315">
                                  <table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">Quantity</td>
                                  <td width="215"><input name="Quantity" type="text" class="txtbox-200-style-edit" id="Quantity"  /></td>
                                </tr>
                              </table>

                              </td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">RESELL PRICE&nbsp;</td>
                                  <td width="215"><input name="NewResellPrice" type="text" class="txtbox-200-style-edit" id="NewResellPrice" /></td>
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
                                  <td width="100" class="labels-style">BEST PRICE&nbsp;</td>
                                  <td width="215"><input type="checkbox" id="chk_bestprice" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">OFTEN FAULTY&nbsp;</td>
                                  <td width="215"><input type="checkbox" id="chk_faulty" /></td>
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
                                  <td width="100" class="labels-style">DSB&nbsp;</td>
                                  <td width="215"><input type="text" class="txtbox-200-style-edit" id="txt_dsb" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <%--<td width="100" class="labels-style">OFTEN FAULTY&nbsp;</td>
                                  <td width="215"><input type="checkbox" id="chk_faultysss" /></td>--%>
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
                      <td class="height-15px-style">&nbsp;</td>
                      </tr>
                    <tr>
                      <td style="display:none;"><input name="button" type="reset" class="settingsbutton_gray" id="button" value="CLEAR FORM" /></td>
                      </tr>
                    <tr>
                      <td>
                          <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                              <tr>
                                  <td>&nbsp;</td>
                                  <td>
                                      <table align="right" cellpadding="0" cellspacing="0" class="auto-style2">
                                          <tr>
                                              <td width="125px">&nbsp;</td>
                                              <td width="15px">&nbsp;</td>
                                              <td width="125px"><input name="createrecord" type="submit" class="btn-green-style" id="createrecord" value="CREATE" /></td>
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
                    
                    </table>
    </div>
    </form>
</body>
</html>
