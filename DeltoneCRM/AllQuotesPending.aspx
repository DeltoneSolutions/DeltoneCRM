<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="AllQuotesPending.aspx.cs" Inherits="DeltoneCRM.AllQuotesPending" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="js/jquery-latest.js"></script>
	<script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/jquery.dataTables_new.css" rel="stylesheet" />
    
    <script src="js/jquery.dataTables.min.js"></script>
    <script type="text/javascript">

        function Edit(QuoteID, CompanyID, ContactID) {
            $.ajax({
                url: 'Fetch/FetchDBType.aspx',
                data: {
                    Flag: QuoteID,
                },
                success: function (response) {
                    window.open("quote.aspx?OderID=" + QuoteID + "&CompID=" + CompanyID + "&cid=" + ContactID + "&DB=" + response + "&Flag=Y");
                }
            })
        }

        $(document).ready(function () {
            var livetable = $('#livedbtbl').DataTable({
                columns: [
                    { 'data': 'QuoteID' },
                    { 'data': 'QuoteDate' },
                    { 'data': 'CompanyName' },
                    { 'data': 'ContactName' },
                    { 'data': 'QuoteTotal' },
                    { 'data': 'QuotedBy' },
                    { 'data': 'CustomerType' },
                    { 'data': 'QuoteStatus' },
                    { 'data': 'View' },
                ],
                bServerSide: true,
                "iDisplayLength": 25,
                sAjaxSource: 'DataHandlers/AllQuotesDataHandler.ashx',
                "fnServerParams": function (aoData) {
                    aoData.push(
                        { "name": "sStatus", "value": "PENDING" }
                    );
                }
            });

            

        });

        </script>
    <style type="text/css">
        .auto-style1 {
            background-color: #FFF;
            border: 1px solid #CCCCCC;
            width: 978px;
            height: 63px;
        }
        body {
    background-color: #A0E5A0 !important;
}  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <table width="1000" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">ALL QUOTES - PENDING</td>
        </tr>

        <tr>
            <td  class="section_headings"><a href="AllQuotes.aspx">ACTIVE</a>
        </tr>

            <tr>
          <td class="white-box-outline">
              <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                  <tr>
                      <td height="20px">&nbsp;</td>
                  </tr>

        <tr>
          <td>


                        
<table cellpadding="0" width="900" cellspacing="0" border="0" class="display" id="livedbtbl" style="cursor:pointer">
	<thead>
		<tr>
            <th>QUOTE ID</th>
                                                    <th>CREATED DATE</th>
            <th>COMPANY NAME</th>
                                                    <th>CONTACT NAME</th>
			                                        <th>QUOTE TOTAL</th>
            <th>QUOTED BY</th>
            <th>CUSTOMER TYPE</th>
                                                    <th>STATUS</th>
                                                    <th>VIEW</th>
		</tr>
        
	</thead>
     <tr>
            
	<tbody>
		 
	</tbody>

</table>

			
            </td>
        </tr>
  </table>
            </td>
        </tr>
        <tr>
          <td height="25px">&nbsp;</td>
        </tr>

       


      </table>
</asp:Content>
