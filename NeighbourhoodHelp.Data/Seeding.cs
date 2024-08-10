using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NeighbourhoodHelp.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeighbourhoodHelp.Data
{
    public class UserandRolesInitializedData
    {
        public static async Task SeedData(ApplicationDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context);
        }

        private static async Task SeedUsers(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            await CreateAndAssignUser(userManager, "David", "Ogwuche", "dvd@gmail.com", "Ysb123@32", "2349064056077", "Makurdi", "Benue", "Ohen", 5, "User");
            await CreateAndAssignUser(userManager, "Michael", "Batowei", "codedvd@gmail.com", "Ysb123@32", "+2349018015592", "PH", "Porthacort", "Ohen", 4, "Admin");
            await CreateAndAssignUser(userManager, "Ebuwa", "Iguobaro", "ebuwa@gmail.com", "Ysb123@32", "+2349018015592", "PH", "Porthacort", "Garden Estate", 3, "Agent");
            await CreateAndAssignUser(userManager, "Flo", "Uyot", "florentinaantigha27@gmail.com", "Ysb123@32", "+2349013690696", "Abj", "Abuja", "Army Estate", 4, "User");
        }

        private static async Task CreateAndAssignUser(UserManager<AppUser> userManager, string firstName, string lastName, string email, string password, string phoneNumber, string city, string state, string street, int rating, string role)
        {
            if (!userManager.Users.Any(u => u.Email == email))
            {
                var user = new AppUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PostalCode = "12345",
                    Image = "openForCorrection",
                    UserName = email,
                    PhoneNumber = phoneNumber,
                    Street = street,
                    City = city,
                    State = state
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.RoleExistsAsync("Admin").Result == false)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };

                await roleManager.CreateAsync(role);
            }
            if (roleManager.RoleExistsAsync("User").Result == false)
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };

                await roleManager.CreateAsync(role);
            }

            if (roleManager.RoleExistsAsync("Agent").Result == false)
            {
                var role = new IdentityRole
                {
                    Name = "Agent"
                };

                await roleManager.CreateAsync(role);
            }
        }
    }
}
