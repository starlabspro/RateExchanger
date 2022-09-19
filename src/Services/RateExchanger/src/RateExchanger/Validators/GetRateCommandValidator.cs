using FluentValidation;
using RateExchanger.Features;

namespace RateExchanger.Validators;

public class GetRateCommandValidator : AbstractValidator<GetRateCommand>
{
    public GetRateCommandValidator()
    {
        RuleFor(x => x.BaseCurrency)
            .NotNull()
            .NotEmpty()
            .WithMessage($"{nameof(GetRateCommand.BaseCurrency)} must not be null");
    }
}