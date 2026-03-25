using System.Net;
using System.Text.Json;
using GoodStuff.ProductApi.Api.Interfaces;
using GoodStuff.ProductApi.Api.Products;
using GoodStuff.ProductApi.Api.Products.Models;
using MediatR;

namespace GoodStuff.ProductApi.Api.Features.Product.Commands.Update;

public class UpdateCommandHandler(IUnitOfWork uow) : IRequestHandler<UpdateCommand, HttpStatusCode>
{
    public async Task<HttpStatusCode> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        switch (request.Type.ToUpper())
        {
            case ProductCategories.Gpu:
                var gpu = request.BaseProduct.Deserialize<Gpu>()!;
                return await uow.GpuRepository.UpdateAsync(gpu, gpu.id, gpu.Category);
            case ProductCategories.Cpu:
                var cpu = request.BaseProduct.Deserialize<Cpu>()!;
                return await uow.CpuRepository.UpdateAsync(cpu, cpu.id, cpu.Category);
            case ProductCategories.Cooler:
                var cooler = request.BaseProduct.Deserialize<Cooler>()!;
                return await uow.CoolerRepository.UpdateAsync(cooler, cooler.id, cooler.Category);
            default:
                return HttpStatusCode.BadRequest;
        }
    }
}