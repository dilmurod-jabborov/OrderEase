using OrderEase.Service.Services.Categories.Models;

namespace OrderEase.Service.Services.Categories;

public interface ICategoryService
{
    Task CreateAsync(CategoryCreateModels model);
    Task UpdateAsync(long id, CategoryUpdateModels model);
    Task DeleteAsync(long id);
    Task<CategoryViewModels> GetByIdAsync(long id);
    Task<List<CategoryViewModels>> GetAllAsync(string? search);
}
