using System.Text.RegularExpressions;
using FluentValidation;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Models.Validators
{
    public class ContactInputModelValidator : AbstractValidator<ContactInputModel>
    {
        public ContactInputModelValidator()
        {
            RuleFor(m => m.SenderName)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(50)
                .WithMessage("Name cannot exceed 50 characters");
//                .Matches(new Regex(@"[\p{L} ]+$"))
//                .WithMessage("Illegal character in name.");

            RuleFor(m => m.SenderEmail)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("Email address is invalid.");

            RuleFor(m => m.SenderPhoneNumber)
                .Matches(new Regex(@"^\D?(\d{3})\D?\D?(\d{3})\D?(\d{4})$")) // http://regexlib.com/REDetails.aspx?regexp_id=45
                .WithMessage("Phone number (if supplied) must be in US format and include area code.");

            RuleFor(m => m.Subject)
                .NotEmpty()
                .WithMessage("Subject is required.")
                .MaximumLength(50)
                .WithMessage("Subject cannot exceed 50 characters");

            RuleFor(m => m.Message)
                .NotEmpty()
                .WithMessage("Message is required.")
                .MaximumLength(1000)
                .WithMessage("Message body cannot exceed 1000 characters.");
        }
    }
}