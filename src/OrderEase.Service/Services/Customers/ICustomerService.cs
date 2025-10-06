using OrderEase.Service.Services.Customers.Models;

namespace OrderEase.Service.Services.Customers;

public interface ICustomerService
{
    Task CreateAsync(CustomerCreateModel model);
    Task UpdateAsync(long id, CustomerUpdateModel model);
    Task DeleteAsync(long id);
    Task<CustomerViewModel> GetByIdAsync(long id);
    Task<List<CustomerViewModel>> GetAllAsync(
        string? name = null,
        string? phone = null,
        string? email = null);
}
