using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace GoodStuff.ProductApi.Application.Services;

public static partial class Logger
{
    [LoggerMessage(LogLevel.Information, "Calling {getByTypeName} by {unknown}. Type: {type}")]
   public static partial void LogCallingGetbytypenameByUnknownTypeType(ILogger logger, string getByTypeName, string unknown, string type);

    [LoggerMessage(LogLevel.Information, "No products found in {getByTypeName} by {unknown}. Type: {type}")]
   public static partial void LogNoProductsFoundInGetbytypenameByUnknownTypeType(ILogger logger, string getByTypeName, string unknown, string type);

    [LoggerMessage(LogLevel.Information, "Successfully called {getByTypeName} by {unknown}. Type: {type}")]
   public static partial void LogSuccessfullyCalledGetbytypenameByUnknownTypeType(ILogger logger, string getByTypeName, string unknown, string type);

    [LoggerMessage(LogLevel.Error, "Exception in {getByTypeName} by {unknown}. Type: {type}")]
   public static partial void LogExceptionInGetbytypenameByUnknownTypeType(ILogger logger, Exception e, string getByTypeName, string unknown, string type);

    [LoggerMessage(LogLevel.Information, "Calling {getByIdName} by {unknown}. Type: {type}, Id: {id}")]
   public static partial void LogCallingGetbyidnameByUnknownTypeTypeIdId(ILogger logger, string getByIdName, string unknown, string type, string id);

    [LoggerMessage(LogLevel.Warning, "Bad request in {getByIdName} by {unknown}. Type: {type}, Id is empty")]
   public static partial void LogBadRequestInGetbyidnameByUnknownTypeTypeIdIsEmpty(ILogger logger, string getByIdName, string unknown, string type);

    [LoggerMessage(LogLevel.Information, "No product found in {getByIdName} by {unknown}. Type: {type}, Id: {id}")]
   public static partial void LogNoProductFoundInGetbyidnameByUnknownTypeTypeIdId(ILogger logger, string getByIdName, string unknown, string type, string id);

    [LoggerMessage(LogLevel.Information, "Successfully called {getByIdName} by {unknown}. Type: {type}, Id: {id}")]
   public static partial void LogSuccessfullyCalledGetbyidnameByUnknownTypeTypeIdId(ILogger logger, string getByIdName, string unknown, string type, string id);

    [LoggerMessage(LogLevel.Error, "Exception in {getByIdName} by {unknown}. Type: {type}, Id: {id}")]
   public static partial void LogExceptionInGetbyidnameByUnknownTypeTypeIdId(ILogger logger, Exception e,string getByIdName, string unknown, string type, string id);

    [LoggerMessage(LogLevel.Information, "Calling {updateName} by {unknown}. Type: {type}, Product: {product}")]
   public static partial void LogCallingUpdatenameByUnknownTypeTypeProductProduct(ILogger logger, string updateName, string unknown, string type, JsonElement product);

    [LoggerMessage(LogLevel.Warning, "Bad request in {updateName} by {unknown}. Type: {type}, Product is empty")]
   public static partial void LogBadRequestInUpdatenameByUnknownTypeTypeProductIsEmpty(ILogger logger, string updateName, string unknown, string type);

    [LoggerMessage(LogLevel.Information, "Successfully called {updateName} by {unknown}. Type: {type}, Product: {product}")]
   public static partial void LogSuccessfullyCalledUpdatenameByUnknownTypeTypeProductProduct(ILogger logger, string updateName, string unknown, string type, JsonElement product);

    [LoggerMessage(LogLevel.Information, "No product found in {updateName} by {unknown}. Type: {type}, Product: {product}")]
   public static partial void LogNoProductFoundInUpdatenameByUnknownTypeTypeProductProduct(ILogger logger, string updateName, string unknown, string type, JsonElement product);

    [LoggerMessage(LogLevel.Warning, "Update returned bad request in {updateName} by {unknown}. Type: {type}, Product: {product}")]
   public static partial void LogUpdateReturnedBadRequestInUpdatenameByUnknownTypeTypeProductProduct(ILogger logger, string updateName, string unknown, string type, JsonElement product);

    [LoggerMessage(LogLevel.Warning, "Update returned unexpected status {status} in {updateName} by {unknown}. Type: {type}, Product: {product}")]
   public static partial void LogUpdateReturnedUnexpectedStatusStatusInUpdatenameByUnknownTypeTypeProduct(ILogger logger, HttpStatusCode status, string updateName, string unknown, string type, JsonElement product);

    [LoggerMessage(LogLevel.Error, "Exception in {updateName} by {unknown}. Type: {type}, Product: {product}")]
   public static partial void LogExceptionInUpdatenameByUnknownTypeTypeProductProduct(ILogger logger, Exception e, string updateName, string unknown, string type, JsonElement product);

    [LoggerMessage(LogLevel.Information, "Calling {createName} by {caller}. Type: {type}, Product: {product}")]
   public static partial void LogCallingCreatenameByCallerTypeTypeProductProduct(ILogger logger, string createName, string caller, string type, JsonElement product);

    [LoggerMessage(LogLevel.Warning, "Bad request in {createName} by {caller}. Type: {type}, Product is empty")]
   public static partial void LogBadRequestInCreatenameByCallerTypeTypeProductIsEmpty(ILogger logger, string createName, string caller, string type);

    [LoggerMessage(LogLevel.Warning, "Create failed or returned null in {createName} by {caller}. Type: {type}, Product: {product}")]
   public static partial void LogCreateFailedOrReturnedNullInCreatenameByCallerTypeTypeProductProduct(ILogger logger, string createName, string caller, string type, JsonElement product);

    [LoggerMessage(LogLevel.Information, "Successfully created product in {createName} by {caller}. Type: {type}, Product: {product}, Id: {id}")]
   public static partial void LogSuccessfullyCreatedProductInCreatenameByCallerTypeTypeProductProductIdId(ILogger logger, string createName, string caller, string type, JsonElement product, string id);

    [LoggerMessage(LogLevel.Error, "Exception in {createName} by {caller}. Type: {type}, Product: {product}")]
   public static partial void LogExceptionInCreatenameByCallerTypeTypeProductProduct(ILogger logger, Exception e, string createName, string caller, string type, JsonElement product);

    [LoggerMessage(LogLevel.Information, "Delete request received by {caller}. Id: {id}, Type: {type}")]
   public static partial void LogDeleteRequestReceivedByCallerIdIdTypeType(ILogger logger, string caller, Guid id, string type);

    [LoggerMessage(LogLevel.Warning, "Delete failed due to missing parameters. Caller: {caller}, Id: {id}, Type: {type}")]
   public static partial void LogDeleteFailedDueToMissingParametersCallerCallerIdIdTypeType(ILogger logger, string caller, Guid id, string type);

    [LoggerMessage(LogLevel.Information, "Successfully deleted item. Caller: {caller}, Id: {id}, Type: {type}")]
   public static partial void LogSuccessfullyDeletedItemCallerCallerIdIdTypeType(ILogger logger, string caller, Guid id, string type);

    [LoggerMessage(LogLevel.Warning, "Item not found for deletion. Caller: {caller}, Id: {id}, Type: {type}")]
   public static partial void LogItemNotFoundForDeletionCallerCallerIdIdTypeType(ILogger logger, string caller, Guid id, string type);

    [LoggerMessage(LogLevel.Warning, "Bad request during deletion. Caller: {caller}, Id: {id}, Type: {type}")]
   public static partial void LogBadRequestDuringDeletionCallerCallerIdIdTypeType(ILogger logger, string caller, Guid id, string type);

    [LoggerMessage(LogLevel.Error, "Unexpected status code {status} during deletion. Caller: {caller}, Id: {id}, Type: {type}")]
   public static partial void LogUnexpectedStatusCodeStatusDuringDeletionCallerCallerIdIdTypeType(ILogger logger, HttpStatusCode status, string caller, Guid id, string type);

    [LoggerMessage(LogLevel.Error, "Exception occurred during deletion. Caller: {caller}, Id: {id}, Type: {type}")]
   public static partial void LogExceptionOccurredDuringDeletionCallerCallerIdIdTypeType(ILogger logger, Exception e, string caller, Guid id, string type);
}