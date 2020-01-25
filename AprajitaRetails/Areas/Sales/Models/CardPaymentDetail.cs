using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Sales.Models
{
    public class CardPaymentDetail
    {
        [ForeignKey("SalePaymentDetail")]
        public int CardPaymentDetailId { get; set; }
        public int SaleInvoiceId { get; set; } // FK of SalesInvoice
        public int CardType { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public int AuthCode { get; set; }
        public int LastDigit { get; set; }
        public virtual SalePaymentDetail SalePaymentDetail { get; set; }
    }


    public class ManualCardPaymentDetail
    {
        [ForeignKey ("ManualSalePaymentDetail")]
        public int ManualCardPaymentDetailId { get; set; }
        public int ManualSaleInvoiceId { get; set; } // FK of SalesInvoice
        public int CardType { get; set; }
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal Amount { get; set; }
        public int AuthCode { get; set; }
        public int LastDigit { get; set; }
        [ForeignKey ("ManualSalePaymentDetailId")]
        public virtual ManualSalePaymentDetail SalePaymentDetail { get; set; }
    }

}
