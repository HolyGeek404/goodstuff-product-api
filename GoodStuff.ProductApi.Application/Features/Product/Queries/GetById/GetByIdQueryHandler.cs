using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products;
using MediatR;

namespace GoodStuff.ProductApi.Application.Features.Product.Queries.GetById;

public class GetByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetByIdQuery, object?>
{
    public async Task<object?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return request.Type switch
        {
            ProductCategories.Gpu => await uow.GpuRepository.GetById(request.Type, request.Id),
            ProductCategories.Cpu => await uow.CpuRepository.GetById(request.Type, request.Id),
            ProductCategories.Cooler => await uow.CoolerRepository.GetById(request.Type, request.Id),
            _ => Enumerable.Empty<object>()
        };
    }
}