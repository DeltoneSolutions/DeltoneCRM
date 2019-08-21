using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltoneCRM
{   
    public class DeltoneCompany
    {
       public String CompanyName { get; set; }
       public  String CompanyWebSite { get; set; }
       public  String AccountOwner { get; set; }

       public  String FirstName { get; set; }
       public  String LastName { get; set; }
       public  String DefaultAreaCode { get; set; }
       public  String DefaultNumber { get; set; }
       public  String MobileNumber { get; set; }
       public String EmailAddress { get; set; }
        
        /*Shipping Address*/
       public  String ShippingAddressLine1{ get; set; }
       public String ShippingAddresssLine2 { get; set; }
       public String ShippingAddCity { get; set; }
       public String ShippingAddState { get; set; }
       public String ShippingAddPostCode { get; set; }

        /*Billing Address*/
       public String BillingAddressLine1 { get; set; }
       public String BillingAddressLine2 { get; set; }
       public String BillingAddPostCode { get; set; }
       public String BillingAddCity { get; set; }
       public String BillingAddState { get; set; }

       public String IsPrimary { get; set; }

    }
}