using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using PDFtoPrinter;


namespace AprajitaRetails.Ops.Printers
{
    public class InvoicePrinter
    {
        public static void PrintPDFLocal(string filePath)
        {
            PrinterSettings settings = new PrinterSettings();
            string printerName = "Microsoft Print to PDF";
            if (!String.IsNullOrEmpty(settings.PrinterName)) printerName = settings.PrinterName;
            var printer = new PDFtoPrinterPrinter();
            printer.Print(new PrintingOptions(printerName, filePath));
        }

        public static void TestPrint()
        {
           
            string fileName = Path.GetTempPath()+ "testprint.pdf";
            
            using PdfWriter pdfWriter = new PdfWriter(fileName);
            using PdfDocument pdf = new PdfDocument(pdfWriter);
            Document pdfDoc = new Document(pdf);
            //Header
            Paragraph p = new Paragraph("Hello! \n This is Test Print for testing default printer!. \n\n Aprajita Retails Dev. Team.");
            pdfDoc.Add(p);
            pdf.AddNewPage();
            pdfDoc.Close();

            PrintPDFLocal(fileName);
        }

        public static string PrintManaulInvoice(ReceiptHeader header, /*ReceiptFooter footer,*/ ReceiptItemTotal itemTotals, ReceiptDetails details, List<ReceiptItemDetails> itemDetail, bool isRePrint=true)
        {

           // string path = Path.GetTempPath();
            string fileName = Path.GetTempPath() + "MInvoiceNo_" + details.BillNo.Substring(9) + ".pdf";
            using PdfWriter pdfWriter = new PdfWriter(fileName);
            using PdfDocument pdf = new PdfDocument(pdfWriter);
            Document pdfDoc = new Document(pdf);
            //Header
            Paragraph p = new Paragraph(header.StoreName + "\n");
            p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            p.Add(header.StoreAddress + "\n");
            p.Add(header.StoreCity + "\n");
            p.Add(header.StorePhoneNo + "\n");
            p.Add(header.StoreGST + "\n");
            p.Add(PrintInvoiceLine.DotedLine);
            p.Add(PrintInvoiceLine.InvoiceTitle + "\n");
            p.Add(PrintLine.DotedLine);
            pdfDoc.Add(p);
            
            //Details
            Paragraph dp = new Paragraph(ReceiptDetails.Employee + "\n");
            dp.Add(details.BillNo + "\n");
            dp.Add(details.BillDate + "\n");
            dp.Add(details.BillTime + "\n");
            dp.Add(details.CustomerName + "\n");
            dp.Add(PrintLine.DotedLine);
            dp.Add(PrintInvoiceLine.ItemLineHeader1 + "\n");
            dp.Add(PrintInvoiceLine.ItemLineHeader2 + "\n");
            dp.Add(PrintInvoiceLine.ItemLineHeader3 + "\n");
            dp.Add(PrintInvoiceLine.DotedLine + "\n");
            dp.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            pdfDoc.Add(dp);
            Paragraph ip = new Paragraph();
            p.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            double gstPrice = 0.00;
            double basicPrice = 0.00;
            string tab = "    ";

            foreach (ReceiptItemDetails itemDetails in itemDetail)
            {
                if (itemDetails != null)
                {
                    ip.Add(itemDetails.SKUDescription + "\n");
                    ip.Add(itemDetails.HSN + tab + tab + itemDetails.MRP + tab + tab);
                    ip.Add(itemDetails.QTY + tab + tab + itemDetails.Discount + "\n");
                    ip.Add(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + tab + tab);
                    ip.Add(itemDetails.GSTPercentage + "%" + tab + tab + itemDetails.GSTAmount + "\n");
                    gstPrice += Double.Parse(itemDetails.GSTAmount);
                    basicPrice += Double.Parse(itemDetails.BasicPrice);
                }
            }

            ip.Add("\n" + PrintInvoiceLine.DotedLine);
            ip.Add("Total: " + itemTotals.TotalItem + tab + tab + tab + itemTotals.NetAmount + "\n");
            ip.Add("item(s): " + itemTotals.ItemCount + tab + "Net Amount:" + tab + itemTotals.NetAmount + "\n");
            ip.Add(PrintInvoiceLine.DotedLine);
            ip.Add("Tender\n Paid Amount:\t\t Rs. " + itemTotals.CashAmount);
            ip.Add("\n" + PrintInvoiceLine.DotedLine);
            ip.Add("Basic Price:\t\t" + basicPrice.ToString("0.##"));
            ip.Add("\nCGST:\t\t" + gstPrice.ToString("0.##"));
            ip.Add("\nSGST:\t\t" + gstPrice.ToString("0.##") + "\n");
            //ip.Add (PrintLine.DotedLine);
            pdfDoc.Add(ip);

            //Footer
            Paragraph foot = new Paragraph(PrintLine.DotedLine);
            foot.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            foot.Add(PrintInvoiceLine.FooterFirstMessage + "\n");
            foot.Add(PrintInvoiceLine.DotedLine);
            foot.Add(PrintInvoiceLine.FooterThanksMessage + "\n");
            foot.Add(PrintInvoiceLine.FooterLastMessage + "\n");
            foot.Add(PrintInvoiceLine.DotedLine);
            foot.Add("\n");// Just to Check;
            if (isRePrint)
            {
                foot.Add("(Reprinted)\n");
            }
            foot.Add("Printed on: " + DateTime.Now + "\n");

            pdfDoc.Add(foot);

            // pdfDoc.NewPage();
            //Close Documents
            // pdf.AddNewPage();
            pdfDoc.Close();

            //Print to Default Local Added Printer
            PrintPDFLocal(fileName);

            return fileName;

        }
    }
}
