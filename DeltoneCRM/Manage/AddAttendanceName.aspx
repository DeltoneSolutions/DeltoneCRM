<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAttendanceName.aspx.cs"  MasterPageFile="~/SiteInternalNavLevel1.Master" Inherits="DeltoneCRM.Manage.AddAttendanceName" %>

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
            $('#EditWindow').dialog('close');
            return false;
        }

        function CreateAddWindow() {
            $('#createiframe').attr('src', 'addattuser.aspx');
            $('#CreateAddWindow').dialog({
                resizable: false,
                modal: true,
                title: 'NEW LOGIN',
                width: 710,
            });

            return false;
        }



        $(document).ready(function () {
            $('#findcustomerorder').autocomplete({
                source: "../Fetch/FetchSearchOrder.aspx",
                select: function (event, ui) {
                    //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                    // $('#CompanyContactTR').show();
                    // $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                    var valsSPLIT = ui.item.id.split(',');;

                    var url = '../order.aspx?Oderid=' + (valsSPLIT[0]) + '&cid=' + (valsSPLIT[2])
                            + '&Compid=' + (valsSPLIT[1]);
                    window.open(url, "_blank");
                    // $(this).val(''); return false;


                }
            });
        });
       
        function closeEditWindow() {
            ViewEditWindow.dialog("close");
        }


        function DeleteCal(eveID) {

            var r = confirm("Do you want to delete this record?");
            if (r == true) {
                PageMethods.DeleteRecordCS(eveID, deletestatus);
            }
        }

        function deletestatus(addResult) {

            alert("Name Successfully Deleted.");
            location.reload();
        }

        $(document).ready(function () {

            $('#example').dataTable({
                "ajax": "../Fetch/FetchAllUsersAttendanceManage.aspx",
                "columnDefs": [
                    { className: 'align_left', "targets": [0] },
                    { className: 'align_center', "targets": [1,2] },
                ]
            });
        });
    </script>
    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManagerforsalesRep111" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
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
    <asp:Button ID="Button1" runat="server" OnClientClick="return CreateAddWindow();" Text="ADD NEW NAME" class="top-links-button" />
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
            <td class="section_headings">USER LIST</td>
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
                            <th>ID</th>
                            <th>Name</th>
			                <th>Delete</th>
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

    <div id="CreateAddWindow" style="display:none">
        <iframe id="createiframe" width="710" height="286"style="border:0px;"></iframe>   
    </div>

    <div id="EditLoginWindow"  style="display:none">
        <iframe id="IframeEditLogin" width="710" height="364"style="border:0px;"></iframe>  
    </div>

   
    </asp:Content>
