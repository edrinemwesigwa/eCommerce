using Catalog.Core.Entities;
using MongoDB.Driver;
using MongoDB.Driver.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data;
public static class BrandContextSeed
{
    public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
    {
        bool checkBrands = brandCollection.Find(p => true).Any();
        if (!checkBrands)
        {
            //var brandsData = File.ReadAllText(path);
            //var brands  =  JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            //if (brands != null && brands.Any())
            //{
            //    foreach (var brand in brands)
            //    {
            //        brandCollection.InsertOneAsync(brand);
            //    }
            //}

            var brandsData = File.ReadAllText("../Catalog/Catalog.Infrastructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if (brands != null && brands.Any())
            {
                foreach (var brand in brands)
                {
                    brandCollection.InsertOneAsync(brand);
                }
            }
        }
        //bool checkBrands = brandCollection.Find(p => true).Any();
        //string path = Path.Combine(Directory.GetCurrentDirectory(), "Data/SeedData/brands.json");
        //if (!checkBrands)
        //{
        //    var brandsData = File.ReadAllText(path);
        //    var brands  =  JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
        //    if (brands != null && brands.Any())
        //    {
        //        foreach (var brand in brands)
        //        {
        //            brandCollection.InsertOneAsync(brand);
        //        }
        //    }
        //}
    }
}
