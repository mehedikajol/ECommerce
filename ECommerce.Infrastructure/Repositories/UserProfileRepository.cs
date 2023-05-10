using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;

namespace ECommerce.Infrastructure.Repositories;

internal class UserProfileRepository : GenericRepository<UserProfile, Guid>, IUserProfileRepository
{
    public UserProfileRepository(AppDbContext context) : base(context)
    {
    }
}
