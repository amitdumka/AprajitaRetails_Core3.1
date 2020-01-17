using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Expenses.Models
{
    public class ExpenseManager
    {
        public void OnInsert(AprajitaRetailsContext db, CashPayment payment) {

            CashTrigger.UpdateCashInHand(db, payment.PaymentDate, payment.Amount);
        }
        public void OnDelete(AprajitaRetailsContext db, CashPayment payment) {
            CashTrigger.UpdateCashInHand(db, payment.PaymentDate, 0 - payment.Amount);
        }
        public void OnUpdate(AprajitaRetailsContext db, CashPayment payment) {

            var oldPay = db.CashPayments.Find(payment.CashPaymentId);
            if (oldPay != null)
            {
                CashTrigger.UpdateCashInHand(db, oldPay.PaymentDate, 0 - oldPay.Amount);
            }
            CashTrigger.UpdateCashInHand(db, payment.PaymentDate, payment.Amount);

        }
    }
}
