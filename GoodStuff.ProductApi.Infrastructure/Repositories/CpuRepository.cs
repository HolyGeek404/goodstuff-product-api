using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products.Models;
using Microsoft.Azure.Cosmos;

namespace GoodStuff.ProductApi.Infrastructure.Repositories;

public class CpuRepository(CosmosClient cosmosClient) : CosmosRepository<Cpu>(cosmosClient), ICpuRepository;