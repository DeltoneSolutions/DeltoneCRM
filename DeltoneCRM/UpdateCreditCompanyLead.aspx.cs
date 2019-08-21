using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
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
using DeltoneCRM_DAL;
//XERO INTERGRATION RELATED LIBS
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
using System.Text.RegularExpressions;
using System.Text;

namespace DeltoneCRM
{
   

    public partial class UpdateCreditCompanyLead : System.Web.UI.Page
    {
        String strPendingApproval = "";
        String strLoggedUserProfile = "Admin";
        String CreditStatus = String.Empty;
        static String str_ORDERID;
        static int ORDERID;
        static int CONTACTID;
        static int COMPANYID;
        static String COMPANYNAME;
        static String XeroCreditNoteID;
        static String connString;
        static String REL_ORDERID; //Order ID Related to the Credit Note

        SupplierDAL suppdal;
        OrderDAL orderdal;
        PurchaseDAL purchasedal;
        TempSuppNotes tempsuppdal;
        CreditNotesDAL creditnotedal;
        CompanyDAL companydal;
        //Modification done here
        static String ORDERSTATUS = String.Empty;
        //This is from XeroIntegration.cs for debugging purposes
        private const String consumerKey = ""; //Put those in Web  Config
        private const String userAgnetString = "";//Put Those in Web Config;
        XeroApi.OAuth.XeroApiPrivateSession xSession;
        XeroApi.Repository repository;
        protected Repository Repos = null;
        XeroCoreApi api_User; //API USER ACCORDING TO THE SKINNY WRAPPER
        // END
        protected Guid id;
        static String ConnString = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            String CompanyID = String.Empty;
            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ConnString = connString;
            //DAL OBJECTS INSTANTIATION
            suppdal = new SupplierDAL(connString);
            orderdal = new OrderDAL(connString);
            purchasedal = new PurchaseDAL(connString);
            tempsuppdal = new TempSuppNotes(connString);
            creditnotedal = new CreditNotesDAL(connString);
            companydal = new CompanyDAL(connString);
            //set the All Supplers from the begining 
            hdnAllSuppliers.Value = suppdal.getAllSuppliers();

            if (!String.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                String CustID = Request.QueryString["cid"].ToString();
                hdnContactID.Value = CustID;
                getCustomerDetails(CustID);
                //Set the ContactID
                CONTACTID = Int32.Parse(CustID);
            }
            if (!String.IsNullOrEmpty(Request.QueryString["Compid"]))
            {
                CompanyID = Request.QueryString["Compid"].ToString();
                hdnCompanyID.Value = CompanyID;
                Session["OpenedCompanyID"] = CompanyID;//Set the Session for CompanyID
                COMPANYID = Int32.Parse(CompanyID);
                AccountOwnertxt.Text = companydal.getCompanyAccountOwner(COMPANYID);
                //Set CompanyID

                COMPANYNAME = orderdal.getCompanyName(COMPANYID);

            }

            if (!String.IsNullOrEmpty(Request.QueryString["CreditNoteID"]))
            {
                tdOrderCreateDate.Style.Value = "display:block;";
                datecreated.Style.Value = "display:block";
                str_ORDERID = Request.QueryString["CreditNoteID"].ToString();
                int OrderID = Int32.Parse(Request.QueryString["CreditNoteID"].ToString());
                ORDERID = Int32.Parse(Request.QueryString["CreditNoteID"].ToString()); //Assign Credit Note ID
                hdnORDERID.Value = Request.QueryString["CreditNoteID"].ToString();

                //Set the Hidden Credit Note ID
                hdnCreditNoteID.Value = Request.QueryString["CreditNoteID"].ToString();
                //Set the Order Title 
                OrderTitle.InnerHtml = "EDIT CREDIT NOTE: " + orderdal.getXeroOrderID(OrderID);
                OrderTitle.Style.Value = "display:block;";
                //Get CreditNote  Items  by Credit NoteID
                String strOrderedItems = getOrderItemsbyOrderID(OrderID); //GET CREDIT NOTE ITEMS
                //Set the Hiden Value
                hdnEditOrderItems.Value = strOrderedItems;

                var allSupp = creditnotedal.GetSuppliersForCreditNoteById(str_ORDERID);
                if (!IsPostBack)
                {
                    foreach (var item in allSupp)
                    {
                        suppliernameDropRMAEmail.Items.Add(new System.Web.UI.WebControls.ListItem(item, item));
                        DropDownListRMAEdit.Items.Add(new System.Web.UI.WebControls.ListItem(item, item));
                        suppliernameDropRMAEmailCustomer.Items.Add(new System.Web.UI.WebControls.ListItem(item, item));
                        rmaPobxCustomerDrop.Items.Add(new System.Web.UI.WebControls.ListItem(item, item));
                    }
                }

                dtsnumber.InnerText = creditnotedal.getXeroInvoiceNumberFromCreditID(OrderID);

                String strSupplierNotes = FetchSupplierNotes(OrderID);
                String strPromotionalItems = FetchProItems(OrderID);
                String strOrder = getOrderbyOrderID(OrderID); //Fetch the Credit Note
                String strReference = FetchReferenceInfo(OrderID);
                String strTypeOfCall = FetchTypeOfCall(OrderID);
                String strPaymentTerms = String.Empty; //Modified Add Payment Terms
                String XeroGuid = String.Empty;

                CreditStatus = creditnotedal.getCurrentCreditNoteStatus(OrderID);

                hdnCommishSplit.Value = creditnotedal.getCommishSplit(OrderID);
                ACCOUNT_OWNER_ID.Value = creditnotedal.getAccountOwnerID(OrderID);
                ACCOUNT_OWNER.Value = creditnotedal.getAccountOwner(OrderID);

                SALESPERSON_ID.Value = creditnotedal.getSalespersonID(OrderID);
                SALESPERSON.Value = creditnotedal.getSalesperson(OrderID);

                if (!String.IsNullOrEmpty(strOrder))
                {
                    hdnEditOrder.Value = strOrder; //Set Credit Note values for JQuery Access
                    //Set the Order Status here 
                    String[] arr = strOrder.Split(':');
                    //Modification done add order create date
                    String OrderCreateDate = arr[3].ToString();
                    XeroGuid = arr[1].ToString();

                    XeroCreditNoteID = arr[0].ToString(); //Xero Credit Note Number
                    ORDER_CREATE_DATE.Value = OrderCreateDate;//Modification done here

                    datecreated.Value = OrderCreateDate;

                    //Make APPROVE Button Invisible
                    String status = arr[4].ToString();
                    if (status.Equals("APPROVED"))
                    {
                        btnInvoiceApprove.Visible = false;
                    }
                    if (!status.Equals("PENDING"))
                    {
                        if (Session["USERPROFILE"].ToString().Equals("ADMIN"))
                        {
                            var credId = Convert.ToInt32(Request.QueryString["CreditNoteID"].ToString());
                            if (GetCreditNoteHouse(credId) == false)
                                ConverttoInhousebutton.Visible = true;
                        }
                    }


                    REL_ORDERID = arr[7].ToString(); //Fetch the Relevant Order Realted  the CreditNote
                }

                if (!String.IsNullOrEmpty(strSupplierNotes))
                {
                    hdnEditSupplietNotes.Value = strSupplierNotes; //Credit Note Supplier Notes 
                }

                //Set Hidden Field Value
                if (!IsPostBack)
                {
                    // ddlPaymentTerms.Text=strPaymentTerms;
                }

                //Set Type of Credit  field value
                if (!IsPostBack && !String.IsNullOrEmpty(strReference))
                {
                    RefID.Value = strReference;
                    if (strReference == "FAULTY - NOT REG" ||
                        strReference == "FAULTY - PRINT QUALITY" || strReference == "FAULTY - DAMAGED" || strReference == "FAULTY GOODS")
                    {
                        ButtonCreatePopFaulty.Visible = true;

                        faultygoodsTr.Visible = true;

                        rmafaultyDaetils.Text = SetFaultyTypeData(OrderID);

                        if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                        {
                            if (Session["USERPROFILE"].ToString().Equals("ADMIN"))
                            {
                                ButtonCustomerSentEmail.Visible = true;
                                ButtonRequestRMA.Visible = true;
                            }
                        }
                    }



                    if (strReference == "WRONG GOODS - DELTONE FAULT" ||
                        strReference == "WRONG GOODS - SUPPLIER FAULT"
                        || strReference == "WRONG GOODS" || strReference == "WRONG GOODS - CUSTOMER FAULT")
                    {
                        if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                        {
                            if (Session["USERPROFILE"].ToString().Equals("ADMIN"))
                            {
                                ButtonRequestRMA.Visible = true;
                                ButtonCustomerSentEmail.Visible = true;
                                //rmafaultyDaetils.Text = SetFaultyTypeData(OrderID);
                            }
                        }



                    }
                    if (strReference == "CHANGED PRINTER"
                        || strReference == "NO AUTH"
                        || strReference == "DID NOT ORDER")
                    {
                        if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                        {
                            if (Session["USERPROFILE"].ToString().Equals("ADMIN"))
                            {
                                ButtonChangePrinterAddress.Visible = true;
                                ButtonRequestRMA.Visible = true;
                            }
                        }
                    }
                    if (strReference == "PRICE REDUCTION")
                    {
                    }

                }

                //Set Type Of Call DropDown Value
                if (!IsPostBack && !String.IsNullOrEmpty(strTypeOfCall))
                {
                    dllTypeOfCall.Text = strTypeOfCall;
                }

                if (!String.IsNullOrEmpty(strPromotionalItems))
                {
                    hdnEditproitpems.Value = strPromotionalItems;
                }

                strPendingApproval = "PendingApproval";
                //btnOrderSubmit.Text = "Approve Order";
                btnCloseOrderWindow.Style.Value = "display:none";

                //Check the Session here if Only ADMiN Display Approve BUTTON
                if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                {
                    if (Session["USERPROFILE"].ToString().Equals("ADMIN") && !(String.IsNullOrEmpty(XeroGuid)))
                    {
                        btnInvoiceApprove.Style.Value = "display:inline";
                    }
                }

                //Populate the DropDown List 
                PopulateDropDownList(Int32.Parse(CompanyID), Int32.Parse(str_ORDERID));

            }



            //Order for approval 
            if (!String.IsNullOrEmpty(Request.QueryString["Action"]))
            {
                if (Request.QueryString["Action"].ToString().Equals("PendingApproval"))
                {

                }
            }

            if (strLoggedUserProfile.Equals("Admin") && (strPendingApproval.Equals("PendingApproval")))
            {

            }
        }

        protected void btnAddCreditNote_Click(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// Generate PDF Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            //Get Company and Client Details
            CreditNotesDAL cndal = new CreditNotesDAL(ConnString);
            Dictionary<String, String> dinotes = cndal.getNotesObject(ORDERID);
            Dictionary<String, String> di_suppitems = cndal.getPurchaseItemObject(ORDERID);
            //Generate Supplier Docs for Credit Notes
            GenerateSupplierDocs(ORDERID, XeroCreditNoteID, CONTACTID.ToString(), di_suppitems, dinotes);

        }

        /// <summary>
        /// Button Credit Note Submit Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(hdnTotal.Value))  //Use A Another Way
            {
                return;
            }
            /*Credit note Submit Click Event  handler*/

            String strCompanyID = hdnCompanyID.Value;
            String strContactID = hdnContactID.Value;
            String strTest = hdnProfit.Value;
            String OrderNotesStr = OrderNotes.Text.ToString();
            String PaymentTermsStr = ddlPaymentTerms.Text.ToString();
            //String Reference = ReferenceFld.Text.ToString();
            String Reference = RefID.Value.ToString();
            String TypeOfCall = dllTypeOfCall.Text.ToString();
            float COGTotal = 0;
            float CogSubTotal = 0;
            float SplitVolume = 0;

            float profitTotal = 0;
            float profitSubTotal = 0;
            float SuppDelCost = 0;

            float CusDelCost = 0;
            float ProItemCost = 0;

            String ACCOUNT_OWNER_ID_VALUE = ACCOUNT_OWNER_ID.Value; ;
            String ACCOUNT_OWNER_TXT_VALUE = ACCOUNT_OWNER.Value;

            String SALESPERSON_ID_VALUE = SALESPERSON_ID.Value;
            String SALESPERSON_TXT_VALUE = SALESPERSON.Value;

            String CommishSplit = hdnCommishSplit.Value;

            if (CommishSplit == "1")
            {
                SplitVolume = float.Parse(VOLUME_SPLIT_AMOUNT.Value);
            }
            else
            {
                SplitVolume = 0;
            }

            CreditNotesDAL cndal = new CreditNotesDAL(ConnString);

            if (!String.IsNullOrEmpty(hdnCOGTotal.Value))
            {
                COGTotal = float.Parse(hdnCOGTotal.Value);
            }

            if (!String.IsNullOrEmpty(hdnCOGSubTotal.Value))
            {
                CogSubTotal = float.Parse(hdnCOGSubTotal.Value);
            }

            if (!String.IsNullOrEmpty(hdnTotal.Value))
            {
                profitTotal = float.Parse(hdnTotal.Value);
            }
            if (!String.IsNullOrEmpty(hdnSubTotal.Value))
            {
                profitSubTotal = float.Parse(hdnSubTotal.Value);
            }

            if (!String.IsNullOrEmpty(hdnSupplierDeliveryCost.Value))
            {
                SuppDelCost = float.Parse(hdnSupplierDeliveryCost.Value);
            }

            if (!String.IsNullOrEmpty(hdnCustomerDeliveryCost.Value))
            {
                CusDelCost = float.Parse(hdnCustomerDeliveryCost.Value);
            }
            if (!String.IsNullOrEmpty(hdnProCost.Value))
            {
                ProItemCost = float.Parse(hdnProCost.Value);
            }

            //Logged user 
            String strCreatedBy = String.Empty;

            if (!String.IsNullOrEmpty(Session["LoggedUser"] as String))
            {
                strCreatedBy = Session["LoggedUser"].ToString();
            }

            String strProitems = hdnProItems.Value.ToString();
            String strSuppDelItems = hdnSupplierDelCostItems.Value.ToString();//Get Supplier Delivery Cost for the Credit Note
            String strCustDelCostItems = CusDelCostItems.Value.ToString();//Get Customer Delivery Cost for the Credit Note

            String[] strOrderItems = OrderItems.Value.Split('|');
            String[] itemList;

            //Modified Here Add Supplier Notes here 30/04/2015
            String strSuppNotes = String.Empty;
            String strOwnerShipAdminID = String.Empty;

            if (!String.IsNullOrEmpty(hdnSupplierNotes.Value))
            {
                strSuppNotes = hdnSupplierNotes.Value;
            }
            //End Modification Supplier Notes

            int intPaymentTerms = Int32.Parse(ddlPaymentTerms.SelectedValue.ToString());


            strOwnerShipAdminID = ddlUsers.SelectedItem.Text;




            String PROITEMS = String.Empty;
            if (!String.IsNullOrEmpty(hdnPromotionalItems.Value))
            {
                PROITEMS = hdnPromotionalItems.Value;
            }
            //End Promotional Items NOT IN USE 

            //Credit Note Supplier List
            ArrayList alSuppliers = new ArrayList();

            //Credit Note Purchase List 
            Dictionary<String, String> di = new Dictionary<string, String>();

            #region CreditNote_SupplierNotes

            ////Refactor this code in to a seperate function 

            Dictionary<String, String> di_Notes = new Dictionary<String, String>();

            String[] notes;
            notes = strSuppNotes.Split('|');
            String[] noteLine;

            for (int k = 0; k < notes.Length; k++)
            {
                if (!String.IsNullOrEmpty(notes[k]))
                {
                    noteLine = notes[k].Split(',');
                    //Add it to the dictionary Object
                    if (k == 0)
                    {

                        di_Notes.Add(noteLine[0], noteLine[1]);
                    }
                    else
                    {
                        di_Notes.Add(noteLine[1], noteLine[2]);
                    }
                }
            }

            #endregion CreditNote_SupplierNotes

            //Xero Connection for Credit Note Handling
            XeroIntergration xero = new XeroIntergration();

            #region create_CreditNote_PurchaseOrderList

            //Refactor this code snippet and make it as a seperate function
            for (int i = 0; i < strOrderItems.Length; i++)
            {
                if (!String.IsNullOrEmpty(strOrderItems[i]))
                {
                    itemList = strOrderItems[i].Split(',');

                    //Add Supplier Name to the arrayList
                    if (i == 0) //For the First item
                    {
                        //Supplier Code,Qty,COG
                        String strLine = itemList[2] + ":" + itemList[4] + ":" + itemList[3];
                        //String supplierName = itemList[6].Trim();
                        //Modified here Change SupplierName Array Item
                        String supplierName = itemList[7].Trim();

                        if (di.ContainsKey(supplierName))
                        {
                            //Append the Line to the Line 
                            di[supplierName] = di[supplierName] + "|" + strLine;
                        }
                        else //Doesn't contain the Key
                        {
                            di.Add(supplierName, strLine);
                        }
                    }
                    if (i != 0) //Subsquent Items 
                    {
                        //Supplier Code,Qty,COG
                        String strLine = itemList[3] + ":" + itemList[5] + ":" + itemList[4];
                        //String supplierName = itemList[7].Trim();
                        //Modified  here Change Supplier Name Array Item
                        String supplierName = itemList[8].Trim();

                        if (di.ContainsKey(supplierName))
                        {
                            //Append the Line to the Line 
                            di[supplierName] = di[supplierName] + "|" + strLine;
                        }
                        else //Doesn't contains the key
                        {
                            di.Add(supplierName, strLine);
                        }
                    }
                }
            }

            #endregion



            string orderItemsString = "";
            string newvalues = "";


            //CREDIT NOTE UPDATION
            if (!String.IsNullOrEmpty(Request.QueryString["CreditNoteID"]))
            {
                int Orderid = Int32.Parse(Request.QueryString["CreditNoteID"].ToString());
                if (DeleteOrderItems(Orderid, out orderItemsString) > 0)
                {
                    //Modified here Add customer delcost items ,supp delcost items
                    if (UpdateCreditNoteDB(Orderid, strCompanyID, strContactID, strOwnerShipAdminID,
                        OrderNotes.Text, strCustDelCostItems, strSuppDelItems, profitTotal, profitSubTotal, SplitVolume) > 0) //Add Profit Total as Total
                    {
                        newvalues = " CreditNote Id " + Orderid + " ,CreditNote Note " + OrderNotes.Text;


                        //Insert New Order Items 
                        for (int i = 0; i < strOrderItems.Length; i++)
                        {
                            itemList = strOrderItems[i].Split(',');
                            if (strOrderItems[i] != String.Empty)
                            {
                                if (i == 0)
                                {
                                    int firstItem = CreateOrderItems(Orderid, itemList[1], itemList[0],
                                        float.Parse(itemList[5]), Int32.Parse(itemList[4]), float.Parse(itemList[3]),
                                        strCreatedBy, itemList[2], itemList[6]);
                                    orderItemsString = orderItemsString + " Newly created Order Items : Description " + itemList[1] + ", Created By " + strCreatedBy +
                                    ", Quantity " + itemList[4].ToString() +
                                   " , SupplierName " + itemList[6] + " ";
                                }
                                //Rest of the Elements
                                if (i != 0)
                                {
                                    int Item = CreateOrderItems(Orderid, itemList[2], itemList[1],
                                        float.Parse(itemList[6]), Int32.Parse(itemList[5]), float.Parse(itemList[4]),
                                        strCreatedBy, itemList[3], itemList[7]);
                                    orderItemsString = orderItemsString + " Order Items : Description " + itemList[2] + ", Created By " + strCreatedBy +
                                   ", Quantity " + itemList[5].ToString() +
                                  " , SupplierName " + itemList[7] + " ";
                                }

                            }

                        }

                    }

                    //create the Creadit Note  Items List
                    List<DeltoneItem> Credit_ItemList = CreatCreditNoteItems(strOrderItems);

                    #region Add promotional Items(Updation) NOT IN USE

                    #endregion

                    #region  UpdateSplitCommision_CreditNote

                    CreditNotesDAL cnDal = new CreditNotesDAL(ConnString);
                    float AccountOwnerCommission = 0;
                    float SalespersonCommission = 0;

                    AccountOwnerCommission = float.Parse(ACCOUNT_OWNER_COMMISH.Value);
                    AccountOwnerCommission = -AccountOwnerCommission;

                    if (SALESPERSON_COMMISH.Value != "")
                    {
                        SalespersonCommission = float.Parse(SALESPERSON_COMMISH.Value);
                        SalespersonCommission = -SalespersonCommission;
                    }


                    String output = creditnotedal.RemoveCommisionEntry_Credit(Orderid);

                    if (hdnCommishSplit.Value == "0")
                    {
                        cnDal.SplitCommission(Orderid, Int32.Parse(strCompanyID), Int32.Parse(ACCOUNT_OWNER_ID.Value), AccountOwnerCommission, CreditStatus);
                    }
                    else
                    {
                        cnDal.SplitCommission(Orderid, Int32.Parse(strCompanyID), Int32.Parse(ACCOUNT_OWNER_ID.Value), AccountOwnerCommission, CreditStatus);
                        cnDal.SplitCommission(Orderid, Int32.Parse(strCompanyID), Int32.Parse(SALESPERSON_ID.Value), SalespersonCommission, CreditStatus);
                    }

                    #endregion UpdateSplitCommision_CreditNote


                    #region purchase  Items Updation for Supplier Docs(Credit Notes)
                    cndal.UpdatePurhcaseItems(Orderid, di, Session["LoggedUser"].ToString());
                    #endregion

                    #region XeroEntry Updation with the Status DRAFT

                    Repository Res = xero.CreateRepository();
                    String deltoneInvoice_Number = Orderid.ToString();
                    String deltoneInvoice_ID = String.Empty;
                    //String InvoiceReference = ReferenceFld.Text;
                    String InvoiceReference = RefID.Value;

                    connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    orderdal = new OrderDAL(connString);

                    //Fetch the Xero Guid from CRM Table
                    //String xeroGuid = orderdal.getOrderGuid(Orderid);

                    String xeroGuid = getOrderGuid(Orderid);

                    if (!String.IsNullOrEmpty(xeroGuid)) //If Xero Entry is there
                    {
                        XeroApi.Model.CreditNote UpdateCreditNote = xero.UpdateCreditNote(Res, xeroGuid, String.Empty, Credit_ItemList);

                        if (UpdateCreditNote != null)
                        {
                            deltoneInvoice_Number = UpdateCreditNote.CreditNoteNumber.ToString();
                            deltoneInvoice_ID = UpdateCreditNote.CreditNoteID.ToString();
                        }

                    }
                    else
                    {

                    }

                    //Update Supplier Notes(Credit Notes)
                    cndal.UpdateSupplierNotes(Orderid, deltoneInvoice_Number, di_Notes);

                    var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                    var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = connectionstring;

                    var columnName = "CreditNote Table And CreditNote Items All columns";
                    var talbeName = "CreditNote And CreditNote Items";
                    var ActionType = "Updated CreditNote And CreditNoteItems";
                    int primaryKey = Orderid;
                    var lastString = newvalues + orderItemsString;

                    new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", lastString, loggedInUserId, conn, 0,
           columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));

                    if (ORDERSTATUS.Equals("APPROVED"))
                    {

                    }

                    #endregion

                    String Message = "<h2>CREDIT  NOTE: " + deltoneInvoice_Number + " HAS BEEN EDITED</h2>";
                    String NavigateUrl = "CreditNotes/AllCreditNotes.aspx";
                    String PrintURl = "PrintOrder.aspx?Oderid=" + Orderid + "&cid=" + strContactID + "&Compid=" + strCompanyID;
                    string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);

                }

            }


            #region Credit Note Creation
            else  //// Not IN USE ////
            {
                String strOrderStatus = String.Empty;
                if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                {
                    strOrderStatus = "PENDING";
                }
                //End Checking User Profile


                //Modified here Add User Defined Date
                if (CreateOrder(strCompanyID, strContactID, COGTotal, CogSubTotal, profitTotal, profitSubTotal, SuppDelCost, CusDelCost, ProItemCost, strCreatedBy, strSuppDelItems, strProitems, strCustDelCostItems, strOrderStatus, DateTime.Today, strOwnerShipAdminID, OrderNotesStr, PaymentTermsStr, Reference, TypeOfCall, Convert.ToDateTime(datereceived.Value)) > 0)
                {
                    int OrderID = LastOrderID(strCreatedBy);
                    for (int i = 0; i < strOrderItems.Length; i++)
                    {
                        itemList = strOrderItems[i].Split(',');
                        if (strOrderItems[i] != String.Empty)
                        {
                            //For the First Element
                            if (i == 0)
                            {
                                int firstItem = CreateOrderItems(OrderID, itemList[1], itemList[0], float.Parse(itemList[5]), Int32.Parse(itemList[4]), float.Parse(itemList[3]), strCreatedBy, itemList[2], itemList[6]);
                            }
                            //Rest of the Elements
                            if (i != 0)
                            {
                                int Item = CreateOrderItems(OrderID, itemList[2], itemList[1], float.Parse(itemList[6]), Int32.Parse(itemList[5]), float.Parse(itemList[4]), strCreatedBy, itemList[3], itemList[7]);

                            }
                        }
                    }


                    #region WriteCommssions

                    float commission = float.Parse(hdnCommision.Value.ToString());
                    if (Session["LoggedUserID"] != null)
                    {
                        SplitCommission(OrderID, Int32.Parse(strCompanyID), Int32.Parse(Session["LoggedUserID"].ToString()), commission);
                    }
                    #endregion

                    #region AddPro Items
                    if (!String.IsNullOrEmpty(PROITEMS))
                    {
                        CreateProItems(OrderID, PROITEMS);
                    }
                    #endregion

                    #region PurchaseOrder List

                    foreach (var pair in di)
                    {

                        //Modified here Check pair.Key is Empty Check the Supplier 


                        purchasedal.InsertPurchaseItem(OrderID, pair.Key, pair.Value, Session["LoggedUser"].ToString());
                    }

                    #endregion PurchaseOrder List


                    #region XeroEntry Creation with Status(DRAFT)

                    Repository res = xero.CreateRepository();
                    //Repository res = CreateRepository();
                    String strContact = getContactDetailsForInvoice(Int32.Parse(strContactID));
                    String[] arrContact = strContact.Split(':');
                    //String InvReference = ReferenceFld.Text;
                    String InvReference = RefID.Value;

                    XeroApi.Model.Invoice delinvoice = xero.CreateInvoice(OrderID, res, COMPANYNAME, strOrderItems, intPaymentTerms, Convert.ToDecimal(CusDelCost), PROITEMS, InvReference, Convert.ToDateTime(datereceived.Value)); //with Status DRAFT

                    //Create the Supplier Notes if Exsists
                    if (delinvoice != null)
                    {
                        CreateSupplerNotes(OrderID, delinvoice.InvoiceNumber.ToString(), di_Notes);
                    }
                    else
                    {
                        CreateSupplerNotes(OrderID, String.Empty, di_Notes);
                    }



                    #endregion

                    //String deltoneInvoice_Number = OrderID.ToString();
                    String deltoneInvoice_Number = String.Empty;
                    String deltoneInvoice_ID = String.Empty;
                    String Message = String.Empty;

                    if (delinvoice != null)
                    {
                        deltoneInvoice_Number = delinvoice.InvoiceNumber.ToString();
                        deltoneInvoice_ID = delinvoice.InvoiceID.ToString();
                        updateOrderStatus(deltoneInvoice_Number, deltoneInvoice_ID, Int32.Parse(OrderID.ToString()));
                    }

                    Message = (!deltoneInvoice_Number.Equals(String.Empty)) ? "<h2>ORDER: " + deltoneInvoice_Number + " SUCCESSFULLY CREATED</h2>" : "COULD NOT WRITE TO XERO.";
                    String NavigateUrl = "Orders/AllOrders.aspx";
                    String PrintURl = "PrintOrder.aspx?Oderid=" + OrderID + "&cid=" + strContactID + "&Compid=" + strCompanyID;
                    string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
                }
                else
                {

                    String Message = "<h2>Error Creating Order</h2>";
                    String NavigateUrl = "Orders/AllOrders.aspx";
                    String PrintURl = String.Empty;
                    string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);

                }
            }
            #endregion creation

        }


        /// <summary>
        /// Creates the Credit Item Notes List
        /// </summary>
        /// <param name="strOrderItems"></param>
        /// <returns></returns>
        public List<DeltoneItem> CreatCreditNoteItems(String[] strOrderItems)
        {

            List<DeltoneItem> CreditItems = new System.Collections.Generic.List<DeltoneItem>();
            DeltoneItem new_Item;
            String[] itemList;

            for (int i = 0; i < strOrderItems.Length; i++)
            {
                itemList = strOrderItems[i].Split(',');

                if (strOrderItems[i] != String.Empty)
                {
                    if (i == 0)
                    {
                        new_Item = new DeltoneItem();
                        new_Item.ItemDescription = itemList[1];
                        new_Item.Qty = Int32.Parse(itemList[4]);
                        new_Item.SupplierCode = itemList[2];
                        new_Item.SupplierName = itemList[6];
                        new_Item.UnitPrice = Convert.ToDecimal(itemList[5]);
                        new_Item.COG = Convert.ToDecimal(itemList[3]);

                        CreditItems.Add(new_Item);

                    }
                    //Rest of the Elements
                    if (i != 0)
                    {

                        new_Item = new DeltoneItem();
                        new_Item.ItemDescription = itemList[2];
                        new_Item.Qty = Int32.Parse(itemList[5]);
                        new_Item.SupplierCode = itemList[3];
                        new_Item.SupplierName = itemList[7];
                        new_Item.UnitPrice = Convert.ToDecimal(itemList[6]);
                        new_Item.COG = Convert.ToDecimal(itemList[4]);

                        CreditItems.Add(new_Item);

                    }

                }

            }

            return CreditItems;
        }

        protected void btnCloseOrderWindow_Click(object senderr, EventArgs e)
        {
            string strScript = "<script language='javascript'>$(document).ready(function (){ CLOSE_ORDERDIALOG(); });</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }

        /// <summary>
        /// Creates popup dialog for NEW ORDER,QUOTE,EDIT ORDER SUBMISSION
        /// </summary>
        /// <param name="strMessage"></param>
        /// <param name="Navigateurl"></param>
        /// <param name="PrintUrl"></param>
        /// <param name="contactID"></param>
        /// <param name="CompanyID"></param>
        protected void CreateUIDialog(String strMessage, String Navigateurl, String PrintUrl, String contactID, String CompanyID)
        {
            string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + strMessage + "','" + Navigateurl + "','" + PrintUrl + "'); });</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }

        #region Document Generation

        /// <summary>
        /// generate Customer Invoice save it in the Disk
        /// </summary>
        /// <param name="strInvoiceID"></param>
        /// <param name="strContactID"></param>
        /// <param name="ProfitTotal"></param>
        /// <param name="ProfitSubTotal"></param>
        /// <param name="arrInvoiceItems"></param>
        /// <param name="CustomerDelCost"></param>
        protected void GenrateCustomerInvoice(String strInvoiceID, String strContactID, float ProfitTotal, float ProfitSubTotal, String[] arrInvoiceItems, float CustomerDelCost)
        {
            //Retive Contact Related Details
            String strContact = getContactDetailsForInvoice(Int32.Parse(strContactID));
            //Split the Contact 
            String[] arrContact = strContact.Split(':');
            //Generate the PDF
            float gstAMount = ProfitTotal - ProfitSubTotal;
            GeneratePDf(strInvoiceID, String.Empty, arrContact[0], arrContact[1], arrContact[2], CustomerDelCost, arrInvoiceItems, ProfitTotal, ProfitSubTotal, gstAMount);

        }

        /// <summary>
        /// Generate Invoice for suppliers and save it in the Disk
        /// </summary>
        /// <param name="strInvoiceID"></param>
        /// <param name="COGToal"></param>
        /// <param name="COGSubTotal"></param>
        /// <param name="strInvoiceItems"></param>
        /// <param name="SuppDelCost"></param>
        /// <param name="ProItemCost"></param>
        protected void GenerateSupplierInvoice(String strInvoiceID, float COGToal, float COGSubTotal, String[] strInvoiceItems, float SuppDelCost, float ProItemCost)
        {

            //For Each Items in the Order Generate Seperate PDfs for Suppliers 
        }


        /// <summary>
        /// get contact details for the Invoice
        /// </summary>
        /// <param name="ContactID">contact id </param>
        /// <returns></returns>
        protected String getContactDetailsForInvoice(int ContactID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //String strSqlOrderStmt = "select * from dbo.Contacts where ContactID=" + ContactID;
            String strSqlOrderStmt = "select  cp.CompanyName,cn.* from dbo.Contacts cn,dbo.Companies cp where cn.CompanyID=cp.CompanyID and cn.ContactID=" + ContactID;


            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        String conFullName = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                        String conAddress = sdr["STREET_AddressLine1"].ToString() + " " + sdr["STREET_AddressLine2"].ToString() + Environment.NewLine + sdr["STREET_City"] + Environment.NewLine + sdr["STREET_Region"] + " " + sdr["STREET_PostalCode"] + Environment.NewLine + sdr["STREET_Country"].ToString();
                        String conEmail = sdr["Email"].ToString();
                        String CompanyName = sdr["CompanyName"].ToString();

                        strContact = strContact + conFullName + ":" + conAddress + ":" + conEmail + ":" + CompanyName;
                    }
                }
            }
            conn.Close();

            return strContact;
        }


        /// <summary>
        /// generate supplier Docs 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="strInvoiceID"></param>
        /// <param name="strContactID"></param>
        /// <param name="map"></param>
        /// <param name="map_Notes"></param>
        protected void GenerateSupplierDocs(int OrderID, String strInvoiceID, String strContactID, Dictionary<string, string> map, Dictionary<String, String> map_Notes)
        {
            //Retrive the Contact Details
            String strContact = getContactDetailsForInvoice(Int32.Parse(strContactID));
            //Split the Contact
            String[] arrContact = strContact.Split(':');
            String strConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SupplierDAL supdal = new SupplierDAL(strConnString);

            foreach (var pair in map)
            {
                var Supplier = pair.Key;
                var ItemList = pair.Value;
                String[] Lines = ItemList.Split('|');

                //String strDelCost = supdal.getDelCostBySupplierName(Supplier, OrderID); //NOT IN USE instructed by Deltone Sol
                String strDelCost = String.Empty;
                CreateSupplierPDF(Supplier, strInvoiceID, arrContact[3], arrContact[0], arrContact[1], arrContact[2], Lines, map_Notes, strDelCost);
            }

        }

        /// <summary>
        /// Create Supplier PDF
        /// </summary>
        /// <param name="strSupplier">Supplier Name </param>
        /// <param name="strInvoiceID">Invoice ID</param>
        /// <param name="strContactOrganization">contact Organization Name</param>
        /// <param name="strContactFullName">Contact full name</param>
        /// <param name="strContactAddress">contact Address</param>
        /// <param name="strContactEmail">Contact Email</param>
        /// <param name="Lines">Line item Array</param>
        /// <param name="map_Notes">Notes Array</param>
        /// <param name="delCharges">Delivery Charges</param>
        protected void CreateSupplierPDF(String strSupplier, String strInvoiceID, String strContactOrganization, String strContactFullName, String strContactAddress, String strContactEmail, String[] Lines, Dictionary<String, String> map_Notes, String delCharges)
        {
            //Define Fonts for the Order Items Table
            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("customfont", 8, iTextSharp.text.Font.BOLD, Color.RED);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("customfont", 8, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntDefault = FontFactory.GetFont("customfont", 6, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntItalic = FontFactory.GetFont("customfont", 6, iTextSharp.text.Font.BOLDITALIC, Color.BLACK);
            iTextSharp.text.Font fntNormal = FontFactory.GetFont("customfont", 10, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntDeltoneCompName = FontFactory.GetFont("customfont", 12, iTextSharp.text.Font.NORMAL, Color.BLACK);

            string strReportName = strSupplier + "-" + strInvoiceID + "-" + strContactOrganization + ".pdf";

            Document document = new Document(PageSize.A4, 71, 71, 71, 71);
            //string pdfFilePath = Server.MapPath(".") + "\\Invoices\\creditnotes\\";

            try
            {

                string pdfFilePath = "C:\\inetpub\\wwwroot\\DeltoneCRM\\Invoices\\Supplier\\";

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfFilePath + strReportName, FileMode.Create));
                document.Open();

                String strOrderNo = strInvoiceID; //Amend the Xero InvoiceIDm Later 
                String orederDate = DateTime.Now.ToString("dd MMM yyyy");

                ///Due Date Calculation
                String strDueDate = DateTime.Now.AddDays(21).ToString("dd MMM yyyy");

                //TESTING - SHEHAN
                FontFactory.Register("C:\\Windows\\Fonts\\segoeui.ttf", "SEGOEUI");
                FontFactory.Register("C:\\Windows\\Fonts\\seguisb.ttf", "SEGOEUI_SB");

                //Create Fonts 
                var font_14_semibold = FontFactory.GetFont("SEGOEUI_SB", 14, Font.NORMAL, Color.BLACK);
                var font_12_semibold = FontFactory.GetFont("SEGOEUI_SB", 12, Font.NORMAL, Color.BLACK);
                var font_10_semibold = FontFactory.GetFont("SEGOEUI_SB", 10, Font.NORMAL, Color.BLACK);
                var font_08_semibold = FontFactory.GetFont("SEGOEUI_SB", 8, Font.NORMAL, Color.BLACK);
                var font_10_normal = FontFactory.GetFont("SEGOEUI", 10, Font.NORMAL, Color.BLACK);
                var font_09_normal = FontFactory.GetFont("SEGOEUI", 9, Font.NORMAL, Color.BLACK);
                var font_08_normal = FontFactory.GetFont("SEGOEUI", 10, Font.BOLD, Color.RED);

                //Modified here another font for the Line items 

                var font_Line_Item = FontFactory.GetFont("SEGOEUI", 8, Font.NORMAL, Color.BLACK);



                var font_spacer = FontFactory.GetFont("SEGOEUI", 5, Font.NORMAL, Color.BLACK);
                var font_spacer_sml = FontFactory.GetFont("SEGOEUI", 2, Font.NORMAL, Color.BLACK);

                var titleFont = FontFactory.GetFont("SEGOEUI_SB", 14, Font.NORMAL, Color.BLACK);
                var boldTableFont = FontFactory.GetFont("customfont", 8, Font.BOLD);
                var bodyFont = FontFactory.GetFont("customfont", 8, Font.NORMAL);
                Rectangle pageSize = writer.PageSize;

                document.Open();

                #region invoiceDetails

                // NEW SECTION from SHEHAN

                document.Add(new Paragraph("DELTONE SOLUTIONS PTY LTD", font_12_semibold));
                document.Add(new Paragraph("A3/2A Westall Road, Clayton South VIC 3169", font_10_normal));
                document.Add(new Paragraph(" ", font_spacer));

                document.Add(new Paragraph("CREDIT NOTE: " + strOrderNo, font_14_semibold));
                document.Add(new Paragraph(" ", font_14_semibold));

                document.Add(new Paragraph("DELIVER TO", font_10_semibold));
                document.Add(new Paragraph(strContactOrganization, font_10_normal));
                document.Add(new Paragraph(strContactFullName, font_09_normal));
                document.Add(new Paragraph(strContactAddress, font_09_normal));

                document.Add(new Paragraph(" ", font_08_normal));
                document.Add(new Paragraph(" ", font_08_normal));


                //String path="c:\\docs\\final-logo.png";
                //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(path);

                //INSERT Company Logo
                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("c:\\inetpub\\wwwroot\\deltonecrm\\Images\\final-logo.png");
                jpg.SetAbsolutePosition(430, 650);
                jpg.ScaleToFit(140f, 120f);
                jpg.SpacingBefore = 10f;
                jpg.SpacingAfter = 1f;
                jpg.Alignment = Element.ALIGN_LEFT;

                document.Add(jpg);


                #endregion


                #region ItemsTable

                document.Add(new Paragraph("PRODUCT DETAILS", font_10_semibold));
                document.Add(new Paragraph(" ", font_spacer));

                PdfPTable ItemsTable = new PdfPTable(3);
                ItemsTable.DefaultCell.Border = Rectangle.NO_BORDER;

                ItemsTable.WidthPercentage = 100;
                ItemsTable.HorizontalAlignment = 0;
                ItemsTable.SpacingAfter = 10;
                float[] sglTblHdWidths = new float[3];
                sglTblHdWidths[0] = 75f;
                sglTblHdWidths[1] = 10f;
                sglTblHdWidths[2] = 15f;
                ItemsTable.SetWidths(sglTblHdWidths);

                PdfPCell CellOneHdr = new PdfPCell(new Phrase("SUPPLIER CODE", font_08_semibold));
                CellOneHdr.HorizontalAlignment = 0;
                CellOneHdr.BackgroundColor = new Color(204, 204, 204);
                CellOneHdr.PaddingBottom = 7f;
                CellOneHdr.PaddingTop = 6f;
                //CellOneHdr.Border = Rectangle.BOTTOM_BORDER;
                //CellOneHdr.BorderWidthBottom = 1f;
                ItemsTable.AddCell(CellOneHdr);

                //Define Table Header-Unit Price
                PdfPCell CellTreeHdr = new PdfPCell(new Phrase("QTY", font_08_semibold));
                CellTreeHdr.HorizontalAlignment = 1;
                CellTreeHdr.PaddingBottom = 7f;
                CellTreeHdr.PaddingTop = 6f;
                CellTreeHdr.BackgroundColor = new Color(204, 204, 204);
                //CellTreeHdr.Border = Rectangle.BOTTOM_BORDER;
                //CellTreeHdr.BorderWidthBottom = 1f;
                ItemsTable.AddCell(CellTreeHdr);


                //Define Table Header-Quanity
                PdfPCell CellTwoHdr = new PdfPCell(new Phrase("UNIT PRICE", font_08_semibold));
                CellTwoHdr.HorizontalAlignment = 2;
                CellTwoHdr.PaddingBottom = 7f;
                CellTwoHdr.PaddingTop = 6f;
                CellTwoHdr.BackgroundColor = new Color(204, 204, 204);
                //CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                //CellTwoHdr.BorderWidthBottom = 1f;
                ItemsTable.AddCell(CellTwoHdr);


                PdfPCell Cell;
                //Loop through Line items to Popualte the Table
                for (int i = 0; i < Lines.Length; i++)
                {
                    if (!String.IsNullOrEmpty(Lines[i]))
                    {
                        String[] items = Lines[i].Split(':');
                        //SupplierCode cell
                        Cell = new PdfPCell(new Phrase(items[0], font_Line_Item));
                        Cell.HorizontalAlignment = 0;
                        //CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                        //CellTwoHdr.BorderWidthBottom = 1f;
                        ItemsTable.AddCell(Cell);

                        //Quantity cell
                        Cell = new PdfPCell(new Phrase(items[1], font_Line_Item));
                        Cell.HorizontalAlignment = 1;
                        //CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                        //CellTwoHdr.BorderWidthBottom = 1f;
                        ItemsTable.AddCell(Cell);

                        //COG cell
                        Cell = new PdfPCell(new Phrase(items[2], font_Line_Item));
                        Cell.HorizontalAlignment = 2;
                        Cell.PaddingBottom = 5f;
                        Cell.PaddingTop = 3f;
                        //CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                        //CellTwoHdr.BorderWidthBottom = 1f;
                        ItemsTable.AddCell(Cell);
                    }
                }


                //Add DelCost Charges if Exsits

                if (!delCharges.Equals("0") && !delCharges.Equals(String.Empty))
                {
                    Cell = new PdfPCell(new Phrase("D/L Handling", font_Line_Item));
                    Cell.HorizontalAlignment = 0;
                    ItemsTable.AddCell(Cell);


                    Cell = new PdfPCell(new Phrase("1", font_Line_Item));
                    Cell.HorizontalAlignment = 1;

                    ItemsTable.AddCell(Cell);


                    Cell = new PdfPCell(new Phrase(delCharges, font_Line_Item));
                    Cell.HorizontalAlignment = 1;

                    //ItemsTable.AddCell(Cell);


                }

                document.Add(ItemsTable);

                #region SupplierNote
                document.Add(new Paragraph(" ", font_spacer));
                document.Add(new Paragraph("INSTRUCTIONS TO VENDOR", font_10_semibold));
                document.Add(new Paragraph(" ", font_spacer));

                foreach (var item in map_Notes)
                {
                    if (item.Key == strSupplier)
                    {
                        document.Add(new Paragraph(item.Value, font_08_normal));
                        //document.Add(SupplierNote);
                    }
                }

                #endregion

                document.Close();

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString() + "::" + ex.StackTrace.ToString());
            }
        }



        /// <summary>
        /// Creates customer invoice
        /// </summary>
        /// <param name="strInvoiceID">InvoiceID</param>
        /// <param name="strContactOrganization">contact organization name</param>
        /// <param name="strContactFullName">contact fullname</param>
        /// <param name="strContactAddress">contact Address</param>
        /// <param name="contactEmail">contact Email</param>
        /// <param name="customerDelCost">customer DeliveryCost</param>
        /// <param name="strOrderItems">OrderItems Array</param>
        /// <param name="ProfitTotal">profit total</param>
        /// <param name="profitSubTotal">profit sub total</param>
        /// <param name="gstAmount">gst amount</param>
        protected void GeneratePDf(String strInvoiceID, String strContactOrganization, String strContactFullName, String strContactAddress, String contactEmail, float customerDelCost, String[] strOrderItems, float ProfitTotal, float profitSubTotal, float gstAmount)
        {
            //Define Fonts for the Order Items Table
            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, Color.BLACK);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntDefault = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntItalic = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLDITALIC, Color.BLACK);
            iTextSharp.text.Font fntNormal = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, Color.BLACK);

            //string strReportName = "DeltoneInvoice" + DateTime.Now.Ticks + ".pdf";

            string strReportName = "DTS-" + strInvoiceID + ".pdf";

            //Create the Document object
            Document document = new Document(PageSize.A4, 70, 70, 70, 70);
            string pdfFilePath = Server.MapPath(".") + "\\Invoices\\Customer\\";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfFilePath + strReportName, FileMode.Create));
            document.Open();

            String strOrderNo = "DTS-" + strInvoiceID;
            String orederDate = DateTime.Now.ToString("dd MMM yyyy");

            ///Due Date Calculation
            String strDueDate = DateTime.Now.AddDays(21).ToString("dd MMM yyyy");



            //Create Fonts 
            var titleFont = FontFactory.GetFont("Arial", 12, Font.BOLD, Color.RED);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            Rectangle pageSize = writer.PageSize;

            document.Open();

            #region Invoice Details
            PdfPTable OuterTable = new PdfPTable(2);
            OuterTable.HorizontalAlignment = 0;
            OuterTable.WidthPercentage = 100;

            //Set the Propotions of the Cells 
            float[] widths = new float[] { 1f, 1f };

            OuterTable.SetWidths(widths);
            //Set No Border for Table
            OuterTable.DefaultCell.Border = Rectangle.NO_BORDER;


            OuterTable.HorizontalAlignment = 0;
            //leave a gap before and after the table
            OuterTable.SpacingBefore = 20f;
            OuterTable.SpacingAfter = 30f;


            //Title Cell INVOICE with the title font selected
            PdfPCell cell = new PdfPCell(new Phrase("INVOICE", titleFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 3;
            cell.HorizontalAlignment = 1;
            OuterTable.AddCell(cell);


            //Company Name Cell
            PdfPCell cellCompanyName = new PdfPCell(new Phrase("Compnay Name:", fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(cellCompanyName);



            PdfPCell CompanyName = new PdfPCell(new Phrase("Deltone Solutions Pty Ltd \n\n PO BOX 1041 \n\n Clayton South, VIC 3169 \n\n AUSTRALIA \n\n Ph: 1300 787 783 \n\n info@deltonesolutions.com.au \n\n ABN: 51 168 015 870", fntNormal));
            CompanyName.Border = Rectangle.NO_BORDER;
            CompanyName.PaddingTop = 1f;
            OuterTable.AddCell(CompanyName);

            //Invoice Date Cell
            PdfPCell cellInvoiceDate = new PdfPCell(new Phrase("Date:", fntNormal));
            cellInvoiceDate.Border = Rectangle.NO_BORDER;
            cellInvoiceDate.PaddingTop = 1f;
            OuterTable.AddCell(cellInvoiceDate);

            PdfPCell InvoiceDate = new PdfPCell(new Phrase(orederDate, fntNormal));
            InvoiceDate.Border = Rectangle.NO_BORDER;
            InvoiceDate.PaddingTop = 1f;
            OuterTable.AddCell(InvoiceDate);


            //Invoice Number Cell
            PdfPCell cellInvoiceNumber = new PdfPCell(new Phrase("Invoice Number:", fntNormal));
            cellInvoiceNumber.Border = Rectangle.NO_BORDER;
            cellInvoiceNumber.PaddingTop = 1f;
            OuterTable.AddCell(cellInvoiceNumber);

            PdfPCell InvoiceNumber = new PdfPCell(new Phrase(strOrderNo, fntNormal));
            InvoiceNumber.Border = Rectangle.NO_BORDER;
            OuterTable.AddCell(InvoiceNumber);

            //Bill TO Cell
            PdfPCell cellBillTo = new PdfPCell(new Phrase("BillTo:", fntNormal));
            cellBillTo.Border = Rectangle.NO_BORDER;
            OuterTable.AddCell(cellBillTo);

            PdfPCell BillTo = new PdfPCell(new Phrase(strContactOrganization + "\n" + "Attention:" + strContactFullName + "\n" + strContactAddress, bodyFont));
            BillTo.Border = Rectangle.NO_BORDER;
            OuterTable.AddCell(BillTo);

            document.Add(OuterTable);


            #endregion

            #region Items Table

            PdfPTable ItemsTable = new PdfPTable(4);

            //Set No border for the Table
            ItemsTable.DefaultCell.Border = Rectangle.NO_BORDER;

            ItemsTable.WidthPercentage = 100;
            ItemsTable.HorizontalAlignment = 0;
            ItemsTable.SpacingAfter = 10;
            float[] sglTblHdWidths = new float[4];
            sglTblHdWidths[0] = 400f;
            sglTblHdWidths[1] = 50f;
            sglTblHdWidths[2] = 100f;
            sglTblHdWidths[3] = 100f;
            //sglTblHdWidths[4] = 100f;

            ItemsTable.SetWidths(sglTblHdWidths);

            ItemsTable.SetWidths(sglTblHdWidths);

            //Define the Table Header-Description
            PdfPCell CellOneHdr = new PdfPCell(new Phrase("Description - Product to Suit", fntTableFontHdr));
            CellOneHdr.Border = Rectangle.BOTTOM_BORDER;
            CellOneHdr.BorderWidthBottom = 1f;
            ItemsTable.AddCell(CellOneHdr);

            //Define Table Header-Quanity

            PdfPCell CellTwoHdr = new PdfPCell(new Phrase("Qty", fntTableFontHdr));
            CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
            CellTwoHdr.BorderWidthBottom = 1f;
            ItemsTable.AddCell(CellTwoHdr);

            //Define Table Header-Unit Price
            PdfPCell CellTreeHdr = new PdfPCell(new Phrase("Unit Price\n(inc. GST)", fntTableFontHdr));
            CellTreeHdr.Border = Rectangle.BOTTOM_BORDER;
            CellTreeHdr.BorderWidthBottom = 1f;
            ItemsTable.AddCell(CellTreeHdr);

            //Define Amount 
            PdfPCell CellFiveHdr = new PdfPCell(new Phrase("Amount AUD\n(inc. GST)", fntTableFontHdr));
            CellFiveHdr.Border = Rectangle.BOTTOM_BORDER;
            CellFiveHdr.BorderWidthBottom = 1f;
            ItemsTable.AddCell(CellFiveHdr);

            String[] itemRow;

            //Loop through Items in the Items and Populate Table 
            for (int i = 0; i < strOrderItems.Length; i++)
            {

                if (!String.IsNullOrEmpty(strOrderItems[i]))
                {
                    itemRow = strOrderItems[i].Split(',');
                    //For the First Row 
                    if (i == 0)
                    {
                        //Cell Description
                        PdfPCell cellDesc = new PdfPCell(new Phrase(itemRow[1].ToString(), fntTableFont));
                        cellDesc.Border = Rectangle.BOTTOM_BORDER;
                        cellDesc.BorderColorBottom = Color.GRAY;

                        ItemsTable.AddCell(cellDesc);

                        //Cell Quantity
                        PdfPCell cellQty = new PdfPCell(new Phrase(itemRow[4].ToString(), fntTableFont));
                        cellQty.Border = Rectangle.BOTTOM_BORDER;
                        cellQty.BorderColorBottom = Color.GRAY;
                        ItemsTable.AddCell(cellQty);

                        //Cell Unit Price
                        float flUnitPrice = float.Parse(itemRow[5].ToString());

                        //Convert it to Decimal
                        decimal decUnitPrice = Math.Round((decimal)flUnitPrice, 2);

                        PdfPCell cellUnitPrice = new PdfPCell(new Phrase(decUnitPrice.ToString(), fntTableFont));

                        cellUnitPrice.Border = Rectangle.BOTTOM_BORDER;
                        cellUnitPrice.BorderColorBottom = Color.GRAY;
                        ItemsTable.AddCell(cellUnitPrice);

                        //CellAmount 

                        float Amount = (Int32.Parse(itemRow[4]) * float.Parse(itemRow[5]));
                        decimal decRowAmount = Math.Round((decimal)Amount, 2);

                        PdfPCell cellAmount = new PdfPCell(new Phrase(decRowAmount.ToString(), fntTableFont));
                        cellAmount.Border = Rectangle.BOTTOM_BORDER;
                        cellAmount.BorderColorBottom = Color.GRAY;
                        ItemsTable.AddCell(cellAmount);
                    }
                    //Rest of the rows
                    if (i != 0)
                    {
                        //Cell Description
                        PdfPCell cellDesc = new PdfPCell(new Phrase(itemRow[2].ToString(), fntTableFont));
                        cellDesc.Border = Rectangle.BOTTOM_BORDER;
                        cellDesc.BorderColorBottom = Color.GRAY;

                        ItemsTable.AddCell(cellDesc);

                        //Cell Quantity
                        PdfPCell cellQty = new PdfPCell(new Phrase(itemRow[5].ToString(), fntTableFont));
                        cellQty.Border = Rectangle.BOTTOM_BORDER;
                        cellQty.BorderColorBottom = Color.GRAY;
                        ItemsTable.AddCell(cellQty);

                        //Cell Unit Price

                        float flUnitPrice = float.Parse(itemRow[6].ToString());

                        decimal decUnitPrice = Math.Round((decimal)flUnitPrice, 2);

                        PdfPCell cellUnitPrice = new PdfPCell(new Phrase(decUnitPrice.ToString(), fntTableFont));
                        cellUnitPrice.Border = Rectangle.BOTTOM_BORDER;
                        cellUnitPrice.BorderColorBottom = Color.GRAY;
                        ItemsTable.AddCell(cellUnitPrice);

                        //CellAmount 

                        float Amount = (Int32.Parse(itemRow[5]) * float.Parse(itemRow[6]));
                        decimal decAmount = Math.Round((decimal)Amount, 2);


                        PdfPCell cellAmount = new PdfPCell(new Phrase(decAmount.ToString(), fntTableFont));
                        cellAmount.Border = Rectangle.BOTTOM_BORDER;
                        cellAmount.BorderColorBottom = Color.GRAY;
                        ItemsTable.AddCell(cellAmount);
                    }








                }
            }

            //End Loop through Items in the Table 


            //Add Customer Delivery Cost
            PdfPCell cellEmpty;
            cellEmpty = new PdfPCell(new Phrase("", fntTableFont));
            cellEmpty.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellEmpty);


            cellEmpty = new PdfPCell(new Phrase("", fntTableFont));
            cellEmpty.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellEmpty);


            PdfPCell cellCustomerDeliverCost = new PdfPCell(new Phrase("Customer Delivery Cost", fntTableFontHdr));
            cellCustomerDeliverCost.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellCustomerDeliverCost);


            PdfPCell CustomerDelCostAmount = new PdfPCell(new Phrase(customerDelCost.ToString(), fntTableFont));
            CustomerDelCostAmount.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(CustomerDelCostAmount);

            //Add SubTotal     


            cellEmpty = new PdfPCell(new Phrase("", fntTableFont));
            cellEmpty.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellEmpty);


            cellEmpty = new PdfPCell(new Phrase("", fntTableFont));
            cellEmpty.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellEmpty);



            PdfPCell subTotal = new PdfPCell(new Phrase("Subtotal", fntTableFont));
            subTotal.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(subTotal);

            PdfPCell subTotalAmount = new PdfPCell(new Phrase(profitSubTotal.ToString(), fntTableFont));
            subTotalAmount.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(subTotalAmount);



            cellEmpty = new PdfPCell(new Phrase("", fntTableFont));
            cellEmpty.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellEmpty);


            cellEmpty = new PdfPCell(new Phrase("", fntTableFont));
            cellEmpty.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellEmpty);


            float gst = ProfitTotal - profitSubTotal;

            //Add GST Amount
            PdfPCell TotalGST = new PdfPCell(new Phrase("Total GST 10%", fntTableFont));
            TotalGST.Border = Rectangle.BOTTOM_BORDER;
            TotalGST.BorderWidthBottom = 1f;
            ItemsTable.AddCell(TotalGST);


            decimal decgst = Math.Round((decimal)gst, 2);

            PdfPCell TotalGSTAmount = new PdfPCell(new Phrase(decgst.ToString(), fntTableFont));
            TotalGSTAmount.Border = Rectangle.BOTTOM_BORDER;
            TotalGSTAmount.BorderWidthBottom = 1f;
            ItemsTable.AddCell(TotalGSTAmount);


            cellEmpty = new PdfPCell(new Phrase("", fntTableFont));
            cellEmpty.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellEmpty);


            cellEmpty = new PdfPCell(new Phrase("", fntTableFont));
            cellEmpty.Border = Rectangle.NO_BORDER;
            ItemsTable.AddCell(cellEmpty);

            //Total Amount Due AUD
            PdfPCell TotalAmountDue = new PdfPCell(new Phrase("Amount Due AUD", fntTableFontHdr));
            TotalAmountDue.Border = Rectangle.NO_BORDER;

            ItemsTable.AddCell(TotalAmountDue);

            PdfPCell TotalAmount = new PdfPCell(new Phrase(ProfitTotal.ToString(), fntTableFontHdr));
            TotalAmount.Border = Rectangle.NO_BORDER;

            ItemsTable.AddCell(TotalAmount);


            document.Add(ItemsTable);

            #endregion


            #region AccountDetails

            Chunk transferBank = new Chunk("Due Date:" + strDueDate, fntDefault);
            //transferBank.SetUnderline(0.1f, -2f); //0.1 thick, -2 y-location
            document.Add(transferBank);
            document.Add(Chunk.NEWLINE);

            //Account Details
            PdfPTable bottomTable = new PdfPTable(1);
            bottomTable.HorizontalAlignment = 0;
            bottomTable.TotalWidth = 200f;
            //bottomTable.SetWidths(new int[] { 90, 10, 200 });
            bottomTable.LockedWidth = true;
            bottomTable.SpacingBefore = 20;
            bottomTable.DefaultCell.Border = Rectangle.NO_BORDER;
            bottomTable.AddCell(new Phrase("Bank Details:", fntDefault));
            bottomTable.AddCell(new Phrase("ANZ Bank", fntDefault));
            bottomTable.AddCell(new Phrase("BSB: 013-232", fntDefault));
            bottomTable.AddCell(new Phrase("Acct No: 193374956", fntDefault));

            bottomTable.AddCell(new Phrase("Account Name: Deltone Solutions", fntDefault));
            bottomTable.AddCell(new Phrase("Remittance to: accounts@deltonesolutions.com.au", fntDefault));

            bottomTable.AddCell(new Phrase("Please Quote Reference:" + "DTS-" + strInvoiceID, fntItalic));

            document.Add(bottomTable);

            #endregion

            document.Close();
        }


        #endregion Document Generation


        //Edit Order Related Functions

        /// <summary>
        /// fetch contact given by contact ID
        /// </summary>
        /// <param name="contactID"></param>
        /// <returns></returns>
        protected String getContactbyContactId(String contactID)
        {
            String strContact = String.Empty;
            return strContact;
        }

        /// <summary>
        /// Fetch the  Credit Note given by CreditNoteID
        /// </summary>
        /// <param name="CreditNoteID">Credit Note ID</param>
        /// <returns></returns>
        protected String getOrderbyOrderID(int CreditNoteID)
        {
            String strCreditNote = String.Empty;
            String strOrder = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "select * from CreditNotes where CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        //String Reference = sdr["Reference"].ToString();

                        String strGuid = (!DBNull.Value.Equals(sdr["XeroGuid"])) ? sdr["XeroGuid"].ToString() : String.Empty;
                        String xeroCreditNoteID = (!DBNull.Value.Equals(sdr["XeroCreditNoteID"])) ? sdr["XeroCreditNoteID"].ToString() : String.Empty;
                        String TypeOfCredit = sdr["CreditNoteReason"].ToString();
                        String CreatedDate = Convert.ToDateTime(sdr["DateCreated"]).ToString("yyyy-MM-dd");
                        String CreditStatus = sdr["Status"].ToString();
                        String SuppDelItems = sdr["SuppDelItems"].ToString();  //Modified here 
                        String CustomerDelItems = sdr["CusDelCostItems"].ToString();//Modified here 

                        String ORDERID = sdr["OrderID"].ToString(); //Get the Order Related to the Credit Note

                        strCreditNote = xeroCreditNoteID + ":" + strGuid + ":" + TypeOfCredit + ":" + CreatedDate + ":" + CreditStatus + ":" + CustomerDelItems + ":" + SuppDelItems + ":" + ORDERID; //Modified here 

                        //strOrder = strOrder + sdr["SuppDeltems"].ToString() + ":" + sdr["ProItems"].ToString() + ":" + sdr["CusDelCostItems"].ToString() + ":" + sdr["Status"].ToString() + ":" + strORDERDATE + ":" + DueDate + ":" + Reference + ":" + delCharges + ":" + strGuid + ":" + PaymentTerms + ":" + OrderCreateDate;
                        if (!IsPostBack && !String.IsNullOrEmpty(sdr["Notes"].ToString()))
                        {
                            OrderNotes.Text = sdr["Notes"].ToString();
                        }
                    }
                }
            }
            conn.Close();

            return strCreditNote;

        }

        private string SetFaultyTypeData(int creditId)
        {
            var conn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            return new CreditNoteRMAHandler(conn).GetRAMFAULTYGoodsByCreditId(creditId);
        }




        /// <summary>
        /// get credit note items 
        /// </summary>
        /// <param name="CreditNoteID">CreditNoteID</param>
        /// <returns></returns>
        protected String getOrderItemsbyOrderID(int CreditNoteID)
        {

            String strOrderitems = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            String strSqlStmt = "select * from dbo.CreditNote_Item where  CreditNoteID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOrderitems = strOrderitems + sdr["Description"] + "," + sdr["UnitAmount"] + "," + sdr["COG"] + "," + sdr["SupplierCode"] + "," + sdr["Quantity"] + "," + sdr["SupplierName"];
                        strOrderitems = strOrderitems + "|";

                    }
                }

            }
            conn.Close();

            return strOrderitems;

        }

        //End Edit Oreder Related Functions

        //This Function Fetch The LastCreated OrderID
        protected int LastOrderID(String strCratedBy)
        {
            int intOrderID = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "select Top 1 * from dbo.Orders where CreatedBy='" + strCratedBy + "'Order by  CreatedDateTime Desc";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        intOrderID = Int32.Parse(sdr["OrderID"].ToString());
                    }
                }

            }
            conn.Close();

            return intOrderID;
        }
        //This Function Create an Order and OrderItems
        protected int CreateOrder(String strCompanyID, String strContactId, float COGTotal, float COGSubTotal, float ProfitTotal, float ProfitSubtotal, float SuppDelCost, float CusDelCost, float ProItemCost, String strCreatedBy, String SuppDelItems, String ProItems, String strCusDelItems, String status, DateTime duedate, String strOrderedBy, String OrderNotesStr, String PaymentTerms, String Reference, String TypeOfCall, DateTime orderDate)
        {
            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //Modification done here add Order DueDate  and OrderedBy(ownership) 30/04/2015
            String strSqlStmt = "insert into dbo.Orders(CompanyID,ContactID,COGTotal,COGSubTotal,Total,SubTotal,SupplierDelCost,CustomerDelCost,ProItemCost,OrderedDateTime,CreatedDateTime,CreatedBy,SuppDeltems,ProItems,CusDelCostItems,Status,DueDate,OrderedBy, Notes, PaymentTerms, Reference, TypeOfCall) values (@CompanyID,@ContactID,@COGtotal,@CogSubtotal,@ProfitTotal,@ProfitSubTotal,@SuppDelCost,@CusDelCost,@ProItemCost,@OrderDate,CURRENT_TIMESTAMP,@CreatedBy,@SuppDelItems,@Proitems,@CusDelItems,@Status,@duedate,@orderedby, @OrderNotes, @PaymentTerms, @Reference, @TypeOfCall);";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@CompanyID", strCompanyID);
            cmd.Parameters.AddWithValue("@ContactID", strContactId);
            cmd.Parameters.AddWithValue("@COGtotal", COGTotal);
            cmd.Parameters.AddWithValue("@CogSubtotal", COGSubTotal);
            cmd.Parameters.AddWithValue("@ProfitTotal", ProfitTotal);
            cmd.Parameters.AddWithValue("@ProfitSubTotal", ProfitSubtotal);
            cmd.Parameters.AddWithValue("@SuppDelCost", SuppDelCost);
            cmd.Parameters.AddWithValue("@CusDelCost", CusDelCost);
            cmd.Parameters.AddWithValue("@ProItemCost", ProItemCost);
            cmd.Parameters.AddWithValue("@CreatedBy", strCreatedBy);
            //Modification done Add SuppDelCost and Promotional Item Cost
            cmd.Parameters.AddWithValue("@SuppDelItems", SuppDelItems);
            cmd.Parameters.AddWithValue("@Proitems", ProItems);
            cmd.Parameters.AddWithValue("@CusDelItems", strCusDelItems);
            cmd.Parameters.AddWithValue("@Status", status);
            //Modificaton done here Add Order due date pramater and Orderedby Parameter
            cmd.Parameters.AddWithValue("@duedate", duedate);
            cmd.Parameters.AddWithValue("@orderedby", strOrderedBy);
            cmd.Parameters.AddWithValue("@OrderNotes", OrderNotesStr);
            //Modification to record payment terms, reference number and type of call
            cmd.Parameters.AddWithValue("@PaymentTerms", PaymentTerms);
            cmd.Parameters.AddWithValue("@Reference", Reference);
            cmd.Parameters.AddWithValue("@TypeOfCall", TypeOfCall);
            //Modified here Sumudu Kodikar Add user Defined Date time
            //OrderDate
            cmd.Parameters.AddWithValue("@OrderDate", orderDate);
            try
            {
                conn.Open();
                rowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            return rowsEffected;
        }

        //This Function Insert Order Items given by Values
        protected int CreateOrderItems(int CreditNoteID, String strItemDesc, String strItemCode, float UnitAmout, int quantity, float COGAmount, String strCratedBy, String strSupplierItemCode, String strSuppName)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            //Description,Quantity,UnitAmount,SupplierCode,COG,DateCreated,CreatedBy,SupplierName
            String strSqlStmt = "INSERT into dbo.CreditNote_Item(CreditNoteID,Description,Quantity,UnitAmount,COG,SupplierCode,DateCreated,CreatedBy,SupplierName)  values(@creditNoteID,@Description,@Quanity,@UnitAmount,@COG,@SupplierCode,CURRENT_TIMESTAMP,@strCreatedBy,@suppname);";
            SqlCommand cmd = new SqlCommand(strSqlStmt, conn);
            cmd.Parameters.AddWithValue("@creditNoteID", CreditNoteID);
            cmd.Parameters.AddWithValue("@Description", strItemDesc);
            cmd.Parameters.AddWithValue("@Quanity", quantity);
            cmd.Parameters.AddWithValue("@UnitAmount", UnitAmout);
            cmd.Parameters.AddWithValue("@COG", COGAmount);
            cmd.Parameters.AddWithValue("@SupplierCode", strSupplierItemCode);
            cmd.Parameters.AddWithValue("@strCreatedBy", strCratedBy);
            //Modified here add Supplier Name 
            cmd.Parameters.AddWithValue("@suppname", strSuppName);
            try
            {
                conn.Open();
                rowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
                /*If an Exception Occurres  Write the Exception details to the Database including FileName,Method,Detail Error and and LoggedUser if want ,Create a Table and Write to Table if fails Email*/

            }

            return rowEffected;
        }
        //End function Insert Order Items given by Values

        //This Function Remove the CreditNote Items  given by OrderID
        protected int DeleteOrderItems(int CreditNoteId, out string orderItemsString)
        {
            var orderItemString = "";
            orderItemsString = GetPreviousOrderItemsString(CreditNoteId, orderItemString);

            int intRowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strDelStmt = "delete from dbo.CreditNote_Item where  CreditNoteID=" + CreditNoteId;
            SqlCommand cmd = new SqlCommand(strDelStmt, conn);
            //cmd.Parameters.AddWithValue("@OrderID", CreditNoteId);
            try
            {
                conn.Open();
                intRowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            return intRowEffected;
        }
        //End function Remove Order Items

        protected string GetPreviousOrderItemsString(int orderId, string orderItemsStrings)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var orderItemList = new CreditNotesDAL(connectionString).getCreditNoteList(orderId);
            foreach (var item in orderItemList)
            {
                if (orderItemsStrings == "")
                    orderItemsStrings = " Deleted Items: Item Note :" + item.Notes + " Reason " + item.reson + " Status " + item.Status + " ";
                else
                    orderItemsStrings = orderItemsStrings + " Item Description :" + item.Notes + " Reason " + item.reson + " Status " + item.Status + " ";

            }

            return orderItemsStrings;
        }



        /// <summary>
        /// Update the Exsisting Credit Note 
        /// </summary>
        /// <param name="CreditNoteID">creditnoteid</param>
        /// <param name="strCompanyID">companyid</param>
        /// <param name="strContactId">contactid</param>
        /// <param name="strAlteredBy">alteredby</param>
        /// <param name="OrderNotesStr">creditnote note string </param>
        /// <param name="CusDelItems">cusdelitems string </param>
        /// <param name="SupDelItems">suppdelitems string</param>
        /// <returns></returns>
        protected int UpdateCreditNoteDB(int CreditNoteID, String strCompanyID, String strContactId, String strAlteredBy, String OrderNotesStr, String CusDelItems, String SupDelItems, float Total, float SubTotal, float SplitVolumeAmount)
        {
            int intRowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //Modification here 30/04/2015 add Updation due date
            String strSQLUpdateStmt = "Update dbo.CreditNotes set DateAltered=CURRENT_TIMESTAMP,CreatedBy=@CreatedBy,AlteredBy=@AlteredBy,Notes=@OrderNotes,SuppDelItems=@suppdelitems,CusDelCostItems=@customerdelitems,Total=@Total, SubTotal=@SubTotal, SplitVolumeAmount=@SplitVolumeAmount where CreditNote_ID=@CreditNoteID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", CreditNoteID);
            cmd.Parameters.AddWithValue("@AlteredBy", Session["LoggedUser"]);
            cmd.Parameters.AddWithValue("@CreatedBy", strAlteredBy);
            //cmd.Parameters.AddWithValue("@Total", ProfitTotal);
            cmd.Parameters.AddWithValue("@OrderNotes", OrderNotesStr);
            //cmd.Parameters.AddWithValue("@Reason", TypeOfCredit);
            cmd.Parameters.AddWithValue("@suppdelitems", SupDelItems);
            cmd.Parameters.AddWithValue("@customerdelitems", CusDelItems);
            cmd.Parameters.AddWithValue("@Total", Total); //Modified here Add Total Amount 
            cmd.Parameters.AddWithValue("@SubTotal", SubTotal);
            cmd.Parameters.AddWithValue("@SplitVolumeAmount", SplitVolumeAmount);

            try
            {
                conn.Open();
                intRowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            return intRowEffected;
        }
        //End Function Update Order 

        //This Method Approve Status of the Order
        protected void btnInvoiceApprove_Click(object sender, EventArgs e)
        {
            XeroIntergration xero = new XeroIntergration();
            String deltoneInvoice_Number = ORDERID.ToString();
            String deltoneInvoice_ID = String.Empty;
            //Disablee the Submit Button here Chnage the DataBase Record and Navigate Where is came from 
            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var previousValues = new CreditNotesDAL(connectionstring).getCurrentCreditNoteStatus(ORDERID);
            if (ChangeOrderStatus(ORDERID) > 0)
            {

                Repository Res = xero.CreateRepository();
                String connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                orderdal = new OrderDAL(connString);
                //String xeroGuid = orderdal.getOrderGuid(ORDERID);
                String xeroGuid = getOrderGuid(ORDERID);

                //String Order_Reference = ReferenceFld.Text;
                String Order_Reference = String.Empty;

                if (!String.IsNullOrEmpty(xeroGuid))
                {
                    XeroApi.Model.CreditNote ApprovedCreditNote = xero.AuthorizeCreditNote(Res, xeroGuid, String.Empty);
                    if (ApprovedCreditNote != null)
                    {
                        deltoneInvoice_Number = ApprovedCreditNote.CreditNoteNumber.ToString();
                        deltoneInvoice_ID = ApprovedCreditNote.CreditNoteID.ToString();

                        var columnName = "Status";
                        var talbeName = "CreditNote";
                        var ActionType = "APPROVED";
                        int primaryKey = ORDERID;

                        var orderIdForCreditNote = new CreditNotesDAL(connectionstring).getOrderIDFromCreditID(ORDERID);

                        var newvalues = " Credit Note for Order Id " + orderIdForCreditNote + " :Credit Ortder Status changed from " + previousValues + " to APPROVED";



                        var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = connectionstring;
                        var strCompanyID = new CreditNotesDAL(connectionstring).getCompanyIDFromCreditID(ORDERID);

                        new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
                      columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));

                        //  HandleRMARequest(ORDERID);
                    }
                }
                else
                {

                }

                CreditNotesDAL cndal = new CreditNotesDAL(connString);


                Dictionary<String, String> di_Notes = cndal.getNotesObject(ORDERID);
                Dictionary<String, String> di_suppitems = cndal.getPurchaseItemObject(ORDERID);

                GenerateSupplierDocs(ORDERID, XeroCreditNoteID, CONTACTID.ToString(), di_suppitems, di_Notes);

                String Message = "<h2>CREDIT NOTE: " + deltoneInvoice_Number + " HAS BEEN AUTHORIZED</h2>";
                String NavigateUrl = "CreditNotes/AllCreditNotes.aspx";
                String PrintURl = "PrintOrder.aspx?Oderid=" + ORDERID + "&cid=" + CONTACTID + "&Compid=" + COMPANYID;
                string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }
            else
            {
                String Message = "<h2>ERROR OCCURED AUTHORIZING  ORDER: " + deltoneInvoice_Number + "</h2>";
                String NavigateUrl = "CompanyOrders.aspx?ContactID=" + CONTACTID + "&CompanyID=" + COMPANYID + "&Order=" + ORDERID + "&Update=Success";
                String PrintURl = "PrintOrder.aspx?Oderid=" + ORDERID + "&cid=" + CONTACTID + "&Compid=" + COMPANYID;
                string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }


        }

        private static void UpdateRMACreditXeroInfo(int creditNoteId)
        {
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var creditEmailHandler = new CreditNoteRMAHandler(strConnectionString);
            creditEmailHandler.UpdateRMISentToSupplier(creditNoteId);
        }

        private static void UpdateRMASentToCustomer(int creditNoteId)
        {
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var creditEmailHandler = new CreditNoteRMAHandler(strConnectionString);
            creditEmailHandler.UpdateRMISentToCustomer(creditNoteId);
        }

        private void HandleRMARequest(int creditNoteId)
        {
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);
            var orderDal = new OrderDAL(strConnectionString);
            var creditNoteDal = new CreditNotesDAL(strConnectionString);
            var ordersendEmail = new OrderSendEmailDAL(strConnectionString);
            var creditNoteObj = creditNoteDal.getCreditNoteObj(creditNoteId);
            var creditEmailHandler = new CreditNoteRMAHandler(strConnectionString);

            if (creditNoteObj.IsAvail)
            {
                if (creditNoteObj.Reason != "PRICE REDUCTION" || creditNoteObj.Reason != "LOST IN TRANSIT" || creditNoteObj.Reason != "CHANGED PRINTER")
                {
                    var contactId = Convert.ToInt32(creditNoteObj.ContactId);
                    var contact = cdal.GetContactByContactId(contactId);
                    if (!string.IsNullOrEmpty(creditNoteObj.Reason))
                    {

                        var subject = "RAM Request for " + creditNoteObj.Reason + " " + creditNoteObj.XeroCreditNoteID;

                        var orderID = Convert.ToInt32(creditNoteObj.OrderId);
                        var listSuppNames = GetSupplieNamesFromCreditNote(orderID);
                        var xeroOrderDTSnumber = orderDal.getXeroDTSID(orderID);

                        var body = "Hi , <br/>  RMA Request  for this order number :"
                            + xeroOrderDTSnumber + "  <br/> CreditNote Number is :" + creditNoteObj.XeroCreditNoteID;

                        if (!string.IsNullOrEmpty(creditNoteObj.Notes))
                        {
                            body = body + "<br/>" + creditNoteObj.Notes;
                        }

                        foreach (var item in listSuppNames)
                        {
                            var suppEmails = ordersendEmail.GetSupplierEmails(item, 2);  //reading credit note send email supplier

                            var from = "info@deltonesolutions.com.au";
                            var fromName = "Deltonesolutions";
                            foreach (var suuEmail in suppEmails)
                            {
                                creditEmailHandler.SendCreditNoteEmail(creditNoteId, "", "", from, fromName, suuEmail.SupplierEmailAddress,
                                    "", "", subject, body, true, null);
                                var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                                var previousValues = "";
                                var newvalues = body;
                                SqlConnection conn = new SqlConnection();
                                conn.ConnectionString = connectionstring;
                                var ActionType = "Email Credit Note";
                                var primaryKey = Convert.ToInt32(creditNoteObj.OrderId);
                                var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                                var columnName = "Email";
                                var talbeName = "CreditNote";
                                new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
                    columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(contact.CompanyId));
                                UpdateRMACreditXeroInfo(creditNoteId);
                            }
                        }

                    }

                }

            }
        }

        private List<string> GetSupplieNamesFromCreditNote(int orderId)
        {
            var listSupNames = new List<string>();
            var orderSuppDeltems = orderdal.getOrderSuppDeltems(orderId);
            var splitNameAndValue = orderSuppDeltems.Split('|');
            foreach (var item in splitNameAndValue)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var name = item.Split(',')[0];
                    if (string.IsNullOrEmpty(name))
                    {
                        name = item.Split(',')[1];
                        listSupNames.Add(name);
                    }
                    else
                    {
                        listSupNames.Add(name);
                    }
                }
            }

            return listSupNames;

        }



        //End Method Approve Status of the Order

        public String getOrderGuid(int CreditNoteID)
        {
            String output = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "select XeroGuid from CreditNotes where CreditNote_ID=" + CreditNoteID;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        output = sdr["XeroGuid"].ToString();

                    }
                }
            }
            conn.Close();

            return output;
        }


        //This Method Change the Order Status Given by OrderID
        protected int ChangeOrderStatus(int CreditNoteID)
        {

            int RowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateOrderStatus = "Update CreditNotes SET Status='APPROVED' WHERE CreditNote_ID=@CreditNoteID";
            SqlCommand cmd = new SqlCommand(strUpdateOrderStatus, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", CreditNoteID);
            try
            {
                conn.Open();
                RowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            return RowEffected;
        }
        //End Method Change the Order Status given by 


        //Company OwnerShip details dropdown List Population
        protected void PopulateDropDownList(int CompanyID, int IOrderID)
        {
            if (!IsPostBack)
            {

                String strLoggedUsers = String.Empty;
                DataTable subjects = new DataTable();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName,LoginID from dbo.Logins WHERE LoginID NOT IN (4,  9, 8,20,21,22) AND Active = 'Y'", conn);
                    adapter.Fill(subjects);
                    ddlUsers.DataSource = subjects;
                    ddlUsers.DataTextField = "FullName";
                    ddlUsers.DataValueField = "LoginID";
                    ddlUsers.DataBind();
                }

                //Get the Current Account OwnerShip
                String strAccountOwnerShip = CompanyOwnerShip(CompanyID);

                OrderDAL odal = new OrderDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

                String CreatedBy = odal.getCreditNoteCreatedBy(IOrderID);
                if (ddlUsers.Items.FindByText(CreatedBy) != null)
                    ddlUsers.Items.FindByText(CreatedBy).Selected = true;
            }

        }

        /// <summary>
        /// Handles the Split Commission
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="CompanyID"></param>
        /// <param name="LoggedID"></param>
        /// <param name="commissionvalue"></param>
        protected void SplitCommission(int OrderID, int CompanyID, int LoggedID, float commissionvalue)
        {

            String strOutPut = String.Empty;
            CreditNotesDAL CreditDAL = new CreditNotesDAL(connString);
            String CreditNote_CreatedDateTime = CreditDAL.CreditNoteCreatedDate(OrderID);

            String OrderStatus = String.Empty;

            OrderStatus = getOrderStatus(OrderID.ToString());

            int CompnyOwnerShip = Int32.Parse(CompanyOwnerShip(CompanyID));
            if (LoggedID == CompnyOwnerShip)
            {

                orderdal.WriteCommision(OrderID, LoggedID, commissionvalue, "NO", LoggedID.ToString(), Convert.ToDateTime(CreditNote_CreatedDateTime), OrderStatus);
            }
            else
            {
                float newCommisionValue = (float)Math.Round((commissionvalue * 0.5), 2);
                orderdal.WriteCommision(OrderID, LoggedID, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreditNote_CreatedDateTime), OrderStatus);
                orderdal.WriteCommision(OrderID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreditNote_CreatedDateTime), OrderStatus);
            }

        }

        protected String getOrderStatus(String OID)
        {
            String OrderStatus = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Status FROM dbo.Orders WHERE OrderID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderStatus = sdr["Status"].ToString();
                        }
                    }

                }
                conn.Close();

            }

            return OrderStatus;
        }


        /// <summary>
        /// This Method Update the Commission Entry in the DB
        /// </summary>
        /// <param name="OrderID"></param>
        /// <param name="CompanyID"></param>
        /// <param name="LoggedID"></param>
        /// <param name="commissionvalue"></param>
        protected void UpdateCommissions(int OrderID, int CompanyID, int LoggedID, float commissionvalue)
        {
            //Remove the Old Entry First
            String output = orderdal.RemoveCommisionEntry(OrderID);
            CreditNotesDAL CreditDAL = new CreditNotesDAL(connString);
            String CreditNote_CreatedDateTime = CreditDAL.CreditNoteCreatedDate(OrderID);

            String OrderStatus = String.Empty;

            OrderStatus = getOrderStatus(OrderID.ToString());

            int CompnyOwnerShip = Int32.Parse(CompanyOwnerShip(CompanyID));
            if (LoggedID == CompnyOwnerShip)
            {

                orderdal.WriteCommision(OrderID, LoggedID, commissionvalue, "NO", LoggedID.ToString(), Convert.ToDateTime(CreditNote_CreatedDateTime), OrderStatus);
            }
            else
            {
                float newCommisionValue = (float)Math.Round((commissionvalue * 0.5), 2);
                orderdal.WriteCommision(OrderID, LoggedID, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreditNote_CreatedDateTime), OrderStatus);
                orderdal.WriteCommision(OrderID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreditNote_CreatedDateTime), OrderStatus);
            }

        }


        //Select the Company Owner
        protected String CompanyOwnerShip(int CompanyID)
        {
            String strOwnershipAdmin = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select  l.FirstName,l.LoginID,c.OwnershipAdminID from Companies c ,dbo.Logins l where  c.OwnershipAdminID=l.LoginID and c.CompanyID=" + CompanyID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            //strOwnershipAdmin = sdr["FirstName"] + ":" + sdr["OwnershipAdminID"];
                            strOwnershipAdmin = sdr["OwnershipAdminID"].ToString();
                        }
                    }
                    conn.Close();

                }

            }

            return strOwnershipAdmin;
        }
        //End Selecting the Company owner

        //End Company OwnerShip details dropdown list Population

        protected void getCustomerDetails(String CustID)
        {
            String strCompanyName = "";
            String CustomerName = "";
            String CustomerBillAddressLine1 = "";
            String CustomerBillAddressLine2 = "";
            String CustomerBillCity = "";
            String CustomerBillPostcode = "";
            String CustomerBillState = "";

            String CustomerShipAddressLine1 = "";
            String CustomerShipAddressLine2 = "";
            String CustomerShipCity = "";
            String CustomerShipPostcode = "";
            String CustomerShipState = "";

            String CustomerContactNumber = "";
            String CustomerEmail = "";


            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT CT.*, CP.CompanyName FROM dbo.Contacts CT, dbo.Companies CP WHERE CT.CompanyID = CP.CompanyID AND ContactID = " + CustID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strCompanyName = sdr["CompanyName"].ToString();
                            CustomerName = sdr["FirstName"].ToString() + ' ' + sdr["LastName"].ToString();
                            CustomerBillAddressLine1 = sdr["STREET_AddressLine1"].ToString();
                            CustomerBillAddressLine2 = sdr["STREET_AddressLine2"].ToString();
                            CustomerBillCity = sdr["STREET_City"].ToString();
                            CustomerBillPostcode = sdr["STREET_PostalCode"].ToString();
                            CustomerBillState = sdr["STREET_Region"].ToString();

                            CustomerShipAddressLine1 = sdr["POSTAL_AddressLine1"].ToString();
                            CustomerShipAddressLine2 = sdr["POSTAL_AddressLine2"].ToString();
                            CustomerShipCity = sdr["POSTAL_City"].ToString();
                            CustomerShipPostcode = sdr["POSTAL_PostalCode"].ToString();
                            CustomerShipState = sdr["POSTAL_Region"].ToString();

                            CustomerContactNumber = sdr["DEFAULT_AreaCode"].ToString() + ' ' + sdr["DEFAULT_Number"].ToString();
                            CustomerEmail = sdr["Email"].ToString();

                        }
                    }
                    conn.Close();
                }
            }

            CompanyNameDIV.InnerHtml = strCompanyName.ToUpper();
            ContactInfo.InnerHtml = CustomerName;
            StreetAddressLine1.InnerHtml = CustomerBillAddressLine1 + ' ' + CustomerBillAddressLine2;
            StreetAddressLine2.InnerHtml = CustomerBillCity.ToUpper() + ' ' + CustomerBillState.ToUpper() + ' ' + CustomerBillPostcode.ToUpper();
            DeliveryContact.InnerHtml = CustomerName;
            DeliveryCompany.InnerHtml = strCompanyName.ToUpper();
            DeliveryAddressLine1.InnerHtml = CustomerShipAddressLine1 + ' ' + CustomerShipAddressLine2;
            DeliveryAddressLine2.InnerHtml = CustomerShipCity.ToUpper() + ' ' + CustomerShipState.ToUpper() + ' ' + CustomerShipPostcode.ToUpper();
            ContactandEmail.InnerHtml = "Telephone: " + CustomerContactNumber + "  |  Email: " + CustomerEmail;

        }


        /// <summary>
        /// Update Supplier Notes(Credit Notes)
        /// </summary>
        /// <param name="OrderID">Credit NotID</param>
        /// <param name="xeroInvoiceNumber">xeroInvoiceNumber</param>
        /// <param name="di">Notes Dictionary Object</param>
        protected void UpdateSupplierNotes(int OrderID, String xeroInvoiceNumber, Dictionary<String, String> di)
        {
            //Remove the Previous Entry
            if (RemoveSupplierNotes(OrderID) > 0)
            {
                CreateSupplerNotes(OrderID, xeroInvoiceNumber, di);
            }
        }
        //End Function Update Supplier Notes

        /// <summary>
        /// Remove Supplier Notes 
        /// </summary>
        /// <param name="OrderID">Credit Note ID</param>
        /// <returns></returns>
        protected int RemoveSupplierNotes(int CreditNoteID)
        {
            int intRowsEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStatement = "Delete from dbo.CN_SupplierNotes where CreditNoteID=@creditnoteid "; //Modified here Remove if only Type='Credit'
            SqlCommand cmd = new SqlCommand(strSQLInsertStatement, conn);
            cmd.Parameters.AddWithValue("@creditnoteid", CreditNoteID);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            return intRowsEffected;
        }

        /// <summary>
        /// Creates Supplier Notes according to the Dictionary Object
        /// </summary>
        /// <param name="intOrederID">Credit NoteID</param>
        /// <param name="xeroInvoiceNumber">xeroInvoice Number</param>
        /// <param name="di">Notes Dictionary Object</param>
        protected void CreateSupplerNotes(int CreditNoteID, String xeroInvoiceNumber, Dictionary<String, String> di)
        {
            foreach (var item in di)
            {

                AddSupplierNotes(CreditNoteID, item.Key, item.Value, xeroInvoiceNumber);
            }

        }


        /// <summary>
        /// Fetch Supplier Notes given by Credit Note ID(Credit Notes)
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        protected String FetchSupplierNotes(int CreditNoteID)
        {
            String strNotes = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "select * from dbo.CN_SupplierNotes  where CreditNoteID=" + CreditNoteID;
            //String strSqlOrderStmt ="select s.Suppliername,s.Notes from SupplierNotes s,CreditNotes cn where s.OrderID=cn.OrderID and Type='Credit' and cn.CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strNotes = strNotes + sdr["Suppliername"].ToString() + ":" + sdr["Notes"].ToString();
                        strNotes = strNotes + "|";
                    }
                }
            }
            conn.Close();


            return strNotes;
        }

        //This function returns the Reference information
        protected String FetchReferenceInfo(int CreditNoteID)
        {
            String strNotes = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "SELECT  CreditNoteReason FROM  CreditNotes WHERE CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strNotes = sdr["CreditNoteReason"].ToString();
                    }
                }
            }
            conn.Close();
            return strNotes;
        }

        //This function returns the Reference information
        protected String FetchTypeOfCall(int OrderID)
        {
            String strNotes = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "SELECT TypeOfCall FROM dbo.Orders where OrderID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strNotes = sdr["TypeOfCall"].ToString();
                    }
                }
            }
            conn.Close();


            return strNotes;
        }


        /// <summary>
        /// Add Supplier Notes 
        /// </summary>
        /// <param name="intOrderID">Credit Note ID</param>
        /// <param name="strSupplierName">Supplier Name</param>
        /// <param name="strNotes">Note String </param>
        /// <param name="xeroInvoiceNumber">Xero Invoice Number</param>
        /// <returns></returns>
        protected int AddSupplierNotes(int intCreditNoteID, String strSupplierName, String strNotes, String xeroInvoiceNumber)
        {
            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //Modified here Add Type ('Credit','SalesOrder')
            String strSQLInsertStatement = "INSERT INTO dbo.CN_SupplierNotes  values(@OrderID,@SupplierName,@Notes,@xeroInvNumber,@Type)";
            SqlCommand cmd = new SqlCommand(strSQLInsertStatement, conn);
            cmd.Parameters.AddWithValue("@CreditNoteID", intCreditNoteID);
            cmd.Parameters.AddWithValue("@SupplierNamee", strSupplierName);
            cmd.Parameters.AddWithValue("@Notes", strNotes);
            cmd.Parameters.AddWithValue("@XeroInvoiceNumber", xeroInvoiceNumber);

            try
            {
                conn.Open();
                rowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }

            return rowsEffected;
        }



        protected int RemoveProItems(int OrderID)
        {

            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strDELTETStmt = "DELETE FROM OrderProItems WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strDELTETStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }
            return intRowsEffected;
        }


        //This function Crerate the Promotional Items
        protected void CreateProItems(int intOrderID, String strProItems)
        {
            String[] ProItems = strProItems.Split('|');
            String[] ProItem;
            for (int i = 0; i < ProItems.Length; i++)
            {
                if (!String.IsNullOrEmpty(ProItems[i]))
                {
                    ProItem = ProItems[i].Split(',');
                    if (i == 0)
                    {
                        float ShippingCost;
                        if (String.IsNullOrEmpty(ProItem[3]))
                        {
                            ShippingCost = 0;
                            AddProItems(intOrderID, 0, ProItem[0], float.Parse(ProItem[1].ToString()), Int32.Parse(ProItem[2]), 0, ProItem[4].ToString());
                        }
                        else
                        {
                            AddProItems(intOrderID, 0, ProItem[0], float.Parse(ProItem[1].ToString()), Int32.Parse(ProItem[2]), float.Parse(ProItem[3].ToString()), ProItem[4].ToString());
                        }

                    }
                    if (i != 0)
                    {
                        float ShipCost;
                        if (String.IsNullOrEmpty(ProItem[4]))
                        {
                            ShipCost = 0;
                            AddProItems(intOrderID, 0, ProItem[1], float.Parse(ProItem[2].ToString()), Int32.Parse(ProItem[3]), 0, ProItem[4].ToString());
                        }
                        else
                        {
                            AddProItems(intOrderID, 0, ProItem[1], float.Parse(ProItem[2].ToString()), Int32.Parse(ProItem[3]), float.Parse(ProItem[4].ToString()), ProItem[5].ToString());
                        }
                    }

                }
            }
        }


        //End creating Promotional Items

        //This method Update Promotional Items 
        protected void UpdatePromotionalItems(int intOrderId, String strProItems)
        {
            //Rermove Previous and Add New promotional Item
            RemoveProItems(intOrderId);

            CreateProItems(intOrderId, strProItems);

        }
        //End Method Update promotional Items


        //This Method Fetch the Promotional Items 
        protected String FetchProItems(int OrderId)
        {
            String ProItems = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "select * from dbo.OrderProItems where OrderID=@OrderID";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@OrderID", OrderId);
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ProItems = ProItems + sdr["PromoItem"].ToString() + ":" + sdr["ProCost"].ToString() + ":" + sdr["PromoQty"].ToString() + ":" + sdr["ShippingCost"].ToString() + ":" + sdr["PromoCode"].ToString();
                        ProItems = ProItems + "|";
                    }
                }
                conn.Close();
            }

            return ProItems;
        }
        //End Method Fetch Promotional Items


        //Modified here Add PromoCode here 
        //This Method Add Promotional Items to the Table
        protected int AddProItems(int intOrderID, int ProID, String strProItem, float proCost, int proQty, float ShippingCost, String promoCode)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLUpdateStmt = "insert into dbo.OrderProItems values(@OrderID,@PromoID,@ProItem,@Qty,@ShipCost,@ProCost,@PromoCode);";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", intOrderID);
            cmd.Parameters.AddWithValue("@PromoID", ProID);
            cmd.Parameters.AddWithValue("@ProItem", strProItem);
            cmd.Parameters.AddWithValue("@ProCost", proCost);
            cmd.Parameters.AddWithValue("@Qty", proQty);
            cmd.Parameters.AddWithValue("@ShipCost", ShippingCost);
            cmd.Parameters.AddWithValue("@PromoCode", promoCode);

            try
            {
                conn.Open();
                intRowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }
            return intRowsEffected;

        }
        //End Method Add Promotional Items to the Table

        //Modified Sumudu Kodikara 22/04/2015
        protected int updateOrderStatus(String strInvoiceNumber, String strGuid, int OrderID)
        {

            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLUpdateStmt = "UPDATE dbo.Orders SET XeroGUID=@Guid,XeroInvoiceNumber=@XeroInvoiceNumber WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@Guid", strGuid);
            cmd.Parameters.AddWithValue("@XeroInvoiceNumber", strInvoiceNumber);

            try
            {
                conn.Open();
                rowsEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                Response.Write(ex.Message.ToString());
            }
            return rowsEffected;

        }

        //This Method Create Reposirity in the Xero System - Originally in XeroIntegration.cs
        public Repository CreateRepository()
        {

            try
            {

                String strpath = @"SSLCert\public_privatekey.pfx";
                //Load the proivate Certificate from the DISK using  the Password to Create it 
                String CertficatePath = Server.MapPath(strpath);
                //X509Certificate2 privateCertificate = new X509Certificate2(@"C:\SSLCertificate\public_privatekey.pfx", "@dmin123");
                X509Certificate2 privateCertificate = new X509Certificate2(CertficatePath, "@dmin123", X509KeyStorageFlags.MachineKeySet);
                IOAuthSession consumerSession = new XeroApiPrivateSession("MyAPITestSoftware", "AKS4LTBIHGV9NHNIKDKZI3GCFOYTLQ", privateCertificate);
                consumerSession.MessageLogger = new DebugMessageLogger();
                return new Repository(consumerSession);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace.ToString() + ":" + ex.Message.ToString());
                Session["XeroMsg"] = ex.StackTrace.ToString() + ":" + ex.Message.ToString();
                return null;
            }
        }



        protected void btnDash_Click(object sender, EventArgs e)
        {

            // Session["orderIDfile"] = orderId;
            Response.Redirect("dashboard1.aspx");



        }
        protected void btnaccountDash_Click(object sender, EventArgs e)
        {
            var comId = Request.QueryString["Compid"];
            Response.Redirect("CompanyInfoLead.aspx?companyid=" + comId);
        }

        [System.Web.Services.WebMethod]
        public static CreditNoteRMAFaultyGoods GetRMAFaultyGoods(CreditNoteRMAFaultyGoods rmaObj)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var creditId = 0;
            var comID = 0;
            var conId = 0;

            if (!string.IsNullOrEmpty(rmaObj.CreditNoteId))
                creditId = Convert.ToInt32(rmaObj.CreditNoteId);
            if (!string.IsNullOrEmpty(rmaObj.CompanyId))
                comID = Convert.ToInt32(rmaObj.CompanyId);
            if (!string.IsNullOrEmpty(rmaObj.ContactId))
                conId = Convert.ToInt32(rmaObj.ContactId);

            if (creditId > 0)
            {


                var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());

                try
                {

                    rmaObj = new CreditNoteRMAHandler(cs).GetRMAFaultyGoods(rmaObj);
                    //CreateAuditRecord(orderID, comId, loggedInUserId, rep.Complaint, rep.OutCome, rep.Question);
                }
                catch (Exception ex)
                {
                    var _logger1 = LogManager.GetLogger(typeof(Order));
                    _logger1.Error("Error RMA Faulty Goods get details" + ex);
                }

            }

            return rmaObj;

        }


        [System.Web.Services.WebMethod]
        public static void UpdateRMAFaultyGoods(CreditNoteRMAFaultyGoods rmaObj)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var creditId = 0;
            var comID = 0;
            var conId = 0;

            if (!string.IsNullOrEmpty(rmaObj.CreditNoteId))
                creditId = Convert.ToInt32(rmaObj.CreditNoteId);
            if (!string.IsNullOrEmpty(rmaObj.CompanyId))
                comID = Convert.ToInt32(rmaObj.CompanyId);
            if (!string.IsNullOrEmpty(rmaObj.ContactId))
                conId = Convert.ToInt32(rmaObj.ContactId);

            if (creditId > 0)
            {


                var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());

                try
                {

                    new CreditNoteRMAHandler(cs).UpdateRMAFORFaultyGoods(rmaObj);
                    CreateAuditRecord(creditId, comID, loggedInUserId, rmaObj.BatchNumber, rmaObj.ModelNumber, rmaObj.ErrorMessage);
                }
                catch (Exception ex)
                {
                    var _logger1 = LogManager.GetLogger(typeof(Order));
                    _logger1.Error("Error RMA Faulty Goods uupdate details" + ex);
                }

            }

        }

        private static void CreateAuditRecord(int orderId, int companyID, int userID, string BatchNumber,
           string ModelNumber, string ErrorMessage)
        {
            var columnName = "All Column";
            var talbeName = "RMATracking";
            var ActionType = "Created";
            int primaryKey = orderId;



            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;


            var newvalues = "Credit Note  Id " + orderId + "RMA Faulty Details. Batch "
                + BatchNumber + ". Model " + ModelNumber + ". Error " + ErrorMessage;



            var loggedInUserId = userID;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;
            var previousValues = "";
            var strCompanyID = companyID;

            new DeltoneCRM_DAL.CompanyDAL(CONNSTRING).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));
        }

        [System.Web.Services.WebMethod]
        public static string ReadFalutyData(int creditId)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            return new CreditNoteRMAHandler(cs).GetRAMFAULTYGoodsByCreditId(creditId);

        }

        [System.Web.Services.WebMethod]
        public static void CallRMAEmail(string creditNoteId, string suppName, string contactId, string companyId)
        {
            RMAEmailSupplierManage(creditNoteId, suppName, companyId);

        }

        private static void RMAEmailSupplierManage(string creditNoteId, string suppName, string companyId)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var creditorderID = Convert.ToInt32(creditNoteId);
            var typeEmail = 2; //RMA Request Email
            var suppEmails = new OrderSendEmailDAL(cs).GetSupplierEmails(suppName, typeEmail);
            ContactDAL cdal = new ContactDAL(cs);
            var orderDal = new OrderDAL(cs);
            var creditNoteDal = new CreditNotesDAL(cs);
            var ordersendEmail = new OrderSendEmailDAL(cs);
            var creditNoteObj = creditNoteDal.getCreditNoteObj(creditorderID);
            var creditEmailHandler = new CreditNoteRMAHandler(cs);
            var comadal = new CompanyDAL(cs);
            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());

            ILog _logger = LogManager.GetLogger(typeof(WebForm1));
            if (creditNoteObj.IsAvail)
            {
                //  var contact = cdal.GetContactByContactId(contactId);
                if (!string.IsNullOrEmpty(creditNoteObj.Reason))
                {
                    var xeroOrderDTSnumber = orderDal.getXeroDTSID(Convert.ToInt32(creditNoteObj.OrderId));
                    var itemsByCreditNote = creditNoteDal.getCreditNoteItemsByCreditId(creditorderID);
                    var companyName = comadal.getCompanyNameByID(companyId);
                    var itemsBySuname = (from el in itemsByCreditNote where el.SupplierName == suppName select el).ToList();


                    var rma = new CreditNoteRMAFaultyGoods();
                    rma.CreditNoteId = creditNoteId;
                    rma.SupplierName = suppName;
                    rma = new CreditNoteRMAHandler(cs).GetRMAFaultyGoods(rma);

                    var faultybatchnumber = "";
                    if (!string.IsNullOrEmpty(rma.BatchNumber))
                        faultybatchnumber = "  <br/> Batch Number is : " + rma.BatchNumber;

                    var faultymodelnumber = "";
                    if (!string.IsNullOrEmpty(rma.ModelNumber))
                        faultymodelnumber = "  <br/> Model Number is : " + rma.ModelNumber;
                    var faultyerrormessage = "";
                    if (!string.IsNullOrEmpty(rma.ErrorMessage))
                        faultyerrormessage = " <br/> Error Message is : " + rma.ErrorMessage;

                    var faultynotes = "";
                    if (!string.IsNullOrEmpty(rma.FaultyNotes))
                        faultynotes = "  <br/> Note is : " + rma.FaultyNotes;

                    var subject = "RMA Request for " + creditNoteObj.Reason + " " + xeroOrderDTSnumber;
                    String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";

                    var bodytitle = "Hi  , <br/>  RMA Request  for this Order Number :"
                    + xeroOrderDTSnumber + "  <br/> Credit Note Number is :" + creditNoteObj.XeroCreditNoteID +
                  "  <br/> Company Name is :" + companyName;

                    if (!string.IsNullOrEmpty(faultymodelnumber))
                        bodytitle = bodytitle + faultymodelnumber;

                    if (!string.IsNullOrEmpty(faultybatchnumber))
                        bodytitle = bodytitle + faultybatchnumber;

                    if (!string.IsNullOrEmpty(faultyerrormessage))
                        bodytitle = bodytitle + faultyerrormessage;

                    if (!string.IsNullOrEmpty(faultynotes))
                        bodytitle = bodytitle + faultynotes;

                    var body = CreateEmailTemplateForSupplier(creditorderID, itemsBySuname, creditNoteObj, bottomBanner, bodytitle);
                    _logger.Info("RMAEmailSupplierManage : Send email to Supplier:" + bodytitle);
                    if (!string.IsNullOrEmpty(creditNoteObj.Notes))
                    {
                        // body = body + "<br/>" + creditNoteObj.Notes;
                    }
                    try
                    {
                        var from = "info@deltonesolutions.com.au";
                        var fromName = "Deltonesolutions";
                        foreach (var item in suppEmails)
                        {

                            creditEmailHandler.SendCreditNoteEmailAlternative(creditorderID, companyId,
                                suppName, from, fromName, item.SupplierEmailAddress,
                                 "krit@deltonesolutions.com.au", "", subject, body, true, null);
                            var previousValues = "";
                            var newvalues = body;

                            newvalues = Regex.Replace(newvalues, @"(<img\/?[^>]+>)", @"",
        RegexOptions.IgnoreCase);

                            if (newvalues.Length > 1000)
                                newvalues = newvalues.Substring(0, 1000);
                            SqlConnection conn = new SqlConnection();
                            conn.ConnectionString = cs;
                            var ActionType = "Email RMA";
                            var primaryKey = Convert.ToInt32(creditNoteObj.OrderId);
                            var columnName = "Email";
                            var talbeName = "CreditNote";
                            new DeltoneCRM_DAL.CompanyDAL(cs).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
                columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(companyId));
                            UpdateRMACreditXeroInfo(creditorderID);

                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(" Error Occurred SendMail method RMAEmailSupplierManage :" + ex.Message);
                    }

                }

            }
        }


        [System.Web.Services.WebMethod]
        public static void RMAReceivedEmailTOCustomer(string creditNoteId, string suppName, string contactId, string companyId)
        {
            sentRMATOCustomer(creditNoteId, suppName, companyId);
        }
        [System.Web.Services.WebMethod]
        public static void SendPoBoxInfoTOCustomer(string creditNoteId, string suppName, string contactId, string companyId)
        {
            var creId = Convert.ToInt32(creditNoteId);
            CreatePOBOXEmailTemplateForCustomer(creId, suppName, companyId);
        }

        [System.Web.Services.WebMethod]
        public static void RMAUpdateMe(DeltoneCRM_DAL.CreditNoteRMAHandler.RMAUpdate obj)
        {
            UpdateRMANUmber(obj);
        }

        private static void UpdateRMANUmber(DeltoneCRM_DAL.CreditNoteRMAHandler.RMAUpdate obj)
        {
            var CONNSTRING = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = CONNSTRING;



            if (new CreditNoteRMAHandler(CONNSTRING).GetRMAByCreditNoteIdAndSuppName(Convert.ToInt32(obj.CreId), obj.SuppName) == 0)
                new CreditNotesDAL(CONNSTRING).WriteSupplierIntoRMA(obj.CreId, obj.SuppName);

            var rmaId = new CreditNoteRMAHandler(CONNSTRING).GetRMAByCreditNoteIdAndSuppName(Convert.ToInt32(obj.CreId), obj.SuppName);
            var RMAID = rmaId.ToString();
            String SqlStmt = @"UPDATE dbo.RMATracking SET SentToSupplier=@SentToSupplier, 
                      SentToSupplierDateTime=@SentToSupplierDateTime, ApprovedRMA=@ApprovedRMA, ApprovedRMADateTime=@ApprovedRMADateTime,
                      CreditInXero=@CreditInXero, CreditInXeroDateTime=@CreditInXeroDateTime, RMAToCustomer=@RMAToCustomer, 
                      RMAToCustomerDateTime=@RMAToCustomerDateTime, AdjustedNoteFromSupplier=@AdjustedNoteFromSupplier, 
                      AdjustedNoteFromSupplierDateTime=@AdjustedNoteFromSupplierDateTime, Status=@Status, SupplierRMANumber=@SupplierRMANumber, 
                TrackingNumber=@TrackingNumber , InHouse=@inHouse,Notes=@notes WHERE CreditNoteID =@crId AND SupplierName=@suppName";
            SqlCommand cmd = new SqlCommand(SqlStmt, conn);

            cmd.Parameters.Add("@suppName", SqlDbType.NVarChar).Value = obj.SuppName;
            cmd.Parameters.Add("@crId", SqlDbType.Int).Value = obj.CreId;
            String STS = obj.STS;
            String STSWrite = String.Empty;
            //DateTime STSDateTime = new DateTime();
            if (STS == "True")
            {
                STSWrite = "1";
            }
            else
            {
                STSWrite = "0";
            }

            String inHouse = obj.INHouse;
            String inHouseWrite = String.Empty;
            //DateTime STSDateTime = new DateTime();
            if (inHouse == "True")
            {
                inHouseWrite = "1";
            }
            else
            {
                inHouseWrite = "0";
            }

            cmd.Parameters.AddWithValue("@inHouse", inHouseWrite);

            String ARMA = obj.ARMA;
            String ARMAWrite = String.Empty;

            if (ARMA == "True")
            {
                ARMAWrite = "1";
            }
            else
            {
                ARMAWrite = "0";
            }

            String CIX = obj.CIX;
            String CIXWrite = String.Empty;

            if (CIX == "True")
            {
                CIXWrite = "1";
            }
            else
            {
                CIXWrite = "0";
            }

            String RTC = obj.RTC;
            String RTCWrite = String.Empty;

            if (RTC == "True")
            {
                RTCWrite = "1";
            }
            else
            {
                RTCWrite = "0";
            }

            String ANFS = obj.ANFS;
            String ANFSWrite = String.Empty;

            if (ANFS == "True")
            {
                ANFSWrite = "1";
            }
            else
            {
                ANFSWrite = "0";
            }

            String NewStatus = String.Empty;
            //if (STS == "True" && ARMA == "True" && CIX == "True" && RTC == "True" && ANFS == "True")
            //{
            //    NewStatus = "COMPLETED";
            //}
            //else
            //{

            //}
            NewStatus = new CreditNotesDAL(CONNSTRING).RMAStatusByCreditIdAndSuppllier(obj.CreId, obj.SuppName);

            if (inHouse == "True")
            {
                NewStatus = "COMPLETED";
            }
            else

                if (ANFSWrite == "1")
                {
                    NewStatus = "COMPLETED";
                }
                else
                    if (obj.chk_Completed == "True")
                        NewStatus = "COMPLETED";

            var notesValues = obj.Notes;

            cmd.Parameters.AddWithValue("@notes", notesValues);

            cmd.Parameters.AddWithValue("@SentToSupplier", STSWrite);
            if (STS == "True")
            {
                if (checkIfSTSHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@SentToSupplierDateTime", DateTime.Today);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SentToSupplierDateTime", DateTime.Parse(checkIfSTSHasValue(RMAID)));
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@SentToSupplierDateTime", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@ApprovedRMA", ARMAWrite);

            if (ARMA == "True")
            {
                if (checkIfARMAHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@ApprovedRMADateTime", DateTime.Today);
                    //  sentRMATOCustomer(RMAID);  // sent email to customer once RMA Approved
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ApprovedRMADateTime", DateTime.Parse(checkIfARMAHasValue(RMAID)));
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@ApprovedRMADateTime", DBNull.Value);
            }


            cmd.Parameters.AddWithValue("@CreditInXero", CIXWrite);

            if (CIX == "True")
            {
                if (checkIfCIXHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@CreditInXeroDateTime", DateTime.Today);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CreditInXeroDateTime", DateTime.Parse(checkIfCIXHasValue(RMAID)));
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@CreditInXeroDateTime", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@RMAToCustomer", RTCWrite);

            if (RTC == "True")
            {
                if (checkIfRTCHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@RMAToCustomerDateTime", DateTime.Today);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RMAToCustomerDateTime", DateTime.Parse(checkIfRTCHasValue(RMAID)));
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@RMAToCustomerDateTime", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplier", ANFSWrite);

            if (ANFS == "True")
            {
                if (checkIfANFSHasValue(RMAID) == "")
                {
                    cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplierDateTime", DateTime.Today);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplierDateTime", DateTime.Parse(checkIfANFSHasValue(RMAID)));
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@AdjustedNoteFromSupplierDateTime", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Status", NewStatus);

            cmd.Parameters.AddWithValue("@SupplierRMANumber", obj.SRMAN);

            cmd.Parameters.AddWithValue("@TrackingNumber", obj.TN);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        private static void sentRMATOCustomer(string creditNoteIDVa, string suppname, string comId)
        {

            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);
            var orderDal = new OrderDAL(strConnectionString);
            var creditNoteDal = new CreditNotesDAL(strConnectionString);
            var ordersendEmail = new OrderSendEmailDAL(strConnectionString);
            var creditNoteID = Convert.ToInt32(creditNoteIDVa);
            var creditNoteObj = creditNoteDal.getCreditNoteObj(creditNoteID);
            var creditEmailHandler = new CreditNoteRMAHandler(strConnectionString);

            if (creditNoteObj.IsAvail)
            {

                var body = CreateEmailTemplateForCustomer(creditNoteID, suppname, comId);


                creditEmailHandler.UpdateRMISentToCustomer(creditNoteID);
                var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
                var previousValues = "";
                var newvalues = body;
                newvalues = Regex.Replace(newvalues, @"(<img\/?[^>]+>)", @"",
   RegexOptions.IgnoreCase);
                if (newvalues.Length > 1000)
                    newvalues = newvalues.Substring(0, 1000);
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = strConnectionString;
                var ActionType = "Email RMA";
                var primaryKey = Convert.ToInt32(creditNoteObj.OrderId);
                var columnName = "Email";
                var talbeName = "CreditNote";
                new DeltoneCRM_DAL.CompanyDAL(strConnectionString).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
    columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(comId));




            }
        }

        private static String checkIfSTSHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT SentToSupplierDatetime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["SentToSupplierDatetime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        private static String checkIfARMAHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT ApprovedRMADateTime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["ApprovedRMADateTime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        private DeltoneCRM.process.EditRMA.RMATrackingClient GetCreditNoteIdFromRAMTracking(int RmaId)
        {

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var rMATrackingClient = new DeltoneCRM.process.EditRMA.RMATrackingClient();

            String SqlStmt = "SELECT CreditNoteID,SupplierRMANumber,TrackingNumber FROM dbo.RMATracking WHERE RMAID = " + RmaId;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        rMATrackingClient.CreditNoteID = Convert.ToInt32(sdr["CreditNoteID"].ToString());
                        if (sdr["SupplierRMANumber"] != DBNull.Value)
                            rMATrackingClient.SupplierRMANumber = sdr["SupplierRMANumber"].ToString();
                        if (sdr["TrackingNumber"] != DBNull.Value)
                            rMATrackingClient.TrackingNumber = sdr["TrackingNumber"].ToString();
                    }
                }
                conn.Close();
            }


            return rMATrackingClient;
        }

        private static String checkIfCIXHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT CreditInXeroDateTime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["CreditInXeroDateTime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        private static String checkIfRTCHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT RMAToCustomerDateTime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["RMAToCustomerDateTime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }

        private static String checkIfANFSHasValue(String RID)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String output = String.Empty;

            String SqlStmt = "SELECT AdjustedNoteFromSupplierDateTime FROM dbo.RMATracking WHERE RMAID = " + RID;

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = SqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        output = sdr["AdjustedNoteFromSupplierDateTime"].ToString();
                    }
                }
                conn.Close();
            }


            return output;
        }


        private static string CreateEmailTemplateForCustomer(int creditNoteID, string suppName, string companyId)
        {
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);
            var orderDal = new OrderDAL(strConnectionString);
            var creditNoteDal = new CreditNotesDAL(strConnectionString);
            var ordersendEmail = new OrderSendEmailDAL(strConnectionString);
            var creditNoteObj = creditNoteDal.getCreditNoteObj(creditNoteID);
            var creditEmailHandler = new CreditNoteRMAHandler(strConnectionString);

            var itemsByCreditNote = creditNoteDal.getCreditNoteItemsByCreditId(creditNoteID);
            var rmaId = creditEmailHandler.GetSupplierRMANumberByCreditNoteIdAndSuppName(creditNoteID, suppName);

            var itemsBySuname = (from el in itemsByCreditNote where el.SupplierName == suppName select el).ToList();
            if (creditNoteObj.IsAvail)
            {

                return SendMail(creditNoteID, itemsBySuname, creditNoteObj, rmaId, suppName);
            }

            return "";
        }

        private static void CreatePOBOXEmailTemplateForCustomer(int creditNoteID, string suppName, string companyId)
        {
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);
            var orderDal = new OrderDAL(strConnectionString);
            var creditNoteDal = new CreditNotesDAL(strConnectionString);
            var ordersendEmail = new OrderSendEmailDAL(strConnectionString);
            var creditNoteObj = creditNoteDal.getCreditNoteObj(creditNoteID);
            var creditEmailHandler = new CreditNoteRMAHandler(strConnectionString);

            var itemsByCreditNote = creditNoteDal.getCreditNoteItemsByCreditId(creditNoteID);
            // var rmaId = creditEmailHandler.GetSupplierRMANumberByCreditNoteIdAndSuppName(creditNoteID, suppName);

            // var itemsBySuname = (from el in itemsByCreditNote where el.SupplierName == suppName select el).ToList();
            if (creditNoteObj.IsAvail)
            {
                var rmaId = creditNoteObj.XeroCreditNoteID;
                var body = SendPoBOX(creditNoteID, itemsByCreditNote, creditNoteObj, rmaId, suppName);
                creditEmailHandler.UpdateRMISentToCustomer(creditNoteID);

                var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
                var previousValues = "";
                var newvalues = body;
                newvalues = Regex.Replace(newvalues, @"(<img\/?[^>]+>)", @"",
   RegexOptions.IgnoreCase);
                if (newvalues.Length > 1000)
                    newvalues = newvalues.Substring(0, 1000);
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = strConnectionString;
                var ActionType = "Email RMA";
                var primaryKey = Convert.ToInt32(creditNoteObj.OrderId);
                var columnName = "Email";
                var talbeName = "CreditNote";
                new DeltoneCRM_DAL.CompanyDAL(strConnectionString).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
    columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(companyId));
            }
        }


        private static string SendPoBOX(int credtNotId, List<DeltoneCRM_DAL.CreditNotesDAL.CreditNoteItemEle> itmsBySupp,
            DeltoneCRM_DAL.CreditNotesDAL.CreditNoteObj creObj, string rmaId, string suppName)
        {
            ILog _logger = LogManager.GetLogger(typeof(WebForm1));

            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);

            var fromInfo = "info@deltonesolutions.com.au";
            var fromAddress = new MailAddress(fromInfo, "DELTONE SOLUTIONS PTY LTD");

            var BccAddress = new MailAddress("sentemails@deltonesolutions.com.au", "EMAIL SENT TO CUSTOMER");

            var contactId = Convert.ToInt32(creObj.ContactId);
            var contact = cdal.GetContactByContactId(contactId);
            var orderDal = new OrderDAL(strConnectionString);


            var firstTitle = "Dear " + contact.FirstName + ", <br/>";
            var secondString = @"Thank you for contacting Deltone Solutions Pty Ltd regarding Order #orderReplc#. 
                                    We are sorry to hear that you are having trouble with the items you purchased. <br/>";
            var orderID = Convert.ToInt32(creObj.OrderId);
            var xeroOrderDTSnumber = orderDal.getXeroDTSID(orderID);
            secondString = secondString.Replace("#orderReplc#", xeroOrderDTSnumber);


            var comFirstAndSecond = firstTitle + secondString;

            var toemail = contact.Email;
            var contactName = contact.FirstName;
            // toemail = "paruthi001@gmail.com";
            var toAddress = new MailAddress(toemail, contactName);

            const String fromPassword = "deltonerep1";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);

            var Subject = "RMA Request";

            String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";
            String body = HtmBodForSendingPOBOXToCustomer(itmsBySupp, rmaId, bottomBanner, comFirstAndSecond);
            _logger.Info("Message UpdateCredit SendPoBOX method" + body);

            AlternateView avHTML = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            LinkedResource btmbanner = new LinkedResource("C:\\temp\\DeltoneCRM\\DeltoneCRM\\Images\\bottom-banner-email.jpg");

            btmbanner.ContentId = "bottombanner";

            avHTML.LinkedResources.Add(btmbanner);

            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtpout.asia.secureserver.net",
                    Port = 80,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = netWorkCrdential
                };

                MailMessage deltonemail = new MailMessage(fromAddress, toAddress);
                deltonemail.Subject = Subject;
                deltonemail.IsBodyHtml = true;
                deltonemail.Body = body;
                deltonemail.Bcc.Add(BccAddress);
                deltonemail.Bcc.Add("krit@deltonesolutions.com.au");

                deltonemail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;

                deltonemail.AlternateViews.Add(avHTML);

                smtp.Send(deltonemail);
            }
            catch (Exception ex)
            {
                _logger.Error(" Error Occurred SendPoBOX method UpdateCredit :" + ex.Message);
                //Console.WriteLine(ex.Message.ToString() + " : " + ex.StackTrace.ToString());
                return "";

            }
            return body;
        }


        private static string SendMail(int credtNotId, List<DeltoneCRM_DAL.CreditNotesDAL.CreditNoteItemEle> itmsBySupp,
            DeltoneCRM_DAL.CreditNotesDAL.CreditNoteObj creObj, string rmaId, string suppName)
        {
            String strConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            ContactDAL cdal = new ContactDAL(strConnectionString);

            var fromInfo = "info@deltonesolutions.com.au";
            ILog _logger = LogManager.GetLogger(typeof(WebForm1));
            var fromAddress = new MailAddress(fromInfo, "DELTONE SOLUTIONS PTY LTD");

            var BccAddress = new MailAddress("sentemails@deltonesolutions.com.au", "EMAIL SENT TO CUSTOMER");

            var contactId = Convert.ToInt32(creObj.ContactId);
            var contact = cdal.GetContactByContactId(contactId);
            var orderDal = new OrderDAL(strConnectionString);


            var firstTitle = "Dear " + contact.FirstName + ", <br/>";
            var secondString = @"Thank you for contacting Deltone Solutions Pty Ltd regarding Order #orderReplc#. 
                                    We are sorry to hear that you are having trouble with the items you purchased. <br/>";
            var orderID = Convert.ToInt32(creObj.OrderId);
            var xeroOrderDTSnumber = orderDal.getXeroDTSID(orderID);
            secondString = secondString.Replace("#orderReplc#", xeroOrderDTSnumber);


            var comFirstAndSecond = firstTitle + secondString;

            var toemail = contact.Email;
            var contactName = contact.FirstName;
            //   toemail = "paruthi001@gmail.com";
            var toAddress = new MailAddress(toemail, contactName);

            const String fromPassword = "deltonerep1";
            var netWorkCrdential = new NetworkCredential("info@deltonesolutions.com.au", fromPassword);

            var Subject = "RMA Request";

            String bottomBanner = "<img src=\"cid:bottombanner\" height='105' width='550'>";
            String body = HTMLBody(credtNotId, bottomBanner, comFirstAndSecond, itmsBySupp, rmaId, suppName);
            _logger.Info("Message SendMail method" + body);

            AlternateView avHTML = AlternateView.CreateAlternateViewFromString(body, null, MediaTypeNames.Text.Html);
            LinkedResource btmbanner = new LinkedResource("C:\\temp\\DeltoneCRM\\DeltoneCRM\\Images\\bottom-banner-email.jpg");

            btmbanner.ContentId = "bottombanner";

            avHTML.LinkedResources.Add(btmbanner);

            try
            {
                var smtp = new SmtpClient
                {
                    Host = "smtpout.asia.secureserver.net",
                    Port = 80,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = netWorkCrdential
                };

                MailMessage deltonemail = new MailMessage(fromAddress, toAddress);
                deltonemail.Subject = Subject;
                deltonemail.IsBodyHtml = true;
                deltonemail.Body = body;
                deltonemail.Bcc.Add(BccAddress);
                deltonemail.Bcc.Add("krit@deltonesolutions.com.au");

                deltonemail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.Delay;

                deltonemail.AlternateViews.Add(avHTML);

                smtp.Send(deltonemail);



            }
            catch (Exception ex)
            {



                _logger.Error(" Error Occurred SendMail method UpdateCredit :" + ex.Message);
                //Console.WriteLine(ex.Message.ToString() + " : " + ex.StackTrace.ToString());
                return "";
            }

            return body;
        }


        private static string HtmBodForSendingPOBOXToCustomer(List<DeltoneCRM_DAL.CreditNotesDAL.CreditNoteItemEle> itmsBySupp,
            string rmaId, string bottomBanner,
            string titleSection)
        {
            string output = string.Empty;

            var secondSection = @"So that we can track this case we have created an RMA (return merchandise authorisation) 
                                in our system. Your unique RMA Number is " + rmaId + " ." +
                               "Please state this number in all future correspondence relating to this matter.";

            var listItems = new List<string>();
            var notInstructions = @"<br/>Please Note:
This RMA number is only approved for the next 14 days, goods must be returned within the required time frame and only the 
following listed below are approved for a return. Any prodcts returned that aren’t listed below or pass the 14 days will not be credited.";
            var suppAddress = "3.Please send the item to:  PO BOX 1041, CLAYTON SOUTH. VIC 3169 or A3 Hallmarc Business Park, 2A Westall Rd. CLAYTON SOUTH. VIC 3169.";

            var returnInst = @" <br/> <br/><strong> Return Instructions </strong> :<br/>
 1.Please package the item carefully so that no further damage is done during return shipping.
  <br/>2.Please clearly mark the outer carton with your RMA Number is " + rmaId +
" <br/>  " + suppAddress;


            output = "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 3.2//EN'><html>";
            output = output + "<body style='font-family:Calibri;'><table align='center' cellpadding='0' cellspacing='0' width='100%'><tr><td><table  cellpadding='0' cellspacing='0' width='780px' height='85px'>";

            output = output + titleSection + "<br/>" + secondSection + returnInst + "</td></tr><tr><td>&nbsp;</td></tr><tr><td><table style='width:720px;'><tr>";

            output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;color:red;'><span style:'color:red !important;font-family:Calibri;'>" + notInstructions + "</span></tr>";

            foreach (var item in itmsBySupp)
            {
                var descitem = item.Description.Trim();
                if (!listItems.Contains(descitem))
                {
                    listItems.Add(descitem);

                    output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;'>" + item.Quantity + "*" + item.Description + "( " + item.SupplierCode + " ) </td></tr>";
                }



            }


            var dbName = "Returns Department";

            Char spacer = (char)13;


            output = output + "<tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>";
            output = output + @"<tr><td style='font-family:Calibri;'>KIND REGARDS</td></tr><tr><td>&nbsp;
     </td></tr><tr><td style='font-family:Calibri;'> <strong>" + dbName
                                                      + "</strong></td></tr><tr><td style='font-family:Calibri;'><strong>1300 787 783</strong></td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>"
                                                      + bottomBanner + "</td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";

            return output;

        }

        private static String HTMLBody(int creditId, String bottomBanner,
            string titleSection, List<DeltoneCRM_DAL.CreditNotesDAL.CreditNoteItemEle> itmsBySupp,
            string rmaId, string suppName)
        {
            String output = String.Empty;

            var secondSection = @"So that we can track this case we have created an RMA (return merchandise authorisation) 
                                in our system. Your unique RMA Number is " + rmaId + " ." +
                                "Please state this number in all future correspondence relating to this matter.";

            var listItems = new List<string>();
            var notInstructions = @"<br/>Please Note:
This RMA number is only approved for the next 14 days, goods must be returned within the required time frame and only the 
following listed below are approved for a return. Any prodcts returned that aren’t listed below or pass the 14 days will not be credited.";
            var suppAddress = "3.A Courier Will be booked to pickup the goods.";
            if (suppName == "Ausjet")
                suppAddress = "3.Please send the item to:  Reply paid 75916, 247 South Street, Cleveland QLD 4163";
            else
                if (suppName == "TOD")
                    suppAddress = "3.Please send the item to:  Reply paid 84206, Returns Department, 10 Du Rietz Crt, SouthSide QLD 4570";

            var returnInst = @" <br/> <br/><strong> Return Instructions </strong> :<br/>
 1.Please package the item carefully so that no further damage is done during return shipping.
  <br/>2.Please clearly mark the outer carton with your RMA Number is " + rmaId +
" <br/>  " + suppAddress;

            var obtainMessage = "<br/>****Please obtain a tracking ID (number) from the post and email it to returns @deltonesolutions.com.au together with your RMA number";

            output = "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 3.2//EN'><html>";
            output = output + "<body style='font-family:Calibri;'><table align='center' cellpadding='0' cellspacing='0' width='100%'><tr><td><table  cellpadding='0' cellspacing='0' width='780px' height='85px'>";

            output = output + titleSection + "<br/>" + secondSection + returnInst + "</td></tr><tr><td>&nbsp;</td></tr><tr><td><table style='width:720px;'><tr>";
            output = output + "<td style='width:320px;text-align:left;font-family:Calibri;background-color:yellow;'><span style:'background-color:yellow !important;font-family:Calibri;'>" + obtainMessage + "</span></tr>";

            output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;color:red;'><span style:'color:red !important;font-family:Calibri;'>" + notInstructions + "</span></tr>";

            foreach (var item in itmsBySupp)
            {
                var descitem = item.Description.Trim();
                if (!listItems.Contains(descitem))
                {
                    listItems.Add(descitem);

                    output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;'>" + item.Quantity + "*" + item.Description + "( " + item.SupplierCode + " ) </td></tr>";
                }



            }


            var dbName = "Returns Department";

            Char spacer = (char)13;


            output = output + "<tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>";
            output = output + @"<tr><td style='font-family:Calibri;'>KIND REGARDS</td></tr><tr><td>&nbsp;
     </td></tr><tr><td style='font-family:Calibri;'> <strong>" + dbName
                                                      + "</strong></td></tr><tr><td style='font-family:Calibri;'><strong>1300 787 783</strong></td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family:Calibri;'>"
                                                      + bottomBanner + "</td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";

            return output;
        }



        private static string CreateEmailTemplateForSupplier(int credtNotId, List<DeltoneCRM_DAL.CreditNotesDAL.CreditNoteItemEle> itmsBySupp,
            DeltoneCRM_DAL.CreditNotesDAL.CreditNoteObj creObj, String bottomBanner, string title)
        {
            String output = String.Empty;
            var listItems = new List<string>();

            output = "<!DOCTYPE HTML PUBLIC ' -//W3C//DTD HTML 3.2//EN'><html>";
            var notInstructions = @" <br/> Items Listed Below.";
            output = output + "<body style='font-family:Calibri;'><table align='center' cellpadding='0' cellspacing='0' width='100%'><tr><td><table  cellpadding='0' cellspacing='0' width='780px' height='85px'>";
            output = output + title + notInstructions + "</td></tr><tr><td>&nbsp;</td></tr><tr><td><table style='width:720px;'><tr>";

            foreach (var item in itmsBySupp)
            {
                var descitem = item.Description.Trim();
                if (!listItems.Contains(descitem))
                {
                    listItems.Add(descitem);

                    output = output + "<tr><td style='width:320px;text-align:left;font-family:Calibri;'>" + item.Quantity + "*" + item.Description + "( " + item.SupplierCode + " ) </td></tr>";
                }



            }



            Char spacer = (char)13;

            output = output + @"<tr><td style='font-family:Calibri;'>KIND REGARDS</td></tr><tr><td>&nbsp;
     </td></tr><tr><td style='font-family:Calibri;'></td></tr><tr><td style='font-family:Calibri;'>"
                                                      + bottomBanner + "</td></tr></table></td></tr><tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr></table></body></html>";

            return output;
        }

        [System.Web.Services.WebMethod]
        public static void UpdateProductINHouse(string creditNoteId)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var crId = Convert.ToInt32(creditNoteId);
            var creditNoteDal = new CreditNotesDAL(cs);
            var itemsByCreditNote = creditNoteDal.getCreditNoteItemsByCreditId(crId);
            var ingouseSuppId = 8;

            UpdateOrInsertItemBySupplierCodeInHouse(itemsByCreditNote, ingouseSuppId);

            UpdateCreditHouse(crId);
        }


        private static StringBuilder UpdateOrInsertItemBySupplierCodeInHouse(List<DeltoneCRM_DAL.CreditNotesDAL.CreditNoteItemEle> list, int supplierId)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                SqlConnection conn = new SqlConnection();
                String companyNote = String.Empty;
                var canUpdateItem = false;
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
                if (!string.IsNullOrEmpty(item.SupplierCode))
                {

                    item.SupplierCode = "DS" + item.SupplierCode;
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = conn;
                    cmd.CommandText = " SELECT ItemID,Quantity from Items where SupplierItemCode=@SupplierItemCode and SupplierID=@suppId ";
                    cmd.Parameters.AddWithValue("@SupplierItemCode", item.SupplierCode);
                    cmd.Parameters.AddWithValue("@suppId", supplierId);
                    double resellprice = 0;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        if (sdr.HasRows)
                        {
                            var itemId = 0;
                            var qty = 0;
                            while (sdr.Read())
                            {
                                if (sdr["ItemID"] != DBNull.Value)
                                    itemId = Convert.ToInt32(sdr["ItemID"].ToString());
                                if (sdr["Quantity"] != DBNull.Value)
                                    qty = Convert.ToInt32(sdr["Quantity"].ToString());
                            }
                            canUpdateItem = true;
                            conn.Close();
                            var productPrice = Convert.ToDouble(item.COG);
                            if (productPrice > 0)
                                resellprice = productPrice * 4;
                            qty = qty + Convert.ToInt32(item.Quantity);
                            String strSQLUpdateStmt = @"update Items set COG=@cog, ManagerUnitPrice=@ResellPrice,
                        RepUnitPrice=@ResellPrice,AlterationDate=CURRENT_TIMESTAMP , Quantity=@Quantity where ItemID=@ItemID";
                            SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
                            cmd2.Parameters.AddWithValue("@cog", Convert.ToDouble(item.COG));
                            cmd2.Parameters.AddWithValue("@ResellPrice", resellprice);
                            cmd2.Parameters.AddWithValue("@ItemID", itemId);
                            cmd2.Parameters.AddWithValue("@Quantity", qty);
                            try
                            {
                                // sb.Append(String.Format("Updated  Code = {0}: Price = {1}  Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                                // sb.Append(Environment.NewLine);
                                conn.Open();
                                cmd2.ExecuteNonQuery().ToString();
                                conn.Close();

                            }
                            catch (Exception ex)
                            {
                                if (conn != null) { conn.Close(); }

                            }
                        }


                        else
                        {
                            canUpdateItem = true;
                            conn.Close();
                            var BestPrice = 'Y';
                            var Faulty = 'N';
                            var productPrice = Convert.ToDouble(item.COG);
                            if (productPrice > 0)
                                resellprice = productPrice * 4;
                            String strSQLInsertStmt = @"insert into dbo.Items(SupplierID,SupplierItemCode, Description," +
                           " COG, ManagerUnitPrice, RepUnitPrice, Active,CreatedBy,CreatedDateTime, PriceLock, ReportedFaulty,Quantity,AlterationDate) " +
                           " values (@SupplierID,@ItemCode,@Description, @COG, @ResellPRice, @ResellPrice,'Y','SYSTEM',CURRENT_TIMESTAMP, @PriceLock, @ReportedFaulty,@Quantity,CURRENT_TIMESTAMP);";
                            SqlCommand cmd3 = new SqlCommand(strSQLInsertStmt, conn);
                            cmd3.Parameters.AddWithValue("@SupplierID", supplierId);
                            cmd3.Parameters.AddWithValue("@ItemCode", item.SupplierCode);
                            cmd3.Parameters.AddWithValue("@Description", item.Description);
                            cmd3.Parameters.AddWithValue("@COG", Convert.ToDouble(item.COG));
                            cmd3.Parameters.AddWithValue("@ResellPrice", resellprice);
                            cmd3.Parameters.AddWithValue("@PriceLock", BestPrice);
                            cmd3.Parameters.AddWithValue("@ReportedFaulty", Faulty);
                            cmd3.Parameters.AddWithValue("@Quantity", item.Quantity);

                            try
                            {
                                conn.Open();
                                cmd3.ExecuteNonQuery();
                                conn.Close();

                            }
                            catch (Exception ex)
                            {
                                if (conn != null) { conn.Close(); }

                            }

                        }

                    }
                }
                if (canUpdateItem)
                    UpdateCreditNoteItemHouse(item.CreditNoteItem_ID);
            }


            return sb;
        }

        private bool GetCreditNoteHouse(int creditNoteId)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            cmd.CommandText = " SELECT InHouse from CreditNotes where CreditNote_ID=@CreditNote_ID ";
            cmd.Parameters.AddWithValue("@CreditNote_ID", creditNoteId);
            var iNhouse = false;
            conn.Open();
            using (SqlDataReader sdr = cmd.ExecuteReader())
            {

                if (sdr.HasRows)
                {
                    var qty = 0;
                    while (sdr.Read())
                    {
                        if (sdr["InHouse"] != DBNull.Value)
                            iNhouse = Convert.ToBoolean(sdr["InHouse"].ToString());

                    }
                }
            }
            // sb.Append(String.Format("Updated  Code = {0}: Price = {1}  Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
            // sb.Append(Environment.NewLine);


            conn.Close();


            return iNhouse;
        }

        private static void UpdateCreditHouse(int creditNoteItemId)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strSQLUpdateStmt = @"update CreditNotes set InHouse=@Inhouse, DateAltered=CURRENT_TIMESTAMP 
                         where CreditNote_ID=@ItemID";
            SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
            cmd2.Parameters.AddWithValue("@Inhouse", true);
            cmd2.Parameters.AddWithValue("@ItemID", creditNoteItemId);

            try
            {
                // sb.Append(String.Format("Updated  Code = {0}: Price = {1}  Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                // sb.Append(Environment.NewLine);
                conn.Open();
                cmd2.ExecuteNonQuery().ToString();
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }

            }
        }

        private static void UpdateCreditNoteItemHouse(string creditNoteItemId)
        {
            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            String strSQLUpdateStmt = @"update CreditNote_Item set Inhouse=@Inhouse, AlteredDate=CURRENT_TIMESTAMP 
                         where CreditNoteItem_ID=@ItemID";
            SqlCommand cmd2 = new SqlCommand(strSQLUpdateStmt, conn);
            cmd2.Parameters.AddWithValue("@Inhouse", true);
            cmd2.Parameters.AddWithValue("@ItemID", creditNoteItemId);

            try
            {
                // sb.Append(String.Format("Updated  Code = {0}: Price = {1}  Description = {2}", item.SupplierItemCode, item.PriceUpdate, item.Description));
                // sb.Append(Environment.NewLine);
                conn.Open();
                cmd2.ExecuteNonQuery().ToString();
                conn.Close();

            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }

            }
        }
    }
}