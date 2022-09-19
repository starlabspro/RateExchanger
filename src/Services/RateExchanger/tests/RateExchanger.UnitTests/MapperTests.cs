using AutoMapper;
using RateExchanger.Profiles;

namespace RateExchanger.UnitTests;

public class MapperTests
{
    [Fact]
    public void MapperProfile_ForAllAssemblies_ShouldValidateSuccessfully()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<RateExchangerProfile>(); });

        config.AssertConfigurationIsValid();
    }
}