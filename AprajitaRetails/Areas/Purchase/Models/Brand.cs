using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AprajitaRetails.Areas.Purchase.Models
{
    //global Class
    public class Brand
    {
        public int BrandId { get; set; }
        [Display (Name = "Brand")]
        public string BrandName { get; set; }
        [Display (Name = "Brand Code")]
        public string BCode { get; set; }
    }
    public class Category
    {
        public int CategoryId { get; set; }
        [Display (Name = "Category")]
        public string CategoryName { get; set; }
        [Display (Name = "Primary")]
        public bool IsPrimaryCategory { get; set; }
        [Display (Name = "Secondary")]
        public bool IsSecondaryCategory { get; set; }
    }
    public class Supplier
    {
        public int SupplierID { get; set; }
        [Display (Name = "Supplier")]
        public string SuppilerName { get; set; }
        public string Warehouse { get; set; }
        public ICollection<ProductPurchase> ProductPurchases { get; set; }
    }
    public class PurchaseTaxType
    {
        public int PurchaseTaxTypeId { get; set; }
        [Display (Name = "Tax")]
        public string TaxName { get; set; }
        [Display (Name = "Tax Type")]
        public TaxType TaxType { get; set; }
        [Display (Name = "Composite Rate")]
        [DataType (DataType.Currency), Column (TypeName = "money")]
        public decimal CompositeRate { get; set; }
        //Navigation
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
    }

}
