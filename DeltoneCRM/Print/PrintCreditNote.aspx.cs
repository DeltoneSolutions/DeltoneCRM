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

namespace DeltoneCRM.Print
{
    public partial class PrintCreditNote : System.Web.UI.Page
    {
        String strPendingApproval = "";
        String strLoggedUserProfile = "Admin";
        int ORDERID;
        int CONTACTID;
        int COMPANYID;
        static int CreditNoteID;
        String connString;
        SupplierDAL suppdal;
        OrderDAL orderdal;
        //TempSuppNotes tempsupdal;

        //Modification done here
        static String ORDERSTATUS = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            String CompanyID = String.Empty;
            connString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            suppdal = new SupplierDAL(connString);
            orderdal = new OrderDAL(connString);
            CreditNotesDAL creditdal = new CreditNotesDAL(connString);
            //tempsupdal = new TempSuppNotes(connString);

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
                //Set CompanyID
                COMPANYID = Int32.Parse(CompanyID);
                Session["OpenedCompanyID"] = CompanyID;//Set the Session for CompanyID
            }

            if (!String.IsNullOrEmpty(Request.QueryString["Oderid"]))
            {
                int OrderID = Int32.Parse(Request.QueryString["Oderid"].ToString());
                ORDERID = Int32.Parse(Request.QueryString["Oderid"].ToString());
                hdnORDERID.Value = Request.QueryString["Oderid"].ToString();

                //Set the Hidden Credit Note ID here   and Global Variable
                CreditNoteID = Int32.Parse(Request.QueryString["Oderid"].ToString());
                hdnCreditNoteID.Value = Request.QueryString["Oderid"].ToString();

                //ordercreatedbydiv.InnerText = orderdal.getOrderOwner(OrderID);





                String OrderIDFromCreditID = creditdal.getOrderIDFromCreditID(ORDERID);

                String XeroOrderID = orderdal.getXeroDTSID(Convert.ToInt32(OrderIDFromCreditID));

                suppliernotesdiv.InnerHtml = orderdal.getSupplierNotes(XeroOrderID);

                hdnCommishSplit.Value = creditdal.getCommishSplit(OrderID);
                ACCOUNT_OWNER_ID.Value = creditdal.getAccountOwnerID(OrderID);
                ACCOUNT_OWNER.Value = creditdal.getAccountOwner(OrderID);

                SALESPERSON_ID.Value = creditdal.getSalespersonID(OrderID);
                SALESPERSON_TXT.Value = creditdal.getSalesperson(OrderID);


                String CREIDT_NOTE = creditdal.getCreditNote(CreditNoteID);//Fetch the Credit Note
                String[] credit_arr = CREIDT_NOTE.Split('|');

                String createdDate = Convert.ToDateTime(credit_arr[0].ToString()).ToShortDateString();

                divOrderCreatedDate.InnerHtml = createdDate;
                createdby.InnerHtml = credit_arr[4].ToString();
                creditnotereason.InnerHtml = credit_arr[1].ToString();
                creditStatus.InnerHtml = credit_arr[3].ToString();

                CN_CreditNotes.InnerHtml = credit_arr[2].ToString();

                docstampdiv.InnerHtml = orderdal.getStampType(COMPANYID, CONTACTID, OrderID);
                //AccountOwnerDIV.InnerHtml = "Account Owner: " + getAccountOwner(OrderID);

                //Modified here fetch oreder Date 
                String OrderCreatedDate = orderdal.getOrderCreatedDate(OrderID);
                String strOrderCreatedDate = String.IsNullOrEmpty(OrderCreatedDate) ? String.Empty : Convert.ToDateTime(OrderCreatedDate).ToShortDateString();

                //divOrderCreatedDate.InnerHtml = strOrderCreatedDate;

                //Set the Order Title 
                OrderTitle.InnerHtml = "CREDIT NOTE: " + creditdal.FetchCreditNoteID(OrderID);  //Fetch CreditNoteID
                //CreditNote Title in Red Color
                ordertd.InnerHtml = "ORDER NUMBER :" + XeroOrderID;
                //ordernotesdiv.InnerHtml = orderdal.getOrderNotes(XeroOrderID);
                OrderTitle.Style.Value = "display:block;";
                //Hide the DropDown List
                QorN.Style.Value = "display:none;";

                //String strOrderedItems = getOrderItemsbyOrderID(OrderID);

                String strOrderedItems = creditdal.getCreditNoteItems(CreditNoteID); //Fetch Saved Credit Note Items 

                hdnEditOrderItems.Value = strOrderedItems;

                String strSupplierNotes = FetchSupplierNotes(OrderID);
                String strPromotionalItems = FetchProItems(OrderID);

                String strOrder = getCreditNotebyCreditNoteID(OrderID);//Fetch Credit Note

                if (!String.IsNullOrEmpty(strOrder))
                {
                    hdnEditOrder.Value = strOrder;
                    //Set the Order Status here 
                    String[] arr = strOrder.Split(':');
                    ORDERSTATUS = arr[2].ToString();
                    if (ORDERSTATUS.Equals("QUOTE"))
                    {
                        //Change the Button Text
                        btnOrderSubmit.Text = "CREATE ORDER";
                        //Change the HTML Text
                        OrderTitle.InnerHtml = "QUOTE:" + ORDERID;
                    }

                }

                if (!String.IsNullOrEmpty(strSupplierNotes))
                {
                    //Populate the Hidden value
                    hdnEditSupplietNotes.Value = strSupplierNotes;
                }

                //Modified 5/5/2015 populate promotional Items
                if (!String.IsNullOrEmpty(strPromotionalItems))
                {
                    hdnEditproitpems.Value = strPromotionalItems;
                }
                //Set the Credit Note Button Visible
                btnAddCreditNote.Visible = true;
            }


            btnCancelInvoice.Style.Value = "display:inline";
            strPendingApproval = "PendingApproval";
            btnOrderSubmit.Text = "Approve Order";

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

        //Test Method Added 11/5/2014 
        protected void TestMe_Click(object sender, EventArgs e)
        {
            Response.Write("<h2>Button Clicked</h2>");
        }

        protected void btnAddCreditNote_Click(object sender, EventArgs e)
        {
            //OrderID,ContactID,CompanyID
            Response.Redirect("CreditNote.aspx?OrderID=" + ORDERID + "&ContactID=" + CONTACTID + "&CompanyID=" + COMPANYID);
        }

        //Button Submit,Apporove Order Click Functions
        protected void btnOrderSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(hdnTotal.Value))  //Validate in a Another way
            {
                return;
            }

            String strCompanyID = hdnCompanyID.Value;
            String strContactID = hdnContactID.Value;
            String strTest = hdnProfit.Value;
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
                strCreatedBy = Session["LoggedUser"].ToString();
            }

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

            //Account ownerShip Modificaiton done 30/04/2015

            //Modified 5/5/2015 Add Prompotional Item

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
            //Modification 1/5/2015 Supplier Notes

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
                        String conAddress = sdr["STREET_AddressLine1"].ToString() + Environment.NewLine + sdr["STREET_City"] + Environment.NewLine + sdr["STREET_Region"] + "" + sdr["STREET_PostalCode"] + Environment.NewLine + sdr["STREET_Country"].ToString();
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
        protected void GenerateSupplierDocs(String strInvoiceID, String strContactID, Dictionary<string, string> map, Dictionary<String, String> map_Notes)
        {

            //Retrive the Contact Details
            String strContact = getContactDetailsForInvoice(Int32.Parse(strContactID));
            //Split the Contact
            String[] arrContact = strContact.Split(':');

            foreach (var pair in map)
            {
                var Supplier = pair.Key;
                var ItemList = pair.Value;
                String[] Lines = ItemList.Split('|');
                CreateSupplierPDF(Supplier, strInvoiceID, arrContact[3], arrContact[0], arrContact[1], arrContact[2], Lines, map_Notes);
            }

        }
        //End method Genrate Supplier PDFs Given by Details


        //This Method Create Supplier PDF Given by Details
        protected void CreateSupplierPDF(String strSupplier, String strInvoiceID, String strContactOrganization, String strContactFullName, String strContactAddress, String strContactEmail, String[] Lines, Dictionary<String, String> map_Notes)
        {
            //Define Fonts for the Order Items Table
            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, Color.BLACK);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntDefault = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntItalic = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLDITALIC, Color.BLACK);
            iTextSharp.text.Font fntNormal = FontFactory.GetFont("Arial", 6, iTextSharp.text.Font.BOLD, Color.BLACK);

            string strReportName = strSupplier + "-" + strInvoiceID + "-" + strContactOrganization + ".pdf";

            Document document = new Document(PageSize.A4, 70, 70, 70, 70);
            string pdfFilePath = Server.MapPath(".") + "\\Invoices\\Supplier\\";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfFilePath + strReportName, FileMode.Create));
            document.Open();

            String strOrderNo = strInvoiceID; //Amend the Xero InvoiceIDm Later 
            String orederDate = DateTime.Now.ToString("dd MMM yyyy");

            ///Due Date Calculation
            String strDueDate = DateTime.Now.AddDays(21).ToString("dd MMM yyyy");

            //Create Fonts 
            var titleFont = FontFactory.GetFont("Arial", 12, Font.BOLD, Color.RED);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            Rectangle pageSize = writer.PageSize;

            document.Open();

            #region invoiceDetails

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

            document.Add(OuterTable);

            #endregion


            #region ItemsTable

            PdfPTable ItemsTable = new PdfPTable(3);
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

            document.Add(ItemsTable);


            #region SupplierNote
            document.Add(Chunk.NEWLINE);

            foreach (var item in map_Notes)
            {
                if (item.Key == strSupplier)
                {
                    Chunk SupplierNote = new Chunk("Note:" + item.Value, fntDefault);
                    document.Add(SupplierNote);
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
        /// Fetch Credit Note given by Credit NoteID
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        protected String getCreditNotebyCreditNoteID(int CreditNoteID)
        {

            String strOrder = String.Empty;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;

            String strSqlOrderStmt = "select * from dbo.CreditNotes where CreditNote_ID=" + CreditNoteID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlOrderStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        strOrder = strOrder + sdr["SuppDelItems"].ToString() + ":" + sdr["CusDelCostItems"].ToString() + ":" + sdr["Status"].ToString();
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

            String strSqlStmt = "select * from dbo.Ordered_Items where OrderID=" + OrderID;
            using (SqlCommand cmd = new SqlCommand())
            {

                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        strOrderitems = strOrderitems + sdr["ItemCode"].ToString() + "," + sdr["Description"] + "," + sdr["UnitAmount"] + "," + sdr["COGamount"] + "," + sdr["SupplierCode"] + "," + sdr["Quantity"] + "," + sdr["SupplierName"];
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


        //This Function Insert Order Items given by Values
        protected int CreateOrderItems(int OrderID, String strItemDesc, String strItemCode, float UnitAmout, int quantity, float COGAmount, String strCratedBy, String strSupplierItemCode, String strSuppName)
        {
            int rowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String sqlOrderedItemStmt = "insert into dbo.Ordered_Items(OrderID,Description,ItemCode,UnitAmount,Quantity,CreatedDateTime,CreatedBy,COGamount,SupplierCode,SupplierName)values (@OrderID,@ItemDescription,@ItemCode,@UnitAmout,@qty,CURRENT_TIMESTAMP,@CreatedBy,@COGAmout,@SupplierCode,@SuppName);";
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
        protected int DeleteOrderItems(int OrderId)
        {
            int intRowEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strDelStmt = "delete dbo.Ordered_Items where OrderID=" + OrderId;
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





        //This Method Approve Status of the Order
        protected void btnInvoiceApprove_Click(object sender, EventArgs e)
        {
            //Disablee the Submit Button here Chnage the DataBase Record and Navigate Where is came from 
            if (ChangeOrderStatus(ORDERID) > 0)
            {
                Response.Write("<h3>Order Approved</h3>");
                //Redirect to the DashBoard
                Response.Redirect("Dashboard1.aspx?Order=" + ORDERID + "&Status=Approved");
            }
            else
            {
                Response.Write("Error approving Order");
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



        //This method Handles the Split Commision 
        protected void SplitCommission(int OrderID, int CompanyID, int LoggedID, float commissionvalue)
        {

            String strOutPut = String.Empty;

            int CompnyOwnerShip = Int32.Parse(CompanyOwnerShip(CompanyID));
            if (LoggedID == CompnyOwnerShip)
            {

                //orderdal.WriteCommision(OrderID, LoggedID, commissionvalue, "NO", LoggedID.ToString());
            }
            else
            {
                float newCommisionValue = (float)Math.Round((commissionvalue * 0.5), 2);
                //orderdal.WriteCommision(OrderID, LoggedID, newCommisionValue, "YES", LoggedID.ToString());
                //orderdal.WriteCommision(OrderID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString());
            }

        }


        //This Method Update Commision Entries in DB
        protected void UpdateCommissions(int OrderID, int CompanyID, int LoggedID, float commissionvalue)
        {
            //Remove the Old Entry First
            String output = orderdal.RemoveCommisionEntry(OrderID);

            int CompnyOwnerShip = Int32.Parse(CompanyOwnerShip(CompanyID));
            if (LoggedID == CompnyOwnerShip)
            {

                //orderdal.WriteCommision(OrderID, LoggedID, commissionvalue, "NO", LoggedID.ToString());
            }
            else
            {
                float newCommisionValue = (float)Math.Round((commissionvalue * 0.5), 2);
                //orderdal.WriteCommision(OrderID, LoggedID, newCommisionValue, "YES", LoggedID.ToString());
                //orderdal.WriteCommision(OrderID, CompnyOwnerShip, newCommisionValue, "YES", LoggedID.ToString());
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
            //DeliveryContact.InnerHtml = CustomerName;
            DeliveryCompany.InnerHtml = strCompanyName.ToUpper();
            //DeliveryAddressLine1.InnerHtml = CustomerShipAddressLine1 + ' ' + CustomerShipAddressLine2;
            //DeliveryAddressLine2.InnerHtml = CustomerShipCity.ToUpper() + ' ' + CustomerShipState.ToUpper() + ' ' + CustomerShipPostcode.ToUpper();
            ContactandEmail.InnerHtml = "T: " + CustomerContactNumber + ' ' + "  |  " + ' ' + "E: " + CustomerEmail;

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
            String strSqlOrderStmt = "select * from dbo.SupplierNotes where OrderID=" + OrderID;
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



        //This Function Add Supplier Notes 
        protected int AddSupplierNotes(int intOrderID, String strSupplierName, String strNotes, String xeroInvoiceNumber)
        {
            int rowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLInsertStatement = "INSERT INTO SupplierNotes values(@OrderID,@SupplierName,@Notes,@xeroInvNumber)";
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
                            AddProItems(intOrderID, 0, ProItem[0], float.Parse(ProItem[1].ToString()), Int32.Parse(ProItem[2]), 0);
                        }
                        else
                        {
                            AddProItems(intOrderID, 0, ProItem[0], float.Parse(ProItem[1].ToString()), Int32.Parse(ProItem[2]), float.Parse(ProItem[3].ToString()));
                        }

                    }
                    if (i != 0)
                    {
                        float ShipCost;
                        if (String.IsNullOrEmpty(ProItem[4]))
                        {
                            ShipCost = 0;
                            AddProItems(intOrderID, 0, ProItem[1], float.Parse(ProItem[2].ToString()), Int32.Parse(ProItem[3]), 0);
                        }
                        else
                        {
                            AddProItems(intOrderID, 0, ProItem[1], float.Parse(ProItem[2].ToString()), Int32.Parse(ProItem[3]), float.Parse(ProItem[4].ToString()));
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
                        ProItems = ProItems + sdr["PromoItem"].ToString() + ":" + sdr["ProCost"].ToString() + ":" + sdr["PromoQty"].ToString() + ":" + sdr["ShippingCost"].ToString();
                        ProItems = ProItems + "|";
                    }
                }
                conn.Close();
            }

            return ProItems;
        }
        //End Method Fetch Promotional Items



        //This Method Add Promotional Items to the Table
        protected int AddProItems(int intOrderID, int ProID, String strProItem, float proCost, int proQty, float ShippingCost)
        {
            int intRowsEffected = -1;
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSQLUpdateStmt = "insert into dbo.OrderProItems values(@OrderID,@PromoID,@ProItem,@Qty,@ShipCost,@ProCost);";
            SqlCommand cmd = new SqlCommand(strSQLUpdateStmt, conn);
            cmd.Parameters.AddWithValue("@OrderID", intOrderID);
            cmd.Parameters.AddWithValue("@PromoID", ProID);
            cmd.Parameters.AddWithValue("@ProItem", strProItem);
            cmd.Parameters.AddWithValue("@ProCost", proCost);
            cmd.Parameters.AddWithValue("@Qty", proQty);
            cmd.Parameters.AddWithValue("@ShipCost", ShippingCost);

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

        protected string getAccountOwner(int OrderID)
        {
            String AccountOwner = String.Empty;

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStringDeltoneCRM"].ConnectionString;
            String strSqlStmt = "select FirstName, LastName from dbo.Companies CP, dbo.Logins LG where CP.CompanyID IN (SELECT CompanyID FROM dbo.Orders WHERE OrderID=@OrderID) AND CP.OwnershipAdminID = LG.LoginID";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                cmd.CommandText = strSqlStmt;
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        AccountOwner = sdr["FirstName"].ToString() + " " + sdr["LastName"].ToString();
                    }
                }
                conn.Close();
            }

            return AccountOwner;
        }


    }
}