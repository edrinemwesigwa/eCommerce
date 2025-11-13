

using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

// EF Core implementation of product/brand/type repositories using CatalogDbContext.
public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
{
    private readonly CatalogDbContext _db;

    public ProductRepository(CatalogDbContext db) => _db = db;
   

    // PRODUCTS

    public async Task<Product> GetProduct(string id)
    {
        if (!int.TryParse(id, out var intId))
            throw new ArgumentException("Id must be an integer for SQL-based Catalog.", nameof(id));

        return await _db.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .FirstOrDefaultAsync(p => p.Id == intId);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _db.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrand(string brandName)
    {
        return await _db.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .Where(p => p.ProductBrand.Name.ToLower() == brandName.ToLower())
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        return await _db.Products
            .Include(p => p.ProductBrand)
            .Include(p => p.ProductType)
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        if (!int.TryParse(id, out var intId))
            return false;

        var entity = await _db.Products.FindAsync(intId);
        if (entity == null)
            return false;

        _db.Products.Remove(entity);
        return await _db.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        _db.Products.Update(product);
        return await _db.SaveChangesAsync() > 0;
    }

    // BRANDS

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _db.ProductBrands.ToListAsync();
    }

    // TYPES

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _db.ProductTypes.ToListAsync();
    }
}
