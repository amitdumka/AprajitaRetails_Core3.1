using System.ComponentModel.DataAnnotations;

namespace AprajitaRetails.Areas.Purchase.Models
{
    //Global Class

    public class Category
    {
        public int CategoryId { get; set; }
        [Display(Name="Category")]
        public string CategoryName { get; set; }
        [Display(Name ="Primary")]
        public bool IsPrimaryCategory { get; set; }
        [Display(Name ="Secondary")]
        public bool IsSecondaryCategory { get; set; }
    }


}
