<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteInternalNavLevel1.Master" CodeBehind="RepReports.aspx.cs" Inherits="DeltoneCRM.Reports.RepReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .buttonLInk {
  font: bold 11px Arial;
  text-decoration: none;
  text-align:center;
  background-color: #EEEEEE;
  color: #333333;
  padding: 2px 6px 2px 6px;
  border-top: 1px solid #CCCCCC;
  border-right: 1px solid #333333;
  border-bottom: 1px solid #333333;
  border-left: 1px solid #CCCCCC;
  width:200px !important;
  height:40px !important;
}

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
                    window.open(url, "blank");
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
                       <%-- <td><asp:HyperLink NavigateUrl="sel_MonthlySalesFigures.aspx" Width="140" Height="40" CssClass="buttonLInk" Text="Sales Figures" runat="server"></asp:HyperLink></td>
                        <td width="50px">&nbsp;</td>
                        <td><asp:HyperLink NavigateUrl="CustomerByProductForRe.aspx" Width="140" Height="40" CssClass="buttonLInk" Text ="Customer By Ordered Product" runat="server" /></td>--%>
                       <td width="180px" class="main-btns" onclick="javascript:window.open('sel_MonthlySalesFigures.aspx','_self')">Sales Figures</td>
                          <td width="50px">&nbsp;</td>
                         <td width="180px" class="main-btns" onclick="javascript:window.open('CustomerByProductForRe.aspx','_self')">Customer By Ordered Product</td>
                       <%-- <td><asp:HyperLink NavigateUrl="sel_LeadReport.aspx" Text="Lead Report" runat="server" /></td>--%>
                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr>
                        <%--<td><asp:HyperLink NavigateUrl="sel_MonthlySalesFigures.aspx" Text="Monthly Sales Figures" runat="server"></asp:HyperLink></td>
                        <td width="50px">&nbsp;</td>
                        <td><asp:HyperLink NavigateUrl="sel_SalesReport.aspx" Text ="Sales Report" runat="server" /></td>
                        <td width="50px">&nbsp;</td>
                        <td><asp:HyperLink ID="HyperLink1" NavigateUrl="sel_AccountOwner.aspx" Text ="Accounts By Owner" runat="server" /></td>

                        <td width="50px">&nbsp;</td>--%>

                      
                    </tr>
                      

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