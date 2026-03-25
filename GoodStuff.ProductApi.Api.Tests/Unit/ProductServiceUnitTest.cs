using System.Net;
using System.Text.Json;
using GoodStuff.ProductApi.Api.Interfaces;
using GoodStuff.ProductApi.Api.Models;
using GoodStuff.ProductApi.Api.Services;
using Moq;

namespace GoodStuff.ProductApi.Api.Tests.Unit;

public class ProductServiceUnitTest
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IGpuRepository> _gpuRepository = new();
    private readonly Mock<ICpuRepository> _cpuRepository = new();
    private readonly Mock<ICoolerRepository> _coolerRepository = new();
    private readonly ProductServiceUnit _serviceUnit;

    public ProductServiceUnitTest()
    {
        _uow.SetupProperty(x => x.GpuRepository, _gpuRepository.Object);
        _uow.SetupProperty(x => x.CpuRepository, _cpuRepository.Object);
        _uow.SetupProperty(x => x.CoolerRepository, _coolerRepository.Object);

        _serviceUnit = new ProductServiceUnit(_uow.Object);
    }

    [Fact]
    public async Task GetByIdAndType_WhenTypeIsGpu_ReturnsGpu()
    {
        var product = CreateGpu();
        _gpuRepository.Setup(x => x.GetById(ProductCategories.Gpu, product.id)).ReturnsAsync(product);

        var result = await _serviceUnit.GetByIdAndType(ProductCategories.Gpu, product.id);

        var typed = Assert.IsType<Gpu>(result);
        Assert.Equal(product.id, typed.id);
        VerifyGetByIdCalled(ProductCategories.Gpu, product.id);
    }

    [Fact]
    public async Task GetByIdAndType_WhenTypeIsCpu_ReturnsCpu()
    {
        var product = CreateCpu();
        _cpuRepository.Setup(x => x.GetById(ProductCategories.Cpu, product.id)).ReturnsAsync(product);

        var result = await _serviceUnit.GetByIdAndType(ProductCategories.Cpu, product.id);

        var typed = Assert.IsType<Cpu>(result);
        Assert.Equal(product.id, typed.id);
        VerifyGetByIdCalled(ProductCategories.Cpu, product.id);
    }

    [Fact]
    public async Task GetByIdAndType_WhenTypeIsCooler_ReturnsCooler()
    {
        var product = CreateCooler();
        _coolerRepository.Setup(x => x.GetById(ProductCategories.Cooler, product.id)).ReturnsAsync(product);

        var result = await _serviceUnit.GetByIdAndType(ProductCategories.Cooler, product.id);

        var typed = Assert.IsType<Cooler>(result);
        Assert.Equal(product.id, typed.id);
        VerifyGetByIdCalled(ProductCategories.Cooler, product.id);
    }

    [Fact]
    public async Task GetByIdAndType_WhenTypeIsUnsupported_ReturnsNull()
    {
        var result = await _serviceUnit.GetByIdAndType("RAM", "123");

        Assert.Null(result);
        VerifyNoGetByIdCalls();
    }

    [Fact]
    public async Task GetByType_WhenTypeIsGpu_ReturnsProducts()
    {
        var products = new[] { CreateGpu() };
        _gpuRepository.Setup(x => x.GetByType(ProductCategories.Gpu)).ReturnsAsync(products);

        var result = await _serviceUnit.GetByType(ProductCategories.Gpu);

        var typed = Assert.IsAssignableFrom<IEnumerable<Gpu>>(result);
        Assert.Single(typed);
        VerifyGetByTypeCalled(ProductCategories.Gpu);
    }

    [Fact]
    public async Task GetByType_WhenTypeIsCpu_ReturnsProducts()
    {
        var products = new[] { CreateCpu() };
        _cpuRepository.Setup(x => x.GetByType(ProductCategories.Cpu)).ReturnsAsync(products);

        var result = await _serviceUnit.GetByType(ProductCategories.Cpu);

        var typed = Assert.IsAssignableFrom<IEnumerable<Cpu>>(result);
        Assert.Single(typed);
        VerifyGetByTypeCalled(ProductCategories.Cpu);
    }

    [Fact]
    public async Task GetByType_WhenTypeIsCooler_ReturnsProducts()
    {
        var products = new[] { CreateCooler() };
        _coolerRepository.Setup(x => x.GetByType(ProductCategories.Cooler)).ReturnsAsync(products);

        var result = await _serviceUnit.GetByType(ProductCategories.Cooler);

        var typed = Assert.IsAssignableFrom<IEnumerable<Cooler>>(result);
        Assert.Single(typed);
        VerifyGetByTypeCalled(ProductCategories.Cooler);
    }

    [Fact]
    public async Task GetByType_WhenTypeIsUnsupported_ReturnsNull()
    {
        var result = await _serviceUnit.GetByType("RAM");

        Assert.Null(result);
        VerifyNoGetByTypeCalls();
    }

    [Fact]
    public async Task GetFilterByType_WhenTypeIsCpu_ReturnsFilters()
    {
        var filters = new CpuFilters
        {
            Team = ["AMD"],
            Cores = ["8"],
            Socket = ["AM5"],
            Architecture = ["Zen 5"],
            TDP = ["120W"]
        };

        _cpuRepository.Setup(x => x.GetFiltersAsync(ProductCategories.Cpu)).ReturnsAsync(filters);

        var result = await _serviceUnit.GetFilterByType(ProductCategories.Cpu);

        Assert.Same(filters, result);
        _cpuRepository.Verify(x => x.GetFiltersAsync(ProductCategories.Cpu), Times.Once);
    }

    [Fact]
    public async Task GetFilterByType_WhenTypeIsUnsupported_ReturnsNull()
    {
        var result = await _serviceUnit.GetFilterByType(ProductCategories.Gpu);

        Assert.Null(result);
        _cpuRepository.Verify(x => x.GetFiltersAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Create_WhenTypeIsGpu_CallsGpuRepository()
    {
        var product = CreateGpu();
        var payload = JsonSerializer.SerializeToElement(product);
        _gpuRepository
            .Setup(x => x.CreateAsync(It.Is<Gpu>(gpu => gpu.id == product.id), product.id, product.Category))
            .ReturnsAsync(product);

        var result = await _serviceUnit.Create(payload, ProductCategories.Gpu);

        var typed = Assert.IsType<Gpu>(result);
        Assert.Equal(product.id, typed.id);
        VerifyCreateCalled(ProductCategories.Gpu, product.id, product.Category);
    }

    [Fact]
    public async Task Create_WhenTypeIsCpu_CallsCpuRepository()
    {
        var product = CreateCpu();
        var payload = JsonSerializer.SerializeToElement(product);
        _cpuRepository
            .Setup(x => x.CreateAsync(It.Is<Cpu>(cpu => cpu.id == product.id), product.id, product.Category))
            .ReturnsAsync(product);

        var result = await _serviceUnit.Create(payload, ProductCategories.Cpu);

        var typed = Assert.IsType<Cpu>(result);
        Assert.Equal(product.id, typed.id);
        VerifyCreateCalled(ProductCategories.Cpu, product.id, product.Category);
    }

    [Fact]
    public async Task Create_WhenTypeIsCooler_CallsCoolerRepository()
    {
        var product = CreateCooler();
        var payload = JsonSerializer.SerializeToElement(product);
        _coolerRepository
            .Setup(x => x.CreateAsync(It.Is<Cooler>(cooler => cooler.id == product.id), product.id, product.Category))
            .ReturnsAsync(product);

        var result = await _serviceUnit.Create(payload, ProductCategories.Cooler);

        var typed = Assert.IsType<Cooler>(result);
        Assert.Equal(product.id, typed.id);
        VerifyCreateCalled(ProductCategories.Cooler, product.id, product.Category);
    }

    [Fact]
    public async Task Create_WhenTypeIsUnsupported_ReturnsNull()
    {
        var payload = JsonSerializer.SerializeToElement(new { id = "123" });

        var result = await _serviceUnit.Create(payload, "RAM");

        Assert.Null(result);
        VerifyNoCreateCalls();
    }

    [Theory]
    [InlineData(ProductCategories.Gpu)]
    [InlineData(ProductCategories.Cpu)]
    [InlineData(ProductCategories.Cooler)]
    public async Task Delete_WhenTypeIsSupported_CallsCorrectRepository(string type)
    {
        var id = Guid.NewGuid();

        _gpuRepository.Setup(x => x.DeleteAsync(id, ProductCategories.Gpu)).ReturnsAsync(HttpStatusCode.NoContent);
        _cpuRepository.Setup(x => x.DeleteAsync(id, ProductCategories.Cpu)).ReturnsAsync(HttpStatusCode.NoContent);
        _coolerRepository.Setup(x => x.DeleteAsync(id, ProductCategories.Cooler)).ReturnsAsync(HttpStatusCode.NoContent);

        var result = await _serviceUnit.Delete(id, type);

        Assert.Equal(HttpStatusCode.NoContent, result);
        VerifyDeleteCalled(type, id);
    }

    [Fact]
    public async Task Delete_WhenTypeIsUnsupported_ReturnsBadRequest()
    {
        var result = await _serviceUnit.Delete(Guid.NewGuid(), "RAM");

        Assert.Equal(HttpStatusCode.BadRequest, result);
        VerifyNoDeleteCalls();
    }

    [Fact]
    public async Task Update_WhenTypeIsGpu_CallsGpuRepository()
    {
        var product = CreateGpu();
        var payload = JsonSerializer.SerializeToElement(product);
        _gpuRepository
            .Setup(x => x.UpdateAsync(It.Is<Gpu>(gpu => gpu.id == product.id), product.id, product.Category))
            .ReturnsAsync(HttpStatusCode.OK);

        var result = await _serviceUnit.Update(payload, ProductCategories.Gpu);

        Assert.Equal(HttpStatusCode.OK, result);
        VerifyUpdateCalled(ProductCategories.Gpu, product.id, product.Category);
    }

    [Fact]
    public async Task Update_WhenTypeIsCpu_CallsCpuRepository()
    {
        var product = CreateCpu();
        var payload = JsonSerializer.SerializeToElement(product);
        _cpuRepository
            .Setup(x => x.UpdateAsync(It.Is<Cpu>(cpu => cpu.id == product.id), product.id, product.Category))
            .ReturnsAsync(HttpStatusCode.OK);

        var result = await _serviceUnit.Update(payload, ProductCategories.Cpu);

        Assert.Equal(HttpStatusCode.OK, result);
        VerifyUpdateCalled(ProductCategories.Cpu, product.id, product.Category);
    }

    [Fact]
    public async Task Update_WhenTypeIsCooler_CallsCoolerRepository()
    {
        var product = CreateCooler();
        var payload = JsonSerializer.SerializeToElement(product);
        _coolerRepository
            .Setup(x => x.UpdateAsync(It.Is<Cooler>(cooler => cooler.id == product.id), product.id, product.Category))
            .ReturnsAsync(HttpStatusCode.OK);

        var result = await _serviceUnit.Update(payload, ProductCategories.Cooler);

        Assert.Equal(HttpStatusCode.OK, result);
        VerifyUpdateCalled(ProductCategories.Cooler, product.id, product.Category);
    }

    [Fact]
    public async Task Update_WhenTypeIsUnsupported_ReturnsBadRequest()
    {
        var payload = JsonSerializer.SerializeToElement(new { id = "123" });

        var result = await _serviceUnit.Update(payload, "RAM");

        Assert.Equal(HttpStatusCode.BadRequest, result);
        VerifyNoUpdateCalls();
    }

    private void VerifyGetByIdCalled(string type, string id)
    {
        _gpuRepository.Verify(x => x.GetById(ProductCategories.Gpu, id), type == ProductCategories.Gpu ? Times.Once : Times.Never);
        _cpuRepository.Verify(x => x.GetById(ProductCategories.Cpu, id), type == ProductCategories.Cpu ? Times.Once : Times.Never);
        _coolerRepository.Verify(x => x.GetById(ProductCategories.Cooler, id), type == ProductCategories.Cooler ? Times.Once : Times.Never);
    }

    private void VerifyNoGetByIdCalls()
    {
        _gpuRepository.Verify(x => x.GetById(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _cpuRepository.Verify(x => x.GetById(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _coolerRepository.Verify(x => x.GetById(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    private void VerifyGetByTypeCalled(string type)
    {
        _gpuRepository.Verify(x => x.GetByType(ProductCategories.Gpu), type == ProductCategories.Gpu ? Times.Once : Times.Never);
        _cpuRepository.Verify(x => x.GetByType(ProductCategories.Cpu), type == ProductCategories.Cpu ? Times.Once : Times.Never);
        _coolerRepository.Verify(x => x.GetByType(ProductCategories.Cooler), type == ProductCategories.Cooler ? Times.Once : Times.Never);
    }

    private void VerifyNoGetByTypeCalls()
    {
        _gpuRepository.Verify(x => x.GetByType(It.IsAny<string>()), Times.Never);
        _cpuRepository.Verify(x => x.GetByType(It.IsAny<string>()), Times.Never);
        _coolerRepository.Verify(x => x.GetByType(It.IsAny<string>()), Times.Never);
    }

    private void VerifyCreateCalled(string type, string id, string category)
    {
        _gpuRepository.Verify(x => x.CreateAsync(It.IsAny<Gpu>(), id, category), type == ProductCategories.Gpu ? Times.Once : Times.Never);
        _cpuRepository.Verify(x => x.CreateAsync(It.IsAny<Cpu>(), id, category), type == ProductCategories.Cpu ? Times.Once : Times.Never);
        _coolerRepository.Verify(x => x.CreateAsync(It.IsAny<Cooler>(), id, category), type == ProductCategories.Cooler ? Times.Once : Times.Never);
    }

    private void VerifyNoCreateCalls()
    {
        _gpuRepository.Verify(x => x.CreateAsync(It.IsAny<Gpu>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _cpuRepository.Verify(x => x.CreateAsync(It.IsAny<Cpu>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _coolerRepository.Verify(x => x.CreateAsync(It.IsAny<Cooler>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    private void VerifyDeleteCalled(string type, Guid id)
    {
        _gpuRepository.Verify(x => x.DeleteAsync(id, ProductCategories.Gpu), type == ProductCategories.Gpu ? Times.Once : Times.Never);
        _cpuRepository.Verify(x => x.DeleteAsync(id, ProductCategories.Cpu), type == ProductCategories.Cpu ? Times.Once : Times.Never);
        _coolerRepository.Verify(x => x.DeleteAsync(id, ProductCategories.Cooler), type == ProductCategories.Cooler ? Times.Once : Times.Never);
    }

    private void VerifyNoDeleteCalls()
    {
        _gpuRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
        _cpuRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
        _coolerRepository.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
    }

    private void VerifyUpdateCalled(string type, string id, string category)
    {
        _gpuRepository.Verify(x => x.UpdateAsync(It.IsAny<Gpu>(), id, category), type == ProductCategories.Gpu ? Times.Once : Times.Never);
        _cpuRepository.Verify(x => x.UpdateAsync(It.IsAny<Cpu>(), id, category), type == ProductCategories.Cpu ? Times.Once : Times.Never);
        _coolerRepository.Verify(x => x.UpdateAsync(It.IsAny<Cooler>(), id, category), type == ProductCategories.Cooler ? Times.Once : Times.Never);
    }

    private void VerifyNoUpdateCalls()
    {
        _gpuRepository.Verify(x => x.UpdateAsync(It.IsAny<Gpu>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _cpuRepository.Verify(x => x.UpdateAsync(It.IsAny<Cpu>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        _coolerRepository.Verify(x => x.UpdateAsync(It.IsAny<Cooler>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    private static Gpu CreateGpu() => new()
    {
        Name = "Test GPU",
        Team = "AMD",
        Price = "3900",
        ProductImg = "gpu.png",
        Category = ProductCategories.Gpu,
        id = "gpu-123",
        Warranty = "36",
        ProducerCode = "GPU123",
        GpuProcessorLine = "RX",
        PcieType = "PCIe 5.0",
        MemorySize = "16 GB",
        MemoryType = "GDDR6",
        MemoryBus = "256-bit",
        MemoryRatio = "18 Gbps",
        CoreRatio = "2400 MHz",
        CoresNumber = "4096",
        CoolingType = "Air",
        OutputsType = "HDMI, DP",
        SupportedLibraries = "DX12",
        PowerConnector = "2x8-pin",
        RecommendedPsuPower = "750W",
        Length = "320 mm",
        Width = "120 mm",
        Height = "60 mm",
        GpuProcessorName = "Navi",
        Manufacturer = "Sapphire"
    };

    private static Cpu CreateCpu() => new()
    {
        Name = "Test CPU",
        Team = "AMD",
        Price = "2500",
        ProductImg = "cpu.png",
        Category = ProductCategories.Cpu,
        id = "cpu-456",
        Warranty = "36",
        ProducerCode = "CPU123",
        Family = "Ryzen",
        Series = "9000",
        Socket = "AM5",
        SupportedChipsets = "X870",
        RecommendedChipset = "X870E",
        Architecture = "Zen 5",
        Frequency = "5.0 GHz",
        Cores = "8",
        Threads = "16",
        UnlockedMultiplayer = "Yes",
        CacheMemory = "96 MB",
        IntegratedGpu = "Yes",
        IntegratedGpuModel = "Radeon",
        SupportedRam = "DDR5",
        Lithography = "4 nm",
        Tdp = "120W",
        AdditionalInfo = "Test",
        IncludedCooler = "No"
    };

    private static Cooler CreateCooler() => new()
    {
        Name = "Test Cooler",
        Team = "Noctua",
        Price = "120",
        ProductImg = "cooler.png",
        Category = ProductCategories.Cooler,
        id = "cooler-789",
        Warranty = "72",
        ProducerCode = "COOL123",
        CoolerType = "Air",
        Compatibility = "AM5",
        Size = "Dual Tower",
        HeatPipes = "7",
        Fans = "2",
        RpmControl = "PWM",
        Rmp = "1500",
        BearingType = "SSO2",
        FanSize = "140 mm",
        Connector = "4-pin",
        SupplyVoltage = "12V",
        SupplyCurrent = "0.2A",
        Highlight = "Quiet",
        MtbfLifetime = "150000h",
        Height = "165 mm",
        Width = "150 mm",
        Depth = "161 mm",
        Weight = "1320 g",
        Manufacture = "Noctua"
    };
}
