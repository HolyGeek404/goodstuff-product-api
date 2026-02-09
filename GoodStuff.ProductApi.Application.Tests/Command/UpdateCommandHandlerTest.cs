using System.Net;
using System.Text.Json;
using GoodStuff.ProductApi.Application.Features.Product.Commands.Update;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Application.Tests.Helpers;
using GoodStuff.ProductApi.Domain.Products;
using GoodStuff.ProductApi.Domain.Products.Models;
using Moq;

namespace GoodStuff.ProductApi.Application.Tests.Command;

public class UpdateCommandHandlerTest
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IGpuRepository> _gpuRepo = new();
    private readonly Mock<ICpuRepository> _cpuRepo = new();
    private readonly Mock<ICoolerRepository> _coolerRepo = new();
    private readonly UpdateCommandHandler _handler;

    public UpdateCommandHandlerTest()
    {
        _uow.SetupGet(x => x.GpuRepository).Returns(_gpuRepo.Object);
        _uow.SetupGet(x => x.CpuRepository).Returns(_cpuRepo.Object);
        _uow.SetupGet(x => x.CoolerRepository).Returns(_coolerRepo.Object);

        _handler = new UpdateCommandHandler(_uow.Object);
    }

    [Fact]
    public async Task Handle_WhenTypeIsGpu_UpdatesGpuAndReturnsStatus()
    {
        // Arrange
        var gpu = ProductFactory.CreateGpu();
        var command = new UpdateCommand
        {
            Type = ProductCategories.Gpu,
            BaseProduct = JsonSerializer.SerializeToElement(gpu)
        };

        _gpuRepo.Setup(r => r.UpdateAsync(It.IsAny<Gpu>(), gpu.id, gpu.Category)).ReturnsAsync(HttpStatusCode.OK);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result);
        _gpuRepo.Verify(r => r.UpdateAsync(It.Is<Gpu>(g => g.id == gpu.id), gpu.id, gpu.Category), Times.Once);

        VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_WhenTypeIsCpu_UpdatesCpuAndReturnsStatus()
    {
        // Arrange
        var cpu = ProductFactory.CreateCpu();
        var command = new UpdateCommand
        {
            Type = ProductCategories.Cpu,
            BaseProduct = JsonSerializer.SerializeToElement(cpu)
        };

        _cpuRepo.Setup(r => r.UpdateAsync(It.IsAny<Cpu>(), cpu.id, cpu.Category)).ReturnsAsync(HttpStatusCode.OK);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result);
        _cpuRepo.Verify(r => r.UpdateAsync(It.Is<Cpu>(c => c.id == cpu.id), cpu.id, cpu.Category), Times.Once);

        VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_WhenTypeIsCooler_UpdatesCoolerAndReturnsStatus()
    {
        // Arrange
        var cooler = ProductFactory.CreateCooler();
        var command = new UpdateCommand
        {
            Type = ProductCategories.Cooler,
            BaseProduct = JsonSerializer.SerializeToElement(cooler)
        };

        _coolerRepo.Setup(r => r.UpdateAsync(It.IsAny<Cooler>(), cooler.id, cooler.Category)).ReturnsAsync(HttpStatusCode.OK);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result);
        _coolerRepo.Verify(r => r.UpdateAsync(It.Is<Cooler>(c => c.id == cooler.id), cooler.id, cooler.Category), Times.Once);

        VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_WhenTypeIsUnsupported_ReturnsBadRequest()
    {
        // Arrange
        var command = new UpdateCommand
        {
            Type = "unknown",
            BaseProduct = new JsonElement()
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, result);
        VerifyNoOtherCalls();
    }

    [Fact]
    public async Task Handle_WhenCancelled_ThrowsOperationCanceledException()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        var command = new UpdateCommand
        {
            Type = ProductCategories.Gpu,
            BaseProduct = new JsonElement()
        };

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() =>
            _handler.Handle(command, cts.Token));
    }

    // ---------- Helpers ----------

    private void VerifyNoOtherCalls()
    {
        _gpuRepo.Verify(r => r.UpdateAsync(It.IsAny<Gpu>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtMostOnce);
        _cpuRepo.Verify(r => r.UpdateAsync(It.IsAny<Cpu>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtMostOnce);
        _coolerRepo.Verify(r => r.UpdateAsync(It.IsAny<Cooler>(), It.IsAny<string>(), It.IsAny<string>()), Times.AtMostOnce);
    }
}
