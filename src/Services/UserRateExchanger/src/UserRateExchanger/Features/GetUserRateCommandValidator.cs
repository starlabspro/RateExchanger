using FluentValidation;

namespace UserRateExchanger.Features;

public class GetUserRateCommandValidator : AbstractValidator<GetUserRateCommand>
{
    public GetUserRateCommandValidator()
    {
        RuleFor(x => x.BaseCurrency)
            .NotNull()
            .NotEmpty()
            .Length(3)
            .WithMessage($"{nameof(GetUserRateCommand.BaseCurrency)} must not be null.");

        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage($"{nameof(GetUserRateCommand.UserId)} must be a number greater than 0.");
    }
}