using FluentValidation;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Models.Validators
{
    public class RepairJobInputModelValidator : AbstractValidator<RepairJobInputModel>
    {
        public RepairJobInputModelValidator()
        {
            RuleFor(rj => rj.Cost)
                .GreaterThan(-1);

            RuleFor(rj => rj.Description)
                .MaximumLength(100);
        }
    }
}