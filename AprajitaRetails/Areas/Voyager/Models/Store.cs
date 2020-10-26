using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Uploader.Models;
using AprajitaRetails.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class Store
    {
        public int StoreId { get; set; }
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
        [Display(Name = "PAN No")]
        public string PanNo { get; set; }
        [Display(Name = "GSTIN ")]
        public string GSTNO { get; set; }
        [Display(Name = "Employees Count")]
        public int NoOfEmployees { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Opening Date")]
        public DateTime OpeningDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Closing Date")]
        public DateTime? ClosingDate { get; set; }
        [Display(Name = "Operative")]
        public bool Status { get; set; }

        public virtual ICollection<ImportPurchase> ImportPurchases { get; set; }
        public virtual ICollection<ImportInWard> ImportInWards { get; set; }
        public virtual ICollection<ImportSaleItemWise> ImportSaleItemWises { get; set; }
        public virtual ICollection<ImportSaleRegister> ImportSaleRegisters { get; set; }

        //Purchase
        public virtual ICollection<ProductPurchase> ProductPurchases { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

        public virtual ICollection<DailySale> DailySales { get; set; }
        public virtual ICollection<Salesman> Salesmen { get; set; }
        public virtual ICollection<CashInHand> CashInHands { get; set; }
        public virtual ICollection<CashInBank> CashInBanks { get; set; }

        //public int CompanyId { get; set; }
        //public virtual Company Company { get; set; }



    }

    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhoneNo { get; set; }
        public string WebSite { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }


}
