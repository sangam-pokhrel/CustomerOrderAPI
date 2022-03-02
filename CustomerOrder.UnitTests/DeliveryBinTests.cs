using CustomerOrder.Application.Interfaces;
using CustomerOrder.Application.Services;
using CustomerOrder.Common.Enums;
using CustomerOrder.DTO;
using FluentAssertions;
using Xunit;

namespace CustomerOrder.UnitTests
{
    public class DeliveryBinTests : UnitTestFixture
    {
        private IDeliveryBinService _deliveryBinService;

        [Theory]
        [MemberData(nameof(CalculationTestData))]
        public async Task CalulateBinWidth_Should_Return_Correct_Bin_Width(List<ProductRequest> productRequests, decimal expectedResult)
        {
            _deliveryBinService = new DeliveryBinService(await GetDbContext());
            var result = _deliveryBinService.CalulateBinWidth(productRequests);
            result.Should().Be(expectedResult);
        }

        #region Setup Test Data

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

        #endregion Setup Test Data
    }
}