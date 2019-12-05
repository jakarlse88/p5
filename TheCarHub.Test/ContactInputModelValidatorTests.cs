using FluentValidation.TestHelper;
using TheCarHub.Models.Validators;
using Xunit;

namespace TheCarHub.Test
{
    public class ContactInputModelValidatorTests
    {
        private readonly ContactInputModelValidator _validator;

        public ContactInputModelValidatorTests()
        {
            _validator = new ContactInputModelValidator();
        }

        [Fact]
        public void TestRuleForName()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(m => m.SenderName, null as string)
                .WithErrorMessage("Name is required.");

            _validator
                .ShouldHaveValidationErrorFor(m => m.SenderName,
                    "asdfghjkløasdfghjkløasdfghjkløasdfghjkløasdfghjklø1")
                .WithErrorMessage("Name cannot exceed 50 characters");

//            _validator
//                .ShouldHaveValidationErrorFor(m => m.SenderName, "Jon k@arlsen")
//                .WithErrorMessage("Illegal character in name.");
        }

        [Fact]
        public void TestRuleForSenderEmail()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(m => m.SenderEmail, null as string)
                .WithErrorMessage("Email address is required.");

            _validator
                .ShouldHaveValidationErrorFor(m => m.SenderEmail, "jon")
                .WithErrorMessage("Email address is invalid.");
            
            _validator
                .ShouldHaveValidationErrorFor(m => m.SenderEmail, "jon@jon")
                .WithErrorMessage("Email address is invalid.");
        }

        [Fact]
        public void TestRuleForSenderPhoneNumber()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(m => m.SenderPhoneNumber, "111-222-33333")
                .WithErrorMessage("Phone number (if supplied) must be in US format and include area code.");
            
            _validator
                .ShouldHaveValidationErrorFor(m => m.SenderPhoneNumber, "11122233333")
                .WithErrorMessage("Phone number (if supplied) must be in US format and include area code.");
            
            _validator
                .ShouldNotHaveValidationErrorFor(m => m.SenderPhoneNumber, "111-222-3333");

            _validator
                .ShouldNotHaveValidationErrorFor(m => m.SenderPhoneNumber, "111.222.3333");

            _validator
                .ShouldNotHaveValidationErrorFor(m => m.SenderPhoneNumber, "(111) 222-3333");
        }

        [Fact]
        public void TestRuleForSubject()
        {
            _validator
                .ShouldHaveValidationErrorFor(m => m.Subject, null as string)
                .WithErrorMessage("Subject is required.");

            _validator
                .ShouldHaveValidationErrorFor(m => m.Subject, "asdfghjkløasdfghjkløasdfghjkløasdfghjkløasdfghjkløæ")
                .WithErrorMessage("Subject cannot exceed 50 characters");
        }

        [Fact]
        public void TestRuleForMessage()
        {
            // Assert
            _validator
                .ShouldHaveValidationErrorFor(m => m.Message, null as string)
                .WithErrorMessage("Message is required.");

            string testString = "";

            for (int i = 0; i <= 10001; i++)
            {
                testString += "a";
            }

            _validator
                .ShouldHaveValidationErrorFor(m => m.Message, testString)
                .WithErrorMessage("Message body cannot exceed 1000 characters.");
        }

    }
}