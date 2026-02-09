using GoodStuff.ProductApi.Domain.Products.Models;

namespace GoodStuff.ProductApi.Application.Interfaces;

public interface IGpuRepository : IReadRepository<Gpu>, IWriteRepository<Gpu>;