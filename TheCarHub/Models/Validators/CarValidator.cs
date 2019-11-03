using System.Text.RegularExpressions;
using FluentValidation;
using TheCarHub.Models.Entities;
using TheCarHub.Models.ViewModels;

namespace TheCarHub.Models.Validators
{
    public class CarValidator : AbstractValidator<CarViewModel>
    {
        public CarValidator()
        {
            RuleFor(c => c.VIN)
                .NotEmpty()
                .Matches(new Regex("[A-HJ-NPR-Z0-9]{17}"));

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