namespace AprajitaRetails.Areas.Voyager.Models
{
    internal class PaymentDetails
    {
        public int PaymentDetailsID { get; set; }
        public string InvoiceNo { get; set; }
        public SalePayMode PayMode { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public int CardDetailsID { get; set; }
    }


}
