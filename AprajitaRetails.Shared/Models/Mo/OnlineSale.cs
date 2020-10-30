using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Models
{
    public class OnlineSale
    {
        public int OnlineSaleId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Sale Date")]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Invoice No")]
        public string InvNo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public string VoyagerInvoiceNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "VoygerSale Date")]
        public DateTime VoygerDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal VoyagerAmount { get; set; }

        public string ShippingMode { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal VendorFee { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal ProfitValue { get; set; }

        public string Remarks { get; set; }
        [ForeignKey("OnlineVendor")]
        public int OnlineVendorId { get; set; }
        public virtual OnlineVendor Vendor { get; set; }

        public virtual OnlineSaleReturn SaleReturn { get; set; }

        public string UserName { get; set; }

    }

    public class OnlineSaleReturn
    {
        public int OnlineSaleReturnId { get; set; }

        public int OnlineSaleId { get; set; }
        public virtual OnlineSale OnlineSale { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        [Display(Name = "Invoice No")]
        public string InvNo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public string VoyagerInvoiceNo { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "VoygerSale Date")]
        public DateTime VoygerDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal VoyagerAmount { get; set; }

        public string Remarks { get; set; }
        [Display(Name = "Recived")]
        public bool IsRecived { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Recived Date")]
        public DateTime? RecivedDate { get; set; }

        public string UserName { get; set; }

        //[ForeignKey("OnlineVendor")]
        //public int OnlineVendorId { get; set; }
        //public virtual OnlineVendor Vendor { get; set; }

    }




}