using MediatR;

namespace GoodStuff.ProductApi.Application.Features.Product.Queries.GetByType;

public record GetByTypeQuery : IRequest<object?>
{
    public required string Type { get; init; }
}