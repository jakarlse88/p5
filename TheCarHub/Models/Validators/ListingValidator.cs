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
                .MinimumLength(5)
                .MaximumLength(255);
        }
    }
}