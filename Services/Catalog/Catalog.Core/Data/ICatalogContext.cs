using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data;

// Switch ICatalogContext abstraction to EF Core so design-time tools can create CatalogDbContext cleanly.
public interface ICatalogContext
{
    DbSet<Product> Products { get; }
    DbSet<ProductBrand> ProductBrands { get; }
    DbSet<ProductType> ProductTypes { get; }
}
