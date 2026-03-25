using GoodStuff.ProductApi.Api.Interfaces;
using GoodStuff.ProductApi.Api.Products;

namespace GoodStuff.ProductApi.Api.Services;

public class ProductService(IUnitOfWork uow)
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
    
    
}