using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace DeltoneCRM
{
    public partial class CreatePDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //New Fonts Section by SHEHAN
            //string fontpath = Server.MapPath("./");
            //BaseFont customfont = BaseFont.CreateFont(fontpath + "VAGROUNDED-Thin.ttf", BaseFont.CP1252, BaseFont.EMBEDDED);
            
            //Define Fonts for the Order Items Table
            iTextSharp.text.Font fntTableFontHdr = FontFactory.GetFont("customfont", 8, iTextSharp.text.Font.BOLD, Color.RED);
            iTextSharp.text.Font fntTableFont = FontFactory.GetFont("customfont", 8, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntDefault = FontFactory.GetFont("customfont", 6, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntItalic = FontFactory.GetFont("customfont", 6, iTextSharp.text.Font.BOLDITALIC, Color.BLACK);
            iTextSharp.text.Font fntNormal = FontFactory.GetFont("customfont", 10, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font fntDeltoneCompName = FontFactory.GetFont("customfont", 12, iTextSharp.text.Font.NORMAL, Color.BLACK);

            string strReportName = "Sample Doc.pdf";

            Document document = new Document(PageSize.A4, 71, 71, 71, 71);
            string pdfFilePath = Server.MapPath(".");
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(pdfFilePath + strReportName, FileMode.Create));
            document.Open();

            String strOrderNo = "200142"; //Amend the Xero InvoiceIDm Later 
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
            var font_08_normal = FontFactory.GetFont("SEGOEUI", 8, Font.NORMAL, Color.BLACK);

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
            document.Add(new Paragraph("IMPROVATA", font_10_normal));
            document.Add(new Paragraph("Sajith Heshan Jayaratne", font_09_normal));
            document.Add(new Paragraph("1329 Centre Road", font_09_normal));
            document.Add(new Paragraph("Clayton VIC 3877", font_09_normal));

            document.Add(new Paragraph(" ", font_08_normal));
            document.Add(new Paragraph(" ", font_08_normal));

            // INSERT Company Logo

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("e:\\webprojects\\deltonecrm\\deltonecrm\\Images\\final-logo.png");
            jpg.SetAbsolutePosition(430, 650);
            jpg.ScaleToFit(140f, 120f);
            jpg.SpacingBefore = 10f;
            jpg.SpacingAfter = 1f;
            jpg.Alignment = Element.ALIGN_LEFT;

            document.Add(jpg);

            //Set the Propotions of the Cells 
            //float[] widths = new float[] { 1f, 1f };

            //OuterTable.SetWidths(widths);
            //Set No Border for Table
            //OuterTable.DefaultCell.Border = Rectangle.NO_BORDER;

            //OuterTable.HorizontalAlignment = 0;
            //leave a gap before and after the table
            //OuterTable.SpacingBefore = 20f;
            //OuterTable.SpacingAfter = 30f;

            //Title Cell INVOICE with the title font selected
            //PdfPCell cell = new PdfPCell(new Phrase("PURCHASE ORDER", titleFont));
            //cell.Border = Rectangle.NO_BORDER;
            //cell.Colspan = 2;
            //cell.HorizontalAlignment = 0;
            //OuterTable.AddCell(cell);

            //Contact Organization Cell
            //PdfPCell cellCompanyName = new PdfPCell(new Phrase("Organization Name ZZZ:", fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(cellCompanyName);

            //PdfPCell CompanyName = new PdfPCell(new Phrase("TEST COMPANY ZZZ", fntNormal));
            //CompanyName.Border = Rectangle.NO_BORDER;
            //CompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(CompanyName);

            //Contact Name Cell
            //PdfPCell cellContactName = new PdfPCell(new Phrase("ContactName", fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(cellContactName);

            //PdfPCell ContactName = new PdfPCell(new Phrase("TEST CONTACT", fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(ContactName);

            //Contact Address Cell
            //PdfPCell cellContactAddress = new PdfPCell(new Phrase("Address", fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(cellContactAddress);

            //PdfPCell ContactAddress = new PdfPCell(new Phrase("123 TEST STREET", fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(ContactAddress);


            //Purchase Order Number
            //PdfPCell cellOrderNumber = new PdfPCell(new Phrase("Order Number", fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(cellOrderNumber);


            //PdfPCell OrderNumber = new PdfPCell(new Phrase(strOrderNo, fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(OrderNumber);

            //Purchase Order Date
            //PdfPCell cellOrderDate = new PdfPCell(new Phrase("Date", fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(cellOrderDate);

            //PdfPCell OrderDate = new PdfPCell(new Phrase(orederDate, fntNormal));
            //cellCompanyName.Border = Rectangle.NO_BORDER;
            //cellCompanyName.PaddingTop = 1f;
            //OuterTable.AddCell(OrderDate);

            //document.Add(OuterTable);

            #endregion

            document.Add(new Paragraph("PRODUCT DETAILS", font_10_semibold));
            document.Add(new Paragraph(" ", font_spacer));


            #region ItemsTable

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
            for (int i = 0; i < 5; i++)
            {
                    //SupplierCode cell
                    Cell = new PdfPCell(new Phrase("Test Item 1", font_08_normal));
                    Cell.HorizontalAlignment = 0;
                    //CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                    //CellTwoHdr.BorderWidthBottom = 1f;
                    ItemsTable.AddCell(Cell);

                    //Quantity cell
                    Cell = new PdfPCell(new Phrase("1", font_08_normal));
                    Cell.HorizontalAlignment = 1;
                    //CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                    //CellTwoHdr.BorderWidthBottom = 1f;
                    ItemsTable.AddCell(Cell);

                    //COG cell
                    Cell = new PdfPCell(new Phrase("$24.00", font_08_normal));
                    Cell.HorizontalAlignment = 2;
                    Cell.PaddingBottom = 5f;
                    Cell.PaddingTop = 3f;
                    //CellTwoHdr.Border = Rectangle.BOTTOM_BORDER;
                    //CellTwoHdr.BorderWidthBottom = 1f;
                    ItemsTable.AddCell(Cell);
            }

            //End loop through items to Populate Table

            //Add Supplier Notes for the Purchase Orders 

            document.Add(ItemsTable);


            #region SupplierNote
            document.Add(new Paragraph(" ", font_spacer));
            document.Add(new Paragraph("INSTRUCTIONS TO VENDOR", font_10_semibold));
            document.Add(new Paragraph(" ", font_spacer));
            document.Add(new Paragraph("Customer retention and satisfaction is paramount within our business model. Deltone Solutions has been trusted to provide reliable service and cost-effective products to schools, small-businesses, large corporations and everything in between. We make ordering printer supplies easy and we are here to support you every step of the way.", font_08_normal));
            #endregion

            document.Close();

            #endregion
        }
    }


}