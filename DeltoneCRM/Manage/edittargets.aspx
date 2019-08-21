<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edittargets.aspx.cs" Inherits="DeltoneCRM.Manage.edittargets" MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
<link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="../js/jquery-1.11.1.min.js"></script>
    <link href="../css/NewCSS.css" rel="stylesheet" />
    <script src="../Scripts/jquery-ui.js"></script>

    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>
    <script src="../js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/jquery.dataTables_new.css" />

     <link href="../Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />
    <script src="../Scripts/jscolor.js"></script>
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />

    <!--Jquery UI References-->
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>

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
            $('#EditWindow').dialog('close');
            return false;
        }

        function Edit(TargetID) {
            $('#viewiframe').attr('src', 'ViewEditScreens/ViewEditTarget.aspx?cid=' + TargetID);
            ViewEditDialog = $('#ViewEditWindow').dialog({
                resizable: false,
                modal: true,
                title: 'VIEW/EDIT Target',
                width: 710,
            });
        }

        function closeEditWindow() {
            ViewEditDialog.dialog("close");
        }

        function CreateAddWindow() {
            $('#createiframe').attr('src', 'addtarget.aspx');
            $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'NEW STAFF TARGET',
                width: 710,
            });

            return false;
        }

        function CreateAddPublicWindow() {

            $('#createpubliciframe').attr('src', 'addpublicholidays.aspx');
            $('#CreateAddpublicWindow').dialog({
                resizable: false,
                modal: true,
                title: 'ADD PUBLIC HOLIDAYS',
                width: 710,
            });

            return false;
        }


        $(document).ready(function () {

            $('#example').dataTable({
                "ajax": "../Fetch/FetchAllTargets.aspx",
                "columnDefs": [
                   
                    { className: 'align_center', "targets": [0,1,2, 3, 4,5,6] },
                ],
                "order": [[0, "desc"]],
                "iDisplayLength": 50,
            });
        });


        function CreateAddTargetStatsConfigure() {

            $('#CreateAddstatstargetiframe').attr('src', 'SetStatsConfigure.aspx');
            $('#CreateAddstatstargetWindow').dialog({
                resizable: false,
                modal: true,
                title: 'SET TARGET CONFIG',
                width: 710,
            });

            return false;

        }
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
    <asp:Button ID="Button1" runat="server" OnClientClick="return CreateAddWindow();" Text="ADD TARGET" class="top-links-button"/>
                            
                           
    <asp:Button ID="Button2" runat="server" OnClientClick="return CreateAddPublicWindow();" Text="ADD PUBLIC HOLIDAYS" class="top-links-button"/>

                                  <asp:Button ID="Button3" runat="server"  OnClientClick="return CreateAddTargetStatsConfigure();" 
                                      Text="Configure Stats Target" class="top-links-button"/>
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
                <td class="section_headings">TARGET LIST FOR
                    <asp:Label ID="lblMonthName" runat="server"></asp:Label>
                </td>
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
                            <th>Target ID</th>
                            <th>User</th>
                            <th>Target Year </th>
                            <th>Target Month</th>
                            <th>Target Commission</th>
                            <th>Working Days</th>
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
   <iframe id="viewiframe" width="710" height="400"style="border:0px;"></iframe>   
</div>

    <div id="CreateAddWindow" style="display:none">
   <iframe id="createiframe" width="710" height="400"style="border:0px;"></iframe>   
</div>

    <div id="CreateAddpublicWindow" style="display:none">
   <iframe id="createpubliciframe" width="710" height="400"style="border:0px;"></iframe>   
</div>

         <div id="CreateAddstatstargetWindow" style="display:none">
   <iframe id="CreateAddstatstargetiframe" width="710" height="400"style="border:0px;"></iframe>   
</div>

        </form>
    </asp:Content>
