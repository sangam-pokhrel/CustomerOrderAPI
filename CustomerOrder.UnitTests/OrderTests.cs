using CustomerOrder.Application.Interfaces;
using CustomerOrder.Application.Services;
using CustomerOrder.Common.Constants;
using CustomerOrder.Common.Enums;
using CustomerOrder.DTO;
using FluentAssertions;
using Xunit;
using Moq;

namespace CustomerOrder.UnitTests
{
    public class OrderTests : UnitTestFixture
    {
        private IOrderService _orderService;
        private readonly Mock<IDeliveryBinService> _deliveryBinServiceMock;

        public OrderTests()
        {
            _deliveryBinServiceMock = new Mock<IDeliveryBinService>();
        }

        #region Tests

        [Fact]
        public async Task GetOrder_Should_Return_Order()
        {
            _orderService = new OrderService(await GetDbContext(), _deliveryBinServiceMock.Object);
            var orderResult = await _orderService.GetOrder(orderId);
            orderResult.RequiredBinWidth.Should().Be(order.RequiredBinWidth);
            orderResult.Products.Should().ContainEquivalentOf(product);
        }

        [Fact]
        public async Task GetOrder_With_IncorrectId_Should_Throw_Http_Exception_With_Not_Found_Msg()
        {
            var incorrectOrderId = "O12345";
            _orderService = new OrderService(await GetDbContext(), _deliveryBinServiceMock.Object);
            var orderRequest = async () => await _orderService.GetOrder(incorrectOrderId);
            await orderRequest.Should().ThrowAsync<HttpRequestException>().WithMessage(ErrorConstants.OrderNotFoundMsg);
        }

        [Fact]
        public async Task SaveOrder_Should_Save_Order()
        {
            var products = new List<ProductRequest> {
                    product1,
                    product2
                };

            var newOrder = new OrderRequest
            {
                OrderId = newOrderId,
                Products = products
            };

            _deliveryBinServiceMock.Setup(x => x.CalulateBinWidth(products)).Returns(newMinBinWidth).Verifiable();
            _orderService = new OrderService(await GetDbContext(), _deliveryBinServiceMock.Object);
            var result = await _orderService.SaveOrder(newOrder);
            result.RequiredBinWidth.Should().Be(newMinBinWidth);
            _deliveryBinServiceMock.Verify();

            //check if data was saved properly
            var getResult = await _orderService.GetOrder(newOrderId);
            getResult.RequiredBinWidth.Should().Be(newMinBinWidth);
            getResult.Products.Should().ContainEquivalentOf(product1);
            getResult.Products.Should().ContainEquivalentOf(product2);
        }

        [Fact]
        public async Task SaveOrder_With_ExistingId_Should_Throw_Exception()
        {
            var newOrder = new OrderRequest
            {
                OrderId = orderId, //existing orderId
                Products = new List<ProductRequest> {
                    product1,
                    product2
                }
            };

            _orderService = new OrderService(await GetDbContext(), _deliveryBinServiceMock.Object);
            var orderRequest = async () => await _orderService.SaveOrder(newOrder);
            await orderRequest.Should().ThrowAsync<Exception>();
        }

        #endregion Tests

        #region Setup Private Test Data

        private static readonly string newOrderId = "O4353435";
        private static readonly decimal newMinBinWidth = 156;

        private static readonly ProductRequest product1 = new ProductRequest
        {
            ProductType = ProductTypeEnum.Photobook.ToString(),
            Quantity = 4
        };

        private static readonly ProductRequest product2 = new ProductRequest
        {
            ProductType = ProductTypeEnum.Canvas.ToString(),
            Quantity = 5
        };

        public static IEnumerable<object[]> CalculationTestData =>
          new List<object[]>
          {
                new object[]
                {
                    new List<ProductRequest>
                    {
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Photobook.ToString(),
                            Quantity = 1
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Canvas.ToString(),
                            Quantity = 2
                        }
                    },
                    51
                },
                new object[]
                {
                   new List<ProductRequest>
                   {
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Photobook.ToString(),
                            Quantity = 1
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Calendar.ToString(),
                            Quantity = 2
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Mug.ToString(),
                            Quantity = 4
                        }
                   },
                   133
                },
                new object[]
                {
                   new List<ProductRequest>
                   {
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Photobook.ToString(),
                            Quantity = 1
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Canvas.ToString(),
                            Quantity = 2
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Mug.ToString(),
                            Quantity = 5
                        }
                   },
                   239
                },
                new object[]
                {
                   new List<ProductRequest>
                   {
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Mug.ToString(),
                            Quantity = 7
                        }
                   },
                   188
                },
                new object[]
                {
                   new List<ProductRequest>
                   {
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Photobook.ToString(),
                            Quantity = 1
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Calendar.ToString(),
                            Quantity = 2
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Cards.ToString(),
                            Quantity = 1
                        }
                   },
                   43.70
                },
                new object[]
                {
                   new List<ProductRequest>
                   {
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Photobook.ToString(),
                            Quantity = 3
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Calendar.ToString(),
                            Quantity = 4
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Canvas.ToString(),
                            Quantity = 1
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Cards.ToString(),
                            Quantity = 2
                        },
                        new ProductRequest
                        {
                            ProductType = ProductTypeEnum.Mug.ToString(),
                            Quantity = 7
                        }
                   },
                   310.40
                }
          };

        #endregion Setup Private Test Data
    }
}