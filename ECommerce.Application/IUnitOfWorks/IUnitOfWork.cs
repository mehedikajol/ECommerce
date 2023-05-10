﻿using ECommerce.Application.IRepositories;

namespace ECommerce.Application.IUnitOfWorks;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    ISubCategoryRepository SubCategories { get; }
    IProductRepository Products { get; }
    IStockRepository Stocks { get; }
    IUserProfileRepository UserProfiles { get; }

    Task CompleteAsync();
}
