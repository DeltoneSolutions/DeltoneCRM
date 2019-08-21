<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContact.aspx.cs" Inherits="DeltoneCRM.AddContact" MasterPageFile="~/Site1.Master" %>
<asp:Content ID="MainHeader" ContentPlaceHolderID="head" runat="server">
    <title>Add Conpany Contact</title>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <script src="js/jquery-1.9.1.js"></script>
	<script src="js/jquery-ui-1.10.3.custom.js"></script>

    <script   type="text/javascript" >
        var dialog;

        function DisplayResult() {
            dialog.dialog("open");
        }

        $(document).ready(function () {



            dialog = $("#dialog-form").dialog({
                autoOpen: false,
                width: 800,
                modal: true,

                close: function () {

                    $('.blackout').css("display", "none");
                    dialog.dialog("close");
                }
            });


        });

    </script>


    <style type="text/css">
        body,td,th {
	        font-family: "PT Sans", sans-serif;
        }
        body {
	        background-color: #F2F2F2;
	        margin-top: 50px;
        }

        .Tbl 
        {
            border:thin;

        }

        .Error 
        {
            color:red;

        }
  </style>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div>

        <div id="divErrorDisplay" runat="server" visible="false">
            <asp:Label ID="lblError" CssClass="Error" runat="server"></asp:Label>
        </div>

        <div id="divAddContacts"  runat="server" visible="true">
    
   <table  id="tblAddContacts" runat="server"  >
         
           <tr>
                 <td>&nbsp;</td>
           </tr>

            <tr >
                 <td><b>Company Name</b></td>
                
             </tr>

             <tr>
                  <td><asp:TextBox ID="txtContactName" runat="server" ></asp:TextBox></td>
             </tr>
             
             <tr>
                 <td>&nbsp;</td>
             </tr>

             <tr>
             <td><b>Primary Person:</b><asp:CheckBox ID="chkPrimary" runat="server" Checked="true" /></td>
             </tr>

             <tr>
                 <td>
                   <table id="tbl_contact">
                       <tr>
                           <td>First Name</td> <td>Last Name</td> <td>Email</td>
                           
                       </tr>
                       <tr>
                           <td><asp:TextBox ID="txtFistName" runat="server"></asp:TextBox></td>
                           <td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
                           <td><asp:TextBox ID="txtContactEmail" runat="server"></asp:TextBox></td>
                       </tr>


                   </table>
                 </td>
             </tr>

           
            <tr>

               <td>&nbsp;</td>
            </tr>

              <tr>
                <td><b>Telephone</b></td>
             </tr>

             <!--Table Telephone-->
              <tr>
                  <td>
                      <table id="tblTelephone" >
                          <tr>

                              <td>Country</td><td>Area</td><td>Number</td>
                          </tr>
                          <tr>

                              <td><asp:TextBox ID="txtTeleCountryCode" runat="server" Width="40"></asp:TextBox></td>
                              <td><asp:TextBox ID="TextTeleAreaCode" runat="server" Width="40"></asp:TextBox></td>
                              <td><asp:TextBox ID="TextTeleNumber" runat="server"></asp:TextBox></td>
                          </tr>


                      </table>

                  </td>


              </tr>
              <!--End Table Telephone-->

             <tr>
               <td>&nbsp;</td>
             </tr>


             <tr>
                <td><b>Mobile</b></td>
             </tr>

       <!--Talbe Mobile-->
       <tr>
           <td>
               <table id="tblMobile">
                   <tr>
                       <td>Country</td><td>Area</td><td>Number</td>
                   </tr>
                   <tr>

                              <td><asp:TextBox ID="txtMobileCountryCode" runat="server" Width="40"></asp:TextBox></td>
                              <td><asp:TextBox ID="txtMobileAreaCode" runat="server" Width="40"></asp:TextBox></td>
                              <td><asp:TextBox ID="txtMobileNumber" runat="server"></asp:TextBox></td>
                   </tr>

               </table>

           </td>


       </tr>
       <!--End Talbe Mobile-->

       <tr>
           <td><b>Fax</b></td>

       </tr>

       <!--Table Fax-->
        <tr>
           <td>
               <table id="TblFax">
                   <tr>
                       <td>Country</td><td>Area</td><td>Number</td>
                   </tr>
                   <tr>

                              <td><asp:TextBox ID="txtFaxCountryCode" runat="server" Width="40"></asp:TextBox></td>
                              <td><asp:TextBox ID="txtFaxAreaCode" runat="server" Width="40"></asp:TextBox></td>
                              <td><asp:TextBox ID="txtFaxNumber" runat="server"></asp:TextBox></td>
                   </tr>

               </table>

           </td>


       </tr>

       <!--End Table fax-->

              <tr>
                 <td>&nbsp;</td>
              </tr>                 
     </table>


    <!--Billin Address Panel-->
        <asp:Panel ID="pnlBillingAddress" runat="server">
            <asp:Table ID="tblBillingAddress" runat="server">

                 <asp:TableRow>
                    
                    <asp:TableCell ColumnSpan="2">&nbsp;</asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    
                    <asp:TableCell ColumnSpan="2"><b>Postal Address</b></asp:TableCell>
                </asp:TableRow>


                 <asp:TableRow>
                     <asp:TableCell >Street Address/PO BOX</asp:TableCell>
                     <asp:TableCell><asp:TextBox ID="txtPostalStreetAddress" runat="server"></asp:TextBox></asp:TableCell>
                 </asp:TableRow>

                 <asp:TableRow>
                     <asp:TableCell >Town/City</asp:TableCell>
                     <asp:TableCell><asp:TextBox ID="txtPostalTownCity" runat="server"></asp:TextBox></asp:TableCell>
                 </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>State</asp:TableCell>
                    <asp:TableCell><asp:DropDownList ID="ddPostalState" runat="server">
                                    <asp:ListItem Selected="True">select</asp:ListItem>
                                    <asp:ListItem>VIC</asp:ListItem>
                                    <asp:ListItem>TAS</asp:ListItem>
                                    <asp:ListItem>NT</asp:ListItem>
                                    <asp:ListItem>ACT</asp:ListItem>
                                    <asp:ListItem>SA</asp:ListItem>
                                    <asp:ListItem>NSW</asp:ListItem>
                                    <asp:ListItem>QLD</asp:ListItem>
                                   </asp:DropDownList>

                    </asp:TableCell>
                </asp:TableRow>

                 <asp:TableRow>
                     <asp:TableCell >Postal/Zip Code</asp:TableCell>
                     <asp:TableCell><asp:TextBox ID="txtPostalZipCode" runat="server"></asp:TextBox></asp:TableCell>
                 </asp:TableRow>


                <asp:TableRow>

                    <asp:TableCell>Country</asp:TableCell>
                    <asp:TableCell><asp:TextBox ID="txtPostalCountry" runat="server"></asp:TextBox></asp:TableCell>
                </asp:TableRow>

            </asp:Table>
        </asp:Panel>
    <!--End Billing Address Panel-->

    

    <!--Shipping Address Panel-->
    <asp:Panel ID="pnlShippingAddress" runat="server">
        <asp:Table ID="tblShippingAddress" runat="server">

            <asp:TableRow><asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><b>Physical Address</b>&nbsp;&nbsp;<i>Same As Above</i>
                    <!--
                <asp:LinkButton ID="lbSameAsAbove" runat="server" Text="[Same as Postal Address]" OnClick="lbSameAsAbove_Clicked"></asp:LinkButton>
                    -->

                    <asp:CheckBox ID="chk" 
                    runat="server" 
                    AutoPostBack="true"
                    OnCheckedChanged="CtrlChanged"  Checked="false"/>
                          
                </asp:TableCell>

            </asp:TableRow>


              <asp:TableRow >
                  <asp:TableCell>
                       <asp:ScriptManager ID="ScriptManager1" runat="server" 
  EnablePageMethods = "true">
       </asp:ScriptManager>
                      <asp:UpdatePanel ID="upShippingAddress" runat="server"   UpdateMode="Conditional">
                      

                          <ContentTemplate >

                              <asp:Panel ID="pnlShippingAddres" runat="server"  Enabled="true" >
                                  <asp:Table  runat="server"  ID="tblShip">

                                
                                       <asp:TableRow>
                                           <asp:TableCell>Street Address/PO BOX</asp:TableCell>
                                           <asp:TableCell><asp:TextBox ID="txtPhysicalStreetAddress"  runat="server"  ></asp:TextBox></asp:TableCell>
                                       </asp:TableRow>

                                      <asp:TableRow>
                                           <asp:TableCell>Town/City</asp:TableCell>
                                           <asp:TableCell><asp:TextBox ID="txtPhysicalTownCity"  runat="server"  ></asp:TextBox></asp:TableCell>
                                       </asp:TableRow>



                                          <asp:TableRow>
                                                <asp:TableCell>State</asp:TableCell>
                                                <asp:TableCell><asp:DropDownList ID="ddPhysicalState" runat="server">
                                                                <asp:ListItem Selected="True">select</asp:ListItem>
                                                                <asp:ListItem>VIC</asp:ListItem>
                                                                <asp:ListItem>TAS</asp:ListItem>
                                                                <asp:ListItem>NT</asp:ListItem>
                                                                <asp:ListItem>ACT</asp:ListItem>
                                                                <asp:ListItem>SA</asp:ListItem>
                                                                <asp:ListItem>NSW</asp:ListItem>
                                                                <asp:ListItem>QLD</asp:ListItem>
                                                               </asp:DropDownList>

                                                </asp:TableCell>
                                         </asp:TableRow>

                                         <asp:TableRow>

                                                <asp:TableCell>Postal/Zip Code</asp:TableCell>
                                                <asp:TableCell><asp:TextBox ID="txtPhysicalPostalZipCode" runat="server"></asp:TextBox></asp:TableCell>
                                        </asp:TableRow>

                                         <asp:TableRow>

                                                <asp:TableCell>Country</asp:TableCell>
                                                <asp:TableCell><asp:TextBox ID="txtPhysicalCountry" runat="server"></asp:TextBox></asp:TableCell>
                                        </asp:TableRow>


                                  </asp:Table>
                                  </asp:Panel>
                          </ContentTemplate>
                      </asp:UpdatePanel>  
                  </asp:TableCell>
              </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell><asp:Button  ID="btnContactSubmit" Text="Save" runat="server" OnClick="btnContactSubmit_Click" />&nbsp;
                <asp:Button  ID="btnContactCancel" Text="Cancel" runat="server" OnClick="btnContactCancel_Click" /></asp:TableCell>
            </asp:TableRow>




        </asp:Table>
    </asp:Panel>
    <!--End Shipping Address panel-->


 </div>

<!--End Add Contacts Panel-->
 
    </div>
     
         <!--Contact Creation Message Dialog-->
         <div id="dialog-form" title="Contact Created"  >
             <span id="spContact"  runat="server"></span><br />
             <a href="DashBoard.aspx"  runat="server">Return to DashBoard</a>
             <a href="ConpanyInfo.aspx?CompanyID=17472" runat="server">Return to CompanyInfo</a>
         </div>
        <!--End Contact creation Message Dialog-->



    </asp:Content>

