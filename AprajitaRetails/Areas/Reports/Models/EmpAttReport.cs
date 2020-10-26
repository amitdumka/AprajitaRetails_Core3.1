using AprajitaRetails.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Reports.Models
{


    public class EmpAttReport
    {
        public int EmpAttReportId { get; set; }

        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public string EmployeeName { get; set; }

        public EmpType Type { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joinning Date")]
        public DateTime JoinningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }
        public bool IsWorking { get; set; }


        public decimal TotalDaysPresent { get; set; }

        public decimal TotalDaysAbsent { get; set; }

        public decimal TotalDaysHalfDay { get; set; }

        public decimal TotalSundayPresent { get; set; }

        public decimal TotalFinalPresent { get; set; }

        public int NoOfWorkingDays { get; set; }

    }
    public class EmpFinReport
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public string EmployeeName { get; set; }
        public EmpType Type { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Joinning Date")]
        public DateTime JoinningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Leaving Date")]
        public DateTime? LeavingDate { get; set; }
        public bool IsWorking { get; set; }


        public int EmpFinReportId { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal? TotalSale { get; set; }
        public int? NoOfBill { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal? AverageSale { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal? TotalLastPcIncentive { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal? TotalWowBillIncentive { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal? TotalSaleIncentive { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalSalaryPaid { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalSalaryAdvancePaid { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalAdvancePaidOff { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal TotalBalance { get; set; }
    }

    public class EmpReport
    {
        public int EmpReportId { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


        public EmpAttReport? AttReport { get; set; }
        public EmpFinReport? FinReport { get; set; }

    }


    public class BookingOverDue
    {
        [Display(Name = "Booking ID")]
        public int BookingId { get; set; }
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Slip No")]
        public string SlipNo { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery Date")]
        public DateTime DelveryDate { get; set; }
        public int Quantity { get; set; }
        [Display(Name = "Due Days")]
        public int NoDays { get; set; }

    }


}
