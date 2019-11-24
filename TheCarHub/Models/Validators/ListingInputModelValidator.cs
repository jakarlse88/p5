using FluentValidation;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Models.Validators
{
    public class ListingInputModelValidator : AbstractValidator<ListingInputModel>
    {
        public ListingInputModelValidator()
        {
            // Car details
            RuleFor(l => l.Car)
                .SetValidator(new CarInputModelValidator());

            RuleFor(l => l.RepairJob)
                .SetValidator(new RepairJobInputModelValidator());
            
            // Repairjob details
            RuleFor(l => l.RepairJob.Description)
                .MaximumLength(100);

            // Listing details
            RuleFor(l => l.Title)
                .NotEmpty()
                .MaximumLength(75);

            RuleFor(l => l.Description)
                .MaximumLength(1000);

            RuleFor(l => l.PurchasePrice)
                .GreaterThan(0m);

            RuleFor(l => l.PurchaseDate)
                .NotEmpty();
        }
    }
}