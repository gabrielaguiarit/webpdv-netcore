using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebPDV.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<OrderProduct> OrderItems { get; set; }

        public decimal CalculateTotal => OrderItems != null && OrderItems.Any()  ? OrderItems.Select(x => x.SubTotal).Sum() : 0;
    }
}
