<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Start.aspx.cs" Inherits="DeltoneCRM.Thread_Test.Start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <asp:Button ID="btnStart" runat="server" Text="Long Running Process" OnClick="btnStart_Click" />
    
    </div>
    </form>
</body>
</html>
