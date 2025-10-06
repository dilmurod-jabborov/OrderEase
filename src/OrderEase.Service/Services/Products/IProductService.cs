using OrderEase.Service.Services.Products.Models;

namespace OrderEase.Service.Services.Products;

public interface IProductService
{
    Task CreateAsync(ProductCreateModel model);
    Task UpdateAsync(long id, ProductUpdateModel model);
    Task DeleteAsync(long  id);  
    Task<ProductViewModel> GetByIdAsync(long id);
    Task<List<ProductViewModel>> GetAllAsync(string? name = null);
    Task<List<ProductViewModel>> GetAllByCategoryIdAsync(long categoryId);
    Task<List<ProductViewModel>> GetAllOutOffStockProductAsync();
}