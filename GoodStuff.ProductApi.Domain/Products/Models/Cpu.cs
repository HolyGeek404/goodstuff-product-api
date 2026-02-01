namespace GoodStuff.ProductApi.Domain.Products.Models;

public class Cpu : BaseProduct
{
    public required string Family { get; set; }
    public required string Series { get; set; }
    public required string Socket { get; set; }
    public required string SupportedChipsets { get; set; }
    public required string RecommendedChipset { get; set; }
    public required string Architecture { get; set; }
    public required string Frequency { get; set; }
    public required string Cores { get; set; }
    public required string Threads { get; set; }
    public required string UnlockedMultiplayer { get; set; }
    public required string CacheMemory { get; set; }
    public required string IntegratedGpu { get; set; }
    public required string IntegratedGpuModel { get; set; }
    public required string SupportedRam { get; set; }
    public required string Lithography { get; set; }
    public required string Tdp { get; set; }
    public required string AdditionalInfo { get; set; }
    public required string IncludedCooler { get; set; }
}