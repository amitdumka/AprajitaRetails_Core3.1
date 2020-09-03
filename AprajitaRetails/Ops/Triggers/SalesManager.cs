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
            SaleBot.NotifySale (db, dailySale.SalesmanId, dailySale.Amount);
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

            SaleBot.NotifySale (db,  dailySale.SalesmanId, dailySale.Amount);


        }
    }

    public class RegularSaleManager
    {
        public string GenerateInvoiceNo(bool isManual=true)
        {
            return "InvoiceNo.";
        }

        public void OnInsert(AprajitaRetailsContext db, SaveOrderDTO sales, int StoreId=1)
        {
            Customer cust = db.Customers.Where(c => c.MobileNo == sales.MobileNo).FirstOrDefault();
            if (cust == null)
            {
                cust = new Customer {
                    City=sales.Address, Age=30, FirstName=sales.Name, Gender=Genders.Male, LastName=sales.Name, MobileNo=sales.MobileNo, 
                    NoOfBills=0,TotalAmount=0, CreatedDate=DateTime.Now.Date
                };
                db.Customers.Add(cust);
                
            }
            string InvNo = GenerateInvoiceNo(true);
            List<RegularSaleItem> itemList = new List<RegularSaleItem>();
            RegularInvoice Invoice = new RegularInvoice {
                Customer = cust, InvoiceNo = InvNo, OnDate = sales.OnDate, IsManualBill=true, StoreId=StoreId
            };
            
            foreach (var item in sales.SaleItems)
            {
                RegularSaleItem sItem = new RegularSaleItem {
                    BarCode=item.BarCode, MRP=item.Price, Qty=item.Quantity, BasicAmount=item.Amount, Discount=0, SalesmanId=item.Salesman, Units=item.Units, 
                    InvoiceNo=InvNo,ProductItemId=-1 
                    
                };
                ProductItem pItem = db.ProductItems.Where(c => c.Barcode == item.BarCode).FirstOrDefault();
                Stock stock = db.Stocks.Where(c => c.ProductItemId == pItem.ProductItemId && c.StoreId == StoreId).FirstOrDefault();

                sItem.ProductItemId = pItem.ProductItemId;
                decimal amt = (decimal)item.Quantity * item.Price;

                sItem.BasicAmount = (amt*100)/(100+pItem.TaxRate);
                
                sItem.TaxAmount = (sItem.BasicAmount * pItem.TaxRate) / 100;
                sItem.BillAmount = sItem.BasicAmount + sItem.TaxAmount;

            }


        }
    }
}
