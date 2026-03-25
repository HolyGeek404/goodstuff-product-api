using System.Net;
using System.Text.Json;
using GoodStuff.ProductApi.Api.Interfaces;
using GoodStuff.ProductApi.Api.Models;

namespace GoodStuff.ProductApi.Api.Services;

public class ProductServiceUnit(IUnitOfWork uow) : IProductService
{
    #region Queries
    public async Task<object?> GetByIdAndType(string type, string id)
    {
        return type switch
        {
            ProductCategories.Gpu => await uow.GpuRepository.GetById(type, id),
            ProductCategories.Cpu => await uow.CpuRepository.GetById(type, id),
            ProductCategories.Cooler => await uow.CoolerRepository.GetById(type, id),
            _ => null
        };
    }
    public async Task<object?> GetByType(string type)
    {
        return type switch
        {
            ProductCategories.Gpu => await uow.GpuRepository.GetByType(type),
            ProductCategories.Cpu => await uow.CpuRepository.GetByType(type),
            ProductCategories.Cooler => await uow.CoolerRepository.GetByType(type),
            _ => null
        };
    }
    public async Task<object?> GetFilterByType(string type)
    {
        return type switch
        {
            ProductCategories.Cpu => await uow.CpuRepository.GetFiltersAsync(type),
            _ => null
        };
    }
    #endregion

    #region Commands
    public async Task<BaseProduct?> Create(JsonElement product, string type)
    {
        switch (type.ToUpper())
        {
            case ProductCategories.Gpu:
                var gpu = product.Deserialize<Gpu>()!;
                return await uow.GpuRepository.CreateAsync(gpu, gpu.id, gpu.Category);
            case ProductCategories.Cpu:
                var cpu = product.Deserialize<Cpu>()!;
                return await uow.CpuRepository.CreateAsync(cpu, cpu.id, cpu.Category);
            case ProductCategories.Cooler:
                var cooler = product.Deserialize<Cooler>()!;
                return await uow.CoolerRepository.CreateAsync(cooler, cooler.id, cooler.Category);
            default:
                return null;
        }
    }
    public async Task<HttpStatusCode> Delete(Guid id, string type)
    {
        return type.ToUpper() switch
        {
            ProductCategories.Gpu => await uow.GpuRepository.DeleteAsync(id, type),
            ProductCategories.Cpu => await uow.CpuRepository.DeleteAsync(id, type),
            ProductCategories.Cooler => await uow.CoolerRepository.DeleteAsync(id, type),
            _ => HttpStatusCode.BadRequest
        };
    }
    public async Task<HttpStatusCode> Update(JsonElement product, string type)
    {
        switch (type.ToUpper())
        {
            case ProductCategories.Gpu:
                var gpu = product.Deserialize<Gpu>()!;
                return await uow.GpuRepository.UpdateAsync(gpu, gpu.id, gpu.Category);
            case ProductCategories.Cpu:
                var cpu = product.Deserialize<Cpu>()!;
                return await uow.CpuRepository.UpdateAsync(cpu, cpu.id, cpu.Category);
            case ProductCategories.Cooler:
                var cooler = product.Deserialize<Cooler>()!;
                return await uow.CoolerRepository.UpdateAsync(cooler, cooler.id, cooler.Category);
            default:
                return HttpStatusCode.BadRequest;
        }
    }
    #endregion
}