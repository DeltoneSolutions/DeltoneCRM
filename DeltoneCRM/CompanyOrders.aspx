<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyOrders.aspx.cs" Inherits="DeltoneCRM.CompanyOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
<link href="css/jquery.dataTables_new.css" rel="stylesheet" />
<script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

<script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
<script src="js/jquery-ui-1.10.3.custom.js"></script>
<style type="text/css">

    td {
        font-family: 'PT Sans Narrow', sans-serif;
        font-size: 12px;
        color:#333;
    }

    body{
        font-family:Arial, Helvetica, sans-serif; 
        font-size:13px;
        }
.info, .success, .warning, .error, .validation {
            border: 1px solid;
            margin: 10px 0px;
            padding:15px 10px 15px 50px;
            background-repeat: no-repeat;
            background-position: 10px center;
   }

   .success {
        color: #4F8A10;
        background-color: #DFF2BF;
        background-image:url('success.png');
       }

    .lbl {
        font-weight:bold;
    }

</style>
<script type="text/javascript">

    $(document).ready(function () {

        company = $('#<%=hdnCompanyID.ClientID%>').val();
        contact = $('#<%=hdnContactID.ClientID%>').val();

        $('#example').dataTable({

            "ajax": "Fetch/FetchOrders.aspx?CompanyID=" + company + "&ContactID=" + contact,
            "columnDefs": [
            {
            "targets": [0],
            "visible": true,
            "searchable": true
            },

            ]

        });

        $('#example').on('click', 'tbody tr', function (e) {
            var OrderID = $('td', this).eq(0).text();
            
        });
        //CompanyID, ContactID
    });


    //This function Load The Order in view Mode
    function ViewOrder(OrderID, CompanyID, ContactID) {
       //window.open("Order.aspx?Oderid=" + OrderID + "&cid=" + ContactID + "&Compid=" + CompanyID);
    }

    //End function Load Order in View Mode


    //This Function Load the Edit View
    function EditOrder(OrderID, CompanyID, ContactID) {
        window.open("Order.aspx?Oderid=" + OrderID + "&cid=" + ContactID + "&Compid=" + CompanyID);
    }
    //End Function load Edit view


</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2>Company Orders</h2>

  <div id="divDisplay"  runat="server" class="success"  >

       <asp:Label ID="lblMessage" runat="server" CssClass="lbl"></asp:Label>

   </div>

    <div id="divCompanyOrders">

            <asp:Button ID="btnCrateOrder" runat="server" Text="CREATE NEW" OnClick="btnCrateOrder_Click" /><br />
            <!--hidden Files for ComapanyId and ContactId-->
            <input type="hidden" value="" runat="server" id="hdnCompanyID" />
            <input type="hidden" value="" runat="server" id="hdnContactID" />
            <!--End Hidden Fields-->

            <table  class="display" id="example">

                <thead>
		            <tr>
       		            <th>Order ID</th>
			            <th>Order DateTime</th>
                        <th>Created By</th>
			            <th>Edit</th>
		            </tr>
        
	            </thead>
     
	            <tbody>
		 
	            </tbody>

            </table>
    </div>

       <asp:Button ID="btnGo" runat="server"  OnClick="btnGo_Click"  Text="Submit"  Visible="false" />
 
</asp:Content>
