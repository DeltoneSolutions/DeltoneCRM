<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addsupplier.aspx.cs" Inherits="DeltoneCRM.Manage.addsupplier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Deltone Add New Supplier</title>
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
            width: 125px;
            }
        
        </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>



  <script type="text/javascript">

      $(document).ready(function () {

          $("#<%=form1.ClientID%>").validate({
              onfocusout: false,
              onkeyup: false,
              rules: {
                  NewItem: {
                      required: true,
                  },
                  NewCost: {
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
                  //alert('Triggered');

              },

              submitHandler: function (form) {
                  $.ajax({
                      type: "POST",
                      url: "Process/Process_AddSupplier.aspx",
                      data: {
                          SupplierName: $('#NewItem').val(),
                          DeliveryCost: $('#NewCost').val(),
                          Address: $('#suppaddress').val(),
                          ContactName: $('#contactnamesupp').val(),
                          PhoneNumber: $('#suppphonenumber').val(),
                          City: $('#suppcity').val(),
                          State: $('#suppstate').val(),
                          PostCode: $('#supppostcode').val(),
                          SalesEmail: $('#suppsalesemail').val(),
                          ReturnEmail: $('#suppreturnemail').val(),
                          AccountsPhoneNumber: $('#suppaccountPhonenumber').val(),
                          UserName: $('#suppusername').val(),
                          AccountsEmail: $('#contactnameemail').val(),
                          Notes: $('#suppnotes').val(),
                          Password: $('#supppassword').val(),


                      },
                      success: function (msg) {
                          alert('Supplier has been created successfully');
                          window.parent.closeIframe();
                          window.parent.location.reload(false);
                      },
                      error: function (xhr, err) {
                          alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                          alert("responseText: " + xhr.responseText);
                      },
                  });
              }

          })

      });

 </script>


</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="750" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                    </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td>&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">SUPPLIER NAME</td>
                                  <td width="215"><input name="NewItem" type="text" class="txtbox-200-style-edit" id="NewItem" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">STD DELIVERY $</td>
                                  <td width="200"><input name="NewCost" type="text" class="txtbox-200-style-edit" id="NewCost" /></td>
                                </tr>
                              </table></td>
                               
                            </tr>
                              <tr> <td  width="20">&nbsp;</td> </tr>
                              <tr>
                                    <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">ADDRESS</td>
                                  <td width="215"><input name="suppaddress" type="text" class="txtbox-200-style-edit" id="suppaddress" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">CITY</td>
                                  <td width="200"><input name="suppcity" type="text" class="txtbox-200-style-edit" id="suppcity" /></td>
                                </tr>
                              </table></td>
                              </tr>

                              <tr> <td  width="20">&nbsp;</td> </tr>
                              <tr>
                                    <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">STATE</td>
                                  <td width="215"> <select id="suppstate" style="width:220px;height:30px;">
 <option value="VIC" >   VIC  </option>
 <option value="TAS" >  TAS         </option>
 <option value="NT" >   NT          </option>
 <option value="ACT" >    ACT     </option> 
                       <%-- <option value="#008000" style="background-color:#008000"> Quote Follow Up </option>--%>

                        <option value="SA" > SA   </option>
                     
                         <option value="NSW" > NSW   </option>
                         <option value="QLD" > QLD   </option>
                        <option value="WA" > WA   </option>
</select></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">POSTCODE</td>
                                  <td width="200"><input name="supppostcode" type="text" class="txtbox-200-style-edit" id="supppostcode" /></td>
                                </tr>
                              </table></td>
                              </tr>
                               <tr> <td  width="20">&nbsp;</td> </tr>
                              <tr>
                                    <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">SALES EMAIL</td>
                                  <td width="215"><input name="suppsalesemail" type="text" class="txtbox-200-style-edit" id="suppsalesemail" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">RETURN EMAIL</td>
                                  <td width="200"><input name="suppreturnemail" type="text" class="txtbox-200-style-edit" id="suppreturnemail" /></td>
                                </tr>
                              </table></td>
                              </tr>
                               <tr> <td  width="20">&nbsp;</td> </tr>
                              <tr>
                                    <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">PHONE NUMBER</td>
                                  <td width="215"><input name="suppphonenumber" type="text" class="txtbox-200-style-edit" id="suppphonenumber" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">ACCOUNT PHONE NUMBER</td>
                                  <td width="200"><input name="suppaccountPhonenumber" type="text" class="txtbox-200-style-edit" id="suppaccountPhonenumber" /></td>
                                </tr>
                              </table></td>
                              </tr>
                                <tr> <td  width="20">&nbsp;</td> </tr>
                              <tr>
                                    <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">USERNAME</td>
                                  <td width="215"><input name="suppusername" type="text" class="txtbox-200-style-edit" id="suppusername" /></td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                              <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">PASSWORD</td>
                                  <td width="200"><input name="supppassword" type="text" class="txtbox-200-style-edit" id="supppassword" /></td>
                                </tr>
                              </table></td>
                              </tr>
                              <tr> <td  width="20">&nbsp;</td> </tr>
                               <tr>
                                    <td ><table  border="0" cellspacing="0" cellpadding="0" style="width: 331px" >
                                <tr>
                                  <td width="100" class="labels-style">NOTES</td>
                                  <td width="400"><textarea id="suppnotes" cols="60" class="txtbox-200-style-edit" rows="24"></textarea> </td>
                                </tr>
                              </table></td>
                              <td width="20">&nbsp;</td>
                           <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">CONTACT NAME</td>
                                  <td width="200"><input name="contactnamesupp" type="text" class="txtbox-200-style-edit" id="contactnamesupp" /></td>
                                </tr>
                              </table></td>
                              </tr>
                                <tr> <td  width="20">&nbsp;</td> </tr>
                               <tr>
                                    <td ><table  border="0" cellspacing="0" cellpadding="0" style="width: 331px" >
                                 <td width="315"><table width="315" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels-style">ACCOUNTS EMAIL</td>
                                  <td width="200"><input name="contactnameemail" type="text" class="txtbox-200-style-edit" id="contactnameemail" /></td>
                                </tr>
                              </table></td>
                              </table></td>
                              <td width="20">&nbsp;</td>
                         
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
                      <td>
                          <table align="right" cellpadding="0" cellspacing="0" class="auto-style1">
                              <tr>
                                  <td><input name="createrecord" id="createrecord" type="submit" class="btn-green-style"  value="CREATE" /></td>
                              </tr>
                          </table>
                        </td>
                      </tr>
                    <tr>
                      <td class="height-15px-style">&nbsp;</td>
                      </tr>
                    <tr>
                      <td><table width="680" border="0" cellspacing="0" cellpadding="0">
                        <tr style="display:none;">
                          <td width="430">&nbsp;</td>
                          <td width="250">&nbsp;<input name="button" type="reset" class="settingsbutton_gray" id="button" value="CLEAR FORM" /></td>
                          </tr>
                        </table></td>
                    </tr>
                    
                    </table>
    </div>
    </form>
</body>
</html>
