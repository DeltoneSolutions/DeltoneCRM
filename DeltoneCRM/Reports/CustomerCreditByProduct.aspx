﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerCreditByProduct.aspx.cs"
     MasterPageFile="~/NoNav.Master" Inherits="DeltoneCRM.Reports.CustomerCreditByProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="http://code.jquery.com/jquery-latest.js"></script>
	<script src="../js/jquery-ui-1.10.3.custom.js"></script>
    <link href="../css/jquery.dataTables_new.css" rel="stylesheet" />
    
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            //var table = $('#protable').DataTable({
            //    "ajax": "queries/getOrderedProducts.aspx",
            //    "aaSorting": [[0, "desc"]],
            //         "iDisplayLength": 50,
            // });


        });


        //function CreateTableData(catValue) {
        //    $('#callbacktableDiv ').hide();
        //    $('#notcallbacktable').show();
        //    if (table)
        //        table.destroy();

        //    table = $('#livedbtbl').DataTable({
        //        "ajax": "../Fetch/FetchAllQuotes.aspx?n=1",
        //        "aaSorting": [[0, "desc"]],
        //        "iDisplayLength": 25,
        //        "fnServerParams": function (aoData) {
        //            aoData.push(
        //                { "name": "category", "value": catValue }
        //            );
        //        }
        //    });
        //}

        $(function () {
            $('#<%=searchTextBox.ClientID%>').autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        url: "CustomersBYProduct.aspx/GetProductCode",
                        data: "{ 'pre':'" + request.term + "'}",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert("Error");
                        }
                    });
                }
            });
        });

        function reloadPage() {
            $("#ContentPlaceHolder1_searchTextBox").val("");
            $("#ContentPlaceHolder1_StartDateSTxt").val("");
            $("#ContentPlaceHolder1_EndDateSTxt").val("");
            $("#ContentPlaceHolder1_slTypeOfCredit").val("0");
            location.reload();
        }

        function PrintPage() {
            var printContent = document.getElementById
            ('<%= pnlGridView.ClientID %>');
            var printWindow = window.open("All Records",
            "Print Panel", 'left=50000,top=50000,width=0,height=0');
            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
        }

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
            width:70%; 
       min-height:150px; 
       text-align:center;
       font-weight:bold;
      
        }
           .messagecss{
    margin-left:300px;
    color:green;
    text-align:center;
       font-weight:bold;
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
            margin-bottom: 10px;
            margin-top: 10px;
            margin-right: 200px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }

        .moverigRight{
              float: right;
            margin-bottom: 10px;
            margin-top: 10px;
            margin-right: 40px;
            color: blue;
            text-align: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManagerProCodeSearch" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    
      <div>
            
              
           <asp:Button OnClick="btnbackupload_Click" ID="Buttonback" Text="Back" ForeColor="Blue" Width="10%"
                    runat="server" CssClass="buttonClass moveRight" CausesValidation="false" />
            <asp:Button OnClick="btnupload_Click" ID="btnDashboard" Text="Dashboard" ForeColor="Blue" Width="10%"
                    runat="server" CssClass="buttonClass moverigRight" CausesValidation="false" />

            
        </div>
     <div id="gridDiv">
        
    <br />
       
      <%--  <asp:CheckBox ID="ShowAllCheckBox" runat="server" Text=" All" Width="20%" OnCheckedChanged="SearchButton_Click"/>
         <asp:CheckBox ID="ShowActive" runat="server" Text=" Active" Width="20%" OnCheckedChanged="SearchButton_Click"/>
         <asp:CheckBox ID="ShowInActive" runat="server" Text=" InActive" Width="20%" OnCheckedChanged="SearchButton_Click"/>
        <asp:CheckBox ID="ShowHoldActive" runat="server" Text=" Hold" Width="20%" OnCheckedChanged="SearchButton_Click"/>--%>

         <br />
          
       <span id="testdd" class="messagecss" style="margin-top:12px;"> <label>REASON:</label>   <select id="slTypeOfCredit" runat="server" name="slTypeOfCredit">
                        <option value="0" selected="selected">-ALL-</option>
                        <option value="FAULTY - NOT REG">FAULTY - NOT REG</option>
                          <option value="FAULTY - PRINT QUALITY">FAULTY - PRINT QUALITY</option>
                          <option value="FAULTY - DAMAGED">FAULTY - DAMAGED</option>
                        <option value="WRONG GOODS - DELTONE FAULT">WRONG GOODS - DELTONE FAULT</option>
                        <option value="WRONG GOODS - SUPPLIER FAULT">WRONG GOODS - SUPPLIER FAULT</option>
                         <option value="WRONG GOODS - CUSTOMER FAULT">WRONG GOODS - CUSTOMER FAULT</option>
                         <option value="SUPPLIER DOES NOT HAVE STOCK">SUPPLIER DOES NOT HAVE STOCK</option>
                        <option value="PRICE REDUCTION">PRICE REDUCTION</option>
                        <option value="CHANGED PRINTER">CHANGED PRINTER</option>
                        <option value="DID NOT ORDER">DID NOT ORDER</option>
                        <option value="LOST IN TRANSIT">LOST IN TRANSIT</option>
                        <option value="LIQUIDATION">LIQUIDATION  </option>
                        <option value="UNECONOMICAL">UNECONOMICAL</option>
                       <option value="NO AUTH">NO AUTH</option>
                    </select> 

       </span>
        <asp:Label ID="messagelable" runat="server" CssClass="messagecss"></asp:Label>
           <br /><br />
          <br />
      <input type="button" id="prBtn"style=" color:blue;cursor:pointer;" value="Print" onclick="PrintPage();" />
    <asp:TextBox ID="searchTextBox" runat="server" Width="25%"></asp:TextBox>
         Start Date:
                    <input id="StartDateSTxt" type="text" name="StartDateTxt" size="18" runat="server" />
               
         End Date:
                    <input id="EndDateSTxt" type="text" name="EndDateTxt" size="18" runat="server" />
              

        <asp:Button OnClick="SearchButton_Click" ID="btnsearch" Text="Search" ForeColor="Blue" Width="20%" runat="server" CausesValidation="false" />
     <asp:Button OnClientClick="reloadPage();" ID="btnreset" Text="Reset" ForeColor="Blue" Width="10%" runat="server" CausesValidation="false" />
    
     <br />
            <br />
         <table width="100%" id="pnlGridView" runat="server" align="center" class="ContentTable">
    <tr>
        <td colspan="2" align="center">
            <h1>
                Search Customer By Credited Product</h1>
        </td>
    </tr>
    <tr>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2">
    <asp:GridView ID="GridViewProduct" runat="server" AllowPaging="true" PageSize="50"   OnSorting="gridView_Sorting" AllowSorting="true" 
        OnPageIndexChanging="GridView1_PageIndexChanging"    alternatingrowstyle-backcolor="Linen" headerstyle-backcolor="SkyBlue"
         AutoGenerateColumns="False"  EmptyDataText="No records" Width="100%" ShowFooter="true">
    <Columns>
           
        <asp:TemplateField HeaderText="COMPANY" ItemStyle-Width="120px" SortExpression="CompanyName">
            <ItemTemplate>
                <asp:Label ID="CompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
            </ItemTemplate>
           
        </asp:TemplateField>

        <asp:TemplateField HeaderText="CONTACT" SortExpression="contactName">
              <ItemTemplate>
                <asp:Label ID="contactName" runat="server" Text='<%# Eval("contactName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="TELEPHONE" SortExpression="tlenumber">
              <ItemTemplate>
                <asp:Label ID="tlenumber" runat="server" Text='<%# Eval("tlenumber") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="REASON" SortExpression="tlenumber">
              <ItemTemplate>
                <asp:Label ID="resontt" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="CREDIT #" SortExpression="OrderID">
              <ItemTemplate>
                <asp:Label ID="CreditID" runat="server" Text='<%# Eval("CreditID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField HeaderText="ORDER #" SortExpression="OrderID">
              <ItemTemplate>
                <asp:Label ID="OrderID" runat="server" Text='<%# Eval("OrderID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="CREDITED DATE" SortExpression="CreatedDateTime" >
              <ItemTemplate>
                <asp:Label ID="CreatedDateTime" runat="server" Text='<%# Convert.ToDateTime(Eval("CreatedDateTime")).ToString("d") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField HeaderText="PRODUCT CODE" SortExpression="SupplierItemCode">
              <ItemTemplate>
                <asp:Label ID="SupplierItemCode" runat="server" Text='<%# Eval("SupplierItemCode") %>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>

          <asp:TemplateField HeaderText="DESCRIPTION" SortExpression="Description">
              <ItemTemplate>
                <asp:Label ID="Description" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>

          <asp:TemplateField HeaderText="QUANTITY" SortExpression="itemQty">
              <ItemTemplate>
                <asp:Label ID="itemQty" runat="server" Text='<%# Eval("itemQty") %>'></asp:Label>
            </ItemTemplate>
              </asp:TemplateField>
              
          <asp:TemplateField HeaderText="Unit Amount" SortExpression="UnitAmount">
              <ItemTemplate>
                <asp:Label ID="UnitAmount" runat="server" Text='<%# Eval("UnitAmount") %>'></asp:Label>
            </ItemTemplate>
              </asp:TemplateField>
              
          <asp:TemplateField HeaderText="Created By" SortExpression="CreatedBy">
              <ItemTemplate>
                <asp:Label ID="CreatedBy" runat="server" Text='<%# Eval("CreatedBy") %>'></asp:Label>
            </ItemTemplate>
              </asp:TemplateField>
              
          <asp:TemplateField HeaderText="Account Owner" SortExpression="AccountOwner">
              <ItemTemplate>
                <asp:Label ID="AccountOwner" runat="server" Text='<%# Eval("AccountOwner") %>'></asp:Label>
            </ItemTemplate>

        </asp:TemplateField>
       
    </Columns>
</asp:GridView>
        <br />
   </td>
    </tr>

        </table> 
           </div>
    

    <%--  <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">CUSTOMER SEARCH BY ORDERED PRODUCT</td>
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
<table cellpadding="0" cellspacing="0" border="0" class="display" id="protable" style="cursor:pointer">
	<thead>
		<tr>
            
            <th align="left">COMPANY</th>
			<th align="left">CONCTACT PERSON</th>
            <th align="left">TELEPHONE</th>
               <th align="left">ORDER ID</th>
			<th align="left">ORDERED DATE</th>
             <th align="left">PRODUCT CODE</th>
             <th align="left">DESCRIPTION</th>
            <th align="left">QUANTITY</th>
		</tr>
        
	</thead>
     <tr>
            
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
      </table>--%>

     <script type="text/javascript">

         $(document).ready(function () {

             var dateNow = new Date();
             $("#ContentPlaceHolder1_StartDateSTxt").datepicker({ dateFormat: 'dd-mm-yy' });
             $("#ContentPlaceHolder1_EndDateSTxt").datepicker({ dateFormat: 'dd-mm-yy' });

         });
         </script>
  </asp:Content>