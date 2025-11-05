using System;
using InventorySystem.Models;
using Microsoft.AspNetCore.Identity;

namespace InventorySystem.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var idService = scope.ServiceProvider.GetRequiredService<InventorySystem.Services.CustomIdService>();

            string[] roles = new[] { "Admin", "Creator", "User" };
            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                {
                    await roleManager.CreateAsync(new IdentityRole(r));
                }
            }

            // Create default admin user if not exists. Credentials can be provided via environment variables:
            // SEED_ADMIN_EMAIL and SEED_ADMIN_PASSWORD
            var adminEmail = Environment.GetEnvironmentVariable("SEED_ADMIN_EMAIL") ?? "admin@local";
            var adminPassword = Environment.GetEnvironmentVariable("SEED_ADMIN_PASSWORD") ?? "Admin123!";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true, FullName = "Administrator" };
                var result = await userManager.CreateAsync(admin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
                else
                {
                    // If creation failed, log errors to console for diagnostics in dev.
                    foreach (var err in result.Errors)
                    {
                        Console.WriteLine($"[DbInitializer] Admin create error: {err.Code} - {err.Description}");
                    }
                }
            }

            // Seed demo inventories and items if none exist
            if (!db.Inventories.Any())
            {
                var inv1 = new Inventory { Name = "Office Supplies", Description = "Pens, paper, and office essentials", OwnerId = admin.Id };
                var inv2 = new Inventory { Name = "Electronics", Description = "Laptops, monitors, accessories", OwnerId = admin.Id };
                db.Inventories.AddRange(inv1, inv2);
                await db.SaveChangesAsync();

                // Create sample items using CustomIdService to generate CustomId
                var item1 = new Item { InventoryId = inv1.Id, Name = "Ballpoint Pen", CustomFieldsJson = "{\"color\":\"blue\"}" };
                item1.CustomId = await idService.GenerateAsync(inv1.Id);
                var item2 = new Item { InventoryId = inv1.Id, Name = "A4 Paper Pack", CustomFieldsJson = "{\"count\":500}" };
                item2.CustomId = await idService.GenerateAsync(inv1.Id);

                var e1 = new Item { InventoryId = inv2.Id, Name = "14-inch Laptop", CustomFieldsJson = "{\"ram\":\"16GB\",\"ssd\":\"512GB\"}" };
                e1.CustomId = await idService.GenerateAsync(inv2.Id);
                var e2 = new Item { InventoryId = inv2.Id, Name = "USB-C Dock", CustomFieldsJson = "{\"ports\":6}" };
                e2.CustomId = await idService.GenerateAsync(inv2.Id);

                db.Items.AddRange(item1, item2, e1, e2);
                await db.SaveChangesAsync();
            }
        }
    }
}
