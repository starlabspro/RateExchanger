using FluentAssertions;
using RateExchanger.Features;
using RateExchanger.UnitTests.Helpers;

namespace RateExchanger.UnitTests;

public class ValidatorTests
{
    private readonly GetRateCommandValidator _validator;

    public ValidatorTests()
    {
        _validator = new GetRateCommandValidator();
    }

    [Fact]
    public async Task GetRateCommandValidator_ForValidCommand_Succeeds()
    {
        // Arrange
        var validRequest = TestHelper.GenerateValidGetRateCommand();

        // Act
        var validationResult = await _validator.ValidateAsync(validRequest);

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task GetRateCommandValidator_ForInvalidCommand_ReturnsValidationErrors()
    {
        // Arrange
        var invalidRequest = TestHelper.GenerateInvalidGetRateCommand();

        // Act
        var validationResult = await _validator.ValidateAsync(invalidRequest);

        // Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(x => x.PropertyName == nameof(GetRateCommand.BaseCurrency));
    }
}