using System;
using System.Collections.Generic;

namespace AprajitaRetails.Models
{
    public class OnlineVendor
    {
        public int OnlineVendorId { get; set; }
        public string VendorName { get; set; }
        public DateTime OnDate { get; set; }
        public bool IsActive { get; set; }
        public string Remark { get; set; }
        public DateTime? OffDate { get; set; }
        public string Reason { get; set; }
        public ICollection<OnlineSale> OnlineSales { get; set; }
        // public ICollection<OnlineSaleReturn> OnlineSaleReturns { get; set; }
    }




}