<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/SiteInternalNavLevel1.Master" CodeBehind="AllocatedLeadReport.aspx.cs" Inherits="DeltoneCRM.Manage.AllocatedLeadReport" %>

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

      


        $(document).ready(function () {
          
        });
       
       

        $(document).ready(function () {

            $('#example').dataTable({
                "ajax": "../Fetch/FetchAllocatedCompanies.aspx",
                "aaSorting": [[1, "desc"]],
                "iDisplayLength": 25,
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
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="section_headings">Allocated Company List</td>
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
                             <th>Lead Id</th>
                            <th>Company Name</th>
                             <th>Contact Name</th>
                             <th>Company Owner</th>
			                <th>From </th>
                             <th>To </th>
                              <th>Allocated Rep </th>
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

  

   
    </asp:Content>
