using GoodStuff.ProductApi.Application.DTO;
using GoodStuff.ProductApi.Application.Features.Product.Queries.GetFilters;
using GoodStuff.ProductApi.Application.Interfaces;
using GoodStuff.ProductApi.Domain.Products;
using Moq;

namespace GoodStuff.ProductApi.Application.Tests.Queries;

public class GetFiltersQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _uow = new();
    private readonly Mock<ICpuRepository> _cpuRepo = new();
    private readonly GetFiltersQueryHandler _handler;

    public GetFiltersQueryHandlerTests()
    {
        _uow.SetupGet(x => x.CpuRepository).Returns(_cpuRepo.Object);
        _handler = new GetFiltersQueryHandler(_uow.Object);
    }

    [Fact]
    public async Task Handle_WhenTypeIsCpu_ReturnsFilters()
    {
        // Arrange
        var expected = new CpuFilters
        {
            Team = ["AMD"],
            Cores = ["8"],
            Socket = ["AM5"],
            Architecture = ["Zen 5"],
            TDP = ["105W"]
        };
        var query = new GetFiltersQuery { Type = ProductCategories.Cpu };

        _cpuRepo.Setup(r => r.GetFiltersAsync(ProductCategories.Cpu)).ReturnsAsync(expected);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expected, result);
        _cpuRepo.Verify(r => r.GetFiltersAsync(ProductCategories.Cpu), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenTypeIsUnsupported_ReturnsNull()
    {
        // Arrange
        var query = new GetFiltersQuery { Type = "unknown" };

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);
        _cpuRepo.Verify(r => r.GetFiltersAsync(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenCancellationRequested_ThrowsOperationCanceledException()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        await cts.CancelAsync();
        var query = new GetFiltersQuery { Type = ProductCategories.Cpu };

        // Act & Assert
        await Assert.ThrowsAsync<OperationCanceledException>(() => _handler.Handle(query, cts.Token));
        _cpuRepo.Verify(r => r.GetFiltersAsync(It.IsAny<string>()), Times.Never);
    }
}
