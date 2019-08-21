<%@ Page Title="" Language="C#" MasterPageFile="~/Login.Master" AutoEventWireup="true" CodeBehind="DeltoneCRMLogin.aspx.cs" Inherits="DeltoneCRM.DeltoneCRMLogin" %>
<%@ MasterType VirtualPath="~/Site1.Master" %> 
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DeltoneCRM - Dashboard</title>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="js/jquery-1.9.1.js"></script>
	<script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
 
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <script type="text/javascript">
    </script>

    <style type="text/css">
        .welcome-outline {
            background-color: #FFF;
            border: 1px solid #CCCCCC;
            width: 978px;
            text-align:center;
        }
        .auto-style2 {
            width: 300px;
        }
        .username-password-textbox {
	        font-family: 'Open Sans', sans-serif;
	        font-size: 12px;
	        color: #666666;
	        text-decoration: none;
	        font-weight: 400;
	        padding-top: 0px;
	        padding-right: 10px;
	        padding-bottom: 0px;
	        padding-left: 10px;
	        width: 250px;
	        height: 35px;
	        background-color: #eefbfb;
	        border: 1px solid #4fc2bd;
	        outline: none;
        }
        .login-button {
            font-family: 'Open Sans', sans-serif;
            color: #ffffff;
	        font-size: 13px;
            font-weight:400;
            width:270px;
            height: 35px;
            border: 1px solid #eb473d;
            background-color: #eb473d;
            outline: none;
        }
        .login-button:hover {
            cursor:pointer;
            background-color: #eb271b;
        }
        .login-welcome {
            font-family: 'Open Sans', sans-serif;
            color: #eb473d;
	        font-size: 13px;
            font-weight:600;
        }
    </style>

</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table align="center" cellpadding="0" cellspacing="0" width="400px">
            <tr>
                <td height="25px">&nbsp;</td>
            </tr>
            <tr>
                <td class="login-welcome">Welcome. Please login...</td>
            </tr>
            <tr>
                <td height="25px">&nbsp;</td>
            </tr>
            <tr>
                <td class="welcome-outline">

                    <div id="divDeltoneCRM" title="DletoneCRM Login">


                        <table align="center" cellpadding="0" cellspacing="0" class="auto-style2">
                            <tr>
                                <td height="25px">&nbsp;</td>
                            </tr>

                            <tr>
                                <td height="25px">&nbsp;</td>
                            </tr>

                            <tr>
                                <td><asp:TextBox ID="txtUserName" runat="server" class="username-password-textbox" placeholder="Username"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox id="txtPassWord" TextMode="password" runat="server" placeholder="Password" class="username-password-textbox"/></td>
                            </tr>
                            <tr>
                                <td height="25px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td><asp:Button ID="btnLogin" runat="server" Text="LOGIN" OnClick="btnLogin_Click" class="login-button" /></td>
                            </tr>
                            <tr>
                                <td height="25px">&nbsp;</td>
                            </tr>
                            <tr>
                                <td height="25px">&nbsp;</td>
                            </tr>
                        </table>

        

        <table id="tblDelToneLogin">


            <tr>
                <td colspan="2"><asp:Label ID="Msg" ForeColor="red" runat="server" /></td>

            </tr>


        </table>



    </div>

                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>

    




</asp:Content>
