﻿<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="sel_NoSalesReport.aspx.cs" Inherits="DeltoneCRM.Reports.sel_NoSalesReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style type="text/css">
        .auto-style1 {
            width: 300px;
        }
        .auto-style2 {
            height: 3px;
        }
        .auto-style3 {
            height: 36px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">Sales Report</td>
        </tr>

        <tr>
            <td  class="section_headings">&nbsp;</td>
        </tr>

        <tr>
          <td class="white-box-outline">
              &nbsp;
              <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                  <tr>
                      <td class="auto-style3">Product:</td>
                      <td class="auto-style3"></td>
                      <td class="auto-style3">
                          <input id="prodcode" type="text" name="prodcode" runat="server" /></td>
                  </tr>
                  <tr>
                      <td class="auto-style2">Start Date</td>
                      <td class="auto-style2"></td>
                      <td class="auto-style2">
                          <input id="startdate" type="text" name="startdate" runat="server" /></td>
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
                          <input id="enddate" type="text" name="enddate" runat="server" /></td>
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
