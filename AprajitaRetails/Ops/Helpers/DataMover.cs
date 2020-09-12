using AprajitaRetails.Areas.Voyager.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.Helpers
{
    public class DataMover
    {
        public class DataMoveReturn
        {
            public bool IsOk { get; set; }
            public bool IsSalaryOk { get; set; }
            public bool IsAdvPayOk { get; set; }
            public bool IsAdvRecOk { get; set; }
            public bool IsAttOk { get; set; }
            public bool IsDupRecord { get; set; }
        }

        /// <summary>
        /// Move  Partiular Tailor Data to Employee marked with Tailor
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tailorId"></param>
        /// <param name="StoreId"></param>
        /// <returns></returns>
        //public DataMoveReturn MoveTailorToPayroll(AprajitaRetailsContext db, int tailorId, int StoreId)
        //{
        //    DataMoveReturn dmReturn = new DataMoveReturn();

        //    var tailor = db.TailoringEmployees.Find(tailorId);
        //    if (tailor == null) { dmReturn.IsOk = false; return dmReturn; }

        //    var dupemp = db.Employees.Where(c => c.StaffName == tailor.StaffName && c.MobileNo == tailor.MobileNo && c.MobileNo == tailor.MobileNo && c.IsTailors == true).FirstOrDefault();
        //    if (dupemp != null)
        //    {
        //        dmReturn.IsOk = false; dmReturn.IsDupRecord = true;
        //        return dmReturn;
        //    }
        //    else dmReturn.IsDupRecord = false;
        //    //TODO: Verify that his/her Record is already added or not

        //    Employee newEmp = new Employee
        //    {
        //        StaffName = tailor.StaffName,
        //        IsTailors = true,
        //        Category = EmpType.Tailors,
        //        IsWorking = tailor.IsWorking,
        //        JoiningDate = tailor.JoiningDate,
        //        MobileNo = tailor.MobileNo,
        //        StoreId = StoreId,
        //        LeavingDate = tailor.LeavingDate
        //    };

        //    db.Employees.Add(newEmp);
        //    if (db.SaveChanges() == 1)
        //    {
        //        dmReturn.IsOk = true;
        //        var att = db.TailorAttendances.Where(c => c.TailoringEmployeeId == tailorId).ToList();
        //        //List<Attendance> listAtt = new List<Attendance>();
        //        int RecordCount = 0;
        //        foreach (var item in att)
        //        {
        //            Attendance newAtt = new Attendance
        //            {
        //                AttDate = item.AttDate,
        //                Remarks = item.Remarks,
        //                EntryTime = item.EntryTime,
        //                IsTailoring = true,
        //                Status = item.Status,
        //                StoreId = StoreId,
        //                EmployeeId = newEmp.EmployeeId /* check if update on Save*/,
        //                Employee = newEmp
        //            };
        //            db.Attendances.Add(newAtt);
        //            RecordCount++;
        //        }
        //        if (db.SaveChanges() != RecordCount) dmReturn.IsAttOk = false; else dmReturn.IsAttOk = true;
        //        RecordCount = 0;
        //        var sal = db.TailoringSalaryPayments.Where(c => c.TailoringEmployeeId == tailorId).ToList();
        //        foreach (var item in sal)
        //        {
        //            SalaryPayment salary = new SalaryPayment
        //            {
        //                Amount = item.Amount,
        //                Details = item.Details,
        //                EmployeeId = newEmp.EmployeeId,
        //                Employee = newEmp,
        //                PaymentDate = item.PaymentDate,
        //                PayMode = item.PayMode,
        //                SalaryMonth = item.SalaryMonth,
        //                StoreId = StoreId,
        //                SalaryComponet = item.SalaryComponet
        //            };
        //            db.SalaryPayments.Add(salary);
        //            RecordCount++;
        //        }

        //        if (db.SaveChanges() != RecordCount) dmReturn.IsSalaryOk = false; else dmReturn.IsSalaryOk = true;

        //        RecordCount = 0;
        //        var advPay = db.TailoringStaffAdvancePayments.Where(c => c.TailoringEmployeeId == tailorId).ToList();
        //        foreach (var item in advPay)
        //        {
        //            StaffAdvancePayment pay = new StaffAdvancePayment
        //            {
        //                StoreId = StoreId,
        //                Amount = item.Amount,
        //                Details = item.Details,
        //                PayMode = item.PayMode,
        //                Employee = newEmp,
        //                EmployeeId = newEmp.EmployeeId,
        //                PaymentDate = item.PaymentDate
        //            };
        //            db.StaffAdvancePayments.Add(pay);
        //            RecordCount++;
        //        }
        //        if (db.SaveChanges() != RecordCount) dmReturn.IsAdvPayOk = false; else dmReturn.IsAdvPayOk = true;

        //        RecordCount = 0;
        //        var advRec = db.TailoringStaffAdvanceReceipts.Where(c => c.TailoringEmployeeId == tailorId).ToList();
        //        foreach (var item in advRec)
        //        {
        //            StaffAdvanceReceipt staff = new StaffAdvanceReceipt
        //            {
        //                StoreId = StoreId,
        //                Amount = item.Amount,
        //                Details = item.Details,
        //                PayMode = item.PayMode,
        //                Employee = newEmp,
        //                EmployeeId = newEmp.EmployeeId,
        //                ReceiptDate = item.ReceiptDate
        //            };
        //            db.StaffAdvanceReceipts.Add(staff);
        //            RecordCount++;
        //        }
        //        if (db.SaveChanges() == RecordCount) dmReturn.IsAdvRecOk = true;
        //        else dmReturn.IsAdvRecOk = false;

        //        return dmReturn;
        //    }
        //    else
        //    {
        //        dmReturn.IsOk = false; return dmReturn;
        //    }


        //}
    
    
        public static List<SalaryPayment>  MoveAdvancePaymentToSalary(AprajitaRetailsContext db, int StoreId = 1)
        {
           
            var adv = db.StaffAdvancePayments.Where(c => c.StoreId == StoreId && (c.IsDataMoved==false || c.IsDataMoved==null)).ToList();
            List<SalaryPayment> salaries = new List<SalaryPayment>();
            foreach (var item in adv)
            {
                SalaryPayment salary = new SalaryPayment {
                    StoreId=StoreId, Amount=item.Amount,  EmployeeId=item.EmployeeId, PaymentDate= item.PaymentDate, 
                    PayMode=item.PayMode, UserName="System", SalaryComponet=SalaryComponet.Advance, Details=item.Details,
                    SalaryMonth=item.PaymentDate.Date.Month+"/"+item.PaymentDate.Date.Year
                };
                salaries.Add(salary);
                item.IsDataMoved = true;
            }

            db.SalaryPayments.AddRange(salaries);
            int ctr = db.SaveChanges();
            db.StaffAdvancePayments.UpdateRange(adv);
            int ctr2 = db.SaveChanges();
           // if (ctr != ctr2) return null;

            return salaries;



        }
    
    
    }
}
