<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/SiteInternalNavLevel1.Master" CodeBehind="AddTestButton.aspx.cs" Inherits="DeltoneCRM.AddTestButton" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button OnClick="SearchButton_Click" ID="btnsearch" Text="Search" runat="server" CausesValidation="false" />
    
    </asp:Content>