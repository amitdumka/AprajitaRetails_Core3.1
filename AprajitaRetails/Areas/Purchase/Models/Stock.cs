namespace AprajitaRetails.Areas.Voyager.Models
{
    public class Stock
    {
        public int StockID { set; get; }

        public int ProductItemId { set; get; }
        public virtual ProductItem ProductItem { get; set; }

        public double Quantity { set; get; }
        public double SaleQty { get; set; }
        public double PurchaseQty { get; set; }
        public Units Units { get; set; }


    }


}
