using ECommerce.Application.IRepositories;
using ECommerce.Core.Entities;
using ECommerce.Infrastructure.Context;
using ECommerce.Infrastructure.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories;

internal class UserProfileRepository : GenericRepository<UserProfile, Guid>, IUserProfileRepository
{
    public UserProfileRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<UserProfile> GetProfileByIdentityId(Guid id)
    {
        return await _dbSet.Where(up => up.UserId == id).FirstOrDefaultAsync();
    }
}
