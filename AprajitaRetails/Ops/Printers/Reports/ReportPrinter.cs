using AprajitaRetails.Models.ViewModels;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Printers.Reports
{
    public class ReportPrinter
    {
        string tabs = "    ";
        public string PrintCashBook(List<CashBook> cbList)
        {
            string fileName = "cashBook_" + DateTime.Today.ToShortDateString() + ".pdf";
            using PdfWriter pdfWriter = new PdfWriter(fileName);
            using PdfDocument pdfDoc = new PdfDocument(pdfWriter);
            using Document doc = new Document(pdfDoc, PageSize.A4.Rotate());

            Paragraph header = new Paragraph(ReportHeaderDetails.FirstLine).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            header.Add(ReportHeaderDetails.SecondLine);
            header.Add(ReportHeaderDetails.CashBook);

            float[] columnWidths = { 1, 5, 15, 5, 5, 5 };
            Table table = new Table(UnitValue.CreatePercentArray(columnWidths)).SetBorder(new DashedBorder(2));

            PdfFont f = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            Cell cell = new Cell(1, 3)
                    .Add(new Paragraph("Cash Book(s)"))
                    .SetFont(f)
                    .SetFontSize(13)
                    .SetFontColor(DeviceGray.WHITE)
                    .SetBackgroundColor(DeviceGray.BLACK)
                    .SetTextAlignment(TextAlignment.CENTER);

            table.AddHeaderCell(cell);

            for (int i = 0; i < 2; i++)
            {
                Cell[] headerFooter = new Cell[]{
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("#")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Date")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Particulars")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("CashIn")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("CashOut")),
                    new Cell().SetBackgroundColor(new DeviceGray(0.75f)).Add(new Paragraph("Cash Balance"))
            };

                foreach (Cell hfCell in headerFooter)
                {
                    if (i == 0)
                    {
                        table.AddHeaderCell(hfCell);
                    }
                    else
                    {
                        table.AddFooterCell(hfCell);
                    }
                }
            }

            foreach (var item in cbList)

            {
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.EDate.ToShortDateString())));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.Particulars)));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.CashIn + "")));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.CashIn + "")));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.CashOut + "")));
                table.AddCell(new Cell().SetTextAlignment(TextAlignment.CENTER).Add(new Paragraph(item.CashBalance + "")));
            }
            doc.Add(table);

            doc.Close();
            return fileName;


        }
    }

    public class ReportHeaderDetails
    {
        // For other Purpose it should take data from stores table
        public const string FirstLine = "Aprajita Retails";
        public const string SecondLine = "Bhagalpur Road, Dumka";

        public const string CashBook = "Cash Book";

        public const string CashBookTableHeader = "SNo.     Date	            Particulars	                                        CashIn	            CashOut	            CashBalance     ";



    }




}
