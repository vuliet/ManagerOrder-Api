using System.ComponentModel.DataAnnotations.Schema;

namespace ManagerOrder.Api.Models.Entities
{
    public class OrderProductEntity
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsCancel { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderEntity? Order { get; set; }
    }
}
