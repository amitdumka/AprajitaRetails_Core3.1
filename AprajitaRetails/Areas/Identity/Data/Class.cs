using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AprajitaRetails.Areas.Identity.Data
{
    public class ARUser : IdentityUser
    {
        [PersonalData]
        public int EmployeeId { get; set; }
        [PersonalData]
        public bool IsWorking { get; set; }
    }
}
