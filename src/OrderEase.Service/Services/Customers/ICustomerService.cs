using OrderEase.Service.Services.Customers.Models;

namespace OrderEase.Service.Services.Customers;

public interface ICustomerService
{
    Task CreateAsync(CustomerCreateModel model);
    Task UpdateAsync(int id, CustomerUpdateModel model);
    Task DeleteAsync(int id);
    Task<CustomerViewModel> GetByIdAsync(int id);
    Task<List<CustomerViewModel>> GetAllAsync(
        string? name = null,
        string? phone = null,
        string? email = null);
}
