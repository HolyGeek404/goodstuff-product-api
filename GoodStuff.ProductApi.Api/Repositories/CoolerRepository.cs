using GoodStuff.ProductApi.Api.Interfaces;
using GoodStuff.ProductApi.Api.Models;
using Microsoft.Azure.Cosmos;

namespace GoodStuff.ProductApi.Api.Repositories;

public class CoolerRepository(CosmosClient cosmosClient) : CosmosRepository<Cooler>(cosmosClient), ICoolerRepository;