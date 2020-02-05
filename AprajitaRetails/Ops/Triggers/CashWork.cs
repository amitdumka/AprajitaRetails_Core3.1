using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;

namespace AprajitaRetails.Ops.Triggers
{
    public class CashWork
    {
        public void Process_OpenningBalance(AprajitaRetailsContext db, DateTime date, bool saveit = false)
        {

            CashInHand today;
            today = db.CashInHands.Where(c => c.CIHDate.Date == date.Date).FirstOrDefault();

            DateTime yDate = date.AddDays(-1);
            CashInHand yesterday = db.CashInHands.Where(c => (c.CIHDate.Date) == (yDate.Date)).FirstOrDefault();
            bool isNew = false;
            if (today == null)
            {
                today = new CashInHand() { CashIn = 0, CashOut = 0, CIHDate = date, ClosingBalance = 0, OpenningBalance = 0 };
                isNew = true;
            }

            if (yesterday == null)
            {
                yesterday = new CashInHand() { CashIn = 0, CashOut = 0, CIHDate = yDate, ClosingBalance = 0, OpenningBalance = 0 };
                today.OpenningBalance = 0;
                today.ClosingBalance = today.OpenningBalance + today.CashIn - today.CashOut;
                db.CashInHands.Add(yesterday);
            }
            else
            {
                yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;

                today.OpenningBalance = yesterday.ClosingBalance;
                today.ClosingBalance = today.OpenningBalance + today.CashIn - today.CashOut;

                db.Entry(yesterday).State = EntityState.Modified;

            }

            if (isNew)
                db.CashInHands.Add(today);
            else
                db.Entry(today).State = EntityState.Modified;

            if (saveit)
                db.SaveChanges();
        }

        public void Process_ClosingBalance(AprajitaRetailsContext db, DateTime date, bool saveit = false)
        {
            CashInHand today;
            today = db.CashInHands.Where(c => (c.CIHDate.Date) == (date.Date)).FirstOrDefault();
            if (today != null)
            {
                if (today.ClosingBalance != today.OpenningBalance + today.CashIn - today.CashOut)
                {
                    today.ClosingBalance = today.OpenningBalance + today.CashIn - today.CashOut;
                    db.Entry(today).State = EntityState.Modified;
                    if (saveit) db.SaveChanges();
                }

            }
        }

        public void Process_BankOpenningBalance(AprajitaRetailsContext db, DateTime date, bool saveit = false)
        {

            CashInBank today;
            today = db.CashInBanks.Where(c => c.CIBDate.Date == date.Date).FirstOrDefault();

            DateTime yDate = date.AddDays(-1);
            CashInBank yesterday = db.CashInBanks.Where(c => (c.CIBDate.Date) == (yDate.Date)).FirstOrDefault();

            bool isNew = false;
            if (today == null)
            {
                today = new CashInBank() { CashIn = 0, CashOut = 0, CIBDate = date, ClosingBalance = 0, OpenningBalance = 0 };
                isNew = true;
            }

            if (yesterday == null)
            {
                yesterday = new CashInBank() { CashIn = 0, CashOut = 0, CIBDate = yDate, ClosingBalance = 0, OpenningBalance = 0 };
                today.OpenningBalance = 0;
                today.ClosingBalance = today.OpenningBalance + today.CashIn - today.CashOut;
                db.CashInBanks.Add(yesterday);
            }
            else
            {
                yesterday.ClosingBalance = yesterday.OpenningBalance + yesterday.CashIn - yesterday.CashOut;

                today.OpenningBalance = yesterday.ClosingBalance;
                today.ClosingBalance = today.OpenningBalance + today.CashIn - today.CashOut;

                db.Entry(yesterday).State = EntityState.Modified;

            }

            if (isNew)
                db.CashInBanks.Add(today);
            else
                db.Entry(today).State = EntityState.Modified;

            if (saveit)
                db.SaveChanges();
        }

        public void Process_BankClosingBalance(AprajitaRetailsContext db, DateTime date, bool saveit = false)
        {
            CashInBank today;
            today = db.CashInBanks.Where(c => (c.CIBDate.Date) == (date.Date)).FirstOrDefault();
            if (today != null)
            {
                if (today.ClosingBalance != today.OpenningBalance + today.CashIn - today.CashOut)
                {
                    today.ClosingBalance = today.OpenningBalance + today.CashIn - today.CashOut;
                    db.Entry(today).State = EntityState.Modified;
                    if (saveit) db.SaveChanges();
                }

            }
        }

        public void JobOpeningClosingBalance(AprajitaRetailsContext db)
        {


            Process_OpenningBalance(db, DateTime.Today, true);
            Process_ClosingBalance(db, DateTime.Today, true);
            Process_BankOpenningBalance(db, DateTime.Today, true);
            Process_BankClosingBalance(db, DateTime.Today, true);



        }

        public void CreateNextDayOpenningBalance(AprajitaRetailsContext db, DateTime date, bool saveit = false)
        {
            date = date.AddDays(1);// Next Day

            Process_OpenningBalance(db, date, saveit); //TODO: many lines is repeating so create inline call or make new function
            Process_BankOpenningBalance(db, date, saveit);//TODO: many lines is repeating so create inline call or make new function
        }

        public decimal GetClosingBalance(AprajitaRetailsContext db, DateTime forDate, bool IsBank = false)
        {
            if (IsBank)
            {
                var bal = db.CashInBanks.Where(c => c.CIBDate.Date == forDate.Date).Select(c => new { c.CashIn, c.CashOut, c.OpenningBalance }).FirstOrDefault();
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
                var bal = db.CashInHands.Where(c => c.CIHDate.Date == forDate.Date).Select(c => new { c.CashIn, c.CashOut, c.OpenningBalance }).FirstOrDefault();
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
        public void CashInHandCorrectionForMonth(AprajitaRetailsContext db, DateTime forDate)
        {
            IEnumerable<CashInHand> cashs = db.CashInHands.Where(c => c.CIHDate.Month == forDate.Month).OrderBy(c => c.CIHDate);

            decimal cBal = 0;

            if (cashs != null && cashs.Count() > 0)
            {
                cBal = GetClosingBalance(db, cashs.First().CIHDate);
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

                    // Log.Info("CashInHand Correction failed");
                }

            }

        }

        public void CashInBankCorrectionForMonth(AprajitaRetailsContext db, DateTime forDate)
        {
            IEnumerable<CashInBank> cashs = db.CashInBanks.Where(c => c.CIBDate.Month == forDate.Month).OrderBy(c => c.CIBDate);

            decimal cBal = 0;

            if (cashs != null && cashs.Count() > 0)
            {
                cBal = GetClosingBalance(db, cashs.First().CIBDate);
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

                    // Log.Info("CashInBank Correction failed");
                }

            }

        }



    }
}
