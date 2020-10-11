using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Models.Voy
{
   

    public class VBInvoice
    {
        public int VBInvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime OnDate { get; set; }
        public int StoreId { get; set; }

        public string BillType { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillGrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }

        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public bool Tailoring { get; set; }

        public ICollection< VBPaymentDetail> VBPaymentDetails { get; set; }
        public ICollection<VBLineItem> VBLineItems { get; set; }

    }

    public class VBPaymentDetail
    {
        public int VBPaymentDetailId { get; set; }

        public string Mode { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }

        public int VBInvoiceid { get; set; }
        public virtual VBInvoice Invoice { get; set; }
    }

    public class VBLineItem
    {
        public int VBLineItemId { get; set; }
        public int SerialNo { get; set; }
        public string LineItemType { get; set; }
        public string ItemCode { get; set; }
        public double Qty { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal LineTotalAmount { get; set; }

        public int VBInvoiceid { get; set; }
        public  virtual VBInvoice Invoice { get; set; }



    }
   
}
