<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalesRepCS.aspx.cs" 
    enableEventValidation="false"  MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.SalesRepCS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      
       <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="../js/jquery-2.1.3.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    <link href="../css/Overall.css" rel="stylesheet" />
    
  <%--  <script src="../js/jquery.dataTables.min.js"></script>--%>

     <script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
    </script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js">
    </script>

    <title> Sales Rep CS</title>
    <style type='text/css'>
        /* css for timepicker */
        .ui-timepicker-div dl {
            text-align: left;
        }

            .ui-timepicker-div dl dt {
                height: 25px;
            }

            .ui-timepicker-div dl dd {
                margin: -25px 0 10px 65px;
            }

        .style1 {
            width: 100%;
        }

        /* table fields alignment*/
        .alignRight {
            text-align: right;
            padding-right: 10px;
            padding-bottom: 10px;
        }

        .alignLeft {
            text-align: left;
            padding-bottom: 10px;
        }

        body {
            margin: 40px 10px;
            padding: 0;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
            font-size: 22px;
        }
  

        #calendar {
            max-width: 1000px;
            margin: 0 auto;
        }

        .fc-event {
            font-size: 1.4em !important;
        }

        .fc button {
            font-size: 1.4em !important;
        }

        .different-tip-color {
            background-color: #CAED9E;
            border-color: #90D93F;
            color: #3F6219;
        }

        th.ui-datepicker-week-end,
td.ui-datepicker-week-end {
    display: none;
}

         .buttonClass
{
    padding: 2px 20px;
    text-decoration: none;
    border: solid 1px #3E3E42;
    height:40px;
    cursor:pointer;
}
.buttonClass:hover
{
    border: solid 1px Black;
    background-color: #ffffff;
}
.moveRight{
    float:right;
    margin-bottom:30px;
    color:blue;
    text-align:center;
       font-weight:bold;
}


.button3 {background-color: #4CAF50;} /* Green */
.button2 {background-color: #008CBA;} /* Blue */
.button4 {background-color: #f44336;} /* Red */ 
.button1 {background-color: #e7e7e7; } /* Gray */ 
.button5 {background-color: #555555;} /* Black */
.button0 {background-color: white;} /* Black */

       .tab01-01 {
             font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #fff;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab01-01:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab01-02 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e7e7e7;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab01-02:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
                .tab02-01 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e7e7e7;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab02-01:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab02-02 {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #ffffff;
            padding-left: 15px;
            border-top-color: #cccccc;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
        }
        .tab02-03 {
            font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e6e9ee;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .tab02-03:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
        .tab01-01Noclicked {
           font-family: 'Raleway', sans-serif;
            color: #b6b5b5;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #e7e7e7;
            height: 35px;
            padding-left: 15px;
            border-bottom-color: #cccccc;
            border-bottom-style: solid;
            border-bottom-width: 1px;
            background-color: #e7e7e7;
          
        }
            .tab01-01Noclicked:hover {
            color: #333333;
            background-color: #c4dde6;
            cursor: pointer;
        }
              .tabclicked {
            font-family: 'Raleway', sans-serif;
            color: #333333;
            text-align:left;
            font-size:14px;
            font-weight:600;
            background-color: #b6b5b5;
            padding-left: 15px;
            border-top-color: #cccccc;
            border-top-style: solid;
            border-top-width: 1px;
            border-left-color: #cccccc;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-color: #cccccc;
            border-right-style: solid;
            border-right-width: 1px;
        }
                      .ui-widget-content.ui-dialog {
    border: 1px solid #000 !important;
}


                       .buttons-print {
            float: left;
            background-color: #ffffff;
            height: 40px;
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 20px;
            cursor: pointer;
            margin-bottom: 10px;
            color: blue;
            text-align: center;
            font-weight: bold;
            margin-left: 10px;
        }
    </style>

    <script type="text/javascript">
        var canCall = '<%= SampleVariable %>';


        function openNotCompleted() {

            callSalesRepData("nc");
            $('#alltab').removeClass("tab01-01");
            $('#alltab').addClass("tabclicked");
            $('#newtab').addClass("tab01-02");
            $('#newtab').addClass("tab01-01Noclicked");
            $('#newtab').removeClass("tabclicked");
            
        }


        function opencompleted() {
            callSalesRepData("co");
            $('#alltab').addClass("tab01-01");
            $('#alltab').removeClass("tabclicked");
            $('#alltab').addClass("tab01-01Noclicked");
            $('#newtab').addClass("tabclicked");
            $('#newtab').removeClass("tab01-02");
           
        }

        var table;
        $(document).ready(function () {

            openNotCompleted();

        });

        function callSalesRepData(par) {

            if (canCall) {
                if (table)
                    table.destroy();
                table = $('#salesRep').DataTable({
                    "ajax": "Fetch/FetchSalesRepCS.aspx",
                    "columnDefs": [
                         { className: 'align_left', "targets": [0, 1,  3] },

                    ],
                    dom: 'lBfrtip',
                    buttons: [
                        'print'
                    ],
                    "order": [[0, "desc"]],
                    "iDisplayLength": 25,
                    "fnServerParams": function (aoData) {
                        aoData.push(
                            { "name": "category", "value": par }
                        );
                    }

                });
            }
            else {
                if (table)
                    table.destroy();
                table = $('#salesRep').DataTable({
                    "ajax": "Fetch/FetchSalesRepCS.aspx",
                    "columnDefs": [
                         { className: 'align_left', "targets": [0, 1,  3] },
                         {
                             "targets": [9],
                             "visible": false,
                             "searchable": false
                         },
                    ],
                    dom: 'lBfrtip',
                    buttons: [
                        'print'
                    ],
                    "order": [[0, "desc"]],
                    "iDisplayLength": 25,
                    "fnServerParams": function (aoData) {
                        aoData.push(
                            { "name": "category", "value": par }
                        );
                    }

                });
            }
        }

        

    </script>

        </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagerforsalesRep" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
  <%-- <asp:Button OnClick="openDialog();" ID="btnopendialog" Text="Add New" ForeColor="Blue" Width="20%" runat="server" CausesValidation="false" />--%>
    <div id="main">
        <br />
         <br />
        
       
   <table width="1600" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
         <tr>
          <td height="25px"> 
         <asp:Button OnClick="btnupload_Click" ID="btnDashboard" Text="Dashboard" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" /> </td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">Sales Rep Customer Service</td>
        </tr>
        <td  class="section_headings">
                <table >
                     <tr>
                         <td width="250px" class="tab01-01" id="alltab" onclick="openNotCompleted();">Not Completed</td>
                         <td width="250px" class="tab01-02" id="newtab" onclick="opencompleted();">Completed</td>
                        
                     </tr>
                 </table>
     </td>
        
      
        <tr>
          <td class="white-box-outline">
              <table align="center" cellpadding="0" cellspacing="0" class="inner-table-size">
                  <tr>
                      <td height="20px">&nbsp;</td>
                  </tr>

                  <tr>
                      <td>

                          <div class="container">
<table cellpadding="0" width="1500" cellspacing="0" border="0" class="display" id="salesRep" style="cursor:pointer">
	<thead>
		<tr>
            <th align="left">Id</th>
			<th align="left">Date</th>
            <th align="left">CS Type</th>
            <th align="left">Invoice #</th>
            <th align="left">Account</th>
			<th align="left">Contact #</th>
			<th align="left">Contact Name</th>
            <th align="left">Issue</th>
            <th align="left">OutCome</th>
            <th align="left">Question</th>
            <th align="left">Rep Name</th>
			<th align="left">Invoice Due Date</th>
            <th align="left">EDIT</th>
            <%--<th align="left">DELETE</th>--%>
		</tr>
        
	</thead>
    
            


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

      
      </table>

    </div>
     <div id="addDialogCs" style="font: 70% 'Trebuchet MS', sans-serif; margin: 50px; display: none;" title="Update Customer Service">
        <table class="style1">
            <tr>
                <td class="alignRight" ></td>
                <td class="alignRight" >  <span id="nameCom" style="float:left;font-weight:bold;color:red;font-size:20px;"></span></td>
            </tr>
             <tr id="alinktr" >
                <td class="alignRight" ></td>
                <td class="alignRight" >
                  
                   <a id="urlLinkOrder" target="_blank" style="color:red;font-size:15px;margin-right:12px;color:blueviolet;">View Order</a>  <a id="urlLink" target="_blank" style="color:red;font-size:15px;color:blueviolet;">View Account</a></td>

            </tr>
            <tr>
                <td class="alignRight">ISSUE:</td>
                <td class="alignLeft">
                    <textarea id="complaintMessage"  cols="70" rows="8"></textarea><br />
                </td>
            </tr>
            <tr>
                <td class="alignRight">OUTCOME :</td>
                <td class="alignLeft">
                    <textarea id="outcomeMessage" cols="70" rows="8"></textarea></td>
            </tr>
       <tr>
                <td class="alignRight">QUESTION :</td>
                <td class="alignLeft">
                    <textarea id="questionMessage" cols="70" rows="8"></textarea></td>
            </tr>

             <tr>
                <td class="alignRight">COMPLETED :</td>
                <td class="alignLeft">
                    <input type="checkbox" id="statusCheck"/></td>
            </tr>
           
        </table>

    </div>
    <script type="text/javascript">
        
        var clickedId=0;
        var statusCheck=false;
        $(document).ready(function () {
            $('#addDialogCs').dialog({
                autoOpen: false,
                width: 700,
                modal: true,
                buttons: [

            {
                text: "Cancel",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    $(this).dialog("close");
                }
            },
            {
                text: "Update",
                "class": 'ui-button ui-corner-all ui-widget',
                click: function () {
                    statusCheck = false;
                    if ($('#statusCheck').is(":checked")) {
                        statusCheck = true;
                    }
                    //alert("sent:" + addStartDate.format("dd-MM-yyyy hh:mm:ss tt") + "==" + addStartDate.toLocaleString());
                    var RaiseSalesCsUI = {
                        Complaint: $("#complaintMessage").val(),
                        OutCome: $("#outcomeMessage").val(),
                        Question: $("#questionMessage").val(),
                        Id: clickedId,
                        Status:statusCheck

                    };

                    if (validateInput()) {
                        //alert("sending " + eventToAdd.title);

                        PageMethods.UpdateRepCS(RaiseSalesCsUI, addSuccess);
                        $(this).dialog("close");
                    }
                }
            }
                ]
            });

        });
        function RaiseSalesCsUI() {
            this.Id = 0;
            this.Complaint = "";
            this.OutCome = "";
            this.Question = "";
        }

        function ViewCal(EveId) {
            objSalesRep = new RaiseSalesCsUI();
            clickedId = EveId;
            $('#salesRep tbody').on('click', 'tr', function () {
                var ccc = table.row(this).data();
                $("#nameCom").text(ccc[4] + " --> " + ccc[3]);
            });
            objSalesRep = PageMethods.ReadSalesRepCS(EveId, onSucceed, onError);
            return false;
        }

        function onSucceed(response) {
            document.getElementById("statusCheck").checked = false;
            $("#complaintMessage").val(response.Complaint);
            $("#outcomeMessage").val(response.OutCome);
            $("#questionMessage").val(response.Question);
            var url = "ConpanyInfo.aspx?companyid=" + response.companyId;
            $("#urlLink").attr("href",url);
            if (response.Status)
                document.getElementById("statusCheck").checked = true;
            if (response.CsTyte == "1") {
                $("#urlLinkOrder").text("View Order");
                var urlOrder = "order.aspx?Oderid=" + response.orderId + "&cid=" + response.contactId + "&Compid=" + response.companyId;
                $("#urlLinkOrder").attr("href", urlOrder);
            }
            else {
                var urlOrder = "UpdateCredit.aspx?CreditNoteID=" + response.orderId + "&cid=" + response.contactId + "&Compid=" + response.companyId;
                $("#urlLinkOrder").text("View Credit");
                $("#urlLinkOrder").attr("href", urlOrder);
            }
           
            $('#addDialogCs').dialog('open');
        }
        function onError(err) {

            alert(err);
        }
        function addSuccess(addResult) {

            alert("CS Successfully Updated.");
            location.reload();
        }


        function validateInput() {

            if ($("#complaintMessage").val() == "") {
                alert("Please enter Issue");
                return false;
            }
            //if ($("#outcomeMessage").val() == "") {
            //    alert("Please enter outcome");
            //    return false;
            //}



            return true;
        }

        function DeleteCal(eveID) {

            var r = confirm("Do you want to delete this record?");
            if (r == true) {
                PageMethods.DeleteRecordCS(eveID, deletestatus);
            }
        }

        function deletestatus(addResult) {

            alert("CS Successfully Deleted.");
            location.reload();
        }

        function openWinAccount( CompID) {
            var url = "ConpanyInfo.aspx?companyid=" + encodeURIComponent(CompID);
            window.open(url);
        }
    </script>
  
            </asp:Content>