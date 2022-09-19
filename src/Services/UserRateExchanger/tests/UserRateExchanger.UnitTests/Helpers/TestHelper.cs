using Bogus;
using UserRateExchanger.Features;

namespace UserRateExchanger.UnitTests.Helpers;

public static class TestHelper
{
    public static GetUserRateCommand GenerateValidGetUserRateCommand()
    {
        return new Faker<GetUserRateCommand>()
            .CustomInstantiator(f =>
                new GetUserRateCommand(
                    f.Random.Int(1, 10),
                    f.Finance.Currency().Code,
                    new[] { f.Finance.Currency().Code }
                )
            );
    }

    public static GetUserRateCommand GenerateInvalidGetUserRateCommand()
    {
        return new Faker<GetUserRateCommand>()
            .CustomInstantiator(f =>
                new GetUserRateCommand(
                    -1,
                    null,
                    null
                )
            );
    }
}