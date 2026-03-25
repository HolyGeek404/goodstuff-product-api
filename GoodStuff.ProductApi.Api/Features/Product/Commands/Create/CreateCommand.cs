using System.Text.Json;
using GoodStuff.ProductApi.Api.Products.Models;
using MediatR;

namespace GoodStuff.ProductApi.Api.Features.Product.Commands.Create;

public class CreateCommand : IRequest<BaseProduct?>
{
    public required JsonElement Product { get; set; }
    public required string Type { get; set; }
}