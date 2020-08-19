using AprajitaRetails.Models;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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
      
        public decimal TotalFinalPresent{ get; set; }

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


        public int EmpFinReportId {get;set;}
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        
        public EmpAttReport? AttReport { get; set; }
        public EmpFinReport? FinReport { get; set; }

    }


    public class BookingOverDue
    {
        public int BookingId { get; set; }
        public string CustomerName { get; set; }
        public string SlipNo { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime DelveryDate { get; set; }
        public int Quantity { get; set; }
        public int NoDays { get; set; }

    }


}
