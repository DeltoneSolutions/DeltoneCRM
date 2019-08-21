<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllOrders.aspx.cs" Inherits="DeltoneCRM.Orders.AllOrders" MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-latest.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">

        //URL Encode has to done here
        function openWin(OrderID, ContID, CompID)
        {
            var url = '../order.aspx?Oderid=' + encodeURIComponent(OrderID) + '&cid=' + encodeURIComponent(ContID) + '&Compid=' + encodeURIComponent(CompID);
            window.open(url);
        }

        function format(OrderID)
        {
          
            //Make the Ajax Request and Get the Error or Success Message
            var RESULT = '';
            var MESSAGE = '';

            //Make the Ajax Request and Get the Error or Success Message
            /*$.ajax({
                type: "POST",
                url: "../Process/Process_FetchLOG.aspx",
                async: false,
                data: {
                    Order_ID: OrderID,
                },
                success: function (msg) {

                    alert(msg);
                    if (msg)
                    {
                        RESULT = msg.split(':', 3);
                        MESSAGE = msg.split(':', 0);
                        alert(RESULT + ':' + MESSAGE);
                    }
                },
                error: function (xhr, err)
                {
                    alert(err);
                },
            });*/


            return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                '<tr>' +
                    '<td>Order:</td>' +
                    '<td></td>' +
                '</tr>' +
                '<tr>' +
                    '<td></td>' +
                    '<td></td>' +
                '</tr>' +
                '<tr>' +
                    '<td></td>' +
                    '<td></td>' +
                '</tr>' +
            '</table>';
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
                    $(this).val(''); return false;


                }
            });
                
            var param = getParameterByName("s");
            if (param == null)
                param = "3";
            var table = $('#example').DataTable({
                processing: true,
                "language": {
                    "processing": "Hang on. Waiting for response..." //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
               
                columns: [
                   { 'data': 'OrderNo' },
                   { 'data': 'CompanyName' },
                   { 'data': 'ContactPerson' },
                   { 'data': 'InvNumber' },
                   { 'data': 'OrderTotal' },
                   { 'data': 'CreatedDate' },
                   { 'data': 'OrderedDate' },
                   { 'data': 'DueDate' },
                   { 'data': 'CreatedBy' },
                   { 'data': 'Status' },
                   { 'data': 'SupplierNotes' },
                   { 'data': 'ViewEdit' },
                ],
                "iDisplayLength": 25,
                "order": [[0, "desc"]],
                fnServerParams: function (aoData) {
                    aoData.push(
                        { "name": "s", "value": param }
                    );
                },
                sAjaxSource: "../DataHandlers/OrderManageDisplayHandler.ashx",
            });

            function getParameterByName(name, url) {
                if (!url) url = window.location.href;
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }

            $('#example tbody').on('click', '.details', function () {
               
                var tr = $(this).closest('tr');
                var element = $(this).closest('td').next();
                
                // var OrderID = tr.find('td:first').text();
                var OrderID = element.text();

                var row = table.row(tr);
                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    // Open this row
                    row.child(format(OrderID)).show();
                    tr.addClass('shown');
                }
            });

           
        });
    </script>
    <style type="text/css">
        .auto-style2 {
            height: 25px;
        }
        .inner-table-size {
            width: 940px;
        }

        body {
    background-color: #B2DFEE !important;
}
    </style>
    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">PENDING - APPROVED - BACK ORDER - EOM - INHOUSE - COMPLETED - ALL</td>
        </tr>

        <tr>
            <td  class="section_headings"><a href="AllOrders.aspx?s=3">PENDING</a> 
                | <a href="AllOrdersApproved.aspx">APPROVED</a>| <a href="AllOrders.aspx?s=5">BACK ORDER</a> | <a href="AllOrders.aspx?s=6">EOM</a> | <a href="AllOrders.aspx?s=7">INHOUSE</a>   | <a href="AllOrders.aspx?s=1">COMPLETED</a>|<a href="AllOrders.aspx?s=4">ALL</a></td>
        </tr>

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
			<th align="left">ORDER NO</th>
            <th align="left">COMPANY</th>
			<th align="left">CONCTACT PERSON</th>
			<th align="left">INVOICE NO</th>
            <th align="left">TOTAL</th>
             <th align="left">ORDERED</th>
            <th align="left">INVOICE </th>
            <th align="left">DUE </th>
            <th align="left">CREATED BY</th>
			<th align="left">STATUS</th>
            <th align="left">SUP STAT</th>
            <th align="left">EDIT</th>
		</tr>
        
	</thead>
     <tr>
            
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

     </asp:Content>