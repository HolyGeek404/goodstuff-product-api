using System.Net;
using System.Text.Json;
using MediatR;

namespace GoodStuff.ProductApi.Api.Features.Product.Commands.Update;

public class UpdateCommand : IRequest<HttpStatusCode>
{
    public required JsonElement BaseProduct { get; set; }
    public required string Type { get; set; }
}