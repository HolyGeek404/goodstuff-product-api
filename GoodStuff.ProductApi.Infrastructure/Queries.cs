namespace GoodStuff.ProductApi.Infrastructure;

public static class Queries
{
    public const string GetAllByType = "SELECT * FROM c WHERE c.Category = @category";

    public const string GetSingleById = "SELECT * FROM c WHERE c.Category = @category AND c.id = @id";
}