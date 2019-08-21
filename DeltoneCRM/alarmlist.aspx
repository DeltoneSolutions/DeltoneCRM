<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="alarmlist.aspx.cs" Inherits="DeltoneCRM.alarmlist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .alarm-heading-style {
	font-family: 'Droid Sans', sans-serif;
	font-size: 14px;
	font-weight: 400 !important;
	color: #fff;
	text-align: left;	/*padding-left: 30px;*/
}
        .alarm-heading2-style {
	font-family: 'Droid Sans', sans-serif;
	font-size: 14px;
	font-weight: 400 !important;
    color: #fff;
	text-align: right;	/*padding-left: 30px;*/
}
        .alarm-description {
            font-family: 'Droid Sans', sans-serif;
	font-size: 12px;
	font-weight: 400 !important;
	color: #fff;
	text-align: left;	/*padding-left: 30px;*/
        }
        .alarm_buttons {
            color: #fff;
            font-family: 'Droid Sans', sans-serif;
	font-size: 12px;
	font-weight: 400 !important;
        }
        .createdby {
            color: #391326;
            font-family: 'Droid Sans', sans-serif;
	font-size: 12px;
	font-weight: 400 !important;
        }
    </style>

    <script src="js/jquery-2.1.3.js"></script>

    <script type="text/javascript">

        function OpenCompany(CID, AID) {
            window.open("ConpanyInfo.aspx?companyid=" + CID, "_self");
        }

        function DismissAlarm(AID) {
            $.ajax({
                url: 'process/ProcessDeleteAlarm.aspx',
                data: {
                    AID: AID,
                },
                success: function (response) {
                    alert("Alarm successfully deleted");
                    window.parent.getAlarmCount();
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="tableContent" width="490" border="0" cellspacing="0" cellpadding="0" bgcolor="#bf4080" runat="server"></table>
    </form>
</body>
</html>
