<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateCommission.aspx.cs" MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.Manage.UpdateCommission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
  <script src="../js/jquery-1.11.1.min.js" type="text/javascript"></script>
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <script src="../js/jquery.validate.js" type="text/javascript"></script>
        <link href="../css/ManageCSS.css" rel="stylesheet" />
         <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../css/jquery.dataTables_new.css" />



    <%--<script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>--%>
    <title>Update XeroReference</title>

     <script type="text/javascript">

         $(document).ready(function () {
             $('#findcustomerorder').autocomplete({
                 source: "../Fetch/FetchSearchOrder.aspx",
                 select: function (event, ui) {
                     //window.open('ConpanyInfo.aspx?companyid=' + ui.item.id);
                     // $('#CompanyContactTR').show();
                     // $('#CompanyContactiFrame').attr('src', 'CompanyContactInfo.aspx?cid=' + ui.item.id);
                     var valsSPLIT = ui.item.id.split(',');;

                     var url = '../order.aspx?Oderid=' + (valsSPLIT[0]) + '&cid=' + (valsSPLIT[2])
                             + '&Compid=' + (valsSPLIT[1]);
                     window.open(url, "_blank");
                     // $(this).val(''); return false;


                 }
             });
         });
    </script>

    <style type='text/css'>
        /* css for timepicker */


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

        #gridDiv {
            max-width: 70%;
            margin: 0 auto;
            width: 70%;
            min-height: 150px;
            text-align: center;
            font-weight: bold;
        }

        .messagecss {
            margin-left: 300px;
            color: green;
            text-align: center;
            font-weight: bold;
        }


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

        #gridDiv {
            max-width: 70%;
            margin: 0 auto;
            width: 70%;
            min-height: 150px;
            text-align: center;
            font-weight: bold;
        }

        .messagecss {
            margin-left: 300px;
            color: green;
            text-align: center;
            font-weight: bold;
        }

        .buttonClass {
            padding: 2px 20px;
            text-decoration: none;
            border: solid 1px #3E3E42;
            height: 40px;
            cursor: pointer;
        }

            .buttonClass:hover {
                border: solid 1px Black;
                background-color: #ffffff;
            }

        .moveRight {
            float: right;
            margin-bottom: 30px;
            color: blue;
            text-align: center;
            font-weight: bold;
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
            padding-top: 4px;
        }

        .buttons-excel {
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
            padding-top: 4px;
        }

        .undoButtonStyle {
            margin-left: 30px;
        }

        .inputColorGreen {
            border-top-color: #4fc2bd;
            border-right-color: #4fc2bd;
            border-left-color: #4fc2bd;
            border-bottom-color: #4fc2bd;
            border-top-width: 1px;
            border-right-width: 1px;
            border-left-width: 1px;
            border-bottom-width: 1px;
            border-top-style: solid;
            border-right-style: solid;
            border-bottom-style: solid;
            border-left-style: solid;
            outline: none;
            float: right;
        }

        .inputTextStyle {
            float: right;
            text-align: right;
            color: #4fc2bd !important;
        }

        #drop_zone {
            max-width: 40%;
            margin: 0 auto;
            width: 40%;
            min-height: 150px;
            text-align: center;
            text-transform: uppercase;
            font-weight: bold;
            font-size: 30px;
            border: 8px dashed #3E3E42;
            height: 160px;
        }

        #spanConatt {
            display: block;
            vertical-align: middle;
            line-height: normal;
            margin-top: 50px;
            opacity: 0.3;
        }

        td.highlight {
            font-weight: bold;
            color: red !important;
        }

        td.highlightclicked {
            font-weight: bold;
            color: #4274f4 !important;
        }

        /*tr.odd { background-color: blue; } 
 tr.even { background-color: green; }

table.dataTable tr.odd .sorting_1 { background-color: blue; } 
table.dataTable tr.even .sorting_1 { background-color: green; }*/


        table.dataTable tbody tr.myeven {
            background-color: #f2dede !important;
        }

        table.dataTable tbody tr.myodd {
            background-color: #bce8f1 !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {

           

        });

       

        function ShowProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
        }

        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
        }


       
   

    </script>

    <style type="text/css">
        .imageerror{
            max-width:1000px;
        }
        .auto-style1 {
            width: 1087px;
        }
        .auto-style2 {
            background-color: #FFF;
            border: 1px solid #CCCCCC;
            width: 1087px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="gridDiv">
       
     <h4>PLEASE ENTER OUR ORDER NUMBER </h4>

          <asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false" />
        <asp:Button OnClick="btnupload_Click" ID="Button1" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />


   
         <br /><br />
         
        
         <h3> <span style="color:red"> 
             <asp:Literal  ID="messagelabel"  runat="server"></asp:Literal> </span> </h3>
         <br /><br />
      

       
            <br />

            <table width="580"  border="0" align="center" cellpadding="0" cellspacing="0">

                
<tr>
                          <td height="15" class="auto-style1">&nbsp;</td>
                        </tr>
                                   <tr>
                                  <td class="auto-style1">Order Number(21304)&nbsp;&nbsp;&nbsp;</td>
                                  <td width="215"><input name="ordernumberdisplay" type="text" style="width:150px;height:25px;" autocomplete="off" class="txtbox-200-style-edit" id="ordernumberdisplay" /></td>
                                </tr>
                 <tr>
                          <td height="15" class="auto-style1">&nbsp;</td>
                        </tr>
                                  
                                        <tr>
                                  <td class="auto-style1"></td>
                                  <td width="215"><input name="getxeroordercommision" type="button" id="getxeroordercommision" style="height:30px;" class="clk_button_01" 
                                      id="createrecord" value="DISPLAY ORDER COMMISSION" onclick="loadHolidayTable();"/>   </td>
                                </tr>
                                         <tr>
                                              
                        <td >
                            <table align="center" cellpadding="0" cellspacing="0" class="width-940-style">
                                <tr>
                                    <td height="20px">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td  class="width-940-style">



                                        <table id="orderlistconfig"  class="width-940-style">

                                            <thead>
                                                <tr>
                                                    <th align="left">CommId</th>
                                                    <th align="left">UserId </th>
                                                    <th align="left">RepName </th>
                                                   <th align="left">Amount </th>
                                                    <th align="left">OrderId </th>
                                                </tr>
                                            </thead>

                                            <tbody>
                                            </tbody>
                                        </table>

                                    </td>
                                </tr>
                                <tr>
                                    <td height="20px">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                                  

                                       <tr>
                          <td height="15" class="auto-style1">&nbsp;</td>
                                              <div id="dvProgressBar" style="float: inherit; visibility: hidden;">
                            <img src="/images/loadingimage1.gif" />
                            <strong style="color: red;">Updating, Please Wait...</strong>
                        </div>
                                             <tr>
                                
                                  <td class="auto-style1">User Id(10)<input name="useridone" type="text" style="width:150px;height:25px;" autocomplete="off" class="txtbox-200-style-edit" id="useridone" />
                                     
                                  </td>
                                                    <td width="215"> Commission   <input name="useridonecommission" type="text" style="width:150px;height:25px;" autocomplete="off" class="txtbox-200-style-edit" id="useridonecommission" /></td>
                                </tr>

                                   <tr>
                          <td height="15" class="auto-style1">&nbsp;</td>
                        </tr>
                                            
                        </tr>
                                         <tr>
                          <td height="15" class="auto-style1">&nbsp;</td>
                        </tr>
                                   <tr>
                                  <td class="auto-style1"></td>
                                  <td width="215"><input name="createrecordxero" type="button" id="createrecordxero" style="height:30px;" class="clk_button_01" 
                                      id="createrecord" value="UPDATE ORDER COMMISSION"  onclick="UpdateCommissionOrder();"/>   </td>
                                </tr>
                                          
        </table> 


       
    </div>



    <script>


        function UpdateCommissionOrder() {

           

                ShowProgressBar();
                $.ajax({
                    type: "POST",
                    url: "Process/ProcessAddCommission.aspx",
                    data: {
                        orderId: $('#ordernumberdisplay').val(),
                        userid: $('#useridone').val(),
                        amount: $('#useridonecommission').val()
                    },
                    success: function (msg) {
                        //console.log(msg);
                        if (msg == "Ok") {
                            HideProgressBar();
                            $('#useridone').val('');
                            $('#useridonecommission').val('');
                            alert('Successfully Updated');
                            loadHolidayTable();

                        }
                        else {
                            HideProgressBar();
                            alert('Error Occurred');
                        }
                        // window.parent.closeAddFrame();
                        // window.parent.location.reload(false);
                    },
                    error: function (xhr, err) {
                        alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                        alert("responseText: " + xhr.responseText);
                    },
                });
            
        }

        var linkTableComDatatable;
        function loadHolidayTable() {

            console.log('rrr');
            if (linkTableComDatatable) {
                linkTableComDatatable.destroy();
            }
            var catValue = $("#ordernumberdisplay").val();
            linkTableComDatatable = $('#orderlistconfig').DataTable({
                "ajax": "Process/FetchOrderCommission.aspx",
               "aaSorting": [[0, "desc"]],
               "iDisplayLength": 25,
               "fnServerParams": function (aoData) {
                   aoData.push(
                       { "name": "itemId", "value": catValue }
                   );
               }
           });
        }


        $(document).ready(function () {
            // loadHolidayTable();

            $('#orderlistconfig').DataTable();

        });
      
    </script>
</asp:Content>

