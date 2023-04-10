using Autofac;
using ECommerce.Infrastructure.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ECommerce.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>().As<IdentityDbContext>().InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
