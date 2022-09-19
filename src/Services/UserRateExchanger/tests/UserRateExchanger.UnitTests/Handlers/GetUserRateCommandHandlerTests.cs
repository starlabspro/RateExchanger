using AutoMapper;
using BuildingBlocks.Caching;
using BuildingBlocks.Contracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using UserRateExchanger.Features;

namespace UserRateExchanger.UnitTests;

public class GetUserRateCommandHandlerTests
{
    private readonly Mock<ILogger<GetUserRateCommandHandler>> _loggerMock;
    private readonly Mock<ICacheManager<List<DateTime>>> _cacheManagerMock;
    private readonly Mock<IRateExchangerService> _rateExchangerServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetUserRateCommandHandler _sut;

    public GetUserRateCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetUserRateCommandHandler>>();
        _cacheManagerMock = new Mock<ICacheManager<List<DateTime>>>();
        _rateExchangerServiceMock = new Mock<IRateExchangerService>();
        _mapperMock = new Mock<IMapper>();
        _sut = new GetUserRateCommandHandler(_loggerMock.Object,
            _rateExchangerServiceMock.Object, _mapperMock.Object, _cacheManagerMock.Object);
    }

    [Fact]
    public void Handle_WithValidData_ReturnsSuccess()
    {
        var userRateCommand = new GetUserRateCommand()
        {
            BaseCurrency = "EUR",
            OtherCurrencies = new[]
            {
                "GBR",
                "USD"
            }
        };
        var userRateResponseDto = new GetUserRateResponseDto
        {
            BaseCurrency = "EUR",
            Rates = new Dictionary<string, decimal>()
            {
                { "EUR", Decimal.MinValue }
            }
        };
        var exchangeRateRequest = new GetExchangeRateRequest()
        {
            BaseCurrency = "EUR",
            OtherCurrencies = new []
            {
                "USD",
                "GBR"
            }
        };

        var exchangeRateResponse = new GetExchangeRateResponse("EUR", new Dictionary<string, decimal>());

        var dateTimestamps = new List<DateTime>()
        {
            new(2022, 3, 4, 23, 2, 3),
            new(2022, 3, 4, 20, 2, 3),
            new(2022, 3, 4, 13, 2, 1)
        };

        _cacheManagerMock.Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dateTimestamps);
        _cacheManagerMock.Setup(x => x.IsValidAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _mapperMock.Setup(x => x.Map<GetExchangeRateRequest>(It.IsAny<GetUserRateCommand>()))
            .Returns(exchangeRateRequest);
        _rateExchangerServiceMock.Setup(x => x.GetExchangeRateAsync(exchangeRateRequest))
            .Returns(Task.FromResult(exchangeRateResponse));
        _mapperMock.Setup(x => x.Map<GetUserRateResponseDto>(It.IsAny<GetExchangeRateResponse>()))
            .Returns(userRateResponseDto);

        var result = _sut.Handle(userRateCommand, default);

        result.Should().NotBeNull();
        _cacheManagerMock.Verify(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        _cacheManagerMock.Verify(x => x.IsValidAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Once);
        _cacheManagerMock.Verify(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<List<DateTime>>(), It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(x => x.Map<GetExchangeRateRequest>(It.IsAny<GetUserRateCommand>()), Times.Once);
        _mapperMock.Verify(x => x.Map<GetUserRateResponseDto>(It.IsAny<GetExchangeRateResponse>()), Times.Once);
        _rateExchangerServiceMock.Verify(x => x.GetExchangeRateAsync(exchangeRateRequest), Times.Once);
        _cacheManagerMock.VerifyNoOtherCalls();
        _mapperMock.VerifyNoOtherCalls();
        _rateExchangerServiceMock.VerifyNoOtherCalls();
    }
}