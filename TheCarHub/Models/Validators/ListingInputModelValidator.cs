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

            // RepairJob details
            RuleFor(l => l.RepairJob)
                .SetValidator(new RepairJobInputModelValidator());
            
            // Listing details
            RuleFor(l => l.Title)
                .NotEmpty()
                .WithMessage("Listing must have a title.")
                .MaximumLength(75)
                .WithMessage("Listing title cannot exceed 75 characters");

            RuleFor(l => l.Description)
                .MaximumLength(5000)
                .WithMessage("Description cannot exceed 5000 characters.");

            RuleFor(l => l.PurchasePrice)
                .GreaterThan(0m)
                .WithMessage("Purchase price must be greater than $0.");

            RuleFor(l => l.PurchaseDate)
                .NotEmpty()
                .WithMessage("A purchase date must be selected.");
        }
    }
}