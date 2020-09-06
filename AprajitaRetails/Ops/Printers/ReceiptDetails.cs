//using iText.Pdfa;
using System;
//using iText.Kernel.Pdf;
//using iText.Layout;
//using iText.Layout.Element;
//using System.Windows.Forms;

namespace AprajitaRetails.Ops.Printers
{
    public class ReceiptDetails
    {
        public const string Employee = "Cashier: M0001      Name: Manager"; //TODO: implement to help 

        public string BillNo;// = "Bill NO: 67676767";
        public string BillDate;// = "                Date: ";
        public string BillTime; //= "                Time: ";
        public string CustomerName;// = "Customer Name: ";

        public ReceiptDetails(string invNo, DateTime onDate, string time, string custName)
        {
            BillNo = "Bill No: "+invNo;
            BillDate = "                Date: "+onDate.Date.ToShortDateString();
            BillTime = "                Time: "+time;
            CustomerName = "Customer Name: "+custName;
        }


    }



}
