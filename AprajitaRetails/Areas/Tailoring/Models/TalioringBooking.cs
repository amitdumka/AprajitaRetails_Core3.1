using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AprajitaRetails.Areas.Voyager.Models;
namespace AprajitaRetails.Models
{

    public class TalioringBooking
    {
        public int TalioringBookingId { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Customer Name")]
        public string CustName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Delivery Date")]
        public DateTime DeliveryDate { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Trail Date")]
        public DateTime TryDate { get; set; }

        [Display(Name = "Booking Slip")]
        public string BookingSlipNo { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Total Qty")]
        public int TotalQty { get; set; }

        [Display(Name = "Shirt Qty")]
        public int ShirtQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Shirt Price")]
        public decimal ShirtPrice { get; set; }

        [Display(Name = "Pant Qty")]
        public int PantQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Pant Price")]
        public decimal PantPrice { get; set; }

        [Display(Name = "Coat Qty")]
        public int CoatQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Coat Price")]
        public decimal CoatPrice { get; set; }

        [Display(Name = "Kurta Qty")]
        public int KurtaQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Kurta Price")]
        public decimal KurtaPrice { get; set; }

        [Display(Name = "Bundi Qty")]
        public int BundiQty { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Bundi Price")]
        public decimal BundiPrice { get; set; }

        [Display(Name = "Others Qty")]
        public int Others { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "money"), Display(Name = "Others Price")]
        public decimal OthersPrice { get; set; }

        [DefaultValue(false)]
        public bool IsDelivered { get; set; }

        public virtual ICollection<TalioringDelivery> Deliveries { get; set; }

        //Version 3.0
        [DefaultValue(1)]
        public int? StoreId { get; set; }
        public virtual Store Store { get; set; }
    }
}