/*This Class consists Functions that facilitates Creating,Updating,Searching Deltone Solution's Invoices,Contacts,etc
 * Author:Sumudu Kodikara(Improvata)
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Logging;
using DevDefined.OAuth.Storage.Basic;
using XeroApi;
using XeroApi.Model;
using XeroApi.OAuth;
using System.Security.Cryptography.X509Certificates;
using Xero.Api.Core.Model;
using  Xero.Api.Infrastructure.Interfaces;
using  Xero.Api.Infrastructure.OAuth.Signing;
using  Xero.Api.Infrastructure.OAuth;
using Xero.Api.Core;
using Xero.Api.Example.Applications;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Serialization;
using NUnit.Framework;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;


namespace DeltoneCRM
{   
    
    public class XeroIntergration
    {
        private const String consumerKey = ""; //Put those in Web  Config
        private const String userAgnetString = "";//Put Those in Web Config;

        XeroApi.OAuth.XeroApiPrivateSession xSession;
        XeroApi.Repository repository;
        protected Repository Repos = null;
        XeroCoreApi api_User; //Api User According to the Skinny Wrapper

    
        //This Method Create Reposirity in the Xero System
        public  Repository  CreateRepository()
        {  
          
            try
            {
                //Load the proivate Certificate from the DISK using  the Password to Create it 
                X509Certificate2 privateCertificate = new X509Certificate2(@"C:\SSLCertificate\\public_privatekey.pfx", "@dmin123");
                IOAuthSession consumerSession = new XeroApiPrivateSession("MyAPITestSoftware", "QXHJ4IHP7KJPKGRR0SDRUOO8LTE063", privateCertificate);
                consumerSession.MessageLogger = new DebugMessageLogger();
                return new Repository(consumerSession);
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.StackTrace.ToString() + ":" + ex.Message.ToString());
                return null;
            }
        }

        //This Methos find XeroModel Contact given by Guid
        public XeroApi.Model.Contact findContact(Repository repos,String strGuid)
        {

            XeroApi.Model.Contact deltoneContact = new XeroApi.Model.Contact();

            var Deltonecontact = repos.Contacts.Where(Contact => Contact.ContactID == Guid.Parse(strGuid)).ToList();
            

            foreach (var contact in Deltonecontact)
            {

                deltoneContact=  (XeroApi.Model.Contact)contact;
            }

            return deltoneContact;
              
        }

        //This Function Create a Contact In the Xero System
        public XeroApi.Model.Contact CreateContact(Repository repos,String strCompnayName,String strFirstName,String strLastName,String strDefaultAreadCode,String strDefaultCountryCode,String strDefaultNumber,String strFaxAreaCode,String strFaxCountryCode,String strFaxNumber,String strMobileAreaCode,String strMobileCountryCode,String strMobileNumber,String strEmail,String strStreetAddressLine1,String strStreetCity,String strSteetCountry,String strStreetPostalCode,String strStreetRegion,String strPostalAddressLine1,String strPostalCity,String strPostalCountry,String strPostalCode,String strPostalRegion)
        {
            XeroApi.Model.Contact DeltoneContact = new XeroApi.Model.Contact();

            try
            {
                DeltoneContact.Name = strCompnayName; //Company Name
                DeltoneContact.FirstName = strFirstName; //First Name of the Compnay Contact
                DeltoneContact.LastName = strLastName; //Last name of the Compnay Contact
                DeltoneContact.EmailAddress = strEmail; //Email of the Company Contact
                DeltoneContact.ContactStatus = "ACTIVE"; //(ACTIVE,ARCHIVED)
                DeltoneContact.IsCustomer=true;

                //Define  Default Phone Number
                XeroApi.Model.Phone Default_phone = new XeroApi.Model.Phone();
                Default_phone.PhoneType = "DEFAULT";
                Default_phone.PhoneAreaCode = strDefaultAreadCode;
                Default_phone.PhoneCountryCode = strDefaultCountryCode;
                Default_phone.PhoneNumber = strDefaultNumber;

                //Define Mobile phone Number
                XeroApi.Model.Phone Mobile = new XeroApi.Model.Phone();
                Mobile.PhoneType = "MOBILE";
                Mobile.PhoneAreaCode = strMobileAreaCode;
                Mobile.PhoneCountryCode = strMobileCountryCode;
                Mobile.PhoneNumber = strMobileNumber;

                //Define Fax Number 

                XeroApi.Model.Phones deltonePhones = new XeroApi.Model.Phones();

                deltonePhones.Add(Default_phone);
                deltonePhones.Add(Mobile);

                DeltoneContact.Phones = deltonePhones;

                //Define Street Address  here 

                XeroApi.Model.Addresses deltone_Address = new Addresses();
                XeroApi.Model.Address street_Address= new XeroApi.Model.Address();
                street_Address.AddressType = "STREET";
                street_Address.AddressLine1 = strStreetAddressLine1;
                street_Address.City = strStreetCity;
                street_Address.Country = strSteetCountry;
                street_Address.PostalCode = strStreetPostalCode;
                street_Address.Region = strStreetPostalCode;

                //Define Postal Address here

                XeroApi.Model.Address postal_Address = new XeroApi.Model.Address();
                postal_Address.AddressType = "POBOX";
                postal_Address.AddressLine1 = strPostalAddressLine1;
                postal_Address.City = strPostalCity;
                postal_Address.Country = strPostalCountry;
                postal_Address.PostalCode = strPostalCode;
                postal_Address.Region = strPostalRegion;

                deltone_Address.Add(street_Address);
                deltone_Address.Add(postal_Address);


                DeltoneContact.Addresses = deltone_Address;
              


                var createdContact = repos.Create(DeltoneContact);

                //Check wether Validation Errors Exsists or Not 
                if (createdContact.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
                {
                    foreach (var message in createdContact.ValidationErrors)
                    {
                        Console.WriteLine("Validation Error: " + message.Message);
                    }
                    return null;
                }

                return ((XeroApi.Model.Contact)createdContact);

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                return null;
            }

        }
        //End Function Create Dletone Contacty in Xero System


        /*This method Update the Deltone Contact in the Xero System*/

        public XeroApi.Model.Contact UpdateContact(Repository repos,String strGuid, String strCompnayName, String strFirstName, String strLastName, String strDefaultAreadCode, String strDefaultCountryCode, String strDefaultNumber, String strFaxAreaCode, String strFaxCountryCode, String strFaxNumber, String strMobileAreaCode, String strMobileCountryCode, String strMobileNumber, String strEmail, String strStreetAddressLine1, String strStreetCity, String strSteetCountry, String strStreetPostalCode, String strStreetRegion, String strPostalAddressLine1, String strPostalCity, String strPostalCountry, String strPostalCode, String strPostalRegion)
        {
            XeroApi.Model.Contact DeltoneContact = new XeroApi.Model.Contact();

            try
            {
                DeltoneContact.Name = strCompnayName; //Company Name
                DeltoneContact.FirstName = strFirstName; //First Name of the Compnay Contact
                DeltoneContact.LastName = strLastName; //Last name of the Compnay Contact
                DeltoneContact.EmailAddress = strEmail; //Email of the Company Contact
                DeltoneContact.ContactStatus = "ACTIVE"; //(ACTIVE,ARCHIVED)
                DeltoneContact.IsCustomer = true;

                DeltoneContact.ContactID = new Guid(strGuid);

                //Define  Default Phone Number
                XeroApi.Model.Phone Default_phone = new XeroApi.Model.Phone();
                Default_phone.PhoneType = "DEFAULT";
                Default_phone.PhoneAreaCode = strDefaultAreadCode;
                Default_phone.PhoneCountryCode = strDefaultCountryCode;
                Default_phone.PhoneNumber = strDefaultNumber;

                //Define Mobile phone Number
                XeroApi.Model.Phone Mobile = new XeroApi.Model.Phone();
                Mobile.PhoneType = "MOBILE";
                Mobile.PhoneAreaCode = strMobileAreaCode;
                Mobile.PhoneCountryCode = strMobileCountryCode;
                Mobile.PhoneNumber = strMobileNumber;

                //Define Fax Number 

                XeroApi.Model.Phones deltonePhones = new XeroApi.Model.Phones();

                deltonePhones.Add(Default_phone);
                deltonePhones.Add(Mobile);

                DeltoneContact.Phones = deltonePhones;

                //Define Street Address  here 

                XeroApi.Model.Addresses deltone_Address = new Addresses();
                XeroApi.Model.Address street_Address = new XeroApi.Model.Address();
                street_Address.AddressType = "STREET";
                street_Address.AddressLine1 = strStreetAddressLine1;
                street_Address.City = strStreetCity;
                street_Address.Country = strSteetCountry;
                street_Address.PostalCode = strStreetPostalCode;
                street_Address.Region = strStreetPostalCode;

                //Define Postal Address here

                XeroApi.Model.Address postal_Address = new XeroApi.Model.Address();
                postal_Address.AddressType = "POBOX";
                postal_Address.AddressLine1 = strPostalAddressLine1;
                postal_Address.City = strPostalCity;
                postal_Address.Country = strPostalCountry;
                postal_Address.PostalCode = strPostalCode;
                postal_Address.Region = strPostalRegion;

                deltone_Address.Add(street_Address);
                deltone_Address.Add(postal_Address);

                DeltoneContact.Addresses = deltone_Address;

                var createdContact = repos.UpdateOrCreate(DeltoneContact);

                //Check wether Validation Errors Exsists or Not 
                if (createdContact.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
                {
                    foreach (var message in createdContact.ValidationErrors)
                    {
                        Console.WriteLine("Validation Error: " + message.Message);
                    }
                    return null;
                }

                return ((XeroApi.Model.Contact)createdContact);

            }
            catch (Exception ex)
            {
                ex.StackTrace.ToString();
                return null;
            }

        }



        /*End Method Update Contact*/



        //This Method Feltch the Organization Name from Xero
        protected String FetchOrganizationName(Repository Res)
        {

            String strOrganizationName = String.Empty;
            if (Res != null)
            {
                strOrganizationName = Res.Organisation.Name;
            }

            return strOrganizationName;

        }
        //End Method Fetch Organization Name 

        //This Function Update Contact in Xero System given by Details
        protected void UpdateContact(Repository repos)
        {
            //Update the Contact Given by a value exsist in contact model

        }


        //This Function Create a New Invoice in the Xero System Given by Details //Add Invoice Representator  Modified Sumudu Kodikara 1/52015
        public XeroApi.Model.Invoice CreateInvoice(Repository repository, String strContactName, String[] arrOrderItems, int PaymentTerms, Decimal deDeliveryCharges,String PROMO)
        {  

            if (repository == null)
            {
                //User Must Authenticate and trow an Error
                return null;
            }
         
            String[] arrLineItem;
            
            //create Line Itmes 
           XeroApi.Model.LineItems LItems = new LineItems();

            //Xero Model LineItem
           XeroApi.Model.LineItem lItem;

            /*Invoice Line Items Creation*/
            #region Invoice LineItems
                for (int i = 0; i < arrOrderItems.Length; i++)
                {
                    if (!String.IsNullOrEmpty(arrOrderItems[i]))
                    {
                        arrLineItem = arrOrderItems[i].Split(',');
                        if (i == 0)
                        {  

                            lItem = new XeroApi.Model.LineItem { Description = arrLineItem[1], ItemCode = arrLineItem[2],  UnitAmount = Convert.ToDecimal(arrLineItem[5]), Quantity = Int32.Parse(arrLineItem[4]),TaxType="OUTPUT"};
                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }
                        if (i != 0)
                        {
                            lItem = new XeroApi.Model.LineItem { Description = arrLineItem[2], ItemCode = arrLineItem[3], UnitAmount = Convert.ToDecimal(arrLineItem[6]), Quantity = Int32.Parse(arrLineItem[5]), TaxType = "OUTPUT" };
                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }

                    }
                }
                //Delivery Handling item Adding here
                lItem = new XeroApi.Model.LineItem { Description = "Delivery & Handling, D & H", UnitAmount = Convert.ToDecimal(deDeliveryCharges) };
                lItem.AccountCode = "200";
                LItems.Add(lItem);   
            #endregion


            #region Promotional Items Creation 
                
                if (!String.IsNullOrEmpty(PROMO))
                {

                    String[] ProItems = PROMO.Split('|');
                    String[] ProItem;
                    for (int i = 0; i < ProItems.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(ProItems[i]))
                        {
                            ProItem = ProItems[i].Split(',');
                            if (i == 0)
                            {
                                float ShippingCost;
                                decimal amount = 0;

                                //Add a line for promotional Item
                                lItem = new XeroApi.Model.LineItem { Description = ProItem[0].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[2]) };
                                lItem.AccountCode = "200";
                                LItems.Add(lItem);
                            }
                            if (i != 0)
                            {
                                float ShipCost;
                                decimal amount = 0;

                                //Add a line for promotional Item
                                lItem = new XeroApi.Model.LineItem { Description = ProItem[1].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[3]) };
                                lItem.AccountCode = "200";
                                LItems.Add(lItem);
                            }

                        }
                    }

                }

             #endregion Promotional Items Creation

                /*End Invoice Line Items Creation*/

           XeroApi.Model.Invoice deltoneInvoice = new XeroApi.Model.Invoice
            {   
                
                Type = "ACCREC",
                Contact = new XeroApi.Model.Contact { Name = strContactName },
                Date=DateTime.Today,
                DueDate = DateTime.Today.AddDays(PaymentTerms),
                Status = "DRAFT",
                LineItems = LItems,
                LineAmountTypes =XeroApi.Model.LineAmountType.Inclusive
             
            };


            var createdInvoice = repository.Create(deltoneInvoice);
            
            //Check wether Validation Errors Exsists or Not 
            if (createdInvoice.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in createdInvoice.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                }
                return null;
            }

            return ((XeroApi.Model.Invoice)createdInvoice);

        }


        //this Method Update XERO Invoice Status given by Guid
        public XeroApi.Model.Invoice UpdtateInvoice(Repository repos, String strGuid, String[] arrOrderItems, int PaymentTerms, Decimal deDeliveryCharges,String PROMO)
        {

            if (repos == null)
            {
                return null;
            }

            String[] arrLineItem;

            //create Line Itmes 
            XeroApi.Model.LineItems LItems = new LineItems();

            //Xero Model LineItem
            XeroApi.Model.LineItem lItem;

            /*Invoice Line Items Creation*/
            #region Invoice LineItems
            for (int i = 0; i < arrOrderItems.Length; i++)
            {
                if (!String.IsNullOrEmpty(arrOrderItems[i]))
                {
                    arrLineItem = arrOrderItems[i].Split(',');
                    if (i == 0)
                    {

                        lItem = new XeroApi.Model.LineItem { Description = arrLineItem[1], ItemCode = arrLineItem[2], UnitAmount = Convert.ToDecimal(arrLineItem[5]), Quantity = Int32.Parse(arrLineItem[4]), TaxType = "OUTPUT" };
                        lItem.AccountCode = "200";
                        LItems.Add(lItem);
                    }
                    if (i != 0)
                    {
                        lItem = new XeroApi.Model.LineItem { Description = arrLineItem[2], ItemCode = arrLineItem[3], UnitAmount = Convert.ToDecimal(arrLineItem[6]), Quantity = Int32.Parse(arrLineItem[5]), TaxType = "OUTPUT" };
                        lItem.AccountCode = "200";
                        LItems.Add(lItem);
                    }

                }
            }
            //Delivery Handling item Adding here
            lItem = new XeroApi.Model.LineItem { Description = "Delivery & Handling, D & H", UnitAmount = Convert.ToDecimal(deDeliveryCharges) };
            lItem.AccountCode = "200";
            LItems.Add(lItem);
            #endregion
            /*End Invoice Line Items Creation*/


            #region Promotional Item Creation 

            if (!String.IsNullOrEmpty(PROMO))
            {

                String[] ProItems = PROMO.Split('|');
                String[] ProItem;
                for (int i = 0; i < ProItems.Length; i++)
                {
                    if (!String.IsNullOrEmpty(ProItems[i]))
                    {
                        ProItem = ProItems[i].Split(',');
                        if (i == 0)
                        {
                            float ShippingCost;
                            decimal amount = 0;

                            //Add a line for promotional Item
                            lItem = new XeroApi.Model.LineItem { Description = ProItem[0].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[2]) };
                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }
                        if (i != 0)
                        {
                            float ShipCost;
                            decimal amount = 0;

                            //Add a line for promotional Item
                            lItem = new XeroApi.Model.LineItem { Description = ProItem[1].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[3])};
                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }

                    }
                }

            }


            #endregion Promotional Item Creation


            XeroApi.Model.Invoice deltoneInvoice = new XeroApi.Model.Invoice
            {
                Type = "ACCREC",
                InvoiceID = new Guid(strGuid),
                 LineItems = LItems,
                LineAmountTypes =XeroApi.Model.LineAmountType.Inclusive,
                Status = "AUTHORISED"
            };

            var updatedInvoice = repos.UpdateOrCreate(deltoneInvoice);

            if (updatedInvoice.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in updatedInvoice.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                }
                return null;
            }
            return ((XeroApi.Model.Invoice)updatedInvoice);
        }


        /*Test Method for Creating Sample Invoice in the Xero System*/
        public XeroApi.Model.Invoice ExampleInvoice()
        {
          string TestContactName = "Sumudu Kodikara (Test)";
          XeroApi.Model.Invoice invoiceToCreate = new  XeroApi.Model.Invoice
          {
                Type = "ACCREC",
                Contact = new XeroApi.Model.Contact { Name = TestContactName },
                Date = DateTime.Today,
                DueDate = DateTime.Today.AddDays(14),
                Status = "DRAFT",
                
               
                LineItems = new LineItems
                        {
                            new   XeroApi.Model.LineItem
                                {
                                    Description = "Services Rendered",
                                    Quantity = 1,
                                    UnitAmount = 1,
                                    TaxType="INPUT"
                                    
                                }
                        }
            };

            var createdInvoice = repository.Create(invoiceToCreate);
            return null;
        }



        /*Create the Purchase Order  In the Xero System With Type Accounts Payable*/
        protected void CreatePurchaseOrder(Repository Repos)
        {
            
              
        }
        
        //This Method Create Private User According to the XERO WRAPPER LIB
        public XeroCoreApi CreateAPI()
        {
            XeroCoreApi api_private = null;
            try
            {
                X509Certificate2 privateCertificate = new X509Certificate2(@"C:\OpenSSL-Win32\bin\public_privatekey.pfx", "sumudu123");
                api_private = new XeroCoreApi("https://api.xero.com", new PrivateAuthenticator(privateCertificate),
                new Consumer("AKS4LTBIHGV9NHNIKDKZI3GCFOYTLQ", "6KMAWJ07MI01UGLNQ7G3RJ3NLCLYSU"), null,
                new DefaultMapper(), new DefaultMapper());

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString() + ":" + ex.Message.ToString());
                api_private = null;
            }
            
            return api_private;
        }


        /*This Function Creates a New Item In the Xero System */
        public  void  CreateItem(Repository Repos,String strItemCode,String strItemDescription,decimal SalesUnitPrice,decimal PurchaseUnitPrice)
        {
            String strOrganization = String.Empty;
            Xero.Api.Core.Model.Item DeltoneItem = new Xero.Api.Core.Model.Item();
            String strvalstatus = String.Empty;
            try
            {
                X509Certificate2 privateCertificate = new X509Certificate2(@"C:\OpenSSL-Win32\bin\public_privatekey.pfx", "sumudu123");
                var  private_app_api = new XeroCoreApi("https://api.xero.com", new PrivateAuthenticator(privateCertificate),
                new Consumer("AKS4LTBIHGV9NHNIKDKZI3GCFOYTLQ","6KMAWJ07MI01UGLNQ7G3RJ3NLCLYSU"), null,
                new DefaultMapper(), new DefaultMapper());
                 var org = private_app_api.Organisation;
                strOrganization = org.ToString();
               
                /*
                DeltoneItem.Code = code;
                DeltoneItem.Description = "Test Description";
                SalesDetails price_Sales = new Xero.Api.Core.Model.SalesDetails();
                price_Sales.UnitPrice = 12;
                price_Sales.TaxType="OUTPUT";
                price_Sales.AccountCode = "200";
                PurchaseDetails price_purchase = new Xero.Api.Core.Model.PurchaseDetails();
                price_purchase.UnitPrice = 10;
                price_purchase.TaxType = "OUTPUT";
                price_purchase.AccountCode = "200";
                DeltoneItem.SalesDetails = price_Sales;
                DeltoneItem.PurchaseDetails = price_purchase;
                var Created_Item=  private_app_api.Create(DeltoneItem);
                */

               var code = "DeltoneTestItem185";
               var item = private_app_api.Create(new Xero.Api.Core.Model.Item
               {
                   Code = code,
                   Description = "Buy cheap sell high",
                   SalesDetails = new SalesDetails
                   {
                       AccountCode = "200",
                       UnitPrice = 25.00m
                   },
                   PurchaseDetails = new PurchaseDetails
                   {
                       AccountCode = "200",
                       UnitPrice = 15.0m
                   }
               });

               if (item.Id != Guid.Empty && item.Code == code)
               {
                   Console.Write("Working");
               }
               else //Validation Error Occures
               {

                   if (item.ValidationStatus == Xero.Api.Core.Model.Status.ValidationStatus.Error)
                   {
                       foreach (var message in item.Errors)
                       {
                           Console.WriteLine("Validation Error: " + message.Message);
                       }
                   }
               }

            }
           catch (Exception ex)
            {
                
               // ex.StackTrace.ToString();
            }
 
        }

        /*This Function Update an Exsisting Item in the Xero System*/
        public void UpdateanItem(String strItemCode, String strDescription, Decimal salesUnitPrice, Decimal PurchaseUnitPrice)
        {
        }

        /*This Function Create a New Contact in the Xero System */
        public void CreateXeroContact(String CompanyName,String WebSite,bool primary,DeltoneCompany delComPany)
        {

            Xero.Api.Core.Model.Phone tele = new Xero.Api.Core.Model.Phone();
            tele.PhoneType = PhoneType.Default;
            tele.PhoneAreaCode = delComPany.DefaultAreaCode;
            tele.PhoneCountryCode = "+61";
            tele.PhoneNumber = delComPany.DefaultNumber;

            Xero.Api.Core.Model.Phone mobile = new Xero.Api.Core.Model.Phone();
            mobile.PhoneType = PhoneType.Mobile;
           // mobile.PhoneAreaCode = delComPany.DefaultAreaCode;
            mobile.PhoneCountryCode = "+61";
            mobile.PhoneNumber = delComPany.MobileNumber;


            List<Xero.Api.Core.Model.Phone> del_phones = new List<Xero.Api.Core.Model.Phone>();
            del_phones.Add(tele);
            del_phones.Add(mobile);
            
            XeroCoreApi private_user = CreateAPI();
            
            var Contact = private_user.Create(new Xero.Api.Core.Model.Contact
            {

                Name = delComPany.CompanyName,
                Website = delComPany.CompanyWebSite,
                Phones = del_phones,
              
            });
            
        }

        /*This Function Update the Contact in the Xero System*/
        public void UpdateXeroContact()
        {
        }

    }
}