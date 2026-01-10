using BusinessLogicLayer.DTO;
using FluentValidation;

namespace eCommerce.BusinessLogicLayer.Validators
{
    public class ProductUpdateRquestValidator :AbstractValidator<ProductUpdtaeRequest>
    {
        public ProductUpdateRquestValidator() 
        {
            //ProductID
            RuleFor(temp => temp.ProductId)
                .NotEmpty().WithMessage("ProductId Can't be blank");

            // ProductName
            RuleFor(temp => temp.ProductName)
                .NotEmpty().WithMessage("Product Name Can't be blank");

            //Category
            RuleFor(temp => temp.Category)
                .IsInEnum().WithMessage("Category Can't be blank");

            //UnitPrice
            RuleFor(temp => temp.UnitPrice)
                .InclusiveBetween(0, double.MaxValue).WithMessage($"UnitPrice should between 0 to {double.MaxValue}");

            //QuantityInStock
            RuleFor(temp => temp.QuantityInStock)
                .InclusiveBetween(0, int.MaxValue).WithMessage($"QuantityInStock should between 0 to {int.MaxValue}");

        }
    }
}
