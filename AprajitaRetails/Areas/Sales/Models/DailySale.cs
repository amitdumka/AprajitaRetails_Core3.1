using AprajitaRetails.Areas.Voyager.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    //Version 3.0 
    // Added Feature
    //Income

    public class DailySale
    {
        public int DailySaleId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }
        [Display(Name = "Invoice No")]
        public string InvNo { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Display(Name = "Payment Mode")]
        public PayMode PayMode { get; set; }
        [Display(Name = "Cash Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CashAmount { get; set; }
        [ForeignKey("Salesman")]
        public int SalesmanId { get; set; }
        public virtual Salesman Salesman { get; set; }
        [Display(Name = "Due")]
        public bool IsDue { get; set; }
        [Display(Name = "Manual Bill")]
        public bool IsManualBill { get; set; }
        [Display(Name = "Tailoring Bill")]
        public bool IsTailoringBill { get; set; }
        [Display(Name = "Sale Return")]
        public bool IsSaleReturn { get; set; }
        public string Remarks { get; set; }
        [DefaultValue(false)]
        public bool IsMatchedWithVOy { get; set; }

        public int? EDCTranscationId { get; set; }
        public virtual EDCTranscation EDCTranscation { get; set; }

        public int? MixAndCouponPaymentId { get; set; }
        public virtual MixAndCouponPayment MixAndCouponPayment { get; set; }

        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }
        public string UserName { get; set; }

        public virtual CouponPayment CouponPayment { get; set; }
        public virtual PointRedeemed PointRedeemed { get; set; }
    }

    public class CouponPayment
    {
        public int CouponPaymentId { get; set; }
        public string CouponNumber { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }

        public int DailySaleId { get; set; }
        public virtual DailySale DailySale { get; set; }
        public string InvoiceNumber { get; set; }

        public string Remark { get; set; }

    }
    public class PointRedeemed
    {
        public int PointRedeemedId { get; set; }
        public string CustomerMobileNumber { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }

        public int DailySaleId { get; set; }
        public virtual DailySale DailySale { get; set; }
        public string InvoiceNumber { get; set; }

        public string Remark { get; set; }
    }

    public class EDC
    {
        public int EDCId { get; set; }
        public int TID { get; set; }
        public string EDCName { get; set; }
        public int AccountNumberId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public bool IsWorking { get; set; }
        public string MID { get; set; }
        public string Remark { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

    }
    public class EDCTranscation
    {
        public int EDCTranscationId { get; set; }
        public int EDCId { get; set; }
        public virtual EDC CardMachine { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        public string CardEndingNumber { get; set; }
        public CardMode CardTypes { get; set; }
        public string InvoiceNumber { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }

    }


    public class MixAndCouponPayment
    {
        public int MixAndCouponPaymentId { get; set; }

        public string InvoiceNumber { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OnDate { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public MixAndCouponPayment Mode { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }


    }

}