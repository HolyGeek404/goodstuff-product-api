using System.Net;
using GoodStuff.ProductApi.Application.Features.Product.Commands.Delete;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products;
using GoodStuff.ProductApi.Domain.Products.Models;
using Moq;

namespace GoodStuff.ProductApi.Application.Tests.Command;

public class DeleteCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IGpuRepository> _gpuRepo = new();
    private readonly Mock<ICpuRepository> _cpuRepo = new();
    private readonly Mock<ICoolerRepository> _coolerRepo = new();

    private readonly DeleteCommandHandler _handler;

    public DeleteCommandHandlerTests()
    {
        _gpuRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(HttpStatusCode.OK);
        _cpuRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(HttpStatusCode.OK);
        _coolerRepo.Setup(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(HttpStatusCode.OK);

        _uow.SetupGet(x => x.GpuRepository).Returns(_gpuRepo.Object);
        _uow.SetupGet(x => x.CpuRepository).Returns(_cpuRepo.Object);
        _uow.SetupGet(x => x.CoolerRepository).Returns(_coolerRepo.Object);

        _handler = new DeleteCommandHandler(_uow.Object);
    }

    [Theory]
    [InlineData(ProductCategories.Gpu)]
    [InlineData(ProductCategories.Cpu)]
    [InlineData(ProductCategories.Cooler)]
    public async Task Handle_WhenTypeIsSupported_CallsCorrectRepository(string type)
    {
        // Arrange
        var request = new DeleteCommand
        {
            Type = type,
            Id = Guid.NewGuid()
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.OK, result);
        VerifyOnly(type, request.Id);
    }

    [Fact]
    public async Task Handle_WhenTypeIsUnsupported_ReturnsBadRequest()
    {
        // Arrange
        var request = new DeleteCommand
        {
            Type = "RAM",
            Id = Guid.NewGuid()
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, result);
        VerifyNone();
    }

    [Fact]
    public async Task Handle_WhenCancellationRequested_ThrowsOperationCanceledException()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        var request = new DeleteCommand
        {
            Type = ProductCategories.Gpu,
            Id = Guid.NewGuid()
        };

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(request, cts.Token));
    }

    // ---------- Helpers ----------

    private void VerifyOnly(string type, Guid id)
    {
        _gpuRepo.Verify(r => r.DeleteAsync(id, ProductCategories.Gpu), type == ProductCategories.Gpu ? Times.Once() : Times.Never());
        _cpuRepo.Verify(r => r.DeleteAsync(id, ProductCategories.Cpu), type == ProductCategories.Cpu ? Times.Once() : Times.Never());
        _coolerRepo.Verify(r => r.DeleteAsync(id, ProductCategories.Cooler), type == ProductCategories.Cooler ? Times.Once() : Times.Never());
    }

    private void VerifyNone()
    {
        _gpuRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
        _cpuRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
        _coolerRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<string>()), Times.Never);
    }
}
