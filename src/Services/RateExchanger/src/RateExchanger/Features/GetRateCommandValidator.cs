using FluentValidation;

namespace RateExchanger.Features;

public class GetRateCommandValidator : AbstractValidator<GetRateCommand>
{
    public GetRateCommandValidator()
    {
        RuleFor(x => x.BaseCurrency)
            .NotNull()
            .NotEmpty()
            .Length(3)
            .WithMessage($"{nameof(GetRateCommand.BaseCurrency)} must be a valid 3 letter currency code");
    }
}