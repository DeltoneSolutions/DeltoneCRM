<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteAllocateRep.aspx.cs" MasterPageFile="~/SiteInternalNavLevel1.Master" Inherits="DeltoneCRM.QuoteAllocateRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
   <%-- <link href="css/jquery.dataTables_new.css" rel="stylesheet" />--%>
    <script src="js/jquery-1.11.1.min.js"></script>
    <link href="css/NewCSS.css" rel="stylesheet" />
    <script src="Scripts/jquery-ui.js"></script>

    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.0/jquery-ui.min.js"></script>
    <script src="js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="css/jquery.dataTables_new.css" />

    <%-- <link href="Scripts/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
    <script src="Scripts/jscolor.js"></script>
    <link rel="stylesheet" media="all" type="text/css" href="http://code.jquery.com/ui/1.11.0/themes/smoothness/jquery-ui.css" />
 <script type="text/javascript" src="https://cdn.datatables.net/r/dt/jq-2.1.4,jszip-2.5.0,pdfmake-0.1.18,dt-1.10.9,af-2.0.0,b-1.0.3,b-colvis-1.0.3,b-html5-1.0.3,b-print-1.0.3,se-1.0.1/datatables.min.js"></script>
  <%--  <script type="text/javascript"  src="//code.jquery.com/jquery-1.12.4.js">
	</script>--%>
<%--	<script type="text/javascript"  src="https://cdn.datatables.net/1.10.15/js/jquery.dataTables.min.js">
	</script>--%>
	<script type="text/javascript"  src="https://cdn.datatables.net/buttons/1.3.1/js/dataTables.buttons.min.js">
	</script>
	<script type="text/javascript"  src="//cdn.datatables.net/buttons/1.3.1/js/buttons.print.min.js">
        </script>
    <title>Company List</title>

    <style type='text/css'>
        /* css for timepicker */
       

          body {
           background-color: #b3ffaa !important;
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

.buttons-print{
     float:left;
      background-color: #ffffff;
      height:40px;
      padding: 2px 20px;
    text-decoration: none;
    border: solid 1px #3E3E42;
    height:20px;
    cursor:pointer;
     margin-bottom:10px;
    color:blue;
    text-align:center;
       font-weight:bold;
        margin-left:10px;

}
    </style>

    <script type="text/javascript">
        var table;

        $(document).ready(function () {
            callMe();
        });

        function callMe() {

          
                CreateTableData();
        }


        function CreateTableData(){

            if (table)
                table.destroy();

            table = $('#noordercompany').DataTable({
                processing: true,
                "language": {
                    "processing": '<img src="/images/loadingimage1.gif"> Hang on. Waiting for response...</img>' //add a loading image,simply putting <img src="loader.gif" /> tag.
                },
                "ajax": "../Fetch/FetchAllocatedQuotesRep.aspx",
                "columnDefs": [
                        {
                            className: 'align_left', "targets": [0, 1, 2, 4, 5],
                            className: 'align_center', "targets": [3],
                        },

                        { "width": "10%", "targets": 0 }
                ],
                "aaSorting": [[3, "asc"]],
                "iDisplayLength": 25,
               

            });
        }

        function Edit(QuoteID, CompanyID, ContactID, dbty) {
            window.open("RepAllocateQuoteDisplay.aspx?OderID=" + QuoteID + "&CompID=" + CompanyID + "&cid=" + ContactID + "&DB=" + dbty + "&Flag=Y", '_blank');
        }

        function OpenCompany(CompanyID) {

            window.open('CompanyInfoLead.aspx?companyid=' + CompanyID, '_blank');
        }

        function printDiv() {
            var divToPrint = document.getElementById("noordercompany");
            newWin = window.open("");
            newWin.document.write(divToPrint.outerHTML);
            newWin.print();
            newWin.close();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div id="gridDiv">
        <br />


        <br />
        <asp:Label ID="messagelable" runat="server" CssClass="messagecss"></asp:Label>
        <br />
        <br />

        <div id="main">
            <br />
            <br />


            <table width="1200" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">
                <tr>
                    <td height="25px"></td>
                </tr>
             
              
        <tr>
            <td height="25px" class="section_headings">ALLOCATED QUOTES</td>
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
                                        <table cellpadding="0" width="1000" cellspacing="0" border="0" class="display" id="noordercompany" style="cursor: pointer">
                                            <thead>
                                               <tr>
            <th>QUOTE ID</th>
                                                    <th>CREATED DATE</th>
                                                    <th>ALLOCATED DATE</th>
                                                   <th>REMAINING DAYS</th>
            <th>COMPANY NAME</th>
                                                    <th>CONTACT NAME</th>
           
			                                        <th>QUOTE TOTAL</th>
            <th>QUOTED BY</th>
            <th>CUSTOMER TYPE</th>
                                                    <th>STATUS</th>
                                                    <th>VIEW</th>
            
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

    </div>
</asp:Content>
