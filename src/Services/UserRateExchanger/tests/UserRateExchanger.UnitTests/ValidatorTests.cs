using FluentAssertions;
using UserRateExchanger.Features;
using UserRateExchanger.UnitTests.Helpers;

namespace UserRateExchanger.UnitTests;

public class ValidatorTests
{
    private readonly GetUserRateCommandValidator _validator;

    public ValidatorTests()
    {
        _validator = new GetUserRateCommandValidator();
    }

    [Fact]
    public async Task GetRateCommandValidator_ForValidCommand_Succeeds()
    {
        // Arrange
        var validRequest = TestHelper.GenerateValidGetUserRateCommand();

        // Act
        var validationResult = await _validator.ValidateAsync(validRequest);

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task GetRateCommandValidator_ForInvalidCommand_ReturnsValidationErrors()
    {
        // Arrange
        var validRequest = TestHelper.GenerateInvalidGetUserRateCommand();

        // Act
        var validationResult = await _validator.ValidateAsync(validRequest);

        // Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().Contain(x => x.PropertyName == nameof(GetUserRateCommand.BaseCurrency));
    }
}