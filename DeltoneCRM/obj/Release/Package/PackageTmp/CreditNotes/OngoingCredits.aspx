<%@ Page Title="" Language="C#" MasterPageFile="~/SiteInternalNavLevel1.Master" AutoEventWireup="true" CodeBehind="OngoingCredits.aspx.cs" Inherits="DeltoneCRM.CreditNotes.OngoingCredits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-latest.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

    <style type="text/css">
        body {
    background-color: #FFFFCC !important;
}
    td.details-control {
        background: url('../images/details_open.png') no-repeat center center;
        cursor: pointer;
    }
    tr.shown td.details-control {
        background: url('../images/details_close.png') no-repeat center center;
    }
    .squaredThree {
	width: 20px;	
	margin: 20px auto;
	position: relative;
    }
    .squaredThree label {
	cursor: pointer;
	position: absolute;
	width: 20px;
	height: 20px;
	top: 0;
	border-radius: 4px;

	-webkit-box-shadow: inset 0px 1px 1px rgba(0,0,0,0.5), 0px 1px 0px rgba(255,255,255,.4);
	-moz-box-shadow: inset 0px 1px 1px rgba(0,0,0,0.5), 0px 1px 0px rgba(255,255,255,.4);
	box-shadow: inset 0px 1px 1px rgba(0,0,0,0.5), 0px 1px 0px rgba(255,255,255,.4);

	background: -webkit-linear-gradient(top, #222 0%, #45484d 100%);
	background: -moz-linear-gradient(top, #222 0%, #45484d 100%);
	background: -o-linear-gradient(top, #222 0%, #45484d 100%);
	background: -ms-linear-gradient(top, #222 0%, #45484d 100%);
	background: linear-gradient(top, #222 0%, #45484d 100%);
	filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#222', endColorstr='#45484d',GradientType=0 );
}
    .squaredThree label:after {
	-ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=0)";
	filter: alpha(opacity=0);
	opacity: 0;
	content: '';
	position: absolute;
	width: 9px;
	height: 5px;
	background: transparent;
	top: 4px;
	left: 4px;
	border: 3px solid #fcfff4;
	border-top: none;
	border-right: none;

	-webkit-transform: rotate(-45deg);
	-moz-transform: rotate(-45deg);
	-o-transform: rotate(-45deg);
	-ms-transform: rotate(-45deg);
	transform: rotate(-45deg);
}
    .squaredThree label:hover::after {
	-ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=30)";
	filter: alpha(opacity=30);
	opacity: 0.3;
}

.squaredThree input[type=checkbox]:checked + label:after {
	-ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=100)";
	filter: alpha(opacity=100);
	opacity: 1;
}
input[type=checkbox] {
	visibility: hidden;
}
        .auto-style1 {
            width: 25px;
        }
        .txtbox_format{
             display: inline-block;
  -webkit-box-sizing: content-box;
  -moz-box-sizing: content-box;
  box-sizing: content-box;
  padding: 10px 20px;
  border: 2px solid #b7b7b7;
  -webkit-border-radius: 9px;
  border-radius: 9px;
  font-size: 14px;
  color: rgba(0,142,198,1);
  -o-text-overflow: clip;
  text-overflow: clip;
  background: rgba(255,255,255,1);
  text-shadow: 1px 1px 0 rgba(255,255,255,0.66) ;
  -webkit-transition: all 200ms cubic-bezier(0.42, 0, 0.58, 1);
  -moz-transition: all 200ms cubic-bezier(0.42, 0, 0.58, 1);
  -o-transition: all 200ms cubic-bezier(0.42, 0, 0.58, 1);
  transition: all 200ms cubic-bezier(0.42, 0, 0.58, 1);
  width: 250px;
        }

        .btn {
  -webkit-border-radius: 6;
  -moz-border-radius: 6;
  border-radius: 6px;
  font-family: Arial;
  color: #ffffff;
  font-size: 20px;
  background: #EB473D;
  padding: 10px 20px 10px 20px;
  text-decoration: none;
}

.btn:hover {
  background: #db170d;
  text-decoration: none;
}
    </style>

    <script type="text/javascript">

        function Edit(CID)
        {
            $('#CreditNoteID').val(CID);
            $.ajax({
                url: '../Fetch/FetchRMADetails.aspx',
                data: {
                    RID: CID
                },
                success: function (response) {
                    var splitString = response.split('|');
                    if (splitString[0] == '1')
                    {
                        $('#chk_S2S').prop('checked', true);
                    }
                    else
                    {
                        $('#chk_S2S').prop('checked', false);
                    }

                    if (splitString[1] == '1')
                    {
                        $('#chk_AR').prop('checked', true);
                    }
                    else
                    {
                        $('#chk_AR').prop('checked', false);
                    }

                    if (splitString[2] == '1')
                    {
                        $('#chk_CIX').prop('checked', true);
                    }
                    else
                    {
                        $('#chk_CIX').prop('checked', false);
                    }

                    if (splitString[3] == '1')
                    {
                        $('#chk_R2C').prop('checked', true);
                    }
                    else
                    {
                        $('#chk_R2C').prop('checked', false);
                    }

                    if (splitString[4] == '1')
                    {
                        $('#chk_ANFS').prop('checked', true);
                    }
                    else
                    {
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

        function format(d) {
            // `d` is the original data object for the row
            return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
                '<tr>' +
                    '<td>Sent To Supplier Date Time:</td>' +
                    '<td>' + d.SentToSupplierDateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Approved RMA Date Time:</td>' +
                    '<td>' + d.ApprovedRMADateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Credit In Xero Date Time:</td>' +
                    '<td>' + d.CreditInXeroDateTime + '</td>' +
                '</tr>' +
                '<tr>' +
                    '<td>Sent To Customer Date Time:</td>' +
                    '<td>' + d.RMAToCustomerDateTime + '</td>' +
                '</tr>' +
                 '<tr>' +
                    '<td>Adjustment Notice Date Time:</td>' +
                    '<td>' + d.AdjustedNoteFromSupplierDateTime + '</td>' +
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

            var table = $('#example').DataTable({
                columns: [
                    {
                        "className": 'details-control',
                        "orderable": false,
                        "data": null,
                        "defaultContent": ''
                    },
                    { 'data': 'CreditNoteID' },
                    { 'data': 'XeroCreditNote' },
                    { 'data': 'SupplierName' },
                    { 'data': 'Raised' },
                    { 'data': 'RaisedDateTime' },
                    { 'data': 'SentToSupplier' },
                    { 'data': 'ApprovedRMA' },
                    { 'data': 'RMAToCustomer' },
                    { 'data': 'AdjustedNoteFromSupplier' },
                    { 'data': 'CreditInXero' },
                    { 'data': 'InHouse' },
                    { 'data': 'SupplierRMANumber' },
                    { 'data': 'TrackingNumber' },
                    { 'data': 'Status' },
                    { 'data': 'ViewEdit' },
                ],
                bServerSide: true,
                "iDisplayLength": 25,
                sAjaxSource: "../DataHandlers/OngoingRMADataHandler.ashx",
                "columnDefs": [
                    {
                        "targets": [1],
                        "visible": false,
                        "searchable": true
                    },
                    { className: 'align_center', "targets": [4, 5, 6, 7, 8, 9, 10, 14] },
                ]

            });

            $('#example tbody').on('click', 'td.details-control', function () {
                var tr = $(this).closest('tr');
                var row = table.row(tr);

                if (row.child.isShown()) {
                    // This row is already open - close it
                    row.child.hide();
                    tr.removeClass('shown');
                }
                else {
                    // Open this row
                    row.child(format(row.data())).show();
                    tr.addClass('shown');
                }
            });

        })
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="1280" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">RMA TRACKING</td>
        </tr>

        <tr>
            <td  class="section_headings">&nbsp;</td>
        </tr>

        <tr>
          <td class="white-box-outline">
              <table align="center" cellpadding="0" cellspacing="0" style="width:1080px;" >
                  <tr>
                      <td height="20px">&nbsp;</td>
                  </tr>

                  <tr>
                      <td>

                          <div >
<table cellpadding="0" cellspacing="0" border="0"  id="example" style="cursor:pointer" >
	<thead>
		<tr>
            <th style="width:25px;"></th>
            <th align="left">CREDIT NOTE ID</th>
            <th>CREDIT #</th>
            <th align="left">SUPPLIER NAME</th>
            <th align="left">RAISED</th>
			<th align="left">RAISED DATE</th>
			<th align="left">SENT TO SUPPLIER</th>
            <th align="left">APPROVED RMA</th>
            
            <th align="left">SENT TO CUSTOMER</th>
            <th align="left">ADJUSTED NOTE</th>
            <th align="left">CREDIT IN XERO</th>
             <th align="left">In House</th>
            <th>RMA #</th>
            <th>Tracking #</th>
			<th align="left">STATUS</th>
            <th align="left">VIEW/EDIT</th>
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
