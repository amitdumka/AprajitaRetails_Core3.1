using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using Microsoft.EntityFrameworkCore;
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



        public void OnInsert(AprajitaRetailsContext db, DueRecoverd objects)
        {
            DuesList duesList = db.DuesLists.Find(objects.DuesListId);
            if (objects.Modes == PaymentModes.Cash)
            {
                CashTrigger.UpdateCashInHand(db, objects.PaidDate, objects.AmountPaid);
            }
            else
            {
                CashTrigger.UpdateCashInBank(db, objects.PaidDate, objects.AmountPaid);
            }

            if (objects.IsPartialPayment)
            {
                duesList.IsPartialRecovery = true;
            }
            else
            {
                duesList.IsRecovered = true;
                duesList.RecoveryDate = objects.PaidDate;
            }
            db.Entry(duesList).State = EntityState.Modified;

        }

        public void OnUpdate(AprajitaRetailsContext db, DueRecoverd objects)
        {
            DueRecoverd dr = db.DueRecoverds.Find(objects.DueRecoverdId);
            DuesList duesList = db.DuesLists.Find(objects.DuesListId);


            if (dr.AmountPaid != objects.AmountPaid)
            {
                //Remove Amount from In-Hands
                if (dr.Modes == PaymentModes.Cash)
                {
                    CashTrigger.UpdateCashInHand(db, objects.PaidDate, 0 - dr.AmountPaid);
                }
                else
                {
                    CashTrigger.UpdateCashInBank(db, objects.PaidDate, 0 - dr.AmountPaid);
                }
                //Add Amount
                if (objects.Modes == PaymentModes.Cash)
                {
                    CashTrigger.UpdateCashInHand(db, objects.PaidDate, objects.AmountPaid);
                }
                else
                {
                    CashTrigger.UpdateCashInBank(db, objects.PaidDate, objects.AmountPaid);
                }
            }

            if (dr.IsPartialPayment != objects.IsPartialPayment)
            {
                if (objects.IsPartialPayment && dr.IsPartialPayment == false)
                {
                    duesList.IsPartialRecovery = true;
                    duesList.IsRecovered = false;
                    duesList.RecoveryDate = null;
                }
                else if (dr.IsPartialPayment && objects.IsPartialPayment == false)
                {
                    duesList.IsPartialRecovery = false;
                    duesList.IsRecovered = true;
                    duesList.RecoveryDate = objects.PaidDate;
                }
            }

            db.Entry(dr).State = EntityState.Modified;
            db.Entry(duesList).State = EntityState.Modified;

        }

        public void OnDelete(AprajitaRetailsContext db, DueRecoverd objects)
        {
            DuesList duesList = db.DuesLists.Find(objects.DuesListId);
            if (objects.Modes == PaymentModes.Cash)
            {
                CashTrigger.UpdateCashInHand(db, objects.PaidDate, 0 - objects.AmountPaid);
            }
            else
            {
                CashTrigger.UpdateCashInBank(db, objects.PaidDate, 0 - objects.AmountPaid);

            }

            if (objects.IsPartialPayment)
            {
                duesList.IsPartialRecovery = false;
            }
            else
            {
                duesList.IsRecovered = false;
                duesList.RecoveryDate = null;
            }
            db.Entry(duesList).State = EntityState.Modified;

        }

    }
}
