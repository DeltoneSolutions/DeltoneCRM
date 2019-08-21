<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Uploader.aspx.cs" Inherits="DeltoneCRM.Uploader.Uploader" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Has Header?" />
    <asp:RadioButtonList ID="rbHDR" runat="server">
        <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
        <asp:ListItem Text="No" Value="No"></asp:ListItem>
    </asp:RadioButtonList>

    
    </asp:Content>
