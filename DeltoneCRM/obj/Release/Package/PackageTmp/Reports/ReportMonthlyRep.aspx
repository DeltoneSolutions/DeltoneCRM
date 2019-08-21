<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/NoNav.Master" CodeBehind="ReportMonthlyRep.aspx.cs" Inherits="DeltoneCRM.Reports.ReportMonthlyRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />
     <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
     <style type="text/css">
        .auto-style1 {
            width: 300px;
        }
        .auto-style2 {
            height: 3px;
        }

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
       <asp:ScriptManager ID="ScriptManagerfororder" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>

     <table class="width-980-style" border="0" align="center" cellpadding="0" cellspacing="0">
        
         
         <tr>
          <td height="25px"><asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle setmar" CausesValidation="false" />
                    <asp:Button OnClick="btnupload_Click" ID="Button3" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />
                   </td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">Monthly Rep Sales Figures</td>
        </tr>

        <tr>
            <td  class="section_headings">&nbsp;</td>
        </tr>

        <tr>
          <td class="white-box-outline">
              &nbsp;
              <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                  <tr id="repdropTr" runat="server">
                      <td>Rep:</td>
                      <td>&nbsp;</td>
                      <td>
                          <asp:DropDownList ID="ddlRepList" runat="server">
                          </asp:DropDownList>
                      </td>
                  </tr>
                  <tr style="display:none;">
                      <td class="auto-style2">Month:</td>
                      <td class="auto-style2"></td>
                      <td class="auto-style2">
                          <asp:DropDownList ID="DropDownList1" runat="server">
                              <asp:ListItem Value="01">January</asp:ListItem>
                              <asp:ListItem Value="02">February</asp:ListItem>
                              <asp:ListItem Value="03">March</asp:ListItem>
                              <asp:ListItem Value="04">April</asp:ListItem>
                              <asp:ListItem Value="05">May</asp:ListItem>
                              <asp:ListItem Value="06">June</asp:ListItem>
                              <asp:ListItem Value="07">July</asp:ListItem>
                              <asp:ListItem Value="08">August</asp:ListItem>
                              <asp:ListItem Value="09">September</asp:ListItem>
                              <asp:ListItem Value="10">October</asp:ListItem>
                              <asp:ListItem Value="11">November</asp:ListItem>
                              <asp:ListItem Value="12">December</asp:ListItem>
                          </asp:DropDownList>
                      </td>
                  </tr>
                   <tr >
                   <td >Start Date: </td>
                        <td>&nbsp;</td>
                <td >
                    <input id="StartDateTxt" type="text" name="StartDateTxt"  size="24" runat="server"/>
                   
                </td>
                   </tr>
                   <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                  </tr>
                   <tr >
                   <td >End Date: </td>
                        <td>&nbsp;</td>
                <td >
                    <input id="EndDateTxt" type="text" name="EndDateTxt" size="24" runat="server"/>
                   
                </td>
                   </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                  </tr>
                  <tr style="display:none;">
                      <td>Year</td>
                      <td>&nbsp;</td>
                      <td>
                          <input id="enddate" type="text" name="enddate" runat="server" /></td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>
                          <asp:Button ID="Button1" runat="server" Text="Generate Commission Report" OnClick="GR_Click" />
                          <br />

                          
                      </td>
                       <td>&nbsp;</td>
                       <td ><asp:Button ID="Button4" runat="server" Text="Generate Volume Report" OnClick="VL_Click" /></td>
                  </tr>
              </table>
            </td>
        </tr>
            </tr>
       
                </table>
   <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css"/>
  <link rel="stylesheet" href="/resources/demos/style.css"/>
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">

        
        $(document).ready(function () {

            var dateNow = new Date();
            $("#ContentPlaceHolder1_StartDateTxt").datepicker({ dateFormat: 'dd-mm-yy' });
            $("#ContentPlaceHolder1_EndDateTxt").datepicker({ dateFormat: 'dd-mm-yy' });
          //  ('#expiration_datepicker').datetimepicker('setDate', (new Date()));
           // ('#expiration_datepicker').datetimepicker('setDate', (new Date()));
          
        });

    </script>
</asp:Content>