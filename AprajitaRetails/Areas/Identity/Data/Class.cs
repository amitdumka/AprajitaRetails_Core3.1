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
