//using iText.Pdfa;
//using iText.Kernel.Pdf;
//using iText.Layout;
//using iText.Layout.Element;
//using System.Windows.Forms;

namespace AprajitaRetails.Ops.Printers
{
    /*
    Store Name
    Address
    City
    Phone
    GST
    ++++++++++++++++++++++++++++++
    Retail Invoice
    +++++++++++++++++++++++++++++++
    Cashier :m01        Name:Manager
    Bill NO: 67676767
                        Date:8989
                        Time:909090
    Customer Name :hjhjhh
    ++++++++++++++++++++++++++++++++
    SKU Code/Description
    HSN     MRP     Qty     Disc
    cgst%   AMT    sgst%    AMT
    +++++++++++++++++++++++++++++++++
    M767676767/Tesca
            1000     1.2     0.0
    2.50     20.50   2.50    20.50
    +++++++++++++++++++++++++++++++
    Total :1.2              1041
    item(s) 1   Net Amount  1041
    ++++++++++++++++++++++++++++++++
    Tender
    Cash Amount       Rs  1041
    ++++++++++++++++++++++++++++++
    Basic Price             1000
    Cgst                    20.50
    sgst                    20.50
    ++++++++++++++++++++++++++++++=
            ** Amount included GST
    +++++++++++++++++++++++++++++++
    Thanks You
    Visit Again
    ++++++++++++++++++++++++++++++++++
    */

    public class ReceiptItemDetails
    {
        public string BasicPrice { get; set; }
        public string HSN { get; set; }
        public string SKUDescription { get; set; }
        public string MRP { get; set; }
        public string QTY { get; set; }
        public string Discount { get; set; }
        public string GSTPercentage { get; set; }
        public string GSTAmount { get; set; }
        public string Amount { get; set; }
    }



}
