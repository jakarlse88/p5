using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace TheCarHub.Data 
{
    public static class IdentitySeedData
    {
        private const string AdminUser = "admin";
        private const string AdminPassword = "P@assword123";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = 
                    (UserManager<IdentityUser>)scope
                    .ServiceProvider
                    .GetService(typeof(UserManager<IdentityUser>));

                IdentityUser user = await userManager.FindByIdAsync(AdminUser);

                if (user == null) 
                {
                    user = new IdentityUser("admin");
                    await userManager.CreateAsync(user, AdminPassword);
                }
            }
        }
    }
}