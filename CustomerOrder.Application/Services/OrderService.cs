using CustomerOrder.Application.Interfaces;
using CustomerOrder.Common.Constants;
using CustomerOrder.Common.Enums;
using CustomerOrder.DTO;
using CustomerOrder.Infrastructure;
using CustomerOrder.Infrastructure.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CustomerOrder.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly CustomerOrderContext _customerOrderContext;
        private readonly IDeliveryBinService _deliveryBinService;

        public OrderService(CustomerOrderContext customerOrderContext, IDeliveryBinService deliveryBinService)
        {
            _customerOrderContext = customerOrderContext;
            _deliveryBinService = deliveryBinService;
        }

        public async Task<OrderResponse> SaveOrder(OrderRequest orderRequest)
        {
            try
            {
                var requiredBinWidth = _deliveryBinService.CalulateBinWidth(orderRequest.Products);
                await _customerOrderContext.Orders.AddAsync(new Order
                {
                    Id = orderRequest.OrderId,
                    RequiredBinWidth = requiredBinWidth
                });

                ProductTypeEnum productTypeValue = 0;
                foreach (var product in orderRequest.Products)
                {
                    Enum.TryParse(product.ProductType, out productTypeValue);
                    await _customerOrderContext.OrderProducts.AddAsync(new OrderProduct
                    {
                        OrderId = orderRequest.OrderId,
                        ProductTypeId = (short)productTypeValue,
                        Quantity = product.Quantity
                    });
                }

                await _customerOrderContext.SaveChangesAsync();

                return new OrderResponse { RequiredBinWidth = requiredBinWidth };
            }
            catch (DbUpdateException ex)
                  when ((ex.InnerException as SqlException)?.Number == 2627)    //catch primary key violation
            {
                throw new Exception(ErrorConstants.DuplicateOrderIdMsg);
            }
        }

        public async Task<OrderDetailResponse> GetOrder(string orderId)
        {
            var orderResult = await _customerOrderContext.Orders.Where(x => x.Id == orderId)
                                   .Select(x => new OrderDetailResponse
                                   {
                                       RequiredBinWidth = x.RequiredBinWidth,
                                       Products = x.OrderProducts.Select(y => new ProductRequest
                                       {
                                           ProductType = y.ProductType.Name,
                                           Quantity = y.Quantity
                                       })
                                   }).FirstOrDefaultAsync();

            if (orderResult == null)
                throw new HttpRequestException(ErrorConstants.OrderNotFoundMsg, null, statusCode: HttpStatusCode.NotFound);

            return orderResult;
        }
    }
}