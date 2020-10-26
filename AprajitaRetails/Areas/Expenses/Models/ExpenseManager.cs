using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using System.Linq;

namespace AprajitaRetails.Areas.Expenses.Models
{
    public class ExpenseManager
    {
        public void OnInsert(AprajitaRetailsContext db, CashPayment payment)
        {

            CashTrigger.UpDateCashOutHand(db, payment.PaymentDate, payment.Amount);
        }
        public void OnDelete(AprajitaRetailsContext db, CashPayment payment)
        {
            CashTrigger.UpDateCashOutHand(db, payment.PaymentDate, 0 - payment.Amount);
        }
        public void OnUpdate(AprajitaRetailsContext db, CashPayment payment)
        {

            var oldPay = db.CashPayments.Where(c => c.CashPaymentId == payment.CashPaymentId).Select(d => new { d.Amount, d.PaymentDate }).FirstOrDefault();

            if (oldPay != null)
            {
                CashTrigger.UpDateCashOutHand(db, oldPay.PaymentDate, 0 - oldPay.Amount);
            }
            CashTrigger.UpDateCashOutHand(db, payment.PaymentDate, payment.Amount);

        }


        public void OnInsert(AprajitaRetailsContext db, PettyCashExpense payment)
        {

            CashTrigger.UpDateCashOutHand(db, payment.ExpDate, payment.Amount);
        }
        public void OnDelete(AprajitaRetailsContext db, PettyCashExpense payment)
        {
            CashTrigger.UpDateCashOutHand(db, payment.ExpDate, 0 - payment.Amount);
        }
        public void OnUpdate(AprajitaRetailsContext db, PettyCashExpense payment)
        {
            var oldPay = db.PettyCashExpenses.Where(c => c.PettyCashExpenseId == payment.PettyCashExpenseId).Select(d => new { d.Amount, d.ExpDate }).FirstOrDefault();


            if (oldPay != null)
            {
                CashTrigger.UpDateCashOutHand(db, oldPay.ExpDate, 0 - oldPay.Amount);
            }
            CashTrigger.UpDateCashOutHand(db, payment.ExpDate, payment.Amount);

        }

        public void OnInsert(AprajitaRetailsContext db, Payment payment)
        {
            if (payment.PayMode == PaymentMode.Cash)
            {

                CashTrigger.UpDateCashOutHand(db, payment.PayDate, payment.Amount);
            }
            //TODO: in future make it more robust
            if (payment.PayMode != PaymentMode.Cash)
            {
                CashTrigger.UpDateCashOutBank(db, payment.PayDate, payment.Amount);
            }


        }
        public void OnDelete(AprajitaRetailsContext db, Payment payment)
        {
            if (payment.PayMode == PaymentMode.Cash)
            {

                CashTrigger.UpDateCashOutHand(db, payment.PayDate, 0 - payment.Amount);
            }
            //TODO: in future make it more robust
            if (payment.PayMode != PaymentMode.Cash)
            {
                CashTrigger.UpDateCashOutBank(db, payment.PayDate, 0 - payment.Amount);
            }
        }
        public void OnUpdate(AprajitaRetailsContext db, Payment payment)
        {

            var oldPay = db.Payments.Where(c => c.PaymentId == payment.PaymentId).Select(d => new { d.Amount, d.PayDate, d.PayMode }).FirstOrDefault();

            if (oldPay != null)
            {
                if (oldPay.PayMode == PaymentMode.Cash)
                {

                    CashTrigger.UpDateCashOutHand(db, oldPay.PayDate, 0 - oldPay.Amount);
                }
                //TODO: in future make it more robust
                if (oldPay.PayMode != PaymentMode.Cash)
                {
                    CashTrigger.UpDateCashOutBank(db, oldPay.PayDate, 0 - oldPay.Amount);
                }
            }


            if (payment.PayMode == PaymentMode.Cash)
            {

                CashTrigger.UpDateCashOutHand(db, payment.PayDate, payment.Amount);
            }
            //TODO: in future make it more robust
            if (payment.PayMode != PaymentMode.Cash)
            {
                CashTrigger.UpDateCashOutBank(db, payment.PayDate, payment.Amount);
            }

        }

        public void OnInsert(AprajitaRetailsContext db, Expense payment)
        {
            if (payment.PayMode == PaymentMode.Cash)
            {

                CashTrigger.UpDateCashOutHand(db, payment.ExpDate, payment.Amount);
            }
            //TODO: in future make it more robust
            if (payment.PayMode != PaymentMode.Cash && payment.PayMode != PaymentMode.Others)
            {
                CashTrigger.UpDateCashOutBank(db, payment.ExpDate, payment.Amount);
            }
            if (payment.PayMode == PaymentMode.Others)
            {
                CashTrigger.UpdateSuspenseAccount(db, payment.ExpDate, payment.Amount, true, "ExpensesId:" + payment.ExpenseId, false, true);
            }


        }
        public void OnDelete(AprajitaRetailsContext db, Expense payment)
        {
            if (payment.PayMode == PaymentMode.Cash)
            {

                CashTrigger.UpDateCashOutHand(db, payment.ExpDate, 0 - payment.Amount);
            }
            //TODO: in future make it more robust
            if (payment.PayMode != PaymentMode.Cash && payment.PayMode != PaymentMode.Others)
            {
                CashTrigger.UpDateCashOutBank(db, payment.ExpDate, 0 - payment.Amount);
            }
            if (payment.PayMode == PaymentMode.Others)
            {
                CashTrigger.UpdateSuspenseAccount(db, payment.ExpDate, payment.Amount, true, "ExpensesId:" + payment.ExpenseId, true, true);
            }
        }
        public void OnUpdate(AprajitaRetailsContext db, Expense payment)
        {

            var oldPay = db.Payments.Where(c => c.PaymentId == payment.ExpenseId).Select(d => new { d.Amount, d.PayDate, d.PayMode }).FirstOrDefault();

            if (oldPay != null)
            {
                if (oldPay.PayMode == PaymentMode.Cash)
                {

                    CashTrigger.UpDateCashOutHand(db, payment.ExpDate, 0 - payment.Amount);
                }
                //TODO: in future make it more robust
                if (oldPay.PayMode != PaymentMode.Cash && payment.PayMode != PaymentMode.Others)
                {
                    CashTrigger.UpDateCashOutBank(db, payment.ExpDate, 0 - payment.Amount);
                }
                if (oldPay.PayMode == PaymentMode.Others)
                {
                    CashTrigger.UpdateSuspenseAccount(db, payment.ExpDate, payment.Amount, true, "ExpensesId:" + payment.ExpenseId, true, true);
                }
            }

            if (payment.PayMode == PaymentMode.Cash)
            {

                CashTrigger.UpDateCashOutHand(db, payment.ExpDate, payment.Amount);
            }
            //TODO: in future make it more robust
            if (payment.PayMode != PaymentMode.Cash && payment.PayMode != PaymentMode.Others)
            {
                CashTrigger.UpDateCashOutBank(db, payment.ExpDate, payment.Amount);
            }
            if (payment.PayMode == PaymentMode.Others)
            {
                CashTrigger.UpdateSuspenseAccount(db, payment.ExpDate, payment.Amount, true, "ExpensesId:" + payment.ExpenseId, false, true);
            }

        }
    }
}
