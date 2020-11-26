using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        //28. Create new class with static method
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                //28. If no product brands currently in context...
                if (!context.ProductBrands.Any())
                {
                    //28. read all brands data from json file in solution, deserialize and add all data for each into current context, then save...
                    var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                //28. If no product types currently in context...
                if (!context.ProductTypes.Any())
                {
                    //28. read all types data from json file in solution, deserialize and add all data for each into current context, then save...
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                //28. If no products currently in context...
                if (!context.Products.Any())
                {
                    //28. read all product data from json file in solution, deserialize and add all data for each into current context, then save...
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }
                    await context.SaveChangesAsync();
                }

                //210. 
                if (!context.DeliveryMethods.Any())
                {
                    //210. read all delivery.json file in solution, deserialize and add all data for each into current context, then save...
                    var dmData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);
                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}