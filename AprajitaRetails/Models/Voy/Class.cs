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


namespace AprajitaRetails.Models.JsonData
{



    public class Rootobject
    {
        public Xml xml { get; set; }
        public Root root { get; set; }
    }

    public class Xml
    {
        public string version { get; set; }
    }

    public class Root
    {
        public Bill bill { get; set; }
    }

    public class Bill
    {
        public string type { get; set; }
        public string bill_number { get; set; }
        public string billing_time { get; set; }
        public string WalletPaymentId { get; set; }
        public string WalletPaymentWalletId { get; set; }
        public string bill_client_id { get; set; }
        public string bill_store_id { get; set; }
        public decimal bill_amount { get; set; }
        public decimal bill_gross_amount { get; set; }
        public string balancepoints_add { get; set; }
        public decimal bill_discount { get; set; }
        public Line_Items line_items { get; set; }
        public Customer customer { get; set; }
        public Custom_Fields Custom_fields { get; set; }
        public Payment_Mode Payment_Mode { get; set; }
    }

    public class Line_Items
    {
        public Line_Item [] line_item { get; set; }
    }

    public class Line_Item
    {
        public string line_item_type { get; set; }
        public string serial { get; set; }
        public string item_code { get; set; }
        public double qty { get; set; }
        public decimal rate { get; set; }
        public decimal value { get; set; }
        public decimal discount_value { get; set; }
        public decimal amount { get; set; }
        public string description { get; set; }
    }

    public class Customer
    {
        public string name { get; set; }
        public string mobile { get; set; }
    }

    public class Custom_Fields
    {
        public Field_Details field_details { get; set; }
    }

    public class Field_Details
    {
        public string field_name { get; set; }
        public string tailoring_req { get; set; }
    }

    public class Payment_Mode
    {
        public Payment_Detail Payment_detail { get; set; }
    }

    public class Payment_Detail
    {
        public Payment [] payment { get; set; }
    }

    public class Payment
    {
        public string mode { get; set; }
        public decimal value { get; set; }
        public string notes { get; set; }
        public Attributes attributes { get; set; }
    }

    public class Attributes
    {
        public Attribute attribute { get; set; }
    }

    public class Attribute
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}



