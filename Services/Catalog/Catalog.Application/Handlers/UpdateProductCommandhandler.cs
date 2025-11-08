using Catalog.Application.Commands;
using Catalog.Application.Mappers;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductCommandhandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;
    public UpdateProductCommandhandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Temporary implementation compatible with new EF model: only map scalar fields,
        // ignore brand/type FKs (not present on UpdateProductCommand).
        if (!int.TryParse(request.Id, out var intId))
            throw new ArgumentException("Id must be an integer for SQL-based Catalog.", nameof(request.Id));

        var updated = await _productRepository.UpdateProduct(new Product
        {
            Id = intId,
            Name = request.Name,
            Summary = request.Summary,
            Description = request.Description,
            ImageFile = request.ImageFile,
            Price = request.Price
        });

        return updated;
    }
}
