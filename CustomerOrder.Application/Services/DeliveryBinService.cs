using CustomerOrder.Application.Interfaces;
using CustomerOrder.DTO;
using CustomerOrder.Infrastructure;

namespace CustomerOrder.Application.Services
{
    public class DeliveryBinService : IDeliveryBinService
    {
        private readonly CustomerOrderContext _customerOrderContext;

        public DeliveryBinService(CustomerOrderContext customerOrderContext)
        {
            _customerOrderContext = customerOrderContext;
        }

        public decimal CalulateBinWidth(IEnumerable<ProductRequest> products)
        {
            var productDetails = (from product in products
                                  join det in _customerOrderContext.ProductTypes on product.ProductType.ToString() equals det.Name
                                  select new
                                  {
                                      product.ProductType,
                                      product.Quantity,
                                      det.Width,
                                      det.Stack
                                  }).ToList();

            var totalWidth = decimal.Zero;
            var totalPlaceTaken = decimal.Zero;

            foreach (var product in productDetails)
            {
                totalPlaceTaken = (decimal)product.Quantity / product.Stack;
                if (totalPlaceTaken % 1 != 0)  // if result is not a whole number
                {
                    totalPlaceTaken = (int)totalPlaceTaken + 1;
                }

                totalWidth += totalPlaceTaken * product.Width;
            }
            return totalWidth;
        }
    }
}
