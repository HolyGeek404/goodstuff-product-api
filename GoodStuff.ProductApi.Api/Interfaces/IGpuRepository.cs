using GoodStuff.ProductApi.Api.Products.Models;

namespace GoodStuff.ProductApi.Api.Interfaces;

public interface IGpuRepository : IReadRepository<Gpu>, IWriteRepository<Gpu>;