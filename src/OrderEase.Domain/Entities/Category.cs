﻿namespace OrderEase.Domain.Entities;

public class Category : Auditable
{
    public string Name { get; set; }

    public ICollection<Product> Products { get; set; }
}
