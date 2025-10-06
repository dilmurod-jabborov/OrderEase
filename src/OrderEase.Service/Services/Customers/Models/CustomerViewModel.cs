﻿namespace OrderEase.Service.Services.Customers.Models;

public class CustomerViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public DateTime CreatedDate { get; set; }
}