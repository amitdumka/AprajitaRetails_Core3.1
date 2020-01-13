using AprajitaRetails.Data;
using AprajitaRetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Triggers
{
    public class SalesManager
    {
        private void UpDateAmount(AprajitaRetailsContext db,PayModes PayMode, DateTime SaleDate, decimal CashAmount)
        {
            if (PayMode == PayModes.Cash && CashAmount > 0)
            {
                CashTrigger.UpdateCashInHand(db, SaleDate, CashAmount);

            }
            //TODO: in future make it more robust
            if (PayMode != PayModes.Cash && PayMode != PayModes.Coupons && PayMode != PayModes.Points)
            {
                CashTrigger.UpdateCashInBank(db, SaleDate, CashAmount);
            }
        }
        private void UpdateDueAmount(AprajitaRetailsContext db, PayModes PayMode, DateTime SaleDate, Decimal CashAmount)
        {

        }

        private void UpdateSalesRetun(AprajitaRetailsContext db, DailySale dailySale) {

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


        public void ProcessAccounts(AprajitaRetailsContext db, DailySale dailySale)
        {
            if (!dailySale.IsSaleReturn)
            {
                if (!dailySale.IsDue)
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

        public void ProcessAccountDelete(AprajitaRetailsContext db, DailySale dailySale)
        {
            if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
            {
                CashTrigger.UpdateCashInHand(db, dailySale.SaleDate, 0 - dailySale.CashAmount);
            }
            else if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
            {
                CashTrigger.UpdateCashInBank(db, dailySale.SaleDate, 0 - dailySale.CashAmount);
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
                
                throw new Exception();
            }
        }

        public void ProcessAccountEdit(AprajitaRetailsContext db, DailySale dailySale)
        {
            var oldSale = db.DailySales.Find(dailySale.DailySaleId);

            if (oldSale.PayMode == PayModes.Cash) 
            {
                CashTrigger.UpDateCashOutHand(db, oldSale.SaleDate, oldSale.CashAmount);
                

            }
            else
            {
                CashTrigger.UpDateCashOutBank(db, oldSale.SaleDate, oldSale.CashAmount);

            }
            if (oldSale.IsDue)
            {
                if (!oldSale.IsDue)
                {
                    //TODO: Remove due
                }
            }
            else { //TODO: Add due
            }
            
            if (oldSale.IsSaleReturn) { } else { }
            



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
}
