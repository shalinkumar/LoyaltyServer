using FluentValidation;
using webapi.Requests.Category;

namespace webapi.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryValidator()
        {
            _ = this.RuleFor(r => r.Name).NotEmpty().WithMessage("Name was not supplied to create the category.");
            _ = this.RuleFor(r => r.Description).NotEmpty().WithMessage("Description was not supplied to create the category");
        }
    }
}
