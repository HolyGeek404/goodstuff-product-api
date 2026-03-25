using GoodStuff.ProductApi.Api.DTO;
using GoodStuff.ProductApi.Api.Products.Models;

namespace GoodStuff.ProductApi.Api.Interfaces;

public interface ICpuRepository : IReadRepository<Cpu>, IWriteRepository<Cpu>
{
    Task<CpuFilters> GetFiltersAsync(string category);
}
