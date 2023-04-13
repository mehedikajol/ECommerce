using Autofac;
using ECommerce.Application.IRepositories;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.UnitOfWorks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ECommerce.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>().As<IdentityDbContext>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerLifetimeScope();


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
