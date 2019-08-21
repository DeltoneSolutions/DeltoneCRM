<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoSalesAccounts.aspx.cs" Inherits="DeltoneCRM.Reports.NoSalesAccounts" MasterPageFile="~/NoNav.Master" %>


<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.9.1.js"></script>
        <script src="../js/jquery-ui-1.10.3.custom.js"></script>
       <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $(document).ready(function () {

            LoaderWait = $('#LoadingDiv').dialog({
                autoOpen: false,
                modal: true,
                width: 710,
            });


            $.ajax({
                url: 'queries/getNoSalesData.aspx',
                beforeSend: function () {
                    LoaderWait.dialog('open')
                },
                data: {
                    RepID: $('#<%=hidden_repid.ClientID%>').val(),
                    DateID: $('#<%=hidden_dateid.ClientID%>').val(),
                },
                complete: function () {
                    LoaderWait.dialog('close')
                },
                success: function (data) {
                    var result = data.split('~');
                    result.sort();
                    $.each(result, function (index, value) {
                        var splitdata = result[index].split('|');
                        var newtablerow = '<tr><td width="330" class="report-content-left-aligned">' + splitdata[0] + '</td><td width="200" class="report-content-left-aligned">' + splitdata[1] + '</td> <td width="150" class="report-content-left-aligned">' + splitdata[2] + '</td> <td width="150" class="report-content-left-aligned">' + splitdata[3] + '</td> <td width="100" class="report-content-right-aligned">' + splitdata[4] + '</td></tr>'
                        $('#infotable').append(newtablerow);
                    });
                }
            });
        });
    </script>

    <link href= "../css/reports.css" rel='stylesheet' type='text/css'/>
    <link href='https://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>

</asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="width-980-style" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="report-outer-border">
    <table class="width-930-style" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="25">&nbsp;</td>
            </tr>
            <tr>
              <td height="25">&nbsp;</td>
            </tr>
            <tr>
              <td height="25"><table width="930" border="0" cellspacing="0" cellpadding="0">
                <tr>
                  <td width="700" class="report-heading-style">NO SALES FOR LONGER THAN <span id="divdaterange" runat="server"></span> MONTHS</td>
                  <td width="230" align="right" class="report-heading-date-style">REPORT GENERATED: <span id="thedate" runat="server"></span></td>
                  </tr>
                </table></td>
            </tr>
            <tr>
              <td height="35">&nbsp;</td>
            </tr>
            <tr>
              <td><table width="930" border="0" cellspacing="0" class="report-table-padding" id="infotable">
                <tr>
                  <td width="330" class="report-heading-left-aligned">COMPANY NAME</td>
                  <td width="200" class="report-heading-left-aligned">CONTACT PERSON</td>
                  <td width="150" class="report-heading-left-aligned">PHONE NUMBER</td>
                  <td width="150" class="report-heading-left-aligned">SALES PERSON</td>
                  <td width="100" class="report-heading-right-aligned">LAST ORDERED</td>
                  </tr>
                <tr>
                  <td width="330">&nbsp;</td>
                  <td width="200">&nbsp;</td>
                  <td width="150">&nbsp;</td>
                  <td width="150">&nbsp;</td>
                  <td width="100">&nbsp;</td>
                  </tr>
              </table></td>
            </tr>
            <tr>
              <td height="35" class="tbl-totals-brdrs-style">&nbsp;</td>
            </tr>
            <tr bgcolor="#F2F2F2" >
              <td height="25">&nbsp;</td>
            </tr>
            <tr>
              <td height="25">
                  <input id="hidden_repid" type="text" name="hidden_repid" runat="server" hidden="hidden" /> <input id="hidden_dateid" type="text" name="hidden_dateid" runat="server" hidden="hidden" /></td>
            </tr>
            <tr bgcolor="#F2F2F2">
              <td height="25">&nbsp;</td>
            </tr>
            <tr>
              <td height="25">&nbsp;</td>
            </tr>
          </table>
                </td>
            </tr>
        </table>
    <div id="LoadingDiv">
  <p>&nbsp;</p>
  <p>Please wait until this page is loaded.</p>
</div>
</asp:Content>