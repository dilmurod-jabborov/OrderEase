namespace OrderEase.Domain.Entities;

public class Product : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }

    public Category Category { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
