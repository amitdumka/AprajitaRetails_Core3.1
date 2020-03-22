using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Areas.Purchase.Models
{
    //Global Class

    public class ProductItem
    {
        public int ProductItemId { set; get; }

        public string Barcode { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public virtual Brand BrandName { get; set; }

        [Display(Name ="Style Code")]
        public string StyleCode { get; set; }
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }
        [Display(Name = "Item Desc")]
        public string ItemDesc { get; set; }

        [Display(Name = "Category")]
        public ProductCategorys Categorys { get; set; }
        [Display(Name = "Product Type")]
        public Category MainCategory { get; set; }
        [Display(Name = "Product Series")]
        public Category ProductCategory { get; set; }
        [Display(Name = "Sub Category")]
        public Category ProductType { get; set; }


        public decimal MRP { get; set; }
        [Display(Name = "Tax Rate")]
        public decimal TaxRate { get; set; }    // TODO:Need to Review in final Edition
        public decimal Cost { get; set; }

        public Sizes Size { get; set; }
        public Units Units { get; set; }


        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }


    }


}
