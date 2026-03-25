using System.Net;
using GoodStuff.ProductApi.Api.Interfaces;
using GoodStuff.ProductApi.Api.Products;
using MediatR;

namespace GoodStuff.ProductApi.Api.Features.Product.Commands.Delete;

public class DeleteCommandHandler(IUnitOfWork uow) : IRequestHandler<DeleteCommand, HttpStatusCode>
{
    public async Task<HttpStatusCode> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return request.Type.ToUpper() switch
        {
            ProductCategories.Gpu => await uow.GpuRepository.DeleteAsync(request.Id, request.Type),
            ProductCategories.Cpu => await uow.CpuRepository.DeleteAsync(request.Id, request.Type),
            ProductCategories.Cooler => await uow.CoolerRepository.DeleteAsync(request.Id, request.Type),
            _ => HttpStatusCode.BadRequest
        };
    }
}