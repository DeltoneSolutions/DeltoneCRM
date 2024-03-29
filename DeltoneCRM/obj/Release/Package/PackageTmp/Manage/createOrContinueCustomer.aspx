﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="createOrContinueCustomer.aspx.cs" Inherits="DeltoneCRM.Manage.createOrContinueCustomer" %>

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
	background-color: #CCCCCC;
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

            createCompany();
            continueWithCreatedCom();
        });
       
        var comIdFromData="";

        function continueWithCreatedCom(){
            comIdFromData = getParameterByName("CompID");
            $('#continuerecord').click(function () {
                
               
                window.parent.switchToCreateContact(comIdFromData);
            });
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

        function createCompany() {
            $('#createrecord').click(function () {
               
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
                       // alert('Triggered');

                    },
                    submitHandler: function (form) {
                        $.ajax({
                            type: "POST",
                            url: "Process/Process_AddCustomer.aspx",
                            data: {
                                NewCompany: $('#NewCompany').val(),
                                NewWebsite: $('#NewWebsite').val(),
                                NewAccountOwner: $('#DropDownList1').val(),
                            },
                            success: function (msg) {

                                if (msg == 'ERROR') {
                                    alert('COMPANY WITH THE NAME ' + $('#NewCompany').val() + ' AlREADY EXSISTS');
                                    $("#continuerecord").show();
                                }
                                else {
                                    // window.parent.switchToCreateContact(msg);
                                    alert('COMPANY SUCCESSFULLY CREATED');
                                    comIdFromData = msg;
                                    $("#continuerecord").show();
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

            });
        }
    </script>
</head>
<body>
    <form id="form1" name="form1" runat="server">
    <div>
    <table width="680" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td height="15">&nbsp;</td>
                    </tr>
                    <tr>
                      <td height="25" class="green_headings">&nbsp;&nbsp;&nbsp; COMPANY DETAILS</td>
                      </tr>
                    <tr>
                      <td bgcolor="#FFFFFF"><table width="650" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                          <td height="15">&nbsp;</td>
                        </tr>
                        <tr>
                          <td><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="300"><table width="300" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <div id="errDiv"></div>
                                  <td width="100" class="labels_001">COMPANY NAME&nbsp;&nbsp;&nbsp;</td>
                                  <td width="200"><input name="NewCompany" type="text" class="textbox_001" title="Please enter company name" id="NewCompany" /></td>
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
                          <td height="15"><table width="650" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                              <td width="300"><table width="300" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="100" class="labels_001">ACCOUNT OWNER&nbsp;</td>
                                  <td width="200">
                                      <asp:DropDownList ID="DropDownList1" runat="server">
                                      </asp:DropDownList>
                                    </td>
                                </tr>
                              </table></td>
                              <td width="50">&nbsp;</td>
                              <td width="300">&nbsp;</td>
                            </tr>
                          </table></td>
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
                      <td><table width="680" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="430">&nbsp;</td>
                          <td width="250">
                              <input name="createrecord" type="submit" class="clk_button_01" id="createrecord" value="CREATE COMPANY" />                            
                              <input name="button" type="reset" class="settingsbutton_gray" id="button" style="display:none;" value="CLEAR FORM" />
                              <input name="continuerecord" type="button" class="clk_button_01" id="continuerecord" value="CONTINUE" />
                          </td>
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
