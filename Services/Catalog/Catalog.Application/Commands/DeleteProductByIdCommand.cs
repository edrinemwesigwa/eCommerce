using MediatR;

namespace Catalog.Application.Commands;
public class DeleteProductByIdCommand : IRequest<bool>
{   
    public string Id { get; }
    public DeleteProductByIdCommand(string id)
    {
        Id = id;
    }
}
