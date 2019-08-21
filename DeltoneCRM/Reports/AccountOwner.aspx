<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="AccountOwner.aspx.cs" Inherits="DeltoneCRM.Reports.AccountOwner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.9.1.js"></script>
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>

    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../css/overall.css" rel="stylesheet" type="text/css" />
     <link href= "../css/reports.css" rel='stylesheet' type='text/css'/>
    <link href='https://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>

    <script type="text/javascript">

       

        $(document).ready(function () {

            $('#<%=btnPrintReport.ClientID%>').click(function () {
                window.print();
            });

            LoaderWait = $('#LoadingDiv').dialog({
                autoOpen: false,
                modal: true,
                width: 710,
            });

            $.ajax({
                url: 'queries/getAccountOwner.aspx',
                beforeSend: function () {
                    LoaderWait.dialog('open')
                },
                data: {
                    Repname: $('#<%=hidden_repid.ClientID%>').val(),
                },
                complete: function () {
                    LoaderWait.dialog('close')
                },
                success: function (data) {
                    var result = data.split('~');
                    result.sort();
                    $.each(result, function (index, value) {
                        var splitdata = result[index].split('|');
                        var newtablerow = '<tr><td width="230">' + splitdata[0] + '</td><td width="230">' + splitdata[1] + '</td><td width="230">' + splitdata[2] + '</td><td style="align:right;">' + splitdata[3] + '</td></tr>';
                        $('#infotable').append(newtablerow);
                    });
                }
            });
        });

        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="width-980-style" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>&nbsp;</td>
        </tr>
         
         
         <tr>
            <td class="allaround-all">
    <table class="width-930-style" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td height="25" class="report_white_background" style="text-align: right;"><asp:Button ID="btnPrintReport" runat="server" class="report-print-btn" Text=" PRINT REPORT " /></td>
            </tr>
            <tr>
              <td height="25" class="report_white_background">&nbsp;</td>
            </tr>
            <tr>
              <td height="25" class="report_white_background"><table class="width-930-style" border="0" cellspacing="0" cellpadding="0">
                  <table width="930" border="0" cellspacing="0" cellpadding="0" class="report_white_background">
                <tr>
                  <td width="700" class="report-heading-style">
                      ACCOUNTS OWNED BY
                      <asp:Label ID="Replbl" runat="server" Text="Label"></asp:Label>
                    </td>
                  <td width="230" align="right" class="report-heading-date-style">REPORT GENERATED:&nbsp;<asp:Label ID="datelbl" runat="server" Text="Label"></asp:Label>
&nbsp;</td>
                  </tr>
                </table>
</td>
            </tr>
            <tr>
              <td height="35" class="report_white_background">&nbsp;</td>
            </tr>
            <tr>
              <td class="report_white_background"><table width="930" border="0" cellspacing="0" cellpadding="0" id="infotable">
                <tr>
                  <td width="230" class="report-heading-left-aligned">SALES REP</td>
                  <td width="230" class="report-heading-left-aligned">COMPANY</td>
                  <td width=230" class="report-heading-left-aligned">CONTACT PERSON</td>
                  <td class="report-heading-left-aligned">DATE OF LAST SALE</td>
                  </tr>
                <tr>

                  <td width="230">&nbsp;</td>
                  <td width="230">&nbsp;</td>
                  <td width="230">&nbsp;</td>
                  <td>&nbsp;</td>

                  </tr>
              </table></td>
            </tr>
            <tr>
              <td height="35" class="report_white_background">&nbsp;</td>
            </tr>
             <tr bgcolor="#F2F2F2" >
              <td height="25" class="report_white_background">&nbsp;</td>
            </tr>
            <tr bgcolor="#F2F2F2" >
              <td height="25" class="report_white_background">
                  
              </td>
            </tr>
            <tr>
              <td height="25">
                  <input id="hidden_repid" type="text" name="hidden_repid" runat="server" hidden="hidden"/></td>
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
