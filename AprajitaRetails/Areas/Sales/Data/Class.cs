using AprajitaRetails.Areas.Sales.Models.Views;
using AprajitaRetails.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Sales.Data
{
    public class SaleHelper
    {
        public static InvoiceDetails GetInvoiceData(AprajitaRetailsContext db, int id)
        {
            var inv = db.RegularInvoices.Include(c => c.Customer).Include(c => c.PaymentDetail).ThenInclude(c => c.CardDetail).Where(c=>c.RegularInvoiceId==id).FirstOrDefault();
            if (inv == null) { return null; }
            var saleitem = db.RegularSaleItems.Include(c=>c.Salesman).Include(c => c.ProductItem).Where(c => c.InvoiceNo == inv.InvoiceNo).ToList();

            InvoiceDetails iDetails = new InvoiceDetails {
                Invoice=SaleInvoiceView.CopyTo(inv,saleitem),Error="OK", Msg="Data Present"
            };

            if (iDetails.Invoice.PaymentMode == "Card") iDetails.IsCardPayment = true; else iDetails.IsCardPayment = false;

            return iDetails;
            
        }


    }
   public class InvoiceDetails
    {
        public SaleInvoiceView Invoice;
        public bool IsCardPayment;
        public string Msg;
        public string Error;
    }

    public class SaleItemView
    {
        public string BarCode;
        public string SmCode;
        public string ProductName;
        public decimal MRP;
        public decimal BillAmount;
        public double Qty;
        public string Units;
    }

    public class SaleInvoiceView
    {
        public string InvoiceNo;
        public string CustomerName;

        public DateTime OnDate;

        public string TotalAmount, Discount, TotalQty, NoofItem;

        public List<SaleItemView> SaleItems;
        public string PaymentMode;
        public string CashAmount;
        public string CardAmount;
        public string CardNumber;
        public string CardType;
        public string AuthCode;

        public static SaleInvoiceView CopyTo(RegularInvoice inv , List<RegularSaleItem> sItems)
        {
            List<SaleItemView> saleItems = new List<SaleItemView>();
            foreach (var item in sItems)
            {
                SaleItemView si = new SaleItemView
                {
                    BarCode = item.BarCode,
                    BillAmount = item.BillAmount,
                    MRP = item.MRP,
                    Qty = item.Qty,
                    ProductName = item.ProductItem.ProductName,
                    SmCode = item.Salesman.SalesmanName, Units="Pcs/Mtrs"
                };
                saleItems.Add(si);
                //TODO: add unit name
            }


            SaleInvoiceView vm = new SaleInvoiceView
            {
                SaleItems = saleItems,
                InvoiceNo = inv.InvoiceNo,
                OnDate = inv.OnDate,
                NoofItem = inv.TotalItems.ToString(),
                CustomerName = inv.Customer.FullName,
                TotalQty = inv.TotalQty.ToString(),
                TotalAmount = inv.TotalBillAmount.ToString(),
                Discount = inv.TotalDiscountAmount.ToString()
            };

            if (inv.PaymentDetail.CardAmount > 0)
            {
                vm.PaymentMode = "Card";
                vm.CardAmount = inv.PaymentDetail.CardAmount.ToString();
                vm.AuthCode = inv.PaymentDetail.CardDetail.AuthCode.ToString();
                vm.CardNumber = inv.PaymentDetail.CardDetail.LastDigit.ToString();
                vm.CardType = "#";
            }
            else
            {
                vm.PaymentMode = "Cash";
                vm.CashAmount = inv.PaymentDetail.CashAmount.ToString();
            }

            return vm;
        
        }
    }

}
