using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Areas.Admin.Ops
{
    public static class Seed
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>> ();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>> ();
            string [] roleNames = { "Admin", "StoreManager", "Salesman", "Accountant", "RemoteAccountant", "Member", "PowerUser" };
            IdentityResult roleResult;
            foreach ( var roleName in roleNames )
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync (roleName);
                if ( !roleExist )
                {
                    roleResult = await RoleManager.CreateAsync (new IdentityRole (roleName));
                }
            }
            //creating a super user who could maintain the web app
            var poweruser = new IdentityUser
            {
                UserName = "Admin",
                Email = "Admin@AprajitaRetails.In"
            };
            string UserPassword = "Admin@1234";
            var _user = await UserManager.FindByEmailAsync ("Admin@AprajitaRetails.In");
            if ( _user == null )
            {
                var createPowerUser = await UserManager.CreateAsync (poweruser, UserPassword);
                if ( createPowerUser.Succeeded )
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync (poweruser, "Admin");

                    //Need to Update Confirmed Email. 

                    _ = await UserManager.GetUserIdAsync (poweruser);
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync (poweruser);
                    _ = await UserManager.ConfirmEmailAsync (poweruser, code);
                }
            }
        }

    }//end of Seed

}
