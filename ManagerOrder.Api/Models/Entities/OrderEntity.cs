namespace ManagerOrder.Api.Models.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsPayment { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<OrderProductEntity> Products { get; set; } = new List<OrderProductEntity>();
    }
}
