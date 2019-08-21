/*This Class consists Functions that facilitate Creating,Updating,Searching Deltone Solution's Invoices,Contacts,etc
 * Aurthor:Sumudu Kodikara(Improvata)
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
using Xero.Api.Core.Model;

using System.Security.Cryptography.X509Certificates;
using Xero.Api.Core.Model;
using Xero.Api.Infrastructure.Interfaces;
using Xero.Api.Infrastructure.OAuth.Signing;
using Xero.Api.Infrastructure.OAuth;
using Xero.Api.Core;
using Xero.Api.Example.Applications;
using Xero.Api.Example.Applications.Private;
using Xero.Api.Example.Applications.Public;
using Xero.Api.Example.TokenStores;
using Xero.Api.Serialization;
using NUnit.Framework;
using Xero.Api.Core.Model.Status;
using Xero.Api.Core.Model.Types;
using DeltoneCRM_DAL;
using System.Configuration;
using System.Collections;
using XeroApi.OAuth;
using XeroApi.Model;

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

        #region Repository_Creation

        //public XeroIntergration()
        //{
        //    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //}


        /*This Method Create Reposirity in the Xero System*/
        public Repository CreateRepository()
        {
            //String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //LogginDAL dal = new LogginDAL(ConnString);
            // System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
           
            try

            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

       //         System.Net.ServicePointManager.ServerCertificateValidationCallback =
       //delegate(object sender1, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
       //                        System.Security.Cryptography.X509Certificates.X509Chain chain,
       //                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
       //{
       //    return true; // **** Always accept
       //};
                X509Certificate2 privateCertificate = new X509Certificate2(@"C:\inetpub\wwwroot\deltonecrm\SSLCert\public_privatekey.pfx", "@dmin123", X509KeyStorageFlags.MachineKeySet);
                IOAuthSession consumerSession = new XeroApiPrivateSession("MyAPITestSoftware", "AKS4LTBIHGV9NHNIKDKZI3GCFOYTLQ", privateCertificate);
              
                consumerSession.MessageLogger = new DebugMessageLogger();
                return new Repository(consumerSession);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString() + ":" + ex.Message.ToString());
                //dal.WriteTOLog(0, ex.StackTrace.ToString() + ":" + ex.Message.ToString(), "XeroIntergration.cs", "CreateRepository","ERROR");
                return null;
            }
        }


        /*This Method Create Private User According to the XERO WRAPPER LIB(NEW)*/
        public XeroCoreApi CreateAPI()
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal = new LogginDAL(ConnString);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

   //         System.Net.ServicePointManager.ServerCertificateValidationCallback =
   //delegate(object sender1, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
   //                        System.Security.Cryptography.X509Certificates.X509Chain chain,
   //                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
   //{
   //    return true; // **** Always accept
   //};

            XeroCoreApi api_private = null;
            try
            {
                X509Certificate2 privateCertificate = new X509Certificate2(@"C:\inetpub\wwwroot\deltonecrm\SSLCert\public_privatekey.pfx", "@dmin123", X509KeyStorageFlags.MachineKeySet);
                api_private = new XeroCoreApi("https://api.xero.com", new PrivateAuthenticator(privateCertificate),
                new Consumer("AKS4LTBIHGV9NHNIKDKZI3GCFOYTLQ", "6KMAWJ07MI01UGLNQ7G3RJ3NLCLYSU"), null,
                new DefaultMapper(), new DefaultMapper());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString() + ":" + ex.Message.ToString());
                dal.WriteTOLog(0, ex.StackTrace.ToString() + ":" + ex.Message.ToString(), "XeroIntergration.cs", "CreateAPI(NEW)", "ERROR");
                api_private = null;
            }

            return api_private;
        }

        private string GetDelcostForSupplier(string supplierNameandValue, string supplierName)
        {
            string suppcost = "";
            if (!string.IsNullOrEmpty(supplierNameandValue))
            {
                var splitNameAndValue = supplierNameandValue.Split(',');
                foreach (var item in splitNameAndValue)
                {
                    var name = item.Split(':')[0];
                    var costVal = item.Split(':')[1];
                    name = name.Replace("del_", "");
                    if (name.ToUpper() == supplierName.ToUpper())
                    {
                        suppcost = costVal;
                    }
                }

            }
            return suppcost;

        }


        private string GetDeliveryCostRealForSupplier(string supplierNameandValue, string supplierName)
        {
            string suppcost = "";
            if (!string.IsNullOrEmpty(supplierNameandValue))
            {
                var splitNameAndValue = supplierNameandValue.Split('|');
                foreach (var item in splitNameAndValue)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var costVal = "";
                        var costRealVal = "";
                        var name = item.Split(',')[0];
                        if (string.IsNullOrEmpty(name))
                        {
                            name = item.Split(',')[1];
                            costVal = item.Split(',')[2];
                            costRealVal = item.Split(',')[3];
                        }
                        else
                        {
                            costVal = item.Split(',')[1];
                            costRealVal = item.Split(',')[2];
                        }

                        if (name.Trim().ToUpper() == supplierName.Trim().ToUpper())
                        {
                            return suppcost = costRealVal;
                        }
                    }
                }

            }
            return suppcost;

        }


        private string GetAdjustmentForSupplier(string supplierNameandValue, string supplierName)
        {
            string suppcost = "";
            if (!string.IsNullOrEmpty(supplierNameandValue))
            {
                var splitNameAndValue = supplierNameandValue.Split('|');
                foreach (var item in splitNameAndValue)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var costAdjVal = "";
                        var name = item.Split(',')[0];
                        if (string.IsNullOrEmpty(name))
                        {
                            name = item.Split(',')[1];
                            costAdjVal = item.Split(',')[2];
                        }
                        else
                        {
                            costAdjVal = item.Split(',')[1];
                        }

                        if (name.ToUpper() == supplierName.ToUpper())
                        {
                            return suppcost = costAdjVal;
                        }
                    }
                }

            }
            return suppcost;

        }

        private DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName
            GetXeroInfo(List<DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName> listItems,
            string supplierName)
        {
            var obj = new DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName();

            obj = (from item in listItems where item.SupplierName == supplierName select item).FirstOrDefault();

            return obj;
        }

        Dictionary<string, string> dictXeroAccountSupplierList = new Dictionary<string, string>()
        {  
            {"SCRIBAL","399"},
            {"Q-Imaging","400"},
            {"Toner Warehouse","401"},
            {"Dynamic","402"},
            {"TOD","403"},
            {"XIT","404"},
            {"Microjet","405"},
            {"Alloys","406"},
            {"Ausjet","407"},
            {"GNS","408"},
            {"General","409"},
            {"SYNNEX","410"},
            {"CartridgeOne","396"}
        };


        private string GetXeroAccountCodeForPurchase(string suppName)
        {
            // var keyValueDefault = "General";

            string SuppCode = "409";   //default
            if (dictXeroAccountSupplierList.TryGetValue(suppName, out SuppCode))
            {
                return SuppCode;
            }
            else
                SuppCode = "409";

            if (string.IsNullOrEmpty(SuppCode))
                SuppCode = "409";
            return SuppCode;
        }

        public void FillXeroPurchaseItems(List<DeltoneCRM_DAL.PurchaseDAL.PurchaseItemAndSupplier> purchaseItemsandSupplier,
            string constring, int orderID, string reference, string suppdelcost, bool isNotNew = false,
            List<DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName> purchaseExroINfo = null)
        {

            var purchasedal = new PurchaseDAL(constring);
            var purchaseDate = DateTime.Now;
            var adjValue = purchasedal.GetAdjByOrderId(orderID);
            var totalDelCost = 0.0m;
            string SuppCode = "409";
            //  var purchaseItems=purchaseItemsandSupplier.

            foreach (var item in purchaseItemsandSupplier)
            {
                var listXeroItems = new List<Xero.Api.Core.Model.LineItem>();
                var purchaseItems = item.OrderItems;
                var contactName = item.SupplierName;
                var delCost = GetDeliveryCostRealForSupplier(suppdelcost, contactName);

                if (!string.IsNullOrEmpty(delCost))
                    totalDelCost = Convert.ToDecimal(delCost);

                if (!string.IsNullOrEmpty(adjValue))
                {
                    var adjBySupplier = GetAdjustmentForSupplier(adjValue, contactName);
                    if (!string.IsNullOrEmpty(adjBySupplier))
                        totalDelCost = totalDelCost + Convert.ToDecimal(adjBySupplier);
                }

                List<Xero.Api.Core.Model.LineItem> LItems = new List<Xero.Api.Core.Model.LineItem>();

                //Xero Model LineItem
                Xero.Api.Core.Model.LineItem lItem;
                int qty = 0;
                /*Invoice Line Items Creation*/
                #region Invoice LineItems
                for (int i = 0; i < purchaseItems.Count; i++)
                {

                    var description = purchasedal.ItemDescription(purchaseItems[i].SupplierCode);

                    lItem = new Xero.Api.Core.Model.LineItem
                    {
                        Description = description,
                        ItemCode = purchaseItems[i].SupplierCode,
                        UnitAmount = Convert.ToDecimal(purchaseItems[i].COGAmount),
                        Quantity = Int32.Parse(purchaseItems[i].Quantity),
                        TaxType = "INPUT"
                    };
                    lItem.AccountCode = GetXeroAccountCodeForPurchase(contactName.Trim());
                    // qty = qty + Int32.Parse(purchaseItems[i].Quantity);
                    LItems.Add(lItem);

                }
                // Delivery Handling item Adding here
                if (totalDelCost > 0)
                {
                    lItem = new Xero.Api.Core.Model.LineItem
                    {
                        ItemCode = "D & H",
                        Description = "DELIVERY & HANDLING",
                        UnitAmount = totalDelCost,
                        TaxType = "INPUT",
                        Quantity = 1
                    };
                    lItem.AccountCode = SuppCode;
                    LItems.Add(lItem);
                }
                if (LItems.Count > 0)
                    Create_authorised_purchase_order(contactName, LItems, purchaseDate,
                        purchasedal, orderID, reference, delCost, isNotNew, purchaseExroINfo);

            }

        }


        public void ChangeStatusPurchaseOrderBilled(
         List<DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName> purchaseExroINfo)
        {

            UpdatePurchaseOrderBill(purchaseExroINfo);

        }

        public void ChangeStatusPurchaseOrderAuthorised(
          List<DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName> purchaseExroINfo)
        {

            UpdatePurchaseOrderAuthorised(purchaseExroINfo);

        }

        private void UpdatePurchaseOrderAuthorised(
            List<DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName> purchaseExroINfo)
        {
            var apiXero = CreateAPI();

            var repos = CreateRepository();
            PurchaseOrder purchaseOrder = null;

            foreach (var item in purchaseExroINfo)
            {
                var contactName = item.SupplierName.ToUpper();


                try
                {

                    if (!string.IsNullOrWhiteSpace(item.XeroPurchaseId))
                    {
                        var guID = new Guid(item.XeroPurchaseId);
                        purchaseOrder = apiXero.PurchaseOrders.Find(guID);
                        if (purchaseOrder != null)
                        {
                            purchaseOrder.Status = PurchaseOrderStatus.Authorised;
                            purchaseOrder = apiXero.PurchaseOrders.Update(purchaseOrder);
                        }

                    }
                }

                catch (Xero.Api.Infrastructure.Exceptions.XeroApiException xrex)
                {
                    var dataxrex = xrex.Data;

                    //foreach (var message in purchaseOrder.Errors)
                    //{
                    //    Console.WriteLine("Validation Error: " + message.Message);
                    //}
                    return;
                }


            }
        }


        private void UpdatePurchaseOrderBill(List<DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName> purchaseExroINfo)
        {
            var apiXero = CreateAPI();

            var repos = CreateRepository();
            PurchaseOrder purchaseOrder = null;

            foreach (var item in purchaseExroINfo)
            {
                var contactName = item.SupplierName.ToUpper();

                try
                {

                    if (!string.IsNullOrWhiteSpace(item.XeroPurchaseId))
                    {
                        var guID = new Guid(item.XeroPurchaseId);
                        purchaseOrder = apiXero.PurchaseOrders.Find(guID);
                        if (purchaseOrder != null)
                        {
                            purchaseOrder.Status = PurchaseOrderStatus.Billed;

                            purchaseOrder = apiXero.PurchaseOrders.Update(purchaseOrder);
                        }

                    }
                }

                catch (Xero.Api.Infrastructure.Exceptions.XeroApiException xrex)
                {
                    var dataxrex = xrex.Data;

                    //foreach (var message in purchaseOrder.Errors)
                    //{
                    //    Console.WriteLine("Validation Error: " + message.Message);
                    //}
                    return;
                }


            }

        }

        // change status to Authorised from Draft 
        private void Create_authorised_purchase_order(string contactName,
            List<Xero.Api.Core.Model.LineItem> items, DateTime datePurchase, PurchaseDAL purchasedal, int orderID,
            string reference, string delcost, bool isNotNew = false,
            List<DeltoneCRM_DAL.PurchaseDAL.PurchaseItemIdAndSupplierName> purchaseExroINfo = null)
        {
            var apiXero = CreateAPI();

            var repos = CreateRepository();
            PurchaseOrder purchaseOrder = null;
            var noCaseChanges = contactName;
            contactName = contactName.ToUpper();
            string contactId = findContactByName(repos, contactName);


            if (!string.IsNullOrEmpty(contactId))
            {
                var contactGuid = new Guid(contactId);

                try
                {
                    if (isNotNew)
                    {
                        var xeroPurchasdINfo = GetXeroInfo(purchaseExroINfo, noCaseChanges);
                        if (xeroPurchasdINfo != null)
                        {
                            if (!string.IsNullOrWhiteSpace(xeroPurchasdINfo.XeroInvoiceNumber)
                                &&
                                !string.IsNullOrWhiteSpace(xeroPurchasdINfo.XeroPurchaseId))
                            {
                                var guID = new Guid(xeroPurchasdINfo.XeroPurchaseId);
                                purchaseOrder = apiXero.PurchaseOrders.Update(
                                   new PurchaseOrder
                                   {
                                       Status = PurchaseOrderStatus.Draft,
                                       Date = datePurchase,
                                       Reference = reference,
                                       Contact = new Xero.Api.Core.Model.Contact { Id = contactGuid },
                                       LineItems = items,
                                       Id = guID,
                                       Number = xeroPurchasdINfo.XeroInvoiceNumber
                                   }
                               );
                                purchasedal.UpdatePurchaseItemBYXeroInvoiceId(orderID, contactName, xeroPurchasdINfo.XeroInvoiceNumber,
                                reference, delcost, xeroPurchasdINfo.XeroPurchaseId);
                            }
                            else
                            {
                                purchaseOrder = apiXero.PurchaseOrders.Create(
                           new PurchaseOrder
                           {
                               Status = PurchaseOrderStatus.Draft,
                               Date = datePurchase,
                               Reference = reference,
                               Contact = new Xero.Api.Core.Model.Contact { Id = contactGuid },
                               LineItems = items
                           }
                       );

                                var poNumber = purchaseOrder.Number;
                                var purchaseId = purchaseOrder.Id.ToString();

                                purchasedal.UpdatePurchaseItemBYXeroInvoiceId(orderID, contactName, poNumber, reference, delcost, purchaseId);
                            }
                        }
                        else
                        {
                            purchaseOrder = apiXero.PurchaseOrders.Create(
                           new PurchaseOrder
                           {
                               Status = PurchaseOrderStatus.Draft,
                               Date = datePurchase,
                               Reference = reference,
                               Contact = new Xero.Api.Core.Model.Contact { Id = contactGuid },
                               LineItems = items
                           }
                       );

                            var poNumber = purchaseOrder.Number;
                            var purchaseId = purchaseOrder.Id.ToString();

                            purchasedal.UpdatePurchaseItemBYXeroInvoiceId(orderID, contactName, poNumber, reference, delcost, purchaseId);
                        }
                    }
                    else
                    {

                        purchaseOrder = apiXero.PurchaseOrders.Create(
                           new PurchaseOrder
                           {
                               Status = PurchaseOrderStatus.Draft,
                               Date = datePurchase,
                               Reference = reference,
                               Contact = new Xero.Api.Core.Model.Contact { Id = contactGuid },
                               LineItems = items
                           }
                       );

                        var poNumber = purchaseOrder.Number;
                        var purchaseId = purchaseOrder.Id.ToString();

                        purchasedal.UpdatePurchaseItemBYXeroInvoiceId(orderID, contactName, poNumber, reference, delcost, purchaseId);

                    }


                }


                   // return null;
                //}

                catch (Xero.Api.Infrastructure.Exceptions.XeroApiException xrex)
                {
                    var dataxrex = xrex.Data;

                    //foreach (var message in purchaseOrder.Errors)
                    //{
                    //    Console.WriteLine("Validation Error: " + message.Message);
                    //}
                    return;
                }

            }

            //  

            // Assert.True(purchaseOrder.Id != Guid.Empty);
            // Assert.True(purchaseOrder.Status == PurchaseOrderStatus.Authorised);
        }

        /* This Method Fetch the  Oraganization given by repository*/
        protected String FetchOrganizationName(Repository Res)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal = new LogginDAL(ConnString);

            String strOrganizationName = String.Empty;
            if (Res != null)
            {
                strOrganizationName = Res.Organisation.Name;
            }
            return strOrganizationName;

        }


                #endregion Repository_Creation

        #region CONTACT


        //Find Contact by Name 
        public String findContactByName(Repository repos, String contactName)
        {

            String contactGuid = String.Empty;

            XeroApi.Model.Contact deltoneContact = new XeroApi.Model.Contact();

            var contacts = from con in repos.Contacts
                           where con.Name == contactName
                           select con;

            foreach (XeroApi.Model.Contact c in contacts)
            {
                contactGuid = c.ContactID.ToString();

            }

            return contactGuid;

        }



        //This Methos find XeroModel Contact given by Guid
        public XeroApi.Model.Contact findContact(Repository repos, String strGuid)
        {

            XeroApi.Model.Contact deltoneContact = new XeroApi.Model.Contact();

            var Deltonecontact = repos.Contacts.Where(Contact => Contact.ContactID == Guid.Parse(strGuid)).ToList();


            foreach (var contact in Deltonecontact)
            {

                deltoneContact = (XeroApi.Model.Contact)contact;
            }

            return deltoneContact;

        }

        //This Function Create a Contact In the Xero System
        public XeroApi.Model.Contact CreateContact(Repository repos, String strCompnayName, String strFirstName, String strLastName, String strDefaultAreadCode, String strDefaultCountryCode, String strDefaultNumber, String strFaxAreaCode, String strFaxCountryCode, String strFaxNumber, String strMobileAreaCode, String strMobileCountryCode, String strMobileNumber, String strEmail, String strStreetAddressLine1, String strStreetCity, String strSteetCountry, String strStreetPostalCode, String strStreetRegion, String strPostalAddressLine1, String strPostalCity, String strPostalCountry, String strPostalCode, String strPostalRegion)
        {
            XeroApi.Model.Contact DeltoneContact = new XeroApi.Model.Contact();
            String strErrorMessage = String.Empty;
            //Dictionary<String, XeroApi.Model.Contact> result_List = new Dictionary<string, XeroApi.Model.Contact>();

            try
            {
                DeltoneContact.Name = strCompnayName; //Company Name
                DeltoneContact.FirstName = strFirstName; //First Name of the Compnay Contact
                DeltoneContact.LastName = strLastName; //Last name of the Compnay Contact
                DeltoneContact.EmailAddress = strEmail; //Email of the Company Contact
                DeltoneContact.ContactStatus = "ACTIVE"; //(ACTIVE,ARCHIVED)
                DeltoneContact.IsCustomer = true;

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
                //Modified here Add AttentionTO
                street_Address.AttentionTo = strFirstName + " " + strLastName;

                //Define Postal Address here

                XeroApi.Model.Address postal_Address = new XeroApi.Model.Address();
                postal_Address.AddressType = "POBOX";
                postal_Address.AddressLine1 = strPostalAddressLine1;
                postal_Address.City = strPostalCity;
                postal_Address.Country = strPostalCountry;
                postal_Address.PostalCode = strPostalCode;
                postal_Address.Region = strPostalRegion;
                postal_Address.AttentionTo = strFirstName + " " + strLastName;

                //Modified here AttentionTo

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
                        strErrorMessage = strErrorMessage + message.Message.ToString();
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
        public XeroApi.Model.Contact UpdateContact(Repository repos, String strGuid, String strCompnayName, String strFirstName, String strLastName, String strDefaultAreadCode, String strDefaultCountryCode, String strDefaultNumber, String strFaxAreaCode, String strFaxCountryCode, String strFaxNumber, String strMobileAreaCode, String strMobileCountryCode, String strMobileNumber, String strEmail, String strStreetAddressLine1, String strStreetCity, String strSteetCountry, String strStreetPostalCode, String strStreetRegion, String strPostalAddressLine1, String strPostalCity, String strPostalCountry, String strPostalCode, String strPostalRegion)
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

                //Modified here Add Attention to 

                street_Address.AttentionTo = strFirstName + " " + strLastName;



                //Define Postal Address here

                XeroApi.Model.Address postal_Address = new XeroApi.Model.Address();
                postal_Address.AddressType = "POBOX";
                postal_Address.AddressLine1 = strPostalAddressLine1;
                postal_Address.City = strPostalCity;
                postal_Address.Country = strPostalCountry;
                postal_Address.PostalCode = strPostalCode;
                postal_Address.Region = strPostalRegion;

                //Modified here Add Attention To 
                postal_Address.AttentionTo = strFirstName + " " + strLastName;

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


        /*Creates XERO contact using wrapper lib(NEW) */
        public void CreateXeroContact(String CompanyName, String WebSite, bool primary, DeltoneCompany delComPany)
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

        #endregion CONTACT

        #region INVOICE

        //This Function Create a New Invoice in the Xero System Given by Details //Add Invoice Representator  Modified Sumudu Kodikara 1/52015
        public Xero.Api.Core.Model.Invoice CreateInvoice(int OrderId, XeroCoreApi repository,
            String strContactName, String[] arrOrderItems, int PaymentTerms, Decimal deDeliveryCharges, String PROMO, String strReference, DateTime invoiceDate)
        {
            Dictionary<String, XeroApi.Model.Invoice> di_result = new Dictionary<string, XeroApi.Model.Invoice>();
            XeroApi.Model.Invoice Result_Invoice = new XeroApi.Model.Invoice();
            String strErrorMessage = String.Empty; //No Errors found here
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal_login = new LogginDAL(ConnString);
            ILog _logger = LogManager.GetLogger(typeof(XeroIntergration));
            _logger.Info("Create invoice company name :" + strContactName + " Order Id " + OrderId);


            if (repository == null)
            {
                //User Must Authenticate and trow an Error
                return null;
            }

            String[] arrLineItem;

            //create Line Itmes 
            var LItems = new List<Xero.Api.Core.Model.LineItem>();

            //Xero Model LineItem
            Xero.Api.Core.Model.LineItem lItem;

            /*Invoice Line Items Creation*/
            #region Invoice LineItems
            for (int i = 0; i < arrOrderItems.Length; i++)
            {
                if (!String.IsNullOrEmpty(arrOrderItems[i]))
                {
                    arrLineItem = arrOrderItems[i].Split(',');
                    if (i == 0)
                    {

                        lItem = new Xero.Api.Core.Model.LineItem { Description = arrLineItem[1], ItemCode = arrLineItem[2], UnitAmount = Convert.ToDecimal(arrLineItem[5]), Quantity = Int32.Parse(arrLineItem[4]), TaxType = "OUTPUT" };
                        lItem.AccountCode = "200";
                        LItems.Add(lItem);
                    }
                    if (i != 0)
                    {
                        lItem = new Xero.Api.Core.Model.LineItem { Description = arrLineItem[2], ItemCode = arrLineItem[3], UnitAmount = Convert.ToDecimal(arrLineItem[6]), Quantity = Int32.Parse(arrLineItem[5]), TaxType = "OUTPUT" };
                        lItem.AccountCode = "200";
                        LItems.Add(lItem);
                    }

                }
            }
            //Delivery Handling item Adding here
            lItem = new Xero.Api.Core.Model.LineItem { ItemCode = "D & H", Description = "Delivery & Handling", UnitAmount = Convert.ToDecimal(deDeliveryCharges), TaxType = "OUTPUT" };
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
                            lItem = new Xero.Api.Core.Model.LineItem { Description = ProItem[0].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[2]), TaxType = "OUTPUT" };
                            //Modified here Add PROMO ITEM CODE here
                            lItem.ItemCode = ProItem[4].ToString();

                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }
                        if (i != 0)
                        {
                            float ShipCost;
                            decimal amount = 0;

                            //Add a line for promotional Item
                            lItem = new Xero.Api.Core.Model.LineItem { Description = ProItem[1].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[3]), TaxType = "OUTPUT" };
                            //Modifed here Add PROMO ITEM CODE here
                            lItem.ItemCode = ProItem[5].ToString();
                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }

                    }
                }

            }

            #endregion Promotional Items Creation

            /*End Invoice Line Items Creation*/

            Xero.Api.Core.Model.Invoice deltoneInvoice = new Xero.Api.Core.Model.Invoice
             {

                 Type = InvoiceType.AccountsReceivable,
                 Contact = new Xero.Api.Core.Model.Contact { Name = strContactName },
                 Date = invoiceDate,//Modified here 
                 DueDate = invoiceDate.AddDays(PaymentTerms),//Modified here
                 Status = Xero.Api.Core.Model.Status.InvoiceStatus.Draft,
                 LineItems = LItems,
                 // Reference = strReference,
                 //LineAmountTypes = Xero.Api.Core.Model.Types.LineAmountType.Inclusive,


             };

           

            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            //System.Net.ServicePointManager.CheckCertificateRevocationList = false;
            //System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            //System.Net.ServicePointManager.UseNagleAlgorithm = false;
            //System.Net.ServicePointManager.Expect100Continue = true;
            //System.Net.ServicePointManager.SetTcpKeepAlive(true, 100, 100);
          //  System.Net.ServicePointManager. = true;
           
            try
            {
               

                var createdInvoice = repository.Create(deltoneInvoice);


                //Check wether Validation Errors Exsists or Not 
                if (createdInvoice.ValidationStatus == Xero.Api.Core.Model.Status.ValidationStatus.Error)
                {
                    foreach (var message in createdInvoice.Warnings)
                    {
                        Console.WriteLine("Validation Error: " + message.Message);
                        //dal.WriteTOLog(OrderId, message.Message.ToString(), "XeroIntergration.cs", "CreateInvoice");
                        Log_Details(OrderId, message.Message.ToString(), "XeroIntergration.cs", "CreateInvoice", dal_login, "ERROR");//WRITE TO LOG
                    }

                    return null;
                }
                else
                {
                    Log_Details(OrderId, "CREATE SUCCESS MSG", String.Empty, String.Empty, dal_login, "SUCCESS");//Success Msg
                }

                return ((Xero.Api.Core.Model.Invoice)createdInvoice);
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.StackTrace.ToString() + ":" + ex.Message.ToString();
                //dal.WriteTOLog(OrderId, strErrorMessage, "XeroIntergration.cs", "CreateInvoice");
                Log_Details(OrderId, strErrorMessage, "XeroIntergration.cs", "CreateInvoice", dal_login, "ERROR");//WRITE TO LOG
                return null;
            }

        }


        //This Function Create a New Invoice in the Xero System Given by Details //Add Invoice Representator  Modified Sumudu Kodikara 1/52015
        public XeroApi.Model.Invoice CreateInvoice(int OrderId, Repository repository, String strContactName, String[] arrOrderItems, int PaymentTerms, Decimal deDeliveryCharges, String PROMO, String strReference, DateTime invoiceDate)
        {
            Dictionary<String, XeroApi.Model.Invoice> di_result = new Dictionary<string, XeroApi.Model.Invoice>();
            XeroApi.Model.Invoice Result_Invoice = new XeroApi.Model.Invoice();
            String strErrorMessage = String.Empty; //No Errors found here
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal_login = new LogginDAL(ConnString);
            ILog _logger = LogManager.GetLogger(typeof(XeroIntergration));
            _logger.Info("Create invoice company name :" + strContactName + " Order Id " + OrderId);


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
            lItem = new XeroApi.Model.LineItem { ItemCode = "D & H", Description = "Delivery & Handling", UnitAmount = Convert.ToDecimal(deDeliveryCharges), TaxType = "OUTPUT" };
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
                            lItem = new XeroApi.Model.LineItem { Description = ProItem[0].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[2]), TaxType = "OUTPUT" };
                            //Modified here Add PROMO ITEM CODE here
                            lItem.ItemCode = ProItem[4].ToString();

                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }
                        if (i != 0)
                        {
                            float ShipCost;
                            decimal amount = 0;

                            //Add a line for promotional Item
                            lItem = new XeroApi.Model.LineItem { Description = ProItem[1].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[3]), TaxType = "OUTPUT" };
                            //Modifed here Add PROMO ITEM CODE here
                            lItem.ItemCode = ProItem[5].ToString();
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
                Date = invoiceDate,//Modified here 
                DueDate = invoiceDate.AddDays(PaymentTerms),//Modified here
                Status = "DRAFT",
                LineItems = LItems,
                Reference = strReference,
                LineAmountTypes = XeroApi.Model.LineAmountType.Inclusive,

            };


            try
            {
               // System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

       //         System.Net.ServicePointManager.ServerCertificateValidationCallback =
       //delegate(object sender1, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
       //                        System.Security.Cryptography.X509Certificates.X509Chain chain,
       //                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
       //{
       //    return true; // **** Always accept
       //};

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
              // repository.ke
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var createdInvoice = repository.Create(deltoneInvoice);


                //Check wether Validation Errors Exsists or Not 
                if (createdInvoice.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
                {
                    foreach (var message in createdInvoice.ValidationErrors)
                    {
                        Console.WriteLine("Validation Error: " + message.Message);
                        //dal.WriteTOLog(OrderId, message.Message.ToString(), "XeroIntergration.cs", "CreateInvoice");
                        Log_Details(OrderId, message.Message.ToString(), "XeroIntergration.cs", "CreateInvoice", dal_login, "ERROR");//WRITE TO LOG
                    }

                    return null;
                }
                else
                {
                    Log_Details(OrderId, "CREATE SUCCESS MSG", String.Empty, String.Empty, dal_login, "SUCCESS");//Success Msg
                }

                return ((XeroApi.Model.Invoice)createdInvoice);
            }
            catch (Exception ex)
            {
                strErrorMessage = ex.StackTrace.ToString() + ":" + ex.Message.ToString();
                //dal.WriteTOLog(OrderId, strErrorMessage, "XeroIntergration.cs", "CreateInvoice");
                Log_Details(OrderId, strErrorMessage, "XeroIntergration.cs", "CreateInvoice", dal_login, "ERROR");//WRITE TO LOG
                return null;
            }

        }

        /*Uopdate the XERO Invoice given by the details*/
        public XeroApi.Model.Invoice UpdtateInvoice(int OrderId, Repository repos, String strGuid, String[] arrOrderItems, int PaymentTerms, Decimal deDeliveryCharges, String PROMO, String OrderStatus, String strReference, DateTime invoiceDate)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal_login = new LogginDAL(ConnString);


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
            lItem = new XeroApi.Model.LineItem { ItemCode = "D & H", Description = "Delivery & Handling", UnitAmount = Convert.ToDecimal(deDeliveryCharges), TaxType = "OUTPUT" };
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
                            //Modified here Add Promotional Item Code here
                            lItem.ItemCode = ProItem[4].ToString();
                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }
                        if (i != 0)
                        {
                            float ShipCost;
                            decimal amount = 0;

                            //Add a line for promotional Item
                            lItem = new XeroApi.Model.LineItem { Description = ProItem[1].ToString(), UnitAmount = Convert.ToDecimal(amount), Quantity = Int32.Parse(ProItem[3]) };
                            //Modified here Add Promotional Item Code here
                            lItem.ItemCode = ProItem[5].ToString();
                            lItem.AccountCode = "200";
                            LItems.Add(lItem);
                        }

                    }
                }

            }


            #endregion Promotional Item Creation


            String xeroOrderStatus = OrderStatus.Equals("APPROVED") ? "AUTHORISED" : "DRAFT";

            XeroApi.Model.Invoice deltoneInvoice = new XeroApi.Model.Invoice
            {
                Type = "ACCREC",
                InvoiceID = new Guid(strGuid),
                LineItems = LItems,
                Date = invoiceDate, //Modified here 
                DueDate = invoiceDate.AddDays(PaymentTerms),//Modified here 
                Reference = strReference,
                LineAmountTypes = XeroApi.Model.LineAmountType.Inclusive,
                Status = xeroOrderStatus
            };

            var updatedInvoice = repos.UpdateOrCreate(deltoneInvoice);

            if (updatedInvoice.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in updatedInvoice.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                    Log_Details(OrderId, message.Message.ToString(), "XeroIntergration.cs", "UpdateInvoice", dal_login, "ERROR");//WRITE TO THE LOG
                }
                return null;
            }
            else
            {
                Log_Details(OrderId, "UPDATE SUCCESS MSG", String.Empty, String.Empty, dal_login, "SUCCESS");//WRITE TO LOG
            }
            return ((XeroApi.Model.Invoice)updatedInvoice);
        }

        /*Authorize the Invoice In Xero System*/
        public XeroApi.Model.Invoice ApproveInvoice(int OrderId, Repository repos, String InvoiceID, String strReference)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal_login = new LogginDAL(ConnString);


            if (repos == null)
            {
                return null;
            }

            XeroApi.Model.Invoice deltoneInvoice = new XeroApi.Model.Invoice
            {
                Type = "ACCREC",
                InvoiceID = new Guid(InvoiceID),
                Reference = strReference,
                LineAmountTypes = XeroApi.Model.LineAmountType.Inclusive,
                Status = "AUTHORISED"
            };

            var ApprovedInvoice = repos.UpdateOrCreate(deltoneInvoice);

            if (ApprovedInvoice.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in ApprovedInvoice.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                    Log_Details(OrderId, message.Message.ToString(), "XeroIntergration.cs", "ApproveInvoice", dal_login, "ERROR");//WRITE TO LOG
                    //dal.WriteTOLog(OrderId, message.Message.ToString(), "XeroIntergration.cs", "ApproveInvoice");
                }
                return null;
            }
            else
            {
                Log_Details(OrderId, "SUCCESS MSG", String.Empty, String.Empty, dal_login, "SUCCESS");//WRITE TO LOG
            }
            return ((XeroApi.Model.Invoice)ApprovedInvoice);

        }


        /*This method Cancel the Invoice in the Xero System*/
        public XeroApi.Model.Invoice CancelInvoice(int OrderID, Repository repos, String strCurrentStatus, String invoiceGuid)
        {

            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal_login = new LogginDAL(ConnString);

            //Get the Xero Status of the Invoice and determine what action to be taken.
            String xeroStatus = strCurrentStatus.Equals("APPROVED") ? "VOIDED" : "DELETED"; /* APPROVED == AUTHORIZED in xero else DRAFT*/

            //String xeroStatus = strCurrentStatus.Equals("AUTHORIZED") ? "VOIDED":"DELETED";
            if (repos == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(invoiceGuid))
            {
                return null;
            }

            XeroApi.Model.Invoice deltoneInvoice = new XeroApi.Model.Invoice
            {
                Type = "ACCREC",
                InvoiceID = new Guid(invoiceGuid),
                LineAmountTypes = XeroApi.Model.LineAmountType.Inclusive,
                Status = xeroStatus
            };

            var ApprovedInvoice = repos.UpdateOrCreate(deltoneInvoice);

            if (ApprovedInvoice.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in ApprovedInvoice.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                    //dal.WriteTOLog(OrderID, message.Message.ToString(), "XeroIntergration.cs", "CancelInvoice");
                    Log_Details(OrderID, message.Message.ToString(), "XeroIntergration.cs", "CancelInvoice", dal_login, "ERROR");//WRITE TO LOG
                }
                return null;
            }
            else
            {
                Log_Details(OrderID, "APPROVE SUCCESS MSG", String.Empty, String.Empty, dal_login, "SUCCESS");//SUCCESS MSG
            }
            return ((XeroApi.Model.Invoice)ApprovedInvoice);

        }
        /*End Mehtod Cancel the Invoice in the Xero System*/



        /*Test Method for Creating Sample Invoice in the Xero System*/
        public XeroApi.Model.Invoice ExampleInvoice()
        {
            string TestContactName = "Sumudu Kodikara (Test)";
            XeroApi.Model.Invoice invoiceToCreate = new XeroApi.Model.Invoice
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



        /*Creates the Purchase Order in the XERO System */
        public XeroApi.Model.Invoice CreatePurchaseOrder(Repository Repos)
        {
            XeroApi.Model.Invoice purchaseInvoice = null;
            return purchaseInvoice;
        }

        /*Updates the Purchase Order in the XERO System*/
        public XeroApi.Model.Invoice UpdatePurchaseOrder(Repository repos)
        {
            XeroApi.Model.Invoice updatePurchaseInvoice = null;
            return updatePurchaseInvoice;
        }

        #endregion INVOICE

        #region ITEM

        /*This Function Creates a New Item In the Xero System */
        public Xero.Api.Core.Model.Item CreateItem(String strItemCode, String strItemDescription, decimal SalesUnitPrice, decimal PurchaseUnitPrice)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal = new LogginDAL(ConnString);


            String strOrganization = String.Empty;
            Xero.Api.Core.Model.Item DeltoneItem = null;
            String strvalstatus = String.Empty;
            try
            {
                X509Certificate2 privateCertificate = new X509Certificate2(@"C:\DeltoneCRM\DeltoneCRM\SSLCert\public_privatekey.pfx", "@dmin123");
                var private_app_api = new XeroCoreApi("https://api.xero.com", new PrivateAuthenticator(privateCertificate),
                new Consumer("AKS4LTBIHGV9NHNIKDKZI3GCFOYTLQ", "6KMAWJ07MI01UGLNQ7G3RJ3NLCLYSU"), null,
                new DefaultMapper(), new DefaultMapper());

                var code = strItemCode;
                var item = private_app_api.Create(new Xero.Api.Core.Model.Item
                {
                    Code = code,
                    Description = strItemDescription,
                    SalesDetails = new SalesDetails
                    {
                        AccountCode = "200",

                        UnitPrice = (decimal)SalesUnitPrice
                    },
                    PurchaseDetails = new PurchaseDetails
                    {
                        AccountCode = "200",
                        UnitPrice = (decimal)PurchaseUnitPrice

                    }
                });

                if (item.ValidationStatus == Xero.Api.Core.Model.Status.ValidationStatus.Error)
                {
                    foreach (var message in item.Errors)
                    {
                        Console.WriteLine("Validation Error: " + message.Message);
                        dal.WriteTOLog(0, message.Message.ToString(), "XeroIntergration.cs", "CreateItem", "ERROR");
                    }
                    DeltoneItem = null;
                }
                else
                {
                    DeltoneItem = (Xero.Api.Core.Model.Item)item;
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.StackTrace.ToString() + ":" + ex.Message.ToString());
                dal.WriteTOLog(0, ex.StackTrace.ToString() + ":" + ex.Message.ToString(), "XeroIntergration.cs", "CreateItem", "ERROR");
                DeltoneItem = null;
            }

            return DeltoneItem;
        }

        /*This Function Update an Exsisting Item in the Xero System*/
        public Xero.Api.Core.Model.Item UpdateanItem(String strItemCode, String strGuid, String strDescription, Decimal salesUnitPrice, Decimal PurchaseUnitPrice)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal = new LogginDAL(ConnString);

            Xero.Api.Core.Model.Item updateItem = null;
            XeroCoreApi api_private = CreateAPI();
            var code = strItemCode;
            var item = api_private.Update(new Xero.Api.Core.Model.Item
            {
                Id = new Guid(strGuid),
                Code = code,
                Description = strDescription,
                SalesDetails = new SalesDetails
                {
                    AccountCode = "200",

                    UnitPrice = (decimal)salesUnitPrice
                },
                PurchaseDetails = new PurchaseDetails
                {
                    AccountCode = "200",
                    UnitPrice = (decimal)PurchaseUnitPrice

                }
            });

            if (item.ValidationStatus == Xero.Api.Core.Model.Status.ValidationStatus.Error)
            {
                foreach (var message in item.Errors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                    //Write to Log OrderID=0
                    dal.WriteTOLog(0, message.Message.ToString(), "XeroIntergration.cs", "UpdateanItem", "ERROR");
                }
                updateItem = null;
            }
            else
            {
                updateItem = (Xero.Api.Core.Model.Item)item;
            }

            return updateItem;
        }
        /*End Function Update an Item*/



        #endregion ITEM

        #region CREDIT_NOTES

        /*ADD a Credit Note in the XERO System with Allocation*/
        public XeroApi.Model.CreditNote CreateCreditNote(Repository res, String strContactGuid, String strReference, String strCreditItems, List<DeltoneItem> CreditItems)
        {

            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal = new LogginDAL(ConnString);

            if (res == null)
            {
                return null;
            }


            XeroApi.Model.LineItems LItems = new XeroApi.Model.LineItems();
            XeroApi.Model.LineItem LItem;


            #region CreditNote Line Items



            foreach (DeltoneItem item in CreditItems)
            {
                LItem = new XeroApi.Model.LineItem();
                LItem.Description = item.ItemDescription;
                LItem.Quantity = item.Qty;
                LItem.UnitAmount = item.UnitPrice;
                LItem.TaxType = "OUTPUT";
                LItem.AccountCode = "200";

                LItems.Add(LItem);

            }
            /*
            LItem.Description="Services Rendered";
            LItem.Quantity=1;
            LItem.UnitAmount=1;
            LItem.TaxType="OUTPUT";
            LItems.Add(LItem);
           */

            #endregion

            #region supplierCost

            #endregion

            #region Promoitem

            #endregion


            XeroApi.Model.CreditNote credit_note = new XeroApi.Model.CreditNote
            {

                Type = "ACCRECCREDIT",
                Contact = res.FindById<XeroApi.Model.Contact>(new Guid(strContactGuid)),
                Date = DateTime.Today,
                LineAmountTypes = XeroApi.Model.LineAmountType.Inclusive,
                Reference = strReference,
                LineItems = LItems,
            };

            var createdCreditNote = res.Create(credit_note);

            //Check wether Validation Errors Exsists or Not 
            if (createdCreditNote.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in createdCreditNote.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                }
                return null;
            }

            return (XeroApi.Model.CreditNote)createdCreditNote;

        }



        //This Method Authorize the Credit Note  in the  Xero System
        public XeroApi.Model.CreditNote AuthorizeCreditNote(Repository res, String strGuid, String strReference)
        {
            if (res == null)
            {
                return null;
            }


            XeroApi.Model.CreditNote credit_note = new XeroApi.Model.CreditNote
            {

                Type = "ACCRECCREDIT",
                CreditNoteID = new Guid(strGuid),
                Status = "AUTHORISED",
                LineAmountTypes = XeroApi.Model.LineAmountType.Inclusive,
                Reference = strReference,



            };

            var createdCreditNote = res.UpdateOrCreate(credit_note);

            //Check wether Validation Errors Exsists or Not 
            if (createdCreditNote.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in createdCreditNote.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                }
                return null;
            }

            return (XeroApi.Model.CreditNote)createdCreditNote;

        }


        //This Method Update the Credit Note in the Xero System
        public XeroApi.Model.CreditNote UpdateCreditNote(Repository res, String strCreditNoteGuid, String strReference, List<DeltoneItem> CreditItems)
        {

            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal = new LogginDAL(ConnString);

            if (res == null)
            {
                return null;
            }


            XeroApi.Model.LineItems LItems = new XeroApi.Model.LineItems();
            XeroApi.Model.LineItem LItem;


            #region CreditNote Line Items



            foreach (DeltoneItem item in CreditItems)
            {
                LItem = new XeroApi.Model.LineItem();
                LItem.Description = item.ItemDescription;
                LItem.Quantity = item.Qty;
                LItem.UnitAmount = item.UnitPrice;
                LItem.TaxType = "OUTPUT";
                LItem.AccountCode = "200";
                LItems.Add(LItem);

            }

            #endregion

            XeroApi.Model.CreditNote credit_note = new XeroApi.Model.CreditNote
            {

                Type = "ACCRECCREDIT",
                CreditNoteID = new Guid(strCreditNoteGuid),
                //Contact = res.FindById<XeroApi.Model.Contact>(new Guid(strContactGuid)),
                //Date = DateTime.Today,
                LineAmountTypes = XeroApi.Model.LineAmountType.Inclusive,
                Reference = strReference,
                LineItems = LItems,
            };

            var createdCreditNote = res.UpdateOrCreate(credit_note);

            //Check wether Validation Errors Exsists or Not 
            if (createdCreditNote.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in createdCreditNote.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                }
                return null;
            }

            return (XeroApi.Model.CreditNote)createdCreditNote;

        }


        //This method Cancel(DELETE/VOID) credit note in xero system
        public XeroApi.Model.CreditNote CancelCreditNote(int CreditNoteID, Repository repos, String strCurrentStatus, String CreditNoteGuid)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            LogginDAL dal_login = new LogginDAL(ConnString);
            //Get the Xero Status of the CreditNote and determine what action to be taken.
            String xeroStatus = strCurrentStatus.Equals("APPROVED") ? "VOIDED" : "DELETED";

            if (repos == null)
            {
                return null;
            }


            //Fetch the credit note from Xero using LINQ

            /*
            XeroApi.Model.CreditNote creditNote =( from cn in repos.CreditNotes
                                                  where cn.CreditNoteNumber == strCreditNoteNumber
                                                  select cn).First();

            CreditNoteGuid = creditNote.CreditNoteID.ToString();
             */




            XeroApi.Model.CreditNote delCreditNote = new XeroApi.Model.CreditNote
            {
                Type = "ACCRECCREDIT",
                CreditNoteID = new Guid(CreditNoteGuid),
                LineAmountTypes = XeroApi.Model.LineAmountType.Inclusive,
                Status = xeroStatus
            };

            var DeltedCreditNote = repos.UpdateOrCreate(delCreditNote);

            if (DeltedCreditNote.ValidationStatus == XeroApi.Model.ValidationStatus.ERROR)
            {
                foreach (var message in DeltedCreditNote.ValidationErrors)
                {
                    Console.WriteLine("Validation Error: " + message.Message);
                    Log_Details(CreditNoteID, message.Message.ToString(), "XeroIntergration.cs", "CancelCreditNote", dal_login, "ERROR");//WRITE TO LOG
                }
                return null;
            }
            else
            {
                Log_Details(CreditNoteID, "CANCEL CREDIT NOTE SUCCESS MSG", String.Empty, String.Empty, dal_login, "SUCCESS");//SUCCESS MSG
            }
            return ((XeroApi.Model.CreditNote)DeltedCreditNote);
        }


        #endregion CREDIT_NOTES

        #region SUPPLIERS

        /*Create a Supplier(As a Contact) in the XERO System*/
        public void CreateSupplier(Repository res)
        {
        }


        #endregion

        #region Code_Snippets

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
                * //UnitPrice = 25.00m
                */


        //if (item.Id != Guid.Empty && item.Code == code)
        //{
        //  Console.Write("Item Sucessfully Created");

        //}
        //else //Validation Error Occures
        //{

        #endregion Code_Snippets

        #region Pruchase Order

        /// <summary>
        /// create Invoice in Xero System Type=AccountsPayable
        /// </summary>
        /// <returns></returns>
        public XeroApi.Model.Invoice CreatePurchaseInvoice()
        {
            XeroApi.Model.Invoice pur_Invoice = new XeroApi.Model.Invoice();


            return pur_Invoice;
        }

        /// <summary>
        /// Update purchase Invoice in Xero System
        /// </summary>
        /// <returns></returns>
        public XeroApi.Model.Invoice UpdatePurchaseInvoice()
        {
            XeroApi.Model.Invoice purInvoice_Update = new XeroApi.Model.Invoice();


            return purInvoice_Update;
        }


        /// <summary>
        /// Delete Purhcase Invoice in Xero System
        /// </summary>
        /// <returns></returns>
        public XeroApi.Model.Invoice DeletePurchaseInvoice()
        {
            XeroApi.Model.Invoice purInvoice_delete = new XeroApi.Model.Invoice();

            return purInvoice_delete;
        }



        #endregion

        /*This Method Log the Details in DeltoneCRM  LOG  */
        public void Log_Details(int OrderID, String Message, String filename, String MethodName, LogginDAL dal, String result)
        {
            if (dal.Rows_Exsists(OrderID))
            {
                dal.UpdateLog(OrderID, Message, filename, MethodName, result);
            }
            else
            {
                dal.WriteTOLog(OrderID, Message, filename, MethodName, result);
            }
        }



        #endregion
    }

}