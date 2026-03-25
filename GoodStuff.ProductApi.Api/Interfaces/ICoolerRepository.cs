using GoodStuff.ProductApi.Domain.Products.Models;

namespace GoodStuff.ProductApi.Application.Interfaces;

public interface ICoolerRepository : IReadRepository<Cooler>, IWriteRepository<Cooler>;