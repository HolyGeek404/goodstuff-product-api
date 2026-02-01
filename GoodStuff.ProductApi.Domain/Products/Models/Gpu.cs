namespace GoodStuff.ProductApi.Domain.Products.Models;

public class Gpu : BaseProduct
{
    public required string GpuProcessorLine { get; set; }
    public required string PcieType { get; set; }
    public required string MemorySize { get; set; }
    public required string MemoryType { get; set; }
    public required string MemoryBus { get; set; }
    public required string MemoryRatio { get; set; }
    public required string CoreRatio { get; set; }
    public required string CoresNumber { get; set; }
    public required string CoolingType { get; set; }
    public required string OutputsType { get; set; }
    public required string SupportedLibraries { get; set; }
    public required string PowerConnector { get; set; }
    public required string RecommendedPsuPower { get; set; }
    public required string Length { get; set; }
    public required string Width { get; set; }
    public required string Height { get; set; }
    public required string GpuProcessorName { get; set; }
    public required string Manufacturer { get; set; }
}