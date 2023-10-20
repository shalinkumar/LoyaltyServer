using FluentValidation;
using webapi.Requests.Product;

namespace webapi.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductValidator()
        {
            _ = this.RuleFor(r => r.Name).NotEmpty().WithMessage("Name was not supplied to create the category.");
            _ = this.RuleFor(r => r.UserDescription).NotEmpty().WithMessage("Description was not supplied to create the category");
            _ = this.RuleFor(r => r.Price).NotEmpty().WithMessage("Price was not supplied to create the category");
            _ = this.RuleFor(r => r.Category).NotEmpty().WithMessage("Category was not supplied to create the category");
            _ = this.RuleFor(r => r.Color).NotEmpty().WithMessage("Color was not supplied to create the category");
        }

    }
}
