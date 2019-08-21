<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="DeltoneCRM.Order" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="MainHeader" ContentPlaceHolderID="head" runat="server">
<script src="../js/jquery-1.9.1.js"></script>
<script src="../js/jquery-ui-1.10.3.custom.js"></script>
<link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>

<script type="text/javascript">
</script>

<style type="text/css">

    .Message {
        color:green;
        font-size:1.5em;
    }


</style>

</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
  <h2>Orders</h2>

      <div id="divDisplay"  runat="server">

          <asp:Label ID="lblMessage" runat="server" CssClass="Message"></asp:Label>

      </div>

      <table id="tblOrders"   runat="server">
          <asp:Button ID="btnTest"    runat="server" Text="Go"  />
          <asp:Button ID="btnSecond" runat="server" Text="Submit" OnClick="btn_Click" />
         
      </table>




    




</asp:Content>