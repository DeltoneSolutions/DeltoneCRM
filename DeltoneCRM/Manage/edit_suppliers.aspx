<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit_suppliers.aspx.cs" Inherits="DeltoneCRM.Manage.edit_suppliers" MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-1.11.1.min.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="../css/Overall.css" rel="stylesheet" />
    
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

    <style type="text/css">
        .width-980-style {
            width: 1200px;
        }
        .width-940-style {
            width: 1100px;
        }
    </style>

    <script type="text/javascript">


        function closeIframe() {
            $('#CreateAddWindow').dialog('close');
            return false;
        }

        function closeEditIframe() {
            $('#editIframeWindow').dialog('close');
            return false;
        }

        function closeEditWindow() {
            $('#editIframeWindow').dialog('close');
            return false;
        }

        function Edit(SupplierID) {
            $('#iframeEdit').attr('src', 'ViewEditScreens/ViewEditSupplier.aspx?suppid=' + SupplierID);
            $('#editIframeWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT SUPPLIER',
                width: 900,
            });

            return false;

        }

        function CreateAddWindow() {
            $('#createiframe').attr('src', 'addsupplier.aspx');
            $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'ADD SUPPLIER',
                width: 900,
            });

            return false;
        }

        $(document).ready(function () {

            $('#example').dataTable({
                "ajax": "../Fetch/FetchAllSuppliersForManage.aspx",
                "columnDefs": [
                    { className: 'align_left', "targets": [0, 1,2,4,5,6,7,8,9,10,11] },
                    { className: 'align_center', "targets": [3,12, 13] },
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
    <asp:Button ID="Button1" runat="server" OnClientClick="return CreateAddWindow();" Text="ADD NEW SUPPLIER" class="top-links-button" />
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
            <td class="section_headings">SUPPLIER LIST</td>
        </tr>
        <tr>
            <td class="white-box-outline">
                <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                    
                    <tr>
                        <td height="25px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td>

                            <table id="example" >

                    <thead>
                        <tr>
                            <th>Supplier ID</th>
                            <th>Supplier Name</th>
                             <th>Contact Name</th>
			                <th>Standard Delivery Cost</th>
                             <th>Address</th>
                             <th>City</th>
                             <th>Accounts Email</th>
                             <th>Sales Email</th>
                            <th>Return Email</th>
                             <th>Phone Number</th>
                             <th>Accounts Phone Number</th>
                             <th>Username</th>
                             <th>Password</th>
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
                        <td height="25px">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="25px">&nbsp;</td>
        </tr>
    </table>



    <div id="CreateAddWindow" style="display:none">
   <iframe id="createiframe" width="950" height="600"style="border:0px;"></iframe>   
</div>

    <div id="editIframeWindow" style="display:none">

         <iframe id="iframeEdit" width="950" height="600"style="border:0px;"></iframe>   

    </div>


    </asp:Content>