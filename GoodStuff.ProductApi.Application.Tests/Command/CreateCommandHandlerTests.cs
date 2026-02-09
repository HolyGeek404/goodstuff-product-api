using GoodStuff.ProductApi.Application.Features.Product.Commands.Create;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products;
using GoodStuff.ProductApi.Domain.Products.Models;
using Moq;
using System.Text.Json;
using GoodStuff.ProductApi.Application.Tests.Helpers;

namespace GoodStuff.ProductApi.Application.Tests.Command;

public class CreateCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IGpuRepository> _gpuRepo = new();
    private readonly Mock<ICpuRepository> _cpuRepo = new();
    private readonly Mock<ICoolerRepository> _coolerRepo = new();
    private readonly CreateCommandHandler _handler;

    public CreateCommandHandlerTests()
    {
        _uow.SetupGet(u => u.GpuRepository).Returns(_gpuRepo.Object);
        _uow.SetupGet(u => u.CpuRepository).Returns(_cpuRepo.Object);
        _uow.SetupGet(u => u.CoolerRepository).Returns(_coolerRepo.Object);
        _handler = new CreateCommandHandler(_uow.Object);
    }

    [Fact]
    public async Task Handle_WhenTypeIsGpu_CallsGpuRepository()
    {
        // Arrange
        var gpu = ProductFactory.CreateGpu();
        var command = new CreateCommand
        {
            Type = ProductCategories.Gpu,
            Product = JsonSerializer.SerializeToElement(gpu)
        };

        _gpuRepo.Setup(r => r.CreateAsync(It.IsAny<Gpu>(), gpu.id, gpu.Category)).ReturnsAsync(gpu);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Gpu>(result);
        _gpuRepo.Verify(r => r.CreateAsync(It.IsAny<Gpu>(), gpu.id, gpu.Category), Times.Once);

        VerifyOnly(command.Type);
    }

    [Fact]
    public async Task Handle_WhenTypeIsCpu_CallsCpuRepository()
    {
        // Arrange
        var cpu = ProductFactory.CreateCpu();
        var command = new CreateCommand
        {
            Type = ProductCategories.Cpu,
            Product = JsonSerializer.SerializeToElement(cpu)
        };
        _cpuRepo.Setup(r => r.CreateAsync(It.IsAny<Cpu>(), cpu.id, cpu.Category)).ReturnsAsync(cpu);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Cpu>(result);
        _cpuRepo.Verify(r => r.CreateAsync(It.IsAny<Cpu>(), cpu.id, cpu.Category), Times.Once);

        VerifyOnly(command.Type);
    }
    
    [Fact]
    public async Task Handle_WhenTypeIsCooler_CallsCoolerRepository()
    {
        // Arrange
        var cooler = ProductFactory.CreateCooler();
        var command = new CreateCommand
        {
            Type = ProductCategories.Cooler,
            Product = JsonSerializer.SerializeToElement(cooler)
        };
        _coolerRepo.Setup(r => r.CreateAsync(It.IsAny<Cooler>(), cooler.id, cooler.Category)).ReturnsAsync(cooler);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<Cooler>(result);
        _coolerRepo.Verify(r => r.CreateAsync(It.IsAny<Cooler>(), cooler.id, cooler.Category), Times.Once);

        VerifyOnly(command.Type);
    }

    [Fact]
    public async Task Handle_WhenTypeIsUnsupported_ReturnsNull()
    {
        // Arrange
        var command = new CreateCommand
        {
            Type = "unknown",
            Product = new JsonElement()
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
        VerifyOnly(command.Type);
    }

    // ---------- Helpers ----------

    private void VerifyOnly(string type)
    {
        _gpuRepo.Verify(r => r.CreateAsync(It.IsAny<Gpu>(), It.IsAny<string>(),It.IsAny<string>()), type == ProductCategories.Gpu ? Times.Once() : Times.Never());
        _cpuRepo.Verify(r => r.CreateAsync(It.IsAny<Cpu>(), It.IsAny<string>(),It.IsAny<string>()), type == ProductCategories.Cpu ? Times.Once() : Times.Never());
        _coolerRepo.Verify(r => r.CreateAsync(It.IsAny<Cooler>(), It.IsAny<string>(),It.IsAny<string>()), type == ProductCategories.Cooler ? Times.Once() : Times.Never());
    }
}
