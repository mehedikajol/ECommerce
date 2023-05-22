using ECommerce.Application.IServices;
using ECommerce.Core.Entities;
using ECommerce.Core.Entities.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

namespace ECommerce.Infrastructure.Context;

public class AppDbContext : IdentityDbContext
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        _currentUserService = this.GetService<ICurrentUserService>();
        _dateTimeService = this.GetService<IDateTimeService>();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.InsertedDate = _dateTimeService.UTCDateTime();
                    entry.Entity.InsertedBy = _currentUserService.GetCurrentUserEmail() ?? entry.Entity.InsertedBy;
                    entry.Entity.ModifiedBy = "";
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedDate = _dateTimeService.UTCDateTime();
                    entry.Entity.ModifiedBy = _currentUserService.GetCurrentUserEmail() ?? entry.Entity.ModifiedBy;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<Order> Orders { get; set; }
}
