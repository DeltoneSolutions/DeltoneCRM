﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DeltoneCRM_DAL;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace DeltoneCRM.Manage
{
    public partial class edit_contact : System.Web.UI.Page
    {
        String ContactID = String.Empty;
        String strConnString;

        protected void Page_Load(object sender, EventArgs e)
        {
            strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            if (!String.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                ContactID = Request.QueryString["cid"].ToString();
                Populate(ContactID);
            }
        }

        protected void Populate(String strContactID)
        {
            ContactDAL contactdal = new ContactDAL(strConnString);
            String strItem = contactdal.getContact(Int32.Parse(strContactID));
            String[] arrItem = strItem.Split(':');

            NewContactID.Value = strContactID;
            NewFirstName.Value = arrItem[0].ToString();
            NewLastName.Value = arrItem[1].ToString();
            NewDefaultAreaCode.Value = arrItem[2].ToString();
            NewDefaultNumber.Value = arrItem[3].ToString();
            NewMobileNumber.Value = arrItem[4].ToString();
            NewEmailAddy.Value = arrItem[5].ToString();
            NewShipLine1.Value = arrItem[6].ToString();
            NewShipLine2.Value = arrItem[7].ToString();
            NewShipCity.Value = arrItem[8].ToString();
            NewShipPostcode.Value = arrItem[9].ToString();

            if (NewShipState.Items.FindByValue(arrItem[10].ToString()) != null)
            {
                NewShipState.ClearSelection();
                NewShipState.Items.FindByValue(arrItem[10].ToString()).Selected = true;
            }
           
            //NewShipState.Value = arrItem[10].ToString();
            NewBillLine1.Value = arrItem[11].ToString();
            NewBillLine2.Value = arrItem[12].ToString();
            NewBillCity.Value = arrItem[13].ToString();
            NewBillPostcode.Value = arrItem[14].ToString();
            if (!string.IsNullOrEmpty(arrItem[15].ToString()))
            {
                if (NewBillState.Items.FindByValue(arrItem[15].ToString()) != null)
                {
                    NewBillState.ClearSelection();
                    NewBillState.Items.FindByValue(arrItem[15].ToString()).Selected = true;
                }
            }
           // NewBillState.Value = arrItem[15].ToString();

            if (arrItem[16].ToString().Trim().Equals("YES"))
            {
                PrimaryContactchkbox.Checked = true;
            }

            if (arrItem[17].ToString().Trim().Equals("N"))
            {
                activechkbox.Checked = true;
            }
            else
            {
                activechkbox.Checked = false;
                string strScript = "<script language='javascript'>$(document).ready(function (){$('#activechkbox').toggleCheckedState(true);});</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }


            //Populate the DropDown List with the Selected Value

        }
    }
}