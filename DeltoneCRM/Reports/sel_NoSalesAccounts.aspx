<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sel_NoSalesAccounts.aspx.cs" Inherits="DeltoneCRM.Reports.sel_NoSalesAccounts" MasterPageFile="~/NoNav.Master" %>


<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 300px;
        }

         .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 40px;
            cursor: pointer;
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


    </style>
    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">Accounts that there has been no sales</td>
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
                      <td>Date Range</td>
                      <td>&nbsp;</td>
                      <td>
                          <asp:DropDownList ID="DropDownList2" runat="server">
                              <asp:ListItem Value="3">Within Last 3 Months</asp:ListItem>
                              <asp:ListItem Value="6">Within Last 6 Months</asp:ListItem>
                              <asp:ListItem Value="9">Within Last 9 Months</asp:ListItem>
                              <asp:ListItem Value="12">Within Last 12 Months</asp:ListItem>
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
