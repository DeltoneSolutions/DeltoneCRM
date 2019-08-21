using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using DeltoneCRM_DAL;


namespace DeltoneCRM.Manage.Process
{
    public partial class ProcessEditSupplier : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SupplierDAL suppdal = new SupplierDAL(connString);
            int SuppID = Int32.Parse(Request.Form["suppid"].ToString());
            String suppname = Request.Form["suppname"].ToString();
            float deliverycost =float.Parse(Request.Form["deliverycost"].ToString());
            string ActInact = Request.Form["ActInact"].ToString();

            string finalActInact = "";

            if (ActInact == "false")
            {
                finalActInact = "N";
            }
            else
            {
                finalActInact = "Y";
            }

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
                SupplierID=SuppID,
                StandardDeliveryCost = Request.Form["deliverycost"].ToString(),
                SupplierName = Request.Form["suppname"].ToString(),
                ContactName = ContactName,
                PhoneNumber = PhoneNumber,
                Address = Address,
                City = City,
                State = State,
                PostCode = PostCode,
                SalesEmail = SalesEmail,
                ReturnEmail = ReturnEmail,
                AccountsPhoneNumber = AccountsPhoneNumber,
                AccountsEmail = AccountsEmail,
                Notes = Notes,
                UserName = UserName,
                Password = Password


            };

            var userLoggedin=Session["LoggedUser"].ToString();
            DeltoneCRMDAL dal = new DeltoneCRMDAL();
            dal.UpdateSupplierName(supplierObj, userLoggedin);
            Response.Write(1);
           // Response.Write(suppdal.UpdateSupplier(SuppID,suppname,deliverycost, finalActInact));

        }
    }
}