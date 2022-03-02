using CustomerOrder.Common.Enums;
using CustomerOrder.DTO;
using CustomerOrder.Infrastructure;
using CustomerOrder.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace CustomerOrder.UnitTests
{
    public class UnitTestFixture
    {
        protected readonly string orderId;
        protected readonly ProductTypeEnum defaultProduct;
        protected readonly decimal defaultRequiredBinWidth;
        protected readonly ProductRequest product;
        protected readonly Order order;

        public UnitTestFixture()
        {
            //setup default order
            orderId = "O4453";
            defaultProduct = ProductTypeEnum.Photobook;

            product = new ProductRequest
            {
                ProductType = defaultProduct.ToString(),
                Quantity = 1
            };

            order = new()
            {
                Id = orderId,
                RequiredBinWidth = defaultRequiredBinWidth,
                OrderProducts = new List<OrderProduct> {
                    new OrderProduct
                    {
                        OrderId = orderId,
                        ProductTypeId = (short)defaultProduct,
                        Quantity = product.Quantity
                    }
                }
            };
        }

        protected async Task<CustomerOrderContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<CustomerOrderContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new CustomerOrderContext(options);
            dbContext.Database.EnsureCreated();

            if (!dbContext.Orders.Any())
                dbContext.Orders.Add(order);    //Add default Order

            await dbContext.SaveChangesAsync();
            return dbContext;
        }
    }
}