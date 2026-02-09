using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products.Models;
using Microsoft.Azure.Cosmos;

namespace GoodStuff.ProductApi.Infrastructure.Repositories;

public class GpuRepository(CosmosClient cosmosClient) : CosmosRepository<Gpu>(cosmosClient), IGpuRepository;