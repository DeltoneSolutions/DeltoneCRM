

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
using DeltoneCRM_DAL;
using DeltoneCRM.Classes;
using System.Globalization;

namespace DeltoneCRM
{
    public partial class RepAllocateQuoteDisplay : System.Web.UI.Page
    {

        String strPendingApproval = "";
        String strLoggedUserProfile = "Admin";
        static String str_ORDERID;
        static int ORDERID;
        static int CONTACTID;
        static int COMPANYID;
        string BDType = "";
        static String COMPANYNAME;
        static String connString;
        SupplierDAL suppdal;
        OrderDAL orderdal;
        PurchaseDAL purchasedal;
        TempSuppNotes tempsuppdal;
        CompanyDAL companydal;
        QuoteDAL quotedal;
        //Modification done here
        static String ORDERSTATUS = String.Empty;

        String QuoteStatus = String.Empty;
        public string IsVisibleDateCallBack = "";
        String WhichDB = String.Empty;
        protected Guid id;
        public string QuoteEmailTemplate = "";
        public string ConfirmationEmailTemplate = "";
        public string ConfirmationEmailTemplateFooter = "";
        public string UrlQuote = "http://delcrm/RepAllocateQuoteDisplay.aspx?OderID={0}&CompID={1}&cid={2}&DB={3}&Flag=Y";
        public string CustomeFullName { get; set; }
        public string QuoteConfirmationTemplate = "Hi #firstnamerepace#," +

"<br/><br/> Thank you for taking time out of your day to discuss your #nnmm# printing requirements." +

" <br/><br/> Here at Deltone Solutions our main goal is to help you save money on your inks and toners without having to buy in bulk to get a discounted price." 
+"Via smart connections and partnering with world class manufactures we drive value back to our customers by providing a more efficient and quality performing product." +

"<br/><br/> Please find below the prices for your consumables.";


        public string QuoteTemplateEmail = "Dear #firstnamerepace#," +
"<br/><br/> Thank you for choosing Deltone Solutions. I have arranged the order according to our recent discussion and have listed the items and quantities for the order below.";

        public string QuoteTemplateEmailSecond = "<br/><br/> To confirm the order, please reply back to this email with the following:<span style=\"background-color: rgb(0, 255, 0);\">I #fullnamerepace# wish to confirm this order. </span> " +
         "<br/><br/> Standard delivery is  2-3 working days , but if the order is required urgently, we are able to offer  overnight delivery. Please advise us whether this is the case."
        + "All our goods also come out on a <span > 21 day account, however a 2% discount is offered on upfront credit card payments (except via Amex).</span>" +

           "<br/><br/> Have a great day and I look forward to speaking with you in the future. Should you require anything please contact me anytime, we are here to help.";
    
        protected void Page_Load(object sender, EventArgs e)
        {
            String CompanyID = String.Empty;
            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            suppdal = new SupplierDAL(connString);
            orderdal = new OrderDAL(connString);
            purchasedal = new PurchaseDAL(connString);
            tempsuppdal = new TempSuppNotes(connString);
            companydal = new CompanyDAL(connString);
            quotedal = new QuoteDAL(connString);
            QuoteEmailTemplate = QuoteConfirmationTemplate;
            ConfirmationEmailTemplate = QuoteTemplateEmail;
            ConfirmationEmailTemplateFooter = QuoteTemplateEmailSecond;

            String ContactFirstName = String.Empty;

            String LoggedUserID = Session["LoggedUserID"].ToString();


            WhichDB = Request.QueryString["DB"].ToString();

            hdnAllSuppliers.Value = suppdal.getAllSuppliers();

            btnConvertToOrder.Visible = false;
            btnCancelQuote.Visible = false;
            btnResendEmail.Visible = false;
            btnApproveQuote.Visible = false;

            QuoteByID.Value = LoggedUserID;


            SetEventDescription();


            if (Request.QueryString["Exists"] == "Y")
            {
                ExistingCustomer.Value = "YES";
            }
            else
            {
                ExistingCustomer.Value = "NO";
            }

            if (!String.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                if (!String.IsNullOrEmpty(Request.QueryString["Flag"]))
                {
                    String CustID = Request.QueryString["cid"].ToString();
                    hdnContactID.Value = CustID;
                    if (WhichDB == "QuoteDB" || WhichDB == "")
                    {
                        getNewCustomerDetails(CustID);

                    }
                    else
                    {
                        getExisitingCustomerDetails(CustID);
                    }

                    CONTACTID = Int32.Parse(CustID);
                    ContactFirstName = quotedal.getNewContactFirstName(CustID, WhichDB);
                    QuoteEmailTemplate = QuoteEmailTemplate.Replace("#firstnamerepace#", ContactFirstName);
                    ConfirmationEmailTemplate = ConfirmationEmailTemplate.Replace("#firstnamerepace#", ContactFirstName);
                    ConfirmationEmailTemplateFooter = ConfirmationEmailTemplateFooter.Replace("#fullnamerepace#", ContactFirstName);

                }
                else
                {
                    String CustID = Request.QueryString["cid"].ToString();
                    hdnContactID.Value = CustID;
                    getExisitingCustomerDetails(CustID);
                    //Set the ContactID
                    CONTACTID = Int32.Parse(CustID);
                    ContactFirstName = quotedal.getContactFirstName(CustID);
                    QuoteEmailTemplate = QuoteEmailTemplate.Replace("#firstnamerepace#", ContactFirstName);
                    ConfirmationEmailTemplate = ConfirmationEmailTemplate.Replace("#firstnamerepace#", ContactFirstName);
                    ConfirmationEmailTemplateFooter = ConfirmationEmailTemplateFooter.Replace("#fullnamerepace#", ContactFirstName);
                }

            }
            if (!String.IsNullOrEmpty(Request.QueryString["Compid"]))
            {
                if (!String.IsNullOrEmpty(Request.QueryString["Flag"]))
                {
                    CompanyID = Request.QueryString["Compid"].ToString();
                    hdnCompanyID.Value = CompanyID;
                    Session["companyId"] = CompanyID;
                    //Set CompanyID
                    COMPANYID = Int32.Parse(CompanyID);
                    Session["OpenedCompanyID"] = COMPANYID;
                    COMPANYNAME = orderdal.getNewCompanyName(COMPANYID, WhichDB);
                    AccountOwnertxt.Text = companydal.getCompanyAccountOwner(COMPANYID);
                    PopulateDropDownList(Int32.Parse(CompanyID));
                }
                else
                {
                    CompanyID = Request.QueryString["Compid"].ToString();
                    hdnCompanyID.Value = CompanyID;
                    //Set CompanyID
                    COMPANYID = Int32.Parse(CompanyID);
                    Session["OpenedCompanyID"] = COMPANYID;
                    COMPANYNAME = orderdal.getCompanyName(COMPANYID);
                    AccountOwnertxt.Text = companydal.getCompanyAccountOwner(COMPANYID);
                    PopulateDropDownList(Int32.Parse(CompanyID));
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
                laId.Text = OrderID.ToString();
                UrlQuote = string.Format(UrlQuote, ORDERID, COMPANYID, CONTACTID, WhichDB);
                String strOrderedItems = getOrderItemsbyOrderID(OrderID);
                hdnEditOrderItems.Value = strOrderedItems;
                Session["UrlQuote"] = UrlQuote;
                String strSupplierNotes = FetchSupplierNotes(OrderID);
                String strPromotionalItems = FetchProItems(OrderID);
                String strOrder = getOrderbyOrderID(OrderID);
                String strReference = FetchReferenceInfo(OrderID);
                String strTypeOfCall = FetchTypeOfCall(OrderID);
                String strPaymentTerms = String.Empty; //Modified Add Payment Terms

                SALESPERON_ID.Value = quotedal.getRealAccountOwnerID(OrderID);

                //Verify which status the email send should be
                String HasEmailBeenSent = quotedal.getQuoteEmailAlready(OrderID);
                var dateReceived = datereceived.Value;
                datereceived.Disabled = true;

                if (HasEmailBeenSent == "" || HasEmailBeenSent == "NO")
                {
                    EmailSend.Value = "SEND";
                }
                else
                {
                    EmailSend.Value = "DONOTSEND";
                }

                //Verify the logged user has authority to auto approve the email going out
                String AllowedToSend = String.Empty;
                //if (Session["USERPROFILE"].ToString() == "ADMIN")
                //{
                AllowedToSend = quotedal.getQuoteAutoApproveStatus(Int32.Parse(LoggedUserID));
                if (AllowedToSend == "NO")
                {
                    EmailSend.Value = "DONOTSEND";
                }
                else
                {
                    EmailSend.Value = "SEND";
                }
                // }


                QuoteStatus = quotedal.getQuoteStatus(OrderID);



                if (QuoteStatus == "PENDING")
                {
                    if (Session["USERPROFILE"].ToString() == "ADMIN")
                    {
                        btnApproveQuote.Visible = true;

                    }
                    btnCancelQuote.Visible = true;

                }
                else if (QuoteStatus == "ACTIVE")
                {
                    if (AllowedToSend == "YES")
                    {
                        btnResendEmail.Visible = true;
                        btnConvertToOrder.Visible = true;
                    }

                    btnCancelQuote.Visible = true;
                }

                String XeroGuid = String.Empty;

                if (!String.IsNullOrEmpty(strOrder))
                {
                    hdnEditOrder.Value = strOrder;
                    //Set the Order Status here 
                    String[] arr = strOrder.Split(':');
                    ORDERSTATUS = arr[3].ToString();
                    ORDER_STATUS.Value = arr[3].ToString();
                    String test = arr[4].ToString();
                    //Modification done add order create date
                    String OrderCreateDate = arr[10].ToString();
                    XeroGuid = arr[8].ToString();
                    strPaymentTerms = arr[9].ToString(); //Modified here Fetch Payment Here 
                    ORDER_DATE.Value = test;
                    ORDER_CREATE_DATE.Value = OrderCreateDate;//Modification done here
                    txt_EmailHeader.InnerText = quotedal.getEmailHeaderText(OrderID);
                    txt_EmailFooter.InnerText = quotedal.getEmailFooterText(OrderID);
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
                }

                if (Session["USERPROFILE"].ToString().Equals("STANDARD"))
                {
                    btnCancelInvoice.Style.Value = "display:none";
                }


                OrderDAL odal = new OrderDAL(connString);
                String strCreatedBy = quotedal.getQuoteCreatedBy(ORDERID);


                if (!IsPostBack)
                {
                    ddlUsers.ClearSelection();
                    if (!string.IsNullOrEmpty(strCreatedBy))
                        ddlUsers.Items.FindByText(strCreatedBy).Selected = true;
                    var quoteCategory = quotedal.getQuoteCategory(OrderID);
                    if (!string.IsNullOrEmpty(quoteCategory))
                    {
                        ddlquoteCategory.SelectedValue = quoteCategory;
                        if (quoteCategory == "1")
                        {
                            IsVisibleDateCallBack = "true";
                            var callBackDate = quotedal.getQuoteCategoryCallBackDate(OrderID);
                            callbackDate.Value = callBackDate;
                        }
                    }

                    LoadQuoteNotes(OrderID);
                    LoadAllocatedQuoteNotes(OrderID, CompanyID);
                }

            }

            else
            {
                if (Session["LoggedUserID"] != null)
                {
                    //ddlUsers.Items.FindByValue(Session["LoggedUserID"].ToString()).Selected = true;
                    //ddlUsers.Items.FindByText(firstname).Selected = true;
                }
                if (!IsPostBack)
                {
                    var headerEmail = "Hi " + ContactFirstName.Trim() + ",";
                    var emailHeadertemplate = "\n\rThank you for your time today. I really appreciate the opportunity to discuss your company’s printings needs.\n\r";
                    emailHeadertemplate = emailHeadertemplate + "Deltone Solutions is all about saving you time and" +
                                                                " money on your printer consumables. Our goal is to make your life easier by" +
                                                                 " supplying you with a reliable product without having to buy in bulk.\n\r";

                    emailHeadertemplate = emailHeadertemplate + "Please find below the prices for your consumables.";
                    txt_EmailHeader.Value = headerEmail + emailHeadertemplate;

                    txt_EmailFooter.Value = "All of our goods come to you on an open 21 day account, however a further 2% discount will apply for upfront credit card payments.\n\rPlease don't hesitate to contact us if you have any questions/queries.Looking forward to hearing from you.";
                }
                SALESPERON_ID.Value = Session["LoggedUserID"].ToString();

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

        protected void btnCancelQuote_Click(object sender, EventArgs e)
        {
            String theOrderNum = str_ORDERID;

            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var previousValues = new QuoteDAL(connectionstring).getQuoteStatus(Convert.ToInt32(theOrderNum));
            var companyId = new QuoteDAL(connectionstring).getCompayID(Convert.ToInt32(theOrderNum));

            var result = quotedal.cancelQuote(theOrderNum);
            var columnName = "Status";
            var talbeName = "Quote";
            var ActionType = "CANCELLED";
            int primaryKey = Convert.ToInt32(theOrderNum);


            var newvalues = " Quote Id " + theOrderNum + " :Credit Ortder Status changed from " + previousValues + " to CANCELLED";



            var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionstring;
            var strCompanyID = new CreditNotesDAL(connectionstring).getCompanyIDFromCreditID(ORDERID);

            new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(previousValues, newvalues, loggedInUserId, conn, 0,
          columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(companyId));

            //////// Refator this snippet///////
            String Message = "<h2>QUOTE: " + theOrderNum + " HAS BEEN CANCELLED</h2>";
            String NavigateUrl = "AllQuotes.aspx";
            String PrintURl = "#";
            string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            ///////End Refatoring///////////
        }

        protected void btnResendQuoteEmail_Click(object sender, EventArgs e)
        {
            if (QuoteStatus == "ACTIVE")
            {
                String theOrderNum = str_ORDERID;
                var loggedUserEmail = getLoggedInUserEmail(Convert.ToInt32(theOrderNum));
                DeltoneQuoteEmailSender EmailSender = new DeltoneQuoteEmailSender();

                EmailSender.SendMail(String.Empty, Convert.ToInt32(theOrderNum), "sentemails@deltonesolutions.com.au", loggedUserEmail);
                messageLabel.Text = "Email has been sent successfully.";

            }
        }

        protected void LoadQuoteNotes(int quoteID)
        {
            var Notes = getQuoteNotes(quoteID);
            displaynotesHistoryDiv.InnerHtml = Notes;
        }

        protected void LoadAllocatedQuoteNotes(int quoteID, string comId)
        {
            var prevoiusNote =string.Empty ;//getQuoteAllocatedNotes(Convert.ToInt32(quoteID));
            var listQuotesByCompany = GetQuotesByCompanyId(comId);
            foreach (var itme in listQuotesByCompany)
            {
                prevoiusNote = prevoiusNote + getQuoteAllocatedNotes(itme);
            }
            allocatedReptoes.InnerHtml = prevoiusNote;
        }

        private static string getQuoteNotes(int quoteId)
        {
            var columnName = "Notes";
            var talbeName = "Companies";
            var ActionType = "Updated";
            int primaryKey = quoteId;
            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var companyNote = new DeltoneCRM_DAL.CompanyDAL(connectionstring).getQuoteCompanyNotes(quoteId.ToString());




            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionstring;

            //   new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(companyNote, newdata, loggedInUserId, conn, 0,
            //   columnName, talbeName, ActionType, primaryKey, companyID);

            return companyNote;
        }

        private List<int> GetQuotesByCompanyId(string comId)
        {
            var listIds = new List<int>();

            String OutPut = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

            String strSqlOrderStmt = String.Empty;

            strSqlOrderStmt = "SELECT QuoteID FROM dbo.Quote WHERE CompanyID =" + comId;

            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        if (sdr["QuoteID"] != DBNull.Value)
                            listIds.Add(Convert.ToInt32(sdr["QuoteID"].ToString()));
                    }
                }
            }
            conn.Close();

            return listIds;

        }

        protected void btnApproveQuote_Click(object sender, EventArgs e)
        {
            String theOrderNum = str_ORDERID;
            var result = quotedal.approveQuote(theOrderNum);

            DeltoneQuoteEmailSender EmailSender = new DeltoneQuoteEmailSender();

            var loggedUserEmail = getLoggedInUserEmail(Convert.ToInt32(theOrderNum));

            EmailSender.SendMail(String.Empty, Convert.ToInt32(theOrderNum), "sentemails@deltonesolutions.com.au", loggedUserEmail);

            //////// Refator this snippet///////
            String Message = "<h2>QUOTE: " + theOrderNum + " HAS BEEN APPROVED AND EMAILED</h2>";
            String NavigateUrl = "AllQuotes.aspx";
            String PrintURl = "#";
            string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
            ///////End Refatoring///////////


        }

        private string getLoggedInUserEmail(int quoteID)
        {
            var dbConnString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            var userEmail = string.Empty;
            var createdBy = quotedal.getQuoteCreatedBy(quoteID);
            if (!string.IsNullOrEmpty(createdBy))
            {
                userEmail = new LoginDAL(dbConnString).getLoginUserEmailFromFirstName(createdBy.Split(' ')[0]);
            }
            if (string.IsNullOrEmpty(userEmail))
            {
                var loggedInUserId = (Session["LoggedUserID"].ToString());
                userEmail = new LoginDAL(dbConnString).getLoginUserEmailFromID(loggedInUserId);
            }

            return userEmail;

        }

        //CLOSE ORDER WINDOW Event handler
        protected void btnCloseOrderWindow_Click(object sender, EventArgs e)
        {
            string strScript = "<script language='javascript'>$(document).ready(function (){ CLOSE_ORDERDIALOG(); });</script>";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
        }


        // Adjust email footer text when Quote Validaty changes
        protected void ddlPayementTerms_Change(object sender, EventArgs e)
        {
            txt_EmailFooter.Value = "We send all goods on an open " + ddlPaymentTerms.Text.ToString() + " day account, however a 2% discount will be applied to orders on immediate credit card payment.\n\rPlease don't hesitate to contact us if you have any questions/queries. Looking forward to hearing from you, we're here to help.";
        }

        // Convert Quote to Order
        protected void btnConvertToOrder_Click(object sender, EventArgs e)
        {
            var quoteID = "";
            var comId = "";
            var contactId = "";
            var dbType = "";

            if (!string.IsNullOrEmpty(Request.QueryString["Compid"]))
            {
                comId = Request.QueryString["Compid"].ToString();

            }

            if (!string.IsNullOrEmpty(Request.QueryString["OderID"]))
            {
                quoteID = Request.QueryString["OderID"].ToString();

            }

            if (!string.IsNullOrEmpty(Request.QueryString["cid"]))
            {
                contactId = Request.QueryString["cid"].ToString();

            }

            if (!string.IsNullOrEmpty(Request.QueryString["DB"]))
            {
                dbType = Request.QueryString["DB"].ToString();

            }

            if (!string.IsNullOrEmpty(quoteID) && !string.IsNullOrEmpty(comId)
                && !string.IsNullOrEmpty(contactId) && !string.IsNullOrEmpty(dbType))
            {

                Response.Redirect("order.aspx?quoteId=" + quoteID + "&comId=" + comId + "&cId=" + contactId + "&dbType=" + dbType);
            }
            // quotedal.ConvertQuoteToOrder(17);


        }

        //Button Submit,Apporove Order Click Functions
        protected void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(hdnTotal.Value))
            {
                //return;
            }

            String strCompanyID = hdnCompanyID.Value;
            String strContactID = hdnContactID.Value;
            String strTest = hdnProfit.Value;
            String OrderNotesStr = OrderNotes.Text.ToString();
            String PaymentTermsStr = ddlPaymentTerms.Text.ToString();
            //String Reference = ReferenceFld.Text.ToString();
            String TypeOfCall = dllTypeOfCall.Text.ToString();
            String EmailHeaderNotes = String.Empty;
            String EmailFooterNotes = String.Empty;

            var reasonWhathappened = sellstatuswhathappened.Text;

            EmailHeaderNotes = txt_EmailHeader.InnerText;
            EmailFooterNotes = txt_EmailFooter.InnerText;

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

            //strCreatedBy = ddlUsers.SelectedItem.Text;

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
            //DateTime OrderDueDate = Convert.ToDateTime(datereceived.Value).AddDays(intPaymentTerms);
            DateTime OrderDueDate = Convert.ToDateTime("19/05/2016");

            //DateTime currentDate = DateTime.Now;
            //DateTime AddedDate = AddWorkingDays(currentDate, 3);



            //String strOwnerShipAdminID = ddlUsers.SelectedItem.Text;
            String strOwnerShipAdminID = "Taras Selemba";

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
                        if (!di_Notes.ContainsKey(noteLine[0]))
                            di_Notes.Add(noteLine[0], noteLine[1]);
                    }
                    else
                    {
                        if (!di_Notes.ContainsKey(noteLine[1]))
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

            var datereValue = datereceived.Value;
            if (string.IsNullOrEmpty(datereValue))
                datereValue = ORDER_CREATE_DATE.Value;



            if (!String.IsNullOrEmpty(Request.QueryString["OderID"]))//////Quote Updation Only in DataBase
            {
                QuoteDAL quote = new QuoteDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

                var quoteCategory = Convert.ToInt32(ddlquoteCategory.SelectedItem.Value);
                UrlQuote = string.Format(UrlQuote, ORDERID, COMPANYID, CONTACTID, WhichDB);
                Session["QuoteCategory"] = quoteCategory;
                Session["UrlQuote"] = UrlQuote;
                Session["OrderIdCreated"] = "true";

                int Orderid = Int32.Parse(Request.QueryString["OderID"].ToString());
                String OrderStatus = getOrderStatus(Request.QueryString["OderID"].ToString());


                string orderItemsString = "";
                string newvalues = "";



                DateTime callBackdate;
                DateTime? dateTimeCallBack = null;

                if (quoteCategory == 1)
                {
                    if (DateTime.TryParse(callbackDate.Value, out callBackdate))
                    {
                        dateTimeCallBack = callBackdate;
                        IsVisibleDateCallBack = "true";
                    }

                }

                //Delete Order items 
                if (DeleteOrderItems(Orderid, out orderItemsString) > 0)
                {
                    //Update the Order 
                    if (UpdateOrder(Orderid, strCompanyID, strContactID, COGTotal, CogSubTotal, profitTotal,
                        profitSubTotal, SuppDelCost, CusDelCost, ProItemCost, strCreatedBy, strSuppDelItems, strProitems, strCustDelCostItems, strCreatedBy,
                        OrderDueDate, strOwnerShipAdminID, OrderNotesStr, PaymentTermsStr, TypeOfCall, Convert.ToDateTime(datereValue),
                        quoteCategory, dateTimeCallBack, reasonWhathappened) > 0)
                    {
                        var quoteCategoryStr = "";
                        if (quoteCategory == 0)
                            quoteCategoryStr = " New";
                        else
                            if (quoteCategory == 1)
                                quoteCategoryStr = " Call back";
                            else
                                if (quoteCategory == 2)
                                    quoteCategoryStr = " May be";
                                else
                                    if (quoteCategory == 3)
                                        quoteCategoryStr = " Sold";
                                    else
                                        quoteCategoryStr = " No";

                        newvalues = " Quote Id " + Orderid + " ,Quote Id Status " + OrderStatus + " ,Quote Note " + OrderNotesStr + " ,Quote Category " + quoteCategoryStr + " ,Created By " + Session["LoggedUser"].ToString() + " ";
                        //Insert New Order Items 
                        for (int i = 0; i < strOrderItems.Length; i++)
                        {
                            itemList = strOrderItems[i].Split(',');
                            if (strOrderItems[i] != String.Empty)
                            {
                                if (i == 0)
                                {

                                    int firstItem = CreateOrderItems(Orderid, itemList[1], itemList[0],
                                        float.Parse(itemList[5]), Int32.Parse(itemList[4]), float.Parse(itemList[3]), strCreatedBy, itemList[2], itemList[6]);
                                    orderItemsString = orderItemsString + " Newly created Quote Items : Description " + itemList[1] + ", Created By " + Session["LoggedUser"].ToString() +
                                    ", Quantity " + itemList[4].ToString() +
                                   " , SupplierName " + itemList[6] + " ";
                                }
                                //Rest of the Elements
                                if (i != 0)
                                {
                                    int Item = CreateOrderItems(Orderid, itemList[2], itemList[1], float.Parse(itemList[6]),
                                        Int32.Parse(itemList[5]), float.Parse(itemList[4]), strCreatedBy, itemList[3], itemList[7]);

                                    orderItemsString = orderItemsString + " Order Quote : Description " + itemList[2] + ", Created By " + Session["LoggedUser"].ToString() +
                                     ", Quantity " + itemList[5].ToString() +
                                    " , SupplierName " + itemList[7] + " ";
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
                        //UpdateCommissions(Orderid, Int32.Parse(strCompanyID), Int32.Parse(ddlUsers.SelectedValue.ToString()), commission, OrderStatus);

                        //UpdateCommissions(Orderid, Int32.Parse(strCompanyID), Int32.Parse(Session["LoggedUserID"].ToString()), commission);
                    }
                    #endregion


                    #region purchase  Items Updation for Supplier Docs
                    //purchasedal.UpdatePurhcaseItems(Orderid, di, Session["LoggedUser"].ToString());
                    #endregion

                    #region XeroEntry Updation with the Status DRAFT

                    String deltoneInvoice_Number = Orderid.ToString();
                    String deltoneInvoice_ID = String.Empty;
                    //String InvoiceReference = ReferenceFld.Text;

                    connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    orderdal = new OrderDAL(connString);


                    XeroApi.Model.Invoice INV = null; //FOR CREATION AND UPDATION OF THE INVOICE


                    //Fetch the Xero Guid from CRM Table
                    //String xeroGuid = orderdal.getOrderGuid(Orderid);

                    /*if (!String.IsNullOrEmpty(xeroGuid)) //If Xero Entry is there
                    {
                        XeroApi.Model.Invoice updatedInvoice = null;

                        INV = updatedInvoice;

                        if (updatedInvoice != null)
                        {
                            deltoneInvoice_Number = updatedInvoice.InvoiceNumber.ToString();
                            deltoneInvoice_ID = updatedInvoice.InvoiceID.ToString();
                        }

                    }
                    else //Xero Entry hasn't been created and reCreate the Entry in the Xero and Update the Deltone Tables
                    {
                        XeroApi.Model.Invoice recreate_Invoice = null;

                        INV = recreate_Invoice;

                        if (recreate_Invoice != null)
                        {
                            //deltoneInvoice_Number = recreate_Invoice.InvoiceNumber.ToString();
                            //deltoneInvoice_ID = recreate_Invoice.InvoiceID.ToString();
                           // updateOrderStatus(deltoneInvoice_Number, deltoneInvoice_ID, Int32.Parse(Orderid.ToString()));
                        }
                    }*/

                    UpdateSupplierNotes(Orderid, deltoneInvoice_Number, di_Notes);

                    if (ORDERSTATUS.Equals("APPROVED"))
                    {
                        Dictionary<String, String> dinotes = tempsuppdal.getNotesObject(ORDERID);
                        Dictionary<String, String> di_suppitems = purchasedal.getPurchaseItemObject(ORDERID);
                    }

                    #endregion
                    var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                    var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = connectionstring;

                    var columnName = "Quote Table And Quote Items All columns";
                    var talbeName = "Quote And Quote Items";
                    var ActionType = "Updated Quote And QuoteItems";
                    int primaryKey = Orderid;
                    var lastString = newvalues + orderItemsString;

                    new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", lastString, loggedInUserId, conn, 0,
           columnName, talbeName, ActionType, primaryKey, Convert.ToInt32(strCompanyID));


                    //////// Refator this snippet///////
                    String Message = "<h2>ORDER: " + deltoneInvoice_Number + " HAS BEEN EDITED</h2>";
                    String NavigateUrl = "AllQuotes.aspx";
                    String PrintURl = "PrintQuote.aspx?Oderid=" + Orderid + "&cid=" + strContactID + "&Compid=" + strCompanyID;
                    string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
                    ///////End Refatoring///////////

                }

            }

            else  ////Quote Creation ////
            {

                QuoteDAL quote = new QuoteDAL(ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString);

                String strOrderStatus = String.Empty;
                if (!String.IsNullOrEmpty(Session["USERPROFILE"] as String))
                {
                    strOrderStatus = "PENDING";
                }

                String QuoteStatus = String.Empty;

                if (Session["USERPROFILE"].ToString() == "ADMIN")
                {
                    QuoteStatus = "ACTIVE";
                }
                else
                {
                    String canSend = quotedal.getQuoteAutoApproveStatus(Int32.Parse(Session["LoggedUserID"].ToString()));
                    if (canSend == "NO")
                    {
                        QuoteStatus = "PENDING";
                    }
                    else
                    {
                        QuoteStatus = "ACTIVE";
                    }

                }

                String QuoteLocation = String.Empty;

                if (String.IsNullOrEmpty(Request.QueryString["DB"]))
                {
                    QuoteLocation = "QuoteDB";
                }
                else
                {
                    if (Request.QueryString["DB"] == "LiveDB")
                    {
                        QuoteLocation = "LiveDB";
                    }
                    else if (Request.QueryString["DB"] == "QuoteDB")
                    {
                        QuoteLocation = "QuoteDB";
                    }

                }
                //End Checking User Profile
                int NewQuoteNumber = 0;
                strCreatedBy = Session["LoggedUser"].ToString();
                var quoteCategory = Convert.ToInt32(ddlquoteCategory.SelectedItem.Value);
                var creditNoteMainString = "";
                string noteItemsString = "";
                DateTime callBackdate;
                DateTime? dateTimeCallBack = null;

                if (quoteCategory == 1)
                {
                    if (DateTime.TryParse(callbackDate.Value, out callBackdate))
                    {
                        dateTimeCallBack = callBackdate;
                        IsVisibleDateCallBack = "true";
                    }

                }

                //Modified here Add User Defined Date
                var orderIdCreated = quote.InsertQuote(strCompanyID, strContactID, COGTotal, CogSubTotal, profitTotal,
                     profitSubTotal, SuppDelCost, CusDelCost, ProItemCost, strCreatedBy,
                     strSuppDelItems, strProitems, strCustDelCostItems, strOwnerShipAdminID,
                     OrderNotesStr, PaymentTermsStr, TypeOfCall, EmailHeaderNotes, EmailFooterNotes,
                     QuoteLocation, QuoteStatus, QuoteByID.Value, quoteCategory, dateTimeCallBack, reasonWhathappened);
                if (orderIdCreated > 0)
                {
                    Session["OrderIdCreated"] = "true";
                    Session["QuoteCategory"] = quoteCategory;


                    int QuoteID = LastOrderID(strCreatedBy);

                    UrlQuote = string.Format(UrlQuote, QuoteID, COMPANYID, CONTACTID, QuoteLocation);

                    Session["UrlQuote"] = UrlQuote;

                    NewQuoteNumber = QuoteID;
                    var quoteCategoryStr = "";
                    if (quoteCategory == 0)
                        quoteCategoryStr = " New";
                    else
                        if (quoteCategory == 1)
                            quoteCategoryStr = " Call back";
                        else
                            if (quoteCategory == 2)
                                quoteCategoryStr = " May be";
                            else
                                if (quoteCategory == 3)
                                    quoteCategoryStr = " Sold";
                                else
                                    quoteCategoryStr = " No";
                    creditNoteMainString = " Quote Id : " + QuoteID
                       + " Created By :" + strCreatedBy + " Notes :" + OrderNotesStr + " , Quote Category " + quoteCategoryStr + " ,Type of call " + TypeOfCall + " , Status: " + QuoteStatus;
                    ////Refator this code snippet//////
                    for (int i = 0; i < strOrderItems.Length; i++)
                    {
                        itemList = strOrderItems[i].Split(',');
                        if (strOrderItems[i] != String.Empty)
                        {
                            //For the First Element
                            if (i == 0)
                            {
                                int firstItem = quote.CreateQuoteItems(QuoteID, itemList[1], itemList[0],
                                    float.Parse(itemList[5]), Int32.Parse(itemList[4]), float.Parse(itemList[3]),
                                    strCreatedBy, itemList[2], itemList[6]);

                                noteItemsString = " Quote Items : Description " + itemList[1].ToString()
                                    + ", Created By " + strCreatedBy + " , SupplierName " + itemList[6] + " ";
                            }
                            //Rest of the Elements
                            if (i != 0)
                            {
                                int Item = quote.CreateQuoteItems(QuoteID, itemList[2], itemList[1],
                                    float.Parse(itemList[6]), Int32.Parse(itemList[5]), float.Parse(itemList[4]), strCreatedBy, itemList[3], itemList[7]);
                                noteItemsString = noteItemsString + " Quote Items : Description "
                                    + itemList[1].ToString() + ", Created By " + strCreatedBy + " , SupplierName " + itemList[6] + " ";

                            }
                        }
                    }
                    ///////End  Refactor this code//////


                    var columnName = "Quotes And Quotes Items All columns";
                    var talbeName = "Quotes And Quotes Items";
                    var ActionType = "Created";
                    int primaryKey = QuoteID;

                    var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

                    var newvalues = creditNoteMainString + noteItemsString;

                    var loggedInUserId = Convert.ToInt32(Session["LoggedUserID"].ToString());
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = connectionstring;

                    new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", newvalues, loggedInUserId, conn, 0,
                  columnName, talbeName, ActionType, primaryKey, COMPANYID);

                    #region WriteCommssions

                    float commission = float.Parse(hdnCommision.Value.ToString());
                    //if (Session["LoggedUserID"] != null)
                    //{
                    //    //SplitCommission(OrderID, Int32.Parse(strCompanyID), Int32.Parse(Session["LoggedUserID"].ToString()), commission);
                    //    //SplitCommission(QuoteID, Int32.Parse(strCompanyID), Int32.Parse(ddlUsers.SelectedValue.ToString()), commission);
                    //}
                    //#endregion

                    //#region AddPro Items
                    //if (!String.IsNullOrEmpty(PROITEMS))
                    //{
                    //    // CreateProItems(QuoteID, PROITEMS);
                    //}
                    //#endregion

                    //#region PurchaseOrder List

                    //foreach (var pair in di)
                    //{
                    //    //purchasedal.InsertPurchaseItem(QuoteID, pair.Key, pair.Value, Session["LoggedUser"].ToString());
                    //}

                    #endregion PurchaseOrder List


                    #region XeroEntry Creation with Status(DRAFT)

                    String strContact = getContactDetailsForInvoice(Int32.Parse(strContactID));
                    String[] arrContact = strContact.Split(':');


                    XeroApi.Model.Invoice INV = null;

                    XeroApi.Model.Invoice delinvoice = null;

                    //Create the Supplier Notes if Exsists
                    //if (delinvoice != null)
                    //{
                    //    INV = delinvoice;
                    //    CreateSupplerNotes(QuoteID, delinvoice.InvoiceNumber.ToString(), di_Notes);
                    //}
                    //else
                    //{
                    CreateSupplerNotes(QuoteID, String.Empty, di_Notes);
                    //  }

                    #endregion

                    #region Send Email If Authorised to

                    if (Session["USERPROFILE"].ToString() == "ADMIN" || Session["LoggedUserID"].ToString() == "10")
                    {
                        DeltoneQuoteEmailSender EmailSender = new DeltoneQuoteEmailSender();
                        var loggedUserEmail = getLoggedInUserEmail(QuoteID);
                        EmailSender.SendMail(String.Empty, QuoteID, "sentemails@deltonesolutions.com.au", loggedUserEmail);
                    }
                    else
                    {
                        //  if (EmailSend.Value == "SEND")
                        // {
                        DeltoneQuoteEmailSender EmailSender = new DeltoneQuoteEmailSender();
                        var loggedUserEmail = getLoggedInUserEmail(QuoteID);
                        EmailSender.SendMail(String.Empty, QuoteID, "sentemails@deltonesolutions.com.au", loggedUserEmail);
                        //  }
                    }

                    #endregion

                    //String deltoneInvoice_Number = OrderID.ToString();
                    String deltoneInvoice_Number = String.Empty;
                    String deltoneInvoice_ID = String.Empty;
                    String Message = String.Empty;

                    //if (delinvoice != null)
                    //{
                    //    //deltoneInvoice_Number = delinvoice.InvoiceNumber.ToString();
                    //    //deltoneInvoice_ID = delinvoice.InvoiceID.ToString();
                    //    //updateOrderStatus(deltoneInvoice_Number, deltoneInvoice_ID, Int32.Parse(QuoteID.ToString()));


                    //}


                    Message = (!NewQuoteNumber.Equals(String.Empty)) ? "<h2>QUOTE  #" + NewQuoteNumber + " SUCCESSFULLY CREATED</h2>" : "COULD NOT WRITE TO XERO.";
                    String NavigateUrl = "AllQuotes.aspx";
                    String PrintURl = "PrintQuote.aspx?Oderid=" + NewQuoteNumber + "&cid=" + strContactID + "&Compid=" + strCompanyID + "&ExistingCustomer=" + ExistingCustomer.Value;
                    string strScript = "<script language='javascript'>$(document).ready(function (){SubmitDialog('" + Message + "','" + NavigateUrl + "','" + PrintURl + "'); });</script>";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupCP", strScript, false);
                }
                else
                {

                    String Message = "<h2>Error Creating Order</h2>";
                    String NavigateUrl = "AllQuotes.aspx";
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


        }
        //This Function Generate the Invoice for Suppliers and Save it in the Disk



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
            String strSqlOrderStmt = "select * from dbo.Quote where QuoteID=" + orderid;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        String strORDERDATE = Convert.ToDateTime(sdr["QuoteDateTime"]).ToString("yyyy-MM-dd");
                        String DueDate = "None";
                        String Reference = sdr["Reference"].ToString();
                        String delCharges = sdr["CustomerDelCost"].ToString();
                        String strGuid = "None";
                        String PaymentTerms = (!DBNull.Value.Equals(sdr["PaymentTerms"])) ? sdr["PaymentTerms"].ToString() : String.Empty;
                        //Modification done here Get Order Crearte Date
                        //String OrderCreateDate = (!DBNull.Value.Equals(sdr["CreatedDateTime"])) ? sdr["CreatedDateTime"].ToString() : String.Empty;
                        String OrderCreateDate = Convert.ToDateTime(sdr["QuoteDateTime"]).ToString("yyyy-MM-dd");

                        strOrder = strOrder + sdr["SuppDelItems"].ToString() + ":" + sdr["ProItems"].ToString() + ":" + sdr["CusDelCostItems"].ToString() + ":" + sdr["Status"].ToString() + ":" + strORDERDATE + ":" + DueDate + ":" + Reference + ":" + delCharges + ":" + strGuid + ":" + PaymentTerms + ":" + OrderCreateDate;
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

            String strSqlStmt = "select * from dbo.Quote_Item where QuoteID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOrderitems = strOrderitems + sdr["ItemCode"].ToString() + "," + sdr["Description"] + ","
                            + sdr["UnitAmount"] + "," + sdr["COGamount"] + "," + sdr["SupplierCode"] + "," + sdr["Quantity"] + ","
                            + sdr["SupplierName"];
                        strOrderitems = strOrderitems + "|";

                    }
                }

            }
            conn.Close();

            return strOrderitems;

        }


        protected string getOrderItemslistStringbyOrderID(int OrderID)
        {

            String strOrderitems = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            String strSqlStmt = "select * from dbo.Quote_Item where QuoteID=" + OrderID;

            var quoteItemsString = "";
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        var obj = new QuoteItem();

                        if (quoteItemsString == "")
                            quoteItemsString = " Deleted Items : ItemCode " + sdr["ItemCode"].ToString() +
                              " Description " + sdr["Description"].ToString() +
                             " Quantity " + sdr["Quantity"].ToString() +
                              " SupplierName " + sdr["SupplierName"].ToString();
                        else
                            quoteItemsString = quoteItemsString + " ItemCode " + sdr["ItemCode"].ToString() +
                              " Description " + sdr["Description"].ToString() +
                             " Quantity " + sdr["Quantity"].ToString() +
                              " SupplierName " + sdr["SupplierName"].ToString();

                    }
                }

            }
            conn.Close();

            return quoteItemsString;

        }

        public class QuoteItem
        {
            public string ItemCode { get; set; }
            public string Description { get; set; }
            public string Quantity { get; set; }
            public string SupplierName { get; set; }
        }



        //End Edit Oreder Related Functions

        //This Function Fetch The LastCreated OrderID
        protected int LastOrderID(String strCreatedBy)
        {
            int intOrderID = 0;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "SELECT TOP 1 * FROM dbo.Quote WHERE QuoteBy='" + strCreatedBy + "'ORDER BY QuoteDateTime Desc";
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            intOrderID = Int32.Parse(sdr["QuoteID"].ToString());
                        }
                    }
                    else
                    {
                        intOrderID = 0;
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
            //cmd.Parameters.AddWithValue("@ClientNotified", NotifyClient);
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
            String sqlOrderedItemStmt = "insert into dbo.Quote_Item(QuoteID,Description,ItemCode,UnitAmount,Quantity,CreatedDateTime,CreatedBy,COGamount,SupplierCode,SupplierName)values (@QuoteID,@ItemDescription,@ItemCode,@UnitAmout,@qty,CURRENT_TIMESTAMP,@CreatedBy,@COGAmout,@SupplierCode,@SuppName);";
            SqlCommand cmd = new SqlCommand(sqlOrderedItemStmt, conn);
            cmd.Parameters.AddWithValue("@QuoteID", OrderID);
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
        protected int DeleteOrderItems(int OrderId, out string orderItemsString)
        {

            orderItemsString = getOrderItemslistStringbyOrderID(OrderId);

            int intRowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strDelStmt = "delete dbo.Quote_Item where QuoteID=" + OrderId;
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



        protected String getOrderStatus(String OID)
        {
            String OrderStatus = String.Empty;
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Status FROM dbo.Quote WHERE QuoteID = " + OID;
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

            }

            return OrderStatus;
        }


        //This Function Update Order  given by Values
        protected int UpdateOrder(int OrderId, String strCompanyID, String strContactId, float COGTotal, float COGSubTotal,
            float ProfitTotal, float ProfitSubtotal, float SuppDelCost, float CusDelCost, float ProItemCost, String strCreatedBy,
            String SuppDelItems, String ProItems, String strCusDelItems, String strAlteredBy, DateTime Orderduedate, String strorderedby,
            String OrderNotesStr, String PaymentTerms, String TypeOfCall, DateTime orderDate, int quoteCategory, DateTime? callBackdate, string reasonWhatHappened)
        {
            int intRowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            //Modification here 30/04/2015 add Updation due date
            String strSQLUpdateStmt = @"Update Quote set QuoteDateTime=@OrderedDate,Total=@Total," +
           " SubTotal=@SubTotal,AlteredDateTime=CURRENT_TIMESTAMP,AlteredBy=@AlteredBy,COGTotal=@COGTotal," +
           " COGSubTotal=@COGSubTotal,SupplierDelCost=@SupDelCost,CustomerDelCost=@CusDelCost,ProItemCost=@ProitemCost," +
           " SuppDelItems=@SupDelItems,ProItems=@ProItems,CusDelCostItems=@CusDelCosItems,Notes=@OrderNotes, PaymentTerms=@PaymentTerms, " +
           " TypeOfCall=@TypeOfCall, QuoteCategory=@QuoteCategory,CallBackDate=@CallBackDate,SellStatusReason=@sellStatusReason where QuoteID=@OrderID";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", OrderId);
            cmd.Parameters.AddWithValue("@AlteredBy", strAlteredBy);
            cmd.Parameters.AddWithValue("@Total", ProfitTotal);
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

            cmd.Parameters.AddWithValue("@OrderNotes", OrderNotesStr);

            //Modification to add Payment Terms, Reference and Type of Call
            cmd.Parameters.AddWithValue("@PaymentTerms", PaymentTerms);
            cmd.Parameters.AddWithValue("@TypeOfCall", TypeOfCall);

            //Modified here Add User Defined Date
            cmd.Parameters.AddWithValue("@OrderedDate", orderDate);
            cmd.Parameters.AddWithValue("@sellStatusReason", reasonWhatHappened);


            cmd.Parameters.AddWithValue("@QuoteCategory", quoteCategory);
            if (callBackdate == null)
                cmd.Parameters.AddWithValue("@CallBackDate", DBNull.Value);
            else
                cmd.Parameters.AddWithValue("@CallBackDate", callBackdate);

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
            if (ChangeOrderStatus(ORDERID) > 0)
            {


                String connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                orderdal = new OrderDAL(connString);
                String xeroGuid = orderdal.getOrderGuid(ORDERID);
                //String Order_Reference = ReferenceFld.Text;

                if (!String.IsNullOrEmpty(xeroGuid))
                {
                    XeroApi.Model.Invoice ApprovedInvoice = null;
                    if (ApprovedInvoice != null)
                    {
                        deltoneInvoice_Number = ApprovedInvoice.InvoiceNumber.ToString();
                        deltoneInvoice_ID = ApprovedInvoice.InvoiceID.ToString();
                    }
                }
                else
                {

                }

                Dictionary<String, String> di_Notes = tempsuppdal.getNotesObject(ORDERID);
                Dictionary<String, String> di_suppitems = purchasedal.getPurchaseItemObject(ORDERID);




                //Send Confirmation Email to the User 






                String Message = "<h2>ORDER: " + deltoneInvoice_Number + " HAS BEEN AUTHORIZED</h2>";
                String NavigateUrl = "AllQuotes.aspx";
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


        //This Method Change the Order Status Given by OrderID
        protected int ChangeOrderStatus(int OrderId)
        {

            int RowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateOrderStatus = "Update dbo.Orders SET Status='APPROVED' WHERE OrderID=@OrderID";
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


        //Company OwnerShip details dropdown List Population
        protected void PopulateDropDownList(int CompanyID)
        {

            String strLoggedUsers = String.Empty;
            DataTable subjects = new DataTable();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                SqlDataAdapter adapter = new SqlDataAdapter("select FirstName +' ' + LastName as FullName, LoginID from dbo.Logins WHERE  Active = 'Y' ", conn);
                adapter.Fill(subjects);
                ddlUsers.DataSource = subjects;
                ddlUsers.DataTextField = "FullName";
                ddlUsers.DataValueField = "LoginID";
                ddlUsers.DataBind();
            }

            String strAccountOwnerShip = String.Empty;

            //Get the Current Account OwnerShip
            if (!String.IsNullOrEmpty(Request.QueryString["FLAG"]))
            {
                strAccountOwnerShip = "House Account";
            }
            else
            {
                strAccountOwnerShip = CompanyOwnerShip(CompanyID);
            }

            //String[] arr = strAccountOwnerShip.Split(':');
            //String firstname = arr[0];
            //String ownershipid = arr[1];

            if (String.IsNullOrEmpty(Request.QueryString["FLAG"]))
            {
                //ddlUsers.Items.FindByValue(strAccountOwnerShip).Selected = true;
            }




            if (Session["LoggedUserID"] != null)
            {
                ddlUsers.Items.FindByValue(Session["LoggedUserID"].ToString()).Selected = true;
                //ddlUsers.Items.FindByText(firstname).Selected = true;
            }

        }


        //This method Handles the Split Commision 
        protected void SplitCommission(int OrderID, int CompanyID, int LoggedID, float commissionvalue)
        {

            String strOutPut = String.Empty;

            OrderDAL oDAL = new OrderDAL(connString);
            String CreatedDateTime = oDAL.OrderCreatedDate(OrderID);
            String OrderStatus = String.Empty;

            OrderStatus = getOrderStatus(OrderID.ToString());


            int CompnyOwnerShip = Int32.Parse(CompanyOwnerShip(CompanyID));
            if (LoggedID == CompnyOwnerShip)
            {

                orderdal.WriteCommision(OrderID, LoggedID, commissionvalue, "NO", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }
            else
            {
                float newCommisionValue = (float)Math.Round((commissionvalue * 0.5), 2);
                orderdal.WriteCommision(OrderID, LoggedID, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
                orderdal.WriteCommision(OrderID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }

        }


        //This Method Update Commision Entries in DB
        protected void UpdateCommissions(int OrderID, int CompanyID, int LoggedID, float commissionvalue, String OrderStatus)
        {
            //Remove the Old Entry First where type='ORDER'
            //String output = orderdal.RemoveCommisionEntry(OrderID);


            OrderDAL oDAL = new OrderDAL(connString);
            String CreatedDateTime = oDAL.OrderCreatedDate(OrderID);


            int CompnyOwnerShip = Int32.Parse(CompanyOwnerShip(CompanyID));
            if (LoggedID == CompnyOwnerShip)
            {

                //orderdal.WriteCommision(OrderID, LoggedID, commissionvalue, "NO", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
            }
            else
            {
                float newCommisionValue = (float)Math.Round((commissionvalue * 0.5), 2);
                //orderdal.WriteCommision(OrderID, LoggedID, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
                //orderdal.WriteCommision(OrderID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString(), Convert.ToDateTime(CreatedDateTime), OrderStatus);
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

                }

            }

            return strOwnershipAdmin;
        }
        //End Selecting the Company owner

        //End Company OwnerShip details dropdown list Population

        protected void getExisitingCustomerDetails(String CustID)
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

        protected void getNewCustomerDetails(String CustID)
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
                    cmd.CommandText = "SELECT CT.*, CP.CompanyName FROM dbo.Quote_Contacts CT, dbo.Quote_Companies CP WHERE CT.CompanyID = CP.CompanyID AND ContactID = " + CustID;
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
            RemoveSupplierNotes(OrderID);
            //Remove the Previous Entry

            CreateSupplerNotes(OrderID, xeroInvoiceNumber, di);

        }
        //End Function Update Supplier Notes

        //This Function Remove Previous Note Entries
        protected int RemoveSupplierNotes(int OrderID)
        {
            int intRowsEffected = -1;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStatement = "Delete from dbo.SupplierNotes where OrderID=@OrderId";
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
            String strSqlOrderStmt = "select * from dbo.SupplierNotes where Type='SalesOrder' and  OrderID=" + OrderID;
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
            String strSqlOrderStmt = "SELECT Reference FROM dbo.Orders where OrderID=" + OrderID;
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


        //This Function Add Supplier Notes 
        protected int AddSupplierNotes(int intOrderID, String strSupplierName, String strNotes, String xeroInvoiceNumber)
        {
            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStatement = "INSERT INTO SupplierNotes values(@OrderID,@SupplierName,@Notes,@xeroInvNumber,'SalesOrder')";
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
                //intRowsEffected = cmd.ExecuteNonQuery();
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
            //RemoveProItems(intOrderId);

            //CreateProItems(intOrderId, strProItems);

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


        //over mutliple days
        [System.Web.Services.WebMethod]
        public static int addEvent(DeltoneCRM.DataHandlers.DisplayEventHandler.ImproperCalendarEvent improperEvent)
        {
            string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            HttpContext.Current.Session["calldate"] = improperEvent.start;
            var startDate = Convert.ToDateTime(improperEvent.start);

            var endDate = startDate.AddHours(1);

            var isAllDay = false;
            DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent cevent = new DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent()
            {
                title = improperEvent.title,
                description = improperEvent.description,
                start = startDate,
                end = endDate,
                allDay = isAllDay,
                color = improperEvent.color

            };
            var comId = 0;

            var url = "";

            if (HttpContext.Current.Session["UrlQuote"] != null)
                url = (HttpContext.Current.Session["UrlQuote"].ToString());

            cevent.url = url;

            if (HttpContext.Current.Session["companyId"] != null)
                comId = Convert.ToInt32(HttpContext.Current.Session["companyId"].ToString());

            var userId = 0;
            if (HttpContext.Current.Session["LoggedUserID"] != null)
                userId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            int key = new CalendarEventDAL(cs).addEvent(cevent, comId, userId);

            //List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];

            //if (idList != null)
            //{
            //    idList.Add(key);
            //}


            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionstring;

            var columnName = "CalendarEvent all columns Quotes";
            var talbeName = "CalendarEvent";
            var ActionType = "Event Created";
            int primaryKey = key;
            var companyName = new DeltoneCRM_DAL.CompanyDAL(connectionstring).getCompanyNameByID(comId.ToString());
            var lastString = "Event Scheduled for company " + companyName + ": " + cevent.title;

            new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", lastString, userId, conn, 0,
   columnName, talbeName, ActionType, primaryKey, comId);


            return key; //return the primary key of the added cevent object




            return -1; //return a negative number just to signify nothing has been added
        }

        [System.Web.Services.WebMethod]
        public static ContactUpdate ReadContact(int contactId, string dbtype)
        {

            if (dbtype == "QuoteDB" || dbtype == "")
            {
                return getNewCustomerContactDetails(contactId.ToString());
            }
            else
                return getExisitingCustomerContactDetails(contactId.ToString());
        }

        [System.Web.Services.WebMethod]
        public static string ReadDataCompany(int comId, string dbtype)
        {

            return getNewCompanyNameAndOwnerID(comId, dbtype);
        }

        protected static string getNewCompanyNameAndOwnerID(int CompID, String WhichDB)
        {
            String output = String.Empty;
            string name = "";
            string ownerName = "";
            SqlConnection conn = new SqlConnection();
            var strConn = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;
            conn.ConnectionString = strConn;
            String strSqlOrderStmt = String.Empty;

            if (WhichDB == "QuoteDB")
            {
                strSqlOrderStmt = " select CompanyName,CreatedBy from dbo.Quote_Companies where CompanyID=" + CompID;
            }
            else
            {
                strSqlOrderStmt = " select CompanyName,CreatedBy from dbo.Companies where CompanyID=" + CompID;
                ownerName = new CompanyDAL(strConn).getCompanyAccountOwner(CompID);
            }


            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        name = sdr["CompanyName"].ToString();
                        if (WhichDB == "QuoteDB")
                        {
                            ownerName = sdr["CreatedBy"].ToString();
                        }

                    }
                }
            }
            conn.Close();
            output = name + "," + ownerName;
            return output;
        }
        protected static ContactUpdate getNewCustomerContactDetails(String CustID)
        {
            var obj = new ContactUpdate();

            String strCompanyName = "";
            String CustomerName = "";

            String CustomerShipAddressLine1 = "";
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
                    cmd.CommandText = "SELECT CT.*, CP.CompanyName FROM dbo.Quote_Contacts CT, dbo.Quote_Companies CP WHERE CT.CompanyID = CP.CompanyID AND ContactID = " + CustID;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            strCompanyName = sdr["CompanyName"].ToString();
                            CustomerName = sdr["FirstName"].ToString();
                            var lastName = sdr["LastName"].ToString();

                            CustomerShipAddressLine1 = sdr["STREET_AddressLine1"].ToString();
                            CustomerShipCity = sdr["STREET_City"].ToString();
                            CustomerShipPostcode = sdr["STREET_PostalCode"].ToString();
                            CustomerShipState = sdr["STREET_Region"].ToString();

                            CustomerContactNumber = sdr["DEFAULT_AreaCode"].ToString() + ' ' + sdr["DEFAULT_Number"].ToString();
                            CustomerEmail = sdr["Email"].ToString();
                            obj.CompanyName = strCompanyName;
                            obj.ContactId = Convert.ToInt32(CustID);
                            obj.Address1 = CustomerShipAddressLine1;
                            obj.City = CustomerShipCity;
                            obj.Email = CustomerEmail;
                            obj.FirstName = CustomerName;
                            obj.LastName = lastName;
                            obj.Phone = CustomerContactNumber;
                            obj.PostCode = CustomerShipPostcode;
                            obj.State = CustomerShipState;

                        }
                    }
                }
            }

            return obj;

        }

        protected static ContactUpdate getExisitingCustomerContactDetails(String CustID)
        {
            var obj = new ContactUpdate();

            String strCompanyName = "";
            String CustomerName = "";

            String CustomerShipAddressLine1 = "";
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
                            CustomerName = sdr["FirstName"].ToString();
                            var lastName = sdr["LastName"].ToString();


                            CustomerShipAddressLine1 = sdr["STREET_AddressLine1"].ToString();
                            CustomerShipCity = sdr["STREET_City"].ToString();
                            CustomerShipPostcode = sdr["STREET_PostalCode"].ToString();
                            CustomerShipState = sdr["STREET_Region"].ToString();

                            CustomerContactNumber = sdr["DEFAULT_AreaCode"].ToString() + ' ' + sdr["DEFAULT_Number"].ToString();
                            CustomerEmail = sdr["Email"].ToString();
                            obj.CompanyName = strCompanyName;
                            obj.ContactId = Convert.ToInt32(CustID);
                            obj.Address1 = CustomerShipAddressLine1;
                            obj.City = CustomerShipCity;
                            obj.Email = CustomerEmail;
                            obj.FirstName = CustomerName;
                            obj.LastName = lastName;
                            obj.Phone = CustomerContactNumber;
                            obj.PostCode = CustomerShipPostcode;
                            obj.State = CustomerShipState;

                        }
                    }
                }
            }

            return obj;

        }


        [System.Web.Services.WebMethod]
        public static void UpdateContactDetails(ContactUpdate obj)
        {

            if (obj.dbType == "QuoteDB" || obj.dbType == "")
            {
                UpdateContactQuoteCompany(obj);
            }
            else
                UpdateContactExistingCompany(obj);

            //{
            //    return getNewCustomerContactDetails(contactId.ToString());
            //}
            //else
            //    return getExisitingCustomerContactDetails(contactId.ToString());
        }

        protected static void UpdateContactQuoteCompany(ContactUpdate obj)
        {
            UpdateQuote_Contacts(obj);
            var comId = Convert.ToInt32(getCompanyIdQuoteByContact(obj.ContactId.ToString()));
            UpContactQuote_Companies(comId, obj.CompanyName);
        }

        private static void UpContactQuote_Companies(int companyID, string comName)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strSqlContactStmt = @"UPDATE Quote_Companies SET CompanyName=@FirstName,AmendedBy=@AlteredBy ,
                                              AlteredDateTime=CURRENT_TIMESTAMP WHERE CompanyID=@CompanyID";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = comName;
                    cmd.Parameters.Add("@AlteredBy", SqlDbType.NVarChar).Value = HttpContext.Current.Session["LoggedUser"].ToString();
                    cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyID;

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        private static void UpContactExisting_Companies(int companyID, string comName)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strSqlContactStmt = @"UPDATE Companies SET CompanyName=@FirstName, AmendedBy=@AlteredBy ,
                                              AlteredDateTime=CURRENT_TIMESTAMP WHERE CompanyID=@CompanyID";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = comName;
                    cmd.Parameters.Add("@AlteredBy", SqlDbType.NVarChar).Value = HttpContext.Current.Session["LoggedUser"].ToString();
                    cmd.Parameters.Add("@CompanyID", SqlDbType.Int).Value = companyID;

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }


        public static string getCompanyIdQuoteByContact(String CID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

            String strSqlContactStmt = "SELECT CompanyID FROM dbo.Quote_Contacts WHERE ContactID = " + CID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = sdr["CompanyID"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;
        }


        public static string getCompanyQuoteIdQuoteByContact(String CID)
        {
            String strContact = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

            String strSqlContactStmt = "SELECT CompanyID FROM dbo.Contacts WHERE ContactID = " + CID;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = strSqlContactStmt;
                cmd.Connection = conn;
                conn.Open();

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strContact = sdr["CompanyID"].ToString();
                    }
                }
            }
            conn.Close();
            return strContact;
        }


        private static void UpdateQuote_Contacts(ContactUpdate obj)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strSqlContactStmt = @"UPDATE Quote_Contacts SET FirstName=@FirstName, LastName=@LastName , AlteredBy=@AlteredBy ,
                                              STREET_AddressLine1=@STREET_AddressLine1, STREET_City=@STREET_City ,DEFAULT_Number=@DEFAULT_Number , Email=@Email ,
                                             STREET_PostalCode=@STREET_PostalCode, STREET_Region=@STREET_Region,AlteredDateTime=CURRENT_TIMESTAMP WHERE ContactID=@ContactID";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = obj.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = obj.LastName;
                    cmd.Parameters.Add("@AlteredBy", SqlDbType.NVarChar).Value = HttpContext.Current.Session["LoggedUser"].ToString();
                    cmd.Parameters.Add("@STREET_AddressLine1", SqlDbType.NVarChar).Value = obj.Address1;
                    cmd.Parameters.Add("@STREET_City", SqlDbType.NVarChar).Value = obj.City;
                    cmd.Parameters.Add("@STREET_PostalCode", SqlDbType.NVarChar).Value = obj.PostCode;
                    cmd.Parameters.Add("@STREET_Region", SqlDbType.NVarChar).Value = obj.State;
                    cmd.Parameters.Add("@DEFAULT_Number", SqlDbType.NVarChar).Value = obj.Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = obj.Email;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = obj.ContactId;

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        protected static void UpdateContactExistingCompany(ContactUpdate obj)
        {
            UpdateExistingCom(obj);
            var comId = Convert.ToInt32(getCompanyQuoteIdQuoteByContact(obj.ContactId.ToString()));
            UpContactExisting_Companies(comId, obj.CompanyName);
        }

        private static void UpdateExistingCom(ContactUpdate obj)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                using (SqlCommand cmd = new SqlCommand())
                {
                    var strSqlContactStmt = @"UPDATE Contacts SET FirstName=@FirstName, LastName=@LastName , AlteredBy=@AlteredBy ,
                                              STREET_AddressLine1=@STREET_AddressLine1, STREET_City=@STREET_City ,DEFAULT_Number=@DEFAULT_Number , Email=@Email ,
                                             STREET_PostalCode=@STREET_PostalCode, STREET_Region=@STREET_Region, AlteredDateTime=CURRENT_TIMESTAMP WHERE ContactID=@ContactID";

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = obj.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = obj.LastName;
                    cmd.Parameters.Add("@AlteredBy", SqlDbType.NVarChar).Value = HttpContext.Current.Session["LoggedUser"].ToString();
                    cmd.Parameters.Add("@STREET_AddressLine1", SqlDbType.NVarChar).Value = obj.Address1;
                    cmd.Parameters.Add("@STREET_City", SqlDbType.NVarChar).Value = obj.City;
                    cmd.Parameters.Add("@STREET_PostalCode", SqlDbType.NVarChar).Value = obj.PostCode;
                    cmd.Parameters.Add("@DEFAULT_Number", SqlDbType.NVarChar).Value = obj.Phone;
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = obj.Email;
                    cmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = obj.ContactId;
                    cmd.Parameters.Add("@STREET_Region", SqlDbType.NVarChar).Value = obj.State;
                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }

        public class ContactUpdate
        {
            public int ContactId { get; set; }
            public string dbType { get; set; }
            public string CompanyName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Address1 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostCode { get; set; }
        }

        protected void btnupload_Click(object sender, EventArgs e)
        {
            Response.Redirect("dashboard1.aspx");
        }
        protected void btnaccountDash_Click(object sender, EventArgs e)
        {
            //var comId = Request.QueryString["Compid"];
            Response.Redirect("CalendarFull.aspx");
        }


        private void SetEventDescription()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["CompID"]))//////Quote Updation Only in DataBase
            {
                var comId = Convert.ToInt32(Request.QueryString["CompID"].ToString());
                allocatequoteNotes.Visible = true;
                var currentUrl = Request.Url.ToString();
                var fullquery = Request.QueryString;
                var urlForm = "http://delcrm/quote.aspx?" + fullquery;

                var listEvents = new CalendarEventDAL(connString).getEventsByComID(comId);

                var filterEvent = (from eve in listEvents where eve.url == urlForm select eve).ToList();
                if (filterEvent.Count() > 0)
                {
                    OrderNotes.Text = OrderNotes.Text + filterEvent[0].description;
                }

                SetQuoteMessage();
            }
        }


        private void SetQuoteMessage()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["OderID"]))//////Quote Updation Only in DataBase
            {
                var comId = Convert.ToInt32(Request.QueryString["OderID"].ToString());


                String Notes = String.Empty;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString; ;

                String strSqlContactStmt = "SELECT Notes FROM dbo.QuoteAllocate WHERE QuoteId = " + comId;
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = strSqlContactStmt;
                    cmd.Connection = conn;
                    conn.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                Notes = sdr["Notes"].ToString();
                            }
                        }
                    }
                }
                conn.Close();

                TextBoxNoteAllocatedQuote.Text = Notes;
            }

        }

        private static void UpdateQuoteNotes(string quoteId, string message)
        {
            String RowsEffected = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLUpdateStmt = "UPDATE dbo.QuoteAllocate SET Notes=@message WHERE QuoteId=@quoteId";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@quoteId", quoteId);
            cmd.Parameters.AddWithValue("@message", message);
            try
            {
                conn.Open();
                RowsEffected = cmd.ExecuteNonQuery().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                RowsEffected = ex.Message.ToString();
            }
            conn.Close();

        }

        private static string getQuoteAllocatedNotes(int quoteId)
        {
            var columnName = "Notes";
            var talbeName = "Companies";
            var ActionType = "Updated";
            int primaryKey = quoteId;
            var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            var companyNote = new DeltoneCRM_DAL.CompanyDAL(connectionstring).getQuoteAllocatedCompanyNotes(quoteId.ToString());


            var loggedInUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = connectionstring;

            //   new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog(companyNote, newdata, loggedInUserId, conn, 0,
            //   columnName, talbeName, ActionType, primaryKey, companyID);

            return companyNote;
        }

        public static void AddAllocatedNotes(string quoteId, string notes)
        {
            int RowEffected = -1;
            var prevoiusNote = getQuoteAllocatedNotes(Convert.ToInt32(quoteId));
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strUpdateCompNotes = "UPDATE  dbo.QuoteAllocate SET Notes = @CompNotes WHERE QuoteID = " + quoteId;
            SqlCommand cmd = new SqlCommand(strUpdateCompNotes, conn);
            var comNotes = HttpContext.Current.Session["LoggedUser"] + "--<b><font  color=\"red\">" + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "</font></b>--"
                + notes;
            comNotes = comNotes + "<br/><br/>" + prevoiusNote;
            cmd.Parameters.AddWithValue("@CompNotes", comNotes);

            try
            {
                conn.Open();
                RowEffected = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                if (conn != null) { conn.Close(); }
                //Response.Write(ex.Message.ToString());
            }

            // return RowEffected;
        }


        [System.Web.Services.WebMethod]
        public static int addEventTest(DeltoneCRM.DataHandlers.DisplayEventHandler.ImproperCalendarEvent improperEvent,
            bool donotCreateEvent)
        {


            //List<int> idList = (List<int>)System.Web.HttpContext.Current.Session["idList"];

            //if (idList != null)
            //{
            //    idList.Add(key);


            var quoteId = 0;
            int key = 0;
            if (!string.IsNullOrEmpty(improperEvent.QuoteId))
            {
                if (Convert.ToInt32(improperEvent.QuoteId) > 0)
                {
                    quoteId = Convert.ToInt32(improperEvent.QuoteId);
                    string cs = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                    HttpContext.Current.Session["calldate"] = improperEvent.start;
                    var startDate = Convert.ToDateTime(improperEvent.start);

                    var endDate = startDate.AddHours(1);

                    var isAllDay = false;
                    DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent cevent = new DeltoneCRM_DAL.CalendarEventDAL.CalendarEvent()
                    {
                        title = improperEvent.title,
                        description = improperEvent.description,
                        start = startDate,
                        end = endDate,
                        allDay = isAllDay,
                        color = improperEvent.color

                    };
                    var comId = 0;

                    var url = "";


                    if (donotCreateEvent == false)
                    {
                        if (HttpContext.Current.Session["UrlQuote"] != null)
                            url = (HttpContext.Current.Session["UrlQuote"].ToString());

                        cevent.url = url;

                        if (HttpContext.Current.Session["companyId"] != null)
                            comId = Convert.ToInt32(HttpContext.Current.Session["companyId"].ToString());

                        new CalendarEventDAL(cs).deleteEventQuote(comId);

                        var userId = 0;
                        if (HttpContext.Current.Session["LoggedUserID"] != null)
                            userId = Convert.ToInt32(HttpContext.Current.Session["LoggedUserID"].ToString());
                        key = new CalendarEventDAL(cs).addQuoteEvent(cevent, comId, userId);
                        var connectionstring = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
                        SqlConnection conn = new SqlConnection();
                        conn.ConnectionString = connectionstring;

                        var columnName = "CalendarEvent all columns Quotes";
                        var talbeName = "CalendarEvent";
                        var ActionType = "Event Created";
                        int primaryKey = key;
                        var companyName = new DeltoneCRM_DAL.CompanyDAL(connectionstring).getCompanyNameByID(comId.ToString());
                        var lastString = "Event Scheduled for company " + companyName + ": " + cevent.title;

                        new DeltoneCRM_DAL.CompanyDAL(connectionstring).CreateActionONAuditLog("", lastString, userId, conn, 0,
               columnName, talbeName, ActionType, primaryKey, comId);
                    }

                    AddAllocatedNotes(quoteId.ToString(), cevent.description);
                }
                else
                {
                    HttpContext.Current.Session["calendarObj"] = improperEvent;
                    return -2;
                }

            }
            else
            {


                HttpContext.Current.Session["calendarObj"] = improperEvent;
                return -2;


            }


            return key; //return the primary key of the added cevent object




            return -1; //return a negative number just to signify nothing has been added
        }

        [System.Web.Services.WebMethod]
        public static void UpdateQuoteNotesCa(string quoteId, string message)
        {

            UpdateQuoteNotes(quoteId, message);
        }
    }
}