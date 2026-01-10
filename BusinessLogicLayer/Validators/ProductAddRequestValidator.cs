using BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.BusinessLogicLayer.Validators
{
    public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
    {
        public ProductAddRequestValidator() 
        {
            // ProductName
            RuleFor(temp => temp.Productname)
                .NotEmpty().WithMessage("Product Name Can't be blank");

            //Category
            RuleFor(temp => temp.Category)
                .IsInEnum().WithMessage("Category Can't be blank");

            //Unit Price
            RuleFor(temp => temp.UnitPrice)
                .InclusiveBetween(0, double.MaxValue).WithMessage($"Unit price should between 0 to {double.MaxValue}");

            //QuantityInStock
            RuleFor(temp => temp.QuantityInStock)
                .InclusiveBetween(0, int.MaxValue).WithMessage($"Quantity in stock should between 0 to {int.MaxValue}");
        }
    }
}
