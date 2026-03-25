using GoodStuff.ProductApi.Api.Models;

namespace GoodStuff.ProductApi.Api.Interfaces;

public interface IGpuRepository : IReadRepository<Gpu>, IWriteRepository<Gpu>;