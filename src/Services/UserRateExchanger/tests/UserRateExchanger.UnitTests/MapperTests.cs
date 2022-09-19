using AutoMapper;
using UserRateExchanger.Profiles;

namespace UserRateExchanger.UnitTests;

public class MapperTests
{
    [Fact]
    public void MapperProfile_ForAllAssemblies_ShouldValidateSuccessfully()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<UserRateExchangerProfile>(); });

        config.AssertConfigurationIsValid();
    }
}