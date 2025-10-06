using Microsoft.EntityFrameworkCore;
using OrderEase.DataAccess.Repositories;
using OrderEase.Domain.Entities;
using OrderEase.Service.Exceptions;
using OrderEase.Service.Services.Customers.Models;

namespace OrderEase.Service.Services.Customers;

public class CustomerService(
    IRepository<Customer> customerRepository) : ICustomerService
{
    public async Task CreateAsync(CustomerCreateModel model)
    {
        var existCustomer = customerRepository.SelectAllAsQueryable()
            .FirstOrDefault(c => c.Phone == model.Phone);

        if (existCustomer is not null)
            throw new AlreadyExistException("Previously registered with this phone!");

        var customer = await customerRepository.InsertAsync(
            new Customer
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                CreatedAt = DateTime.Now,
                Address = model.Address
            });
    }

    public async Task UpdateAsync(int id, CustomerUpdateModel model)
    {
        var existCustomer = await customerRepository.SelectAsync(id)
            ?? throw new NotFoundException("You are not registered!");

        existCustomer.FirstName = model.FirstName;
        existCustomer.LastName = model.LastName;
        existCustomer.Email = model.Email;
        existCustomer.Phone = model.Phone;
        existCustomer.Address = model.Address;

        await customerRepository.UpdateAsync(existCustomer);
    }

    public async Task DeleteAsync(int id)
    {
        var existCustomer = await customerRepository.SelectAsync(id)
            ?? throw new NotFoundException("You are not registered!");

        await customerRepository.DeleteAsync(existCustomer);
    }

    public async Task<CustomerViewModel> GetByIdAsync(int id)
    {
        var customer = await customerRepository.SelectAsync(id)
            ?? throw new NotFoundException("No such user information found!");

        return new CustomerViewModel
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            Phone = customer.Phone,
        };
    }

    public async Task<List<CustomerViewModel>> GetAllAsync(
        string? name = null,
        string? phone = null,
        string? email = null)
    {
        var customers = customerRepository.SelectAllAsQueryable()
            .Where(c => !c.IsDeleted);

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(phone) ||
            string.IsNullOrWhiteSpace(email))
        {
            name = name.ToLower();
            email = email.ToLower();

            customers = customers.Where(c =>
            c.FirstName.ToLower() == name ||
            c.LastName.ToLower() == name ||
            c.Email.ToLower() == email ||
            c.Phone == phone);
        }

        return await customers.Select(c =>
        new CustomerViewModel
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            Phone = c.Phone,
        })
            .ToListAsync();
    }

}