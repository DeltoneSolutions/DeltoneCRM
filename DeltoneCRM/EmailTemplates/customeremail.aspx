<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="customeremail.aspx.cs" Inherits="DeltoneCRM.EmailTemplates.customeremail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <style type="text/css">
        body {
    background-color: #F7F5F2;
}
.emailheader {
    height: 50px;
    background-color: #3E3E42;
    
}
.OrderNumberTitle {
    font-family: Calibri;
            font-size: 16px;
            font-weight:600;
            color: #3E3E42;
            text-align: center;
}
.ThankYouMessage {
    font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            font-weight:400;
            color: #3E3E42;
            text-align: center;
}
.itemheader {
    font-family: 'Trebuchet MS'
            font-size: 12px;
            font-weight:400;
            background-color: #56648a;
            color: white;
            text-align: center;
            width: 100px;
            height: 20px;
            padding-top: 5px;
            border-radius: 5px;
}
.individualItems_Left {
     font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            font-weight:400;
}
.individualItems_Center {
     font-family: 'Droid Sans', sans-serif;
            font-size: 12px;
            font-weight:400;
            text-align:center;
}

    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            $.ajax({
                url: 'query/getOrderDetails.aspx',
                data: {
                    OID: 2824,
                },
                async:false,
                success: function (msg) {
                    var StringBuilder;
                    var OverallSplit = msg.split("~");
                    $.each(OverallSplit, function(index,value)
                    {
                        var IndiSplit = OverallSplit[index].split("|");
                        $.each(IndiSplit, function (index2, value2) {
                            StringBuilder = "<tr><td class='individualItems_Left'>" + IndiSplit[0] + "</td><td class='individualItems_Center'>" + IndiSplit[1] + "</td><td class='individualItems_Center'>" + IndiSplit[2] + "</td></tr>";
                            
                        });
                        $('#ordertable tr:last').after(StringBuilder);
                    });
                },
                error: function (xhr, err) {
                    alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                    alert("responseText: " + xhr.responseText);
                },
            });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table align="center" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="emailheader">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><table align="center" cellpadding="0" cellspacing="0" width="980px">

                    <tr>
                        <td class="OrderNumberTitle">PURCHASE ORDER CONFIRMATION - ORDER #: <asp:Label ID="lbl_OrderNumber" runat="server" Text="Order Number Here"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="ThankYouMessage">Thank you for ordering with Deltone Solutions. The following order will be dispatched to you within the next 3-5 business days.<br />
                            <br /> If you experience any issues with your order, please contact our staff on the number provided above.</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                        <table align="center" cellpadding="0" cellspacing="0" width="920px" id="ordertable">
                            <tr>
                                <td width="520px" align="left"> <div id="itemheader" class="itemheader">ITEMS</div></td>
                                <td  width="200px" align="center"><div id="Div1" class="itemheader">QUANTITY</div></td>
                                <td  width="200px" align="center"><div id="Div2" class="itemheader">PRICE</div></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>

                        </td>

                    </tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr><td>Your order should arrive in 2-4 business days to the address below. </td></tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr><td>&nbsp;</td></tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
