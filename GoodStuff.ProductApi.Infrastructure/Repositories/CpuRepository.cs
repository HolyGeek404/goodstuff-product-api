using GoodStuff.ProductApi.Application.DTO;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products.Models;
using Microsoft.Azure.Cosmos;

namespace GoodStuff.ProductApi.Infrastructure.Repositories;

public class CpuRepository(CosmosClient cosmosClient) : CosmosRepository<Cpu>(cosmosClient), ICpuRepository
{
    public async Task<CpuFilters> GetFiltersAsync(string category)
    {
        var queries = QueryBuilder.GetFilterParams(category).ToList();
        if (queries.Count < 5)
        {
            return new CpuFilters
            {
                Team = [],
                Cores = [],
                Socket = [],
                Architecture = [],
                TDP = []
            };
        }

        return new CpuFilters
        {
            Team = await GetDistinctValuesAsync(queries[0]),
            Cores = await GetDistinctValuesAsync(queries[1]),
            Socket = await GetDistinctValuesAsync(queries[2]),
            Architecture = await GetDistinctValuesAsync(queries[3]),
            TDP = await GetDistinctValuesAsync(queries[4])
        };
    }

    private async Task<string[]> GetDistinctValuesAsync(QueryDefinition query)
    {
        var iterator = Container.GetItemQueryIterator<string>(query);
        var results = new List<string>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response.Resource);
        }

        return results.ToArray();
    }
}
