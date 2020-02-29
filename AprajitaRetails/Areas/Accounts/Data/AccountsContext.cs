using AprajitaRetails.Areas.Accounts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Accounts.Data
{
    public class AccountsContext:DbContext
    {

        public AccountsContext(DbContextOptions<AccountsContext> options) : base(options)
        {
        }

        public DbSet<LedgerMaster> Masters { get; set; }
        public DbSet<Party> Parties { get; set; }
       // public DbSet<LedgerEntry> LedgerEntries { get; set; }
    }
}
