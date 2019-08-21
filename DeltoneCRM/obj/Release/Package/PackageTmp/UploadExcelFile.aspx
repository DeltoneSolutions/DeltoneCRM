<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadExcelFile.aspx.cs" Inherits="DeltoneCRM.UploadExcelFile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style type="text/css">
          #img-loadwaiting {
 
  position: relative;
  background-color: gray; /* for demonstration */
}
.ajax-loader {
position: absolute;
  left: 0px;
  top: 0px;
  width: 100%;
  height: 100%;
  z-index: 9999;
  background: url('Images/loadingimage1.gif') 50% 50% no-repeat rgb(249,249,249);
  background-repeat : no-repeat;
    background-position : center;
}
#loadingiamge {
    position: absolute; width: 100%; height: 100%; background: url('Images/loadingimage1.gif') no-repeat center center;
}

    </style>
     <link href="../css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet" />
    <script src="http://code.jquery.com/jquery-latest.js"></script>
    <script type="text/javascript">


      function showLoadingImage() {
          $('#loadingiamge').show();
        }

        function hideLoadingImage() {
            $('#loadingiamge').hide();
        }

        function testslash() {

            var testdd = "a sdsf aadb/c ss";
            if (testdd.indexOf("/") > -1)
                alert("string contains forwar");

            var tret = "ac aa\\c ss";
            if (tret.indexOf('/\\/') > -1)
                alert("string contains back");
            alert("avv" + tret.indexOf("\\"));

            re1 = new RegExp("\\\\", "")
            strHasBackslashes = re1.test(tret);
            alert("avccc" + strHasBackslashes);
            strHasBackslashes = (/\\/).test(tret);
            alert("aaar" + strHasBackslashes);

        }

        var dictState = []; // create an empty array
        function createDaysByState() {


            dictState.push({
                key: "VIC",
                value: "2"
            });
            dictState.push({
                key: "QLD",
                value: "2"
            });
            dictState.push({
                key: "NSW",
                value: "2"
            });
            dictState.push({
                key: "ACT",
                value: "2"
            });
            dictState.push({
                key: "TAS",
                value: "5"
            });
            dictState.push({
                key: "WA",
                value: "5"
            });

            dictState.push({
                key: "NT",
                value: "5"
            });
        }

        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
         <h4>Select Dynamic Supplier file to upload:</h4> XlSX

       <asp:FileUpload id="FileUpload1"                 
           runat="server">
       </asp:FileUpload>

       <br /><br />

       <asp:Button id="UploadButton" 
           Text="Upload file"
           OnClick="UploadButton_Click"
           runat="server">
       </asp:Button>    
<br /><br />



          <br /><br />

       <asp:Button id="Button2" 
           Text="Contact Update Xero"
           OnClick="UploadButtonXero_Click"
           runat="server">
       </asp:Button>    
<br /><br />

          <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>


           <div>
    
         <h4>Select Ausjet Supplier file to upload:</h4> CSV

       <asp:FileUpload id="FileUpload2"                 
           runat="server">
       </asp:FileUpload>

       <br /><br />

                 <h4>Select Ausjet Supplier file to upload:</h4> XlS

       <asp:FileUpload id="FileUpload3"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />

       <asp:Button id="Button1" 
           Text="Upload file"
           OnClick="UploadAusjetButton_Click"
           runat="server">
       </asp:Button>    


               
                 <h4>Select TOD File to upload:</h4> XlS

       <asp:FileUpload id="FileUpload4"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />


               <asp:Button ID="buttonNewPriceFormat" Text="New Price TOD Book" runat="server" OnClick="UploadNewClick"/>

<br /><br />

                      <h4>Select Microjet File to upload:</h4> XlS

       <asp:FileUpload id="FileUpload5"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />


               <asp:Button ID="button4" Text="New Price Book" runat="server" OnClick="UploadNewMicroClick"/>

<br /><br />


 <h4>Select House Promo File to upload:</h4> XlS

       <asp:FileUpload id="FileUpload6"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />


               <asp:Button ID="button5" Text="Promo File" runat="server" OnClick="uploadPromo"/>

<br /><br />



<h4>Select Q-Image File to upload:</h4> XlS

       <asp:FileUpload id="FileUpload7"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />


               <asp:Button ID="button6" Text="QIMage" runat="server" OnClick="uploadQimage"/>

<br /><br />


               <h4>Select RTS File to upload:</h4> XlS

       <asp:FileUpload id="FileUpload8"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />


               <asp:Button ID="button7" Text="RTS Upload" runat="server" OnClick="uploadRts"/>



               <br /><br />


               <h4>Select CartridgeOne File to upload:</h4> XlS

       <asp:FileUpload id="FileUpload9"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />


               <asp:Button ID="button8" Text="CartridgeOne Upload" runat="server" OnClick="UploadCartidges"/>

               <h4>Select WareHouse File to upload:</h4> XlS

       <asp:FileUpload id="FileUpload10"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />


               <asp:Button ID="button9" Text="WareHouse Item Upload" runat="server" OnClick="UploadWareINHouse"/>

<br /><br />



  <h4>Select UploadLead File to upload:</h4> XlS

       <asp:FileUpload id="FileUpload11"                 
           runat="server">
       </asp:FileUpload>

               <br /><br />


               <asp:Button ID="button10" Text="Lead Company And Item" runat="server" OnClick="CreateUploadLead"/>

               <br /><br />
          <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
    </div>



          <asp:Button id="Button3" 
           Text="Test Email"
           OnClick="EmailSendFiles"
           runat="server">
       </asp:Button> 
        <div id="loadingiamge">

        </div>
        <input type="button" onclick="testslash()" value="Show Me" />
    </form>
</body>
    

    
</html>
