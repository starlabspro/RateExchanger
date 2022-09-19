using BuildingBlocks.Caching;
using EasyCaching.Core;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RateExchanger.Features;

namespace RateExchanger.UnitTests;

public class CacheManagerTests
{
    private const string CacheName = "TestCache";

    // Task<T?> GetAsync(string key, CancellationToken cancellationToken);
    // Task<bool> IsValidAsync(string key, CancellationToken cancellationToken);
    // Task UpdateAsync(string key, T? data, CancellationToken cancellationToken);

    [Fact]
    public async Task GetAsync_ForValidKey_ReturnsCachedValues()
    {
        // Arrange
        var key = "sample-key";
        var loggerMock = new Mock<ILogger<RateExchangerCacheManager>>();
        var optionsMock = new Mock<IOptions<CacheOptions>>();
        var factoryMock = new Mock<IEasyCachingProviderFactory>();
        var providerMock = new Mock<IEasyCachingProvider>();

        optionsMock
            .Setup(x => x.Value)
            .Returns(new CacheOptions
            {
                CacheName = CacheName,
                ExpirationTime = 100,
                SizeLimit = 100
            });

        providerMock
            .Setup(x => x.GetAsync<Dictionary<string, decimal>>(key, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CacheValue<Dictionary<string, decimal>>(new Dictionary<string, decimal>()
            {
                { "USD", 1.0m },
                { "EUR", 0.9m }
            }, true));

        factoryMock
            .Setup(x => x.GetCachingProvider(CacheName))
            .Returns(providerMock.Object);

        var sut = new RateExchangerCacheManager(loggerMock.Object, optionsMock.Object, factoryMock.Object);

        // Act
        var cachedValues = await sut.GetAsync(key, It.IsAny<CancellationToken>());

        // Assert
        cachedValues.Should().NotBeNull();
        cachedValues.Should().ContainKey("USD").WhoseValue.Should().Be(1.0m);
        cachedValues.Should().ContainKey("EUR").WhoseValue.Should().Be(0.9m);
    }

    [Fact]
    public async Task GetAsync_ForInvalidKey_ReturnsCachedValues()
    {
        // Arrange
        var key = "sample-key";
        var invalidKey = "invalid-key";
        var loggerMock = new Mock<ILogger<RateExchangerCacheManager>>();
        var optionsMock = new Mock<IOptions<CacheOptions>>();
        var factoryMock = new Mock<IEasyCachingProviderFactory>();
        var providerMock = new Mock<IEasyCachingProvider>();

        optionsMock
            .Setup(x => x.Value)
            .Returns(new CacheOptions
            {
                CacheName = CacheName,
                ExpirationTime = 100,
                SizeLimit = 100
            });

        providerMock
            .Setup(x => x.GetAsync<Dictionary<string, decimal>>(key, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CacheValue<Dictionary<string, decimal>>(new Dictionary<string, decimal>()
            {
                { "USD", 1.0m },
                { "EUR", 0.9m }
            }, true));

        factoryMock
            .Setup(x => x.GetCachingProvider(CacheName))
            .Returns(providerMock.Object);

        var sut = new RateExchangerCacheManager(loggerMock.Object, optionsMock.Object, factoryMock.Object);

        // Act
        var cachedValues = await sut.GetAsync(invalidKey, It.IsAny<CancellationToken>());

        // Assert
        cachedValues.Should().BeNull();
    }
}