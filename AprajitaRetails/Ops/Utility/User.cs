using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace AprajitaRetails.Ops.Utility
{
    public static class UserAdmin
    {


        public static async Task<bool> AddUserAsync(UserManager<IdentityUser> userManager, string UserName, bool isPowerUser)
        {
            var user = new IdentityUser { UserName = UserName, Email = UserName + "@aprajitaretails.in" };
            var result = await userManager.CreateAsync (user, UserName + "@1234").ConfigureAwait (true);
            if ( result.Succeeded )
            {
                //here we tie the new user to the "Admin" role 
                if ( isPowerUser )
                    await userManager.AddToRoleAsync (user, "StoreManager").ConfigureAwait (true);
                else
                    await userManager.AddToRoleAsync (user, "Salesman").ConfigureAwait (true);
                //TODO: Need to Update Confirmed Email. 

                var userId = await userManager.GetUserIdAsync (user).ConfigureAwait (true);
                var code = await userManager.GenerateEmailConfirmationTokenAsync (user).ConfigureAwait (true);
                var result2 = await userManager.ConfirmEmailAsync (user, code).ConfigureAwait (true);
                return true;
            }



            return false;
        }
    }
}
