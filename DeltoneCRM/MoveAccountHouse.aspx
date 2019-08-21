<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MoveAccountHouse.aspx.cs" Inherits="DeltoneCRM.MoveAccountHouse"
     MasterPageFile="~/NoNav.Master" enableEventValidation="false"  %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="js/jquery-1.11.1.min.js"></script>
    <script src="js/jquery-ui-1.10.3.custom.js"></script>

    <title>Company List</title>
    
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
    </style>

    <script type="text/javascript">

        function validate() {
            alert($('#<%= GridView1.ClientID%> input[name*="chkSelect":checked').length);
            
            if ($('#<%= GridView1.ClientID%> input[name*="chkSelect":checked').length > 0) {
                return true;
            }
            else {
                alert('Please select minimum a record.  Thanks');
                return false;
            }
            
        }
    </script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  
    <div id="gridDiv">
    <br />
       
      <%--  <asp:CheckBox ID="ShowAllCheckBox" runat="server" Text=" All" Width="20%" OnCheckedChanged="SearchButton_Click"/>
         <asp:CheckBox ID="ShowActive" runat="server" Text=" Active" Width="20%" OnCheckedChanged="SearchButton_Click"/>
         <asp:CheckBox ID="ShowInActive" runat="server" Text=" InActive" Width="20%" OnCheckedChanged="SearchButton_Click"/>
        <asp:CheckBox ID="ShowHoldActive" runat="server" Text=" Hold" Width="20%" OnCheckedChanged="SearchButton_Click"/>--%>

         <br />
        <asp:Label ID="messagelable" runat="server" CssClass="messagecss"></asp:Label>
           <br /><br />
          <br />
       <asp:Button ID="btnsave" runat="server" OnClick="SaveButton_Click" Text="MOVE TO HOUSE" Width="20%" ForeColor="Blue" CausesValidation="false" />
   
     <asp:Button OnClick="btnupload_Click" ID="btnDashboard" Text="Dashboard" ForeColor="Blue" Width="10%" runat="server" CausesValidation="false" />
    
     <br />
            <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="100"  OnPageIndexChanging="GridView1_PageIndexChanging"
         AutoGenerateColumns="False"  EmptyDataText="No records" Width="100%">
    <Columns>
            <asp:TemplateField>
           
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ID" SortExpression="CompanyID">
            <ItemTemplate>
                <asp:Label ID="LabelId" runat="server" Text='<%# Eval("CompanyID") %>'></asp:Label>
            </ItemTemplate>
           
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName">
              <ItemTemplate>
                <asp:Label ID="LabelName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="Last Order Date" SortExpression="CompanyName">
              <ItemTemplate>
                <asp:Label ID="LastOrderDate" runat="server" Text='<%# Eval("LastOrderDate") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
       
    </Columns>
</asp:GridView>
        <br />
       
          
           </div>
    </asp:Content>

