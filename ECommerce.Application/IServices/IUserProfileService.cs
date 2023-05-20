using ECommerce.Application.BusinessEntities;

namespace ECommerce.Application.IServices;

public interface IUserProfileService
{
    Task<IEnumerable<UserProfile>> GetAllUserProfiles();
    Task<UserProfile> GetUserProfileById(Guid id);
    Task<UserProfile> GetUserProfileByIdentityId(Guid id);
    Task CreateUserProfile(UserProfile profile);
    Task UpdateUserProfile(UserProfile profile);
    Task DeleteUserProfile(Guid id);
}
