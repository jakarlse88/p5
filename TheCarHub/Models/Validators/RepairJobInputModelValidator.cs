using FluentValidation;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Models.Validators
{
    public class RepairJobInputModelValidator : AbstractValidator<RepairJobInputModel>
    {
        public RepairJobInputModelValidator()
        {
            RuleFor(rj => rj.Cost)
                .GreaterThan(-1)
                .WithMessage("Cost must be 0 or higher.");

            RuleFor(rj => rj.Description)
                .MaximumLength(100)
                .WithMessage("Description cannot exceed 100 characters.");
        }
    }
}