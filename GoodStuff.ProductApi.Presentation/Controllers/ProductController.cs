using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using GoodStuff.ProductApi.Application.Features.Product.Commands.Create;
using GoodStuff.ProductApi.Application.Features.Product.Commands.Delete;
using GoodStuff.ProductApi.Application.Features.Product.Commands.Update;
using GoodStuff.ProductApi.Application.Features.Product.Queries.GetById;
using GoodStuff.ProductApi.Application.Features.Product.Queries.GetByType;
using GoodStuff.ProductApi.Application.Services;
using GoodStuff.ProductApi.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodStuff.ProductApi.Presentation.Controllers;

[ApiController]
[Route("[controller]/{type:alpha}")]
public class ProductController(IMediator mediator, ILogger<ProductController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetByType([FromRoute]string type)
    {
        var caller = User.FindFirst("appid")?.Value ?? "Unknown";
        Logger.LogCallingGetbytypenameByUnknownTypeType(logger, nameof(GetByType), caller, type);

        try
        {
            var result = await mediator.Send(new GetByTypeQuery { Type = type });
            if (result == null)
            {
                Logger.LogNoProductsFoundInGetbytypenameByUnknownTypeType(logger, nameof(GetByType), caller, type);
                return NotFound($"No products found for type: {type}");
            }

            Logger.LogSuccessfullyCalledGetbytypenameByUnknownTypeType(logger, nameof(GetByType), caller, type);
            return new JsonResult(result);
        }
        catch (Exception ex)
        {
            Logger.LogExceptionInGetbytypenameByUnknownTypeType(logger, ex, nameof(GetByType), caller, type);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute]string type, [FromRoute]string id)
    {
        var caller = User.FindFirst("appid")?.Value ?? "Unknown";
        Logger.LogCallingGetbyidnameByUnknownTypeTypeIdId(logger, nameof(GetById), caller, type, id);

        if (string.IsNullOrEmpty(id))
        {
            Logger.LogBadRequestInGetbyidnameByUnknownTypeTypeIdIsEmpty(logger, nameof(GetById), caller, type);
            return BadRequest("Product id cannot be empty.");
        }

        try
        {
            var products = await mediator.Send(new GetByIdQuery { Type = type, Id = id });
            if (products == null)
            {
                Logger.LogNoProductFoundInGetbyidnameByUnknownTypeTypeIdId(logger, nameof(GetById), caller, type, id);
                return NotFound($"No product found for type: {type} and id: {id}");
            }

            Logger.LogSuccessfullyCalledGetbyidnameByUnknownTypeTypeIdId(logger, nameof(GetById), caller, type, id);
            return new JsonResult(products);
        }
        catch (Exception ex)
        {
            Logger.LogExceptionInGetbyidnameByUnknownTypeTypeIdId(logger, ex, nameof(GetById), caller, type, id);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPatch]
    [Authorize(Roles = "Update")]
    public async Task<IActionResult> Update([FromBody]JsonElement product, [FromRoute]string type)
    {
        var caller = User.FindFirst("appid")?.Value ?? "Unknown";
        Logger.LogCallingUpdatenameByUnknownTypeTypeProductProduct(logger, nameof(Update), caller, type, product);

        if (string.IsNullOrEmpty(product.ToString()))
        {
            Logger.LogBadRequestInUpdatenameByUnknownTypeTypeProductIsEmpty(logger, nameof(Update), caller, type);
            return BadRequest("Product cannot be empty.");
        }

        try
        {
            var result = await mediator.Send(new UpdateCommand { BaseProduct = product, Type = type });

            switch (result)
            {
                case HttpStatusCode.NotFound:
                    Logger.LogNoProductFoundInUpdatenameByUnknownTypeTypeProductProduct(logger, nameof(Update), caller, type, product);
                    var productNode = JsonNode.Parse( product.GetRawText())!.AsObject();
                    return NotFound($"No product found to update for type: {type} and product: {productNode["id"]}");
                case HttpStatusCode.OK or HttpStatusCode.Created or HttpStatusCode.NoContent:
                    Logger.LogSuccessfullyCalledUpdatenameByUnknownTypeTypeProductProduct(logger, nameof(Update), caller, type, product);
                    return NoContent();
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        catch (Exception ex)
        {
            Logger.LogExceptionInUpdatenameByUnknownTypeTypeProductProduct(logger, ex, nameof(Update), caller, type, product);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Create")]
    public async Task<IActionResult> Create([FromBody]JsonElement product, [FromRoute]string type)
    {
        var caller = User.FindFirst("appid")?.Value ?? "Unknown";
        Logger.LogCallingCreatenameByCallerTypeTypeProductProduct(logger, nameof(Create), caller, type, product);

        if (string.IsNullOrEmpty(product.ToString()))
        {
            Logger.LogBadRequestInCreatenameByCallerTypeTypeProductIsEmpty(logger, nameof(Create), caller, type);
            return BadRequest("Product cannot be empty.");
        }

        try
        {
            var result = await mediator.Send(new  CreateCommand { Product = product, Type = type });

            if (result == null || string.IsNullOrEmpty(result.id))
            {
                Logger.LogCreateFailedOrReturnedNullInCreatenameByCallerTypeTypeProductProduct(logger, nameof(Create), caller, type, product);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Logger.LogSuccessfullyCreatedProductInCreatenameByCallerTypeTypeProductProductIdId(logger, nameof(Create), caller, type, product, result.id);

            return CreatedAtAction(nameof(GetById), new { type = result.Category, id = result.id }, result);
        }
        catch (Exception ex)
        {
            Logger.LogExceptionInCreatenameByCallerTypeTypeProductProduct(logger, ex, nameof(Create), caller, type, product);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpDelete]
    [Authorize(Roles = "Delete")]
    public async Task<IActionResult> Delete(Guid id, [FromRoute]string type)
    {
        var caller = User.FindFirst("appid")?.Value ?? "Unknown";
        Logger.LogDeleteRequestReceivedByCallerIdIdTypeType(logger, caller, id, type);

        if (id == Guid.Empty || string.IsNullOrEmpty(type))
        {
            Logger.LogDeleteFailedDueToMissingParametersCallerCallerIdIdTypeType(logger, caller, id, type);
            return BadRequest("Both 'id' and 'type' are required.");
        }

        try
        {
            var result = await mediator.Send(new DeleteCommand { Id = id, Type = type });

            switch (result)
            {
                case HttpStatusCode.NoContent:
                    Logger.LogSuccessfullyDeletedItemCallerCallerIdIdTypeType(logger, caller, id, type);
                    return NoContent();

                case HttpStatusCode.NotFound:
                    Logger.LogItemNotFoundForDeletionCallerCallerIdIdTypeType(logger, caller, id, type);
                    return NotFound();

                case HttpStatusCode.BadRequest:
                    Logger.LogBadRequestDuringDeletionCallerCallerIdIdTypeType(logger, caller, id, type);
                    return BadRequest();

                default:
                    Logger.LogUnexpectedStatusCodeStatusDuringDeletionCallerCallerIdIdTypeType(logger, result, caller, id, type);
                    return StatusCode((int)result);
            }
        }
        catch (Exception ex)
        {
            Logger.LogExceptionOccurredDuringDeletionCallerCallerIdIdTypeType(logger, ex, caller, id, type);
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
        }
    }
}