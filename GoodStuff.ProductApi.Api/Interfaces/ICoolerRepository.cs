using GoodStuff.ProductApi.Api.Products.Models;

namespace GoodStuff.ProductApi.Api.Interfaces;

public interface ICoolerRepository : IReadRepository<Cooler>, IWriteRepository<Cooler>;