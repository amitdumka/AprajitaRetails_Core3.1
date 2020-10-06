using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Tailoring.Data
{
    public class TailoringManager
    {
        public void OnInsert(AprajitaRetailsContext db, TailoringStaffAdvanceReceipt salPayment)
        {
            UpdateInAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.ReceiptDate, false);
        }
        public void OnInsert(AprajitaRetailsContext db, TailoringStaffAdvancePayment salPayment)
        {
            UpdateOutAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.PaymentDate, false);
        }
        public void OnInsert(AprajitaRetailsContext db, TailoringSalaryPayment salPayment)
        {
            UpdateOutAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.PaymentDate, false);
        }

        public void OnDelete(AprajitaRetailsContext db, TailoringSalaryPayment salPayment)
        {
            UpdateOutAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.PaymentDate, true);
        }
        public void OnDelete(AprajitaRetailsContext db, TailoringStaffAdvanceReceipt salPayment)
        {
            UpdateOutAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.ReceiptDate, true);
        }
        public void OnDelete(AprajitaRetailsContext db, TailoringStaffAdvancePayment salPayment)
        {
            UpdateOutAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.PaymentDate, true);
        }

        //public void OnUpdate(AprajitaRetailsContext db, TailoringSalaryPayment salPayment)
        //{

        //    var old = db.TailoringSalaryPayments.Where(c => c.TailoringSalaryPaymentId == salPayment.TailoringSalaryPaymentId).Select(d => new { d.Amount, d.PaymentDate, d.PayMode }).FirstOrDefault();
        //    if (old != null)
        //    {
        //        UpdateOutAmount(db, old.Amount, old.PayMode, old.PaymentDate, true);
        //    }
        //    UpdateOutAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.PaymentDate, false);
        //}

        //public void OnUpdate(AprajitaRetailsContext db, TailoringStaffAdvancePayment salPayment)
        //{
        //    var old = db.TailoringStaffAdvancePayments.Where(c => c.TailoringStaffAdvancePaymentId == salPayment.TailoringStaffAdvancePaymentId).Select(d => new { d.Amount, d.PaymentDate, d.PayMode }).FirstOrDefault();
        //    if (old != null)
        //    {
        //        UpdateOutAmount(db, old.Amount, old.PayMode, old.PaymentDate, true);
        //    }
        //    UpdateOutAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.PaymentDate, false);
        //}
        //public void OnUpdate(AprajitaRetailsContext db, TailoringStaffAdvanceReceipt salPayment)
        //{

        //    var old = db.TailoringStaffAdvanceReceipts.Where(c => c.TailoringStaffAdvanceReceiptId == salPayment.TailoringStaffAdvanceReceiptId).Select(d => new { d.Amount, d.ReceiptDate, d.PayMode }).FirstOrDefault();
        //    if (old != null)
        //    {
        //        UpdateInAmount(db, old.Amount, old.PayMode, old.ReceiptDate, true);
        //    }
        //    UpdateInAmount(db, salPayment.Amount, salPayment.PayMode, salPayment.ReceiptDate, false);
        //}

        //private void UpDateSalaryAmount(AprajitaRetailsContext db, SalaryPayment salPayment, bool IsEdit)
        //{
        //    if (IsEdit)
        //    {
        //        if (salPayment.PayMode == PayMode.Cash)
        //        {
        //            CashTrigger.UpDateCashOutHand(db, salPayment.PaymentDate, 0 - salPayment.Amount);

        //        }
        //        //TODO: in future make it more robust
        //        if (salPayment.PayMode != PayMode.Cash && salPayment.PayMode != PayMode.Coupons && salPayment.PayMode != PayMode.Points)
        //        {
        //            CashTrigger.UpDateCashOutBank(db, salPayment.PaymentDate, 0 - (salPayment.Amount - salPayment.Amount));
        //        }
        //    }
        //    else
        //    {
        //        if (salPayment.PayMode == PayMode.Cash )
        //        {
        //            CashTrigger.UpDateCashOutHand(db, salPayment.PaymentDate, salPayment.Amount);

        //        }
        //        //TODO: in future make it more robust
        //        if (salPayment.PayMode != PayMode.Cash && salPayment.PayMode != PayMode.Coupons && salPayment.PayMode != PayMode.Points)
        //        {
        //            CashTrigger.UpDateCashOutBank(db, salPayment.PaymentDate, salPayment.Amount - salPayment.Amount);
        //        }
        //    }

        //}
        //private void UpDateStaffPaymentAmount(AprajitaRetailsContext db, StaffAdvancePayment salPayment, bool IsEdit)
        //{
        //    if (IsEdit)
        //    {
        //        if (salPayment.PayMode == PayMode.Cash)
        //        {
        //            CashTrigger.UpDateCashOutHand(db, salPayment.PaymentDate, 0 - salPayment.Amount);

        //        }
        //        //TODO: in future make it more robust
        //        if (salPayment.PayMode != PayMode.Cash && salPayment.PayMode != PayMode.Coupons && salPayment.PayMode != PayMode.Points)
        //        {
        //            CashTrigger.UpDateCashOutBank(db, salPayment.PaymentDate, 0 - (salPayment.Amount - salPayment.Amount));
        //        }
        //    }
        //    else
        //    {
        //        if (salPayment.PayMode == PayMode.Cash)
        //        {
        //            CashTrigger.UpDateCashOutHand(db, salPayment.PaymentDate, salPayment.Amount);

        //        }
        //        //TODO: in future make it more robust
        //        if (salPayment.PayMode != PayMode.Cash && salPayment.PayMode != PayMode.Coupons && salPayment.PayMode != PayMode.Points)
        //        {
        //            CashTrigger.UpDateCashOutBank(db, salPayment.PaymentDate, salPayment.Amount - salPayment.Amount);
        //        }
        //    }

        //}
        //private void UpDateStaffReciptAmount(AprajitaRetailsContext db, StaffAdvanceReceipt salPayment, bool IsEdit)
        //{
        //    if (IsEdit)
        //    {
        //        if (salPayment.PayMode == PayMode.Cash)
        //        {
        //            CashTrigger.UpdateCashInHand(db, salPayment.ReceiptDate, 0 - salPayment.Amount);

        //        }
        //        //TODO: in future make it more robust
        //        if (salPayment.PayMode != PayMode.Cash && salPayment.PayMode != PayMode.Coupons && salPayment.PayMode != PayMode.Points)
        //        {
        //            CashTrigger.UpdateCashInBank(db, salPayment.ReceiptDate, 0 - (salPayment.Amount - salPayment.Amount));
        //        }
        //    }
        //    else
        //    {
        //        if (salPayment.PayMode == PayMode.Cash)
        //        {
        //            CashTrigger.UpdateCashInHand(db, salPayment.ReceiptDate, salPayment.Amount);

        //        }
        //        //TODO: in future make it more robust
        //        if (salPayment.PayMode != PayMode.Cash && salPayment.PayMode != PayMode.Coupons && salPayment.PayMode != PayMode.Points)
        //        {
        //            CashTrigger.UpdateCashInBank(db, salPayment.ReceiptDate, salPayment.Amount - salPayment.Amount);
        //        }
        //    }

        //}

        private void UpdateOutAmount(AprajitaRetailsContext db, decimal Amount, PayMode PayMode, DateTime PaymentDate, bool IsEdit)
        {
            if (IsEdit)
            {
                if (PayMode == PayMode.Cash)
                {
                    CashTrigger.UpDateCashOutHand(db, PaymentDate, 0 - Amount);

                }
                //TODO: in future make it more robust
                if (PayMode != PayMode.Cash && PayMode != PayMode.Coupons && PayMode != PayMode.Points)
                {
                    CashTrigger.UpDateCashOutBank(db, PaymentDate, 0 - (Amount - Amount));
                }
            }
            else
            {
                if (PayMode == PayMode.Cash)
                {
                    CashTrigger.UpDateCashOutHand(db, PaymentDate, Amount);

                }
                //TODO: in future make it more robust
                if (PayMode != PayMode.Cash && PayMode != PayMode.Coupons && PayMode != PayMode.Points)
                {
                    CashTrigger.UpDateCashOutBank(db, PaymentDate, Amount - Amount);
                }
            }
        }
        private void UpdateInAmount(AprajitaRetailsContext db, decimal Amount, PayMode PayMode, DateTime PaymentDate, bool IsEdit)
        {
            if (IsEdit)
            {
                if (PayMode == PayMode.Cash)
                {
                    CashTrigger.UpdateCashInHand(db, PaymentDate, 0 - Amount);

                }
                //TODO: in future make it more robust
                if (PayMode != PayMode.Cash && PayMode != PayMode.Coupons && PayMode != PayMode.Points)
                {
                    CashTrigger.UpdateCashInBank(db, PaymentDate, 0 - (Amount - Amount));
                }
            }
            else
            {
                if (PayMode == PayMode.Cash)
                {
                    CashTrigger.UpdateCashInHand(db, PaymentDate, Amount);

                }
                //TODO: in future make it more robust
                if (PayMode != PayMode.Cash && PayMode != PayMode.Coupons && PayMode != PayMode.Points)
                {
                    CashTrigger.UpdateCashInBank(db, PaymentDate, Amount - Amount);
                }
            }
        }

        public void OnUpdateData(AprajitaRetailsContext db, TalioringDelivery delivery,bool isEdit, bool isDelete = false)
        {
            TalioringBooking booking = db.TalioringBookings.Find(delivery.TalioringBookingId);
            if (isEdit)
            {
                if (booking != null)
                {
                    var oldId = db.TailoringDeliveries.Where(c => c.TalioringDeliveryId == delivery.TalioringDeliveryId).Select(c=>new {c.TalioringBookingId }).FirstOrDefault();
                    if (oldId.TalioringBookingId != delivery.TalioringBookingId)
                    {
                        TalioringBooking old = db.TalioringBookings.Find(oldId.TalioringBookingId);
                        old.IsDelivered = false;
                        booking.IsDelivered = true;
                        db.Entry(booking).State = EntityState.Modified;
                        db.Entry(old).State = EntityState.Modified;
                    }
                }
            }
            else
            {
                if (booking != null)
                {
                    if (isDelete)
                    {
                        booking.IsDelivered = false;
                    }
                    else
                    {
                        booking.IsDelivered = true;
                    }
                    db.Entry(booking).State = EntityState.Modified;
                }
            }
            

        }


    }
}
