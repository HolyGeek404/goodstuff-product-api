using System.Net;
using System.Text.Json;
using GoodStuff.ProductApi.Api.Products.Models;

namespace GoodStuff.ProductApi.Api.Services;

public interface IProductService
{
    Task<object?> GetByIdAndType(string type, string id);
    Task<object?> GetByType(string type);
    Task<object?> GetFilterByType(string type);
    Task<BaseProduct?> Create(JsonElement product, string type);
    Task<HttpStatusCode> Delete(Guid id, string type);
    Task<HttpStatusCode> Update(JsonElement product, string type);
}