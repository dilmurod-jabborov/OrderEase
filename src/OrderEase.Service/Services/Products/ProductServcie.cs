using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Repositories;
using OrderEase.Domain.Entities;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Products.Models;

namespace OrderEase.Service.Services.Products;

public class ProductServcie(
    IRepository<Category> categoryRepository,
    IRepository<Product> productRepository)
    : IProductService
{
    public async Task CreateAsync(ProductCreateModel model)
    {
        _ = await categoryRepository.SelectAsync(model.CategoryId)
            ?? throw new NotFoundException("This category is not found!");

        var existProduct = productRepository.SelectAllAsQueryable()
            .Where(p => 
            p.Name == model.Name &&
            p.CategoryId == model.CategoryId);

        if (existProduct is not null)
            throw new AlreadyExistException("This product is available!");

        var product = await productRepository.InsertAsync(new Product
        {
            Name = model.Name,
            CategoryId = model.CategoryId,
            Description = model.Description,
            Stock = model.Stock,
            UnitPrice = model.UnitPrice,
        });
    }

    public async Task UpdateAsync(long id, ProductUpdateModel model)
    {
        var existProduct = await productRepository.SelectAsync(id)
            ?? throw new NotSupportedException("This product is not found!");

        existProduct.Name = model.Name;
        existProduct.Description = model.Description;
        existProduct.Stock = model.Stock;
        existProduct.UnitPrice = model.UnitPrice;

        await productRepository.UpdateAsync(existProduct);
    }

    public async Task DeleteAsync(long id)
    {
        var existProduct = await productRepository.SelectAsync(id)
            ?? throw new NotFoundException("This product is not found!");

        await productRepository.DeleteAsync(existProduct);
    }

    public async Task<ProductViewModel> GetByIdAsync(long id)
    {
        var product = await productRepository.SelectAsync(id)
            ?? throw new NotFoundException("This product is not found!");

        return new ProductViewModel
        {
            Id = id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Stock = product.Stock,
            UnitPrice = product.UnitPrice,
        };
    }

    public async Task<List<ProductViewModel>> GetAllAsync(string? name = null)
    {
        var productQuery = productRepository.SelectAllAsQueryable()
            .Where(p => !p.IsDeleted);

        if (string.IsNullOrEmpty(name))
            productQuery = productQuery
                .Where(p => p.Name.ToLower() == name.ToLower());

        return await productQuery.Select(p => new ProductViewModel
        {
            Id=p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            Description = p.Description,
            Stock = p.Stock,
            UnitPrice= p.UnitPrice,
        })
            .ToListAsync();
    }

    public async Task<List<ProductViewModel>> GetAllByCategoryIdAsync(long categoryId)
    {
        var query = productRepository.SelectAllAsQueryable()
            .Where(p => !p.IsDeleted);

        if(categoryId != null)
            query = query.Where(p => p.CategoryId == categoryId);

        return await query.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            CategoryId = p.CategoryId,
            Description = p.Description,
            Stock = p.Stock,
            UnitPrice = p.UnitPrice,
        })
            .ToListAsync();
    }

    public async Task<List<ProductViewModel>> GetAllOutOffStockProductAsync()
    {
        return await productRepository.SelectAllAsQueryable()
            .Where(p => !p.IsDeleted && p.Stock == 0)
            .Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Stock = p.Stock,
                UnitPrice = p.UnitPrice,
                Description = p.Description,
                CategoryId= p.CategoryId,
            }).ToListAsync();
    }
}
