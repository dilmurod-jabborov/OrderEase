using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Repositories;
using OrderEase.Domain.Entities;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Categories.Models;

namespace OrderEase.Service.Services.Categories;

public class CategoryService(
    IRepository<Category> categoryRepository)
    : ICategoryService
{
    public async Task CreateAsync(CategoryCreateModels model)
    {
        var existCategory = categoryRepository.SelectAllAsQueryable()
            .FirstOrDefault(c => c.Name == model.Name);

        if (existCategory is not null)
            throw new AlreadyExistException("This category already exist!");

        await categoryRepository.InsertAsync(new Category { Name = model.Name });
    }

    public async Task UpdateAsync(long id, CategoryUpdateModels model)
    {
        var existCategory = await categoryRepository.SelectAsync(id)
            ?? throw new NotFoundException("This category is not found!");

        if (existCategory.Name == model.Name)
            throw new Exception("This name is the same as the previous one!");

        existCategory.Name = model.Name;

        await categoryRepository.UpdateAsync(existCategory);
    }

    public async Task DeleteAsync(long id)
    {
        var existCategory = await categoryRepository.SelectAsync(id)
            ?? throw new NotFoundException("This category is not found!");

        await categoryRepository.DeleteAsync(existCategory);
    }

    public async Task<CategoryViewModels> GetByIdAsync(long id)
    {
        var category = await categoryRepository.SelectAsync(id);

        return new CategoryViewModels
        {
            Id = id,
            Name = category.Name,
        };
    }

    public async Task<List<CategoryViewModels>> GetAllAsync(string? search)
    {
        var categories = categoryRepository
            .SelectAllAsQueryable().Where(c => !c.IsDeleted);

        if (string.IsNullOrEmpty(search))
        {
            search = search.ToLower();

            categories.Where(c => c.Name.ToLower() == search);
        }

        return await categories.Select(c => new CategoryViewModels
        {
            Id = c.Id,
            Name = c.Name,
        }).ToListAsync();
    }
}