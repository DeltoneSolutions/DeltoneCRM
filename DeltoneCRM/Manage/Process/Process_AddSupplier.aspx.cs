using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DeltoneCRM.Manage.Process
{
    public partial class Process_AddSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Response.Write(Request.Form["SupplierName"].ToString() + ":" + Request.Form["DeliveryCost"].ToString());
            
            if (!String.IsNullOrEmpty(Request.Form["SupplierName"])  && !String.IsNullOrEmpty(Request.Form["DeliveryCost"]))
            {
                String strSupplierName = Request.Form["SupplierName"].ToString();
                float deliveryCost =float.Parse(Request.Form["DeliveryCost"].ToString());

                String ContactName = Request.Form["ContactName"].ToString();
                String PhoneNumber = Request.Form["PhoneNumber"].ToString();
                String Address = Request.Form["Address"].ToString();
                String City = Request.Form["City"].ToString();
                String State = Request.Form["State"].ToString();
                String PostCode = Request.Form["PostCode"].ToString();
                String SalesEmail = Request.Form["SalesEmail"].ToString();
                String ReturnEmail = Request.Form["ReturnEmail"].ToString();
                String AccountsPhoneNumber = Request.Form["AccountsPhoneNumber"].ToString();
                String AccountsEmail = Request.Form["AccountsEmail"].ToString();
                String Notes = Request.Form["Notes"].ToString();
                String UserName = Request.Form["UserName"].ToString();
                String Password = Request.Form["Password"].ToString();

                var supplierObj = new DeltoneCRM.DeltoneCRMDAL.SupplierObj()
                {
                    StandardDeliveryCost=Request.Form["DeliveryCost"].ToString(),
                    SupplierName=Request.Form["SupplierName"].ToString(),
                    ContactName = ContactName,
                    PhoneNumber = PhoneNumber,
                    Address = Address,
                    City = City,
                    State = State,
                    PostCode = PostCode,
                    SalesEmail=SalesEmail,
                    ReturnEmail=ReturnEmail,
                    AccountsPhoneNumber=AccountsPhoneNumber,
                    AccountsEmail=AccountsEmail,
                    Notes=Notes,
                    UserName=UserName,
                    Password=Password
                    

                };

                DeltoneCRMDAL dal = new DeltoneCRMDAL();
                dal.AddNewSupplier(supplierObj);
               // Response.Write(dal.AddNewSupplier(strSupplierName,deliveryCost));
                Response.Write(1);
              
            }
             



        }
    }
}