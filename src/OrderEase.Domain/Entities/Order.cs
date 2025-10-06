using OrderEase.Domain.Enums;

namespace OrderEase.Domain.Entities;

public class Order : Auditable
{
    public long CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotallAmount { get; set; }
    public Status Status { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }
    public Customer Customer { get; set; }
}
