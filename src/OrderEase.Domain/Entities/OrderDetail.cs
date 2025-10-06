namespace OrderEase.Domain.Entities;

public class OrderDetail : Auditable
{
    public long OrderId { get; set; }
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal LineTotal {  get; set; }

    public Product Product { get; set; }
    public Order Order { get; set; }
}
