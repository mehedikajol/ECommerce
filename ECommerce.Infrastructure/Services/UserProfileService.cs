using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;

namespace ECommerce.Infrastructure.Services;

internal class UserProfileService : IUserProfileService
{
    public Task<IEnumerable<UserProfile>> GetAllUserProfiles()
    {
        throw new NotImplementedException();
    }

    public Task<UserProfile> GetUserProfileById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task CreateUserProfile(UserProfile profile)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserProfile(UserProfile profile)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserProfile(Guid id)
    {
        throw new NotImplementedException();
    }
}
