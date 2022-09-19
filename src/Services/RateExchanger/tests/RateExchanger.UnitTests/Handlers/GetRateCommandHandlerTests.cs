using AutoMapper;
using BuildingBlocks.Caching;
using BuildingBlocks.FixerClient;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RateExchanger.Data;
using RateExchanger.Features;
using RateExchanger.UnitTests.Helpers;

namespace RateExchanger.UnitTests.Handlers;

public class GetRateCommandHandlerTests
{
    [Fact]
    public async Task GetRateCommandHandler_ForValidCommand_ReturnsSuccess()
    {
        // Arrange
        var baseCurrency = "EUR";
        var loggerMock = new Mock<ILogger<GetRateCommandHandler>>();
        var contextMock = new Mock<RateExchangerContext>();
        var fixerClient = new Mock<IFixerClient>();
        var mapperMock = new Mock<IMapper>();
        var cacheManagerMock = new Mock<ICacheManager<Dictionary<string, decimal>>>();

        cacheManagerMock
            .Setup(x => x.GetAsync(baseCurrency, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Dictionary<string, decimal> { { baseCurrency, 1.2m } });

        mapperMock
            .Setup(x => x.Map<GetRateResponseDto>(It.IsAny<GetRateCommand>()))
            .Returns(TestHelper.GenerateValidGetRateResponseDto());

        var sut = new GetRateCommandHandler(
            loggerMock.Object,
            contextMock.Object,
            fixerClient.Object,
            mapperMock.Object,
            cacheManagerMock.Object);

        var command = TestHelper.GenerateValidGetRateCommand(baseCurrency);

        // Act
        var result = await sut.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        result.Should().NotBeNull();
    }
}