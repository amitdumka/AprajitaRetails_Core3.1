using System.Collections.Generic;

namespace AprajitaRetails.Areas.Voyager.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SuppilerName { get; set; }
        public string Warehouse { get; set; }
        public ICollection<ProductPurchase> ProductPurchases { get; set; }
    }


}
