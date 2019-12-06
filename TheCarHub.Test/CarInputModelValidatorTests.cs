using Xunit;
using FluentValidation.TestHelper;
using TheCarHub.Models.Validators;

namespace TheCarHub.Test
{
    public class CarInputModelValidatorTests
    {
        private readonly CarInputModelValidator _validator;

        public CarInputModelValidatorTests()
        {
            _validator = new CarInputModelValidator();    
        }
        
        [Fact]
        public void TestRuleForVin()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(model => model.VIN, "alksjdlk12j303")
                .WithErrorMessage("Invalid VIN format.");
        }

        [Fact]
        public void TestRuleForMake()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(model => model.Make, "a")
                .WithErrorMessage("Car make must be at least 3 characters long.");
            
            _validator
                .ShouldHaveValidationErrorFor(model => model.Make, "qwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiop1")
                .WithErrorMessage("Car make cannot exceed 100 characters.");

        }

        [Fact]
        public void TestRuleForModel()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(model => model.Model, "a")
                .WithErrorMessage("Car model must be at least 3 characters long.");
            
            _validator
                .ShouldHaveValidationErrorFor(model => model.Model, "qwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiopqwertyuiop1")
                .WithErrorMessage("Car model cannot exceed 100 characters.");
        }

        
        [Fact]
        public void TestRuleForTrim()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(model => model.Trim, "12345678901")
                .WithErrorMessage("Car trim cannot exceed 10 characters.");
        }


    }
}