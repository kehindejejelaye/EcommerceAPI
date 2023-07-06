using Ecommerce.API.Data;
using Ecommerce.API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Ecommerce.API.InitializeDb;

public class DbInitializer
{
    public static async Task SeedDb(IApplicationBuilder app)
    {
        var baseDir = Directory.GetCurrentDirectory();

        var dbContext = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<EcommerceContext>();


        var roleManager = app.ApplicationServices.CreateScope()
              .ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        if (dbContext.Database.GetPendingMigrations().Any())
        {
            dbContext.Database.Migrate();
        }

        if (!dbContext.Users.Any())
        {
            var userManager = app.ApplicationServices.CreateScope()
                  .ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager != null)
            {
                var roles = new List<IdentityRole> {
                    new IdentityRole { Id = "e14dcb16-2fe5-4e1d-a60b-36a843b36229", Name = "Admin" }, 
                    new IdentityRole { Id = "c0e9b3be-d52d-4b73-9250-51db5eaf2a7e", Name = "User" },
                };

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                var usersPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/User.json"));

                var userObjList = JsonConvert.DeserializeObject<List<ApplicationUser>>(usersPath);

                for (int i = 0; i < userObjList?.Count; i++)
                {
                    await userManager.CreateAsync(userObjList[i], $"@Password{i+1}");
                    await userManager.AddToRoleAsync(userObjList[i], roles[i].Name);
                }
            }
        }

        if (!dbContext.Countries.Any())
        {
            var countriesPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/Country.json"));

            var countryObjList = JsonConvert.DeserializeObject<List<Country>>(countriesPath);

            await dbContext.Countries.AddRangeAsync(countryObjList);
        }

        if (!dbContext.Addresses.Any())
        {
            var addressesPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/Address.json"));

            var addressObjList = JsonConvert.DeserializeObject<List<Address>>(addressesPath);

            await dbContext.Addresses.AddRangeAsync(addressObjList);
        }

        if (!dbContext.Categories.Any())
        {
            var countriesPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/Category.json"));

            var countriesObjList = JsonConvert.DeserializeObject<List<Category>>(countriesPath);
            await dbContext.Categories.AddRangeAsync(countriesObjList);
        }

        if (!dbContext.Products.Any())
        {
            var productsPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/Product.json"));

            var productsObjList = JsonConvert.DeserializeObject<List<Product>>(productsPath);
            await dbContext.Products.AddRangeAsync(productsObjList);
        }

        if (!dbContext.ProductItems.Any())
        {
            var productItemsPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/ProductItem.json"));

            var productItemsObjList = JsonConvert.DeserializeObject<List<ProductItem>>(productItemsPath);
            await dbContext.ProductItems.AddRangeAsync(productItemsObjList);
        }

        if (!dbContext.Variants.Any())
        {
            var variantsPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/Variant.json"));

            var variantsObjList = JsonConvert.DeserializeObject<List<Variant>>(variantsPath);
            await dbContext.Variants.AddRangeAsync(variantsObjList);
        }

        if (!dbContext.VariantOptions.Any())
        {
            var variantOptionsPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/VariantOption.json"));

            var variantOptionsObjList = JsonConvert.DeserializeObject<List<VariantOption>>(variantOptionsPath);
            await dbContext.VariantOptions.AddRangeAsync(variantOptionsObjList);
        }

        if (!dbContext.ProductItemVariantOptions.Any())
        {
            var pivoPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/ProductItemVariantOption.json"));

            var pivoObjList = JsonConvert.DeserializeObject<List<ProductItemVariantOption>>(pivoPath);
            await dbContext.ProductItemVariantOptions.AddRangeAsync(pivoObjList);
        }

        if (!dbContext.OrderStatus.Any())
        {
            var osPath = File.ReadAllText(Path.Combine(baseDir, "JsonFilesForSeeding/OrderStatus.json"));
            
            var osObjList = JsonConvert.DeserializeObject<List<OrderStatus>>(osPath);
            await dbContext.OrderStatus.AddRangeAsync(osObjList);
        }

        //Saving everything into the database
        await dbContext.SaveChangesAsync();
    }
}
