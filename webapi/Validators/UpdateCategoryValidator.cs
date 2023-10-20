using FluentValidation;
using webapi.Requests.Category;

namespace webapi.Validators
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
    {
        public UpdateCategoryValidator()
        {
            _ = this.RuleFor(r => r.Name).NotEmpty().WithMessage("Name was not supplied to create the category.");
            _ = this.RuleFor(r => r.Description).NotEmpty().WithMessage("Description was not supplied to create the category");
        }
    }
}
