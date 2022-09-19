using Bogus;
using UserRateExchanger.Features;

namespace UserRateExchanger.UnitTests.Helpers;

public static class TestHelper
{
    public static GetUserRateCommand GenerateValidGetUserRateCommand()
    {
        return new Faker<GetUserRateCommand>()
            .RuleFor(x => x.BaseCurrency, f => f.Finance.Currency().Code)
            .RuleFor(x => x.OtherCurrencies, f => new[] { f.Finance.Currency().Code })
            .RuleFor(x => x.UserId, f => f.Random.Int(1, 10))
            .Generate();
    }

    public static GetUserRateCommand GenerateInvalidGetUserRateCommand()
    {
        return new Faker<GetUserRateCommand>()
            .RuleFor(x => x.BaseCurrency, null as string)
            .RuleFor(x => x.OtherCurrencies, null as string[])
            .RuleFor(x => x.UserId, 0)
            .Generate();
    }
}