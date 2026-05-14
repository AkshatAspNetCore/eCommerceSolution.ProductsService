using BusinessLogicLayer.DTO;
using FluentValidation;

namespace BusinessLogicLayer.Validators;

public class ProductUpdateRequestValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateRequestValidator()
    {
        //ProductID
        RuleFor(temp => temp.ProductID)
            .NotEmpty()
            .WithMessage("Product ID should not be empty.");

        //ProductName
        RuleFor(temp => temp.ProductName)
            .NotEmpty()
            .WithMessage("Product name should not be empty.");

        //Category
        RuleFor(temp => temp.Category)
            .IsInEnum()
            .WithMessage("Invalid category option.");

        //UnitPrice
        RuleFor(temp => temp.UnitPrice)
            .InclusiveBetween(0, 500)
            .WithMessage("Unit price should be greater than 0 and less than 500.");

        //Quantity in stock
        RuleFor(temp => temp.QuantityInStock)
            .InclusiveBetween(0, int.MaxValue)
            .WithMessage($"Quantity in stock should be between 0 to {int.MaxValue}.");
    }
}
