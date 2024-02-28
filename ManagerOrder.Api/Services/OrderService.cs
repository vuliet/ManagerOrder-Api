using ManagerOrder.Api.Models.Entities;
using ManagerOrder.Api.Models.Requests;
using ManagerOrder.Api.Models.Responses;
using ManagerOrder.Api.Services.Interfaces;
using Newtonsoft.Json;

namespace ManagerOrder.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly List<ProductEntity> _listProducts;
        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
            _listProducts = FakeListProductEntity();
        }

        public async Task<OrderResponse> CreateOrder(OrderRequest request)
        {
            request = FakeOrderRequest(request);

            var result = new OrderResponse();

            if (!ValidationOrderRequest(request))
                return await Task.FromResult(result);

            MakeOrderHandler(request.ListOrderInfo, result);

            TotalPriceCalculator(result);

            return await Task.FromResult(result);
        }

        #region Private Methods
        private static bool ValidationOrderRequest(OrderRequest orderRequest)
        {
            if (orderRequest is null || !orderRequest.ListOrderInfo.Any())
                return false;

            orderRequest.ListOrderInfo = orderRequest.ListOrderInfo.Where(x => x.Quantity > 0).ToList();

            return true;
        }

        private OrderRequest FakeOrderRequest(OrderRequest request)
        {
            bool isFake = true;
            if (isFake)
            {
                var dummyOrders = new List<OrderInfo>();
                var random = new Random();

                for (int i = 1; i <= 10; i++)
                {
                    int i_fake = Fake_Integer(i);

                    dummyOrders.Add(new OrderInfo
                    {
                        ProductId = i_fake,
                        ProductName = $"Test{i_fake}",
                        Quantity = random.Next(0, 50),
                    });
                }

                var result = new OrderRequest
                {
                    TableNumber = 1,
                    ListOrderInfo = dummyOrders
                };

                _logger.LogInformation($"OrderController -> CreateOrder: Fake request for test " +
                    $"-> request={JsonConvert.SerializeObject(result)}");

                return result;
            }

            return request;
        }

        private static List<ProductEntity> FakeListProductEntity()
        {
            var result = new List<ProductEntity>();

            for (int i = 1; i <= 10; i++)
            {
                result.Add(new ProductEntity
                {
                    Id = i,
                    Name = $"Product{i}",
                    Price = i * 1000
                });
            }

            return result;
        }

        private static int Fake_Integer(int value)
        {
            return value switch
            {
                2 or 7 => 1,
                5 or 8 => 2,
                _ => value,
            };
        }

        private static void AddProductIntoResponse(
            OrderResponse orderResponse,
            decimal totalPrice,
            int productId,
            int quantity,
            string productName,
            decimal unitPrice)
        {
            if (orderResponse is null || orderResponse.Products is null)
                return;

            orderResponse.Products.Add(new ProductResponse
            {
                TotalPrice = totalPrice,
                ProductId = productId,
                Quantity = quantity,
                ProductName = productName,
                UnitPrice = unitPrice
            });
        }

        private void MakeOrderHandler(
            List<OrderInfo> listOrderInfo,
            OrderResponse orderResponse)
        {
            var productCombineQuantity = listOrderInfo
                .Where(x => x.Quantity > 0)
                .GroupBy(p => p.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    Quantity = group.Sum(p => p.Quantity)
                })
                .ToList();

            foreach (var orderInfo in listOrderInfo)
            {
                var orderEntityByIdCombine = productCombineQuantity
                    .FirstOrDefault(x => x.ProductId == orderInfo.ProductId);

                if (orderEntityByIdCombine is null)
                    continue;

                if (orderResponse.Products.Any(x => x.ProductId == orderEntityByIdCombine.ProductId))
                    continue;

                var productInfo = _listProducts.FirstOrDefault(x => x.Id == orderInfo.ProductId);

                decimal totalPrice = productInfo is null ? 0 : productInfo.Price * orderEntityByIdCombine.Quantity;
                decimal unitPrice = productInfo is null ? 0 : productInfo.Price;

                AddProductIntoResponse(
                    orderResponse,
                    totalPrice,
                    orderInfo.ProductId,
                    orderEntityByIdCombine.Quantity,
                    orderInfo.ProductName,
                    unitPrice);
            }
        }

        private static void TotalPriceCalculator(OrderResponse orderResponse)
        {
            if (orderResponse is null)
                return;

            orderResponse.TotalPrice = orderResponse.Products.Sum(x => x.TotalPrice);
        }
        #endregion
    }
}
