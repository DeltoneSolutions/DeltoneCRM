<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddXeroReference.aspx.cs"  MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.Manage.AddXeroReference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/jquery-1.11.1.min.js"></script>
    <script src="../js/jquery-ui-1.10.3.custom.js"></script>
  

    
    <script src="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>
    <link href="//cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/themes/cupertino/jquery-ui.min.css" rel="stylesheet" />



    <%--<script type="text/javascript" src="Scripts/jquery-ui-timepicker-addon-i18n.min.js"></script>--%>
    <title>Update XeroReference</title>

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

            $('#createrecordxero').click(function () {
            UpdateCompanyReference();
        });

        });

        function callValidationMethod() {

            var comname = $("#companyname").val();

            if(comname=="")
            {
                alert("Please enter company name");
                return false;
            }

            var xeroReference = $("#xeroreference").val();

            if (xeroReference == "") {
                alert("Please enter xero reference");
                return false;
            }

            return true;
        }

        function ShowProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = 'visible';
        }

        function HideProgressBar() {
            document.getElementById('dvProgressBar').style.visibility = "hidden";
        }


        function UpdateCompanyReference() {

            if (callValidationMethod()) {

                ShowProgressBar();
                $.ajax({
                    type: "POST",
                    url: "Process/updatecompanyxeroreference.aspx",
                    data: {
                        Companyname: $('#companyname').val(),
                        companyXero: $('#xeroreference').val()
                    },
                    success: function (msg) {
                        //console.log(msg);
                        if (msg == "1") {
                            HideProgressBar();
                            $('#companyname').val('');
                            $('#xeroreference').val('');
                            alert('Successfully Updated');
                          
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
        }

   

    </script>

    <style type="text/css">
        .imageerror{
            max-width:1000px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="gridDiv">
       
     <h4>PLEASE CHECK ERROR MESSAGE </h4>

          <asp:Button OnClick="btnUploadBack" ID="Button2" Text="BACK" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight undoButtonStyle" CausesValidation="false" />
        <asp:Button OnClick="btnupload_Click" ID="Button1" Text="DASHBOARD" ForeColor="Blue" Width="10%" runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />


     <asp:Image ID="dyImage" runat="server" ImageUrl="~/Images/CapZeroError.PNG"  CssClass="imageerror" />
    
         <br /><br />
         
        
         <h3> <span style="color:red"> 
             <asp:Literal  ID="messagelabel"  runat="server"></asp:Literal> </span> </h3>
         <br /><br />
      

        <div id="main">
            <br />

            <table width="580"  border="0" align="center" cellpadding="0" cellspacing="0">

            <tr>
                              <td width="300"><table width="550" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <div id="errDiv"></div>
                                  <td width="250" class="labels-style">COMPANY NAME&nbsp;&nbsp;&nbsp;</td>
                                  <td width="200"><input name="companyname" type="text" style="width:350px;height:25px;" class="textbox_001" autocomplete="off" title="Please enter company name" id="companyname" /></td>
                                </tr>
                                   </tr>
                
<tr>
                          <td height="15">&nbsp;</td>
                        </tr>
                                   <tr>
                                  <td width="250" class="labels-style">XERO REFERENCE&nbsp;&nbsp;&nbsp;</td>
                                  <td width="215"><input name="xeroreference" type="text" style="width:350px;height:25px;" autocomplete="off" class="txtbox-200-style-edit" id="xeroreference" /></td>
                                </tr>
                                   <tr>

                                       <tr>
                          <td height="15">&nbsp;</td>
                                              <div id="dvProgressBar" style="float: inherit; visibility: hidden;">
                            <img src="/images/loadingimage1.gif" />
                            <strong style="color: red;">Updating, Please Wait...</strong>
                        </div>
                        </tr>
                                   <tr>
                                  <td width="250" class="labels-style"></td>
                                  <td width="215"><input name="createrecordxero" type="button" id="createrecordxero" style="height:30px;" class="clk_button_01" 
                                      id="createrecord" value="UPDATE XERO REFERENCE" />   </td>
                                </tr>
                                   <tr>
       
        </table> 


        </div>
    </div>



    <script>
      
    </script>
</asp:Content>
