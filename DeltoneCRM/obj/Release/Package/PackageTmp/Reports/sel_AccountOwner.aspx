<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="sel_AccountOwner.aspx.cs" Inherits="DeltoneCRM.Reports.sel_AccountOwner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">Accounts by Owner</td>
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
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>
                          <asp:Button ID="Button1" runat="server" Text="Generate Report" OnClick="GR_Click" />
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
