using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using DeltoneCRM_DAL;
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
using DeltoneCRM_DAL;


namespace DeltoneCRM.Manage.Process
{
    public partial class Process_EditContact : System.Web.UI.Page
    {

        String strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
        XeroIntergration xero;

        protected void Page_Load(object sender, EventArgs e)
        {
            xero = new XeroIntergration();
            ContactDAL contactdal = new ContactDAL(strConnString);
            CompanyDAL companydal = new CompanyDAL(strConnString);

            int ContactID = Int32.Parse(Request.Form["ContactID"].ToString());
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
            String ActINact = Request.Form["ActInact"].ToString();

            string finalActInact = "";

            if (ActINact == "false")
            {
                finalActInact = "N";
            }
            else
            {
                finalActInact = "Y";
            }

            String finalPrimaryContact = "";

            if (PrimaryContact == "true")
            {
                finalPrimaryContact = "Y";
            }
            else
            {
                finalPrimaryContact = "N";
            }

            var userId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            String str = contactdal.UpdateContact(ContactID, FirstName, LastName, DefaultAreaCode,
                DefaultNumber, MobileNumber, EmailAddy, ShipLine1, ShipLine2, ShipCity, ShipState,
                ShipPostcode, BillLine1, BillLine2, BillCity, BillState, BillPostcode, finalPrimaryContact, finalActInact, userId);

            if (Int32.Parse(str) > 0)
            {
                //Ge the XeroGuid and Update the Details in Xero
                String contactGuid = contactdal.getXeroGuid_ForContact(ContactID);
                if (!String.IsNullOrEmpty(contactGuid) && !(DBNull.Value.Equals(contactGuid)))
                {
                    //Fetch the CompanyName for the Contact
                    String strCompanyName = companydal.GetCompanybyContactID(ContactID);
                    String strDefaultCountryCode = "+61"; //Should have entries in AddContactUI
                    String strDefaultCountry = "Australia"; //Should have entries in AddContactUI


                    Repository res = xero.CreateRepository();//Create the Repository
                    XeroApi.Model.Contact delContact = xero.UpdateContact(res, contactGuid, strCompanyName, FirstName, LastName, DefaultAreaCode, strDefaultCountryCode, DefaultNumber, String.Empty, String.Empty, String.Empty, String.Empty, strDefaultCountryCode, MobileNumber, EmailAddy, ShipLine1, ShipCity, strDefaultCountry, ShipPostcode, ShipState, BillLine1, BillCity, strDefaultCountry, ShipPostcode, BillState);
                    if (delContact != null)
                    {

                    }

                   // Repository res = xero.CreateRepository();
                   // XeroApi.Model.Contact delContact = xero.CreateContact(res, strCompanyName, FirstName, LastName, DefaultAreaCode, strDefaultCountryCode, DefaultNumber, String.Empty, String.Empty, String.Empty, String.Empty, strDefaultCountryCode, MobileNumber, EmailAddy, ShipLine1, ShipCity, strDefaultCountry, ShipPostcode, ShipState, BillLine1, BillCity, strDefaultCountry, BillPostcode, BillState);
                    //if (delContact != null)
                   // {
                        //Update the ContactGuid in the DataBase Table 
                    //    contactdal.UpdateWithXeroDetails(23538, delContact.ContactID.ToString());
                   // }

                }
                else
                {
                    //Entry hasn't been created in the Xero System .Create the Entry and Update the Tables
                }

            }


            Response.Write(str);









        }
    }
}