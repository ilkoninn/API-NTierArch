using App.Core.Entities.Identity;
using App.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.Presistence
{
    public static class AppDbContextSeed
    {
        public static async Task SeedDatabaseAsync(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(EUserRoles)).Cast<EUserRoles>().Select(x => x.ToString()))
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            if (!userManager.Users.Any())
            {
                var user = new AppUser { UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };

                await userManager.CreateAsync(user, "Admin123.?");
                await userManager.AddToRoleAsync(user, EUserRoles.SuperAdmin.ToString());
            }

            await context.SaveChangesAsync();
        }
    }
}
