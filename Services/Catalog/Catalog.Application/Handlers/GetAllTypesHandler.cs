

using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Repositories;
using MediatR;

namespace Catalog.Application.Handlers;
public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuerry, IList<TypesResponse>>
{
    private readonly ITypesRepository _typeRepository;
    public GetAllTypesHandler(ITypesRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }
    public async Task<IList<TypesResponse>> Handle(GetAllTypesQuerry request, CancellationToken cancellationToken)
    {
        var typesList = await _typeRepository.GetAllTypes();
        var typesResponse = ProductMapper.Mapper.Map<IList<TypesResponse>>(typesList);
        return typesResponse;
    }
}
