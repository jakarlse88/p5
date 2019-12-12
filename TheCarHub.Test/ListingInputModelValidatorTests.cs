using System;
using TheCarHub.Models.Validators;
using Xunit;
using FluentValidation.TestHelper;

namespace TheCarHub.Test
{
    public class ListingInputModelValidatorTests
    {
        private readonly ListingInputModelValidator _validator;

        public ListingInputModelValidatorTests()
        {
            _validator = new ListingInputModelValidator();
        }

        [Fact]
        public void TestRuleForCar()
        {
            // Assert
            _validator
                .ShouldHaveChildValidator(model => model.Car, typeof(CarInputModelValidator));
        }

        [Fact]
        public void TestRuleForRepairJob()
        {
            // Assert
            _validator
                .ShouldHaveChildValidator(model => model.RepairJob, typeof(RepairJobInputModelValidator));
        }

        [Fact]
        public void TestRuleForTitle()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(model => model.Title, null as string)
                .WithErrorMessage("Listing must have a title.");

            _validator
                .ShouldHaveValidationErrorFor(model => model.Title,
                    "12345679801234567980123456798012345679801234567980123456798012345679801234567980")
                .WithErrorMessage("Listing title cannot exceed 75 characters");
        }

        [Fact]
        public void TestRuleForDescription()
        {
            // Arrange
            var testString = "";

            for (int i = 0; i <= 1001; i++)
            {
                testString += 'a';
            }
            
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(model => model.Description, testString)
                .WithErrorMessage("Description cannot exceed 1000 characters.");
        }

        [Fact]
        public void TestRuleForPurchasePrice()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(model => model.PurchasePrice, -1)
                .WithErrorMessage("Purchase price must be greater than $0.");
        }

        [Fact]
        public void TestRuleForPurchaseDate()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(model => model.PurchaseDate, new DateTime())
                .WithErrorMessage("A purchase date must be selected.");
        }
    }
}