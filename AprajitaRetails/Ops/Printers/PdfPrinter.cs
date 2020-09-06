//using iText.Pdfa;
using AprajitaRetails.Areas.Sales.Models.Views;
using AprajitaRetails.Data;
using System;
using System.Collections.Generic;
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
    }

    
    public class PrinterHelper
    {
       
        public static List<ReceiptItemDetails> GetInvoiceDetails(AprajitaRetailsContext db, List<RegularSaleItem> saleItem)
        {
            List<ReceiptItemDetails> itemList = new List<ReceiptItemDetails>();
            foreach (var item in saleItem)
            {
                ReceiptItemDetails rid = new ReceiptItemDetails { 
                    BasicPrice=item.BasicAmount.ToString(), Discount=item.Discount.ToString(), MRP=item.MRP.ToString(), 
                    QTY=item.Qty.ToString() , GSTAmount=(item.TaxAmount/2).ToString(), HSN="", GSTPercentage="", SKUDescription=item.BarCode
                };

                if (item.HSNCode != null) rid.HSN = item.HSNCode.ToString();
                rid.SKUDescription+="/" +db.ProductItems.Find(item.ProductItemId).ItemDesc;
                rid.GSTPercentage = (db.SaleTaxTypes.Find(item.SaleTaxTypeId).CompositeRate / 2).ToString();
                itemList.Add(rid);
            }
            return itemList;
        }


        /// <summary>
        /// Get RecieptTotals
        /// </summary>
        /// <param name="inv"></param>
        /// <returns></returns>
        public static ReceiptItemTotal GetReceiptItemTotal(RegularInvoice inv)
        {
            ReceiptItemTotal total = new ReceiptItemTotal
            {
                ItemCount = inv.TotalItems.ToString(),
                TotalItem = inv.TotalQty.ToString(),
                NetAmount = inv.TotalBillAmount.ToString(),
                CashAmount = inv.PaymentDetail.CashAmount.ToString()
            };
            return total;


        }

        /// <summary>
        /// Get Reciept Details based on Invoice No , Date, time and CustomerName
        /// </summary>
        /// <param name="invNo"></param>
        /// <param name="onDate"></param>
        /// <param name="time"></param>
        /// <param name="custName"></param>
        /// <returns></returns>
        public static ReceiptDetails GetReceiptDetails(string invNo, DateTime  onDate, string time, string custName)
        {
            return new ReceiptDetails(invNo, onDate, time, custName);
        }


        /// <summary>
        /// Return RecieptHeader based on StoreId
        /// </summary>
        /// <param name="db"></param>
        /// <param name="Storeid"></param>
        /// <returns></returns>
        public static ReceiptHeader GetReceiptHeader(AprajitaRetailsContext db, int Storeid)
        {
            var store = db.Stores.Find(Storeid);

            ReceiptHeader header = new ReceiptHeader
            {
                StoreName = store.StoreName,
                StoreAddress = store.Address,
                StoreCity = store.City,
                StoreGST = store.GSTNO,
                StorePhoneNo = store.PhoneNo
            };
            return header;
        }

    }



}
