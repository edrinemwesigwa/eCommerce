using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data;
public static  class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> productCollection)
    {
        bool checkProducts = productCollection.Find(p => true).Any();
        //string path = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedData/products.json");
        if (!checkProducts)
        {
            var productsData = File.ReadAllText("../Catalog/Catalog.Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products != null && products.Any())
            {
                foreach (var product in products)
                {
                    productCollection.InsertOneAsync(product);
                }
            }

        } 
        //bool checkProducts = productCollection.Find(p => true).Any();
        //string path = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedData/products.json");
        //if (!checkProducts)
        //{
        //    var productsData = File.ReadAllText(path);
        //    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
        //    if (products != null && products.Any())
        //    {
        //        foreach (var product in products)
        //        {
        //            productCollection.InsertOneAsync(product);
        //        }
        //    }

        //} 
    }
}
