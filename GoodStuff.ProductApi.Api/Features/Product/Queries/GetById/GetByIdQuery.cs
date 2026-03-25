using MediatR;

namespace GoodStuff.ProductApi.Application.Features.Product.Queries.GetById;

public class GetByIdQuery : IRequest<object?>
{
    public required string Type { get; init; }
    public required string Id { get; init; }
}