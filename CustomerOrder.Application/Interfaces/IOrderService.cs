using CustomerOrder.DTO;

namespace CustomerOrder.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> SaveOrder(OrderRequest orderRequest);
        Task<OrderDetailResponse> GetOrder(string orderId);
    }
}