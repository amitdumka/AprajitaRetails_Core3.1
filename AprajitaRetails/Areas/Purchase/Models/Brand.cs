using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Areas.Purchase.Models
{
    //global Class

    public class Brand
    {
        public int BrandId { get; set; }
        [Display(Name = "Brand")]
        public string BrandName { get; set; }
        [Display(Name ="Brand Code")]
        public string BCode { get; set; }
    }


}
