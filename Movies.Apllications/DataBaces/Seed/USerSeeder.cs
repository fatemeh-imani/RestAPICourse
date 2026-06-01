using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Movies.Applications.DataBaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Applications.DataBaces.Seed
{
    public static class USerSeeder
    {
        public static async Task EnsureAsync(IServiceProvider sp)
        {
            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();

            var userId = "d8566de3-b1a6-4a9b-b842-8e3887a82e41";
            var email = "nick@nickchapsas.com";

            var user = await userManager.FindByIdAsync(userId);
            if (user is not null) return;

            user = new ApplicationUser
            {
                Id = userId,              // مهم: همون userid داخل توکن
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "Password123!");
            if (!result.Succeeded)
                throw new Exception(string.Join(" | ", result.Errors.Select(e => e.Description)));
        }
    }
}
