using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Models.AprajitaRetails
{
    public class LedgerHead
    {
        public int LedgerHeadId { get; set; }
        public string HeadName { get; set; }
        public bool IsExpense { get; set; }
        public int? ParentId { get; set; }
        public string Description { get; set; }
    }
}


// Head Name: 

/*
 * Rent
 * worshop rent
 * workshop eletric bill
 * Eletric bill
 * Salary
 * Incentive
 * 
 * 
 */

