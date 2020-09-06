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

    public class ReceiptItemTotal
    {
        public string TotalItem { get; set; }
        public string ItemCount { get; set; }
        public string CashAmount { get; set; }
        public string NetAmount { get; set; }
    }



}
