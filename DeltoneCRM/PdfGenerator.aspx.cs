using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;

namespace DeltoneCRM
{
    public partial class PdfGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GeneratePDf();
        }

        //Test Method to test PDF generation
        protected void PdfTest()
        {
            
            Document doc = new Document(PageSize.A4, 10, 30, 70, 30);
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/OrderDocs/HelloWorld.pdf"), FileMode.Create));

            doc.Open();

            doc.NewPage();

            Font myfont = FontFactory.GetFont("Arial", 9, Font.BOLD, Color.BLACK);
            Paragraph SampleString = new Paragraph("This is a Sample Invoice of Total $1200", myfont);

            doc.Add(SampleString);

            doc.Close();

            OpenNewWindow("http://" + Request.ServerVariables["http://localhost:65085/"] + "/HelloWorld.pdf");

        }
        //End Test Method

        public void OpenNewWindow(string url)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "newWindow", string.Format("<script>window.open('{0}');</script>", url));
        }


        //This is a Test Method
        protected void TestGeneratePDFTable()
        {
            //Deifne the Header Font and Define Table Cell Font 
            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, Color.BLACK);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, Color.BLACK);

            string strReportName = "DeltoneInvoice" + DateTime.Now.Ticks + ".pdf";
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 5, 10, 20, 20);
            string pdfFilePath = Server.MapPath(".") + "\\Reports\\";
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(pdfFilePath + strReportName, FileMode.Create));
            doc.Open();

            //Define the Table with 5 Columns for Description ,Qty,UnitPrice,GST,Amount 
            PdfPTable myTable = new PdfPTable(5);
            //Set the Percentage to 100% of the Page
            myTable.WidthPercentage = 100;

            //Left Align
            myTable.HorizontalAlignment = 0;
            myTable.SpacingAfter = 10;

            //Setting the Table Header Width 
            float[] sglTblHdWidths = new float[5];
            sglTblHdWidths[0] = 400f;
            sglTblHdWidths[1] = 100f;
            sglTblHdWidths[2] = 100f;
            sglTblHdWidths[3] = 100f;
            sglTblHdWidths[4] = 100f;

            //Set the Column width on the Creation 
            myTable.SetWidths(sglTblHdWidths);

            //Define the Table Header-Description
            PdfPCell CellOneHdr = new PdfPCell(new Phrase("Description", fntTableFontHdr));
            myTable.AddCell(CellOneHdr);

            //Define Table Header-Quanity

            PdfPCell CellTwoHdr = new PdfPCell(new Phrase("Quantity", fntTableFontHdr));
            myTable.AddCell(CellTwoHdr);

            //Define Table Header-Unit Price
            PdfPCell CellTreeHdr = new PdfPCell(new Phrase("Unit Price", fntTableFontHdr));
            myTable.AddCell(CellTreeHdr);

            //Define Table Header-GST
            PdfPCell CellFourHdr = new PdfPCell(new Phrase("GST", fntTableFontHdr));
            myTable.AddCell(CellFourHdr);

            //Define AMount 
            PdfPCell CellFiveHdr = new PdfPCell(new Phrase("GST", fntTableFontHdr));
            myTable.AddCell(CellFiveHdr);

            doc.Add(myTable);
            doc.Close();

        }
        //End Test Method


        //This Method Generate the PDF File
        protected void GeneratePDf()
        {   
            //Define Fonts for the Order Items Table
            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD, Color.BLACK);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL, Color.BLACK);


            string strReportName = "DeltoneInvoice" + DateTime.Now.Ticks + ".pdf";
            //Create the Document object
            Document document = new Document(PageSize.A4, 70, 70, 70, 70);
            string pdfFilePath = Server.MapPath(".") + "\\Reports\\";
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfFilePath + strReportName, FileMode.Create));
            document.Open();

               /*Create the Memory Stream*/
               //MemoryStream PDFData = new MemoryStream();
               //PdfWriter writer = PdfWriter.GetInstance(document, PDFData);

            //Local Variables to Details 
            String strOrderNo = "DeltoneOrder-12345";
            String orederDate = DateTime.Now.ToString("dd MMM yyyy");
            decimal totalAmtStr;
            string accountNo = "0123456789012";
            string accountName = "Deltone Solutions PTY LTD";
            string branch = "Clyaton";
            string bank = "WestPac Group";

            //Create Fonts 
            var titleFont = FontFactory.GetFont("Arial", 12, Font.BOLD,Color.RED);
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
             PdfPCell cellCompanyName = new PdfPCell(new Phrase("Compnay Name:", boldTableFont));
             cellCompanyName.Border = Rectangle.NO_BORDER;
             OuterTable.AddCell(cellCompanyName);

             PdfPCell CompanyName = new PdfPCell(new Phrase("DELTONE Solutions", bodyFont));
             CompanyName.Border = Rectangle.NO_BORDER;
             OuterTable.AddCell(CompanyName);

             //Invoice Date Cell
             PdfPCell cellInvoiceDate = new PdfPCell(new Phrase("Invoice Date:", boldTableFont));
             cellInvoiceDate.Border = Rectangle.NO_BORDER;
             OuterTable.AddCell(cellInvoiceDate);

             PdfPCell InvoiceDate = new PdfPCell(new Phrase(orederDate, bodyFont));
             InvoiceDate.Border = Rectangle.NO_BORDER;
             OuterTable.AddCell(InvoiceDate);
                

            //Invoice Number Cell
             PdfPCell cellInvoiceNumber = new PdfPCell(new Phrase("Invoice Number:", boldTableFont));
             cellInvoiceNumber.Border = Rectangle.NO_BORDER;
             OuterTable.AddCell(cellInvoiceNumber);

             PdfPCell InvoiceNumber = new PdfPCell(new Phrase(strOrderNo, bodyFont));
             InvoiceNumber.Border = Rectangle.NO_BORDER;
             OuterTable.AddCell(InvoiceNumber);

            //Bill TO Cell
             PdfPCell cellBillTo = new PdfPCell(new Phrase("BillTo:", boldTableFont));
             cellBillTo.Border = Rectangle.NO_BORDER;
             OuterTable.AddCell(cellBillTo);

             PdfPCell BillTo = new PdfPCell(new Phrase("Rex Meadia Group\n" + "L-9 Flider Street Tower \n  120-130 Flider Street \n Melbourne 3331", bodyFont));
             BillTo.Border = Rectangle.NO_BORDER;
             OuterTable.AddCell(BillTo);

             document.Add(OuterTable);
            

            #endregion 

            #region Items Table

             PdfPTable ItemsTable = new PdfPTable(5);
             ItemsTable.WidthPercentage = 100;
             ItemsTable.HorizontalAlignment = 0;
             ItemsTable.SpacingAfter = 10;
             float[] sglTblHdWidths = new float[5];
             sglTblHdWidths[0] = 400f;
             sglTblHdWidths[1] = 100f;
             sglTblHdWidths[2] = 100f;
             sglTblHdWidths[3] = 100f;
             sglTblHdWidths[4] = 100f;

             ItemsTable.SetWidths(sglTblHdWidths);

             ItemsTable.SetWidths(sglTblHdWidths);

             //Define the Table Header-Description
             PdfPCell CellOneHdr = new PdfPCell(new Phrase("Description", fntTableFontHdr));
             ItemsTable.AddCell(CellOneHdr);

             //Define Table Header-Quanity

             PdfPCell CellTwoHdr = new PdfPCell(new Phrase("Quantity", fntTableFontHdr));
             ItemsTable.AddCell(CellTwoHdr);

             //Define Table Header-Unit Price
             PdfPCell CellTreeHdr = new PdfPCell(new Phrase("Unit Price", fntTableFontHdr));
             ItemsTable.AddCell(CellTreeHdr);

             //Define Table Header-GST
             PdfPCell CellFourHdr = new PdfPCell(new Phrase("GST", fntTableFontHdr));
             ItemsTable.AddCell(CellFourHdr);

             //Define AMount 
             PdfPCell CellFiveHdr = new PdfPCell(new Phrase("GST", fntTableFontHdr));
             ItemsTable.AddCell(CellFiveHdr);


            //Loop through Items in the Items and Populate Table 


            //End Loop through Items in the Table 

             document.Add(ItemsTable);

            #endregion 


             #region AccountDetails

             Chunk transferBank = new Chunk("Bank Account Details:", boldTableFont);
             transferBank.SetUnderline(0.1f, -2f); //0.1 thick, -2 y-location
             document.Add(transferBank);
             document.Add(Chunk.NEWLINE);

             //Account Details
             PdfPTable bottomTable = new PdfPTable(3);
             bottomTable.HorizontalAlignment = 0;
             bottomTable.TotalWidth = 300f;
             bottomTable.SetWidths(new int[] { 90, 10, 200 });
             bottomTable.LockedWidth = true;
             bottomTable.SpacingBefore = 20;
             bottomTable.DefaultCell.Border = Rectangle.NO_BORDER;
             bottomTable.AddCell(new Phrase("Account No", bodyFont));
             bottomTable.AddCell(":");
             bottomTable.AddCell(new Phrase(accountNo, bodyFont));
             bottomTable.AddCell(new Phrase("Account Name", bodyFont));
             bottomTable.AddCell(":");
             bottomTable.AddCell(new Phrase(accountName, bodyFont));
             bottomTable.AddCell(new Phrase("Branch", bodyFont));
             bottomTable.AddCell(":");
             bottomTable.AddCell(new Phrase(branch, bodyFont));
             bottomTable.AddCell(new Phrase("Bank", bodyFont));
             bottomTable.AddCell(":");
             bottomTable.AddCell(new Phrase(bank, bodyFont));
             document.Add(bottomTable);


             #endregion



             document.Close();
        }


        protected void Test()
        {
 iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.BOLD,Color.BLACK);
 iTextSharp.text.Font fntTableFont = FontFactory.GetFont("Arial", 8, iTextSharp.text.Font.NORMAL,Color.BLACK);
 string strReportName = "myPdf" + DateTime.Now.Ticks + ".pdf";
 Document doc = new Document(iTextSharp.text.PageSize.LETTER, 5, 10, 20, 20);
 string pdfFilePath = Server.MapPath(".") + "\\Reports\\";
 PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(pdfFilePath + strReportName, FileMode.Create));
 doc.Open();
PdfPTable myTable = new PdfPTable(3);
 // Table size is set to 100% of the page
 myTable.WidthPercentage = 100;
 //Left aLign
 myTable.HorizontalAlignment = 0;
 myTable.SpacingAfter = 10;
 float[] sglTblHdWidths = new float[3];
 sglTblHdWidths[0] = 15f;
 sglTblHdWidths[1] = 200f;
 sglTblHdWidths[2] = 385f;
 // Set the column widths on table creation. Unlike HTML cells cannot be sized.
 myTable.SetWidths(sglTblHdWidths);
PdfPCell CellOneHdr = new PdfPCell(new Phrase(" ", fntTableFontHdr));
 myTable.AddCell(CellOneHdr);
 PdfPCell CellTwoHdr = new PdfPCell(new Phrase("cell 2 Hdr", fntTableFontHdr));
 myTable.AddCell(CellTwoHdr);
 PdfPCell CellTreeHdr = new PdfPCell(new Phrase("cell 3 Hdr", fntTableFontHdr));
 myTable.AddCell(CellTreeHdr);
PdfPCell CellOne = new PdfPCell(new Phrase("R1 C1", fntTableFont));
 CellOne.Rotation = -90;
 myTable.AddCell(CellOne);
 PdfPCell CellTwo = new PdfPCell(new Phrase("R1 C2", fntTableFont));
 myTable.AddCell(CellTwo);
 PdfPCell CellTree = new PdfPCell(new Phrase("R1 C3", fntTableFont));
 myTable.AddCell(CellTree);
PdfPCell CellOneR2 = new PdfPCell(new Phrase("R2 C1", fntTableFont));
 CellOneR2.Rotation = -90;
 myTable.AddCell(CellOneR2);
 PdfPCell CellTwoR2 = new PdfPCell(new Phrase("R2 C2", fntTableFont));
 myTable.AddCell(CellTwoR2);
 PdfPCell CellTreeR2 = new PdfPCell(new Phrase("R2 C3", fntTableFont));
 myTable.AddCell(CellTreeR2);
doc.Add(myTable);
 doc.Close();
//ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "myscript", "window.open('Reports strReportName + "','MyPDFDocument','toolbar=1,location=1,status=1,scrollbars=1,menubar=1,resizable=1,left=10,top=10,width=860,height=640');", true);


        }

    }
}

