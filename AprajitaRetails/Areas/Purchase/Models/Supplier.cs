using AprajitaRetails.Areas.Voyager.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Areas.Purchase.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        [Display(Name ="Supplier")]
        public string SuppilerName { get; set; }
        public string Warehouse { get; set; }
        public ICollection<ProductPurchase> ProductPurchases { get; set; }
    }


}
