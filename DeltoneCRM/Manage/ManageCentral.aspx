<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageCentral.aspx.cs" Inherits="DeltoneCRM.Manage.ManageCentral" MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
   <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-1.11.1.min.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/Overall.css" rel="stylesheet" />
    <style>
        .manage_btn_01 {
	        font-family: 'Open Sans', sans-serif;
	        font-size: 12px;
	        color: #EEEEEE;
	        text-decoration: none;
	        font-weight: 600;
	        background:#eb473d;
        }
        .manage_btn_01:hover {
	        cursor:pointer;
	        background:#eb271b;
        }
        .width-930-style {
            width: 930px;
        }

        .width-980-style {
            width: 980px;
        }
        .main-btns {
            width: 180px;
            height: 40px;
	        font-family: 'Open Sans', sans-serif;
	        font-size: 12px;
	        color: #eb271b;
	        text-decoration: none;
	        font-weight: 600;
            border: 1px solid #eb271b;
            background-color: #ffffff;
            text-align: center;
        }
        .main-btns:hover {
            color: #ffffff;
            border: 1px solid #eb271b;
            background-color: #eb271b;
            cursor: pointer;
        }
        .all-headings-style {
            font-family: 'Droid Sans', sans-serif;
            font-size: 2.1em;
            color: #6b6b6b;
            font-weight: 400;
            letter-spacing: -1px;
        }

    </style>

    <link href='http://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'>

     <script type="text/javascript">
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
        });
    </script>

    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
        <tr>
          <td height="25" align="right">&nbsp;</td>
        </tr>
        <tr>
          <td class="all-headings-style">Manage</td>
        </tr>
        <tr>
          <td height="20px"></td>
        </tr>
        <tr>
          <td height="25" class="white-box-outline" align="center">
              <table cellpadding="0" cellspacing="0" class="width-930-style">
                  <tr>
                      <td height="25px">

                      </td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                  </tr>
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
          <td>&nbsp</td>
        </tr>
        <tr>
          <td>
              <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                  <tr>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('edit_items.aspx','_self')">PRODUCTS</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('edit_suppliers.aspx','_self')">SUPPLIERS</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('edit_promotionalitems.aspx','_self')">PROMO ITEMS</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('edit_deliverydetails.aspx','_self')">DELIVERY</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('edittargets.aspx','_self')">TARGETS</td>
                  </tr>
              </table>
            </td>
        </tr>
        <td height="20px">
              &nbsp;</td>
        </tr>
        <tr>
          <td>
              <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                  <tr>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('edit_logins.aspx','_self')">USERS</td>
                      <td width="20px" ></td>
                        <td width="180px" class="main-btns" onclick="javascript:window.open('../AdminSuperAccounts.aspx','_self')">Super Accounts</td>
                      <td width="20px" ></td>
                     <%-- <td width="180px" class="main-btns" onclick="javascript:window.open('../CompanyLockList.aspx','_self')">Company List</td>
                      <td width="20px">&nbsp;</td>--%>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../NoOrderCompanies.aspx','_self')">Company List With No Orders</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../LeadCompanyAdmin.aspx','_self')">Leads </td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../AllAccountsRepLoad.aspx','_self')">All Accounts</td>
                  </tr>
              </table>
            </td>
        </tr>
         <td height="20px">
              &nbsp;</td>
        </tr>

         <tr>
          <td>
              <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                  <tr>
                    <td width="180px" class="main-btns" onclick="javascript:window.open('../CompanyLinkList.aspx','_self')">Account Link</td>
                      <td width="20px" ></td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../CallBackDay.aspx','_self')">Callback Events</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../CallBackLeadCompanyList.aspx','_self')">Callback Company Reassign</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns"  onclick="javascript:window.open('../QuoteAllocateAdmin.aspx','_self')">Allocate Last 6 Months Quote </td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../AllocateQuoteRepTestTable.aspx','_self')">Allocate REP'S Quote</td>
                  </tr>
              </table>
            </td>
        </tr>
        <tr>
          <td height="25">&nbsp;</td>
        </tr>

         <tr>
          <td>
              <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                  <tr>
                      
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../AllocateCompanyRepTestTable.aspx','_self')">Allocate REP'S Company</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../RepSuperAccount.aspx','_self')">Set Super Account</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns" onclick="javascript:window.open('../AttendanceSheet.aspx','_self')">Attendance Log</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns"    onclick="javascript:window.open('../Warehouse/WarehouseItems.aspx','_self')">Warehouse</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns"    onclick="javascript:window.open('../DynamicPriceBookUpload.aspx','_self')">Upload Price Book</td>
                  </tr>
              </table>
            </td>
        </tr>
         <tr>
          <td height="25">&nbsp;</td>
        </tr>
         <tr>
          <td>
              <table align="center" cellpadding="0" cellspacing="0" class="width-980-style">
                  <tr>
                      
                      <td width="180px" class="main-btns"  onclick="javascript:window.open('../AllocateNotExistCompanies.aspx','_self')">Allocate Non Existing Company</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns"  onclick="javascript:window.open('AddXeroReference.aspx','_self')">Add Xero Reference</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns"   onclick="javascript:window.open('UpdateCommission.aspx','_self')">Change Order Commission</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns"     onclick="javascript:window.open('AddAttendanceName.aspx','_self')">Add Attendance Name</td>
                      <td width="20px">&nbsp;</td>
                      <td width="180px" class="main-btns"  style=""   onclick="javascript:window.open('AllocatedLeadReport.aspx','_self')">Allocated Companies History</td>
                  </tr>
              </table>
            </td>
        </tr>


         <tr>
          <td height="25">&nbsp;</td>
        </tr>
      </table>
    </asp:Content>