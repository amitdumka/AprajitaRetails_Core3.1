using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Accounts.Models
{


    public class RentedLocation
    {
        public int RentedLocationId { get; set; }
        public string PlaceName { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime OnDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Vacate Date")]
        public DateTime? VacatedDate { get; set; }

        public string City { get; set; }
        public string OwnerName { get; set; }
        public string MobileNo { get; set; }
        public decimal RentAmount { get; set; }
        public decimal AdvanceAmount { get; set; }
        public bool IsRented { get; set; }
        public RentType RentType { get; set; }
    }

    public class Rent
    {
        public int RentId { get; set; }
        [Display(Name ="Location")]
        public int RentedLocationId { get; set; }
        public virtual RentedLocation Location { get; set; }
        public RentType RentType { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime OnDate { get; set; }

        public string Period { get; set; }

        [Display(Name = "Amount")]
        [DataType(DataType.Currency), Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public PaymentMode Mode { get; set; }
        public string PaymentDetails { get; set; }
        public string Remarks { get; set; }
    }




}