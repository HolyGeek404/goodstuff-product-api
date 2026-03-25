namespace GoodStuff.ProductApi.Domain.Products.Models;

public class BaseProduct
{
    public required string Name { get; set; }
    public required string Team { get; set; }
    public required string Price { get; set; }
    public string? ProductImg { get; set; }
    public virtual required string Category { get; init; }

    public required string id { get; set; }

    public required string Warranty { get; set; }
    public required string ProducerCode { get; set; }
}