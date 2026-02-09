using Microsoft.Azure.Cosmos;

namespace GoodStuff.ProductApi.Infrastructure;

public static class QueryBuilder
{
    public static QueryDefinition SelectAllProductsByType(string type)
    {
        return new QueryDefinition(Queries.GetAllByType).WithParameter("@category", type);
    }

    public static QueryDefinition SelectSingleProductById(string type, string id)
    {
        return new QueryDefinition(Queries.GetSingleById).WithParameter("@category", type).WithParameter("@id", id);
    }
    
    public static IEnumerable<QueryDefinition> GetFilterParams(string type)
    {
        var queryList = Queries.GetFilters(type);
        return queryList.Select(s => new QueryDefinition(s)).ToList();
    }
}