using FluentValidation.TestHelper;
using TheCarHub.Models.Validators;
using Xunit;

namespace TheCarHub.Test
{
    public class RepairJobInputModelValidatorTests
    {
        private readonly RepairJobInputModelValidator _validator;

        public RepairJobInputModelValidatorTests()
        {
            _validator = new RepairJobInputModelValidator();
        }

        [Fact]
        public void TestRuleForCost()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(
                    model => model.Cost, 
                    -1)
                .WithErrorMessage("Cost must be 0 or higher.");
        }

        [Fact]
        public void TestRuleForDescription()
        {
            // Arrange
            _validator
                .ShouldHaveValidationErrorFor(
                    model => model.Description,
                    "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901")
                .WithErrorMessage("Description cannot exceed 100 characters.");
        }


    }
}