using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.Collections;
using XeroApi.Model;
using XeroApi.Model.Reporting;
using XeroApi;
using DeltoneCRM_DAL;

//XERO INTERGRATION LIB BLOCK

using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Logging;
using DevDefined.OAuth.Storage.Basic;
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

//END XERO INTERGRATION LIB BLOCK

namespace DeltoneCRM
{
    public partial class CreateCreditNote : System.Web.UI.Page
    {
        static String ORDERID = String.Empty;
        static String CREDITNOTE_ID = String.Empty;

        String CONTACTID = String.Empty;
        String COMPNAYID = String.Empty;
        static String CREDIT_NOTE_REASON = String.Empty;
        CreditNotesDAL creditNoteDAL;
        ProItemDAL prodal;
        CompanyDAL companydal = new CompanyDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        OrderDAL orddal = new OrderDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);
        static String CONNSTRING;

        protected void Page_Load(object sender, EventArgs e)
        {
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            creditNoteDAL = new CreditNotesDAL(strConnectionString);
            prodal = new ProItemDAL(strConnectionString);

            if (!String.IsNullOrEmpty(Request.QueryString["ContactID"]))
            {
                CONTACTID = Request.QueryString["ContactID"].ToString();
                String strCreditNoteContact = creditNoteDAL.FetchContactDetails(Int32.Parse(CONTACTID));
                String[] arrContact = strCreditNoteContact.Split(':');
                ContactInfo.InnerHtml = arrContact[0].ToString();
                CompanyInfo.InnerHtml = arrContact[3].ToString();
                BillingAddress.InnerHtml = arrContact[1].ToString();
                hdnCreditNoteContactDetails.Value = strCreditNoteContact;
            }
            if (!String.IsNullOrEmpty(Request.QueryString["CompanyID"]))
            {
                COMPNAYID = Request.QueryString["CompanyID"].ToString();
                //Create the Session for the CompanyID
                Session["OpenedCompanyID"] = COMPNAYID;
                ACCOUNT_OWNER_ID.Value = companydal.getCompanyOwnershipAdminID(COMPNAYID);
                ACCOUNT_OWNER_TXT.Value = companydal.getCompanyAccountOwner(Int32.Parse(COMPNAYID));

            }

            if (!String.IsNullOrEmpty(Request.QueryString["OrderID"]))
            {
                ORDERID = Request.QueryString["OrderID"].ToString();
                dtsnumber.InnerText = creditNoteDAL.getXeroOrderNumber(ORDERID);
                hdnEditOrder.Value = creditNoteDAL.getOrderDetails(ORDERID.ToString());
                String[] arr_Order = creditNoteDAL.getOrderDetails(ORDERID.ToString()).Split(':');
                OrderNumber.InnerHtml = "Order:" + arr_Order[3].ToString();
                hdnEditOrderItems.Value = creditNoteDAL.getOrderItemsbyOrderId(ORDERID.ToString(), strConnectionString);
                PROCOST.Value = prodal.getPromotionlCost(Int32.Parse(ORDERID));
                PopulateDropDownList(Int32.Parse(COMPNAYID), Int32.Parse(ORDERID));
                hdnORDERID.Value = Request.QueryString["OrderID"].ToString();
                COMMISH_SPLIT.Value = orddal.getCommishSplitBoolean(Int32.Parse(Request.QueryString["OrderID"].ToString()));

                SALESPERON_ID.Value = orddal.getSalespersonByID(Int32.Parse(Request.QueryString["OrderID"].ToString()));

                SALESPERSON_TXT.Value = orddal.getSalespersonName(Int32.Parse(Request.QueryString["OrderID"].ToString()));

            }
        }

        protected void PopulateDropDownList(int CompanyID, int OrderID)
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins", conn);
                adapter.Fill(subjects);
                ddlUsersList.DataSource = subjects;
                ddlUsersList.DataTextField = "FullName";
                ddlUsersList.DataValueField = "LoginID";
                ddlUsersList.DataBind();
            }

            //Get the Current Account OwnerShip
            //String strAccountOwnerShip = CompanyOwnerShip(CompanyID);
            //String[] arr = strAccountOwnerShip.Split(':');
            //String firstname = arr[0];
            //String ownershipid = arr[1];

            String OrderOwner = orddal.getOrderCreatedBy(OrderID);


            ddlUsersList.Items.FindByText(OrderOwner).Selected = true;


        }

        //Create the List of Creadit  Note Items 
        protected List<DeltoneItem> CreateCreaitItems(String strCreditItems)
        {

            List<DeltoneItem> CreditItems = new List<DeltoneItem>();
            DeltoneItem item;
            String[] arrcreditItems = strCreditItems.Split('|');
            String[] line;

            for (int i = 0; i < arrcreditItems.Length; i++)
            {
                if (!String.IsNullOrEmpty(arrcreditItems[i]))
                {

                    line = arrcreditItems[i].Split(',');

                    if (line[0].Contains("D/L Handling")) //If this is Delivery Handling Line 
                    {
                        item = new DeltoneItem();
                        item.ItemDescription = line[0].ToString();
                        item.Qty = 1;
                        item.COG = 0;
                        item.UnitPrice = Convert.ToDecimal(line[4].ToString());
                        CreditItems.Add(item);

                    }
                    else
                    {
                        item = new DeltoneItem();
                        item.ItemDescription = line[0].ToString();
                        item.Qty = Int32.Parse(line[3].ToString());
                        item.COG = Convert.ToDecimal(line[2].ToString());
                        item.UnitPrice = Convert.ToDecimal(line[4].ToString());
                        item.SupplierCode = line[1].ToString();

                        CreditItems.Add(item);
                    }

                }
            }


            return CreditItems;
        }

        /// <summary>
        /// This method creates Credit Note Items
        /// </summary>
        /// <param name="strCreditItems"></param>
        /// <param name="CreditNoteID"></param>
        /// <param name="crDAL"></param>
        /// <param name="CreatedBy"></param>
        protected void CreateCreditNoteItems(String strCreditItems, int CreditNoteID,
            CreditNotesDAL crDAL, String CreatedBy, int companyId, string noteString = "")
        {
            String[] arrcreditItems = strCreditItems.Split('|');
            String[] line;
            string noteItemsString = "";
            for (int i = 0; i < arrcreditItems.Length; i++)
            {
                if (!String.IsNullOrEmpty(arrcreditItems[i]))
                {
                    line = arrcreditItems[i].Split(',');
                    int RowsEffected = -1;

                    if (line[0].Contains("D/L Handling")) //If this is Delivery Handling Line 
                    {
                        RowsEffected = crDAL.CreateCreditNoteItems(CreditNoteID, line[0].ToString(), 1,
                            float.Parse(line[4].ToString()), 0, String.Empty, CreatedBy, "delivery");

                        noteItemsString = " Note Items : Description " + line[1].ToString() + ", Created By " + CreatedBy + " , SupplierName " + "delivery" + " ";

                    }

                    else
                    {
                        String description = line[0].ToString();
                        String SupplierCode = line[1].ToString();
                        float COGPrice = float.Parse(line[2].ToString());
                        int Qty = Int32.Parse(line[3].ToString());
                        float UnitPrice = float.Parse(line[4].ToString());
                        //Modification done here 
                        String SupplierName = line[5].ToString();

                        RowsEffected = crDAL.CreateCreditNoteItems(CreditNoteID, description, Qty, UnitPrice, COGPrice, SupplierCode, CreatedBy, SupplierName);
                        if (noteItemsString == "")

                            noteItemsString = " Note Items : Description " + line[1].ToString() + ", Created By " + CreatedBy + " , SupplierName " + SupplierName + " ";
                        else
                            noteItemsString = noteItemsString + " Description " + line[1].ToString() + ", Created By " + CreatedBy + " , SupplierName " + SupplierName + " ";

                    }


                }
            }

            var columnName = "CreditNote And CreditNote Items All columns";
            var talbeName = "CreditNote And CreditNote Items";
            var ActionType = "Created";
            int primaryKey = CreditNoteID;

            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var newvalues = noteString + noteItemsString;

            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionstring;

            new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(companyId));

        }



        protected void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            String strDelCost = String.Empty;
            float Total = 0; ;
            float SubTotal = 0;
            float SplitVolume = 0;
            String strCreditItems = String.Empty;
            String strCreatedBy = String.Empty;

            String ACCOUNT_OWNER_ID_VALUE = ACCOUNT_OWNER_ID.Value; ;
            String ACCOUNT_OWNER_TXT_VALUE = ACCOUNT_OWNER_TXT.Value;

            String SALESPERSON_ID_VALUE = SALESPERON_ID.Value;
            String SALESPERSON_TXT_VALUE = SALESPERSON_TXT.Value;

            String CommishSplit = COMMISH_SPLIT.Value;

            if (CommishSplit == "1")
            {
                SplitVolume = float.Parse(VOLUME_SPLIT_AMOUNT.Value);
            }
            else
            {
                SplitVolume = 0;
            }


            if (!String.IsNullOrEmpty(Session["LoggedUser"] as String))
            {
                strCreatedBy = Session["LoggedUser"].ToString();
            }

            if (!String.IsNullOrEmpty(CusDelCostItems.Value))
            {
                strDelCost = CusDelCostItems.Value.ToString();
            }

            if (!String.IsNullOrEmpty(hdnTotal.Value))
            {
                Total = float.Parse(hdnTotal.Value);
            }

            if (!String.IsNullOrEmpty(hdnSubTotal.Value))
            {
                SubTotal = float.Parse(hdnSubTotal.Value);
            }

            if (!String.IsNullOrEmpty(OrderItems.Value))
            {
                strCreditItems = OrderItems.Value.ToString();
            }

            if (COMMISH_SPLIT.Value == "1")
            {
                SplitVolume = float.Parse(VOLUME_SPLIT_AMOUNT.Value);
            }
            else
            {
                SplitVolume = 0;
            }

            String CreditNoteReason = txtCreditReason.Text;
            String Notes = OrderNotes.Text;
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            creditNoteDAL = new CreditNotesDAL(strConnectionString);
            ContactDAL cdal = new ContactDAL(strConnectionString);

            String strGuid = cdal.getXeroGuid_ForContact(Int32.Parse(CONTACTID));

            ///Create the  XERO Entry  first 
            XeroIntergration xero = new XeroIntergration();
            Repository res = xero.CreateRepository();

            List<DeltoneItem> cr_items = CreateCreaitItems(strCreditItems);
            XeroApi.Model.CreditNote deltone_CreditNote = xero.CreateCreditNote(res, strGuid, String.Empty, String.Empty, cr_items);

            //CreditNoteReason= txtCreditReason.Text;
            CreditNoteReason = TYPEOFCREDIT.Value;

            //Get Customer delcost items and suppleir delcost items given by OrderID
            OrderDAL ordal = new OrderDAL(CONNSTRING);
            String CustomerDelItems = ordal.getCustomerDelItems(Int32.Parse(ORDERID));
            String SuppDelItems = ordal.getSupplierDelItems(Int32.Parse(ORDERID));
            var creditNoteMainString = "";
            if (deltone_CreditNote != null)
            {
                //Write  to Deltone DB  Modified here 
                if (creditNoteDAL.CreateCreditNote(Int32.Parse(COMPNAYID), Int32.Parse(CONTACTID), Total,
                    strDelCost, strCreatedBy, Int32.Parse(ORDERID), "PENDING", CreditNoteReason, Notes,
                    SuppDelItems, CustomerDelItems, SubTotal, SplitVolume, ACCOUNT_OWNER_ID_VALUE,
                    ACCOUNT_OWNER_TXT_VALUE, SALESPERSON_ID_VALUE, SALESPERSON_TXT_VALUE, CommishSplit) > 0)
                {
                    var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    var companyName = new CompanyDAL(connectionstring).getCompanyNameByID(COMPNAYID);
                   
                    int CreatedCreditNoteID = creditNoteDAL.FetchLastCreatedCreaditNoteID(strCreatedBy);
                    creditNoteMainString = " CreditNote  Id : " + CreatedCreditNoteID
                       + " Created By :" + strCreatedBy + " Notes :" + Notes + " Credit Reason " + CreditNoteReason + " Status: PENDING ";
                    if (!String.IsNullOrEmpty(strCreditItems))
                    {
                        CreateCreditNoteItems(strCreditItems, CreatedCreditNoteID, creditNoteDAL, strCreatedBy, Convert.ToInt32(COMPNAYID), creditNoteMainString);
                    }

                    // Create entries for the RMAs
                    creditNoteDAL.BuildRMAList(ORDERID, CreatedCreditNoteID.ToString());

                    //Update  the Deltone Table With the Xero Entry
                    creditNoteDAL.UpdateWithXeroEntry(CreatedCreditNoteID, deltone_CreditNote.CreditNoteID.ToString(), deltone_CreditNote.CreditNoteNumber.ToString());

                    //Add ORder Supplier Notes as Type 'Credit'
                    Dictionary<String, String> di_notes = ordal.getOrderSupplierNotes(Int32.Parse(ORDERID));

                    //ordal.CreateSupplerNotes(Int32.Parse(ORDERID), deltone_CreditNote.CreditNoteNumber.ToString(), di_notes);
                    creditNoteDAL.CreateSupplerNotes(CreatedCreditNoteID, deltone_CreditNote.CreditNoteNumber.ToString(), di_notes);

                    #region Commission_Calculation

                    CreditNotesDAL cnDal = new CreditNotesDAL(CONNSTRING);
                    float AccountOwnerCommission = 0;
                    float SalespersonCommission = 0;

                    AccountOwnerCommission = float.Parse(ACCOUNT_OWNER_COMMISH.Value);
                    AccountOwnerCommission = -AccountOwnerCommission;

                    if (SALESPERSON_COMMISH.Value != "")
                    {
                        SalespersonCommission = float.Parse(SALESPERSON_COMMISH.Value);
                        SalespersonCommission = -SalespersonCommission;
                    }



                    if (COMMISH_SPLIT.Value == "0")
                    {
                        cnDal.SplitCommission(CreatedCreditNoteID, Int32.Parse(COMPNAYID), Int32.Parse(ACCOUNT_OWNER_ID.Value), AccountOwnerCommission, "PENDING");
                    }
                    else
                    {
                        cnDal.SplitCommission(CreatedCreditNoteID, Int32.Parse(COMPNAYID), Int32.Parse(ACCOUNT_OWNER_ID.Value), AccountOwnerCommission, "PENDING");
                        cnDal.SplitCommission(CreatedCreditNoteID, Int32.Parse(COMPNAYID), Int32.Parse(SALESPERON_ID.Value), SalespersonCommission, "PENDING");
                    }

                    /*
                    commission = float.Parse(hdnCommision.Value.ToString());
                    commission = -commission;



                    
                    if (Session["LoggedUserID"] != null)
                    {


                        cnDal.SplitCommission(CreatedCreditNoteID, Int32.Parse(COMPNAYID), Int32.Parse(Session["LoggedUserID"].ToString()), commission, "PENDING");
                    }*/

                    #endregion  Commission_Calculation

                    ///Dialog  BOX  Stating Success Message and Redirection

                    String Message = "<h2>CREDIT NOTE: " + deltone_CreditNote.CreditNoteNumber + " HAS BEEN CREATED</h2>";
                    String NavigateUrl = "CreditNotes/AllCreditNotes.aspx";
                    String PrintURl = String.Empty;
                    string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
                    //Response.Redirect("CreditNotes/AllCreditNotes.aspx");
                }
            }
            else
            {
                //Couldnt  Write in to Xero Message with  Dialog BOX
                String Message = "<h2>ERROR OCCURED CREATING CREDIT NOTE</h2>";
                String NavigateUrl = "CreditNotes/AllCreditNotes.aspx";
                String PrintURl = String.Empty;
                string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);

            }


        }


    }
}