using AutoMapper;
using BuildingBlocks.Caching;
using BuildingBlocks.Contracts;
using EasyCaching.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using UserRateExchanger.Features;

namespace UserRateExchanger.UnitTests;

public class GetUserRateCommandHandlerTests
{
    private readonly Mock<ILogger<GetUserRateCommandHandler>> _loggerMock;
    private readonly Mock<IOptions<CacheOptions>> _cacheOptionsMock;
    private readonly Mock<IEasyCachingProvider> _cachingProviderMock;
    private readonly Mock<IRateExchangerService> _rateExchangerServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetUserRateCommandHandler _userRateCommandHandlerStub;

    public GetUserRateCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<GetUserRateCommandHandler>>();
        _cacheOptionsMock = new Mock<IOptions<CacheOptions>>();
        _cachingProviderMock = new Mock<IEasyCachingProvider>();
        _rateExchangerServiceMock = new Mock<IRateExchangerService>();
        _mapperMock = new Mock<IMapper>();
        _userRateCommandHandlerStub = new GetUserRateCommandHandler(_loggerMock.Object, _cacheOptionsMock.Object,
            _cachingProviderMock.Object, _rateExchangerServiceMock.Object, _mapperMock.Object);

    }

    [Fact]
    public void Handle_WithValidData_ReturnsSuccess()
    {
        var userRateCommand = new GetUserRateCommand()
        {
            BaseCurrency = "Euro",
            OtherCurrencies = new[]
            {
                "Pound",
                "Dollar"
            }
        };
        var userRateResponseDto = new GetUserRateResponseDto
        {
            BaseCurrency = "Euro",
            Rates = new Dictionary<string, decimal>()
            {
                { "Euro", Decimal.MinValue }
            }
        };
        var exchangeRateRequest = new GetExchangeRateRequest()
        {
            BaseCurrency = "Euro",
            OtherCurrencies = new []
            {
                "Dollar",
                "Pound"
            }
        };
        
        var exchangeRateResponse = new GetExchangeRateResponse("Euro", new Dictionary<string, decimal>());
        var cacheOptions = new CacheOptions()
        {
            ExpirationTime = 60,
            SizeLimit = 2,
            CacheName = "test"
        };
        
        var list = new CacheValue<List<DateTime>>(new List<DateTime>()
        {
            new DateTime(2022, 3, 4, 23, 2, 3),
            new DateTime(2022, 3, 4, 20, 2, 3),
            new DateTime(2022, 3, 4, 13, 2, 1)
        }, true);
        
         _cachingProviderMock.Setup(x => x.GetAsync<List<DateTime>>(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(list));
         _cacheOptionsMock.Setup(x => x.Value).Returns(cacheOptions);
        _mapperMock.Setup(x => x.Map<GetExchangeRateRequest>(It.IsAny<GetUserRateCommand>()))
            .Returns(exchangeRateRequest);
        _rateExchangerServiceMock.Setup(x => x.GetExchangeRateAsync(exchangeRateRequest))
            .Returns(Task.FromResult(exchangeRateResponse));
        _mapperMock.Setup(x => x.Map<GetUserRateResponseDto>(It.IsAny<GetExchangeRateResponse>()))
            .Returns(userRateResponseDto);

        var result = _userRateCommandHandlerStub.Handle(userRateCommand, default);

        Assert.NotNull(result);
        
    }
}