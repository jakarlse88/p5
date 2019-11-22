using System.Text.RegularExpressions;
using FluentValidation;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Models.Validators
{
    public class CarInputModelValidator : AbstractValidator<CarInputModel>
    {
        public CarInputModelValidator()
        {
            RuleFor(c => c.VIN)
                .NotEmpty()
                .Matches(new Regex("^(?=.*[0-9])(?=.*[A-z])[0-9A-z-]{17}$"));

            RuleFor(c => c.Year)
                .NotNull();

            RuleFor(c => c.Make)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(c => c.Model)
                .NotEmpty()
                .MinimumLength(3)
                .MaximumLength(100);

            RuleFor(c => c.Trim)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(25);
        }
    }
}