using System.Text.Json;
using GoodStuff.ProductApi.Domain.Products.Models;
using MediatR;

namespace GoodStuff.ProductApi.Application.Features.Product.Commands.Create;

public class CreateCommand : IRequest<BaseProduct?>
{
    public required JsonElement Product { get; set; }
    public required string Type { get; set; }
}