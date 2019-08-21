<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="sel_LeadReport.aspx.cs" Inherits="DeltoneCRM.Reports.sel_LeadReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 300px;
        }
        .auto-style2 {
            height: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">Lead Report</td>
        </tr>

        <tr>
            <td  class="section_headings">&nbsp;</td>
        </tr>

        <tr>
          <td class="white-box-outline">
              &nbsp;
              <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                  <tr>
                      <td>Rep:</td>
                      <td>&nbsp;</td>
                      <td>
                          <asp:DropDownList ID="ddlRepList" runat="server">
                          </asp:DropDownList>
                      </td>
                  </tr>
                  <tr>
                      <td class="auto-style2">Start Date</td>
                      <td class="auto-style2"></td>
                      <td class="auto-style2">
                          <input id="startdate" type="date" name="startdate" runat="server" /></td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                  </tr>
                  <tr>
                      <td>End Date</td>
                      <td>&nbsp;</td>
                      <td>
                          <input id="enddate" type="date" name="enddate" runat="server" /></td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                  </tr>
                  <tr>
                      <td>Customer</td>
                      <td>&nbsp;</td>
                      <td>                          
                          <asp:DropDownList ID="ddlCustomerList" runat="server">
                          </asp:DropDownList>
                      </td>
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
                          <asp:Button ID="Button1" runat="server" Text="Generate Report"/>
                      </td>
                  </tr>
              </table>
            </td>
        </tr>

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
      </table>
</asp:Content>
