namespace Catalog.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Summary { get; set; }
    public string Description { get; set; }
    public string ImageFile { get; set; }

    // SQL/EF Core navigation + FKs
    public decimal Price { get; set; }

    public int ProductBrandId { get; set; }
    public ProductBrand ProductBrand { get; set; }

    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
}
