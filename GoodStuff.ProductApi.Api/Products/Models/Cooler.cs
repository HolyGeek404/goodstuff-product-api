namespace GoodStuff.ProductApi.Domain.Products.Models;

public class Cooler : BaseProduct
{
    public required string CoolerType { get; set; }
    public required string Compatibility { get; set; }
    public required string Size { get; set; }
    public required string HeatPipes { get; set; }
    public required string Fans { get; set; }
    public required string RpmControl { get; set; }
    public required string Rmp { get; set; }
    public required string BearingType { get; set; }
    public required string FanSize { get; set; }
    public required string Connector { get; set; }
    public required string SupplyVoltage { get; set; }
    public required string SupplyCurrent { get; set; }
    public required string Highlight { get; set; }
    public required string MtbfLifetime { get; set; }
    public required string Height { get; set; }
    public required string Width { get; set; }
    public required string Depth { get; set; }
    public required string Weight { get; set; }
    public required string Manufacture { get; set; }
}