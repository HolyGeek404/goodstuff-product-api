using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products;
using MediatR;

namespace GoodStuff.ProductApi.Application.Features.Product.Queries.GetByType;

public class GetByTypeQueryHandler(IUnitOfWork uow) : IRequestHandler<GetByTypeQuery, object?>
{
    public async Task<object?> Handle(GetByTypeQuery request, CancellationToken cancellationToken)
    {
        return request.Type switch
        {
            ProductCategories.Gpu => await uow.GpuRepository.GetByType(request.Type),
            ProductCategories.Cpu => await uow.CpuRepository.GetByType(request.Type),
            ProductCategories.Cooler => await uow.CoolerRepository.GetByType(request.Type),
            _ => null
        };
    }
}