using Microsoft.AspNetCore.Authorization;    using System;

namespace AprajitaRetails.Areas.Sales.Models
{
    //Processed Tables


    internal class SaleReturnInvoice
    {
        public int SaleReturnInvoiceID { get; set; }
        public int CustomerID { get; set; }
        public string SaleInvoiceNo { get; set; }
        public string ReturnInvoiceNo { get; set; }
        public double TotalQty { get; set; }
        public int TotalReturnItem { get; set; }
        public double Amount { get; set; }
        public double TaxAmount { get; set; }
        public double DiscountAmount { get; set; }
        public DateTime OnDate { get; set; }
        public string NewSaleInvoiceNo { get; set; }
        public string Debit_CreditNotesNo { get; set; }
        public int SalesmanId { get; set; }
    }


}
