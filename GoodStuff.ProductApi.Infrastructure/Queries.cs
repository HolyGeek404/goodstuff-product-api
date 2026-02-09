namespace GoodStuff.ProductApi.Infrastructure;

public static class Queries
{
    public const string GetAllByType = "SELECT * FROM c WHERE c.Category = @category";

    public const string GetSingleById = "SELECT * FROM c WHERE c.Category = @category AND c.id = @id";

    private static readonly string[] CpuFilters =
    [
        "SELECT DISTINCT VALUE c.Team FROM c WHERE c.Category = 'CPU'",
        "SELECT DISTINCT VALUE c.Cores FROM c WHERE c.Category = 'CPU'",
        "SELECT DISTINCT VALUE c.Architecture FROM c WHERE c.Category = 'CPU'",
        "SELECT DISTINCT VALUE c.TDP FROM c WHERE c.Category = 'CPU'"
    ];

    public static string[] GetFilters(string type)
    {
        return type switch
        {
            "CPU" => CpuFilters,
            _ => CpuFilters
        };
    }
}
