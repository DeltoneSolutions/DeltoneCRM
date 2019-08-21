<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="MonthlySalesFigures.aspx.cs" Inherits="DeltoneCRM.Reports.MonthlySalesFigures" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.9.1.js"></script>
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>

    <link href="../css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="../css/overall.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
         .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 40px;
            cursor: pointer;
             margin-top:12px;
        }

            .buttonClass:hover {
                border: solid 1px Black;
                background-color: #ffffff;
            }

        .moveRight {
            float: right;
            margin-bottom: 30px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }
         .setmar{
            margin-left:12px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {

            $('#<%=btnPrintRreport.ClientID%>').click(function () {
                window.print();
            });

            LoaderWait = $('#LoadingDiv').dialog({
                autoOpen: false,
                modal: true,
                width: 710,
            });

            $.ajax({
                url: 'queries/getMonthlySalesFigures.aspx',
                beforeSend: function () {
                    LoaderWait.dialog('open')
                },
                data: {
                    Repname: $('#<%=hidden_repid.ClientID%>').val(),
                // Month: $('#<%=hidden_month.ClientID%>').val(),
                   // Year: $('#<%=hidden_year.ClientID%>').val()
                    startDate: $('#<%=hidden_start.ClientID%>').val(),
                    endDate: $('#<%=hidden_end.ClientID%>').val(),
                },
                complete: function () {
                    LoaderWait.dialog('close')
                },
                success: function (data) {
                    var result = data.split('~');
                    result.sort();
                    $.each(result, function (index, value) {
                        var splitdata = result[index].split('|');
                        var newtablerow = '<tr><td width="100" class="report-content-left-aligned">' + splitdata[0] + '</td> <td width="230" class="report-content-left-aligned">' + splitdata[1] + '</td> <td width="150" class="report-content-left-aligned">' + splitdata[7] + '</td> <td width="50" class="report-content-right-aligned">' + splitdata[4] + '</td><td width="140" class="report-content-right-aligned">' + splitdata[3] + '</td><td width="100" class="report-content-right-aligned">' + splitdata[2] + '</td><td width="100" class="report-content-right-aligned">' + splitdata[5] + '</td> <td width="100" class="report-content-right-aligned">' + splitdata[6] + '</td></tr>';
                        $('#infotable').append(newtablerow);
                    });
                }
            });

            $.ajax({

                url: 'queries/getMonthlySalesTotal.aspx',
                data: {
                    Repname: $('#<%=hidden_repid.ClientID%>').val(),
                   // Month: $('#<%=hidden_month.ClientID%>').val(),
                    //  Year: $('#<%=hidden_year.ClientID%>').val(),
                    startDate: $('#<%=hidden_start.ClientID%>').val(),
                    endDate: $('#<%=hidden_end.ClientID%>').val(),
                },
                success: function (data) {
                    //alert(data);
                    var result = data.split('|');
                    divVolume.innerHTML = result[0];
                    divCommission.innerHTML = result[1];
                    divCreditTotal.innerHTML = result[2];
                    divCreditCommission.innerHTML = result[3];
                    
                    var TComTotal = parseFloat(result[1]) + parseFloat(result[3]);
                    var TVolTotal = parseFloat(result[0]) - parseFloat(result[2]);
                    $('#divCommishTotal').text("$" + TComTotal.toFixed(2));
                    $('#divVolumeTotal').text("$" + TVolTotal.toFixed(2));

                }
            })
        });
    </script>

    <link href= "../css/reports.css" rel='stylesheet' type='text/css'/>
    <link href='https://fonts.googleapis.com/css?family=Droid+Sans:400,700' rel='stylesheet' type='text/css'/>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="width-980-style" border="0" align="center" cellpadding="0" cellspacing="0">
        
         
         <tr>
            <td class="allaround-all">
    <table class="width-930-style" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
              <asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle setmar" CausesValidation="false" />
                    <asp:Button OnClick="btnupload_Click" ID="Button1" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />
                   
        </tr>
            <tr>
              <td height="25" class="report_white_background" style="text-align: right;"><asp:Button ID="btnPrintRreport" runat="server" class="report-print-btn" Text=" PRINT REPORT " /></td>
            </tr>
            <tr>
              <td height="25" class="report_white_background">&nbsp;</td>
            </tr>
            <tr>
              <td height="25" class="report_white_background"><table class="width-930-style" border="0" cellspacing="0" cellpadding="0">
                  <table width="930" border="0" cellspacing="0" cellpadding="0" class="report_white_background">
                <tr>
                  <td width="700" class="report-heading-style">
                      <asp:Label ID="Monthlbl" runat="server" Text="Label"></asp:Label>&nbsp;SALES FIGURES REPORT FOR
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
                  <td width="100" class="report-heading-left-aligned">DATE</td>
                  <td width="230" class="report-heading-left-aligned">COMPANY</td>
                  <td width="150" class="report-heading-left-aligned">SALES REP</td>
                  <td width="50" class="report-heading-right-aligned">NEW ACC</td>
                  <td width="100" class="report-heading-right-aligned">COMMISSION</td>
                  <td width="100" class="report-heading-right-aligned">VOLUME</td>
                  <td width="100" class="report-heading-right-aligned">CREDIT NET</td>
                  <td width="100" class="report-heading-right-aligned">CREDIT VOLUME</td>
                  </tr>
                <tr>
                  <td width="100">&nbsp;</td>
                  <td width="230">&nbsp;</td>
                  <td width="150">&nbsp;</td>
                  <td width="50">&nbsp;</td>
                  <td width="100">&nbsp;</td>
                  <td width="100">&nbsp;</td>
                  <td width="100">&nbsp;</td>
                  <td width="100">&nbsp;</td>
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
                  <table width="930" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                          <td width="430">TOTALS</td>
                          <td width="100">&nbsp;</td>
                          <td width="100"><div id="divCommission"></div></td>
                          <td width="100"><div id="divVolume"></div></td>
                          <td width="100"><div id="divCreditCommission"></div></td>
                          <td width="100"><div id="divCreditTotal"></div></td>
                      </tr>
                  </table>
                  <p>&nbsp;</p>
                  <table width="930" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                          <td width="430">COMMISSION TOTAL</td>
                          <td width="100">&nbsp;</td>
                          <td width="100">&nbsp;</td>
                          <td width="100">&nbsp;</td>
                          <td width="100">&nbsp;</td>
                          <td width="100"><div id="divCommishTotal"></div></td>
                      </tr>
                  </table>
                  <br />
                  <table width="930" border="0" cellspacing="0" cellpadding="0">
                      <tr>
                          <td width="430">VOLUME TOTAL</td>
                          <td width="100">&nbsp;</td>
                          <td width="100">&nbsp;</td>
                          <td width="100">&nbsp;</td>
                          <td width="100">&nbsp;</td>
                          <td width="100"><div id="divVolumeTotal"></div></td>
                      </tr>
                  </table>
              </td>
            </tr>
            <tr>
              <td height="25">
                  <input id="hidden_repid" type="text" name="hidden_repid" runat="server" hidden="hidden" /> 
                  <input id="hidden_month" type="text" name="hidden_month" runat="server" hidden="hidden" />
                  <input id="hidden_year" type="text" name="hidden_year" runat="server"  hidden="hidden"/></td>
                <input id="hidden_start" type="text" name="hidden_start" runat="server" hidden="hidden" />
                <input id="hidden_end" type="text" name="hidden_end" runat="server" hidden="hidden" />
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
