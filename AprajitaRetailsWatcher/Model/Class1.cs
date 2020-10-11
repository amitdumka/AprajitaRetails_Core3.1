using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprajitaRetailsWatcher.Model
{
    public enum SaleInvoiceTypes
    {
        Regular = 1, Manual = 2, SalesReturn = 3, Tailoring = 4, Others = 5
    }

    public enum PayModes { CA, DC, CC, Mix, Wal, CRD, OTH }

    public class VoyagerBillInfo
    {
        public int VoyagerBillId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime BillDate { get; set; }
        public decimal Amount { get; set; }
        public PayModes PayModes { get; set; }
        public DateTime ImportDate { get; set; }
        public bool IsUsed { get; set; }
    }


    public class PathList
    {
        public const string InvoiceXMLPath = "C:\\Capillary";
        public const string TabletSaleXMLPath = "D:\\VoyagerRetail\\TabletSale";
        public const string InvoiceXMLFile = "invoice.xml";
        public const string VoyBillXMLFile = "VoyBill.XML";
        public const string TabletSaleXMLFile = "TabletBill.XML";
        public const string TailoringHubXMLPath = "D:\\VoyagerRetail\\TailoringHub";
        public const string DataBasePath = @"D:\AprajitaRetails\Databases";
    }
    public class VPaymentMode
    {
        public int VPaymentModeID { get; set; }

        public int VoyBillId { get; set; }


        public string PaymentMode { get; set; }


        public string PaymentValue { get; set; }

        public string Notes { get; set; }
        public virtual VoyBill VoyBill { get; set; }
    }



    public class VoyBill
    {

        public int VoyBillId { get; set; }

        public string BillType { get; set; }

        public string BillNumber { get; set; }

        public DateTime? BillTime { get; set; }

        public double BillAmount { get; set; }

        public double BillGrossAmount { get; set; }

        public double BillDiscount { get; set; }

        public string CustomerName { get; set; }

        public string CustomerMobile { get; set; }

        public string StoreID { get; set; }

        public virtual ICollection<VPaymentMode> VPaymentModes { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }

    }

    public class LineItem
    {
        public int LineItemID { get; set; }

        public int VoyBillId { get; set; }

        public string LineType { get; set; }

        public int Serial { get; set; }


        public string ItemCode { get; set; }

        public double Qty { get; set; }

        public double Rate { get; set; }

        public double Value { get; set; }

        public double DiscountValue { get; set; }

        public double Amount { get; set; }


        public string Description { get; set; }
        public virtual VoyBill VoyBill { get; set; }
    }
    class VoyXMLMapper
    {
    }


    public class VoyTable
    {
        public const string T_Bill = "bill";
        public const string T_LineItem = "line_item";
        public const string T_Customer = "customer";
        public const string T_Payments = "payment";
    }

    public class VBEle
    {
        public const string type = "type";
        public const string bill_number = "bill_number";
        public const string billing_time = "billing_time";
        public const string bill_amount = "bill_amount";
        public const string bill_gross_amount = "bill_gross_amount";
        public const string bill_discount = "bill_discount";
        public const string line_items = "line_items";
        public const string line_item = "line_item";
        public const string line_item_type = "line_item_type";
        public const string serial = "serial";
        public const string item_code = "item_code";
        public const string qty = "qty";
        public const string rate = "rate";
        public const string value = "value";
        public const string discount_value = "discount_value";
        public const string amount = "amount";
        public const string description = "description";

        public const string customer = "customer";
        public const string customername = "name";
        public const string mobile = "mobile";

        public const string Payment_Mode = "Payment_Mode";
        public const string Payment_detail = "Payment_detail";
        public const string payment = "payment";
        public const string mode = "mode";
        public const string Payvalue = "value";
        public const string notes = "notes";

        public const string attributes = "attributes";
        public const string attribute = "attribute";
        public const string name = "name";
        public const string values = "value";

        //Bill Type Service
        public const string PayMode_Type_Details = "PayMode_Type_Details";

        public const string PayMode_Value = "PayMode_Value";
        public const string PayMode_Details = "PayMode_Details";
        public const string bill_store_id = "bill_store_id";
    }

}
