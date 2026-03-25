using GoodStuff.ProductApi.Api.Models;

namespace GoodStuff.ProductApi.Api.Interfaces;

public interface ICoolerRepository : IReadRepository<Cooler>, IWriteRepository<Cooler>;