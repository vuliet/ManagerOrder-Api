namespace ManagerOrder.Api.Models.Responses
{
    public class OrderResponse
    {
        public List<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        public decimal TotalPrice { get; set; }
    }

    public class ProductResponse
    {
        public int ProductId { get; set; }

        public required string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
