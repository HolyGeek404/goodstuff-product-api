namespace GoodStuff.ProductApi.Application.DTO;

public class CpuFilters
{
    public string[]? Team { get; set; }
    public string[]? Cores { get; set; }
    public string[]? Socket { get; set; }
    public string[]? Architecture { get; set; }
    public string[]? TDP { get; set; }
}
