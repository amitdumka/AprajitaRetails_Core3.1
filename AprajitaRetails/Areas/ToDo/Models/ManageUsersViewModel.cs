using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace TodoList.Web.Models
{
    public class ManageUsersViewModel
    {
        public IEnumerable<IdentityUser> Administrators { get; set; }
        public IEnumerable<IdentityUser> Users { get; set; }
    }
}
