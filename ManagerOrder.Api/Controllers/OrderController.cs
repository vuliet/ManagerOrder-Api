using ManagerOrder.Api.Models.Requests;
using ManagerOrder.Api.Models.Responses;
using ManagerOrder.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ManagerOrder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        private readonly IOrderService _orderService;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<OrderResponse> CreateOrder(OrderRequest request)
        {
            return await _orderService.CreateOrder(request);
        }

        [HttpPost]
        [Route("payment")]
        public async Task<PaymentResponse> PaymentOrder(PaymentRequest request)
        {
            //todo
            _logger.LogInformation("OrderController -> PaymentOrder -> Payment success!!!");

            await Task.CompletedTask;

            return new PaymentResponse
            {
                IsSuccess = true,
                PaymentType = request.PaymentType,
                TableNumber = request.TableNumber,
                TotalAmount = request.TotalAmount
            };
        }
    }
}