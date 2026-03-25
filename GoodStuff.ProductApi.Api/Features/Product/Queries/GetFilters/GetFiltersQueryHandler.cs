using GoodStuff.ProductApi.Application.DTO;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products;
using MediatR;

namespace GoodStuff.ProductApi.Application.Features.Product.Queries.GetFilters;

public class GetFiltersQueryHandler(IUnitOfWork uow) : IRequestHandler<GetFiltersQuery, CpuFilters?>
{
    public async Task<CpuFilters?> Handle(GetFiltersQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return request.Type switch
        {
            ProductCategories.Cpu => await uow.CpuRepository.GetFiltersAsync(request.Type),
            _ => null
        };
    }
}
