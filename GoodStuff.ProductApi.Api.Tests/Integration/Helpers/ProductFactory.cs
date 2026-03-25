using System.Text.Json;
using GoodStuff.ProductApi.Api.Models;

namespace GoodStuff.ProductApi.Api.Tests.Integration.Helpers;

public static class ProductFactory
{
    public static JsonElement CreateTestGpu() => JsonSerializer.SerializeToElement(new Gpu
    {
        id = "11111111-2222-3333-4444-555555555555",
        Category = ProductCategories.Gpu,
        Name = "TEST",
        Price = "132",
        Team = "TEST",
        Manufacturer = "TEST",
        ProductImg = "TEST",
        Warranty = "TEST",
        GpuProcessorLine = "TEST",
        GpuProcessorName = "TEST",
        PcieType = "TEST",
        MemorySize = "TEST",
        MemoryType = "TEST",
        MemoryBus = "TEST",
        MemoryRatio = "TEST",
        CoreRatio = "TEST",
        CoresNumber = "TEST",
        CoolingType = "TEST",
        OutputsType = "TEST",
        SupportedLibraries = "TEST",
        PowerConnector = "TEST",
        RecommendedPsuPower = "TEST",
        Length = "TEST",
        Width = "TEST",
        Height = "TEST",
        ProducerCode = "TEST"
    });

    public static JsonElement CreateTestCpu() => JsonSerializer.SerializeToElement(new Cpu
    {
        Name = "TEST",
        Category = ProductCategories.Cpu,
        Team = "TEST",
        Price = "TEST",
        Family = "TEST",
        Series = "TEST",
        Socket = "TEST",
        SupportedChipsets = "TEST",
        RecommendedChipset = "TEST",
        Architecture = "TEST",
        Frequency = "TEST",
        Cores = "TEST",
        Threads = "TEST",
        UnlockedMultiplayer = "TEST",
        CacheMemory = "TEST",
        IntegratedGpu = "TEST",
        IntegratedGpuModel = "TEST",
        SupportedRam = "TEST",
        Lithography = "TEST",
        Tdp = "TEST",
        AdditionalInfo = "TEST",
        IncludedCooler = "TEST",
        Warranty = "TEST",
        ProductImg = "TEST",
        id = "11111111-2222-3333-4444-555555555555",
        ProducerCode = "TEST"
    });

    public static JsonElement CreateTestCooler() => JsonSerializer.SerializeToElement(new Cooler
    {
        Name = "TEST",
        Team = "TEST",
        CoolerType = "TEST",
        Compatibility = "TEST",
        Size = "TEST",
        HeatPipes = "TEST",
        Fans = "TEST",
        RpmControl = "TEST",
        Rmp = "TEST",
        BearingType = "TEST",
        FanSize = "TEST",
        Connector = "TEST",
        SupplyVoltage = "TEST",
        SupplyCurrent = "TEST",
        Highlight = "TEST",
        MtbfLifetime = "TEST",
        Height = "TEST",
        Width = "TEST",
        Depth = "TEST",
        Weight = "TEST",
        Warranty = "TEST",
        ProducerCode = "TEST",
        ProductImg = "TEST",
        Price = "TEST",
        Manufacture = "TEST",
        Category = ProductCategories.Cooler,
        id = "11111111-2222-3333-4444-555555555555"
    });
}
