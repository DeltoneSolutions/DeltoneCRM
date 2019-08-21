<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyContactInfoHold.aspx.cs" Inherits="DeltoneCRM.CompanyContactInfoHold" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/Overall.css" rel="stylesheet" />

    <link href='http://fonts.googleapis.com/css?family=Yanone+Kaffeesatz:400,700,300,200' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Raleway:500,600,700,800' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:400,600,700' rel='stylesheet' type='text/css'>

    <style type="text/css">
        body {
            margin-top: 0px;
            margin-bottom: 0px;
            background-color: #ffffff;
        }
        .width-900px-style {
            width: 900px;
        }
        .width-940px-style {
            width: 940px;
        }
        .width-980px-style {
            width: 980px;
        }
        .height-20px-style {
            height: 20px;
        }
        .height-25px-style {
            height: 25px;
        }
        .blue-top-001-style {
            background-color: #62b5e9;
            height: 20px;
        }
        .blue-btn-bg-style {
            background-color: #b6daf4;
            height: 35px;
        }

        .company-info-name-style {
            font-family: 'Raleway', sans-serif;
            color: #666666;
            text-align:left;
            font-size:18px;
            font-weight:600;
            background-color: #77c6f1;
            padding-left: 20px;
            height:40px;
        }
        .company-info-font-style {
            font-family: 'Raleway', sans-serif;
            color: #e0f7fe;
            text-align:left;
            font-size:18px;
            font-weight:600;
            padding-left: 20px;
        }
        .company-info-contacts-style {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align:left;
            font-size:18px;
            font-weight:600;

        }
        .contacts-001-style {
            background-color: #fff;
            width: 780px;
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align:left;
            font-size:12px;
            font-weight:600;
            padding-left: 10px;
            padding-top: 5px;
            height:25px
        }
        .contacts-002-style {
            background-color: #fff;
            width: 100px;
        }
        .contacts-003-style {
            background-color: #fff;
            width: 100px;
        }
        .contacts-004-style {
            background-color: #fff;
            width: 100px;
            font-family: 'Raleway', sans-serif;
            padding-bottom: 10px;
            padding-left: 10px;
            border-bottom-width: 1px;
            border-bottom-color: #d9e6ee;
            border-bottom-style: solid;
        }
        .contacts-005-style {
            background-color: #fff;
            width: 100px;
            border-bottom-width: 1px;
            border-bottom-color: #d9e6ee;
            border-bottom-style: solid;
            padding-bottom: 10px;
            text-align: right;
            padding-right: 7px;
        }
        .contacts-006-style {
            background-color: #ececec;
            width: 100px;
            padding-right: 10px;
            text-align: right;
            border-bottom-width: 1px;
            border-bottom-color: #d9e6ee;
            border-bottom-style: solid;
            padding-bottom: 10px;
        }
        .contacts-btn-create-ordr {
            font-family: 'Raleway', sans-serif;
            font-size: 10px;
            color: #ffffff;
            font-weight: 600;
            background-color: #4fc2bd;
            border: 1px solid #4fc2bd;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        .contacts-btn-edit {
            font-family: 'Raleway', sans-serif;
            font-size: 10px;
            color: #ffffff;
            font-weight: 600;
            background-color: #4fc2bd;
            border: 1px solid #4fc2bd;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
        }
        .add-new-contact-btn {
            font-family: 'Raleway', sans-serif;
            font-size: 11px;
            color: #ffffff;
            font-weight: 600;
            background-color: #4fc2bd;
            border: 1px solid #4fc2bd;
            padding-left: 10px;
            padding-right: 10px;
            padding-top: 5px;
            padding-bottom: 5px;
            height: 30px;
            width:150px;
        }
        .btn-company-printers-style {
            height: 40px;
            width: 40px;
            background-color: #77c6f1;
            border-left-width: 1px;
            border-left-color: #dbe8f1;
            border-left-style: solid;
            text-align: center;
            vertical-align: middle;
        }
        .btn-company-edit-style {
            height: 40px;
            width: 40px;
            background-color: #77c6f1;
            border-left-width: 1px;
            border-left-color: #dbe8f1;
            border-left-style: solid;
            text-align: center;
            vertical-align: middle;
        }
        .btn-company-info-style {
            height: 40px;
            width: 40px;
            background-color: #77c6f1;
            border-left-width: 1px;
            border-left-color: #dbe8f1;
            border-left-style: solid;
            text-align: center;
            vertical-align: middle;
        }
        .btn-company-printers-style:hover {
            background-color: #61b3e5;
            cursor: pointer;
        } 
        .btn-company-info-style:hover {
            background-color: #61b3e5;
            cursor: pointer;
        }
        .btn-company-edit-style:hover {
            background-color: #61b3e5;
            cursor: pointer;
        }        
        .brdr-btm-style {
            background-color: #fff;
            border-bottom-width: 1px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
        }
        .bg-only-01-style {
            background-color: #dbe8f1;
            padding-left: 20px;
            height: 40px;
            font-family: 'Raleway', sans-serif;
            font-size: 12px;
        }
        .bg-only-01x-style {
            background-color: #dbe8f1;
        }
        .bg-only-02-style {
            background-color: #77c6f1;
        }
        .form-margin-style {
            background-color: #77c6f1;
            margin-top: -15px;
        }

        .auto-style1 {
            width: 265px;
        }


        #EditButton {
            width: 101px;
        }
        #Website {
            width: 529px;
        }


        </style>

    <script type="text/javascript">

        function Edit(CompanyID) {
            $('#iframeEditCompany').attr('src', 'Manage/ViewEditScreens/ViewEditCustomers.aspx?cid=' + CompanyID);
            $('#editCompanyIframeWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT CUSTOMER',
                width: 710,
            });

            return false;

        }
        
        function showCompanyPrinters() {
            window.parent.ShowCompPrinters($('#CompID').val());
        }

        function ViewCompDetails() {
            window.parent.OpenCompany($('#CompID').val());
        }

        

        function AddContact() {
            window.parent.AddContact($('#CompID').val(), "DASH");
        }
        
        $(document).ready(function () {
            
        });
    </script>
</head>
<body>
   
    <form id="form1" runat="server" class="form-margin-style">
        <div> 
        <table align="center" cellpadding="0" cellspacing="0" class="width-980px-style">
            
            <tr class="company-info-name-style">
                <td>
                    <table align="center" cellpadding="0" cellspacing="0" class="width-980px-style">
                        <tr>
                            <td class="company-info-font-style"><div id="CompanyName" runat="server"></div></td>
                            <td>
                                <table align="right" cellpadding="0" cellspacing="0" class="auto-style1">
                                    <tr>
                                        <td>&nbsp;</td>
                                        
                                        <td class="btn-company-printers-style" onclick="showCompanyPrinters()">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/btn_03.png" width="24" height="24"/>
                                        </td>
                                        
                                        <td class="btn-company-edit-style">
                                            <div id="EditCompDIV" runat="server">
                                            </div>
                                        </td>

                                        <td class="btn-company-info-style" onclick="ViewCompDetails()">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/btn_01.png" width="24" height="24"/>
                                        </td>

                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table align="center" cellpadding="0" cellspacing="0" class="width-980px-style">
                        <tr>
                            <td class="bg-only-01-style"><div id="Website" runat="server"></div></td>
                            <td class="bg-only-01x-style"><div id="EditButton" runat="server"></div></td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <input type="text" name="CompID" id="CompID" hidden="hidden" runat="server" /></td>
            </tr>

            <tr>
                <td>
                    <table align="center" cellpadding="0" cellspacing="0" class="width-980px-style" id="OverallContactTbl">
                        <tr>
                            <td>
                                <table align="center" cellpadding="0" cellspacing="0" class="width-980px-style" >
                                   
                        <%=populateContacts()%>

                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        </table>
                </td>
            </tr>
            <tr>
                <td align="right"><input name='AddNewContact' type='button' class="add-new-contact-btn" id='AddNewContact' value='ADD NEW CONTACT' onclick="AddContact();"/></td>
            </tr>
            </table>
    </div>
    </form>

    <div id="editCompanyIframeWindow" style="display:none">
         <iframe id="iframeEditCompany" width="710" height="400"style="border:0px;"></iframe>   
    </div>

    

</body>
</html>
