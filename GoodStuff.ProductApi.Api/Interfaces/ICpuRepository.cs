using GoodStuff.ProductApi.Api.Models;

namespace GoodStuff.ProductApi.Api.Interfaces;

public interface ICpuRepository : IReadRepository<Cpu>, IWriteRepository<Cpu>
{
    Task<CpuFilters> GetFiltersAsync(string category);
}
