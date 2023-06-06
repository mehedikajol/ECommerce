using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Application.IUnitOfWorks;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using Mapster;
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
            var profile = entity.Adapt<UserProfile>();
            profile.Gender = (int)entity.Gender;
            profiles.Add(profile);
        }

        return profiles;
    }

    public async Task<UserProfile> GetUserProfileById(Guid id)
    {
        var entity = await _unitOfWork.UserProfiles.GetEntityById(id);
        var profile = entity.Adapt<UserProfile>();
        profile.Gender = (int)entity.Gender;

        return profile;
    }

    public async Task<UserProfile> GetUserProfileByIdentityId(Guid id)
    {
        var entity = await _unitOfWork.UserProfiles.GetProfileByIdentityId(id);
        var profile = entity.Adapt<UserProfile>();
        profile.Gender = (int)entity.Gender;

        return profile;
    }

    public async Task CreateUserProfile(UserProfile profile)
    {
        var userProfileEntity = profile.Adapt<EO.UserProfile>();
        userProfileEntity.Gender = (Gender)profile.Gender;

        await _unitOfWork.UserProfiles.AddEntity(userProfileEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task UpdateUserProfile(UserProfile profile)
    {
        var profileEntity = await _unitOfWork.UserProfiles.GetEntityById(profile.Id);
        if(profileEntity is null)
                throw new NotFoundException("Profile not found.");

        profileEntity.FirstName = profile.FirstName;
        profileEntity.LastName = profile.LastName;
        profileEntity.Gender = (Gender)profile.Gender;
        profileEntity.Email = profile.Email;
        profileEntity.Phone = profile.Phone;
        profileEntity.Address = profile.Address;
        profileEntity.ProfilePictureUrl = profile.ProfilePictureUrl ?? profileEntity.ProfilePictureUrl;

        await _unitOfWork.UserProfiles.UpdateEntity(profileEntity);
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteUserProfile(Guid id)
    {
        await _unitOfWork.UserProfiles.DeleteEntityById(id);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<int> GetUsersCount()
    {
        return await _unitOfWork.UserProfiles.GetCount();
    }
}
