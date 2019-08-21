<%@ Page Title="" Language="C#" MasterPageFile="~/NoNav.Master" AutoEventWireup="true" CodeBehind="sel_ProductReport.aspx.cs" Inherits="DeltoneCRM.Reports.sel_ProductReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" />
    <script   type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.5/jquery.min.js">
    </script> 
    <script   type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js">
    </script>  
    
    <script type="text/javascript">
        $(function () {         

          
            $('#<%=startdate.ClientID%>').datepicker();
            $('#<%=enddate.ClientID%>').datepicker();

           
            $("#<%=prodcode.ClientID%>").autocomplete({
                source: function (request, response) {
                    var param = { keyword: $('#<%=prodcode.ClientID%>').val() };
                    $.ajax({
                        url: "sel_ProductReport.aspx/GetSupplierItemCodes",
                        data:"{'namePrefix':'" + $('#<%=prodcode.ClientID%>').val() + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data)
                        {
                            response(data.d);
                        },
                        error: function (response) {
                            alert("Error" + res.responseText);
                        }
                    });
                },
                select: function (event, ui) {
                    if (ui.item) {
                        //GetCustomerDetails(ui.item.value);
                        $('#<%=prodcode.ClientID%>').val(ui.item.value)
                    }
                },
                minLength: 2
            });

        });

    </script> 
    
    
    
    <style type="text/css">
        .auto-style1 {
            width: 300px;
        }
        .auto-style2 {
            height: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="980" border="0" align="center" cellpadding="0" cellspacing="0" class="MainTable">

        <tr>
          <td height="25px">&nbsp;</td>
        </tr>
        <tr>
          <td height="25px" class="section_headings">PRODUCT SOLD REPORT</td>
        </tr>

        <tr>
            <td  class="section_headings">&nbsp;</td>
        </tr>

        <tr>
          <td class="white-box-outline">
              &nbsp;
              <table align="center" cellpadding="0" cellspacing="0" class="auto-style1">
                  <tr>
                      <td>PRODUCT CODE</td>
                      <td>&nbsp;</td>
                      <td>
                          <input id="prodcode" type="text" name="prodcode" runat="server" /></td>
                  </tr>

                  <tr>
                      <td>&nbsp;</td>
                  </tr>

                  <tr>
                      <td class="auto-style2">START DATE</td>
                      <td class="auto-style2"></td>
                      <td class="auto-style2">
                          <input id="startdate" name="startdate" type="text" runat="server" />   
                      </td>
                          
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                  </tr>
                  <tr>
                      <td>END DATE</td>
                      <td>&nbsp;</td>
                      <td>
                          <input type="text" id="enddate" name="enddate"  runat="server" />
                      </td>    
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                  </tr>
                  <tr>
                      <td>&nbsp;</td>
                      <td>&nbsp;</td>
                      <td>
                          <asp:Button ID="Button1" runat="server" Text="GENERATE REPORT" OnClick="GR_Click" />
                      </td>
                  </tr>
              </table>
            </td>
        </tr>

        <tr>
          <td height="25px">&nbsp;</td>

            <td><asp:GridView ID="gvProductList" runat="server" AutoGenerateColumns="False">
                <Columns>
                     <asp:BoundField DataField="XeroInvoiceNumber" 
                                HeaderText="INVOICE NO" 
                                 />
                    <asp:BoundField DataField="OrderedDateTime" 
                                HeaderText="ORDERED DATE TIME" 
                                 />
                    <asp:BoundField DataField="CompanyName" 
                                HeaderText="COMPANY NAME" 
                                 />
                    <asp:BoundField DataField="ItemCode" 
                                HeaderText="ITEM CODE" 
                                 />

                    <asp:BoundField DataField="SupplierItemCode" 
                                HeaderText="PRODUCT CODE" 
                                 />

                    <asp:BoundField DataField="OEMCode" 
                                HeaderText="OEM CODE" 
                                 />

                    <asp:BoundField DataField="OEMCode" 
                                HeaderText="OEM CODE" 
                                 />

                    <asp:BoundField DataField="Description" 
                                HeaderText="DESCRIPTION" 
                                 />

                    <asp:BoundField DataField="ItemPrice" 
                                HeaderText="ITEM PRICE" 
                                 />

                     <asp:BoundField DataField="InvoiceTotal" 
                                HeaderText="INVOICE TOTAL" 
                                 />
                </Columns>


            </asp:GridView></td>
        </tr>
      </table>
</asp:Content>
