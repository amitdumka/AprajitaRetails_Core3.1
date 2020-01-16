using AprajitaRetails.Data;
using AprajitaRetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Triggers
{
    public class PayRollManager
    {
        public void OnInsert(AprajitaRetailsContext db, StaffAdvanceReceipt salPayment)
        {
            UpDateStaffReciptAmount(db, salPayment, false);
        }
        public void OnInsert(AprajitaRetailsContext db, StaffAdvancePayment salPayment)
        {
           UpDateStaffPaymentAmount(db, salPayment, false);
        }
        public void OnInsert(AprajitaRetailsContext db, SalaryPayment salPayment) {
            UpDateSalaryAmount(db, salPayment, false);
        }
                
        public void OnDelete(AprajitaRetailsContext db, SalaryPayment salPayment) {
            UpDateSalaryAmount(db, salPayment, true);
        }
        public void OnDelete(AprajitaRetailsContext db, StaffAdvanceReceipt salPayment)
        {
            UpDateStaffReciptAmount(db, salPayment, true);
        }
        public void OnDelete(AprajitaRetailsContext db, StaffAdvancePayment salPayment)
        {
            UpDateStaffPaymentAmount(db, salPayment, true);
        }
        public void OnUpdate(AprajitaRetailsContext db, SalaryPayment salPayment) { }
        public void OnUpdate(AprajitaRetailsContext db, StaffAdvancePayment salPayment) { }
        public void OnUpdate(AprajitaRetailsContext db, StaffAdvanceReceipt salPayment) { }

        private void UpDateSalaryAmount(AprajitaRetailsContext db, SalaryPayment salPayment, bool IsEdit)
        {
            if (IsEdit)
            {
                if (salPayment.PayMode == PayModes.Cash)
                {
                    CashTrigger.UpDateCashOutHand(db, salPayment.PaymentDate, 0 - salPayment.Amount);

                }
                //TODO: in future make it more robust
                if (salPayment.PayMode != PayModes.Cash && salPayment.PayMode != PayModes.Coupons && salPayment.PayMode != PayModes.Points)
                {
                    CashTrigger.UpDateCashOutBank(db, salPayment.PaymentDate, 0 - (salPayment.Amount - salPayment.Amount));
                }
            }
            else
            {
                if (salPayment.PayMode == PayModes.Cash )
                {
                    CashTrigger.UpDateCashOutHand(db, salPayment.PaymentDate, salPayment.Amount);

                }
                //TODO: in future make it more robust
                if (salPayment.PayMode != PayModes.Cash && salPayment.PayMode != PayModes.Coupons && salPayment.PayMode != PayModes.Points)
                {
                    CashTrigger.UpDateCashOutBank(db, salPayment.PaymentDate, salPayment.Amount - salPayment.Amount);
                }
            }

        }
        private void UpDateStaffPaymentAmount(AprajitaRetailsContext db, StaffAdvancePayment salPayment, bool IsEdit)
        {
            if (IsEdit)
            {
                if (salPayment.PayMode == PayModes.Cash)
                {
                    CashTrigger.UpDateCashOutHand(db, salPayment.PaymentDate, 0 - salPayment.Amount);

                }
                //TODO: in future make it more robust
                if (salPayment.PayMode != PayModes.Cash && salPayment.PayMode != PayModes.Coupons && salPayment.PayMode != PayModes.Points)
                {
                    CashTrigger.UpDateCashOutBank(db, salPayment.PaymentDate, 0 - (salPayment.Amount - salPayment.Amount));
                }
            }
            else
            {
                if (salPayment.PayMode == PayModes.Cash)
                {
                    CashTrigger.UpDateCashOutHand(db, salPayment.PaymentDate, salPayment.Amount);

                }
                //TODO: in future make it more robust
                if (salPayment.PayMode != PayModes.Cash && salPayment.PayMode != PayModes.Coupons && salPayment.PayMode != PayModes.Points)
                {
                    CashTrigger.UpDateCashOutBank(db, salPayment.PaymentDate, salPayment.Amount - salPayment.Amount);
                }
            }

        }
        private void UpDateStaffReciptAmount(AprajitaRetailsContext db, StaffAdvanceReceipt salPayment, bool IsEdit)
        {
            if (IsEdit)
            {
                if (salPayment.PayMode == PayModes.Cash)
                {
                    CashTrigger.UpdateCashInHand(db, salPayment.ReceiptDate, 0 - salPayment.Amount);

                }
                //TODO: in future make it more robust
                if (salPayment.PayMode != PayModes.Cash && salPayment.PayMode != PayModes.Coupons && salPayment.PayMode != PayModes.Points)
                {
                    CashTrigger.UpdateCashInBank(db, salPayment.ReceiptDate, 0 - (salPayment.Amount - salPayment.Amount));
                }
            }
            else
            {
                if (salPayment.PayMode == PayModes.Cash)
                {
                    CashTrigger.UpdateCashInHand(db, salPayment.ReceiptDate, salPayment.Amount);

                }
                //TODO: in future make it more robust
                if (salPayment.PayMode != PayModes.Cash && salPayment.PayMode != PayModes.Coupons && salPayment.PayMode != PayModes.Points)
                {
                    CashTrigger.UpdateCashInBank(db, salPayment.ReceiptDate, salPayment.Amount - salPayment.Amount);
                }
            }

        }




    }
}
