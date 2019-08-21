<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Status.aspx.cs" Inherits="DeltoneCRM.Thread_Test.Status" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Simple Waiting Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <h1>
                <asp:Label ID="lblStatus" runat="server" 
                Text="Working... Please wait..."></asp:Label>
            </h1>
    </div>
    </form>
</body>
</html>
