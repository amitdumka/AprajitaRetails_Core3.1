using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

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

                _ = await userManager.GetUserIdAsync (user).ConfigureAwait (true);
                var code = await userManager.GenerateEmailConfirmationTokenAsync (user).ConfigureAwait (true);

                _ = await userManager.ConfirmEmailAsync (user, code).ConfigureAwait (true);
                return true;
            }



            return false;
        }
    }
}
