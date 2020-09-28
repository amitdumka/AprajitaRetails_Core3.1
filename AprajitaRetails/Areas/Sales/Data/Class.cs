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
        public SaleInvoiceView Invoice{get; set;}
        public bool IsCardPayment{get; set;}
        public string Msg{get; set;}
        public string Error{get; set;}
    }

    public class SaleItemView
    {
        public string BarCode{get; set;}
        public string SmCode{get; set;}
        public string ProductName{get; set;}
        public decimal MRP{get; set;}
        public decimal BillAmount{get; set;}
        public double Qty{get; set;}
        public string Units{get; set;}
    }

    public class SaleInvoiceView
    {
        public string InvoiceNo{get; set;}
        public string CustomerName{get; set;}

        public DateTime OnDate{get; set;}

        public string TotalAmount { get; set; }
        public string Discount { get; set;}
        public string TotalQty { get; set;}
        public string NoofItem { get; set;}

        public List<SaleItemView> SaleItems{get; set;}
        public string PaymentMode{get; set;}
        public string CashAmount{get; set;}
        public string CardAmount{get; set;}
        public string CardNumber{get; set;}
        public string CardType{get; set;}
        public string AuthCode{get; set;}

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
