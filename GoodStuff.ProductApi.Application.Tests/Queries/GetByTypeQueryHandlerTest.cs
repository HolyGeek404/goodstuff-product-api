using GoodStuff.ProductApi.Application.Features.Product.Queries.GetByType;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Application.Tests.Helpers;
using GoodStuff.ProductApi.Domain.Products;
using GoodStuff.ProductApi.Domain.Products.Models;
using Moq;

namespace GoodStuff.ProductApi.Application.Tests.Queries;

public class GetByTypeQueryHandlerTest
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<IGpuRepository> _gpuRepo = new();
    private readonly Mock<ICpuRepository> _cpuRepo = new();
    private readonly Mock<ICoolerRepository> _coolerRepo = new();

    public GetByTypeQueryHandlerTest()
    {
        _uow.SetupGet(x => x.GpuRepository).Returns(_gpuRepo.Object);
        _uow.SetupGet(x => x.CpuRepository).Returns(_cpuRepo.Object);
        _uow.SetupGet(x => x.CoolerRepository).Returns(_coolerRepo.Object);
    }

    [Theory]
    [InlineData(ProductCategories.Gpu)]
    [InlineData(ProductCategories.Cpu)]
    [InlineData(ProductCategories.Cooler)]
    public async Task Handle_WhenTypeIsSupported_ReturnsProducts(string type)
    {
        // Arrange
        var handler = new GetByTypeQueryHandler(_uow.Object);
        var query = new GetByTypeQuery { Type = type };

        var expected = SetupRepository(type);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(expected, result);
        VerifyOnly(type);
    }

    [Fact]
    public async Task Handle_WhenTypeIsNotSupported_ReturnsNull()
    {
        // Arrange
        var handler = new GetByTypeQueryHandler(_uow.Object);
        var query = new GetByTypeQuery { Type = "unknown" };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
        VerifyNone();
    }

    // ---------- Helpers ----------

    private object? SetupRepository(string type) => type switch
        {
            ProductCategories.Gpu => Setup<Gpu, IGpuRepository>(_gpuRepo, [ProductFactory.CreateGpu()], type),
            ProductCategories.Cpu => Setup<Cpu, ICpuRepository>(_cpuRepo, [ProductFactory.CreateCpu()], type),
            ProductCategories.Cooler => Setup<Cooler, ICoolerRepository>(_coolerRepo, [ProductFactory.CreateCooler()], type),
            _ => null
        };

    private static List<T> Setup<T, TRepo>(Mock<TRepo> repo, List<T> data, string type)
        where T : class
        where TRepo : class, IReadRepository<T>
    {
        repo.Setup(r => r.GetByType(type)).ReturnsAsync(data); return data;
    }

    private void VerifyOnly(string type)
    {
        _gpuRepo.Verify(r => r.GetByType(ProductCategories.Gpu), type == ProductCategories.Gpu ? Times.Once() : Times.Never());
        _cpuRepo.Verify(r => r.GetByType(ProductCategories.Cpu), type == ProductCategories.Cpu ? Times.Once() : Times.Never());
        _coolerRepo.Verify(r => r.GetByType(ProductCategories.Cooler), type == ProductCategories.Cooler ? Times.Once() : Times.Never());
    }

    private void VerifyNone()
    {
        _gpuRepo.Verify(r => r.GetByType(It.IsAny<string>()), Times.Never);
        _cpuRepo.Verify(r => r.GetByType(It.IsAny<string>()), Times.Never);
        _coolerRepo.Verify(r => r.GetByType(It.IsAny<string>()), Times.Never);
    }
}
