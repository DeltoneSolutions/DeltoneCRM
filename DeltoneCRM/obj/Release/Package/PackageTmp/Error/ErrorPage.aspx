<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="DeltoneCRM.Error.ErrorPage"  MasterPageFile="~/SiteInternalNavLevel1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.9.1.js"></script>
    <style type="text/css">
        .Header_Style 
        {
            color:red;
            font-weight:bold;
            text-align: center;
        }

    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br /><br />
    <div id="div_Error" class="Header_Style">
        <h1>Sorry, An Error Has Occurred</h1>
        <h2>Plase contact the Administrator and will work to resolve this issue</h2>
        <h1>&nbsp;</h1>

    </div>
   
</asp:Content>



