using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Accounts.Models
{
    public class ElectricityConnection
    {
        public int ElectricityConnectionId { get; set; }
        public string LocationName { get; set; }
        public string ConnectioName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string ConsumerNumber { get; set; }
        public string ConusumerId { get; set; }
        public ConnectionType Connection { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Connection Date")]
        public DateTime ConnectinDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Disconnection Date")]
        public DateTime? DisconnectionDate { get; set; }

        public int KVLoad { get; set; }
        public bool OwnedMetter { get; set; }

        [Display(Name = "Connection Amount"), DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalConnectionCharges { get; set; }
        [Display(Name = "Security Deposit"), DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal SecurityDeposit { get; set; }
        public string Remarks { get; set; }


    }


    public class EletricityBill
    {
        public int EletricityBillId { get; set; }
        public int ElectricityConnectionId { get; set; }
        public string BillNumber { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date")]
        public DateTime BillDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Date")]
        public DateTime MeterReadingDate { get; set; }

        public double CurrentMeterReading { get; set; }
        public double TotalUnit { get; set; }

        [Display(Name = "Current Amount"), DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal CurrentAmount { get; set; }
        [Display(Name = "Arrear Amount"), DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal ArrearAmount { get; set; }
        [Display(Name = "Net Amount"), DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal NetDemand { get; set; }

        public ElectricityConnection Connection { get; set; }

    }

    public class EBillPayment
    {
        public int EBillPaymentId { get; set; }
        public int EletricityBillId { get; set; }
        public virtual EletricityBill Bill { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }
        [Display(Name = "Amount"), DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public PaymentMode Mode { get; set; }
        public string PaymentDetails { get; set; }
        public string Remarks { get; set; }
        public bool IsPartialPayment { get; set; }
        public bool IsBillCleared { get; set; }


    }
}