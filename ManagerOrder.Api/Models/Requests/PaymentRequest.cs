namespace ManagerOrder.Api.Models.Requests
{
    public class PaymentRequest
    {
        public int TableNumber { get; set; }

        public decimal TotalAmount { get; set; }

        public PaymentType PaymentType { get; set; }
    }

    public enum PaymentType
    {
        CASH = 0,
        ATM = 1,
        VISA = 2
    }
}
