using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Models
{
    //Version 3.0
    public class StoreLocation
    {
        public int StoreLocationId { get; set; }
        [Display(Name = "Store Code")]
        public string StoreCode { get; set; }
        [Display(Name = "Store Name")]
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "Pin Code")]
        public string PinCode { get; set; }
        [Display(Name = "Contact No")]
        public string PhoneNo { get; set; }
        [Display(Name = "Store Manager Name")]
        public string StoreManagerName { get; set; }
        [Display(Name = "SM Contact No")]
        public string StoreManagerPhoneNo { get; set; }



        public virtual ICollection<DailySale> DailySales { get; set; }
        public virtual ICollection<Salesman> Salesmen { get; set; }
        public virtual ICollection<CashInHand> CashInHands { get; set; }
        public virtual ICollection<CashInBank> CashInBanks { get; set; }
    }




}