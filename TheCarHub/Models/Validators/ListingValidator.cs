using FluentValidation;
using TheCarHub.Models.ViewModels;

namespace TheCarHub.Models.Validators
{
    public class ListingValidator : AbstractValidator<ListingViewModel>
    {
        public ListingValidator()
        {
            RuleFor(l => l.Title)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(l => l.Description)
                .NotEmpty()
                .MaximumLength(1000);
            
        }
    }
}