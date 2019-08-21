<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllCreditNotesApproved.aspx.cs" Inherits="DeltoneCRM.CreditNotes.AllCreditNotesApproved"  MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">

 <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
     <style type="text/css">
        .inner-table-size {
            width: 940px;
        }
         body {
    background-color: #FFFFCC !important;
}  
    </style>
     <script type="text/javascript">

         function openWin(CreditNoteID, ContID, CompID) {
             var url = '../UpdateCredit.aspx?CreditNoteID=' + encodeURIComponent(CreditNoteID) + '&cid=' + encodeURIComponent(ContID) + '&Compid=' + encodeURIComponent(CompID);
             window.open(url);
         }

         $(document).ready(function () {
             var table = $('#example').DataTable({
                 "ajax": "../Fetch/FetchApprovedCreditNotes.aspx",
                 "order": [[0, "desc"]],
                 "iDisplayLength": 25,
             });

         });
     </script>
</asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <div>

          <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">ALLCREDIT NOTES - APPROVED</td>
        </tr>

        <tr>

            <td class="section_headings"><a href="AllCreditNotes.aspx">PENDING</a> | <a href="AllCreditNotesApproved.aspx">APPROVED</a></td>
         
        <tr>
          <td class="white-box-outline">
              <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                  <tr>
                      <td height="20px">&nbsp;</td>
                  </tr>

                  <tr>
                      <td>

                          <div class="container">
<table cellpadding="0" cellspacing="0" border="0" class="display" id="example" style="cursor:pointer">
	<thead>
		<tr>
            <th align="left">DETAILS</th>
			<th align="left">CREDIT_NOTE NO</th>
            <th align="left">COMPANY</th>
			<th align="left">CONCTACT PERSON</th>
			<th align="left">INVOICE NO</th>
            <th align="left">TOTAL</th>
            <th align="left">CREATED DATE</th>
             <th align="left">TYPE OF CREDIT</th>
            <th align="left">CREATED BY</th>
			<th align="left">STATUS</th>
            <th align="left">EDIT</th>
		</tr>
        
	</thead> 
            
	<tbody>
		 
	</tbody>

</table>

			</div>


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
    
    </div>
       </asp:Content>  
