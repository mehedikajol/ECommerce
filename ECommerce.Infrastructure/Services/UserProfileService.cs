using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Enums;
using EO = ECommerce.Core.Entities;

namespace ECommerce.Infrastructure.Services;

internal class UserProfileService : IUserProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserProfileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<UserProfile>> GetAllUserProfiles()
    {
        var entities = await _unitOfWork.UserProfiles.GetAllEntities();
        var profiles = new List<UserProfile>();
        
        foreach (var entity in entities)
        {
            profiles.Add(new UserProfile
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Gender = (int)entity.Gender,
                ProfilePictureUrl = entity.ProfilePictureUrl,
                UserId = entity.UserId,
                Address = entity.Address,
            });
        }

        return profiles;
    }

    public async Task<UserProfile> GetUserProfileById(Guid id)
    {
        var entity = await _unitOfWork.UserProfiles.GetEntityById(id);
        var userProfile = new UserProfile
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Phone,
            Gender = (int)entity.Gender,
            ProfilePictureUrl = entity.ProfilePictureUrl,
            UserId = entity.UserId,
            Address = entity.Address,
        };
        return userProfile;
    }

    public async Task<UserProfile> GetUserProfileByIdentityId(Guid id)
    {
        var entity = await _unitOfWork.UserProfiles.GetProfileByIdentityId(id);
        var userProfile = new UserProfile
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Phone,
            Gender = (int)entity.Gender,
            ProfilePictureUrl = entity.ProfilePictureUrl,
            UserId = entity.UserId,
            Address = entity.Address,
        };
        return userProfile;
    }

    public async Task CreateUserProfile(UserProfile profile)
    {
        var userProfileEntity = new EO.UserProfile
        {
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Gender = (Gender)profile.Gender,
            Address = profile.Address,
            Email = profile.Email,
            Phone = profile.Phone,
            ProfilePictureUrl = profile.ProfilePictureUrl,
            UserId = profile.UserId,
            InsertedBy = profile.InsertedBy
        };
        await _unitOfWork.UserProfiles.AddEntity(userProfileEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateUserProfile(UserProfile profile)
    {
        var profileEntity = await _unitOfWork.UserProfiles.GetEntityById(profile.Id);

        profileEntity.FirstName = profile.FirstName;
        profileEntity.LastName = profile.LastName;
        profileEntity.Gender = (Gender)profile.Gender;
        profileEntity.Address = profile.Address;
        profileEntity.ProfilePictureUrl = profile.ProfilePictureUrl;

        await _unitOfWork.UserProfiles.UpdateEntity(profileEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteUserProfile(Guid id)
    {
        await _unitOfWork.UserProfiles.DeleteEntityById(id);
        await _unitOfWork.CompleteAsync();
    }
}
