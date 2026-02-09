using GoodStuff.ProductApi.Application.DTO;
using MediatR;

namespace GoodStuff.ProductApi.Application.Features.Product.Queries.GetFilters;

public class GetFiltersQuery : IRequest<CpuFilters?>
{
    public required string Type { get; init; }
}
