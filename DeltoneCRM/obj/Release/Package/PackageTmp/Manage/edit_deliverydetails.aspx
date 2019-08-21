<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_deliverydetails.aspx.cs" Inherits="DeltoneCRM.Manage.edit_deliverydetails" MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-1.11.1.min.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="../css/Overall.css" rel="stylesheet" />
    
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

        <style type="text/css">
        .width-980-style {
            width: 980px;
        }
        .width-940-style {
            width: 940px;
        }

    </style>

    <script type="text/javascript">

        function closeIframe() {
            $('#CreateAddWindow').dialog('close');
            return false;
        }

        function Edit(DeliveryID) {
            $('#viewiframe').attr('src', 'ViewEditScreens/ViewEditDeliveryItem.aspx?cid=' + DeliveryID);
            ViewEditDialog = $('#ViewEditWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT DELIVERY DETAILS',
                width: 710,
            });

            return false;
        }

      


        function closeEditWindow() {
            ViewEditDialog.dialog("close");
        }

        function CreateAddWindow() {
            $('#createiframe').attr('src', 'adddeliveryitem.aspx');
            $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'NEW DELIVERY ITEM',
                width: 710,
            });

            return false;
        }

        $(document).ready(function () {

            $('#example').dataTable({
                "ajax": "../Fetch/FetchAllDelDetailsForManage.aspx",
                "columnDefs": [
                    { className: 'align_left', "targets": [0, 1, 2] },
                    { className: 'align_center', "targets": [3, 4] },
                ]
            });
        });
    </script>
    </asp:Content>



<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
    <asp:Button runat="server" OnClientClick="return CreateAddWindow();" Text="CREATE NEW DELIVERY ITEM" class="top-links-button" />
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
            <td class="section_headings">DELIVERY DETAILS LIST</td>
        </tr>
        <tr>
            <td class="white-box-outline">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    <tr>
                        <td height="20px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <table id="example" >

                    <thead>
                        <tr>
                            <th>Delivery ID</th>
                            <th>Delivery Details</th>
			                <th>Cost</th>
			                <th>Active</th>
                            <th>VIEW/EDIT</th>
                            <!--Get the Hidden Order ID and Contact ID-->

                        </tr>
                    </thead>

                    <tbody>
		 
	                </tbody>
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
            <td height="25px">&nbsp;</td>
        </tr>
    </table>

    <div id="ViewEditWindow" style="display:none">
        <iframe id="viewiframe" width="710" height="143" style="border:0px;"></iframe>   
    </div>

    <div id="CreateAddWindow" style="display:none">
        <iframe id="createiframe" width="710" height="140" style="border:0px;"></iframe>   
    </div>
    </asp:Content>
