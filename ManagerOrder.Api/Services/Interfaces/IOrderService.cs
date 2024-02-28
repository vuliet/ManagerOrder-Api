using ManagerOrder.Api.Models.Requests;
using ManagerOrder.Api.Models.Responses;

namespace ManagerOrder.Api.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrder(OrderRequest request);
    }
}
