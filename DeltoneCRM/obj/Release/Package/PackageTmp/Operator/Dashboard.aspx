<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="DeltoneCRM.Operator.Dashboard" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <title>DeltoneCRM - Dashboard</title>
     <link href="/css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>

    <link href='http://fonts.googleapis.com/css?family=Yanone+Kaffeesatz:400,700,300,200' rel='stylesheet' type='text/css'>

    <script src="/js/jquery-1.9.1.js"></script>
	<script src="/js/jquery-ui-1.10.3.custom.js"></script>
    <link href="/css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>

    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
   <!-- <link rel="stylesheet" type="text/css" href="/deltonecrm/cdn.datatables.net/1.10.5/css/jquery.dataTables.css"/>-->
    <script src="/js/jquery-ui-1.10.3.custom.js"></script>

<script type="text/javascript">
    var dialog = '';
    $(document).ready(function () {

        $('#findcustomer').autocomplete({

            source: "/Fetch/FetchAllCompanies.aspx",
            select: function (event, ui) {
                window.open('ConpanyInfo.aspx?companyid=' + ui.item.id)
            }
        });

        $('#example').dataTable({

            "ajax": "/Fetch/FetchOrdersForApproval.aspx",
            "columnDefs": [
            {
                "targets": [0],
                "visible": true,
                "searchable": true
            },

                                { className: 'align_left', "targets": [1, 2] },
                                { className: 'align_center', "targets": [0, 3, 4] },

            ]

        });


    });

    //This Function Approve the Order
    function ApproveOrder(OrderID, CompnayID, ContactID) {

        //Naviagete to the Order form 
        window.open('Order.aspx?Oderid=' + OrderID + '&cid=' + ContactID + '&Compid=' + CompnayID + '&Action=PendingApproval');
    }
    //End Function Approve the Order

</script>


    <style type="text/css">
        .auto-style1 {
            width: 980px;
        }
        .auto-style3 {
            width: 940px;
        }
        .auto-style4 {
            width: 978px;
        }
        .db1-auto-style5 {
            height: 35px;
            width:20px;
        }


        </style>


</asp:Content>



<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
   
     <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
         <tr>
             <td height="25px">&nbsp;</td>
         </tr>
         <tr>
             <td class="white-box-outline">

                 <table align="center" cellpadding="0" cellspacing="0" class="auto-style3">
                     <tr>
                         <td height="20px">&nbsp;</td>
                     </tr>
                     <tr>
                         <td>

                             <table width="940" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td class="auto-style5"><input name="findcustomer" type="text" class="main_search" id="findcustomer" size="60" placeholder="CUSTOMER QUICK SEARCH" /></td>
            <td width="10" class="db1-auto-style5"></td>
            <td width="200" align="center" class="btn_main_add_company" onclick="window.open('AddContact.aspx','_blank')">NEW CUSTOMER</td>
            </tr>
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




         <tr>
             <td class="section_headings">ORDERS PENDING APPROVAL</td>
         </tr>
         <tr>
             <td class="white-box-outline">
                 <table align="center" cellpadding="0" cellspacing="0" class="auto-style3">
                     <tr>
                         <td height="20px">&nbsp;</td>
                     </tr>
                     <tr>
                         <td>

                                              <!--Orders pending Approval Panel-->
   <asp:Panel ID="pnlPendingApproval" runat="server" Visible="false">

            

            <table id="example">

                    <thead>
                        <tr>
                            <th width="60px">ORDER NUM</th>
			                <th>ORDER DATE/TIME</th>
                            <th width="100px">CREATED BY</th>
                            <th width="60px">STATUS</th>
			                <th width="50px">APPROVE</th>
                            <!--Get the Hidden Order ID and Contact ID-->

                        </tr>
                    </thead>

                    <tbody>
		 
	                </tbody>
            </table>
  </asp:Panel>
    <!--End Orders Pending Approval Panel-->

                         </td>
                     </tr>
                     <tr>
                         <td height="20px">&nbsp;</td>
                     </tr>

                 </table>
             </td>
         </tr>
         <tr>
             <td class="auto-style4">&nbsp;</td>
         </tr>

         <tr>
             <td class="auto-style4">&nbsp;</td>
         </tr>
    </table>


    


   <asp:Panel ID="pnlMsgOrderApproval" runat="server" BorderColor="green" Visible="false">
       <asp:Label ID="lblOrderApproval" runat="server"   ForeColor="Green"></asp:Label>
   </asp:Panel>

</asp:Content>

