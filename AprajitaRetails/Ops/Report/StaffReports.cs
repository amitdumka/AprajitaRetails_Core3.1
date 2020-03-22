using System;
using System.Collections.Generic;
using System.Linq;
using AprajitaRetails.Data;

namespace AprajitaRetails.Models.Reports
{
    public class StaffPayments
    {
        // Help to calculate current month payout
        public int EmpId { get; set; }

        public decimal SalaryPayment { get; set; }
        public decimal AdavcePayment { get; set; }
        public decimal AdvaceReciepts { get; set; }
        public decimal CurrentMonthSalary { get; set; }
        public decimal NetSalary { get; set; }
    }

    public class StaffReports
    {
        public StaffPayments StaffPayments(AprajitaRetailsContext db, int EmpId, DateTime onDate, decimal curSalary)
        {
            StaffPayments sPay = new StaffPayments
            {
                AdavcePayment = db.StaffAdvancePayments.Where (c => c.EmployeeId == EmpId && ( c.PaymentDate ).Date.Month == ( onDate ).Date.Month).Sum (c => c.Amount),
                AdvaceReciepts = db.StaffAdvanceReceipts.Where (c => c.EmployeeId == EmpId && ( c.ReceiptDate ).Date.Month == ( onDate ).Date.Month).Sum (c => c.Amount),
                SalaryPayment = db.SalaryPayments.Where (c => c.EmployeeId == EmpId && ( c.PaymentDate ).Date.Month == ( onDate ).Date.Month).Sum (c => c.Amount),
                CurrentMonthSalary = curSalary
            };
            sPay.NetSalary = curSalary - ( sPay.SalaryPayment + sPay.AdavcePayment - sPay.AdvaceReciepts );

            return sPay;
        }

        public List<StaffPayments> AllStaffPayments(AprajitaRetailsContext db, DateTime onDate)
        {
            var emp = db.Employees.Where (c => c.IsWorking).Select (c => c.EmployeeId);
            List<StaffPayments> lists = new List<StaffPayments> ();
            foreach ( var EmpId in emp )
            {
                StaffPayments sPay = new StaffPayments
                {
                    AdavcePayment = db.StaffAdvancePayments.Where (c => c.EmployeeId == EmpId && ( c.PaymentDate ).Date.Month == onDate.Date.Month).Sum (c => c.Amount),
                    AdvaceReciepts = db.StaffAdvanceReceipts.Where (c => c.EmployeeId == EmpId && ( c.ReceiptDate ).Date.Month == ( onDate ).Date.Month).Sum (c => c.Amount),
                    SalaryPayment = db.SalaryPayments.Where (c => c.EmployeeId == EmpId && ( c.PaymentDate ).Date.Month == ( onDate ).Date.Month).Sum (c => c.Amount),
                    CurrentMonthSalary = 0//TODO: add option here
                };
                sPay.NetSalary = sPay.CurrentMonthSalary - ( sPay.SalaryPayment + sPay.AdavcePayment - sPay.AdvaceReciepts );

                lists.Add (sPay);
            }

            return lists;
        }

        public void CalculateSalary()
        {
            // use current salary and payslip
        }
    }
}