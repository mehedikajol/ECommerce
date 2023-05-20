using ECommerce.Application.IGenericRepositories;
using ECommerce.Core.Entities;

namespace ECommerce.Application.IRepositories;

public interface IUserProfileRepository : IGenericRepository<UserProfile, Guid>
{
    Task<UserProfile> GetProfileByIdentityId(Guid id);
}
