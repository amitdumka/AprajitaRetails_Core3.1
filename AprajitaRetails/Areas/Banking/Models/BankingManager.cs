using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Banking.Models
{
    public class BankingManager
    {
        public void OnInsert(AprajitaRetailsContext db, BankWithdrawal objectName)
        {
            CashTrigger.UpdateCashInHand(db, objectName.DepoDate, objectName.Amount);
            CashTrigger.UpDateCashOutBank(db, objectName.DepoDate, objectName.Amount);
        }
        public void OnDelete(AprajitaRetailsContext db, BankWithdrawal objectName)
        {
            CashTrigger.UpdateCashInHand(db, objectName.DepoDate, 0 - objectName.Amount);
            CashTrigger.UpDateCashOutBank(db, objectName.DepoDate, 0 - objectName.Amount);
        }
        public void OnUpdate(AprajitaRetailsContext db, BankWithdrawal objectName)
        {
            var old = db.BankWithdrawals.Where(c => c.BankWithdrawalId == objectName.BankWithdrawalId).Select(d => new { d.Amount, d.DepoDate }).FirstOrDefault();
            if (old != null)
            {
                CashTrigger.UpdateCashInHand(db, old.DepoDate, 0 - old.Amount);
                CashTrigger.UpDateCashOutBank(db, old.DepoDate, 0 - old.Amount);
            }
            CashTrigger.UpdateCashInHand(db, objectName.DepoDate, objectName.Amount);
            CashTrigger.UpDateCashOutBank(db, objectName.DepoDate, objectName.Amount);
        }


        public void OnInsert(AprajitaRetailsContext db, BankDeposit objectName)
        {
            
            if (objectName.PayMode == BankPayModes.Cash)
            {
                CashTrigger.UpDateCashOutHand(db, objectName.DepoDate, objectName.Amount);
                CashTrigger.UpdateCashInBank(db, objectName.DepoDate, objectName.Amount);
            }
            //TODO: in future make it more robust
            if (objectName.PayMode != BankPayModes.Cash)
            {
                
                CashTrigger.UpdateCashInBank(db, objectName.DepoDate, objectName.Amount);
            }


        }
        public void OnDelete(AprajitaRetailsContext db, BankDeposit objectName)
        {
            if (objectName.PayMode == BankPayModes.Cash)
            {

                CashTrigger.UpDateCashOutHand(db, objectName.DepoDate, 0-objectName.Amount);
                CashTrigger.UpdateCashInBank(db, objectName.DepoDate, 0-objectName.Amount);
            }
            //TODO: in future make it more robust
            if (objectName.PayMode != BankPayModes.Cash)
            {
                CashTrigger.UpdateCashInBank(db, objectName.DepoDate, 0-objectName.Amount);
            }
        }
        public void OnUpdate(AprajitaRetailsContext db, BankDeposit objectName)
        {

            var old = db.BankDeposits.Where(c => c.BankDepositId == objectName.BankDepositId).Select(d => new { d.Amount, d.DepoDate, d.PayMode }).FirstOrDefault();

            if (old != null)
            {
                if (old.PayMode == BankPayModes.Cash)
                {

                    CashTrigger.UpDateCashOutHand(db, old.DepoDate, 0 - old.Amount);
                    CashTrigger.UpdateCashInBank(db, old.DepoDate, 0 - old.Amount);
                }
                
                if (old.PayMode != BankPayModes.Cash)
                {
                    CashTrigger.UpdateCashInBank(db, old.DepoDate, 0 - old.Amount);
                }
            }

            if (objectName.PayMode == BankPayModes.Cash)
            {

                CashTrigger.UpDateCashOutHand(db, objectName.DepoDate, objectName.Amount);
                CashTrigger.UpdateCashInBank(db, objectName.DepoDate, objectName.Amount);
            }
            //TODO: in future make it more robust
            if (objectName.PayMode != BankPayModes.Cash)
            {
                CashTrigger.UpdateCashInBank(db, objectName.DepoDate, objectName.Amount);
            }

        }

    }
}
