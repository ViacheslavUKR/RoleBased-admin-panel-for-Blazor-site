using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAdminPanel.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAdminPanel
{
    public class Seed
    {
        public static void AddTestUsers(DbContextOptions<ApplicationDbContext> identityDbContextOptions, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            using (var db = new ApplicationDbContext(identityDbContextOptions))
            {
                db.Database.EnsureCreated();
            }

            var user = userManager.FindByEmailAsync("user1@example.com").GetAwaiter().GetResult();
            if (user == null)
            {
                var user1 = new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "user1@example.com",
                    EmailConfirmed = true,
                    UserName = "user1@example.com"
                };
                var res = userManager.CreateAsync(user1, "user1").GetAwaiter().GetResult();
                if (res.Errors.Count() > 0)
                {
                    throw new Exception();
                }
                else 
                    user = userManager.FindByEmailAsync("user1@example.com").GetAwaiter().GetResult();
            }

            var admin = userManager.FindByEmailAsync("admin@example.com").GetAwaiter().GetResult();
            if (admin == null)
            {
                var admin1 = new IdentityUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "admin@example.com",
                    EmailConfirmed = true,
                    UserName = "admin@example.com"
                };
                var res = userManager.CreateAsync(admin1, "admin1").GetAwaiter().GetResult();
                if (res.Errors.Count() > 0)
                {
                    throw new Exception();
                }
                else 
                    admin = userManager.FindByEmailAsync("admin@example.com").GetAwaiter().GetResult();
            }

            if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
            }

            if (!userManager.IsInRoleAsync(admin, "Admin").GetAwaiter().GetResult())
            {
                userManager.AddToRoleAsync(admin, "Admin").GetAwaiter().GetResult();
            }
        }
    }
}