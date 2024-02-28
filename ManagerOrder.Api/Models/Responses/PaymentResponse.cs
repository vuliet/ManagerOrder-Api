using ManagerOrder.Api.Models.Requests;

namespace ManagerOrder.Api.Models.Responses
{
    public class PaymentResponse : PaymentRequest
    {
        public bool IsSuccess { get; set; }
    }
}
