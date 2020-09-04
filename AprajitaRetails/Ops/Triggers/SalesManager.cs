using AprajitaRetails.Areas.Purchase.Models;
using AprajitaRetails.Areas.Sales.Models.Views;
using AprajitaRetails.Areas.Voyager.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Bot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Triggers
{
    public class SalesManager
    {
        private void UpDateAmount(AprajitaRetailsContext db, DailySale dailySale, bool IsEdit)
        {
            if (IsEdit)
            {
                if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                {
                    CashTrigger.UpdateCashInHand(db, dailySale.SaleDate, 0 - dailySale.CashAmount);

                }
                //TODO: in future make it more robust
                if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                {
                    CashTrigger.UpdateCashInBank(db, dailySale.SaleDate, 0 - (dailySale.Amount - dailySale.CashAmount));
                }
            }
            else
            {
                if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                {
                    CashTrigger.UpdateCashInHand(db, dailySale.SaleDate, dailySale.CashAmount);

                }
                //TODO: in future make it more robust
                if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                {
                    CashTrigger.UpdateCashInBank(db, dailySale.SaleDate, dailySale.Amount - dailySale.CashAmount);
                }
            }

        }
        private void UpdateDueAmount(AprajitaRetailsContext db, DailySale dailySale, bool IsEdit)
        {
            if (IsEdit)
            {
                var dId = db.DuesLists.Where(c => c.DailySaleId == dailySale.DailySaleId).FirstOrDefault();
                if (dId != null)
                {
                    db.DuesLists.Remove(dId);
                }
                else
                {
                    //TODO: Handle this
                }
            }
            else
            {
                decimal dueAmt;
                if (dailySale.Amount != dailySale.CashAmount)
                {
                    dueAmt = dailySale.Amount - dailySale.CashAmount;
                }
                else
                    dueAmt = dailySale.Amount;

                DuesList dl = new DuesList() { Amount = dueAmt, DailySale = dailySale, DailySaleId = dailySale.DailySaleId };
                db.DuesLists.Add(dl);
            }


        }

        private void UpdateSalesRetun(AprajitaRetailsContext db, DailySale dailySale, bool IsEdit)
        {

            if (IsEdit)
            {
                if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                {
                    CashTrigger.UpDateCashOutHand(db, dailySale.SaleDate, 0 - dailySale.CashAmount);

                }
                //TODO: in future make it more robust
                if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                {
                    CashTrigger.UpDateCashOutBank(db, dailySale.SaleDate, 0 - (dailySale.Amount - dailySale.CashAmount));
                }
                //dailySale.Amount = 0 - dailySale.Amount;
            }
            else
            {
                if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                {
                    CashTrigger.UpDateCashOutHand(db, dailySale.SaleDate, dailySale.CashAmount);

                }
                //TODO: in future make it more robust
                if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                {
                    CashTrigger.UpDateCashOutBank(db, dailySale.SaleDate, dailySale.Amount - dailySale.CashAmount);
                }
                dailySale.Amount = 0 - dailySale.Amount;
            }


        }


        public void OnInsert(AprajitaRetailsContext db, DailySale dailySale)
        {
            if (!dailySale.IsSaleReturn)
            {
                if (!dailySale.IsDue)
                {
                    UpDateAmount(db, dailySale, false);
                }
                else
                {
                    UpdateDueAmount(db, dailySale, false);
                    UpDateAmount(db, dailySale, false);
                }
            }
            else
            {
                UpdateSalesRetun(db, dailySale, false);

            }
            SaleBot.NotifySale(db, dailySale.SalesmanId, dailySale.Amount);
        }

        public void OnDelete(AprajitaRetailsContext db, DailySale dailySale)
        {
            //TODO: Handle for Dues 
            if (dailySale.IsSaleReturn)
            {
                UpdateSalesRetun(db, dailySale, true);
            }
            else
            {
                if (dailySale.IsDue)
                {
                    UpdateDueAmount(db, dailySale, true);
                }
                else
                {
                    //TODO: Add this option in Create and Edit also
                    // Handle when payment is done by Coupons and Points. 
                    // Need to create table to create Coupn and Royalty point.
                    // Points will go in head for Direct Expenses 
                    // Coupon Table will be colloum for TAS Coupon and Apajita Retails. 

                    //TODO: Need to handle is. 
                    // If payment is cash and cashamount is zero then need to handle this option also 
                    // may be error entry , might be due.

                    // throw new Exception();
                }

                UpDateAmount(db, dailySale, true);
            }


        }

        public void OnUpdate(AprajitaRetailsContext db, DailySale dailySale)
        {
            var oldSale = db.DailySales.Find(dailySale.DailySaleId);

            UpDateAmount(db, oldSale, true);

            if (oldSale.IsSaleReturn)
            {
                // SaleRetun
            }
            else
            {
                // Normal Bill
                if (oldSale.IsDue)
                {
                    if (!dailySale.IsDue)
                    {
                        UpdateSalesRetun(db, oldSale, true);
                    }
                    else
                    {

                    }
                }
                else
                { //TODO: Add due
                }

                UpDateAmount(db, oldSale, true);
                UpDateAmount(db, dailySale, false);
            }

            SaleBot.NotifySale(db, dailySale.SalesmanId, dailySale.Amount);


        }
    }

    public class RegularSaleManager
    {
        public string GenerateInvoiceNo(bool isManual = true)
        {
            return "InvoiceNo.";
        }

        public int OnInsert(AprajitaRetailsContext db, SaveOrderDTO sales, int StoreId = 1)
        {
            Customer cust = db.Customers.Where(c => c.MobileNo == sales.MobileNo).FirstOrDefault();
            if (cust == null)
            {
                cust = new Customer
                {
                    City = sales.Address,
                    Age = 30,
                    FirstName = sales.Name,
                    Gender = Genders.Male,
                    LastName = sales.Name,
                    MobileNo = sales.MobileNo,
                    NoOfBills = 0,
                    TotalAmount = 0,
                    CreatedDate = DateTime.Now.Date
                };
                db.Customers.Add(cust);

            }
            string InvNo = GenerateInvoiceNo(true);
            List<RegularSaleItem> itemList = new List<RegularSaleItem>();
            List<Stock> stockList = new List<Stock>();

            foreach (var item in sales.SaleItems)
            {
                RegularSaleItem sItem = new RegularSaleItem
                {
                    BarCode = item.BarCode,
                    MRP = item.Price,
                    Qty = item.Quantity,
                    Discount = 0,
                    SalesmanId = item.Salesman,
                    Units = item.Units,
                    InvoiceNo = InvNo,
                    BasicAmount = item.Amount,
                    TaxAmount = 0,
                    ProductItemId = -1,
                    BillAmount = 0,
                    SaleTaxTypeId = 1, //TODO: default tax id needed

                };
                ProductItem pItem = db.ProductItems.Where(c => c.Barcode == item.BarCode).FirstOrDefault();
                Stock stock = db.Stocks.Where(c => c.ProductItemId == pItem.ProductItemId && c.StoreId == StoreId).FirstOrDefault();

                sItem.ProductItemId = pItem.ProductItemId;
                decimal amt = (decimal)item.Quantity * item.Price;
                sItem.BasicAmount = (amt * 100) / (100 + pItem.TaxRate);
                sItem.TaxAmount = (sItem.BasicAmount * pItem.TaxRate) / 100;
                sItem.BillAmount = sItem.BasicAmount + sItem.TaxAmount;
                //SaleTax Id
                var taxid = db.SaleTaxTypes.Where(c => c.CompositeRate == pItem.TaxRate).Select(c => c.SaleTaxTypeId).FirstOrDefault();
                if (taxid <= 0)
                {
                    taxid = 1; //TODO: Handle it for creating new saletax id
                }
                sItem.SaleTaxTypeId = taxid;

                itemList.Add(sItem);


                stock.SaleQty += item.Quantity;
                stock.Quantity -= item.Quantity;
                stockList.Add(stock);
            }

            var totalBillamt = itemList.Sum(c => c.BillAmount);
            var totaltaxamt = itemList.Sum(c => c.TaxAmount);
            var totalDiscount = itemList.Sum(c => c.Discount);
            var totalQty = itemList.Sum(c => c.Qty);
            var totalitem = itemList.Count;

            decimal roundoffamt = Math.Round(totalBillamt) - totalBillamt;

            PaymentDetail pd = new PaymentDetail
            {
                CardAmount = sales.PaymentInfo.CardAmount,
                CashAmount = sales.PaymentInfo.CashAmount,
                InvoiceNo = InvNo,
                IsManualBill = true,
                MixAmount = 0,
                PayMode = SalePayMode.Cash
            };

            if (sales.PaymentInfo.CardAmount > 0)
            {
                if (sales.PaymentInfo.CashAmount > 0)
                {
                    pd.PayMode = SalePayMode.Mix;
                }
                else
                {
                    pd.PayMode = SalePayMode.Card;
                }

                CardDetail cd = new CardDetail
                {
                    CardCode = CardTypes.Visa,//TODO: default
                    Amount = sales.PaymentInfo.CardAmount,
                    AuthCode = (int)Int64.Parse(sales.PaymentInfo.AuthCode),
                    InvoiceNo = InvNo,
                    LastDigit = (int)Int64.Parse(sales.PaymentInfo.CardNo),
                    CardType = CardModes.DebitCard//TODO: default 

                };

                if (sales.PaymentInfo.CardType.Contains("Debit") || sales.PaymentInfo.CardType.Contains("debit") || sales.PaymentInfo.CardType.Contains("DEBIT"))
                { cd.CardType = CardModes.DebitCard; }
                else if (sales.PaymentInfo.CardType.Contains("Credit") || sales.PaymentInfo.CardType.Contains("credit") || sales.PaymentInfo.CardType.Contains("CREDIT"))
                { cd.CardType = CardModes.CreditCard; }

                if (sales.PaymentInfo.CardType.Contains("visa") || sales.PaymentInfo.CardType.Contains("Visa") || sales.PaymentInfo.CardType.Contains("VISA"))
                { cd.CardCode = CardTypes.Visa; }
                else if (sales.PaymentInfo.CardType.Contains("MasterCard") || sales.PaymentInfo.CardType.Contains("mastercard") || sales.PaymentInfo.CardType.Contains("MASTERCARD"))
                { cd.CardCode = CardTypes.MasterCard; }
                else if (sales.PaymentInfo.CardType.Contains("Rupay") || sales.PaymentInfo.CardType.Contains("rupay") || sales.PaymentInfo.CardType.Contains("RUPAY"))
                { cd.CardCode = CardTypes.Rupay; }
                else if (sales.PaymentInfo.CardType.Contains("MASTRO") || sales.PaymentInfo.CardType.Contains("mastro") || sales.PaymentInfo.CardType.Contains("Mastro"))
                { cd.CardCode = CardTypes.Rupay; }

                pd.CardDetail = cd;
            }


            RegularInvoice Invoice = new RegularInvoice
            {
                Customer = cust,
                InvoiceNo = InvNo,
                OnDate = sales.OnDate,
                IsManualBill = true,
                StoreId = StoreId,
                SaleItems = itemList,
                CustomerId = cust.CustomerId,
                TotalBillAmount = totalBillamt + roundoffamt,
                TotalDiscountAmount = totalDiscount,
                TotalItems = totalitem,
                TotalQty = totalQty,
                TotalTaxAmount = totaltaxamt,
                RoundOffAmount = roundoffamt,
                PaymentDetail = pd

            };

            db.RegularInvoices.Add(Invoice);
            db.Stocks.UpdateRange(stockList);

           
            int nor= db.SaveChanges();
            return nor;

        }
    }
}
