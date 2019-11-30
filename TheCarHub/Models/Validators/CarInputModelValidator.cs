using System.Runtime.InteropServices;
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
                .Matches(new Regex("^(?=.*[0-9])(?=.*[A-z])[0-9A-z-]{17}$"))
                .WithMessage("Invalid VIN format.");

//            RuleFor(c => c.Year)
//                .NotNull();

            RuleFor(c => c.Make)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Car make must be at least 3 characters long.")
                .MaximumLength(100)
                .WithMessage("Car make cannot exceed 100 characters.");

            RuleFor(c => c.Model)
                .NotEmpty()
                .MinimumLength(3)
                .WithMessage("Car model must be at least 3 characters long.")
                .MaximumLength(100)
                .WithMessage("Car model cannot exceed 100 characters.");

            RuleFor(c => c.Trim)
                .MaximumLength(10)
                .WithMessage("Car trim cannot exceed 10 characters.");
        }
    }
}