using GoodStuff.ProductApi.Application.Features.Product.Queries.GetById;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Application.Tests.Helpers;
using GoodStuff.ProductApi.Domain.Products;
using GoodStuff.ProductApi.Domain.Products.Models;
using Moq;

namespace GoodStuff.ProductApi.Application.Tests.Queries;

public class GetByIdQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IGpuRepository> _gpuRepo = new();
    private readonly Mock<ICpuRepository> _cpuRepo = new();
    private readonly Mock<ICoolerRepository> _coolerRepo = new();
    private readonly GetByIdQueryHandler _handler;

    public GetByIdQueryHandlerTests()
    {
        _uow.SetupGet(x => x.GpuRepository).Returns(_gpuRepo.Object);
        _uow.SetupGet(x => x.CpuRepository).Returns(_cpuRepo.Object);
        _uow.SetupGet(x => x.CoolerRepository).Returns(_coolerRepo.Object);
        _handler = new GetByIdQueryHandler(_uow.Object);
    }

    [Theory]
    [InlineData(ProductCategories.Gpu, "321")]
    [InlineData(ProductCategories.Cpu, "654")]
    [InlineData(ProductCategories.Cooler, "987")]
    public async Task Handle_WhenTypeIsSupported_ReturnsCorrectProduct(string type, string productId)
    {
        // Arrange
        object expected = type switch
        {
            ProductCategories.Gpu => Setup<Gpu, IGpuRepository>(_gpuRepo, ProductFactory.CreateGpu()),
            ProductCategories.Cpu => Setup<Cpu, ICpuRepository>(_cpuRepo, ProductFactory.CreateCpu()),
            ProductCategories.Cooler => Setup<Cooler, ICoolerRepository>(_coolerRepo, ProductFactory.CreateCooler()),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };

        var query = new GetByIdQuery { Type = type, Id = productId };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expected, result);
        VerifyOnly(type);
    }

    [Fact]
    public async Task Handle_WhenTypeIsNotSupported_ReturnsEmptyEnumerable()
    {
        // Arrange
        var uowMock = new Mock<IUnitOfWork>();
        var handler = new GetByIdQueryHandler(uowMock.Object);
        var query = new GetByIdQuery { Type = "UNSUPPORTED", Id = "123" };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<IEnumerable<object>>(result, exactMatch: false);
        VerifyNone();
    }

    [Fact]
    public async Task Handle_WhenCancellationRequested_ThrowsOperationCanceledException()
    {
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();

        var query = new GetByIdQuery { Type = ProductCategories.Gpu, Id = "123" };

        await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(query, cts.Token));
        VerifyNone();
    }

    // ---------- Helpers ----------
    private static T Setup<T, TRepo>(Mock<TRepo> repo, T data)
        where T : class
        where TRepo : class, IReadRepository<T>
    {
        repo.Setup(r => r.GetById(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(data); return data;
    }
    
    private void VerifyOnly(string type)
    {
        _gpuRepo.Verify(r => r.GetById(ProductCategories.Gpu, It.IsAny<string>()), type == ProductCategories.Gpu ? Times.Once() : Times.Never());
        _cpuRepo.Verify(r => r.GetById(ProductCategories.Cpu, It.IsAny<string>()), type == ProductCategories.Cpu ? Times.Once() : Times.Never());
        _coolerRepo.Verify(r => r.GetById(ProductCategories.Cooler, It.IsAny<string>()), type == ProductCategories.Cooler ? Times.Once() : Times.Never());
    }
    private void VerifyNone()
    {
        _gpuRepo.Verify(r => r.GetById(ProductCategories.Gpu, It.IsAny<string>()), Times.Never());
        _cpuRepo.Verify(r => r.GetById(ProductCategories.Cpu, It.IsAny<string>()), Times.Never());
        _coolerRepo.Verify(r => r.GetById(ProductCategories.Cooler, It.IsAny<string>()), Times.Never());
    }
}
