using System.Net;
using MediatR;

namespace GoodStuff.ProductApi.Api.Features.Product.Commands.Delete;

public class DeleteCommand : IRequest<HttpStatusCode>
{
    public required Guid Id { get; set; }
    public required string Type { get; set; }
}