<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInternalNavLevel1.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="DeltoneCRM.Reports.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-1.11.1.min.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/Overall.css" rel="stylesheet" />
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
                    // $(this).val(''); return false;


                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="960px" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table width="920px" cellspacing="0" cellpadding="0">
                    <tr>
                        <td><asp:HyperLink NavigateUrl="sel_NoSalesAccounts.aspx" Text="No Sales Report" runat="server"></asp:HyperLink></td>
                        <td width="50px">&nbsp;</td>
                        <td><asp:HyperLink NavigateUrl="sel_ProductReport.aspx" Text ="Product Purchase Report" runat="server" /></td>
                        <td width="50px">&nbsp;</td>
                        <td><asp:HyperLink NavigateUrl="sel_LeadReport.aspx" Text="Lead Report" runat="server" /></td>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <td><asp:HyperLink NavigateUrl="sel_MonthlySalesFigures.aspx" Text="Monthly Sales Figures" runat="server"></asp:HyperLink></td>
                        <td width="50px">&nbsp;</td>
                        <td><asp:HyperLink NavigateUrl="sel_SalesReport.aspx" Text ="Sales Report" runat="server" /></td>
                        <td width="50px">&nbsp;</td>
                        <td><asp:HyperLink ID="HyperLink1" NavigateUrl="sel_AccountOwner.aspx" Text ="Accounts By Owner" runat="server" /></td>

                        <td width="50px">&nbsp;</td>

                      
                    </tr>
                      <tr><td>&nbsp;</td></tr>
                     <tr><td><asp:HyperLink ID="HyperLink4pro" NavigateUrl="CustomersBYProduct.aspx" Text ="Customer Search By Ordered Product" runat="server" /></td>
                          <td width="50px">&nbsp;</td>
                         <td><asp:HyperLink ID="HyperLink2" NavigateUrl="CustomerCreditByProduct.aspx" Text ="Customer Search By Credited Product" runat="server" /></td>
                         <td width="50px">&nbsp;</td>
                         <td><asp:HyperLink ID="historicallink" NavigateUrl="allusersmonthlysales.aspx" Text ="Historical Sales" runat="server" /></td>
                           <td width="50px">&nbsp;</td>
                          </tr>
                      <tr><td>&nbsp;</td></tr>
                      <tr><td><asp:HyperLink ID="HyperLink3" NavigateUrl="SalesAllusersGraphDisplay.aspx" Text ="Sales Commission Visualization" runat="server" /></td>
                          <td width="50px">&nbsp;</td>
                          <td><asp:HyperLink ID="HyperLink4" NavigateUrl="MonthlyVolumeGraphDisplay.aspx" Text ="Monthly Volume Visualization" runat="server" /></td>
                          <td width="50px">&nbsp;</td>
                          <td><asp:HyperLink ID="HyperLink5" NavigateUrl="NumberOfAccountsGraphDisplay.aspx" Text ="Number Of Accounts Visualization" runat="server" /></td>
                          <td width="50px">&nbsp;</td>
                          </tr>

                     <tr><td>&nbsp;</td></tr>
                    <tr><td><asp:HyperLink ID="HyperLink6" NavigateUrl="CustomerQuotedReportEmail.aspx" Text ="Customer Search By Quoted Product" runat="server" /></td>
                          <td width="50px">&nbsp;</td>
                         <td><asp:HyperLink ID="HyperLink7" NavigateUrl="../StatusForManager.aspx" Text ="Stats" runat="server" /></td>
                         <td width="50px">&nbsp;</td>
                         <td><asp:HyperLink ID="HyperLink8" NavigateUrl="../coldiesStatus.aspx" Text ="Coldies Stats" runat="server" /></td>
                           <td width="50px">&nbsp;</td>
                          </tr>
                      <tr><td>&nbsp;</td></tr>
                      <tr><td><asp:HyperLink ID="HyperLink9" NavigateUrl="ReportMonthlyRep.aspx" Text ="Stats Reports" runat="server" /></td>
                          <td width="50px">&nbsp;</td>
                         <td style="visibility:hidden"><asp:HyperLink ID="HyperLink10" NavigateUrl="../StatusForManager.aspx" Text ="Stats" runat="server" /></td>
                         <td width="50px">&nbsp;</td>
                         <td style="visibility:hidden"><asp:HyperLink ID="HyperLink11" NavigateUrl="../coldiesStatus.aspx" Text ="Coldies Stats" runat="server" /></td>
                           <td width="50px">&nbsp;</td>
                          </tr>

                     <tr><td>&nbsp;</td></tr>
                </table>
                <tr>
            <td>&nbsp;</td>
        </tr>
            </td>
             <tr>
            <td>&nbsp;</td>
        </tr>
        </tr>
    </table>
</asp:Content>
