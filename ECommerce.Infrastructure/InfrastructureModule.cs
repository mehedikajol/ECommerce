﻿using Autofac;
using ECommerce.Application.IRepositories;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using ECommerce.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ECommerce.Infrastructure;

public class InfrastructureModule : Module
{

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AppDbContext>().As<IdentityDbContext>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

        // Repositories
        builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SubCategoryRepository>().As<ISubCategoryRepository>().InstancePerLifetimeScope();
        builder.RegisterType<ProductRepository>().As<IProductRepository>().InstancePerLifetimeScope();
        builder.RegisterType<StockRepository>().As<IStockRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UserProfileRepository>().As<IUserProfileRepository>().InstancePerLifetimeScope();
        builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();

        // Repository services
        builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
        builder.RegisterType<SubCategoryService>().As<ISubCategoryService>().InstancePerLifetimeScope();
        builder.RegisterType<ProductService>().As<IProductService>().InstancePerLifetimeScope();
        builder.RegisterType<StockService>().As<IStockService>().InstancePerLifetimeScope();
        builder.RegisterType<UserProfileService>().As<IUserProfileService>().InstancePerLifetimeScope();
        builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();

        // Other services
        builder.RegisterType<CurrentUserService>().As<ICurrentUserService>().InstancePerLifetimeScope();
        builder.RegisterType<FileHandlerService>().As<IFileHandlerService>().InstancePerLifetimeScope();
        builder.RegisterType<EmailService>().As<IEmailService>().InstancePerLifetimeScope();
        builder.RegisterType<DateTimeService>().As<IDateTimeService>().InstancePerLifetimeScope();

        base.Load(builder);
    }
}
