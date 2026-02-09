using GoodStuff.ProductApi.Domain.Products.Models;

namespace GoodStuff.ProductApi.Application.Interfaces;

public interface ICpuRepository : IReadRepository<Cpu>, IWriteRepository<Cpu>;