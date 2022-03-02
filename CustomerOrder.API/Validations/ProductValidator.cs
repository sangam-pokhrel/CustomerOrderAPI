using CustomerOrder.Common.Constants;
using CustomerOrder.Common.Enums;
using CustomerOrder.DTO;
using FluentValidation;

namespace CustomerOrder.API.Validations
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductType).NotEmpty();
            RuleFor(x => x.ProductType).IsEnumName(typeof(ProductTypeEnum)).WithMessage(ErrorConstants.IncorrectProductTypeMsg);
            RuleFor(x => x.Quantity).NotEmpty();
        }
    }
}