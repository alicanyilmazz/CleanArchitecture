using FluentValidation;

namespace App.Application.Features.ProductFeatures.Commands.CreateProduct;
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.").NotNull().WithMessage("Product name is required.").MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        RuleFor(x => x.Description).MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        RuleFor(x => x.Quantity).InclusiveBetween(1, 100).WithMessage("Stock must be 1-100.");
    }
}
