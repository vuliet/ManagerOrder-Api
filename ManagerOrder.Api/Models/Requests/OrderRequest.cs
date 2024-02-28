namespace ManagerOrder.Api.Models.Requests
{
    public class OrderRequest
    {
        public int TableNumber { get; set; }

        public List<OrderInfo> ListOrderInfo { get; set; } = new List<OrderInfo>();

        public string Description { get; set; } = "";
    }

    public class OrderInfo
    {
        public required string ProductName { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}
