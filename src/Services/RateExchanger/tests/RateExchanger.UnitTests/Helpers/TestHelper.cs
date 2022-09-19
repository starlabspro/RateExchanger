using Bogus;
using RateExchanger.Features;

namespace RateExchanger.UnitTests.Helpers;

public static class TestHelper
{
    public static GetRateCommand GenerateValidGetRateCommand(string? baseCurrency = null)
    {
        return new Faker<GetRateCommand>()
            .CustomInstantiator(f =>
                new GetRateCommand(
                    baseCurrency ?? f.Finance.Currency().Code,
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

    public static GetRateResponseDto GenerateValidGetRateResponseDto(string? baseCurrency = null)
    {
        return new Faker<GetRateResponseDto>()
            .RuleFor(x => x.BaseCurrency, f => baseCurrency ?? f.Finance.Currency().Code)
            .RuleFor(x => x.Rates, f => new Dictionary<string, decimal>()
            {
                { f.Finance.Currency().Code, f.Random.Decimal() }
            })
            .Generate();
    }
}