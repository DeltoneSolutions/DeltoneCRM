<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderFileUpload.aspx.cs" Title="Order File Upload" Inherits="DeltoneCRM.OrderFileUpload" MasterPageFile="~/NoNav.Master" %>

<asp:Content ID="MainHeader" ContentPlaceHolderID="head" runat="server">
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <style>

         body {
            margin: 40px 10px;
            padding: 0;
            font-family: "Lucida Grande",Helvetica,Arial,Verdana,sans-serif;
            font-size: 22px;
        }
  
   /*#drop_zone 
   { 
       margin:10px 0;
       width:40%; 
       min-height:150px; 
       text-align:center;
       text-transform:uppercase;
       font-weight:bold;
       border:8px dashed #898;
                height: 160px;
            }*/

   #drop_zone {
            max-width: 40%;
            margin: 0 auto;
            width:40%; 
       min-height:150px; 
       text-align:center;
       text-transform:uppercase;
       font-weight:bold;
       font-size:30px;
       border:8px dashed #3E3E42;
                height: 160px;


        }

   #spanCon{
       display: block;
  vertical-align: middle;
  line-height: normal;
  margin-top:50px;
  opacity: 0.3;
   }
   
   #gridDiv {
            max-width: 70%;
            margin: 0 auto;
            width:70%; 
       min-height:150px; 
       text-align:center;
       font-weight:bold;
      
        }

   .alink {
    display: block;
    height: 20px;
    width: 90px;
    border: 1px solid #000;
}
 .buttonClass
{
    padding: 2px 20px;
    text-decoration: none;
    border: solid 1px #3E3E42;
}
.buttonClass:hover
{
    border: solid 1px Black;
    background-color: #ffffff;
}
.moveRight{
    float:right;
    margin-right:300px;
    color:blue;
    text-align:center;
       font-weight:bold;
}.messagecss{
    margin-left:300px;
    color:green;
    text-align:center;
       font-weight:bold;
}
  .buttonClassDash
{
    height:40px;
    cursor:pointer;
}
  </style>
    <title></title>
    <script>
        var orderIdQuery = '<%= str_ORDERID %>';
        var files;
        var fileNameChange;
        function handleDragOver(event) {
            event.stopPropagation();
            event.preventDefault();
            var dropZone = document.getElementById('spanCon');
            dropZone.innerHTML = "Drop now";
        }

        function handleDnDFileSelect(event) {
            event.stopPropagation();
            event.preventDefault();

            /* Read the list of all the selected files. */
            files = event.dataTransfer.files;

            /* Consolidate the output element. */
            var form = document.getElementById('form1');
            var data = new FormData(form);
           
            for (var i = 0; i < files.length; i++) {
                s = prompt('Enter File Name','');
                fileNameChange = files[i].name;
                if (s != "")
                    fileNameChange = s;
                data.append(files[i].name, files[i]);
                data.append("namechange", fileNameChange);
            }
            if (s != null) {
                var xhr = new XMLHttpRequest();
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4 && xhr.status == 200 && xhr.responseText) {
                        alert("Upload done!");
                        window.location.reload();
                    } else {
                        //alert("upload failed!");
                    }
                };
                xhr.open('POST', "OrderFileUpload.aspx?ordID=" + orderIdQuery);
                // xhr.setRequestHeader("Content-type", "multipart/form-data");
                xhr.send(data);
            }
            else {
                var dropZone = document.getElementById('spanCon');
                dropZone.innerHTML = "Drop files here";

            }

        }

      
  </script>
</asp:Content>

    <asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <%-- <input name="media[]" type="file" multiple/>--%>
         <br />
        <br />
        <div id="drop_zone">
          <span id="spanCon">Drop files here </span>  </div>

        <br />
        <br /> 
       
         <asp:Button OnClick="btnupload_Click" Visible="false" ID="btnDashboard" Text="Dashboard" ForeColor="Blue" Width="10%" 
              runat="server" CssClass="buttonClass moveRight buttonClassDash" CausesValidation="false" />  
        <br />
        <br />
        <asp:Label ID="messagelable" runat="server" CssClass="messagecss"></asp:Label>
           <br /><br />
        <div id="gridDiv">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnDataBound="GridView1_DataBound" GridLines="Horizontal"  EmptyDataText="No files uploaded" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Text" ItemStyle-Height="28px"   HeaderStyle-ForeColor="#3399ff" />
                           <asp:TemplateField>
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkDelete" ForeColor="Red" Text="Delete"  CssClass="buttonClass" Font-Underline="false" OnClientClick="if (!confirm('Are you sure you want delete?')) return false;"   CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DeleteFile" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" Text="Download" Font-Underline="false" CssClass="buttonClass" CommandArgument='<%# Eval("Value") %>' runat="server" OnClick="DownloadFile"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkView" Text="View" Font-Underline="false" CssClass="buttonClass" CommandArgument='<%# Eval("Value") %>' runat="server" 
                                    OnClick="OpenTab"/>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
           
            </div>
        <div > 
         <asp:Button ID="completeButton" runat="server" Text="All Files Uploaded"  OnClick="changeOrder_click" CausesValidation="false" CssClass="buttonClass moveRight"/> 
            <asp:Button ID="ButtonApproveOrderPurchase" runat="server" Text="Authorize Purchase and Create A Bill"  Visible="false" CausesValidation="false"
                OnClick="ButtonApproveOrderPurchase_click" CssClass="buttonClass moveRight"/> 
          <%--   <asp:Button ID="ButtonCreateBill" runat="server" Text="Create A Bill"  Visible="false"
                OnClick="ButtonCreateBill_click" CssClass="buttonClass moveRight"/> --%>

        </div>
    <script>
        if (window.File && window.FileList && window.FileReader) {
            /************************************ 
             * All the File APIs are supported. * 
             * Entire code goes here.           *
             ************************************/


            /* Setup the Drag-n-Drop listeners. */
            var dropZone = document.getElementById('drop_zone');
            dropZone.addEventListener('dragover', handleDragOver, false);
            dropZone.addEventListener('drop', handleDnDFileSelect, false);

        }
        else {
            alert('Sorry! this browser does not support HTML5 File APIs.');
        }
  </script>


</asp:Content>