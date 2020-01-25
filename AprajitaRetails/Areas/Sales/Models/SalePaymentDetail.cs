using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Sales.Models
{
    public class SalePaymentDetail
    {
        [ForeignKey("SaleInvoice")]
        public int SalePaymentDetailId { get; set; }

        //public int SaleInvoiceId { get; set; }

        public SalePayMode PayMode { get; set; }

        [DefaultValue(0)]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashAmount { get; set; }

        [DefaultValue(0)]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CardAmount { get; set; }

        [DefaultValue(0)]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal MixAmount { get; set; }

        public virtual CardPaymentDetail CardDetails { get; set; }

        public virtual SaleInvoice SaleInvoice { get; set; }
       // public virtual ManualInvoice ManaulInvoice { get; set; }

    }

    public class ManualSalePaymentDetail
    {
        [ForeignKey ("ManualInvoice")]
        public int ManualSalePaymentDetailId { get; set; }
        //public int SaleInvoiceId { get; set; }
        public SalePayMode PayMode { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CashAmount { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CardAmount { get; set; }
        [DefaultValue (0)]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal MixAmount { get; set; }
        public virtual ManualCardPaymentDetail CardDetails { get; set; }
        public virtual ManualInvoice ManualInvoice { get; set; }
    }

}
