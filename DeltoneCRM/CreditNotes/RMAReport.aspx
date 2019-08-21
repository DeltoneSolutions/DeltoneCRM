<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RMAReport.aspx.cs" Inherits="DeltoneCRM.CreditNotes.RMAReport" MasterPageFile="~/SiteInternalNavLevel1.Master" %>


<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-latest.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">

       
        function format(OrderID)
        {
          
            //Make the Ajax Request and Get the Error or Success Message
            var RESULT = '';
            var MESSAGE = '';

            //Make the Ajax Request and Get the Error or Success Message
            /*$.ajax({
                type: "POST",
                url: "../Process/Process_FetchLOG.aspx",
                async: false,
                data: {
                    Order_ID: OrderID,
                },
                success: function (msg) {

                    alert(msg);
                    if (msg)
                    {
                        RESULT = msg.split(':', 3);
                        MESSAGE = msg.split(':', 0);
                        alert(RESULT + ':' + MESSAGE);
                    }
                },
                error: function (xhr, err)
                {
                    alert(err);
                },
            });*/


            return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                '<tr>' +
                    '<td>Order:</td>' +
                    '<td></td>' +
                '</tr>' +
                '<tr>' +
                    '<td></td>' +
                    '<td></td>' +
                '</tr>' +
                '<tr>' +
                    '<td></td>' +
                    '<td></td>' +
                '</tr>' +
            '</table>';
        }


        $(document).ready(function () {

            $('#btn_SaveRMAChanges').click(function () {
                $.ajax({
                    url: '../Process/EditRMA.aspx',
                    data: {
                        QID: $('#CreditNoteID').val(),
                        STS: $('#chk_S2S').is(':checked'),
                        ARMA: $('#chk_AR').is(':checked'),
                        CIX: $('#chk_CIX').is(':checked'),
                        RTC: $('#chk_R2C').is(':checked'),
                        ANFS: $('#chk_ANFS').is(':checked'),
                        INHouse: $('#chk_InH').is(':checked'),
                        SRMAN: $('#RMANumber').val(),
                        TN: $('#TrackingNumber').val(),
                    },
                    success: function (response) {
                        table.ajax.reload();
                        ERMA.dialog('close');
                    }
                })
            })

            ERMA = $('#EditRMA').dialog({
                resizable: false,
                modal: true,
                title: 'EDIT RMA',
                width: 1000,
                autoOpen: false,
            });
                
            var param = getParameterByName("s");
            if (param == null)
                param = "5";
            var table = $('#example').DataTable({
                processing: true,
                "language": {
                    "processing": "Hang on. Waiting for response..." //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
               
                columns: [
                    { 'data': 'RMAID' },
                   { 'data': 'CreditNoteID' },
                   { 'data': 'SupplierName' },
                   { 'data': 'RaisedDateTime' },
                   { 'data': 'SentToSupplier' },
                   { 'data': 'RMAToCustomer' },
                   { 'data': 'CreditInXero' },
                    { 'data': 'InHouse' },
                    { 'data': 'Status' },
                   { 'data': 'SupplierRMANumber' },
                   { 'data': 'TrackingNumber' },
                   { 'data': 'ViewEdit' },
                ],
                "iDisplayLength": 25,
                "order": [[0, "desc"]],
                fnServerParams: function (aoData) {
                    aoData.push(
                        { "name": "s", "value": param }
                    );
                },
                sAjaxSource: "../DataHandlers/RMATRACKINGHandler.ashx",
                "columnDefs": [
                  {
                      "targets": [0],
                      "visible": false,
                      "searchable": false
                  },
                  { className: 'align_center', "targets": [2, 3, 4, 5, 6, 7,8,9] },
                ]
            });

            function getParameterByName(name, url) {
                if (!url) url = window.location.href;
                name = name.replace(/[\[\]]/g, "\\$&");
                var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                    results = regex.exec(url);
                if (!results) return null;
                if (!results[2]) return '';
                return decodeURIComponent(results[2].replace(/\+/g, " "));
            }

            $('#example tbody').on('click', '.details', function () {
               
                var tr = $(this).closest('tr');
                var element = $(this).closest('td').next();
                
                // var OrderID = tr.find('td:first').text();
                var OrderID = element.text();

                var row = table.row(tr);
                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    // Open this row
                    row.child(format(OrderID)).show();
                    tr.addClass('shown');
                }
            });

           
        });

        function openWin(CID) {
            $('#CreditNoteID').val(CID);
            $.ajax({
                url: '../Fetch/FetchRMADetails.aspx',
                data: {
                    RID: CID
                },
                success: function (response) {
                    var splitString = response.split('|');
                    if (splitString[0] == '1') {
                        $('#chk_S2S').prop('checked', true);
                    }
                    else {
                        $('#chk_S2S').prop('checked', false);
                    }

                    if (splitString[1] == '1') {
                        $('#chk_AR').prop('checked', true);
                    }
                    else {
                        $('#chk_AR').prop('checked', false);
                    }

                    if (splitString[2] == '1') {
                        $('#chk_CIX').prop('checked', true);
                    }
                    else {
                        $('#chk_CIX').prop('checked', false);
                    }

                    if (splitString[3] == '1') {
                        $('#chk_R2C').prop('checked', true);
                    }
                    else {
                        $('#chk_R2C').prop('checked', false);
                    }

                    if (splitString[4] == '1') {
                        $('#chk_ANFS').prop('checked', true);
                    }
                    else {
                        $('#chk_ANFS').prop('checked', false);
                    }



                    $('#RMANumber').val(splitString[5]);
                    $('#TrackingNumber').val(splitString[6]);

                    if (splitString[7] == '1') {
                        $('#chk_InH').prop('checked', true);
                    }
                    else {
                        $('#chk_InH').prop('checked', false);
                    }
                }
            })
            ERMA.dialog('open');
        }
    </script>
    <style type="text/css">
        .auto-style2 {
            height: 25px;
        }
        .inner-table-size {
            width: 940px;
        }

     }

          body {
    background-color: #FFFFCC !important;
}
        th.dt-center, td.dt-center { text-align: center; }
        .dt-head-center {text-align: center;}
    </style>
    </asp:Content>

<asp:Content ID="MainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">RMA REPORT</td>
        </tr>

        <tr>
            <td  class="section_headings"><a href="RMAReport.aspx?s=1">CREDIT PENDING</a> 
                | <a href="RMAReport.aspx?s=2">AWAITING RMA</a>
               | <a href="RMAReport.aspx?s=3">AWAITING TRACKING</a>
                |<a href="RMAReport.aspx?s=4">CREDIT IN XERO</a>
                |<a href="RMAReport.aspx?s=5">ALL</a>
            </td>
        </tr>

        <tr>
          <td class="white-box-outline">
              <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                  <tr>
                      <td height="20px">&nbsp;</td>
                  </tr>

                  <tr>
                      <td>

                          <div class="container">
<table cellpadding="0" width="980" cellspacing="0" border="0" class="display" id="example" style="cursor:pointer">
	<thead>
		<tr>
            <th align="left">RMA #</th>
			<th align="left">CREDIT #</th>
            <th align="left">SUPPLIER NAME</th>
			<th align="left">RAISED DATE</th>
			<th align="left">RMA REQUEST SUPPLIER</th>
            <th align="left">RAM REQUEST CUSTOMER</th>
             <th align="left">CREDIT IN XERO</th>
             <th align="left">IN HOUSE</th>
             <th align="left">STATUS</th>
             <th align="left">RMA</th>
            <th align="left">TRACKING </th>
            <th align="left">EDIT</th>
		</tr>
        
	</thead>
     
            
	<tbody>
		 
	</tbody>

</table>

			</div>


                      </td>
                  </tr>
                  <tr>
                      <td height="25px">&nbsp;</td>
                  </tr>
                  
              </table>
            </td>
        </tr>

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
      </table>


    <div id="EditRMA">
        <table cellpadding="0" cellspacing="0" border="0" align="center">
            <thead>
                <tr style="height:7px;">
                    <td>&nbsp;</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="font-size:16px;text-align:center;">Sent To Supplier</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td style="font-size:16px;text-align:center;">RMA has been approved</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td style="font-size:16px;text-align:center;">Credited in Xero</td>
                     <td class="auto-style1">&nbsp;</td>
                    <td style="font-size:16px;text-align:center;">In House</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td style="font-size:16px;text-align:center;">RMA Sent to Customer</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td style="font-size:16px;text-align:center;">Adjusted Note Received From Supplier</td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>&nbsp;</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="auto-style1">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td style="width:25px;">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><div class="squaredThree"><input type="checkbox" id="chk_S2S" /><label for="chk_S2S" /></div></td>
                    <td style="width:25px;">&nbsp;</td>
                    <td><div class="squaredThree"><input type="checkbox" id="chk_AR" /><label for="chk_AR" /></div></td>
                    <td style="width:25px;">&nbsp;</td>
                    <td><div class="squaredThree"><input type="checkbox" id="chk_CIX" /><label for="chk_CIX" /></div></td>
                    <td class="auto-style1">&nbsp;</td>
                    <td><div class="squaredThree"><input type="checkbox" id="chk_InH" /><label for="chk_InH" /></div></td>
                    <td style="width:25px;">&nbsp;</td>
                    <td><div class="squaredThree"><input type="checkbox" id="chk_R2C" /><label for="chk_R2C" /></div></td>
                    <td style="width:25px;">&nbsp;</td>
                    <td><div class="squaredThree"><input type="checkbox" id="chk_ANFS" /><label for="chk_ANFS" /></div></td>
                </tr>
                </tr>
            </tbody>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" align="center" style="width:1000px;" align="center">
            <tr>
                <td>&nbsp;</td>
                <td><input type="text" id="CreditNoteID" hidden="hidden" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width:250px;">&nbsp;</td>
                <td style="width:500px;text-align:center;font-size:16px;">Supplier RMA Number</td>
                <td style="width:250px;">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width:500px;text-align:center;"><input type="text" id="RMANumber" name="RMANumber" class="txtbox_format" /></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="width:500px;text-align:center;font-size:16px;">Tracking Number</td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td>&nbsp;</td>
                <td style="width:500px;text-align:center;"><input type="text" id="TrackingNumber" name="TrackingNumber" class="txtbox_format" /></td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br /><br />
        <table cellpadding="0" cellspacing="0" border="0" align="center" style="width:1000px;" align="center">
            <tr>
                <td style="text-align:center;"><input type="button" id="btn_SaveRMAChanges" value="SAVE" class="btn" /></td>
            </tr>
        </table>
        
            <br /><br />
    </div>

     </asp:Content>
