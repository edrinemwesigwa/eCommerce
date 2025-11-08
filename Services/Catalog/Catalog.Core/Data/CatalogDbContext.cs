using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Data;

public class CatalogDbContext : DbContext
{
    public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductBrand> ProductBrands { get; set; } = null!;
    public DbSet<ProductType> ProductTypes { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductBrand>(b =>
        {
            b.ToTable("ProductBrands");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<ProductType>(t =>
        {
            t.ToTable("ProductTypes");
            t.HasKey(x => x.Id);
            t.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Product>(p =>
        {
            p.ToTable("Products");
            p.HasKey(x => x.Id);
            p.Property(x => x.Name).IsRequired().HasMaxLength(180);
            p.Property(x => x.Summary).IsRequired().HasMaxLength(180);
            p.Property(x => x.Description).IsRequired();
            p.Property(x => x.ImageFile).IsRequired().HasMaxLength(150);
            p.Property(x => x.Price).HasColumnType("decimal(18,2)");

            // Relationships now use FK properties on Product
            p.HasOne(x => x.ProductBrand)
                .WithMany()
                .HasForeignKey(x => x.ProductBrandId)
                .OnDelete(DeleteBehavior.Restrict);

            p.HasOne(x => x.ProductType)
                .WithMany()
                .HasForeignKey(x => x.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}