namespace OrderEase.Service.Services.Products.Models;

public class ProductViewModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }
}
