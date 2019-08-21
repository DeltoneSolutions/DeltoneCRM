<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConpanyInfo.aspx.cs" Inherits="DeltoneCRM.ConpanyInfo" MasterPageFile="~/Site1.Master" %>


<asp:Content ID="HeaderSection" ContentPlaceHolderID="head" runat="server">
    <title>CompanyInfo</title>
    <link href="css/smoothness/jquery-ui-1.10.3.custom.css" rel="stylesheet"/>
    <link href="css/jquery.dataTables_new.css" rel="stylesheet" />
    <script src="//code.jquery.com/jquery-1.11.1.min.js"></script>
	
    <script src="//cdn.datatables.net/1.10.5/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="http://localhost:65085/cdn.datatables.net/1.10.5/css/jquery.dataTables.css"/>

    <!--Jquery UI References-->
	<script src="js/jquery-ui-1.10.3.custom.js"></script>
    <style type="text/css">

         td {
	           font-family: 'PT Sans Narrow', sans-serif;
	           font-size: 12px;
	           color:#333;
            }
    </style>

    <script type="text/javascript" >

        function LoadView(ContactID) {
            //alert(ContactID);
            dialog.dialog("open");
            PopulateViewContact(ContactID);
        }

        //Function to create order from JTable link
        function CreateOrder(ContactID) {
            window.open("order.aspx?cid=" + ContactID);
        }

        //This function populate the Contact Details given by CompanyID
        function PopulateViewContact(contactID)
        {
            $.ajax({
                
                url: '/process/ProcessContactView.aspx',
                data: {
                    contactid: contactID,
                },
                success: function (result) {
                   
                    fillContact(result);
                   
                },
                error: function (request, status, error) {
                    alert(request.responseText);
                }
            });
        }

        function fillContact(data)
        {
            //spcontact, spfirstname, splastname, spTele, spMobile, spfax, spPostalAddress, spPhysicalAddress
            //alert(data);
            var Result = data.split("|");
            $('#spcontact').html(Result[0]);
            $('#spfirstname').html(Result[1]);
            $('#splastname').html(Result[2]);
            $('#spTele').html(Result[3]);
            $('#spMobile').html(Result[4]);
            $('#spfax').html(Result[5]);
            $('#spEmail').html(Result[6]);
            $('#spPostalAddress').html(Result[7]);
            $('#spPhysicalAddress').html(Result[8]);
        }

        //End Function populate Contact Details 

        $(document).ready(function ()
        {

            //View-Contact Dialog initialization
            dialog = $("#divViewContact").dialog({
                autoOpen: false,
                width: 800,
                modal: true,

                close: function () {

                    $('.blackout').css("display", "none");
                    dialog.dialog("close");
                }
            });
            //End View-Contact Dialog Initialization

            //Fetch the Hidden value 
            company = $('#<%=hdnCompany.ClientID%>').val();
            $('#example').dataTable({
                
                "ajax": "/Fetch/FetchContacts.aspx?cid=" + company,
                "columnDefs": [
            {
                "targets": [0],
                "visible": true,
                "searchable": true
            },
          
                ]
               
            });

            $('#example').on('click', 'tbody tr', function (e)
            {
                var contactID = $('td', this).eq(0).text();
                //window.open("order.aspx?cid=" + contactID);
            });


        });

    </script>

   </asp:Content>

<asp:Content ID="TheMainSection" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
         <h3>Company Details</h3>
          
         <!--Company Details Panel-->
          <div id="divCompany">

              <input value="" id="hdnCompany" type="hidden" runat="server" />

          </div>
        <!--End Company Details Panel-->
         
        <!--Contact List Panel-->

          <div id="ContactList">

              <h3>ContactList</h3><br />

              <table  class="display" id="example">
	            <thead>
		            <tr>
       		            <th >Contact ID</th>
			            <th >Contact Name</th>
                        <th >Contact Address</th>
			            <th >Contact Phone</th>
                        <th>View Contact</th> 
                        <th>Create Order</th>
		            </tr>
        
	            </thead>
     
            
	            <tbody>
		 
	            </tbody>

            </table>



          </div>
        <!--End Contact List Panel-->

        <!--Notes Panel-->
          <div id="divNotes">

          </div>
        <!--End Notes Panel-->

        <!--OutStandingpayments panel-->
          <div id="divOutPayments">


          </div>
        <!--End OutStandingpayments panel-->        
    </div>

    <!--Dialog View Contact Info-->
    <div id="divViewContact"  title="ViewContact">
        <table>
            <tr>
                <td>Contact</td>
                <td><span id="spcontact"></span></td>
            </tr>
            <tr>
                <td>FirstName</td>
                <td><span id="spfirstname" ></span></td>
            </tr>
            <tr>
                <td>LastName</td>
                <td><span id="splastname" ></span></td>

            </tr>
            <tr>
                <td>Telephone</td>
                <td><span id="spTele"></span></td>
            </tr>
            <tr>
                <td>Mobile</td>
                <td><span id="spMobile"></span></td>
            </tr>
            <tr>
                <td>Fax</td>
                <td><span id="spfax"></span></td>
            </tr>
            <tr>
                <td>Email</td>
                <td><span id="spEmail"></span></td>
            </tr>

            <tr>
                <td>Postal address</td>
                <td><span id="spPostalAddress"></span></td>
            </tr>
            <tr>
                <td>Physical Address</td>
                <td><span id="spPhysicalAddress"></span></td>
            </tr>

        </table>
    </div>   
    <!--End View Contact Info-->



   <!--Dialog Edit Contact Info-->
    <div id="divEditContact" title="EditContact">
        <table>
         









        </table>
    </div>
   <!--End Dialog Edit ContactInfo-->




</asp:Content>
