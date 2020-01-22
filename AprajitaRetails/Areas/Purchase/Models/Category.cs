namespace AprajitaRetails.Areas.Purchase.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool IsPrimaryCategory { get; set; }
        public bool IsSecondaryCategory { get; set; }
    }


}
