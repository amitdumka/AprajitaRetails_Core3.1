using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Areas.Purchase.Models
{
    public class Brand
    {
        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; }
        public string BCode { get; set; }
    }


}
