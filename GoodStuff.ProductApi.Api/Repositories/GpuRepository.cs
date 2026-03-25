using GoodStuff.ProductApi.Api.Interfaces;
using GoodStuff.ProductApi.Api.Products.Models;
using Microsoft.Azure.Cosmos;

namespace GoodStuff.ProductApi.Api.Repositories;

public class GpuRepository(CosmosClient cosmosClient) : CosmosRepository<Gpu>(cosmosClient), IGpuRepository
{
        
    
}