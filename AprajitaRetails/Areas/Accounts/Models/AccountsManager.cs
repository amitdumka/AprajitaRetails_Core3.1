using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Accounts.Models
{
    public class AccountsManager
    {
        public void OnInsert(AprajitaRetailsContext db, CashReceipt reciepts) {
            CashTrigger.UpdateCashInHand(db, reciepts.InwardDate, reciepts.Amount);
        }
        public void OnDelete(AprajitaRetailsContext db, CashReceipt reciepts) {
            CashTrigger.UpdateCashInHand(db, reciepts.InwardDate, 0-reciepts.Amount);
        }
        public void OnUpdate(AprajitaRetailsContext db, CashReceipt reciepts) {
            var oldPay = db.CashReceipts.Where(c => c.CashReceiptId == reciepts.CashReceiptId).Select(d => new { d.Amount, d.InwardDate }).FirstOrDefault();

            if (oldPay != null)
            {
                CashTrigger.UpdateCashInHand(db, oldPay.InwardDate, 0 - oldPay.Amount);
            }
            CashTrigger.UpdateCashInHand(db, reciepts.InwardDate, reciepts.Amount);
        }

        public void OnInsert(AprajitaRetailsContext db, Receipt reciepts)
        {
            if (reciepts.PayMode == PaymentModes.Cash)
            {

                CashTrigger.UpdateCashInHand(db, reciepts.RecieptDate, reciepts.Amount);
            }
            //TODO: in future make it more robust
            if (reciepts.PayMode != PaymentModes.Cash)
            {
                CashTrigger.UpdateCashInBank(db, reciepts.RecieptDate, reciepts.Amount);
            }


        }
        public void OnDelete(AprajitaRetailsContext db, Receipt reciepts)
        {
            if (reciepts.PayMode == PaymentModes.Cash)
            {

                CashTrigger.UpdateCashInHand(db, reciepts.RecieptDate, 0 - reciepts.Amount);
            }
            //TODO: in future make it more robust
            if (reciepts.PayMode != PaymentModes.Cash)
            {
                CashTrigger.UpdateCashInBank(db, reciepts.RecieptDate, 0 - reciepts.Amount);
            }
        }
        public void OnUpdate(AprajitaRetailsContext db, Receipt reciepts)
        {

            var oldPay = db.Receipts.Where(c => c.ReceiptId == reciepts.ReceiptId).Select(d => new { d.Amount, d.RecieptDate, d.PayMode }).FirstOrDefault();

            if (oldPay != null)
            {
                if (oldPay.PayMode == PaymentModes.Cash)
                {

                    CashTrigger.UpdateCashInHand(db, oldPay.RecieptDate, 0 - oldPay.Amount);
                }
                //TODO: in future make it more robust
                if (oldPay.PayMode != PaymentModes.Cash)
                {
                    CashTrigger.UpdateCashInBank(db, oldPay.RecieptDate, 0 - oldPay.Amount);
                }
            }


            if (reciepts.PayMode == PaymentModes.Cash)
            {

                CashTrigger.UpdateCashInHand(db, reciepts.RecieptDate, reciepts.Amount);
            }
            //TODO: in future make it more robust
            if (reciepts.PayMode != PaymentModes.Cash)
            {
                CashTrigger.UpdateCashInBank(db, reciepts.RecieptDate, reciepts.Amount);
            }

        }

    }
}
