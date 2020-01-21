using System.Collections.Generic;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class ProductItem
    {
        public int ProductItemId { set; get; }

        public string Barcode { get; set; }

        public int BrandId { get; set; }
        public virtual Brand BrandName { get; set; }

        public string StyleCode { get; set; }
        public string ProductName { get; set; }
        public string ItemDesc { get; set; }

        public ProductCategorys Categorys { get; set; }

        public Category MainCategory { get; set; }
        public Category ProductCategory { get; set; }
        public Category ProductType { get; set; }


        public decimal MRP { get; set; }
        public decimal TaxRate { get; set; }    // TODO:Need to Review in final Edition
        public decimal Cost { get; set; }

        public Sizes Size { get; set; }
        public Units Units { get; set; }


        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }


    }


}
