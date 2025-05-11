using CNAB.Domain.Validations;
using FluentAssertions;

namespace CNAB.Domain.Test.Validations;

public class DomainExceptionValidationTest
{
    [Fact(DisplayName = "GetErrors - No Errors No Exception Thrown")]
    public void DomainExceptionValidation_GetErrors_NoErrorsNoExceptionThrown()
    {
        // Arrange
        var hasErrors = false;
        var errorMessage = "An error occurred.";
        
        // Act
        Action action = () =>  DomainExceptionValidation.GetErrors(hasErrors, errorMessage);

        // Assert
        action.Should().NotThrow();
    }

    [Fact(DisplayName = "GetErrors - Errors Domain Exception Thrown")]
    public void DomainExceptionValidation_GetErrors_ErrorsDomainExceptionThrown()
    {
        // Arrange
        bool hasErrors = true;
        string errorMessage = "An error occurred.";

        // Act
        Action action = () => DomainExceptionValidation.GetErrors(hasErrors, errorMessage);

        // Assert
        action.Should().Throw<DomainExceptionValidation>().WithMessage("An error occurred.");
    }
}