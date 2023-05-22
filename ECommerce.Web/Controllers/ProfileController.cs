using ECommerce.Application.BusinessEntities;
using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using ECommerce.Core.Enums;
using ECommerce.Core.Exceptions;
using ECommerce.Web.Extensions;
using ECommerce.Web.Helpers;
using ECommerce.Web.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ECommerce.Web.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly IUserProfileService _profileService;
    private readonly IFileHandlerService _fileHandlerService;
    private readonly FileStorageSettings _settings;

    public ProfileController(
        IUserProfileService profileService,
        IFileHandlerService fileHandlerService,
        IOptions<FileStorageSettings> options)
    {
        _profileService = profileService;
        _fileHandlerService = fileHandlerService;
        _settings = options.Value;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Orders()
    {
        return View();
    }

    public async Task<IActionResult> Details()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var profile = await _profileService.GetUserProfileByIdentityId(new Guid(userId));
        var model = new ProfileDetailsModel
        {
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Email = profile.Email,
            Phone = profile.Phone,
            Gender = Enum.GetName(typeof(Gender), profile.Gender),
            Address = profile.Address,
            ProfilePictureUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, profile.ProfilePictureUrl)
        };
        return View(model);
    }

    public async Task<IActionResult> Edit()
    {
        var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var entity = await _profileService.GetUserProfileByIdentityId(new Guid(userId));
        var model = new ProfileEditModel
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            Phone = entity.Phone,
            ProfilePictureUrl = FileLinkModifier.GenerateImageLink(Request, _settings.DirectoryName, entity.ProfilePictureUrl),
            Address = entity.Address,
            Gender = entity.Gender
        };

        var genders = from Gender s in Enum.GetValues(typeof(Gender))
                      select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["Genders"] = new SelectList(genders, "Id", "Name");

        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProfileEditModel model)
    {
        var genders = from Gender s in Enum.GetValues(typeof(Gender))
                      select new { Id = s.GetHashCode(), Name = s.ToString() };
        ViewData["Genders"] = new SelectList(genders, "Id", "Name");

        if (ModelState.IsValid)
        {
            try
            {
                if (model.ImageFile is not null)
                {
                    var bytes = await model.ImageFile.ToByteArray();
                    var imageUrl = await _fileHandlerService.SaveFileAsync(bytes, model.ImageFile.FileName, UploadImageType.ProfileImage);
                    model.ProfilePictureUrl = imageUrl;
                    var oldEntity = await _profileService.GetUserProfileById(model.Id);
                    await _fileHandlerService.DeleteFileAsync(oldEntity.ProfilePictureUrl);
                }

                var profile = new UserProfile
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Phone = model.Phone,
                    Gender = model.Gender,
                    Address = model.Address,
                    ProfilePictureUrl = model.ProfilePictureUrl
                };

                await _profileService.UpdateUserProfile(profile);
                return RedirectToAction(nameof(Details));
            }
            catch (NotFoundException)
            {
                return Redirect(url: "/Errors/Notfound");
            }
            catch (Exception)
            {
                return Redirect(url: "/Errors/InternalServerError");
            }
        }

        return View(model);
    }

}