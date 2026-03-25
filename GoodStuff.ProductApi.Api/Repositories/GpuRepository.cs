using GoodStuff.ProductApi.Api.Interfaces;
using GoodStuff.ProductApi.Api.Models;
using Microsoft.Azure.Cosmos;

namespace GoodStuff.ProductApi.Api.Repositories;

public class GpuRepository(CosmosClient cosmosClient) : CosmosRepository<Gpu>(cosmosClient), IGpuRepository
{
        
    
}