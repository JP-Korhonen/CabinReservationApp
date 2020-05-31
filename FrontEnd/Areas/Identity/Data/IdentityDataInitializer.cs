using Microsoft.AspNetCore.Identity;

namespace FrontEnd.Areas.Identity.Data
{
    // http://www.binaryintellect.net/articles/5e180dfa-4438-45d8-ac78-c7cc11735791.aspx
    public class IdentityDataInitializer
    {
        public static void SeedData(UserManager<FrontEndUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<FrontEndUser> userManager)
        {
            if (userManager.FindByNameAsync("admin@localhost.org").Result == null)
            {
                FrontEndUser user = new FrontEndUser();
                user.UserName = "admin@localhost.org";
                user.Email = "admin@localhost.org";

                IdentityResult result = userManager.CreateAsync
                (user, "Qwerty12#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Administrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Customer").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Customer";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("CabinOwner").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "CabinOwner";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
