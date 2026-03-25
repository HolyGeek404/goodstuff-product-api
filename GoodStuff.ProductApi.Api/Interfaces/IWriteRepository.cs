using System.Net;
using GoodStuff.ProductApi.Domain.Products.Models;

namespace GoodStuff.ProductApi.Application.Interfaces;

public interface IWriteRepository<TProduct>
{
    public Task<BaseProduct?> CreateAsync(TProduct entity, string id, string pk);
    public Task<HttpStatusCode> UpdateAsync(TProduct entity, string id, string pk);
    public Task<HttpStatusCode> DeleteAsync(Guid id, string partitionKey);
}