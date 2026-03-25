using GoodStuff.ProductApi.Application.DTO;
using GoodStuff.ProductApi.Domain.Products.Models;

namespace GoodStuff.ProductApi.Application.Interfaces;

public interface ICpuRepository : IReadRepository<Cpu>, IWriteRepository<Cpu>
{
    Task<CpuFilters> GetFiltersAsync(string category);
}
