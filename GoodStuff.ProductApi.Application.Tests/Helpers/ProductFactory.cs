using GoodStuff.ProductApi.Domain.Products;
using GoodStuff.ProductApi.Domain.Products.Models;

namespace GoodStuff.ProductApi.Application.Tests.Helpers;

public static class ProductFactory
{
    public static Gpu CreateGpu() => new()
    {
        Name = "Test GPU",
        Category = ProductCategories.Gpu,
        Team = "AMD",
        Price = "3900",
        id = "123",
        Warranty = "5 Years",
        ProducerCode = "GPU123",
        GpuProcessorLine = null,
        PcieType = null,
        MemorySize = null,
        MemoryType = null,
        MemoryBus = null,
        MemoryRatio = null,
        CoreRatio = null,
        CoresNumber = null,
        CoolingType = null,
        OutputsType = null,
        SupportedLibraries = null,
        PowerConnector = null,
        RecommendedPsuPower = null,
        Length = null,
        Width = null,
        Height = null,
        GpuProcessorName = null,
        Manufacturer = null
    };

    public static Cpu CreateCpu() => new()
    {
        Name = "Test CPU",
        Category = ProductCategories.Cpu,
        Team = "Intel",
        Price = "2500",
        id = "456",
        Warranty = "3 Years",
        ProducerCode = "CPU123",
        Family = null,
        Series = null,
        Socket = null,
        SupportedChipsets = null,
        RecommendedChipset = null,
        Architecture = null,
        Frequency = null,
        Cores = null,
        Threads = null,
        UnlockedMultiplayer = null,
        CacheMemory = null,
        IntegratedGpu = null,
        IntegratedGpuModel = null,
        SupportedRam = null,
        Lithography = null,
        Tdp = null,
        AdditionalInfo = null,
        IncludedCooler = null
    };

    public static Cooler CreateCooler() => new()
    {
        Name = "Test Cooler",
        Category = ProductCategories.Cooler,
        Team = "Noctua",
        Price = "120",
        id = "789",
        Warranty = "6 Years",
        ProducerCode = "COOL123",
        CoolerType = null,
        Compatibility = null,
        Size = null,
        HeatPipes = null,
        Fans = null,
        RpmControl = null,
        Rmp = null,
        BearingType = null,
        FanSize = null,
        Connector = null,
        SupplyVoltage = null,
        SupplyCurrent = null,
        Highlight = null,
        MtbfLifetime = null,
        Height = null,
        Width = null,
        Depth = null,
        Weight = null,
        Manufacture = null
    };
}