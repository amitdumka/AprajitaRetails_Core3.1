using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Models.Voy
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
}
