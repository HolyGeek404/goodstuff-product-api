using System.Net;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products.Models;
using Microsoft.Azure.Cosmos;

namespace GoodStuff.ProductApi.Infrastructure.Repositories;

public class CosmosRepository<TProduct>(CosmosClient cosmosClient) : IReadRepository<TProduct>, IWriteRepository<TProduct>
{
    protected readonly Container Container = cosmosClient.GetContainer("GoodStuff", "Products");

    public async Task<IEnumerable<TProduct>> GetByType(string category)
    {
        var query = QueryBuilder.SelectAllProductsByType(category);
        var iterator = Container.GetItemQueryIterator<TProduct>(query);
        var results = new List<TProduct>();
        while (iterator.HasMoreResults)
        {
            var item = await iterator.ReadNextAsync();
            results.AddRange(item.Resource);
        }

        return results;
    }

    public async Task<TProduct?> GetById(string category, string id)
    {
        var query = QueryBuilder.SelectSingleProductById(category, id);
        var iterator = Container.GetItemQueryIterator<TProduct>(query);
        var results = await iterator.ReadNextAsync();
        return results.Resource.FirstOrDefault();
    }

    public async Task<BaseProduct?> CreateAsync(TProduct entity, string id, string pk)
    {
        var partitionKey = new PartitionKey(pk);
        try
        {
            var result = await Container.CreateItemAsync(entity, partitionKey);
            return result.Resource as BaseProduct;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null;
    }

    public async Task<HttpStatusCode> UpdateAsync(TProduct entity, string id, string pk)
    {
        var partitionKey = new PartitionKey(pk);
        try
        {
            var result = await Container.ReplaceItemAsync(entity, id, partitionKey);
            return result.StatusCode;
        }
        catch (Exception)
        {
            return HttpStatusCode.NotFound;
        }
    }

    public async Task<HttpStatusCode> DeleteAsync(Guid id, string partitionKey)
    {
        var result = await Container.DeleteItemAsync<TProduct>(id.ToString(), new PartitionKey(partitionKey));
        return result.StatusCode;
    }
}