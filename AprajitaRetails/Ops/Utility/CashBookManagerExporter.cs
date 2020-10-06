﻿using System;
using System.Collections.Generic;
using System.Linq;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using AprajitaRetails.Ops.Exporter;
using AprajitaRetails.Ops.Triggers;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Ops.Utility
{
    public class CashBookManagerExporter
    {

        private readonly int StoreId = 1;

        public CashBookManagerExporter(int storeId)
        {
            StoreId = storeId;
        }
        //StoreBased Action Reviewed
        public List<CashInHand> CashInHandCorrectionForMonth(AprajitaRetailsContext db, DateTime forDate, int Store)
        {
            IEnumerable<CashInHand> cashs = db.CashInHands.Where(c => c.CIHDate.Month == forDate.Month && c.CIHDate.Year==forDate.Year && c.StoreId == Store).OrderBy(c => c.CIHDate);

            decimal cBal = 0;

            if (cashs != null && cashs.Any())
            {
                cBal = GetClosingBalance(db, cashs.First().CIHDate.AddDays(-1),Store);

                if (cBal == 0)
                    cBal = cashs.First().OpenningBalance;

                foreach (var cash in cashs)
                {
                    cash.OpenningBalance = cBal;

                    cash.ClosingBalance = cash.OpenningBalance + cash.CashIn - cash.CashOut;
                    cBal = cash.ClosingBalance;

                    db.Entry(cash).State = EntityState.Modified;
                }
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return cashs.ToList();
                    // Log.Info("CashInHand Correction failed");
                }
            }
            return cashs.ToList();
        }

        //StoreBased Action Reviewed
        public List<CashBook> CorrectCashInHands(AprajitaRetailsContext db, DateTime date, string fileName, int Store, bool IsDay = false)
        {
            List<CashBook> cashBookList;
            if (IsDay)
                cashBookList = GetDailyCashBook(db, date,Store);
            else
                cashBookList = GetMontlyCashBook(db, date,Store);

            ExcelExporter.CashBookExporter(fileName, cashBookList, "CashBook", Store);

            List<CashInHand> InHandList = CreateCashInHands(db, cashBookList,Store);

            ExcelExporter.CashInHandExporter(fileName, InHandList, "New CashInhand", Store);

            InHandList = CashInHandCorrectionForMonth(db, date, Store);

            ExcelExporter.CashInHandExporter(fileName, InHandList, "Updated CashInhand",Store);
            return cashBookList;
        }

        //StoreBased Action Reviewed
        public decimal GetClosingBalance(AprajitaRetailsContext db, DateTime forDate, int Store, bool IsBank = false)
        {
            if (IsBank)
            {
                var bal = db.CashInBanks.Where(c => c.CIBDate.Date == forDate.Date && c.StoreId == Store).Select(c => new { c.CashIn, c.CashOut, c.OpenningBalance }).FirstOrDefault();
                if (bal != null)
                {
                    return (bal.OpenningBalance + bal.CashIn - bal.CashOut);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                var bal = db.CashInHands.Where(c => c.CIHDate.Date == forDate.Date && c.StoreId == Store).Select(c => new { c.CashIn, c.CashOut, c.OpenningBalance }).FirstOrDefault();

                if (bal != null)
                {
                    return (bal.OpenningBalance + bal.CashIn - bal.CashOut);
                }
                else
                {
                    return 0;
                }
            }
        }
        //StoreBased Action Reviewed
        public List<CashBook> GetDailyCashBook(AprajitaRetailsContext db, DateTime date,int Store)
        {
            List<CashBook> book = new List<CashBook>();

            decimal OpnBal = 0;
            decimal ColBal = 0;
            CashWork worker = new CashWork();
            try
            {
                ColBal = worker.GetClosingBalance(db, date.AddDays(-1), Store);
                OpnBal = (decimal?)db.CashInHands.Where(c => c.CIHDate.Date == date.Date && c.StoreId == Store).FirstOrDefault().OpenningBalance ?? 0;
                if (ColBal != OpnBal)
                    OpnBal = ColBal;
            }
            catch (Exception)
            {
                OpnBal = ColBal;
            }

            //income
            var dSale = db.DailySales.Where(c => c.PayMode == PayModes.Cash && c.StoreId == Store && (c.SaleDate.Date) == (date.Date)).OrderBy(c => c.SaleDate);//ok
            var dRec = db.Receipts.Where(c => c.PayMode == PaymentModes.Cash && c.StoreId == Store && (c.RecieptDate.Date) == (date.Date)).OrderBy(c => c.RecieptDate);//ok
            var dCashRec = db.CashReceipts.Where(c => (c.InwardDate.Date) == (date.Date) && c.StoreId == Store).OrderBy(c => c.InwardDate);//ok
            var dSRec = db.StaffAdvanceReceipts.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && c.StoreId == Store && (c.ReceiptDate.Date) == (date.Date)).OrderBy(c => c.ReceiptDate);//ok
            var dWit = db.BankWithdrawals.Include(C => C.Account).Where(c => (c.DepoDate.Date) == (date.Date) && c.StoreId == Store).OrderBy(c => c.DepoDate);
           
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

            var eCPay = db.CashPayments.Where(c => (c.PaymentDate.Date) == (date.Date) && c.StoreId == Store).OrderBy(c => c.PaymentDate);//ok
            var ePay = db.Payments.Where(c => c.PayMode == PaymentModes.Cash && c.StoreId == Store && (c.PayDate.Date) == (date.Date)).OrderBy(c => c.PayDate.Date);
//            var eStPay = db.StaffAdvancePayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && c.StoreId == Store && (c.PaymentDate.Date) == (date.Date)).OrderBy(c => c.PaymentDate);
            var eSal = db.SalaryPayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && c.StoreId == Store && (c.PaymentDate.Date) == (date.Date)).OrderBy(c => c.PaymentDate);
            var eexp = db.Expenses.Where(c => c.PayMode == PaymentModes.Cash && (c.ExpDate.Date) == (date.Date) && c.StoreId == Store).OrderBy(c => c.ExpDate);
            var eDepo = db.BankDeposits.Include(C => C.Account).Where(c => (c.DepoDate.Date) == (date.Date) && c.StoreId == Store).OrderBy(c => c.DepoDate);
            var eDue = db.DuesLists.Include(c => c.DailySale).Where(c => c.IsRecovered == false && c.StoreId == Store && (c.DailySale.SaleDate.Date) == (date.Date)).OrderBy(c => c.DailySale.SaleDate);
            var eCashEx = db.PettyCashExpenses.Where(c => (c.ExpDate.Date) == (date.Date) && c.StoreId == Store).OrderBy(c => c.ExpDate);

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

            //foreach (var item in eStPay)
            //{
            //    CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
            //    book.Add(b);
            //}

            foreach (var item in eSal)
            {
                CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
                book.Add(b);
            }

            return CorrectBalCashBook(book, OpnBal);
        }

        //StoreBased Action Reviewed
        public List<CashBook> GetMontlyCashBook(AprajitaRetailsContext db, DateTime date, int Store)
        {
            List<CashBook> book = new List<CashBook>();

            DateTime oDate = new DateTime(date.Year, date.Month, 1);

            decimal OpnBal = 0;
            decimal ColBal = 0;
            CashWork worker = new CashWork();
            try
            {
                ColBal = worker.GetClosingBalance(db, oDate.AddDays(-1),Store);
                OpnBal = (decimal?)db.CashInHands.Where(c => (c.CIHDate) == (oDate) && c.StoreId==Store).FirstOrDefault().OpenningBalance ?? 0;
                if (OpnBal != ColBal)
                    OpnBal = ColBal;
            }
            catch (Exception)
            {
                OpnBal = ColBal;
            }

            //income
            var dSale = db.DailySales.Where(c => c.PayMode == PayModes.Cash && c.SaleDate.Month == date.Month && c.SaleDate.Year == date.Year && c.StoreId==Store).OrderBy(c => c.SaleDate);
            var dRec = db.Receipts.Where(c => c.PayMode == PaymentModes.Cash && c.RecieptDate.Month == date.Month && c.RecieptDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.RecieptDate);
            var dCashRec = db.CashReceipts.Where(c => c.InwardDate.Month == date.Month && c.InwardDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.InwardDate);
            var dSRec = db.StaffAdvanceReceipts.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && c.ReceiptDate.Year == date.Year && c.ReceiptDate.Month == date.Month && c.StoreId == Store).OrderBy(c => c.ReceiptDate);
            var dWit = db.BankWithdrawals.Include(C => C.Account).Where(c => c.DepoDate.Month == date.Month && c.DepoDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.DepoDate);
           
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

            var eCPay = db.CashPayments.Where(c => c.PaymentDate.Month == date.Month && c.PaymentDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.PaymentDate);//ok
            var ePay = db.Payments.Where(c => c.PayMode == PaymentModes.Cash && c.PayDate.Month == date.Month && c.PayDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.PayDate);
            var eStPay = db.StaffAdvancePayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && c.PaymentDate.Month == date.Month && c.PaymentDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.PaymentDate);
            //var eSal = db.SalaryPayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && c.PaymentDate.Month == date.Month && c.PaymentDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.PaymentDate);
            var eexp = db.Expenses.Where(c => c.PayMode == PaymentModes.Cash && c.ExpDate.Month == date.Month && c.ExpDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.ExpDate);
            var eDepo = db.BankDeposits.Include(C => C.Account).Where(c => c.DepoDate.Month == date.Month && c.DepoDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.DepoDate);
            var eDue = db.DuesLists.Include(e => e.DailySale).Where(c => c.IsRecovered == false && c.DailySale.SaleDate.Month == date.Month && c.DailySale.SaleDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.DailySale.SaleDate);
            var eCashEx = db.PettyCashExpenses.Where(c => c.ExpDate.Month == date.Month && c.ExpDate.Year == date.Year && c.StoreId == Store).OrderBy(c => c.ExpDate);
           
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

            //foreach (var item in eSal)
            //{
            //    CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, Particulars = item.Employee.StaffName, CashOut = item.Amount, CashBalance = 0 };
            //    book.Add(b);
            //}
            return CorrectBalCashBook(book, OpnBal);
        }

        //StoreBased Action Reviewed
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

        //StoreBased Action
        private List<CashInHand> CreateCashInHands(AprajitaRetailsContext db, List<CashBook> orderBook, int Store)
        {
            List<CashInHand> inHandList = new List<CashInHand>();

            CashInHand cashInHand = null;
            DateTime startDate = orderBook.First().EDate;

            //Remove CashInHand from Database
            DeleteCashInHandForMonth(db, startDate, Store);

            if (startDate.Date != new DateTime(startDate.Year, startDate.Month, 1))
            {
                CashInHand first = new CashInHand()
                {
                    CIHDate = new DateTime(startDate.Year, startDate.Month, 1),
                    OpenningBalance = 0,
                    CashIn = 0,
                    CashOut = 0,
                    ClosingBalance = 0, StoreId=Store
                };
                db.CashInHands.Add(first);
                inHandList.Add(first);
                db.SaveChanges();
            }

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
                        ClosingBalance = 0,
                        StoreId = Store
                    };
                }
                else if (startDate != item.EDate && cashInHand != null)
                {
                    db.CashInHands.Add(cashInHand);
                    inHandList.Add(cashInHand);
                    db.SaveChanges();
                    var datediff = item.EDate - startDate;
                    if (datediff.TotalDays > 1)
                    {
                        for (int xo = 1; xo < datediff.TotalDays; xo++)
                        {
                            cashInHand = new CashInHand()
                            {
                                CIHDate = startDate.AddDays(xo),
                                OpenningBalance = 0,
                                CashIn = 0,
                                CashOut = 0,
                                ClosingBalance = 0,
                                StoreId = Store
                            };
                            db.CashInHands.Add(cashInHand);
                            inHandList.Add(cashInHand);
                            db.SaveChanges();
                        }
                    }

                    cashInHand = new CashInHand()
                    {
                        CIHDate = item.EDate,
                        OpenningBalance = 0,
                        CashIn = item.CashIn,
                        CashOut = item.CashOut,
                        ClosingBalance = 0, StoreId=Store
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
            inHandList.Add(cashInHand);
            db.SaveChanges();
            DateTime endDate;

            endDate = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
            if (startDate != endDate)
            {
                var datediff = endDate - startDate;
                if (datediff.TotalDays > 1)
                {
                    for (int xo = 1; xo < datediff.TotalDays; xo++)
                    {
                        cashInHand = new CashInHand()
                        {
                            CIHDate = startDate.AddDays(xo),
                            OpenningBalance = 0,
                            CashIn = 0,
                            CashOut = 0,
                            ClosingBalance = 0,
                            StoreId = Store
                        };
                        db.CashInHands.Add(cashInHand);
                        inHandList.Add(cashInHand);
                        db.SaveChanges();
                    }
                }

                cashInHand = new CashInHand()
                {
                    CIHDate = endDate,
                    OpenningBalance = 0,
                    CashIn = 0,
                    CashOut = 0,
                    ClosingBalance = 0,
                    StoreId = Store
                };
                db.CashInHands.Add(cashInHand);
                inHandList.Add(cashInHand);
                db.SaveChanges();
            }
            return inHandList;
        }

        // Correct Cash In Hand In Database
        /// <summary>
        /// Delete Cash In Hand For Particular Month & Year and Store
        /// </summary>
        /// <param name="db"></param>
        /// <param name="date"></param>
        private void DeleteCashInHandForMonth(AprajitaRetailsContext db, DateTime date, int Store)
        {
            var cih = db.CashInHands.Where(c => c.CIHDate.Month == date.Month && c.CIHDate.Year==date.Year && c.StoreId== Store);
            db.CashInHands.RemoveRange(cih);
            db.SaveChanges();
        }
    }
}