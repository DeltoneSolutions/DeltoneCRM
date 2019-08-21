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
// Xero Intergration functions
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
using System.Threading;
using System.Security.Cryptography;
// END

namespace DeltoneCRM
{
    public partial class DummyOrder : System.Web.UI.Page
    {



        String strPendingApproval = "";
        String strLoggedUserProfile = "Admin";
        static String str_ORDERID;
        static int ORDERID;
        static int CONTACTID;
        static int COMPANYID;
        static String COMPANYNAME;
        static String connString;
        static int REL_OrderID;
        SupplierDAL suppdal;
        OrderDAL orderdal;
        PurchaseDAL purchasedal;
        CommissionDAL dal_Commission;
        TempSuppNotes tempsuppdal;
        CompanyDAL companydal;
        //Modification done here
        static String ORDERSTATUS = String.Empty;
        //This is from XeroIntegration.cs for debugging purposes
        private const String consumerKey = ""; //Put those in Web  Config
        private const String userAgnetString = "";//Put Those in Web Config;
        XeroApi.OAuth.XeroApiPrivateSession xSession;
        XeroApi.Repository repository;
        protected Repository Repos = null;
        XeroCoreApi api_User; //Api User According to the Skinny Wrapper
        // END 
        protected Guid id;


        protected void Page_Load(object sender, EventArgs e)
        {
            String CompanyID = String.Empty;
            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            suppdal = new SupplierDAL(connString);
            orderdal = new OrderDAL(connString);
            purchasedal = new PurchaseDAL(connString);
            tempsuppdal = new TempSuppNotes(connString);
            dal_Commission = new CommissionDAL(connString);
            companydal = new CompanyDAL(connString);
            COMPANYNAME = orderdal.getCompanyName(COMPANYID);
            hdnAllSuppliers.Value = suppdal.getAllSuppliers();
            //TR_SplitCommission.Visible = false;

            // Build the File Upload Section

            if (!IsPostBack)
                orderNameTextBox.Text = COMPANYNAME;

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
                //Set CompanyID
                COMPANYID = Int32.Parse(CompanyID);
                Session["OpenedCompanyID"] = COMPANYID;


                AccountOwnertxt.Text = companydal.getCompanyAccountOwner(COMPANYID);
                ACCOUNT_OWNER_TXT.Value = companydal.getCompanyAccountOwner(COMPANYID);
                SALESPERSON_TXT.Value = Session["LoggedUser"].ToString();
                String AccountOwnerID = companydal.getCompanyOwnershipAdminID(COMPANYID.ToString());
                ACCOUNT_OWNER_ID.Value = AccountOwnerID;
                SALESPERON_ID.Value = Session["LoggedUserID"].ToString();

                chkbx_ConfirmCommishSplit.Enabled = true;
                if (!IsPostBack)
                {
                    BuildDropDownListForSplitCommission(AccountOwnerID);
                }


                //Populate the Drop down List with the Users
                if (!IsPostBack)
                {
                    PopulateDropDownList(Int32.Parse(CompanyID));
                    ddlPaymentTerms.ClearSelection();
                    ddlPaymentTerms.Items.FindByValue(companydal.getCompanyPaymentTerms(CompanyID)).Selected = true;
                }
            }

            if (!String.IsNullOrEmpty(Request.QueryString["Oderid"]))
            {
                //Modified here Enable Order Create Date 
                tdOrderCreateDate.Style.Value = "display:block;";
                datecreated.Style.Value = "display:block";
                str_ORDERID = Request.QueryString["Oderid"].ToString();// Modified here 

                int OrderID = Int32.Parse(Request.QueryString["Oderid"].ToString());
                ORDERID = Int32.Parse(Request.QueryString["Oderid"].ToString());
                hdnORDERID.Value = Request.QueryString["Oderid"].ToString();

                var orderName = GetOrderName(OrderID);
                if (!string.IsNullOrEmpty(orderName))
                    orderNameTextBox.Text = orderName;

                //Set the Order Title 
                OrderTitle.InnerHtml = "EDIT ORDER: " + ORDERID;
                OrderTitle.Style.Value = "display:block;";

                String strOrderedItems = getOrderItemsbyOrderID(OrderID);
                hdnEditOrderItems.Value = strOrderedItems;

                String strSupplierNotes = FetchSupplierNotes(OrderID);
                String strPromotionalItems = "";//FetchProItems(OrderID);
                String strOrder = getOrderbyOrderID(OrderID);
                String strReference = FetchReferenceInfo(OrderID);
                String strTypeOfCall = FetchTypeOfCall(OrderID);
                String strPaymentTerms = String.Empty; //Modified Add Payment Terms

                String strCommishSplit = orderdal.getDummyCommishSplitBoolean(OrderID);
                String strSplithWith = orderdal.getDummySalespersonName(OrderID);
                String strSplithWithID = orderdal.getDummySalespersonByID(OrderID);
                String VolumeAmountSplit = orderdal.getDummyVolumeSplitAmount(OrderID);

                String Urgency = orderdal.getDummyOrderUrgency(OrderID);

                if (!IsPostBack)
                {
                    ddl_Urgency.SelectedValue = Urgency.ToString();

                }


                SALESPERSON_TXT.Value = strSplithWith.ToString();
                SALESPERON_ID.Value = strSplithWithID.ToString();

                if (strCommishSplit == "1")
                {
                    chkbx_ConfirmCommishSplit.Checked = true;
                    if (!IsPostBack)
                    {
                        splitcommissionwith.SelectedIndex = -1;
                        splitcommissionwith.Items.FindByText(strSplithWith).Selected = true;
                        VOLUME_SPLIT_AMOUNT.Value = VolumeAmountSplit;
                    }



                }
                else
                {
                    chkbx_ConfirmCommishSplit.Checked = false;
                }


                String XeroGuid = String.Empty;

                if (!IsPostBack)
                {
                    if (Directory.Exists(Server.MapPath("~/Uploads/" + str_ORDERID)))
                    {
                        string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/" + str_ORDERID));
                        List<System.Web.UI.WebControls.ListItem> files = new List<System.Web.UI.WebControls.ListItem>();
                        foreach (string filePath in filePaths)
                        {
                            files.Add(new System.Web.UI.WebControls.ListItem(Path.GetFileName(filePath), filePath));
                        }
                        // GridView1.DataSource = files;
                        //  GridView1.DataBind();
                    }
                    else
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Uploads/" + str_ORDERID));
                    }

                }

                String DoesHaveXeroInvoiceNumber = orderdal.getXeroDTSID(Int32.Parse(str_ORDERID));

                if (!Session["USERPROFILE"].ToString().Equals("ADMIN"))
                {
                    TCKNotifyClient.Enabled = false;
                }
                else
                {

                    TCKNotifyClient.Enabled = true;

                }

                // Populate XERO LOG for Administrators
                if (Session["USERPROFILE"].ToString().Equals("ADMIN"))
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        // conn.Open();
                        //  SqlCommand cmd = new SqlCommand("SELECT CreatedDateTime, Msg FROM dbo.DELTONECRM_LOG WHERE OrderID = " + str_ORDERID, conn);
                        // SqlDataReader sdr = cmd.ExecuteReader();
                        // XeroLogGRID.DataSource = sdr;
                        //  XeroLogGRID.DataBind();
                        //  conn.Close();
                    }

                }

                if (!String.IsNullOrEmpty(strOrder))
                {
                    hdnEditOrder.Value = strOrder;
                    //Set the Order Status here 
                    String[] arr = strOrder.Split(':');
                    ORDERSTATUS = arr[3].ToString();
                    if (!IsPostBack)
                    {
                        if (ORDERSTATUS == "APPROVED")
                        {
                            TCKNotifyClient.Checked = false;
                        }
                        else
                        {
                            if (Session["USERPROFILE"].ToString().Equals("ADMIN"))
                            {
                                if (DoesHaveXeroInvoiceNumber != "")
                                {
                                    TCKNotifyClient.Checked = true;
                                }
                                else
                                {
                                    TCKNotifyClient.Checked = false;
                                }

                            }
                        }
                    }
                    ORDER_STATUS.Value = arr[3].ToString();

                    String test = arr[4].ToString();
                    //Modification done add order create date
                    String OrderCreateDate = arr[10].ToString();
                    XeroGuid = arr[8].ToString();
                    strPaymentTerms = arr[9].ToString(); //Modified here Fetch Payment Here 
                    ORDER_DATE.Value = test;
                    ORDER_CREATE_DATE.Value = OrderCreateDate;//Modification done here

                    if (ORDERSTATUS.Equals("QUOTED"))
                    {
                        btnAddCreditNote.Visible = false;
                        btnInvoiceApprove.Visible = false;
                    }
                    else
                    {
                        //Set the Credit Note Button Visible
                        btnAddCreditNote.Visible = false;
                        btnInvoiceApprove.Visible = false;
                    }

                    //ddl Payement Terms changes
                }

                if (!String.IsNullOrEmpty(strSupplierNotes))
                {
                    //Populate the Hidden value
                    hdnEditSupplietNotes.Value = strSupplierNotes;
                }


                //Set Hidden Field Value
                if (!IsPostBack)
                {
                    ddlPaymentTerms.Text = strPaymentTerms;
                }

                //Set Reference Field Value
                if (!IsPostBack && !String.IsNullOrEmpty(strReference))
                {
                    RefID.Value = strReference;
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


                btnCancelInvoice.Style.Value = "display:inline";
                strPendingApproval = "PendingApproval";
                //btnOrderSubmit.Text = "Approve Order";
                btnCloseOrderWindow.Style.Value = "display:none";

                //Modification done here Once Invoice is Approved disable the Submit button

                if (ORDERSTATUS.Equals("APPROVED") && Session["USERPROFILE"].Equals("STANDARD"))
                {
                    btnOrderSubmit.Style.Value = "display:none";
                    FileUploadTR.Visible = false;
                }

                if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
                {
                    btnCancelInvoice.Style.Value = "display:none";
                    FileUploadTR.Visible = false;
                    xerologsection.Visible = false;
                }



                //Check the Session here if Only ADMiN Display Approve BUTTON
                if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                {
                    if (Session["USERPROFILE"].ToString().Equals("ADMIN") && !(ORDERSTATUS.Equals("APPROVED")) && !(String.IsNullOrEmpty(XeroGuid)))
                    {
                        btnInvoiceApprove.Style.Value = "display:inline";
                    }
                }

                OrderDAL odal = new OrderDAL(connString);
                String strCreatedBy = odal.getdummOrderCreatedBy(ORDERID);


                if (!IsPostBack)
                {
                    ddlUsers.ClearSelection();
                    ddlUsers.Items.FindByText(strCreatedBy).Selected = true;
                }

            }

            else
            {
                if (Session["LoggedUserID"] != null)
                {
                    //ddlUsers.Items.FindByValue(Session["LoggedUserID"].ToString()).Selected = true;
                    //ddlUsers.Items.FindByText(firstname).Selected = true;
                }
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

        protected void UploadFile(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + str_ORDERID + "/") + fileName);
                Response.Redirect(Request.Url.AbsoluteUri);
            }

        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            System.IO.File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        //CLOSE ORDER WINDOW Event handler
        protected void btnCloseOrderWindow_Click(object sender, EventArgs e)
        {
            string strScript = "<script language='javascript'>$(document).ready(function (){ CLOSE_ORDERDIALOG(); });</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }


        protected void btnAddCreditNote_Click(object sender, EventArgs e)
        {
            //OrderID,ContactID,CompanyID
            Response.Redirect("CreateCreditNote.aspx?OrderID=" + ORDERID + "&ContactID=" + CONTACTID + "&CompanyID=" + COMPANYID);
            //Response.Redirect("CreditNote.aspx?OrderID=" + ORDERID + "&ContactID=" + CONTACTID + "&CompanyID=" + COMPANYID); 
        }


        //Button Submit,Apporove Order Click Functions
        protected void btnOrderSubmit_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(hdnTotal.Value))
            {
                return;
            }
            DateTime datetimReceived;
            if (!DateTime.TryParse(datereceived.Value, out datetimReceived))
            {
                Response.Write("<script>alert('INVOICE DATE is not valid');</script>");
                return;
            }

            String strCompanyID = hdnCompanyID.Value;
            Boolean blnNotifyClient = TCKNotifyClient.Checked;
            String strContactID = hdnContactID.Value;
            String strTest = hdnProfit.Value;
            String OrderNotesStr = OrderNotes.Text.ToString();
            String PaymentTermsStr = ddlPaymentTerms.Text.ToString();
            String Urgency = String.Empty;

            Urgency = ddl_Urgency.Text.ToString();

            //String Reference = ReferenceFld.Text.ToString();
            String Reference = RefID.Value.ToString();
            String TypeOfCall = dllTypeOfCall.Text.ToString();
            Boolean chkSplitCommish = chkbx_ConfirmCommishSplit.Checked;
            String SplitCommissionWithID = "0";
            String SplitCommissionWithName = "NOSPLIT";
            String SplitVolume = String.Empty;

            String AccountOwner = ACCOUNT_OWNER_TXT.Value;
            String AccountOwnerID = ACCOUNT_OWNER_ID.Value;

            if (chkSplitCommish == true)
            {
                SplitCommissionWithID = splitcommissionwith.SelectedValue;
                SplitCommissionWithName = splitcommissionwith.SelectedItem.ToString();
                SplitVolume = VOLUME_SPLIT_AMOUNT.Value;

            }

            float COGTotal = 0;
            float CogSubTotal = 0;

            float profitTotal = 0;
            float profitSubTotal = 0;
            float SuppDelCost = 0;

            float CusDelCost = 0;
            float ProItemCost = 0;

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
                //strCreatedBy = Session["LoggedUser"].ToString();
            }

            strCreatedBy = ddlUsers.SelectedItem.Text;

            String strProitems = hdnProItems.Value.ToString();
            String strSuppDelItems = hdnSupplierDelCostItems.Value.ToString();
            String strCustDelCostItems = CusDelCostItems.Value.ToString();

            String[] strOrderItems = OrderItems.Value.Split('|');
            String[] itemList;

            //Modified Here Add Supplier Notes here 30/04/2015
            String strSuppNotes = String.Empty;

            if (!String.IsNullOrEmpty(hdnSupplierNotes.Value))
            {
                strSuppNotes = hdnSupplierNotes.Value;
            }
            //End Modification Supplier Notes

            int intPaymentTerms = Int32.Parse(ddlPaymentTerms.SelectedValue.ToString());

            //Modified here 
            DateTime OrderDueDate = Convert.ToDateTime(datereceived.Value).AddDays(intPaymentTerms);

            //DateTime currentDate = DateTime.Now;
            //DateTime AddedDate = AddWorkingDays(currentDate, 3);



            String strOwnerShipAdminID = ddlUsers.SelectedItem.Text;

            String PROITEMS = String.Empty;
            if (!String.IsNullOrEmpty(hdnPromotionalItems.Value))
            {
                PROITEMS = hdnPromotionalItems.Value;

            }
            //End Promotional Items

            //Supplier List
            ArrayList alSuppliers = new ArrayList();

            //Purchase List 
            Dictionary<String, String> di = new Dictionary<string, String>();

            #region SupplierNotes

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

            #endregion

            //Xero Connection
            XeroIntergration xero = new XeroIntergration();

            #region CreatePurchaseOrderList

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

            // Order already exists therefore UPDATE ONLY
            if (!String.IsNullOrEmpty(Request.QueryString["Oderid"]))
            {

                int Orderid = Int32.Parse(Request.QueryString["Oderid"].ToString());
                String OrderStatus = getOrderStatus(Request.QueryString["Oderid"].ToString());




                var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                var accoutOwnerName = AccountOwner;


                var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = connectionstring;


                string orderItemsString = "";
                string newvalues = "";

                //Delete Order items 
                if (DeleteOrderItems(Orderid, out orderItemsString) > 0)
                {
                    //Update the Order 
                    if (UpdateOrder(Orderid, strCompanyID, strContactID, COGTotal, CogSubTotal, profitTotal, profitSubTotal, SuppDelCost,
                        CusDelCost, ProItemCost, strCreatedBy, strSuppDelItems, strProitems, strCustDelCostItems, strCreatedBy,
                        OrderDueDate, strOwnerShipAdminID, OrderNotesStr, PaymentTermsStr, Reference, TypeOfCall, OrderStatus,
                        Convert.ToDateTime(datereceived.Value.ToString()), SplitVolume, SplitCommissionWithID,
                        SplitCommissionWithName, Urgency) > 0)
                    {
                        //if (OrderStatus == "QUOTED")
                        //{
                        //    OrderStatus = "PENDING";
                        //}
                        //newvalues = " Order  Id " + Orderid + " ,Order Status " + OrderStatus + " ,Order Note " + OrderNotesStr; ;

                        //Insert New Order Items 
                        for (int i = 0; i < strOrderItems.Length; i++)
                        {
                            itemList = strOrderItems[i].Split(',');
                            if (strOrderItems[i] != String.Empty)
                            {
                                if (i == 0)
                                {

                                    int firstItem = CreateOrderItems(Orderid, itemList[1], itemList[0], float.Parse(itemList[5]),
                                        Int32.Parse(itemList[4]), float.Parse(itemList[3]), strCreatedBy,
                                        itemList[2], itemList[6]);
                                    //orderItemsString = orderItemsString + " Newly created Order Items : Description " + itemList[1] + ", Created By " + strCreatedBy +
                                    // ", Quantity " + itemList[4].ToString() +
                                    //" , SupplierName " + itemList[6] + " ";
                                }
                                //Rest of the Elements
                                if (i != 0)
                                {
                                    int Item = CreateOrderItems(Orderid, itemList[2], itemList[1], float.Parse(itemList[6]),
                                        Int32.Parse(itemList[5]), float.Parse(itemList[4]), strCreatedBy,
                                        itemList[3], itemList[7]);
                                    //orderItemsString = orderItemsString + " Order Items : Description " + itemList[2] + ", Created By " + strCreatedBy +
                                    // ", Quantity " + itemList[5].ToString() +
                                    //" , SupplierName " + itemList[7] + " ";
                                }

                            }

                        }

                    }
                    #region Add promotional Items(Updation)
                    if (!String.IsNullOrEmpty(PROITEMS))
                    {
                        UpdatePromotionalItems(Orderid, PROITEMS);
                    }
                    else
                    {
                        //Remove Promotioanl Items If Exsists
                        RemoveProItems(Orderid);
                    }
                    #endregion


                    #region  UpdateSplitCommision
                    if (Session["LoggedUserID"] != null)
                    {
                        //float commission = float.Parse(hdnCommision.Value.ToString());
                        UpdateCommissions(Orderid, ACCOUNT_OWNER_ID.Value, SplitCommissionWithID, ACCOUNT_OWNER_COMMISH.Value, SALESPERSON_COMMISH.Value, chkbx_ConfirmCommishSplit.Checked);
                        //SplitCommission(OrderID, ACCOUNT_OWNER_ID.Value, SALESPERON_ID.Value, ACCOUNT_OWNER_COMMISH.Value, SALESPERSON_COMMISH.Value, chkbx_ConfirmCommishSplit.Checked);

                        //UpdateCommissions(Orderid, Int32.Parse(strCompanyID), Int32.Parse(Session["LoggedUserID"].ToString()), commission);
                    }
                    #endregion


                    #region purchase  Items Updation for Supplier Docs
                    purchasedal.UpdatePurhcaseItems(Orderid, di, Session["LoggedUser"].ToString());
                    #endregion

                    #region XeroEntry Updation with the Status DRAFT

                    //  Repository Res = xero.CreateRepository();
                    String deltoneInvoice_Number = Orderid.ToString();
                    //  String deltoneInvoice_ID = String.Empty;
                    //String InvoiceReference = ReferenceFld.Text;
                    // String InvoiceReference = RefID.Value;

                    connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    orderdal = new OrderDAL(connString);


                    XeroApi.Model.Invoice INV = null; //FOR CREATION AND UPDATION OF THE INVOICE


                    //Fetch the Xero Guid from CRM Table
                    String xeroGuid = orderdal.getOrderGuid(Orderid);

                    // if (!String.IsNullOrEmpty(xeroGuid)) //If Xero Entry is there
                    //  {
                    //    XeroApi.Model.Invoice updatedInvoice = xero.UpdtateInvoice(Orderid, Res, xeroGuid, strOrderItems, intPaymentTerms, Convert.ToDecimal(CusDelCost), PROITEMS, ORDERSTATUS, InvoiceReference, Convert.ToDateTime(datereceived.Value));//Change the Status in the XERO System

                    // INV = updatedInvoice;

                    // if (updatedInvoice != null)
                    //{
                    //  deltoneInvoice_Number = updatedInvoice.InvoiceNumber.ToString();
                    //     deltoneInvoice_ID = updatedInvoice.InvoiceID.ToString();
                    //}

                    //  }
                    //  else //Xero Entry hasn't been created and reCreate the Entry in the Xero and Update the Deltone Tables
                    //{
                    // XeroApi.Model.Invoice recreate_Invoice = xero.CreateInvoice(Orderid, Res, COMPANYNAME, strOrderItems, intPaymentTerms, Convert.ToDecimal(CusDelCost), PROITEMS, InvoiceReference, Convert.ToDateTime(datereceived.Value));

                    //  INV = recreate_Invoice;

                    // if (recreate_Invoice != null)
                    // {
                    //   deltoneInvoice_Number = recreate_Invoice.InvoiceNumber.ToString();
                    // deltoneInvoice_ID = recreate_Invoice.InvoiceID.ToString();
                    // updateOrderStatus(deltoneInvoice_Number, deltoneInvoice_ID, Int32.Parse(Orderid.ToString()));
                    //}
                    // }

                    UpdateSupplierNotes(Orderid, deltoneInvoice_Number, di_Notes);

                    if (ORDERSTATUS.Equals("APPROVED"))
                    {
                        // Dictionary<String, String> dinotes = tempsuppdal.getNotesObject(ORDERID);
                        // Dictionary<String, String> di_suppitems = purchasedal.getPurchaseItemObject(ORDERID);
                        // GenerateSupplierDocs(Orderid, deltoneInvoice_Number, CONTACTID.ToString(), di_suppitems, dinotes);
                    }

                    UpdateOrderNameXeroDTS(Orderid);

                    #endregion

                    if (TCKCreateCSV.Checked)
                    {
                        CreateCSV BuildtheCSV = new CreateCSV();
                        // BuildtheCSV.BuildCSV(ORDERID.ToString());

                    }


                    if (TCKNotifyClient.Checked)
                    {

                        DeltonelEmailSender Emailsender = new DeltonelEmailSender();
                        // Emailsender.SendMail(String.Empty, ORDERID.ToString(), "sajith.jayaratne@improvata.com.au", deltoneInvoice_Number);
                    }

                    //         var columnName = "Order Table And Order Items All columns";
                    //         var talbeName = "Order And Order Items";
                    //         var ActionType = "Updated Order And OrderItems";
                    //         int primaryKey = Orderid;
                    //         var lastString = newvalues + orderItemsString;

                    //         new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", lastString, loggedInUserId, conn, 0,
                    //columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));


                    //////// Refator this snippet///////
                    String Message = "<h2>ORDER: " + deltoneInvoice_Number + " HAS BEEN EDITED</h2>";
                    String NavigateUrl = "ConpanyInfo.aspx?companyid=" + strCompanyID;
                    String PrintURl = "PrintOrder.aspx?Oderid=" + Orderid + "&cid=" + strContactID + "&Compid=" + strCompanyID;
                    string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
                    ///////End Refatoring///////////

                }

            }

                ////Order Creation and Save it in Xero as Draft  ////
            else
            {
                String strOrderStatus = String.Empty;
                if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                {
                    strOrderStatus = "PENDING";
                }
                //End Checking User Profile



                //Modified here Add User Defined Date
                if (CreateOrder(strCompanyID, strContactID, COGTotal, CogSubTotal, profitTotal, profitSubTotal, SuppDelCost,
                    CusDelCost, ProItemCost, strCreatedBy, strSuppDelItems, strProitems, strCustDelCostItems, strOrderStatus,
                    OrderDueDate, strOwnerShipAdminID, OrderNotesStr, PaymentTermsStr, Reference, TypeOfCall,
                    Convert.ToDateTime(datereceived.Value), chkSplitCommish, SplitCommissionWithID, SplitCommissionWithName,
                    AccountOwner, AccountOwnerID, SplitVolume, Urgency) > 0)
                {
                    int OrderID = LastOrderID(strCreatedBy);
                    string orderItemsString = "";

                    Directory.CreateDirectory(Server.MapPath("~/Uploads/" + OrderID));

                    ////Refator this code snippet//////
                    for (int i = 0; i < strOrderItems.Length; i++)
                    {
                        itemList = strOrderItems[i].Split(',');
                        if (strOrderItems[i] != String.Empty)
                        {
                            //For the First Element
                            if (i == 0)
                            {
                                int firstItem = CreateOrderItems(OrderID, itemList[1], itemList[0], float.Parse(itemList[5]),
                                    Int32.Parse(itemList[4]), float.Parse(itemList[3]), strCreatedBy, itemList[2], itemList[6]);
                                //orderItemsString = " Order Items : Description " + itemList[1] + ", Created By " + strCreatedBy +
                                //     ", Quantity " + itemList[4].ToString() +
                                //    " , SupplierName " + itemList[6] + " ";

                            }
                            //Rest of the Elements
                            if (i != 0)
                            {
                                int Item = CreateOrderItems(OrderID, itemList[2], itemList[1], float.Parse(itemList[6]), Int32.Parse(itemList[5]),
                                    float.Parse(itemList[4]), strCreatedBy, itemList[3], itemList[7]);
                                //orderItemsString = orderItemsString + " Order Items : Description " + itemList[2] + ", Created By " + strCreatedBy +
                                //     ", Quantity " + itemList[5].ToString() +
                                //    " , SupplierName " + itemList[7] + " ";

                            }
                        }
                    }
                    ///////End  Refactor this code//////





                    #region WriteCommssions

                    //float commission = float.Parse(hdnCommision.Value.ToString());
                    if (Session["LoggedUserID"] != null)
                    {
                        //SplitCommission(OrderID, Int32.Parse(strCompanyID), Int32.Parse(Session["LoggedUserID"].ToString()), commission);
                        //SplitCommission(OrderID, Int32.Parse(strCompanyID), Int32.Parse(ddlUsers.SelectedValue.ToString()), commission);
                        SplitCommission(OrderID, ACCOUNT_OWNER_ID.Value, SplitCommissionWithID, ACCOUNT_OWNER_COMMISH.Value, SALESPERSON_COMMISH.Value, chkbx_ConfirmCommishSplit.Checked);
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
                        //purchasedal.InsertPurchaseItem(OrderID, pair.Key, pair.Value, Session["LoggedUser"].ToString());
                    }

                    #endregion PurchaseOrder List


                    #region XeroEntry Creation with Status(DRAFT)

                    // Repository res = xero.CreateRepository();
                    String strContact = getContactDetailsForInvoice(Int32.Parse(strContactID));
                    String[] arrContact = strContact.Split(':');
                    String InvReference = RefID.Value;


                    XeroApi.Model.Invoice INV = null;

                    UpdateOrderNameXeroDTS(OrderID);

                    //  XeroApi.Model.Invoice delinvoice = xero.CreateInvoice(OrderID, res, COMPANYNAME, strOrderItems, intPaymentTerms, Convert.ToDecimal(CusDelCost), PROITEMS, InvReference, Convert.ToDateTime(datereceived.Value)); //with Status DRAFT


                    //  var columnName = "Order Table And Order Items All columns";
                    //  var talbeName = "Order And Order Items";
                    //  var ActionType = "Created";
                    //  int primaryKey = OrderID;

                    //  var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    //  var accoutOwnerName = AccountOwner;
                    //  var newvalues = "Order  Id " + OrderID + " ,Order Status "
                    //      + strOrderStatus + " ,Order Due Date " + OrderDueDate + ", Order Date " + datereceived.Value.ToString();
                    //  var lastString = newvalues + orderItemsString;

                    //  var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                    //  SqlConnection conn = new SqlConnection();
                    //  conn.ConnectionString = connectionstring;

                    //  new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", lastString, loggedInUserId, conn, 0,
                    //columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));


                    //Create the Supplier Notes if Exsists
                    //  if (delinvoice != null)
                    //  {
                    //     INV = delinvoice;
                    //    CreateSupplerNotes(OrderID, delinvoice.InvoiceNumber.ToString(), di_Notes);
                    /// }
                    //  else
                    {
                        CreateSupplerNotes(OrderID, String.Empty, di_Notes);
                    }

                    #endregion

                    String deltoneInvoice_Number = OrderID.ToString();
                    // String deltoneInvoice_Number = String.Empty;
                    //  String deltoneInvoice_ID = String.Empty;
                    String Message = String.Empty;

                    //if (delinvoice != null)
                    //{
                    //    deltoneInvoice_Number = delinvoice.InvoiceNumber.ToString();
                    //    deltoneInvoice_ID = delinvoice.InvoiceID.ToString();
                    //    updateOrderStatus(deltoneInvoice_Number, deltoneInvoice_ID, Int32.Parse(OrderID.ToString()));

                    //    //Send User Email

                    //    if (TCKCreateCSV.Checked)
                    //    {
                    //        CreateCSV BuildtheCSV = new CreateCSV();
                    //        BuildtheCSV.BuildCSV(ORDERID.ToString());

                    //    }

                    //    if (TCKNotifyClient.Checked)
                    //    {

                    //        //DeltonelEmailSender Emailsender = new DeltonelEmailSender();
                    //        //Emailsender.SendMail(String.Empty, OrderID.ToString(), "dimitri@deltonesolutions.com.au", deltoneInvoice_Number);


                    //    }





                    //}

                    Message = (!deltoneInvoice_Number.Equals(String.Empty)) ? "<h2>ORDER: " + deltoneInvoice_Number + " SUCCESSFULLY CREATED</h2>" : "COULD NOT WRITE TO XERO.";
                    String NavigateUrl = "ConpanyInfo.aspx?companyid=" + strCompanyID;
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

            // Send email to customer 
            //SendConfirmationEmailToClient();

            /*End Submit Click Event Handler*/
        }



        /* Sends an email to the customer with an order confirmation */
        protected void SendConfirmationEmailToClient()
        {
            StreamReader reader = new StreamReader(Server.MapPath("EmailTemplates/customeremail.aspx"));
            string readFile = reader.ReadToEnd();
            string myString = "";
            myString = readFile;
            MailMessage Msg = new MailMessage();
            var fromAddress = new MailAddress("info.deltonesolutions@gmail.com", "DELTONE SOLUTIONS");
            //var toAddress = new MailAddress("sumudu_ch@yahoo.com", "TEST USER");
            const string fromPassword = "deltone123";
            //MailAddress fromMail = new MailAddress("orders@deltonesolutions.com.au");
            Msg.From = fromAddress;
            Msg.To.Add(new MailAddress("dimitri@deltonesolutions.com.au"));
            Msg.Subject = "Order Confirmation from Deltone Solutions";
            Msg.Body = myString.ToString();
            Msg.IsBodyHtml = true;
            //const string fromPassword = "Rowville003";
            try
            {

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                smtp.Send(Msg);
            }
            catch (Exception er)
            {
                String message = er.Message.ToString();
            }
            reader.Dispose();
        }

        /*This Method Create Popup Dialog for NEW ORDER,QUOTE,EDIT ORDER SUBMISSION   */
        protected void CreateUIDialog(String strMessage, String Navigateurl, String PrintUrl, String contactID, String CompanyID)
        {
            string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + strMessage + "','" + Navigateurl + "','" + PrintUrl + "'); });</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }
        /*End Method Create Popup Dialog*/


        #region Document Generation

        //This Function Genearate the Cusomters and Save it in the Disk
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
        //This Function Generate the Invoice for Suppliers and Save it in the Disk

        protected void GenerateSupplierInvoice(String strInvoiceID, float COGToal, float COGSubTotal, String[] strInvoiceItems, float SuppDelCost, float ProItemCost)
        {

            //For Each Items in the Order Generate Seperate PDfs for Suppliers 
        }

        //This Method Fetch Contact Details given by ContactID
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


        //This Method Generate Supplier PDFs given by Details  
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


                //MODIFIED HERE TO ADD DIVERY CHARGES FOR SUPPLIER


                String strDelCost = supdal.getDelCostBySupplierName(Supplier, OrderID);
                CreateSupplierPDF(Supplier, strInvoiceID, arrContact[3], arrContact[0], arrContact[1], arrContact[2], Lines, map_Notes, strDelCost);
            }

        }
        //End method Genrate Supplier PDFs Given by Details


        //This Method Create Supplier PDF Given by Details
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


            // string pdfFilePath = Server.MapPath(".") + "\\Invoices\\Supplier\\";
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

            document.Add(new Paragraph("PURCHASE ORDER: " + strOrderNo, font_14_semibold));
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

            /*PdfPTable OuterTable = new PdfPTable(2);
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
            PdfPCell cell = new PdfPCell(new Phrase("PURCHASE ORDER", titleFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.Colspan = 2;
            cell.HorizontalAlignment = 1;
            OuterTable.AddCell(cell);

            //Contact Organization Cell
            PdfPCell cellCompanyName = new PdfPCell(new Phrase("Organization Name:", fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(cellCompanyName);

            PdfPCell CompanyName = new PdfPCell(new Phrase(strContactOrganization, fntNormal));
            CompanyName.Border = Rectangle.NO_BORDER;
            CompanyName.PaddingTop = 1f;
            OuterTable.AddCell(CompanyName);

            //Contact Name Cell
            PdfPCell cellContactName = new PdfPCell(new Phrase("ContactName", fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(cellContactName);

            PdfPCell ContactName = new PdfPCell(new Phrase(strContactFullName, fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(ContactName);


            //Contact Address Cell
            PdfPCell cellContactAddress = new PdfPCell(new Phrase("Address", fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(cellContactAddress);


            PdfPCell ContactAddress = new PdfPCell(new Phrase(strContactAddress, fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(ContactAddress);


            //Purchase Order Number
            PdfPCell cellOrderNumber = new PdfPCell(new Phrase("Order Number", fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(cellOrderNumber);


            PdfPCell OrderNumber = new PdfPCell(new Phrase(strOrderNo, fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(OrderNumber);

            //Purchase Order Date
            PdfPCell cellOrderDate = new PdfPCell(new Phrase("Date", fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(cellOrderDate);

            PdfPCell OrderDate = new PdfPCell(new Phrase(orederDate, fntNormal));
            cellCompanyName.Border = Rectangle.NO_BORDER;
            cellCompanyName.PaddingTop = 1f;
            OuterTable.AddCell(OrderDate);

            document.Add(OuterTable);*/

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

            if (!delCharges.Equals("0"))
            {
                //Modified here 
                /*
                Cell = new PdfPCell(new Phrase("D/L Handling", font_Line_Item));
                Cell.HorizontalAlignment = 0;
                ItemsTable.AddCell(Cell);
                Cell = new PdfPCell(new Phrase("1", font_Line_Item));
                Cell.HorizontalAlignment = 1;
                ItemsTable.AddCell(Cell);
                Cell = new PdfPCell(new Phrase(delCharges, font_Line_Item));
                Cell.HorizontalAlignment = 1;
                ItemsTable.AddCell(Cell);
                */
            }

            //End loop through items to Populate Table

            //Add Supplier Notes for the Purchase Orders 

            document.Add(ItemsTable);

            /*PdfPTable ItemsTable = new PdfPTable(3);
            ItemsTable.DefaultCell.Border = Rectangle.NO_BORDER;

            ItemsTable.WidthPercentage = 100;
            ItemsTable.HorizontalAlignment = 0;
            ItemsTable.SpacingAfter = 10;
            float[] sglTblHdWidths = new float[3];
            sglTblHdWidths[0] = 200f;
            sglTblHdWidths[1] = 100f;
            sglTblHdWidths[2] = 200f;
            ItemsTable.SetWidths(sglTblHdWidths);


            PdfPCell CellOneHdr = new PdfPCell(new Phrase("SupplierCode", fntTableFontHdr));
            CellOneHdr.Border = Rectangle.BOTTOM_BORDER;
            CellOneHdr.BorderWidthBottom = 1f;
            ItemsTable.AddCell(CellOneHdr);

            //Define Table Header-Unit Price
            PdfPCell CellTreeHdr = new PdfPCell(new Phrase("Qty", fntTableFontHdr));
            CellTreeHdr.Border = Rectangle.BOTTOM_BORDER;
            CellTreeHdr.BorderWidthBottom = 1f;
            ItemsTable.AddCell(CellTreeHdr);


            //Define Table Header-Quanity
            PdfPCell CellTwoHdr = new PdfPCell(new Phrase("Unitprice", fntTableFontHdr));
            CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
            CellTwoHdr.BorderWidthBottom = 1f;
            ItemsTable.AddCell(CellTwoHdr);


            PdfPCell Cell;
            //Loop through Line items to Popualte the Table
            for (int i = 0; i < Lines.Length; i++)
            {
                if (!String.IsNullOrEmpty(Lines[i]))
                {
                    String[] items = Lines[i].Split(':');

                    //SupplierCode cell
                    Cell = new PdfPCell(new Phrase(items[0], fntTableFontHdr));
                    CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                    CellTwoHdr.BorderWidthBottom = 1f;
                    ItemsTable.AddCell(Cell);

                    //Quantity cell

                    Cell = new PdfPCell(new Phrase(items[1], fntTableFontHdr));
                    CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                    CellTwoHdr.BorderWidthBottom = 1f;
                    ItemsTable.AddCell(Cell);


                    //COG cell
                    Cell = new PdfPCell(new Phrase(items[2], fntTableFontHdr));
                    CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                    CellTwoHdr.BorderWidthBottom = 1f;
                    ItemsTable.AddCell(Cell);

                }

            }

            //End loop through items to Populate Table

            //Add Supplier Notes for the Purchase Orders 

            document.Add(ItemsTable);*/


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
        //End Method Create Supplier PDF given by Details


        //This Method Generate the Customer Invoice given by Details
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
        //End Method Generate Invoice PDF 

        #endregion


        //Edit Order Related Functions

        //This method  fetch contact given ContactId
        protected String getContactbyContactId(String contactID)
        {
            String strContact = String.Empty;
            return strContact;
        }


        /// <summary>
        /// Fetch the Order details 
        /// </summary>
        /// <param name="orderid">OrderID</param>
        /// <returns></returns>
        protected String getOrderbyOrderID(int orderid)
        {

            String strOrder = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "select * from dbo.DummyOrders where OrderID=" + orderid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        String strORDERDATE = Convert.ToDateTime(sdr["OrderedDateTime"]).ToString("yyyy-MM-dd");
                        String DueDate = Convert.ToDateTime(sdr["DueDate"]).ToString("yyyy-MM-dd");
                        String Reference = sdr["Reference"].ToString();
                        String delCharges = sdr["CustomerDelCost"].ToString();
                        String strGuid = (!DBNull.Value.Equals(sdr["XeroGUID"])) ? sdr["XeroGUID"].ToString() : String.Empty;
                        String PaymentTerms = (!DBNull.Value.Equals(sdr["PaymentTerms"])) ? sdr["PaymentTerms"].ToString() : String.Empty;
                        //Modification done here Get Order Crearte Date
                        //String OrderCreateDate = (!DBNull.Value.Equals(sdr["CreatedDateTime"])) ? sdr["CreatedDateTime"].ToString() : String.Empty;
                        String OrderCreateDate = Convert.ToDateTime(sdr["CreatedDateTime"]).ToString("yyyy-MM-dd");

                        strOrder = strOrder + sdr["SuppDeltems"].ToString() + ":" + sdr["ProItems"].ToString() + ":" + sdr["CusDelCostItems"].ToString() + ":" + sdr["Status"].ToString() + ":" + strORDERDATE + ":" + DueDate + ":" + Reference + ":" + delCharges + ":" + strGuid + ":" + PaymentTerms + ":" + OrderCreateDate;
                        if (!IsPostBack && !String.IsNullOrEmpty(sdr["Notes"].ToString()))
                        {
                            OrderNotes.Text = sdr["Notes"].ToString();
                        }
                    }
                }
            }
            conn.Close();

            return strOrder;

        }

        //This Method fetch OrderItems by OrderID
        protected String getOrderItemsbyOrderID(int OrderID)
        {

            String strOrderitems = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            String strSqlStmt = "select OT.* , IT.PriceLock, IT.ReportedFaulty from dbo.DummyOrdered_Items OT INNER JOIN dbo.Items IT ON IT.ItemID = OT.ItemCode where OT.OrderID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOrderitems = strOrderitems + sdr["ItemCode"].ToString() + "," + sdr["Description"] + "," + sdr["UnitAmount"] + "," + sdr["COGamount"] + "," + sdr["SupplierCode"] + "," + sdr["Quantity"] + "," + sdr["SupplierName"] + "," + sdr["PriceLock"].ToString().Trim() + "," + sdr["ReportedFaulty"].ToString().Trim();
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
            String strSqlStmt = "select Top 1 * from dbo.DummyOrders where CreatedBy='" + strCratedBy + "'Order by  CreatedDateTime Desc";
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
        protected int CreateOrder(String strCompanyID, String strContactId, float COGTotal, float COGSubTotal,
            float ProfitTotal, float ProfitSubtotal, float SuppDelCost, float CusDelCost, float ProItemCost,
            String strCreatedBy, String SuppDelItems, String ProItems, String strCusDelItems, String status, DateTime duedate,
            String strOrderedBy, String OrderNotesStr, String PaymentTerms, String Reference, String TypeOfCall, DateTime orderDate,
            Boolean CommishSplit, String SplitWithID, String SplitWithName, String AccountOwner, String AccountOwnerID,
            String SplitVolume, String Urgency)
        {
            int rowsEffected = -1;
            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //Modification done here add Order DueDate  and OrderedBy(ownership) 30/04/2015
            String strSqlStmt = "insert into dbo.DummyOrders(CompanyID,ContactID,COGTotal,COGSubTotal,Total,SubTotal,SupplierDelCost,CustomerDelCost,ProItemCost,OrderedDateTime,CreatedDateTime,CreatedBy,SuppDeltems,ProItems,CusDelCostItems,Status,DueDate,OrderedBy, Notes, PaymentTerms, Reference, TypeOfCall, CommishSplit, SplitWith, SplitWithID, OrderEnteredBy, OrderEnteredByID, AccountOwner, AccountOwnerID, VolumeSplitAmount, Urgency) values (@CompanyID,@ContactID,@COGtotal,@CogSubtotal,@ProfitTotal,@ProfitSubTotal,@SuppDelCost,@CusDelCost,@ProItemCost,@OrderDate,CURRENT_TIMESTAMP,@CreatedBy,@SuppDelItems,@Proitems,@CusDelItems,@Status,@duedate,@orderedby, @OrderNotes, @PaymentTerms, @Reference, @TypeOfCall, @CommishSplit, @SplitWith, @SplitWithID, @OrderEnteredBy, @OrderEnteredByID, @AccountOwner, @AccountOwnerID, @VolumeSplitAmount, @Urgency);";
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
            //cmd.Parameters.AddWithValue("@ClientNotified", NotifyClient);
            cmd.Parameters.AddWithValue("@CommishSplit", CommishSplit);
            cmd.Parameters.AddWithValue("@SplitWith", SplitWithName);
            cmd.Parameters.AddWithValue("@SplitWithID", SplitWithID);
            cmd.Parameters.AddWithValue("@OrderEnteredBy", SplitWithName);
            cmd.Parameters.AddWithValue("@OrderEnteredByID", SplitWithID);

            cmd.Parameters.AddWithValue("@AccountOwner", AccountOwner);
            cmd.Parameters.AddWithValue("@AccountOwnerID", AccountOwnerID);
            cmd.Parameters.AddWithValue("@VolumeSplitAmount", SplitVolume);
            cmd.Parameters.AddWithValue("@Urgency", Urgency);
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
        protected int CreateOrderItems(int OrderID, String strItemDesc, String strItemCode, float UnitAmout, int quantity, float COGAmount, String strCratedBy, String strSupplierItemCode, String strSuppName)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlOrderedItemStmt = "insert into dbo.DummyOrdered_Items(OrderID,Description,ItemCode,UnitAmount,Quantity,CreatedDateTime,CreatedBy,COGamount,SupplierCode,SupplierName)values (@OrderID,@ItemDescription,@ItemCode,@UnitAmout,@qty,CURRENT_TIMESTAMP,@CreatedBy,@COGAmout,@SupplierCode,@SuppName);";
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderID);
            cmd.Parameters.AddWithValue("@ItemDescription", strItemDesc);
            cmd.Parameters.AddWithValue("@ItemCode", strItemCode);
            cmd.Parameters.AddWithValue("@UnitAmout", UnitAmout);
            cmd.Parameters.AddWithValue("@qty", quantity);
            cmd.Parameters.AddWithValue("@CreatedBy", strCratedBy);
            cmd.Parameters.AddWithValue("@COGAmout", COGAmount);
            cmd.Parameters.AddWithValue("@SupplierCode", strSupplierItemCode);
            cmd.Parameters.AddWithValue("@SuppName", strSuppName);

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


        //This Function Remove the OrderItems given by OrderID


        protected string GetPreviousOrderItemsString(int orderId, string orderItemsStrings)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var orderItemList = new OrderDAL(connectionString).getOrderItemListByOrderId(orderId.ToString());
            foreach (var item in orderItemList)
            {
                if (orderItemsStrings == "")
                    orderItemsStrings = " Deleted Items: Item Description :" + item.Description + " Quantity " + item.Quantity + " Unit Amount " + item.UnitAmount + " ";
                else
                    orderItemsStrings = orderItemsStrings + " Item Description :" + item.Description + " Quantity " + item.Quantity + " Unit Amount " + item.UnitAmount + " ";

            }

            return orderItemsStrings;
        }

        protected int DeleteOrderItems(int OrderId, out string orderItemsString)
        {
            orderItemsString = string.Empty;
            // orderItemsString = GetPreviousOrderItemsString(OrderId, orderItemString);
            int intRowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strDelStmt = "delete dbo.DummyOrdered_Items where OrderID=" + OrderId;
            SqlCommand cmd = new SqlCommand(strDelStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderId);
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

        //Updates the corresponding hidden fields based on the drop down list change
        protected void SplitWithChange(object sender, EventArgs e)
        {
            ACCOUNT_OWNER_TXT.Value = splitcommissionwith.SelectedItem.ToString();
            ACCOUNT_OWNER_ID.Value = splitcommissionwith.SelectedValue;

        }

        protected String getOrderStatus(String OID)
        {
            String OrderStatus = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Status FROM dbo.DummyOrders WHERE OrderID = " + OID;
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            OrderStatus = sdr["Status"].ToString();
                        }
                    }
                    conn.Close();
                }

            }

            return OrderStatus;
        }


        //This Function Update Order  given by Values
        protected int UpdateOrder(int OrderId, String strCompanyID, String strContactId,
            float COGTotal, float COGSubTotal, float ProfitTotal, float ProfitSubtotal, float SuppDelCost,
            float CusDelCost, float ProItemCost, String strCreatedBy, String SuppDelItems, String ProItems, String strCusDelItems,
            String strAlteredBy, DateTime Orderduedate, String strorderedby, String OrderNotesStr, String PaymentTerms,
            String Reference, String TypeOfCall, String OrderStatus, DateTime orderDate,
            String SplitVolume, String SplitWithID, String SplitWith, String Urgency)
        {
            int intRowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //Modification here 30/04/2015 add Updation due date

            String strSQLUpdateStmt = String.Empty;


            if (OrderStatus == "QUOTED")
            {
                strSQLUpdateStmt = "Update dbo.DummyOrders set OrderedDateTime=@OrderedDate,CreatedBy=@CreatedBy,Total=@Total,SubTotal=@SubTotal,AlteredDateTime=CURRENT_TIMESTAMP,AlteredBy=@AlteredBy,COGTotal=@COGTotal,COGSubTotal=@COGSubTotal,SupplierDelCost=@SupDelCost,CustomerDelCost=@CusDelCost,ProItemCost=@ProitemCost,SuppDeltems=@SupDelItems,ProItems=@ProItems,CusDelCostItems=@CusDelCosItems,DueDate=@duedate,OrderedBy=@OrderedBy, Notes=@OrderNotes, PaymentTerms=@PaymentTerms, Reference=@Reference, Status='PENDING', TypeOfCall=@TypeOfCall, VolumeSplitAmount=@VolumeSplitAmount, OrderEnteredBy=@SplitWith, OrderEnteredByID=@SplitWithID, Urgency=@Urgency where OrderID=@OrderID";
            }
            else
            {
                strSQLUpdateStmt = "Update dbo.DummyOrders set OrderedDateTime=@OrderedDate,CreatedBy=@CreatedBy,Total=@Total,SubTotal=@SubTotal,AlteredDateTime=CURRENT_TIMESTAMP,AlteredBy=@AlteredBy,COGTotal=@COGTotal,COGSubTotal=@COGSubTotal,SupplierDelCost=@SupDelCost,CustomerDelCost=@CusDelCost,ProItemCost=@ProitemCost,SuppDeltems=@SupDelItems,ProItems=@ProItems,CusDelCostItems=@CusDelCosItems,DueDate=@duedate,OrderedBy=@OrderedBy, Notes=@OrderNotes, PaymentTerms=@PaymentTerms, Reference=@Reference, TypeOfCall=@TypeOfCall, VolumeSplitAmount=@VolumeSplitAmount, OrderEnteredBy=@SplitWith, OrderEnteredByID=@SplitWithID, Urgency=@Urgency where OrderID=@OrderID";
            }

            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderId);
            cmd.Parameters.AddWithValue("@AlteredBy", strAlteredBy);
            cmd.Parameters.AddWithValue("@Total", ProfitTotal);
            cmd.Parameters.AddWithValue("@orderName", ProfitTotal);
            cmd.Parameters.AddWithValue("@SubTotal", ProfitSubtotal);
            cmd.Parameters.AddWithValue("@COGTotal", COGTotal);
            cmd.Parameters.AddWithValue("@COGSubTotal", COGSubTotal);
            cmd.Parameters.AddWithValue("@SupDelCost", SuppDelCost);
            cmd.Parameters.AddWithValue("@CusDelCost", CusDelCost);
            cmd.Parameters.AddWithValue("@ProitemCost", ProItemCost);

            //Modification done Add SuppDelCost and Promotional Item Cost
            cmd.Parameters.AddWithValue("@SupDelItems", SuppDelItems);
            cmd.Parameters.AddWithValue("@ProItems", ProItems);
            cmd.Parameters.AddWithValue("@CusDelCosItems", strCusDelItems);
            //Modification dond here Add Updation Due Date and AccountOwner
            cmd.Parameters.AddWithValue("@duedate", Orderduedate);
            cmd.Parameters.AddWithValue("@OrderedBy", strorderedby);
            cmd.Parameters.AddWithValue("@OrderNotes", OrderNotesStr);

            //Modification to add Payment Terms, Reference and Type of Call
            cmd.Parameters.AddWithValue("@PaymentTerms", PaymentTerms);
            cmd.Parameters.AddWithValue("@Reference", Reference);
            cmd.Parameters.AddWithValue("@TypeOfCall", TypeOfCall);

            //Modified here Add User Defined Date
            cmd.Parameters.AddWithValue("@OrderedDate", orderDate);


            cmd.Parameters.AddWithValue("@SplitWith", SplitWith);
            cmd.Parameters.AddWithValue("@SplitWithID", SplitWithID);

            //Modified here 
            cmd.Parameters.AddWithValue("@CreatedBy", strCreatedBy);
            cmd.Parameters.AddWithValue("@VolumeSplitAmount", SplitVolume);

            cmd.Parameters.AddWithValue("@Urgency", Urgency);
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
            var previousValues = new OrderDAL(connectionstring).getORDER_STATUS(ORDERID);
            if (ChangeOrderStatus(ORDERID) > 0)
            {

                Repository Res = xero.CreateRepository();
                String connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                orderdal = new OrderDAL(connString);
                String xeroGuid = orderdal.getOrderGuid(ORDERID);
                //String Order_Reference = ReferenceFld.Text;
                String Order_Reference = RefID.Value;

                if (!String.IsNullOrEmpty(xeroGuid))
                {
                    XeroApi.Model.Invoice ApprovedInvoice = xero.ApproveInvoice(ORDERID, Res, xeroGuid, Order_Reference);
                    if (ApprovedInvoice != null)
                    {
                        deltoneInvoice_Number = ApprovedInvoice.InvoiceNumber.ToString();
                        deltoneInvoice_ID = ApprovedInvoice.InvoiceID.ToString();

                        var columnName = "Status";
                        var talbeName = "Order";
                        var ActionType = "APPROVED";
                        int primaryKey = ORDERID;



                        var newvalues = "Order invoice Id " + deltoneInvoice_Number + " :Order Status changed from " + previousValues + " to APPROVED";



                        var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = connectionstring;
                        var strCompanyID = new OrderDAL(connectionstring).getCompanyIDFromOrder(ORDERID);

                        new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
                      columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));

                    }
                }
                else
                {

                }

                // Dictionary<String, String> di_Notes = tempsuppdal.getNotesObject(ORDERID);
                //  Dictionary<String, String> di_suppitems = purchasedal.getPurchaseItemObject(ORDERID);

                //  GenerateSupplierDocs(ORDERID, deltoneInvoice_Number, CONTACTID.ToString(), di_suppitems, di_Notes);


                //Send Confirmation Email to the User 

                if (TCKCreateCSV.Checked)
                {
                    //  CreateCSV BuildtheCSV = new CreateCSV();
                    //BuildtheCSV.BuildCSV(ORDERID.ToString());

                }

                if (TCKNotifyClient.Checked)
                {
                    if (deltoneInvoice_Number != null)
                    {
                        DeltonelEmailSender mailSender = new DeltonelEmailSender();
                        // mailSender.SendMail(String.Empty, ORDERID.ToString(), "dimitri@deltonesolutions.com.au", deltoneInvoice_Number);
                    }
                }


                CreateCSV BuildtheCSV = new CreateCSV();     // Creating CSV file.
                // BuildtheCSV.BuildCSV(ORDERID.ToString());




                String Message = "<h2>ORDER: " + deltoneInvoice_Number + " HAS BEEN AUTHORIZED</h2>";
                String NavigateUrl = "Orders/AllOrders.aspx";
                String PrintURl = "PrintOrder.aspx?Oderid=" + ORDERID + "&cid=" + CONTACTID + "&Compid=" + COMPANYID;
                string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }
            else
            {
                String Message = "<h2>ERROR AUTHORIZED ORDER: " + deltoneInvoice_Number + "</h2>";
                String NavigateUrl = "CompanyOrders.aspx?ContactID=" + CONTACTID + "&CompanyID=" + COMPANYID + "&Order=" + ORDERID + "&Update=Success";
                String PrintURl = "PrintOrder.aspx?Oderid=" + ORDERID + "&cid=" + CONTACTID + "&Compid=" + COMPANYID;
                string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            }


        }
        //End Method Approve Status of the Order

        protected int UpdateOrderNameXeroDTS(int OrderId)
        {
            string orderName = orderNameTextBox.Text;
            int RowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateOrderStatus = "Update dbo.DummyOrders SET XeroGUID=@nameUpdate WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strUpdateOrderStatus, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderId);
            cmd.Parameters.AddWithValue("@nameUpdate", orderName);
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

        //This Method Change the Order Status Given by OrderID
        protected int ChangeOrderStatus(int OrderId)
        {

            int RowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateOrderStatus = "Update dbo.DummyOrders SET Status='APPROVED' WHERE OrderID=@OrderID";
            SqlCommand cmd = new SqlCommand(strUpdateOrderStatus, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderId);
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

        //Builds the Dropdown list of users for the split commission
        protected void BuildDropDownListForSplitCommission(String LoggedUserID)
        {
            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins", conn);
                adapter.Fill(subjects);
                splitcommissionwith.DataSource = subjects;
                splitcommissionwith.DataTextField = "FullName";
                splitcommissionwith.DataValueField = "LoginID";
                splitcommissionwith.DataBind();
            }

            splitcommissionwith.Items.FindByValue(LoggedUserID).Selected = true;
        }

        //Company OwnerShip details dropdown List Population
        protected void PopulateDropDownList(int CompanyID)
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins", conn);
                adapter.Fill(subjects);
                ddlUsers.DataSource = subjects;
                ddlUsers.DataTextField = "FullName";
                ddlUsers.DataValueField = "LoginID";
                ddlUsers.DataBind();
            }

            //Get the Current Account OwnerShip
            String strAccountOwnerShip = CompanyOwnerShip(CompanyID);
            //String[] arr = strAccountOwnerShip.Split(':');
            //String firstname = arr[0];
            //String ownershipid = arr[1];

            ddlUsers.Items.FindByValue(strAccountOwnerShip).Selected = true;


            if (Session["LoggedUserID"] != null)
            {
                //ddlUsers.Items.FindByValue(Session["LoggedUserID"].ToString()).Selected = true;
                //ddlUsers.Items.FindByText(firstname).Selected = true;
            }

        }


        //This method Handles the Split Commision 
        protected void SplitCommission(int OrderID, String AccountOwner_ID, String SalespersonID, String AccountOwner_Commish, String Salesperson_Commish, Boolean SplitTick)
        {

            String strOutPut = String.Empty;

            OrderDAL oDAL = new OrderDAL(connString);
            String CreatedDateTime = oDAL.DummyOrderCreatedDate(OrderID);
            String OrderStatus = String.Empty;

            OrderStatus = getOrderStatus(OrderID.ToString());

            if (SplitTick == true)
            {
                orderdal.WriteDummyCommision(OrderID, Int32.Parse(AccountOwner_ID), float.Parse(AccountOwner_Commish), "YES", "SYSTEM", Convert.ToDateTime(CreatedDateTime), OrderStatus);
                orderdal.WriteDummyCommision(OrderID, Int32.Parse(SalespersonID), float.Parse(Salesperson_Commish), "YES", "SYSTEM", Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }
            else
            {
                orderdal.WriteDummyCommision(OrderID, Int32.Parse(AccountOwner_ID), float.Parse(AccountOwner_Commish), "YES", "SYSTEM", Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }

            /*
            int CompnyOwnerShip =  Int32.Parse(CompanyOwnerShip(CompanyID));
            if (LoggedID == CompnyOwnerShip)
            {

                orderdal.WriteCommision(OrderID, LoggedID, commissionvalue, "NO", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }
            else
            {
                float newCommisionValue =(float)Math.Round((commissionvalue * 0.5),2);
                orderdal.WriteCommision(OrderID, LoggedID, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
                orderdal.WriteCommision(OrderID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }
             */

        }


        //This Method Update Commision Entries in DB
        protected void UpdateCommissions(int OrderID, String AccountOwner_ID, String SalespersonID, String AccountOwner_Commish, String Salesperson_Commish, Boolean SplitTick)
        {
            //Remove the Old Entry First where type='ORDER'
            //String output = orderdal.RemoveCommisionEntry(OrderID);


            OrderDAL oDAL = new OrderDAL(connString);
            String CreatedDateTime = oDAL.DummyOrderCreatedDate(OrderID);
            String OrderStatus = String.Empty;

            String output = dal_Commission.RemoveDummyCommissionEntry(OrderID, "ORDER");
            OrderStatus = getOrderStatus(OrderID.ToString());

            if (SplitTick == true)
            {
                orderdal.WriteDummyCommision(OrderID, Int32.Parse(AccountOwner_ID), float.Parse(AccountOwner_Commish), "YES", "SYSTEM", Convert.ToDateTime(CreatedDateTime), OrderStatus);
                orderdal.WriteDummyCommision(OrderID, Int32.Parse(SalespersonID), float.Parse(Salesperson_Commish), "YES", "SYSTEM", Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }
            else
            {
                orderdal.WriteDummyCommision(OrderID, Int32.Parse(AccountOwner_ID), float.Parse(AccountOwner_Commish), "YES", "SYSTEM", Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }

            //int CompnyOwnerShip =  Int32.Parse(CompanyOwnerShip(CompanyID));

            /*
            if (LoggedID == CompnyOwnerShip)
            {

                orderdal.WriteCommision(OrderID, LoggedID, commissionvalue, "NO", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }
            else
            {
                float newCommisionValue =(float)Math.Round((commissionvalue * 0.5),2);
                orderdal.WriteCommision(OrderID, LoggedID, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
                orderdal.WriteCommision(OrderID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }*/

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


        //This Function Update Supplier Notes 
        protected void UpdateSupplierNotes(int OrderID, String xeroInvoiceNumber, Dictionary<String, String> di)
        {
            //Remove the Previous Entry
            if (RemoveSupplierNotes(OrderID) > 0)
            {
                CreateSupplerNotes(OrderID, xeroInvoiceNumber, di);
            }
        }
        //End Function Update Supplier Notes

        //This Function Remove Previous Note Entries
        protected int RemoveSupplierNotes(int OrderID)
        {
            int intRowsEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStatement = "Delete from dbo.DummySupplierNotes where OrderID=@OrderId";
            SqlCommand cmd = new SqlCommand(strSQLInsertStatement, conn);
            cmd.Parameters.AddWithValue("@OrderId", OrderID);

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



        //This function create supplier notes According to the Dictionary Object 
        protected void CreateSupplerNotes(int intOrederID, String xeroInvoiceNumber, Dictionary<String, String> di)
        {

            foreach (var item in di)
            {

                AddSupplierNotes(intOrederID, item.Key, item.Value, xeroInvoiceNumber);
            }

        }
        //End Function Add Supplier notes According to Dictionary Object

        //This Function Fetch SupplierNotes given by OrderID
        protected String FetchSupplierNotes(int OrderID)
        {
            String strNotes = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "select * from dbo.DummySupplierNotes where Type='SalesOrder' and  OrderID=" + OrderID;
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
        protected String FetchReferenceInfo(int OrderID)
        {
            String strNotes = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "SELECT Reference FROM dbo.DummyOrders where OrderID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strNotes = sdr["Reference"].ToString();
                    }
                }
            }
            conn.Close();


            return strNotes;
        }

        protected String GetOrderName(int OrderID)
        {
            String strNotes = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlOrderStmt = "SELECT XeroGUID FROM dbo.DummyOrders where OrderID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["XeroGUID"] != DBNull.Value)
                            strNotes = sdr["XeroGUID"].ToString();
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
            String strSqlOrderStmt = "SELECT TypeOfCall FROM dbo.DummyOrders where OrderID=" + OrderID;
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


        //This Function Add Supplier Notes 
        protected int AddSupplierNotes(int intOrderID, String strSupplierName, String strNotes, String xeroInvoiceNumber)
        {
            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStatement = "INSERT INTO DummySupplierNotes values(@OrderID,@SupplierName,@Notes,@xeroInvNumber,'SalesOrder')";
            SqlCommand cmd = new SqlCommand(strSQLInsertStatement, conn);
            cmd.Parameters.AddWithValue("@OrderID", intOrderID);
            cmd.Parameters.AddWithValue("@SupplierName", strSupplierName);
            cmd.Parameters.AddWithValue("@Notes", strNotes);
            cmd.Parameters.AddWithValue("@xeroInvNumber", xeroInvoiceNumber);

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
        //End function Add Supplier Notes


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


        // Adds working days to date
        public static DateTime AddWorkingDays(DateTime specificDate,
                                      int workingDaysToAdd)
        {
            int completeWeeks = workingDaysToAdd / 5;
            DateTime date = specificDate.AddDays(completeWeeks * 7);
            workingDaysToAdd = workingDaysToAdd % 5;
            for (int i = 0; i < workingDaysToAdd; i++)
            {
                date = date.AddDays(1);
                while (!IsWeekDay(date))
                {
                    date = date.AddDays(1);
                }
            }
            return date;
        }

        private static bool IsWeekDay(DateTime date)
        {
            DayOfWeek day = date.DayOfWeek;
            return day != DayOfWeek.Saturday && day != DayOfWeek.Sunday;
        }

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
            Response.Redirect("dashboard1.aspx");
        }

        protected void btnaccountDash_Click(object sender, EventArgs e)
        {
            var comId = Request.QueryString["Compid"];
            Response.Redirect("ConpanyInfo.aspx?companyid=" + comId);
        }

    }
}


