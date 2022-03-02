using CustomerOrder.DTO;

namespace CustomerOrder.Application.Interfaces
{
    public interface IDeliveryBinService
    {
        decimal CalulateBinWidth(IEnumerable<ProductRequest> products);
    }
}