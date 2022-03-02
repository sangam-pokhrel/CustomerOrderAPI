using CustomerOrder.DTO;
using FluentValidation;

namespace CustomerOrder.API.Validations
{
    public class OrderValidator : AbstractValidator<OrderRequest>
    {
        public OrderValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
            RuleFor(x => x.OrderId).MaximumLength(50);
            RuleForEach(x => x.Products).NotEmpty().SetValidator(new ProductValidator());
        }
    }
}