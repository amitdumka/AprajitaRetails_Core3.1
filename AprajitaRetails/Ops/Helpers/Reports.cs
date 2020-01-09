
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;

namespace TAS_AprajiataRetails.Models.Helpers
{
    public class Reports
    {
        public static TailoringReport GetTailoringReport(AprajitaRetailsContext context)
        {
            using (AprajitaRetailsContext db = context)
            {
                return new TailoringReport()
                {
                    TodayBooking = (int?)db.Bookings.Where(c => (c.BookingDate.Date) == (DateTime.Today)).Count() ?? 0,
                    TodayUnit = (int?)db.Bookings.Where(c => (c.BookingDate.Date) == (DateTime.Today)).Sum(c => (int?)c.TotalQty) ?? 0,

                    MonthlyBooking = (int?)db.Bookings.Where(c => (c.BookingDate).Month == (DateTime.Today).Month).Count() ?? 0,
                    MonthlyUnit = (int?)db.Bookings.Where(c => (c.BookingDate).Month == (DateTime.Today).Month).Sum(c => (int?)c.TotalQty) ?? 0,
                    
                    YearlyBooking = (int?)db.Bookings.Where(c => (c.BookingDate).Year == (DateTime.Today).Year).Count() ?? 0,
                    YearlyUnit = (int?)db.Bookings.Where(c => (c.BookingDate).Year == (DateTime.Today).Year).Sum(c => (int?)c.TotalQty) ?? 0,

                    TodaySale = (decimal?)db.Deliveries.Where(c => (c.DeliveryDate.Date) == (DateTime.Today)).Sum(c => (decimal?)c.Amount) ?? 0,
                    YearlySale = (decimal?)db.Deliveries.Where(c => (c.DeliveryDate).Year == (DateTime.Today).Year).Sum(c => (decimal?)c.Amount) ?? 0,
                    MonthlySale = (decimal?)db.Deliveries.Where(c => (c.DeliveryDate).Month == (DateTime.Today).Month).Sum(c => (decimal?)c.Amount) ?? 0,
                };

            }


        }

        public static DailySaleReport GetSaleRecord(AprajitaRetailsContext context)
        {
            using (AprajitaRetailsContext db =context)
            {
                DailySaleReport record = new DailySaleReport
                {
                    DailySale = (decimal?) db.DailySales.Where (C =>  (C.SaleDate) ==  (DateTime.Today)).Sum (c => (decimal?) c.Amount) ?? 0,
                    MonthlySale = (decimal?) db.DailySales.Where (C =>  (C.SaleDate).Month ==  (DateTime.Today).Month).Sum (c => (decimal?) c.Amount) ?? 0,
                    YearlySale = (decimal?) db.DailySales.Where (C =>  (C.SaleDate).Year ==  (DateTime.Today).Year).Sum (c => (decimal?) c.Amount) ?? 0
                };

                return record;
            }

        }

        public static AccountsInfo GetAccoutingRecord(AprajitaRetailsContext context)
        {

            using (AprajitaRetailsContext db = context)
            {
                AccountsInfo info = new AccountsInfo();
                CashInHand cih = db.CashInHands.Where(c => (c.CIHDate) == (DateTime.Today)).FirstOrDefault();

                if (cih != null)
                {
                    info.CashInHand = cih.InHand; info.CashIn = cih.CashIn; info.CashOut = cih.CashOut;
                    info.OpenningBal = cih.OpenningBalance;
                }

                CashInBank cib = db.CashInBanks.Where(c => (c.CIBDate) == (DateTime.Today)).FirstOrDefault();
                if (cib != null)
                {
                    info.CashToBank = cib.CashIn; info.CashFromBank = cib.CashOut;
                    info.CashInBank = cib.InHand;

                }

                var CashExp = db.CashExpenses.Where(c => (c.ExpDate) == (DateTime.Today));
                var CashPay = db.CashPayments.Where(c => (c.PaymentDate) == (DateTime.Today));

                if (CashExp != null)
                {
                    info.TotalCashPayments = (decimal?)CashExp.Sum(c => (decimal?)c.Amount) ?? 0;
                }
                if (CashPay != null)
                {
                    info.TotalCashPayments += (decimal?)CashPay.Sum(c => (decimal?)c.Amount) ?? 0;
                }
                return info;
            }
        }


        public static List<EmployeeInfo> GetEmpInfo(AprajitaRetailsContext context)
        {
            using (AprajitaRetailsContext db = context)
            {
                //List<EmpStatus> statsList = new List<EmpStatus>();
                var emps = db.Attendances.Include(c => c.Employee).
                    Where(c => (c.AttDate) == (DateTime.Today)).OrderByDescending(c => c.Employee.StaffName);

                var empPresent = db.Attendances.Include(c => c.Employee)
                    .Where(c => c.Status == AttUnits.Present && (c.AttDate).Month == (DateTime.Today).Month)
                    .GroupBy(c => c.Employee.StaffName).OrderBy(c => c.Key).Select(g => new { StaffName = g.Key, Days = g.Count() }).ToList();

                var empAbsent = db.Attendances.Include(c => c.Employee)
                    .Where(c => c.Status == AttUnits.Absent && (c.AttDate).Month == (DateTime.Today).Month)
                     .GroupBy(c => c.Employee.StaffName).OrderBy(c => c.Key).Select(g => new { StaffName = g.Key, Days = g.Count() }).ToList();

                var totalSale = db.DailySales.Include(c => c.Salesman).Where(c => (c.SaleDate).Month == (DateTime.Today).Month).Select(a => new { StaffName = a.Salesman.SalesmanName, Amount = a.Amount }).ToList();

                List<EmployeeInfo> infoList = new List<EmployeeInfo>();

                foreach (var item in emps)
                {
                    if (item.Employee.StaffName != "Amit Kumar")
                    {
                        EmployeeInfo info = new EmployeeInfo()
                        {
                            Name = item.Employee.StaffName, AbsentDays = 0, NoOfBills = 0, Present = "", PresentDays = 0, Ratio = 0,
                            TotalSale = 0
                        };

                        if (item.Status == AttUnits.Present)
                            info.Present = "Present";
                        else info.Present = "Absent";

                        try
                        {

                            if (empPresent != null)
                            {
                                var pd = empPresent.Where(c => c.StaffName == info.Name).FirstOrDefault();
                                if (pd != null)
                                  info.PresentDays = pd.Days;
                                 else info.PresentDays = 0;
                                
                            }
                            else
                            {
                                info.PresentDays = 0;
                            }

                            if (empAbsent != null)
                            {
                                var ad = empAbsent.Where(c => c.StaffName == info.Name).FirstOrDefault();
                                if (ad != null)
                                    info.AbsentDays = ad.Days;
                                else info.AbsentDays = 0;
                               
                            }
                            else info.AbsentDays = 0;

                            //var ts = db.DailySales.Include(c=>c.Salesman ).Where (c => c.Salesman.SalesmanName == info.Name && (c.SaleDate).Month == (DateTime.Today).Month).ToList();
                            if (totalSale != null)
                            {
                                var ts = totalSale.Where(c => c.StaffName == info.Name).ToList();
                                info.TotalSale = (decimal?)ts.Sum(c => (decimal?)c.Amount) ?? 0;
                                info.NoOfBills = (int?)ts.Count() ?? 0;
                            }

                            if (info.PresentDays > 0 && info.TotalSale > 0)
                            {
                                info.Ratio = Math.Round( (double)info.TotalSale / info.PresentDays,2);
                            }

                        }
                        catch (Exception)
                        {
                           // Log.Error().Message("empresent exception");
                        }
                           

                            // var ts = db.DailySales.Where(c => c.Salesman.SalesmanName == info.Name && (c.SaleDate).Month == (DateTime.Today).Month);

                            //if (ts != null )
                            //{
                            //    info.TotalSale =(decimal?) ts.Sum(c =>(decimal?) c.Amount)??0;
                            //    info.NoOfBills =(int?) ts.Count()??0;
                            //}

                            //if (info.PresentDays > 0 && info.TotalSale > 0)
                            //{
                            //    info.Ratio = (double)info.TotalSale / info.PresentDays;
                            //}



                            infoList.Add(info);
                    }


                }
                return infoList;
            }
        }


        public static List<CashBook> GetMontlyCashBook(AprajitaRetailsContext context,DateTime date)
        {
            using (AprajitaRetailsContext db = context)
            {
                List<CashBook> book = new List<CashBook>();


                DateTime oDate = new DateTime(date.Year, date.Month, 1);
                decimal OpnBal = 0;
                try
                {
                    OpnBal = (decimal?)db.CashInHands.Where(c => (c.CIHDate) == (oDate)).FirstOrDefault().OpenningBalance ?? 0;

                }
                catch (Exception)
                {
                    OpnBal = 0;
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

            }//end of using

        }

        public static List<CashBook> GetDailyCashBook(AprajitaRetailsContext context,DateTime date)
        {
            using (AprajitaRetailsContext db = context)
            {
                List<CashBook> book = new List<CashBook>();
                decimal OpnBal = 0;
                try
                {
                    OpnBal = (decimal?)db.CashInHands.Where(c => (c.CIHDate) == (date)).FirstOrDefault().OpenningBalance ?? 0;

                }
                catch (Exception)
                {
                    OpnBal = 0;
                }

                // decimal OpnBal= (decimal?)db.CashInHands.Where(c => (c.CIHDate) == (date)).FirstOrDefault().OpenningBalance??0;
                //income
                var dSale = db.DailySales.Where(c => c.PayMode == PayModes.Cash && (c.SaleDate) == (date)).OrderBy(c => c.SaleDate);//ok
                var dRec = db.Receipts.Where(c => c.PayMode == PaymentModes.Cash && (c.RecieptDate) == (date)).OrderBy(c => c.RecieptDate);//ok
                var dCashRec = db.CashReceipts.Where(c => (c.InwardDate) == (date)).OrderBy(c => c.InwardDate);//ok
                var dSRec = db.StaffAdvanceReceipts.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.ReceiptDate) == (date)).OrderBy(c => c.ReceiptDate);//ok
                var dWit = db.Withdrawals.Include(C => C.Account).Where(c => (c.DepoDate) == (date)).OrderBy(c => c.DepoDate);
                var dTalRec = db.TailoringStaffAdvanceReceipts.Include(c => c.Employee).Where(c => c.PayMode == PayModes.Cash && (c.ReceiptDate) == (date)).OrderBy(c => c.ReceiptDate);//ok

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

                var eCPay = db.CashPayments.Where(c => (c.PaymentDate) == (date)).OrderBy(c => c.PaymentDate);//ok
                var ePay = db.Payments.Where(c => c.PayMode == PaymentModes.Cash && (c.PayDate) == (date)).OrderBy(c => c.PayDate);
                var eStPay = db.StaffAdvancePayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate) == (date)).OrderBy(c => c.PaymentDate);
                var eSal = db.Salaries.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate) == (date)).OrderBy(c => c.PaymentDate);
                var eexp = db.Expenses.Where(c => c.PayMode == PaymentModes.Cash && (c.ExpDate) == (date)).OrderBy(c => c.ExpDate);
                var eDepo = db.BankDeposits.Include(C => C.Account).Where(c => (c.DepoDate) == (date)).OrderBy(c => c.DepoDate);
                var eDue = db.DuesLists.Include(c => c.DailySale).Where(c => c.IsRecovered == false && (c.DailySale.SaleDate) == (date)).OrderBy(c => c.DailySale.SaleDate);
                var eCashEx = db.CashExpenses.Where(c => (c.ExpDate) == (date)).OrderBy(c => c.ExpDate);

                var eTalSal = db.TailoringSalaries.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate) == (date)).OrderBy(c => c.PaymentDate);
                var eTalStPay = db.TailoringStaffAdvancePayments.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.PaymentDate) == (date)).OrderBy(c => c.PaymentDate);

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
            } //end of usin

        }

        public static List<CashBook> CorrectBalCashBook(List<CashBook> books, decimal OpnBal)
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


        public static List<CashBook> CorrectCashInHand_OnDB(AprajitaRetailsContext context,DateTime date)
        {

            using (AprajitaRetailsContext db = context)
            {
                List<CashBook> book = new List<CashBook>();

                //income
                var dSale = db.DailySales.Where(c => c.PayMode == PayModes.Cash && (c.SaleDate).Month == (date).Month).OrderBy(c => c.SaleDate);//ok
                var dRec = db.Receipts.Where(c => c.PayMode == PaymentModes.Cash && (c.RecieptDate).Month == (date).Month).OrderBy(c => c.RecieptDate);//ok
                var dCashRec = db.CashReceipts.Where(c => (c.InwardDate).Month == (date).Month).OrderBy(c => c.InwardDate);//ok
                var dSRec = db.StaffAdvanceReceipts.Include(e => e.Employee).Where(c => c.PayMode == PayModes.Cash && (c.ReceiptDate).Month == (date).Month).OrderBy(c => c.ReceiptDate);//ok
                var dWit = db.Withdrawals.Include(C => C.Account).Where(c => (c.DepoDate).Month == (date).Month).OrderBy(c => c.DepoDate);

                var dTalRec = db.TailoringStaffAdvanceReceipts.Include(c => c.Employee).Where(c => c.PayMode == PayModes.Cash && (c.ReceiptDate).Month == (date).Month).OrderBy(c => c.ReceiptDate);//ok

                foreach (var item in dSale)
                {
                    CashBook b = new CashBook() { EDate = item.SaleDate, CashIn = item.Amount, CashOut = 0, CashBalance = 0 };
                    book.Add(b);
                }
                foreach (var item in dRec)
                {
                    CashBook b = new CashBook() { EDate = item.RecieptDate, CashIn = item.Amount, CashOut = 0, CashBalance = 0 };
                    book.Add(b);
                }

                foreach (var item in dCashRec)
                {
                    CashBook b = new CashBook() { EDate = item.InwardDate, CashIn = item.Amount, CashOut = 0, CashBalance = 0 };
                    book.Add(b);
                }

                foreach (var item in dSRec)
                {
                    CashBook b = new CashBook() { EDate = item.ReceiptDate, CashIn = item.Amount, CashOut = 0, CashBalance = 0 };
                    book.Add(b);
                }

                foreach (var item in dTalRec)
                {
                    CashBook b = new CashBook() { EDate = item.ReceiptDate, CashIn = item.Amount, CashOut = 0, CashBalance = 0 };
                    book.Add(b);
                }

                foreach (var item in dWit)
                {
                    CashBook b = new CashBook() { EDate = item.DepoDate, CashIn = item.Amount, CashBalance = 0 };
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
                    CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }

                foreach (var item in eTalSal)
                {
                    CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }




                foreach (var item in eexp)
                {
                    CashBook b = new CashBook() { EDate = item.ExpDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }
                foreach (var item in eDepo)
                {
                    CashBook b = new CashBook() { EDate = item.DepoDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }
                foreach (var item in eCashEx)
                {
                    CashBook b = new CashBook() { EDate = item.ExpDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }
                foreach (var item in eDue)
                {
                    CashBook b = new CashBook() { EDate = item.DailySale.SaleDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }


                foreach (var item in eCPay)
                {
                    CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }

                foreach (var item in ePay)
                {
                    CashBook b = new CashBook() { EDate = item.PayDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }

                foreach (var item in eStPay)
                {
                    CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }

                foreach (var item in eSal)
                {
                    CashBook b = new CashBook() { EDate = item.PaymentDate, CashIn = 0, CashOut = item.Amount, CashBalance = 0 };
                    book.Add(b);
                }
                book = UpdateCashInHand_DB(db, book);
                Utils.CashInHandCorrectionForMonth(db,date);
                return book;

            }//end of using

        }

        //public static List<CashBook> UpdateCorrectCashInHand_DB(List<CashBook> books, AprajitaRetailsContext db)
        //{

        //    IEnumerable<CashBook> orderBook = books.OrderBy(c => c.EDate);

        //    decimal bal = 0;
        //    DateTime dDate = orderBook.First().EDate.AddDays(-1);

        //    var cIh = db.CashInHands.Where(c => (c.CIHDate) == (dDate)).FirstOrDefault();
        //    if (cIh != null)
        //    {
        //        bal = cIh.ClosingBalance = cIh.OpenningBalance + cIh.CashIn - cIh.CashOut;
        //    }
        //    dDate = orderBook.First().EDate;
        //    cIh = db.CashInHands.Where(c => (c.CIHDate) == (dDate)).FirstOrDefault();
        //    if (cIh == null)
        //    {
        //        Utils.CreateCashInHand(db, dDate, 0, 0, true);
        //    }
        //    else
        //    {
        //        cIh.CashIn = 0;
        //        cIh.CashOut = 0;
        //        cIh.OpenningBalance = bal;
        //        cIh.ClosingBalance = cIh.OpenningBalance + cIh.CashIn - cIh.CashOut;

        //    }
        //    db.SaveChanges();
        //    DateTime tDate = dDate.AddDays(-1);
        //    var cashInHandList = db.CashInHands.Where(c => (c.CIHDate) > (tDate));

        //    decimal opibal = 0;
        //    foreach (var item in orderBook)
        //    {
        //        if (dDate != item.EDate)
        //        {
        //            dDate = item.EDate;
        //            CashInHand cs = cashInHandList.Where(c => (c.CIHDate) == (item.EDate)).FirstOrDefault();

        //            cs.CashIn = item.CashIn;
        //            cs.CashOut = item.CashOut;

        //            tDate = item.EDate.AddDays(-1);
        //            cs.OpenningBalance = opibal;
        //            opibal = cs.ClosingBalance = cs.OpenningBalance + cs.CashIn - cs.CashOut;

        //            db.Entry(cs).State = EntityState.Modified;
        //        }
        //        else
        //        {
        //            CashInHand cs = cashInHandList.Where(c => (c.CIHDate) == item.EDate).FirstOrDefault();
        //            cs.CashIn = item.CashIn;
        //            cs.CashOut = item.CashOut;
        //            opibal = cs.ClosingBalance = cs.OpenningBalance + cs.CashIn - cs.CashOut;
        //            db.Entry(cs).State = EntityState.Modified;
        //        }

        //        item.CashBalance = bal + item.CashIn - item.CashOut;
        //        bal = item.CashBalance;
        //    }
        //    db.SaveChanges();
        //    return orderBook.ToList();


        //}


        //public static void ClearCashInHand(AprajitaRetailsContext db, DateTime date)
        //{
        //    var cih = db.CashInHands.Where(c => (c.CIHDate).Month == (date).Month);

        //    foreach (var item in cih)
        //    {
        //        item.CashIn = item.CashOut = item.ClosingBalance = item.OpenningBalance = 0;
        //        db.Entry(item).State = EntityState.Modified;
        //    }
        //    db.SaveChanges();
        //}

        //Function in use
        public static void DeleteCashInHandForMonth(AprajitaRetailsContext db, DateTime date)
        {
            var cih = db.CashInHands.Where(c => (c.CIHDate).Month == (date).Month);
            db.CashInHands.RemoveRange(cih);
            db.SaveChanges();


        }


        //Function used
        public static List<CashBook> UpdateCashInHand_DB(AprajitaRetailsContext db, List<CashBook> books)
        {

            IEnumerable<CashBook> orderBook = books.OrderBy(c => c.EDate);
            CashInHand cashInHand = null;
            DateTime startDate = orderBook.First().EDate;
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


    }


}