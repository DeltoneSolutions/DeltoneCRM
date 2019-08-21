<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editcustomers.aspx.cs" Inherits="DeltoneCRM.Manage.editcustomers" MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-2.1.3.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="../js/jquery.validate.js"></script>
    <link href="../css/Overall.css" rel="stylesheet" />
    
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

    <style type="text/css">
        .width-980-style {
            width: 980px;
        }
        .width-940-style {
            width: 940px;
        }

        .ui-widget-content.ui-dialog {
            border: 1px solid #000 !important;
        }

    </style>

    <script type="text/javascript">


        //Close the Iframe
        function closeIframe() {
            $('#ViewEditWindow').dialog('close');
            return false;
        }

        function closeCreateFrame() {
            $('#CreateAddWindow').dialog('close');
        }

        function switchToCreateContactWithFirstNameAndLastNameAndEmail(CompID,FirstName,LastName,Email) {
            $('.blackout').css("display", "none");
            $('#CreateAddWindow').dialog('close');
            $('#addcontactiframe').attr('src', 'addcontact.aspx?cid=' + CompID + "&loc=MANAGE" + "&fNa=" + FirstName + "&lNa=" + LastName + "&cEm=" + Email);
          //  $('iframe[name=addcontactiframe]').contents().find("#NewFirstName").val(FirstName);
           // $('iframe[name=addcontactiframe]').contents().find("#NewLastName").val(LastName);
           // $('iframe[name=addcontactiframe]').contents().find("#NewEmailAddy").val(Email);

            $('#CreateAddContact').dialog({
                resizable: false,
                modal: true,
                title: 'NEW CONTACT',
                width: 900,
            });
            
          //  console.log('test' + FirstName + LastName + Email);

          //  console.log($('iframe[name=addcontactiframe]').contents().find("#NewFirstName"));

          

            $('#CreateAddContact').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            return false;
        }

        function switchToCreateContact(CompID) {
            $('.blackout').css("display", "none");
            $('#CreateAddWindow').dialog('close');
            $('#addcontactiframe').attr('src', 'addcontact.aspx?cid=' + CompID + "&loc=MANAGE");
            $('#CreateAddContact').dialog({
                resizable: false,
                modal: true,
                title: 'NEW CONTACT',
                width: 900,
            });


            $('#CreateAddContact').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            return false;
        }

        function closeCreateContact() {
            $('.blackout').css("display", "none");
            window.location.reload();
        }

        function closeCreateContactAndOpenOrder(ContactID, CompanyID) {
            
            window.open("../order.aspx?cid=" + ContactID + "&Compid=" + CompanyID);
            window.location.reload();
        }


        function Edit(CompanyID) {
            $('#viewiframe').attr('src', 'ViewEditScreens/vieweditcustomers.aspx?cid=' + CompanyID + "&loc=MANAGE");
            ViewEditDialog = $('#ViewEditWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT COMPANY',
                width: 900,
            });

            $('#ViewEditWindow').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });


            return false;
        }

        function closeEditWindow() {
            ViewEditDialog.dialog("close");
        }

        function CreateAddWindow() {
            $('#createiframe').attr('src', 'addcustomer.aspx');
            $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'NEW CUSTOMER',
                width: 950,
                height:580
            });

            $('#CreateAddWindow').bind('dialogclose', function (event) {
                $('.blackout').css("display", "none");
            });

            return false;
        }


        $(document).ready(function () {

            $('#example').dataTable({
                "ajax": "../Fetch/FetchAllCompaniesForManage.aspx",
                "iDisplayLength": 25,
                "columnDefs": [
                    { className: 'align_left', "targets": [0, 1, 2, 3] },
                    { className: 'align_center', "targets": [4, 5] },
                ]
            });
        });
    </script>
    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form2" name="form2" method="post" action="savecompany.aspx">
        <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
            <tr>
                <td height="25px">&nbsp;</td>
            </tr>
            <tr>
                <td class="top-links-box">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" runat="server" OnClientClick="return CreateAddWindow();" Text="ADD COMPANY" class="top-links-button"/>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="section_headings">COMPANY LIST</td>
            </tr>
            <tr>
                <td class="white-box-outline">
                    <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>

                                <asp:Panel ID="pnlPromoList" runat="server" Visible="true">

            <table id="example" >

                    <thead>
                        <tr>
                            <th>Company ID</th>
                            <th>Company Name</th>
                            <th>Account Owner</th>
			                <th>Company Website</th>
			                <th>Active</th>
                            <th>VIEW/EDIT</th>

                        </tr>
                    </thead>

                    <tbody>
		 
	                </tbody>
            </table>
  </asp:Panel>

                            </td>
                        </tr>
                        <tr>
                            <td height="20px">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td height="25px">&nbsp;</td>
            </tr>
        </table>
        <br />
    

<div id="ViewEditWindow" style="display:none">
   <iframe id="viewiframe" width="950" height="400"style="border:0px;"></iframe>   
</div>

    <div id="CreateAddWindow" style="display:none">
   <iframe id="createiframe" width="900" height="500"style="border:1px;padding:8px 8px 8px 8px"></iframe>   
</div>

        <div id="CreateAddContact" style="display:none">
   <iframe id="addcontactiframe" name="addcontactiframe" width="950" height="700"style="border:0px;"></iframe>   
</div>


        </form>
    </asp:Content>
