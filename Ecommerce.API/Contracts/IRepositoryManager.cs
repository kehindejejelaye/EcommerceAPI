﻿namespace Ecommerce.API.Contracts;

public interface IRepositoryManager
{
    Task SaveAsync();
    public ICategoryRepository Category { get; }
}