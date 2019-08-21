<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="View.aspx.cs" Inherits="DeltoneCRM.View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ContactView</title>

     <style type="text/css">
         .Result 
         {
             font-weight:bold;
             color:green;    
         }
         .display 
         {
             border:thin;
             border-color:green;
         }
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <!--Display Panel-->
        <div id="divDisplay" runat="server" class="display">
            <Asp:label ID="lblResult" runat="server" CssClass="Result">

            </Asp:label>
            
        </div>
        
        <!--End Display Panel-->

    
    </div>
    </form>
</body>
</html>
