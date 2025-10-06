using OrderEase.Service.Services.Categories.Models;

namespace OrderEase.Service.Services.Categories;

public interface ICategoryService
{
    Task CreateAsync(CategoryCreateModels model);
    Task UpdateAsync(int id, CategoryUpdateModels model);
    Task DeleteAsync(int id);
    Task<CategoryViewModels> GetByIdAsync(int id);
    Task<List<CategoryViewModels>> GetAllAsync(string? search);
}
