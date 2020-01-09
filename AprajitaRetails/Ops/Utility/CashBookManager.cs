using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using AprajitaRetails.Ops.Triggers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Utility
{
    public class CashBookManager
    {
        public List<CashBook> GetMontlyCashBook(AprajitaRetailsContext db, DateTime date)
        {

            List<CashBook> book = new List<CashBook>();


            DateTime oDate = new DateTime(date.Year, date.Month, 1);

            decimal OpnBal = 0;
            decimal ColBal = 0;
            CashWork worker = new CashWork();
            try
            {
                ColBal = worker.GetClosingBalance(db, oDate.AddDays(-1));
                OpnBal = (decimal?)db.CashInHands.Where(c => (c.CIHDate) == (oDate)).FirstOrDefault().OpenningBalance ?? 0;
                if (OpnBal != ColBal) OpnBal = ColBal;


            }
            catch (Exception)
            {
                OpnBal = ColBal;
            }
            //income
            var dSale = db.DailySales.Where(c => c.PayMode == PayModes.Cash && (c.SaleDate).Month == (date).Month).OrderBy(c => c.SaleDate);//ok
            var dRec = db.Receipts.Where(c => c.PayMode == PaymentModes.Cash && (c.RecieptDate).Month == (date).Month).OrderBy(c => c.RecieptDate);//ok
            var dCashRec = db.CashReceipts.Where(c => (c.InwardDate).Month == (date).Month).OrderBy(c => c.InwardDate);//ok
            var dSRec = db.StaffAdvanceReceipts.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.ReceiptDate).Month == (date).Month).OrderBy(c => c.ReceiptDate);//ok
            var dWit = db.Withdrawals.Include(C => C.Account).Where(c => (c.DepoDate).Month == (date).Month).OrderBy(c => c.DepoDate);
            var dTalRec = db.TailoringStaffAdvanceReceipts.Include(c => c.Employee).Where(c => c.PayMode == PayModes.Cash && (c.ReceiptDate).Month == (date).Month).OrderBy(c => c.ReceiptDate);//ok
            foreach (var item in dTalRec)
            {
                CashBook b = new CashBook() { EDate = item.ReceiptDate, CashIn = item.Amount, Particulars = item.Employee.StaffName, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in dSale)
            {
                CashBook b = new CashBook() { EDate = item.SaleDate, CashIn = item.Amount, Particulars = item.InvNo, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in dRec)
            {
                CashBook b = new CashBook() { EDate = item.RecieptDate, CashIn = item.Amount, Particulars = item.ReceiptFrom, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in dCashRec)
            {
                CashBook b = new CashBook() { EDate = item.InwardDate, CashIn = item.Amount, Particulars = item.ReceiptFrom, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in dSRec)
            {
                CashBook b = new CashBook() { EDate = item.ReceiptDate, CashIn = item.Amount, Particulars = item.Employee.StaffName, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in dWit)
            {
                CashBook b = new CashBook() { EDate = item.DepoDate, CashIn = item.Amount, Particulars = "Bank=> " + item.Account.Account, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }


            //Expenses

            var eCPay = db.CashPayments.Where(c => (c.PaymentDate).Month == (date).Month).OrderBy(c => c.PaymentDate);//ok
            var ePay = db.Payments.Where(c => c.PayMode == PaymentModes.Cash && (c.PayDate).Month == (date).Month).OrderBy(c => c.PayDate);
            var eStPay = db.StaffAdvancePayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate).Month == (date).Month).OrderBy(c => c.PaymentDate);
            var eSal = db.Salaries.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate).Month == (date).Month).OrderBy(c => c.PaymentDate);
            var eexp = db.Expenses.Where(c => c.PayMode == PaymentModes.Cash && (c.ExpDate).Month == (date).Month).OrderBy(c => c.ExpDate);
            var eDepo = db.BankDeposits.Include(C => C.Account).Where(c => (c.DepoDate).Month == (date).Month).OrderBy(c => c.DepoDate);
            var eDue = db.DuesLists.Include(e => e.DailySale).Where(c => c.IsRecovered == false && (c.DailySale.SaleDate).Month == (date).Month).OrderBy(c => c.DailySale.SaleDate);
            var eCashEx = db.CashExpenses.Where(c => (c.ExpDate).Month == (date).Month).OrderBy(c => c.ExpDate);

            var eTalSal = db.TailoringSalaries.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate).Month == (date).Month).OrderBy(c => c.PaymentDate);
            var eTalStPay = db.TailoringStaffAdvancePayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate).Month == (date).Month).OrderBy(c => c.PaymentDate);

            foreach (var item in eTalStPay)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in eTalSal)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }


            foreach (var item in eexp)
            {
                CashBook b = new CashBook() { EDate = item.ExpDate, CashIn = 0, Particulars = item.Particulars, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in eDepo)
            {
                CashBook b = new CashBook() { EDate = item.DepoDate, CashIn = 0, Particulars = "Bank Depo=> " + item.Account.Account, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in eCashEx)
            {
                CashBook b = new CashBook() { EDate = item.ExpDate, CashIn = 0, Particulars = item.Particulars, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in eDue)
            {
                CashBook b = new CashBook() { EDate = item.DailySale.SaleDate, CashIn = 0, Particulars = "Dues=>" + item.DailySale.InvNo, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }


            foreach (var item in eCPay)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.PaidTo, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in ePay)
            {
                CashBook b = new CashBook() { EDate = item.PayDate, CashIn = 0, Particulars = item.PaymentPartry, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in eStPay)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in eSal)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }
            return CorrectBalCashBook(book, OpnBal);

        }

        public List<CashBook> GetDailyCashBook(AprajitaRetailsContext db, DateTime date)
        {

            List<CashBook> book = new List<CashBook>();

            decimal OpnBal = 0;
            decimal ColBal = 0;
            CashWork worker = new CashWork();
            try
            {
                ColBal = worker.GetClosingBalance(db, date.AddDays(-1));
                OpnBal = (decimal?)db.CashInHands.Where(c => c.CIHDate.Date == date.Date).FirstOrDefault().OpenningBalance ?? 0;
                if (ColBal != OpnBal) OpnBal = ColBal;

            }
            catch (Exception)
            {
                OpnBal = ColBal;
            }


            //income
            var dSale = db.DailySales.Where(c => c.PayMode == PayModes.Cash && (c.SaleDate.Date) == (date.Date)).OrderBy(c => c.SaleDate);//ok
            var dRec = db.Receipts.Where(c => c.PayMode == PaymentModes.Cash && (c.RecieptDate.Date) == (date.Date)).OrderBy(c => c.RecieptDate);//ok
            var dCashRec = db.CashReceipts.Where(c => (c.InwardDate.Date) == (date.Date)).OrderBy(c => c.InwardDate);//ok
            var dSRec = db.StaffAdvanceReceipts.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.ReceiptDate.Date) == (date.Date)).OrderBy(c => c.ReceiptDate);//ok
            var dWit = db.Withdrawals.Include(C => C.Account).Where(c => (c.DepoDate.Date) == (date.Date)).OrderBy(c => c.DepoDate);
            var dTalRec = db.TailoringStaffAdvanceReceipts.Include(c => c.Employee).Where(c => c.PayMode == PayModes.Cash && (c.ReceiptDate.Date) == (date.Date)).OrderBy(c => c.ReceiptDate);//ok

            foreach (var item in dTalRec)
            {
                CashBook b = new CashBook() { EDate = item.ReceiptDate, CashIn = item.Amount, Particulars = item.Employee.StaffName, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in dSale)
            {
                CashBook b = new CashBook() { EDate = item.SaleDate, CashIn = item.Amount, Particulars = item.InvNo, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in dRec)
            {
                CashBook b = new CashBook() { EDate = item.RecieptDate, CashIn = item.Amount, Particulars = item.ReceiptFrom, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in dCashRec)
            {
                CashBook b = new CashBook() { EDate = item.InwardDate, CashIn = item.Amount, Particulars = item.ReceiptFrom, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in dSRec)
            {
                CashBook b = new CashBook() { EDate = item.ReceiptDate, CashIn = item.Amount, Particulars = item.Employee.StaffName, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }




            foreach (var item in dWit)
            {
                CashBook b = new CashBook() { EDate = item.DepoDate, CashIn = item.Amount, Particulars = item.Account.Account, CashOut = 0, CashBalance = 0 };
                book.Add(b);
            }




            //Expenses

            var eCPay = db.CashPayments.Where(c => (c.PaymentDate.Date) == (date.Date)).OrderBy(c => c.PaymentDate);//ok
            var ePay = db.Payments.Where(c => c.PayMode == PaymentModes.Cash && (c.PayDate.Date) == (date.Date)).OrderBy(c => c.PayDate.Date);
            var eStPay = db.StaffAdvancePayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate.Date) == (date.Date)).OrderBy(c => c.PaymentDate);
            var eSal = db.Salaries.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate.Date) == (date.Date)).OrderBy(c => c.PaymentDate);
            var eexp = db.Expenses.Where(c => c.PayMode == PaymentModes.Cash && (c.ExpDate.Date) == (date.Date)).OrderBy(c => c.ExpDate);
            var eDepo = db.BankDeposits.Include(C => C.Account).Where(c => (c.DepoDate.Date) == (date.Date)).OrderBy(c => c.DepoDate);
            var eDue = db.DuesLists.Include(c => c.DailySale).Where(c => c.IsRecovered == false && (c.DailySale.SaleDate.Date) == (date.Date)).OrderBy(c => c.DailySale.SaleDate);
            var eCashEx = db.CashExpenses.Where(c => (c.ExpDate.Date) == (date.Date)).OrderBy(c => c.ExpDate);

            var eTalSal = db.TailoringSalaries.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate) == (date.Date)).OrderBy(c => c.PaymentDate);
            var eTalStPay = db.TailoringStaffAdvancePayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate.Date) == (date.Date)).OrderBy(c => c.PaymentDate);

            foreach (var item in eTalStPay)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in eTalSal)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }


            foreach (var item in eexp)
            {
                CashBook b = new CashBook() { EDate = item.ExpDate, CashIn = 0, Particulars = item.PaidTo, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in eDepo)
            {
                CashBook b = new CashBook() { EDate = item.DepoDate, CashIn = 0, Particulars = "Bank Depo" + item.Account.Account, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in eCashEx)
            {
                CashBook b = new CashBook() { EDate = item.ExpDate, CashIn = 0, Particulars = item.PaidTo, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }
            foreach (var item in eDue)
            {
                CashBook b = new CashBook() { EDate = item.DailySale.SaleDate, CashIn = 0, Particulars = "Dues " + item.DailySale.InvNo, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }


            foreach (var item in eCPay)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.PaidTo, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in ePay)
            {
                CashBook b = new CashBook() { EDate = item.PayDate, CashIn = 0, Particulars = item.PaymentPartry, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in eStPay)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            foreach (var item in eSal)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }


            return CorrectBalCashBook(book, OpnBal);

        }

        private List<CashBook> CorrectBalCashBook(List<CashBook> books, decimal OpnBal)
        {
            IEnumerable<CashBook> orderBook = books.OrderBy(c => c.EDate);

            decimal bal = OpnBal;
            foreach (var item in orderBook)
            {
                item.CashBalance = bal + item.CashIn - item.CashOut;
                bal = item.CashBalance;
            }
            return orderBook.ToList();


        }


        // Correct Cash In Hand In Database
        /// <summary>
        /// Delete Cash In Hand For Particular Month
        /// </summary>
        /// <param name="db"></param>
        /// <param name="date"></param>
        private  void DeleteCashInHandForMonth(AprajitaRetailsContext db, DateTime date)
        {
            var cih = db.CashInHands.Where(c => (c.CIHDate).Month == (date).Month);
            db.CashInHands.RemoveRange(cih);
            db.SaveChanges();
        }

        private  List<CashBook> CreateCashInHands(AprajitaRetailsContext db, List<CashBook> books)
        {

            IEnumerable<CashBook> orderBook = books.OrderBy(c => c.EDate);

            CashInHand cashInHand = null;
            DateTime startDate = orderBook.First().EDate;

            //Remove CashInHand from Database
            DeleteCashInHandForMonth(db, startDate);

            foreach (var item in orderBook)
            {
                if (cashInHand == null)
                {
                    db.SaveChanges();
                    cashInHand = new CashInHand()
                    {
                        CIHDate = item.EDate,
                        OpenningBalance = 0,
                        CashIn = item.CashIn,
                        CashOut = item.CashOut,
                        ClosingBalance = 0
                    };
                }
                else if (startDate != item.EDate && cashInHand != null)
                {
                    db.CashInHands.Add(cashInHand);
                    db.SaveChanges();
                    cashInHand = new CashInHand()
                    {
                        CIHDate = item.EDate,
                        OpenningBalance = 0,
                        CashIn = item.CashIn,
                        CashOut = item.CashOut,
                        ClosingBalance = 0
                    };
                    startDate = item.EDate;
                }
                else
                {
                    cashInHand.CashIn += item.CashIn;
                    cashInHand.CashOut += item.CashOut;
                }


            }
            db.CashInHands.Add(cashInHand);
            db.SaveChanges();

            return orderBook.ToList();
        }

       
        public  List<CashBook> CorrectCashInHands(AprajitaRetailsContext db, DateTime date, bool IsDay=false)
        {
            List<CashBook> cashBookList = null;
            if (IsDay) 
                cashBookList = GetDailyCashBook(db, date);
            else 
                cashBookList = GetMontlyCashBook(db, date);

            cashBookList = CreateCashInHands(db, cashBookList);

            new CashWork().CashInHandCorrectionForMonth(db, date);
            
            return cashBookList;
        }


    }
}
