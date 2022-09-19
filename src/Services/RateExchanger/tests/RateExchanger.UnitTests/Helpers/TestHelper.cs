using Bogus;
using RateExchanger.Features;

namespace RateExchanger.UnitTests.Helpers;

public static class TestHelper
{
    public static GetRateCommand GenerateValidGetRateCommand()
    {
        return new Faker<GetRateCommand>()
            .CustomInstantiator(f =>
                new GetRateCommand(
                    f.Finance.Currency().Code,
                    new[] { f.Finance.Currency().Code }
                )
            );
    }

    public static GetRateCommand GenerateInvalidGetRateCommand()
    {
        return new Faker<GetRateCommand>()
            .CustomInstantiator(f =>
                new GetRateCommand(
                    null,
                    null
                )
            );
    }
}