using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Modified here SK Write Xero Contact
using System.Data.Sql;
using System.Data.SqlClient;
using DeltoneCRM_DAL;
using System.Configuration;
using System.Data.SqlTypes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Collections;
using XeroApi.Model;
using XeroApi.Model.Reporting;
using XeroApi;
using XeroApi.OAuth;
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



namespace DeltoneCRM.Manage
{
    public partial class Process_AddContact : System.Web.UI.Page
    {
        public String CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        ContactDAL contactdal;
        XeroIntergration xero;


        protected void Page_Load(object sender, EventArgs e)
        {

            contactdal = new ContactDAL(CONNSTRING);
            xero = new XeroIntergration();

            int CompID = Int32.Parse(Request.Form["CompID"].ToString());
            String FirstName = Request.Form["NewFirstName"].ToString();
            String LastName = Request.Form["NewLastName"].ToString();
            String DefaultAreaCode = Request.Form["NewDefaultAreaCode"].ToString();
            String DefaultNumber = Request.Form["NewDefaultNumber"].ToString();
            String MobileNumber = Request.Form["NewMobileNumber"].ToString();
            String EmailAddy = Request.Form["NewEmailAddy"].ToString();
            String ShipLine1 = Request.Form["NewShipLine1"].ToString();
            String ShipLine2 = Request.Form["NewShipLine2"].ToString();
            String ShipCity = Request.Form["NewShipCity"].ToString();
            String ShipState = Request.Form["NewShipState"].ToString();
            String ShipPostcode = Request.Form["NewShipPostcode"].ToString();
            String BillLine1 = Request.Form["NewBillLine1"].ToString();
            String BillLine2 = Request.Form["NewBillLine2"].ToString();
            String BillCity = Request.Form["NewBillCity"].ToString();
            String BillState = Request.Form["NewBillState"].ToString();
            String BillPostcode = Request.Form["NewBillPostcode"].ToString();
            String PrimaryContact = Request.Form["PrimaryContact"].ToString();


            String finalPrimaryContact = "";

            if (PrimaryContact == "true")
            {
                finalPrimaryContact = "Y";
            }
            else
            {
                finalPrimaryContact = "N";
            }

            DeltoneCRMDAL dal = new DeltoneCRMDAL();
           // Response.Write(dal.AddNewContact(CompID, FirstName, LastName, DefaultAreaCode, DefaultNumber, MobileNumber, EmailAddy, ShipLine1, ShipLine2, ShipCity, ShipState, ShipPostcode, BillLine1, BillLine2, BillCity, BillState, BillPostcode, finalPrimaryContact));
            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
          String str=  dal.AddNewContact(CompID, FirstName, LastName, DefaultAreaCode, DefaultNumber, MobileNumber, EmailAddy, ShipLine1, ShipLine2,
              ShipCity, ShipState, ShipPostcode, BillLine1, BillLine2, BillCity, BillState, BillPostcode, finalPrimaryContact, loggedInUserId);
          if (!String.IsNullOrEmpty(str))
          {
              String[] arr=str.Split(':');
              String contactID = arr[1].ToString();
              String companyname = arr[2].ToString();
              String companywebsite = arr[3].ToString();

              String strDefaultCountryCode="+61"; //Should have entries in AddContactUI
              String strDefaultCountry = "Australia"; //Should have entries in AddContactUI

              //Create the Repository

              Repository res = xero.CreateRepository();
              XeroApi.Model.Contact delContact = xero.CreateContact(res, companyname, FirstName, LastName, DefaultAreaCode, strDefaultCountryCode, DefaultNumber, String.Empty, String.Empty, String.Empty, String.Empty, strDefaultCountryCode, MobileNumber, EmailAddy, ShipLine1, ShipCity, strDefaultCountry, ShipPostcode, ShipState, BillLine1, BillCity, strDefaultCountry, BillPostcode, BillState);
              if (delContact != null)
              {
                  //Update the ContactGuid in the DataBase Table 
                  contactdal.UpdateWithXeroDetails(CompID, delContact.ContactID.ToString());
              }
              else
              {
              }
                 
              Response.Write(str);

          }




        }
    }
}