<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addcustomer.aspx.cs" Inherits="DeltoneCRM.Manage.addcustomer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="../js/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery.validate.js" type="text/javascript"></script>

<%--  <script   src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>

<script   src="http://ajax.aspnetcdn.com/ajax/jQuery.Validate/1.6/jQuery.Validate.js"></script>--%>

<style type="text/css">

body {
	
	margin-top: 0px;
	font-family: 'PT Sans', sans-serif;
	margin-left: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
.width-680-style {
     width: 680px;
}

</style>
    <script>

        $.validator.addMethod("validateComma", function (value, element) {
            var returnvalue = true;

            if (value.indexOf(',') > -1) {

                returnvalue = false;
            }

            return returnvalue;
        }, "Invalid input please remove comma.");

        $(document).ready(function () {


           


       
        });

        function submitForAdmin() {

            $("#<%=form1.ClientID%>").validate({
                onfocusout: false,
                onkeyup: false,
                rules: {
                    NewCompany: {
                        required: true,
                        validateComma: true,

                    },
                   
                    DropDownList1: {
                        required: true,
                    }
                },
                messages: {
                    NewCompany: {
                        required: "Please Enter Company Name",
                        validateComma: "Company Name Must not contain comma"
                    },
                   
                },

                highlight: function (element) {
                    $(element).closest("input")
                    .addClass("textbox_001_err")
                    .removeClass("textbox_001");
                },
                unhighlight: function (element) {
                    $(element).closest("input")
                    .removeClass("textbox_001_err")
                    .addClass("textbox_001");
                },
                errorElement: "em",
                errorContainer: $("#warning, #summary"),
                errorPlacement: function (error, element) {
                    error.insertAfter(element);
                },
                success: function (label) {
                    //alert('Triggered');

                },
                submitHandler: function (form) {
                    $.ajax({
                        type: "POST",
                        url: "Process/Process_AddCustomer.aspx",
                        data: {
                            NewCompany: $('#NewCompany').val(),
                            NewWebsite: $('#NewWebsite').val(),
                            NewAccountOwner: $('#DropDownList1').val(),
                            ISSkipClicked: 'true'

                        },
                        success: function (msg) {

                            if (msg == 'ERROR') {
                                alert('COMPANY WITH THE NAME ' + $('#NewCompany').val() + ' AlREADY EXSISTS');
                            }
                            else {
                                window.parent.switchToCreateContact(msg);
                            }

                            //window.parent.closeCreateFrame();
                            //window.parent.location.reload(false);
                        },
                        error: function (xhr, err) {
                            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                            alert("responseText: " + xhr.responseText);
                        },
                    });
                },
            });
        }

        function submitForREP() {

            
            $("#<%=form1.ClientID%>").validate({
                onfocusout: false,
                onkeyup: false,
                rules: {
                    NewCompany: {
                        required: true,
                        validateComma: true,

                    },
                    NewFirstName: {
                        required: true,
                    },
                    NewLastName: {
                        required: true,
                    },
                    NewEmailAddy: {
                        required: true,
                    },
                    DropDownList1: {
                        required: true,
                    }
                },
                messages: {
                    NewCompany: {
                        required: "Please Enter Company Name",
                        validateComma: "Company Name Must not contain comma"
                    },
                    NewFirstName: {
                        required: "Please Enter First Name",
                    },
                    NewLastName: {
                        required: "Please Enter Last Name",
                    },
                    NewEmailAddy: {
                        required: "Please Email Address ",
                    }
                },

                highlight: function (element) {
                    $(element).closest("input")
                    .addClass("textbox_001_err")
                    .removeClass("textbox_001");
                },
                unhighlight: function (element) {
                    $(element).closest("input")
                    .removeClass("textbox_001_err")
                    .addClass("textbox_001");
                },
                errorElement: "em",
                errorContainer: $("#warning, #summary"),
                errorPlacement: function (error, element) {
                    error.insertAfter(element);
                },
                success: function (label) {
                    //alert('Triggered');

                },
                submitHandler: function (form) {
                    $.ajax({
                        type: "POST",
                        url: "Process/Process_AddCustomer.aspx",
                        data: {
                            NewCompany: $('#NewCompany').val(),
                            NewWebsite: $('#NewWebsite').val(),
                            NewAccountOwner: $('#DropDownList1').val(),
                            FirstName: $('#NewFirstName').val(),
                            LastName: $('#NewLastName').val(),
                            EmailAddress: $('#NewEmailAddy').val(),

                        },
                        success: function (msg) {

                            if (msg == 'ERROR') {
                                alert('COMPANY WITH THE NAME ' + $('#NewCompany').val() + ' AlREADY EXISTS');
                            }
                            else {
                                window.parent.switchToCreateContactWithFirstNameAndLastNameAndEmail(msg, $('#NewFirstName').val(), $('#NewLastName').val(), $('#NewEmailAddy').val());
                            }

                            //window.parent.closeCreateFrame();
                            //window.parent.location.reload(false);
                        },
                        error: function (xhr, err) {
                            alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                            alert("responseText: " + xhr.responseText);
                        },
                    });
                },
            });
        }

        function callValidationMethod() {

           
            if ($("#skipValidation").is(':checked')) {
                submitForAdmin();
            }
            else {
                
                submitForREP();
            }
        }
    </script>
</head>
<body>
    <form id="form1" name="form1" runat="server">
    <div>
    <table width="580"  border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td height="15">&nbsp;</td>
                    </tr>
                    <tr>
                      <td height="25" class="green_headings">&nbsp;&nbsp;&nbsp;NEW COMPANY DETAILS</td>
                      </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><table width="550" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td height="15">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="550" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="300"><table width="550" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <div id="errDiv"></div>
                                  <td width="250" class="labels-style">COMPANY NAME&nbsp;&nbsp;&nbsp;</td>
                                  <td width="200"><input name="NewCompany" type="text" style="width:350px;height:25px;" class="textbox_001" autocomplete="off" title="Please enter company name" id="NewCompany" /></td>
                                </tr>
                                   <tr id="canshowadmin1" runat="server" style="display:none;">
                                         <td height="15">&nbsp;</td>
                                       </tr>
                                   <tr id="canshowadmin2" runat="server" style="display:none;">
                                  <td width="650" >SKIP BELOW VALIDATION :&nbsp;&nbsp;&nbsp;</td>
                                  <td width="100"> <input type="checkbox" name="skipValidation" id="skipValidation" /> </td>
                                </tr>

<tr>
                          <td height="15">&nbsp;</td>
                        </tr>
                                   <tr>
                                  <td width="250" class="labels-style">FIRST NAME&nbsp;&nbsp;&nbsp;</td>
                                  <td width="215"><input name="NewFirstName" type="text" style="width:350px;height:25px;" autocomplete="off" class="txtbox-200-style-edit" id="NewFirstName" /></td>
                                </tr>
                                   <tr>
                          <td height="15">&nbsp;</td>
                        </tr>
                                  <tr>
                                  <td width="250" class="labels-style">LAST NAME&nbsp;&nbsp;&nbsp;</td>
                                  <td width="200"><input name="NewLastName" style="width:350px;height:25px;" type="text" autocomplete="off" class="txtbox-200-style-edit" id="NewLastName" /></td>
                                </tr>
                                   <tr>
                          <td height="15">&nbsp;</td>
                        </tr>
                                   <tr>
                                  <td width="250" class="labels-style">EMAIL ADDRESS &nbsp;&nbsp;&nbsp;</td>
                                  <td width="200"><input name="NewEmailAddy" style="width:350px;height:25px;" type="text" autocomplete="off" class="txtbox-200-style-edit" id="NewEmailAddy" /></td>
                                </tr>
                                      <tr>
                          <td height="15">&nbsp;</td>
                        </tr>

                                    <tr>
                                  <td width="200" class="labels-style">ACCOUNT OWNER</td>
                                  <td width="200">
                                      <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" Height="35px">
                                      </asp:DropDownList>
                                    </td>
                                </tr>

                              </table></td>
                              <td width="50">&nbsp;</td>



                              <td width="300"><table width="300" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels_001">&nbsp;</td>
                                  <td width="200">&nbsp;</td>
                                </tr>
                              </table></td>
                            </tr>
                          </table></td>
                        </tr>
                        <tr>
                          <td height="15">&nbsp;</td>
                        </tr>
                       
                              
                            
                        <tr>
                          <td height="15">&nbsp;</td>
                        </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td></td>
                      </tr>
                    <tr>
                      <td height="15">&nbsp;</td>
                      </tr>
                    <tr>
                      <td><table width="550" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="450">&nbsp;</td>
                          <td ><input name="createrecord" type="submit" onclick=" callValidationMethod();" style="height:30px;" class="clk_button_01" id="createrecord" value="CREATE COMPANY" />                            <input name="button" type="reset" class="settingsbutton_gray" id="button" style="display:none;" value="CLEAR FORM" /></td>
                          </tr>
                        <tr>
                          <td height="15">&nbsp;</td>
                          <td>&nbsp;</td>
                          </tr>
                        </table></td>
                    </tr>
                    
                    </table>
    </div>
    </form>
</body>
</html>
