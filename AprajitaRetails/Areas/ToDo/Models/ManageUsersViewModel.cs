using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TodoList.Web.Models
{
    public class ManageUsersViewModel
    {
        public IEnumerable<IdentityUser> Administrators { get; set; }
        public IEnumerable<IdentityUser> Users { get; set; }
    }
}
