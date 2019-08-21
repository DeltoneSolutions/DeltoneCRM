using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Web.Configuration;
using System.Configuration;
using DeltoneCRM_DAL;

namespace DeltoneCRM.Manage.ViewEditScreens
{
    public partial class ViewEditSupplier : System.Web.UI.Page
    {

        int SuppID;
        String connString;

        protected void Page_Load(object sender, EventArgs e)
        {

            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            if (!String.IsNullOrEmpty(Request.QueryString["suppid"]))
            {
                SuppID = Int32.Parse(Request.QueryString["suppid"].ToString());
               // Populate(SuppID);
                LoadSupplierDetails(SuppID);
            }

        }
        protected void Populate(int SuppID)
        {

            SupplierDAL suppdal = new SupplierDAL(connString);
            String supplier = suppdal.getSupplier(SuppID);
            String[] arrSupplier = supplier.Split(':');

            //Popualte the Items
            NewItem.Value = arrSupplier[1].ToString();

            double deliverycost = Convert.ToDouble(arrSupplier[2].ToString());
            NewCost.Value = deliverycost.ToString("0.00");
            hdnEditSupplierID.Value = arrSupplier[0].ToString();

            if (arrSupplier[3].ToString().Trim().Equals("N"))
            {
                activechkbox.Checked = true;
            }
            else
            {
                activechkbox.Checked = false;
                string strScript = "<script language='javascript'>$(document).ready(function (){$('#activechkbox').toggleCheckedState(true);});</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }

        }

        private void LoadSupplierDetails(int suppID)
        {
            DeltoneCRMDAL dal = new DeltoneCRMDAL();
            hdnEditSupplierID.Value = suppID.ToString();
            var suppObj = dal.SupplierIdById(suppID);
            NewItem.Value = suppObj.SupplierName;
            NewCost.Value = suppObj.StandardDeliveryCost;
            suppaddress.Value = suppObj.Address;
            suppcity.Value = suppObj.City;
            suppstate.Value = suppObj.State;
            supppostcode.Value = suppObj.PostCode;
            suppsalesemail.Value = suppObj.SalesEmail;
            suppreturnemail.Value = suppObj.ReturnEmail;
            suppphonenumber.Value = suppObj.PhoneNumber;
            suppaccountPhonenumber.Value = suppObj.AccountsPhoneNumber;
            suppusername.Value = suppObj.UserName;
            supppassword.Value = suppObj.Password;
            contactnamesupp.Value = suppObj.ContactName;
            contactnameemail.Value = suppObj.AccountsEmail;
            suppnotes.Value = suppObj.Notes;

            string strScript = "<script language='javascript'>$(document).ready(function (){$('#activechkbox').toggleCheckedState(true);});</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);


        }



    }
}